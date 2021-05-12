using System;
using System.ComponentModel.DataAnnotations;

namespace ActionCommandGame.UI.Mvc.Areas.Administrator.Models
{
    public class SaveItemResource
    {
        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }
        [Display(Name = "Description")]
        public string Description { get; set; }
        [Required]
        [Display(Name = "Price")]
        public int Price { get; set; }
        [Display(Name = "Fuel")]
        public int Fuel { get; set; }
        [Display(Name = "Attack")]
        public int Attack { get; set; }
        [Display(Name = "Defense")]
        public int Defense { get; set; }
        [Display(Name = "ActionCooldownSeconds")]
        public int ActionCooldownSeconds { get; set; }

        public Guid Id { get; set; }
    }
}