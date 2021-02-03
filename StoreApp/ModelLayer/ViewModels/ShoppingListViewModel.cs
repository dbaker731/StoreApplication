using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer.ViewModels
{
    public class ShoppingListViewModel
    {
        public List<InventoryInfoViewModel> Inventories { get; set; }
        public StoreInfoViewModel StoreLocation { get; set; }

        [Display(Name = "Product Name")]
        public string ProductName { get; set; }

        [Required]
        [Range(0, 1000)]
        [Display(Name = "Quantity")]
        public int QuantityAdded { get; set; }
        public Guid StoreId { get; set; }
    }
}
