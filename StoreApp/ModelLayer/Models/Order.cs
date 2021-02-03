using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ModelLayer.Models
{
    public class Order
    {
        [Key]
        public Guid OrderId { get; set; } = Guid.NewGuid();

        public List<OrderLineDetails> ProductsInOrder = new List<OrderLineDetails>();

        [Required]
        public Customer Customer { get; set; }

        [Required]
        public StoreLocation Store { get; set; }

        [Range(0, int.MaxValue)]
        [Display(Name = "Order Total")]
        public decimal TotalPrice { get; set; }

        public bool isOrdered { get; set; } = false;

        public bool isCart { get; set; } = false;

        public DateTime? OrderTime { get; set; }

        public Order(){}

        public Order( Customer newCustomer, StoreLocation store)
        {
            this.Customer = newCustomer;
            this.Store = store;
            this.isCart = true;
        }
    }
}