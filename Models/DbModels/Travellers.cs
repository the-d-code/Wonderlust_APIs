using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WONDERLUST_PROJECT_APIs.Models.DbModels
{
    public class Travellers
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TravellersId { get; set; }

        public int PackageBookingId { get; set; }
        public int PackageId { get; set; }
        public string UserId { get; set; }

        public string FullName { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }
        public long AadharCardNo { get; set; }
        public long ContactNo { get; set; }
        public string BloodGroup { get; set; }
        public DateTime Dob { get; set; }

        public DateTime CreatedAt { get; set; }

        public virtual Package Package { get; set; }
        public virtual PackageBooking Pb { get; set; }
        public virtual User User { get; set; }
    }
}
