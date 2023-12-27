using Microsoft.VisualBasic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WONDERLUST_PROJECT_APIs.Models.DbModels
{
    public class Package
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PackageId { get; set; }
        public string PackageName { get; set; }
        public int Amount { get; set; }
      
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public int NoOfNights { get; set; }
        public int NoOfDays { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
       

        public int CategoryId { get; set; }

        public int CountryId { get; set; }
        public int StateId { get; set; }
        public int CityId { get; set; }

        public int BusId { get; set; }
        public int HotelId { get; set; }
        
        public bool IsBlock { get; set; }
        public DateTime? CreatedAt { get; set; }



        public virtual Bus Bus { get; set; }
        public virtual Category Category { get; set; }
        public virtual Hotel Hotel { get; set; }
        public virtual State State { get; set; }
        public virtual City City { get; set; }
        public virtual Country Country { get; set; }

    }

}
