using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelLayer.Models;

namespace ModelLayer.ViewModels
{
    public class CustomerInfoViewModel
    {
        public Guid CustomerID { get; set; } = Guid.NewGuid();

        [Required]
        [StringLength(20, MinimumLength = 2, ErrorMessage = "Username must be from 2 to 20 characters.")]
        [Display(Name = "Username")]
        public string CustomerUserName { get; set; }

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
        public int CustomerAge { get; set; }

        public DateTime? CustomerBirthday { get; set; }

        public bool? isAdmin { get; set; }

        public StoreLocation PerferedStore { get; set; }

        public Guid StoreId { get; set; }

        [Display(Name = "Favorite Store")]
        public string StoreNameChosen { get; set; }
    }
}
