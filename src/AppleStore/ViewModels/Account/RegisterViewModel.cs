using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AppleStore.ViewModels.Account
{
    public class RegisterViewModel
    {
        [Required]
        [RegularExpression(pattern: @"[^123456789<>!.,{}[]()]")]
        [StringLength(maximumLength: 15, MinimumLength = 2)]
        [Display(Name = "User name")]
        public String UserName { get; set; }

        [Required]
        [RegularExpression(pattern: @"[^123456789<>!.,{}[]()]")]
        [StringLength(maximumLength: 15, MinimumLength = 2)]
        public String City { get; set; }

        [Required]
        [RegularExpression(@"\d{6,18}")]
        public long Phone { get; set; }

        [Required]
        [RegularExpression(pattern: @"[^<>!{}[]()]")]
        [StringLength(maximumLength: 40, MinimumLength = 5)]
        public String Address { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}
