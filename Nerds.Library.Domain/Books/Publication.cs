using System;

namespace Nerds.Library.Books
{
    /// <summary>
    /// The publication metadata of a book.
    /// </summary>
    public sealed class Publication
    {
        /// <summary>
        /// The moment of publication.
        /// </summary>
        public DateTimeOffset PublicationDate { get; set; }

        /// <summary>
        /// The publisher performing this
        /// </summary>
        public Publisher Publisher { get; set; }

        /// <summary>
        /// The ISBN-13.
        /// </summary>
        /// <example>978-3-16-148410-0</example>
        public string ISBN { get; set; }

        // One could imagine more publication-related properties, like:
        // -- public string Edition { get; set; }
        // -- public string Print { get; set; }
    }
}
