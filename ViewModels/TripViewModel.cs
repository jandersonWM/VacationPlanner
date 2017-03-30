using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheWorld.ViewModels
{
    //Allows us to control what properties get sent through API without exposing all properties of Data class (Trip)
    public class TripViewModel
    {
        [Required]
        [StringLength(100, MinimumLength = 4)]
        public string Name { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.UtcNow;
    }
}
