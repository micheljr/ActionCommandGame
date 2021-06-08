using System.ComponentModel.DataAnnotations;

namespace ActionCommandGame.UI.Mvc.Models
{
    public class EditEmailModel
    {
        public string ReturnUrl { get; set; }
        
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}