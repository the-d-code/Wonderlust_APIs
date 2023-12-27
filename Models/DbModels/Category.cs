using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WONDERLUST_PROJECT_APIs.Models.DbModels
{
    public class Category
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CategoryId { get; set; }
       
        public string? CategoryName { get; set; }
     
        public string? CategoryDescription { get; set; }

        
        public string? CategoryImage { get; set; }
        public bool IsActive { get; set; }

        public DateTime CreatedAt { get; set; }

    }
}
