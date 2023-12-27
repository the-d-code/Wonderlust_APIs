using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WONDERLUST_PROJECT_APIs.Models.DbModels
{
    public class Bus
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BusId { get; set; }
        public string BusName { get; set; }
        public int TotalSeat { get; set; }

        public int AvailableSeat { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }

        public DateTime CreatedAt { get; set; }

      
       

    }
}
