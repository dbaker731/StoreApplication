using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelLayer.Models;

namespace ModelLayer.ViewModels
{
    public class CartInfoViewModel
    {
        [Key]
        public Guid OrderId { get; set; } = Guid.NewGuid();

        public List<OrderLineDetails> ProductsInOrder = new List<OrderLineDetails>();

        [Required]
        public CustomerInfoViewModel Customer { get; set; }

        [Required]
        public StoreInfoViewModel Store { get; set; }

        [Range(0, int.MaxValue)]
        [Display(Name = "Order Total")]
        public decimal TotalPrice { get; set; }

        public bool isOrdered { get; set; } = false;

        public bool isCart { get; set; } = false;

        public DateTime? OrderTime { get; set; }


        public int error { get; set; } = 0;

        public string errorMessage { get; set; }
    }
}
