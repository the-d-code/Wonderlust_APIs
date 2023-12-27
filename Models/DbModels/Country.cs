using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WONDERLUST_PROJECT_APIs.Models.DbModels
{
    public class Country
    {
        //public Country()
        //{
        //    //Package = new HashSet<Package>();
        //    //Place = new HashSet<Place>();
        //    State = new HashSet<State>();
        //}
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CountryId { get; set; }
        public string? CountryName { get; set; }

        public bool IsBlock { get; set; }
        public DateTime? CreatedAt { get; set; }
     
       
        //public virtual ICollection<Package> Package { get; set; }
        //public virtual ICollection<Place> Place { get; set; }
        //public virtual ICollection<State> State { get; set; }

    }
}
