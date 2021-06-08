using System.ComponentModel.DataAnnotations;

namespace ActionCommandGame.UI.Mvc.Models
{
    public class EditPasswordModel
    {
        public string ReturnUrl { get; set; }
        
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.")]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }
        
        [Required]
        [Display(Name = "Confirm password")]
        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmNewPassword { get; set; }
        
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.")]
        [DataType(DataType.Password)]
        [Display(Name = "Old password")]
        public string OldPassword { get; set; }
    }
}