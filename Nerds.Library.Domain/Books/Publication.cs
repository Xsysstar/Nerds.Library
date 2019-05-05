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
        public DateTime PublicationDate { get; set; }

        /// <summary>
        /// The publisher performing this
        /// </summary>
        public Publisher Publisher { get; set; }

        /// <summary>
        /// The ISBN-10 (legacy).
        /// </summary>
        public string ISBN10 { get; set; }

        /// <summary>
        /// The ISBN-13 (modern).
        /// </summary>
        public string ISBN13 { get; set; }

        // One could imagine more publication-related properties, like:
        // -- public string Edition { get; set; }
        // -- public string Print { get; set; }
    }
}
