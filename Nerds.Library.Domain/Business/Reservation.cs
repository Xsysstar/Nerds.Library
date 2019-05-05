using System;

namespace Nerds.Library.Business
{
    public sealed class Reservation
    {
        public DateTimeOffset BeginTerm { get; set; }

        public DateTimeOffset EndTerm { get; set; }

        /// <summary>
        /// The unique book instance.
        /// </summary>
        public Guid BookId { get; set; }

        public bool IsBookTaken { get; set; }

        public bool IsBookReturned { get; set; }

        public Customer Customer { get; set; }
    }
}
