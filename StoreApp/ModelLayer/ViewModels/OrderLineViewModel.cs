using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelLayer.Models;

namespace ModelLayer.ViewModels
{
    public class OrderLineViewModel
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
    }
}
