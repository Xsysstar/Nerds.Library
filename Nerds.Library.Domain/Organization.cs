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
        private readonly ICollection<Customer> customers;

        public Organization(ICollection<Book> ownedBooks = null, ICollection<BookBusiness> bookBusinesses = null, ICollection<Customer> customers = null)
        {
            this.ownedBooks = ownedBooks ?? new HashSet<Book>();
            this.bookBusinesses = bookBusinesses ?? new HashSet<BookBusiness>();
            this.customers = customers ?? new HashSet<Customer>();
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
        /// The <see cref="Customer"/> s.
        /// </summary>
        public IQueryable<Customer> Customers => customers.AsQueryable();

        /// <summary>
        /// Adds a new book to the organization.
        /// </summary>
        /// <param name="book">The book.</param>
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

        /// <summary>
        /// Adds a new customer to the organization.
        /// </summary>
        /// <param name="customer">The customer.</param>
        public void AddCustomer(Customer customer)
        {
            if (customers.Contains(customer))
            {
                throw new InvalidOperationException("Customer already contained");
            }

            customers.Add(customer);
        }
    }
}
