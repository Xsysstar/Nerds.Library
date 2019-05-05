using Nerds.Library.Books;

namespace Nerds.Library.Business
{
    public class AvailabilityDetailsDTO
    {
        public BookDetailsDTO BookDetails { get; set; }

        public bool IsAvailable { get; set; }

        static internal AvailabilityDetailsDTO FromAvailability(Availability availabilty)
        {
            return new AvailabilityDetailsDTO
            {
                BookDetails = BookDetailsDTO.FromBook(availabilty.Book),
                IsAvailable = availabilty.Reservation == null,
            };
        }
    }
}
