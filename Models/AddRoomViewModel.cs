using System.ComponentModel.DataAnnotations;

namespace HotelMgmtSys.Models
{
    public class AddRoomViewModel
    {
        [Required]
        public string RoomType { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        public int Price { get; set; }
    }
}
