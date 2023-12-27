namespace WONDERLUST_PROJECT_APIs.Models.InputModels
{
    using System.ComponentModel.DataAnnotations;

    public class LoginInputModel
    {
        [EmailAddress]
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
