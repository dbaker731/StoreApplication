using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        [StringLength(20, MinimumLength = 2, ErrorMessage = "Username must be from 2 to 20 characters.")]
        [Display(Name = "Username")]
        public string CustomerUserName { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "Password must be from 6 to 20 characters.")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string CustomerPassword { get; set; }
    }
}
