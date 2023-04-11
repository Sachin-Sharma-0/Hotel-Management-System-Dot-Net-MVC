namespace HotelMgmtSys.Models
{
    public class Booking
    {
        public int Id { get; set; }
        public int RoomId { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public bool IsPaid { get; set; }
        public virtual Room Room { get; set; }
        public virtual Client Client { get; set; }
        public int RoomNumber { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public DateTime BookingDate { get; set; }
        public string Status { get; set; }
    }

    public class Client
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Room Room { get; set; }
        public int RoomId { get; set; }
        public int TotalCost { get; set; }
        public int OccupiedHours { get; set; }
        public string IdProof { get; set; }
        public string Status { get; set; }
        public string PaymentStatus { get; set; }
        public ICollection<Booking> Bookings { get; set; }
        // other properties
    }

    public class ClientDashboardViewModel
    {
        public Client Client { get; set; }
        public List<Booking> CurrentBookings { get; set; }
        public List<Booking> PastBookings { get; set; }
    }
}
