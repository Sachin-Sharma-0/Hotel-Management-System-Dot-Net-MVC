using System.ComponentModel.DataAnnotations;
using HotelMgmtSys.Models;

namespace HotelMgmtSys.Models
{
    public class Room
    {
        public enum RoomCategory
        {
            Standard,
            Deluxe,
            Suite
        }
        public RoomCategory Category { get; set; }

        public enum RoomStatus
        {
            Available,
            Occupied,
            Reserved,
            OutOfService
        }

        public RoomStatus Status { get; set; }

        [Required]
        public string RoomType { get; set; }
        // public string Status { get; set; }


        [Required]
        public string Description { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        public int Price { get; set; }
        public bool IsBooked { get; set; }
        [Key]
        public int Id { get; set; }
        public int RoomId { get; set; }
        public int Capacity { get; set; }
        public List<Booking> Bookings { get; set; }

        public string RoomNumber { get; set; }

    }
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string MobileNumber { get; set; }
        public string Role { get; set; }
        public string PhotoUrl { get; set; }
    }

    public class EntryLog
    {
        public int Id { get; set; }
        public int GuestId { get; set; }
        public int RoomId { get; set; }
        public DateTime EntryTime { get; set; }
        public DateTime DateTime { get; set; }
    }

    public class ExitLog
    {
        public int Id { get; set; }
        public int GuestId { get; set; }
        public int RoomId { get; set; }
        public DateTime ExitTime { get; set; }
        public DateTime DateTime { get; set; }
    }

}

