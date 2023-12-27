using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WONDERLUST_PROJECT_APIs.Models.DbModels
{
    public class Enquiry
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EnquiryId { get; set; }
        public string UserId { get; set; }
        public long ContactNumber { get; set; }
        public string EmailId { get; set; }
        public string Message { get; set; }
        public bool IsBlock { get; set; }
        public DateTime? CreatedAt { get; set; }


        public virtual User User { get; set; }
    }
}
