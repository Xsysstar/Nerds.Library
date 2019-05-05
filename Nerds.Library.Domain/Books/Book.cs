using System;

namespace Nerds.Library.Books
{
    /// <summary>
    /// A single instance of a book. Every instance is at one physical location at a time.
    /// </summary>
    public sealed class Book
    {
        /// <summary>
        /// The globally unique identifier of this instance.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// The unique barcode to distinguish instances of the same book. Can be same as <see cref="Id"/>.
        /// </summary>
        public string UniqueBarcode { get; set; }
    }
}
