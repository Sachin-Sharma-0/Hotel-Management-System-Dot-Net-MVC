using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace HotelMgmtSys.Models
{
    public class AssignRoomViewModel
    {
        public int RoomId { get; set; }

        [Required]
        [Display(Name = "Check-in Date")]
        [DataType(DataType.Date)]
        public DateTime CheckInDate { get; set; }

        [Required]
        [Display(Name = "Check-out Date")]
        [DataType(DataType.Date)]
        public DateTime CheckOutDate { get; set; }

        [Required]
        [Display(Name = "Number of Guests")]
        public int NumOfGuests { get; set; }

        public List<SelectListItem> Rooms { get; set; }
    }
}
