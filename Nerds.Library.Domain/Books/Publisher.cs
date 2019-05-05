using System;

namespace Nerds.Library.Books
{
    /// <summary>
    /// A publisher of books.
    /// </summary>
    public sealed class Publisher
    {
        /// <summary>
        /// The globally unique identifier of this instance.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// The name of the publisher.
        /// </summary>
        public string Name { get; set; }
    }
}
