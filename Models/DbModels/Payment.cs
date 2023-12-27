using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WONDERLUST_PROJECT_APIs.Models.DbModels
{
    public class Payment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PaymetId { get; set; }

        public int PackageBookingId { get; set; }
        public int PackageId { get; set; }
        public string UserId { get; set; }

        
        public int Amount { get; set; }
      
        public bool PayemtMode { get; set; }
        
        public DateTime CreatedAt { get; set; }
       
        public virtual Package Package { get; set; }
        public virtual PackageBooking PackageBooking { get; set; }
        public virtual User User { get; set; }
    }
}
