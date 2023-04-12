namespace HotelMgmtSys.Models
{
    public class RoomSearch
    {
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public TimeSpan FromTime { get; set; }
        public TimeSpan ToTime { get; set; }
        public string RoomType { get; set; }
        public int Fare { get; set; }
        public int Capacity { get; set; }
        public int RoomId { get; set; }
    }
}
