namespace WONDERLUST_PROJECT_APIs.Models.InputModels
{
    using System.ComponentModel.DataAnnotations;

    public class UserChangeRoleInputModel
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Role { get; set; }
    }
}
