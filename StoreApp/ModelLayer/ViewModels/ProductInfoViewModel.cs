using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer.ViewModels
{
    public class ProductInfoViewModel
    {
        [Key]
        public Guid ProductID { get; set; } = Guid.NewGuid();

        [Required]
        [StringLength(40, MinimumLength = 2, ErrorMessage = "Product name must be from 3 to 40 characters.")]
        [Display(Name = "Product Name")]
        public string ProductName { get; set; }

        [Required]
        [StringLength(250, MinimumLength = 10, ErrorMessage = "Description must be from 10 to 250 characters.")]
        [Display(Name = "Product Description")]
        public string ProductDesc { get; set; }

        [Required]
        [Range(0d, 1000d)]
        [Display(Name = "Price")]
        public decimal ProductPrice { get; set; }

        [Required]
        [Display(Name = "Age Restriction")]
        public bool IsAgeRestricted { get; set; }
    }
}
