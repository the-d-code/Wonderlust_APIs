using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WONDERLUST_PROJECT_APIs.Models.DbModels
{
    public class City
    {
        //public City()
        //{
        //   // Bus = new HashSet<Bus>();
        //    //Hotel = new HashSet<Hotel>();
        //    //Package = new HashSet<Package>();
        //    //SubPlace = new HashSet<SubPlace>();
        //}
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CityId { get; set; } 
        public string CityName { get; set; }
       
        [Display(Name = "State")]
        public int StateId { get; set; }
      

        public bool IsBlock { get; set; }
        public DateTime? CreatedAt { get; set; }
     
       
        [ForeignKey("StateId")]
        public virtual State State { get; set; }
        //public virtual ICollection<Bus> Bus { get; set; }
        //public virtual ICollection<Hotel> Hotel { get; set; }
        //public virtual ICollection<Package> Package { get; set; }
        //public virtual ICollection<SubPlace> SubPlace { get; set; }
    }
}
