using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace ModelLayer.Models
{
    public class StoreLocation
    {
        [Key]
        public Guid StoreLocationId { get; set; } = Guid.NewGuid();

        [Required]
        [Display(Name = "Store Name")]
        public string StoreLocationName { get; set; }

        // make this bigger later
        public string StoreLocationAddress { get; set; }
        public List<Inventory> Inventory { get; set; } = new List<Inventory>();
        public List<Customer> FrequentCustomers { get; set; } = new List<Customer>();

        public StoreLocation(){}

        public StoreLocation(string name, string city)
        {
            this.StoreLocationName = name;
            this.StoreLocationAddress = city;
        }

    }
}