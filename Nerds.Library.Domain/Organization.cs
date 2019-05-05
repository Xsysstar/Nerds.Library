using Nerds.Library.Books;
using System.Collections.Generic;

namespace Nerds.Library
{
    /// <summary>
    /// A library under management of this system.
    /// </summary>
    public sealed class Organization
    {
        /// <summary>
        /// The collection of owned books.
        /// </summary>
        public ICollection<Book> OwnedBooks { get; set; }

        // TODO:
        /////// <summary>
        /////// The book businesses.
        /////// </summary>
        ////public IQueryable<BookBusiness> BookBusinesses { get; set; }
    }
}
