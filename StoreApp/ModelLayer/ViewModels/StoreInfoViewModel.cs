using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelLayer.Models;

namespace ModelLayer.ViewModels
{
    public class StoreInfoViewModel
    {
        [Key]
        public Guid StoreLocationId { get; set; } = Guid.NewGuid();

        [Required]
        [Display(Name = "Store Name")]
        public string StoreLocationName { get; set; }

        // make this bigger later
        public string StoreLocationAddress { get; set; }
        public List<Inventory> Inventory { get; set; } = new List<Inventory>();
    }
}
