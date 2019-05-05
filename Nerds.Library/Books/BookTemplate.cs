using System;
using System.Linq;

namespace Nerds.Library.Books
{
    /// <summary>
    /// A template for <see cref="Book"/>.
    /// </summary>
    internal sealed class BookTemplate
    {
        /// <summary>
        /// The globally unique identifier of this instance.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// The publication metadata of these books.
        /// </summary>
        public Publication Publication { get; set; }

        /// <summary>
        /// The title metadata of these books.
        /// </summary>
        public Title Title { get; set; }

        /// <summary>
        /// The authors of these books.
        /// </summary>
        public IQueryable<Author> Authors { get; set; }

        /// <summary>
        /// The genres of these books.
        /// </summary>
        public IQueryable<Genre> Genres { get; set; }
    }
}
