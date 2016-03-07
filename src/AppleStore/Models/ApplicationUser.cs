using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations;


namespace AppleStore.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        [Required(ErrorMessage = "Max length of city field is 20")]
        [StringLength(maximumLength: 20)]
        public String City { get; set; }

        [Required]
        [StringLength(maximumLength: 40)]
        public String Address { get; set; }
    }
}
