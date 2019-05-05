namespace Nerds.Library.Business
{
    public sealed class Availability
    {
        public Reservation Reservation { get; set; }
        public Books.Book Book { get; set; }
    }
}
