namespace HotelMgmtSys.Models
{
    public class RoomBookingViewModel
    {
        public int RoomId { get; set; }
        public int UserId { get; set; }
        public string RoomType { get; set; }
        public Room Room { get; set; }
        public RoomBooking Booking { get; set; }
        public decimal Fare { get; set; }
        public double TotalFare { get; set; }
        public int Capacity { get; set; }
        public bool IsBooked { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public TimeSpan FromTime { get; set; }
        public TimeSpan ToTime { get; set; }
        public DateTime BookingDate { get; set; }
        public bool GuestCheckout { get; set; }
        public bool IsSignUp { get; set; }
    }
}
