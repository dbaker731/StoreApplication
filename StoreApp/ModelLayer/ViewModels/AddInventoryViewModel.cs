using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelLayer.Models;

namespace ModelLayer.ViewModels
{
    public class AddInventoryViewModel
    {
        public StoreInfoViewModel Store { get; set; }
        public List<ProductInfoViewModel> Products { get; set; }
        public List<InventoryInfoViewModel> Inventory { get; set; }

        [Required]
        [Display(Name = "Product Name")]
        public string ProductName { get; set; }
       
        [Required]
        [Range(0, 1000)]
        [Display(Name = "Quantity")]
        public int QuantityAdded { get; set; }
        public Guid StoreId { get; set; }

    }
}
