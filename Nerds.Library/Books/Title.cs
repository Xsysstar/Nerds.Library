namespace Nerds.Library.Books
{
    /// <summary>
    /// The title metadata of a book
    /// </summary>
    internal sealed class Title
    {
        /// <summary>
        /// The main caption.
        /// </summary>
        /// <example>Harry Potter and the Secret Chamber</example>
        public string Caption { get; set; }

        /// <summary>
        /// A (rare) subcaption usually omitted when referring to the book. Usually denotes a more
        /// elaborate description of the <see cref="Caption"/> in a smaller font or after a colon.
        /// </summary>
        /// <example>A magical story.</example>
        public string Subcaption { get; set; }
    }
}
