using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer.ViewModels
{
    public class CreateCustomerViewModel
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

        [Required]
        [StringLength(20, MinimumLength = 2, ErrorMessage = "The name must be from 3 to 20 characters.")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Use letters only please")]
        [Display(Name = "First Name")]
        public string CustomerFName { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 2, ErrorMessage = "The name must be from 3 to 20 characters.")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Use letters only please")]
        [Display(Name = "Last Name")]
        public string CustomerLName { get; set; }

        [Required]
        [Range(0, 120)]
        [Display(Name = "Age")]
        public int CustomerAge { get; set; }

        [Display(Name = "Birth date")]
        public DateTime? CustomerBirthday { get; set; }

        [Display(Name = "Favorite Store")]
        public string StoreNameChosen { get; set; }

    }
}
