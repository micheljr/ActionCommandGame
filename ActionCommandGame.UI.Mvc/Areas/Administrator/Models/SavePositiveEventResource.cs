using System;
using System.ComponentModel.DataAnnotations;

namespace ActionCommandGame.UI.Mvc.Areas.Administrator.Models
{
    public class SavePositiveEventResource
    {
        public Guid Id { get; set; }
        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }
        [Display(Name = "Description")]
        public string Description { get; set; }
        [Display(Name = "Money")]
        public int Money { get; set; }
        [Display(Name = "Experience")]
        public int Experience { get; set; }
        [Display(Name = "Probability")]
        public int Probability { get; set; }
    }
}