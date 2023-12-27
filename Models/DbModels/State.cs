using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WONDERLUST_PROJECT_APIs.Models.DbModels
{
    public class State
    {
        //public State()
        //{
        //    City = new HashSet<City>();
        //    //Hotel = new HashSet<Hotel>();
        //    //Package = new HashSet<Package>();
        //    //Place = new HashSet<Place>();
        //    //SubPlace = new HashSet<SubPlace>();
        //}

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int StateId { get; set; }
       
        public string StateName { get; set; }

        [Display(Name = "Country")]
        public int CountryId { get; set; }
       

        public bool IsBlock { get; set; }
        public DateTime? CreatedAt { get; set; }
      
        [ForeignKey("CountryId")]
        public virtual Country Country { get; set; }
       
       // public virtual ICollection<City> City { get; set; }
        //public virtual ICollection<Hotel> Hotel { get; set; }
        //public virtual ICollection<Package> Package { get; set; }
        //public virtual ICollection<Place> Place { get; set; }
        //public virtual ICollection<SubPlace> SubPlace { get; set; }
    }
}
