using System;

namespace Nerds.Library.Business
{
    public sealed class Availability
    {
        public Guid BookId { get; set; }

        /// <summary>
        /// Simply a cached value of <see cref="Books.Book.UniqueBarcode"/>
        /// </summary>
        public string UniqueBarcode { get; set; }

        public Reservation Reservation { get; set; }
    }
}
