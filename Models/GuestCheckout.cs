using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace HotelMgmtSys.Models
{
    public enum Gender
    {
        Male,
        Female,
        Other
    }
    public class GuestCheckout
    {
        [Required]
        [Display(Name = "Full Name")]
        public string FullName { get; set; }
        public string Name { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public string RoomType { get; set; }
        public int NumberOfGuests { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.PhoneNumber)]
        public string Mobile { get; set; }

        [Required]
        [Display(Name = "ID Proof")]
        public string IDProof { get; set; }
        public int Id { get; set; }

        [Required]
        public Gender Gender { get; set; }
    }
}
