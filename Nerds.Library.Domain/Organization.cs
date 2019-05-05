using Nerds.Library.Books;
using Nerds.Library.Business;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Nerds.Library
{
    /// <summary>
    /// A library under management of this system.
    /// </summary>
    public sealed class Organization
    {
        private readonly ICollection<Book> ownedBooks;
        private readonly ICollection<BookBusiness> bookBusinesses;

        public Organization(ICollection<Book> ownedBooks = null, ICollection<BookBusiness> bookBusinesses = null)
        {
            this.ownedBooks = ownedBooks ?? new HashSet<Book>();
            this.bookBusinesses = bookBusinesses ?? new HashSet<BookBusiness>();
        }

        /// <summary>
        /// The collection of every <see cref="Book"/>.
        /// </summary>
        public IQueryable<Book> OwnedBooks => ownedBooks.AsQueryable();

        /// <summary>
        /// The book businesses, one per <see cref="BookTemplate"/>.
        /// </summary>
        public IQueryable<BookBusiness> BookBusinesses => bookBusinesses.AsQueryable();

        /// <summary>
        /// Adds a new book to the organization.
        /// </summary>
        /// <param name="book"></param>
        public void AddBook(Book book)
        {
            if (ownedBooks.Contains(book))
            {
                throw new InvalidOperationException("Book already contained");
            }

            ownedBooks.Add(book);
            var business = bookBusinesses.FirstOrDefault(b => b.BookTemplate == book.Template);
            if (business == null)
            {
                business = new BookBusiness(book.Template, this);
                bookBusinesses.Add(business);
            }
        }
    }
}
