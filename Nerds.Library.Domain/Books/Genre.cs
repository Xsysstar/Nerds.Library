using System;

namespace Nerds.Library.Books
{
    /// <summary>
    /// The genre of a book.
    /// </summary>
    public sealed class Genre
    {
        /// <summary>
        /// The globally unique identifier of this instance.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// The caption of this Genre.
        /// </summary>
        public string Caption { get; set; }

        // TODO: make Caption translatable
    }
}
