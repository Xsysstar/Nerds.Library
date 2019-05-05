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
        /// <paramref name="availableOn"/> (for <paramref name="customer"/>).
        /// </summary>
        /// <param name="availableOn">The moment the book should be available.</param>
        /// <param name="customer">The customer, in case he has existing reservations.</param>
        /// <returns>The list of every available <see cref="Book"/>.</returns>
        public IEnumerable<Availability> GetBookAvailability(DateTimeOffset availableOn, Customer customer = null)
        {
            var conflictingReservations = Reservations.Where(r => r.BeginTerm < availableOn && availableOn < r.EndTerm && r.Customer != customer && !r.IsBookReturned);
            var unavailableBookIds = conflictingReservations.Select(r => r.BookId);

            var availabilities = Books.Select(b => new Availability
            {
                BookId = b.Id,
                UniqueBarcode = b.UniqueBarcode,
                IsAvailable = !unavailableBookIds.Contains(b.Id)
            });
            return availabilities;
        }

        /// <summary>
        /// Reserve the <paramref name="book"/> for <paramref name="customer"/>.
        /// </summary>
        /// <param name="book">The book.</param>
        /// <param name="customer">The customer.</param>
        /// <returns>A <see cref="Reservation"/> if successful.</returns>
        public Reservation ReserveBook(Book book, Customer customer)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Return the <paramref name="book"/> by the <paramref name="customer"/>.
        /// </summary>
        /// <param name="book">The book to return.</param>
        /// <param name="customer">
        /// The customer that was expected to hold it. To omit this check, leave <c>null</c>.
        /// </param>
        /// <returns></returns>
        public Reservation ReturnBook(Book book, Customer customer = null)
        {
            throw new NotImplementedException();
        }
    }
}
