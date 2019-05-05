namespace Nerds.Library.Business
{
    public class AvailabilityDetailsDTO
    {
        public string UniqueBarcode { get; set; }

        public bool IsAvailable { get; set; }

        static internal AvailabilityDetailsDTO FromAvailability(Availability availabilty)
        {
            return new AvailabilityDetailsDTO
            {
                UniqueBarcode = availabilty.UniqueBarcode,
                IsAvailable = availabilty.Reservation == null,
            };
        }
    }
}
