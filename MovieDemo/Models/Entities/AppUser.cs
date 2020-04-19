using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MovieDemo.Models.Entities
{
    public class AppUser : Transport
    {
        public long AppUserId { get; set; }
        [Required]
        public string Firstname { get; set; }
        [Required]
        public string Surname { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        [Display(Name = "Confirm Password")]
        [Required]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
        public List<UserFavouriteMovie> UserFavouriteMovies { get; set; }
    }
}
