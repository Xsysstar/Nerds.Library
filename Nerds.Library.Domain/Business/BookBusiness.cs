using Nerds.Library.Books;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;

namespace Nerds.Library.Business
{
    /// <summary>
    /// The customer-related business around the instances of the same book (template).
    /// </summary>
    public sealed class BookBusiness
    {
        private readonly Organization organization;

        private readonly ICollection<Reservation> reservations;

        public BookBusiness(BookTemplate bookTemplate, Organization organization, ICollection<Reservation> reservations = null)
        {
            Debug.Assert(bookTemplate != null, "bookTemplate != null");
            Debug.Assert(organization != null, "organization != null");
            BookTemplate = bookTemplate;
            this.organization = organization;
            this.reservations = reservations ?? new Collection<Reservation>();
        }

        public BookTemplate BookTemplate { get; private set; }

        public IQueryable<Book> Books => organization.OwnedBooks.Where(b => b.Template == BookTemplate);

        public IQueryable<Reservation> Reservations => reservations.AsQueryable();

        /// <summary>
        /// Tries to get an instance of <see cref="BookTemplate"/> that should be available on
        /// <paramref name="availableOn"/>.
        /// </summary>
        /// <param name="availableOn">The moment the book should be available.</param>
        /// <returns>The list of every available <see cref="Book"/>.</returns>
        public IEnumerable<Availability> GetBookAvailabilities(DateTimeOffset availableOn)
        {
            var applicableReservations = Reservations.Where(r => r.BeginTerm <= availableOn && availableOn <= r.EndTerm && !r.IsBookReturned);
            var availabilities = Books.Select(b => new Availability
            {
                Book = b,
                Reservation = applicableReservations.FirstOrDefault(r => r.BookId == b.Id)
            });

            return availabilities;
        }

        /// <summary>
        /// Reserve the <paramref name="book"/> for <paramref name="customer"/>.
        /// </summary>
        /// <param name="book">The book.</param>
        /// <param name="customer">The customer.</param>
        /// <returns>A <see cref="Reservation"/> if successful.</returns>
        /// <exception cref="InvalidOperationException">
        /// Thrown if book is not available for this customer.
        /// </exception>
        public Reservation ReserveBook(Book book, Customer customer)
        {
            Debug.Assert(book != null, "book != null");
            Debug.Assert(customer != null, "customer != null");

            var reservationMoment = DateTimeOffset.Now;

            var availabilities = GetBookAvailabilities(reservationMoment);
            var conflictingReservation = availabilities.FirstOrDefault(a => a.Book == book && a.Reservation != null)?.Reservation;
            if (conflictingReservation != null && conflictingReservation.Customer != customer)
            {
                throw new InvalidOperationException("This book is not available for this customer");
            }

            var reservation = new Reservation
            {
                BeginTerm = reservationMoment,
                EndTerm = reservationMoment.AddDays(14),
                BookId = book.Id,
                Customer = customer,
                IsBookTaken = false,
                IsBookReturned = false,
            };
            reservations.Add(reservation);

            Debug.Assert(GetBookAvailabilities(reservationMoment).Any(a => a.Book == book && a.Reservation == reservation),
                "GetBookAvailability(reservationMoment).Any(a => a.BookId == book.Id && a.Reservation == reservation)");

            return reservation;
        }

        public void TakeBook(Book book, Customer customer)
        {
            var takingMoment = DateTimeOffset.Now;
            var availability = GetBookAvailabilities(takingMoment).FirstOrDefault(a => a.Book == book);

            var reservation = availability.Reservation;
            if (reservation == null)
            {
                reservation = ReserveBook(book, customer);
            }
            else if (reservation.Customer != customer)
            {
                throw new InvalidOperationException("This book was already reserved for another customer and cannot be taken");
            }

            reservation.IsBookTaken = true;
        }

        /// <summary>
        /// Return the <paramref name="book"/> by the <paramref name="customer"/>.
        /// </summary>
        /// <param name="book">The book to return.</param>
        /// <param name="customer">
        /// The customer that was expected to hold it. To omit this check, leave <c>null</c>.
        /// </param>
        public void ReturnBook(Book book, Customer customer = null)
        {
            var returningMoment = DateTimeOffset.Now;
            var availability = GetBookAvailabilities(returningMoment).FirstOrDefault(a => a.Book == book);

            var reservation = availability.Reservation;
            if (reservation == null)
            {
                throw new InvalidOperationException("This book was not reserved in the first place");
            }

            if (customer != null && reservation.Customer != customer)
            {
                throw new InvalidOperationException("This book was reserved by another customer");
            }

            reservation.IsBookReturned = true;

            Debug.Assert(GetBookAvailabilities(returningMoment).Any(a => a.Book == book && a.Reservation == null),
                "GetBookAvailability(reservationMoment).Any(a => a.BookId == book.Id && a.Reservation == null)");
        }

        // TODO:
        // - add CancelReservation
    }
}
