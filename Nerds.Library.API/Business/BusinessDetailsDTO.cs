using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Nerds.Library.Business
{
    public sealed class BusinessDetailsDTO
    {
        public DateTimeOffset? PublicationDate { get; set; }
        public string ISBN { get; set; }
        public string Publisher { get; set; }

        public string Title { get; set; }
        public IEnumerable<string> Authors { get; set; }
        public IEnumerable<string> Genres { get; set; }

        public IEnumerable<AvailabilityDetailsDTO> Availability { get; set; }

        static internal BusinessDetailsDTO FromBook(BookBusiness business)
        {
            return FromBook(business, DateTimeOffset.Now);
        }

        static private BusinessDetailsDTO FromBook(BookBusiness business, DateTimeOffset availabilityCheckMoment)
        {
            Debug.Assert(business != null, "business != null");
            Debug.Assert(business.BookTemplate != null, "business.BookTemplate != null");
            return new BusinessDetailsDTO
            {
                ISBN = business.BookTemplate.Publication?.ISBN,
                Publisher = business.BookTemplate.Publication?.Publisher?.Name,
                PublicationDate = business.BookTemplate.Publication?.PublicationDate,
                Title = business.BookTemplate.Title?.Caption,
                Authors = business.BookTemplate.Authors?.Select(a => a.FullName)?.ToArray(),
                Genres = business.BookTemplate.Genres?.Select(a => a.Caption)?.ToArray(),
                Availability = business.GetBookAvailability(availabilityCheckMoment).Select(AvailabilityDetailsDTO.FromAvailability)
            };
        }
    }
}
