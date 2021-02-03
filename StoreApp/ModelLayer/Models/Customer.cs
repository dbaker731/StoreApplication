using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ModelLayer.Models
{
    public class Customer
    {
        [Key]
        public Guid CustomerID { get; set; } = Guid.NewGuid();

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
        public int CustomerAge { get; set; }

        // move to this eventually instead of age
        public DateTime? CustomerBirthday { get; set; }

        public bool? isAdmin { get; set; } = false;

        public StoreLocation PerferedStore { get; set; }

        public void MakeAdmin()
        {
            isAdmin = true;
        }

        public Customer(){}

        public Customer(string userName, string userPassword, string fName, string lName, int age)
        {
            this.CustomerUserName = userName;
            this.CustomerPassword = userPassword;
            this.CustomerFName = fName;
            this.CustomerLName = lName;
            this.CustomerAge = age;
        }
    }
}