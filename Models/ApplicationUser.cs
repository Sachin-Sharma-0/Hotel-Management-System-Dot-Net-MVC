using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelMgmtSys.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Key]
        public int Id { get; set; }

        public string FullName { get; set; }
        public double MobileNumber { get; set; }
        public string IdProof { get; set; }
        public string Gender { get; set; }
    }
}
