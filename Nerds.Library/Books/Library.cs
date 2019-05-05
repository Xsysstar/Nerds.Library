using System.Linq;

namespace Nerds.Library.Books
{
    /// <summary>
    /// A library under management of this system.
    /// </summary>
    internal sealed class Library
    {
        /// <summary>
        /// The collection of owned books.
        /// </summary>
        public IQueryable<Book> OwnedBooks { get; set; }
    }
}
