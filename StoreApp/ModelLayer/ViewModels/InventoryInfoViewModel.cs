using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelLayer.Models;

namespace ModelLayer.ViewModels
{
    public class InventoryInfoViewModel
    {
        [Key]
        public Guid InventoryId { get; set; } = Guid.NewGuid();

        [Required]
        public Product Product { get; set; }

        [Required]
        public StoreLocation StoreLocation { get; set; }

        [Required]
        [Range(0, 1000)]
        public int ProductQuantity { get; set; } = 0;
    }
}
