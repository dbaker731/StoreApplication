using System;
using System.ComponentModel.DataAnnotations;

namespace ModelLayer.Models
{
    public class OrderLineDetails
    {
        [Key]
        public Guid OrderDetailsId { get; set; } = Guid.NewGuid();

        [Required]
        public Inventory Item { get; set; }

        [Required]
        [Range(0, 100)]
        public int OrderDetailsQuantity { get; set; }
        public Order Order { get; set; }


        public decimal OrderDetailsPrice { get; set; } = 0;

        public decimal TotalPrice ( int quantityOrdered, decimal price )
        {
            return quantityOrdered * price;
        }

        public OrderLineDetails(){}

        public OrderLineDetails(Inventory item, Order cart, int quantity)
        {
            this.Item = item;
            this.Order = cart;
            this.OrderDetailsQuantity = quantity;
        }
    }
}