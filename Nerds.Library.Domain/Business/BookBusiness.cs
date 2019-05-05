using Nerds.Library.Books;
using System;
using System.Collections.Generic;
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

        public BookBusiness(Organization organization, BookTemplate bookTemplate)
        {
            Debug.Assert(organization != null, "organization != null");
            Debug.Assert(bookTemplate != null, "bookTemplate != null");
            this.organization = organization;
            BookTemplate = bookTemplate;
        }

        public BookTemplate BookTemplate { get; private set; }

        private IQueryable<Book> Books => organization.OwnedBooks.Where(b => b.Template == BookTemplate);

        /// <summary>
        /// Tries to get an instance of <see cref="BookTemplate"/> that should be available on
        /// <paramref name="availableOn"/> (for <paramref name="customer"/>).
        /// </summary>
        /// <param name="availableOn">The moment the book should be available.</param>
        /// <param name="customer">The customer, in case he has existing reservations.</param>
        /// <returns>The list of every available <see cref="Book"/>.</returns>
        public IEnumerable<Book> GetAvailableBooks(DateTimeOffset availableOn, Customer customer = null)
        {
            // TODO: filter by availability and customer
            return Books;
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
