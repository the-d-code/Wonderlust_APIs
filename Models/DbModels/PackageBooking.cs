using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WONDERLUST_PROJECT_APIs.Models.DbModels
{
    public class PackageBooking
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PackageBookingId { get; set; }
        public int PackageId { get; set; }
        
        public string UserId { get; set; }
        public string EmailId { get; set; }
        public long ContactNumber { get; set; }
        public DateTime BookingDate { get; set; }
        public int NoOfTravelers { get; set; }
        // public string ModeOfPayment { get; set; }


        public DateTime CreatedAt { get; set; }

        public virtual Package Package { get; set; }
        public virtual User User { get; set; }
    }
}
