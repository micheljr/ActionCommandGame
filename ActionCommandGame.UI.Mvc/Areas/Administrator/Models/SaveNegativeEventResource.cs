using System;
using System.ComponentModel.DataAnnotations;

namespace ActionCommandGame.UI.Mvc.Areas.Administrator.Models
{
    public class SaveNegativeEventResource
    {
        public Guid Id { get; set; }
        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }
        [Display(Name = "Description")]
        public string Description { get; set; }
        [Display(Name = "Defense with gear description")]
        public string DefenseWithGearDescription { get; set; }
        [Display(Name = "Defense without gear description")]
        public string DefenseWithoutGearDescription { get; set; }
        [Display(Name = "Defense loss")]
        public int DefenseLoss { get; set; }
        [Display(Name = "Probability")]
        public int Probability { get; set; }
    }
}