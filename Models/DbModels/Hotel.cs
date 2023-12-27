using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace WONDERLUST_PROJECT_APIs.Models.DbModels
{
    public class Hotel
    {
        public Hotel()
        {
            //Package = new HashSet<Package>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int HotelId { get; set; }
        public string HotelName { get; set; }
       

        public int CountryId { get; set; }
        public int StateId { get; set; }
        public int CityId { get; set; }

        public string Image { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }

        public DateTime CreatedAt { get; set; }

        public virtual Country Country { get; set; }
        public virtual State State { get; set; }
        public virtual City City { get; set; }  


    }
}
