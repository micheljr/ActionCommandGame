using System;
using System.ComponentModel.DataAnnotations;

namespace ActionCommandGame.UI.Mvc.Areas.Gamer.Models
{
    public class SavePlayerResource
    {
        public Guid Id { get; set; }
        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }
    }
}