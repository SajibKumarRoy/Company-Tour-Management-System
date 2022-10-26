using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Company_Tour_Management_System.Models
{
    public class SignInModel
    {
        [Required, EmailAddress]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Display(Name = "Remerber Me")]
        public bool RememberMe { get; set; }

    }
}
