using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelLayer.Models;
using ModelLayer.ViewModels;

namespace BusinessLogicLayer
{
    public class MapperClass
    {
        /// <summary>
        /// Converts a customer object to a CustomerInfoViewModel
        /// </summary>
        /// <param name="customer">Customer object we want to convert</param>
        /// <returns>Updated customerviewmodel</returns>
        public CustomerInfoViewModel ConvertCustomerToCustomerInfoViewModel(Customer customer)
        {
            CustomerInfoViewModel customerInfoViewModel = new CustomerInfoViewModel()
            {
                CustomerID = customer.CustomerID,
                CustomerUserName = customer.CustomerUserName,
                CustomerFName = customer.CustomerFName,
                CustomerLName = customer.CustomerLName,
                CustomerAge = customer.CustomerAge,
                PerferedStore = customer.PerferedStore,
                StoreId = customer.PerferedStore.StoreLocationId,
                isAdmin = customer.isAdmin
            };

            return customerInfoViewModel;
        }

        /// <summary>
        /// Converts a store into a storeinfoviewmodel
        /// </summary>
        /// <param name="store">Store we wnat to convert</param>
        /// <returns>Updated storeinfoviewmodel</returns>
        public StoreInfoViewModel ConvertStoreToStoreInfoViewModel(StoreLocation store)
        {
            StoreInfoViewModel storeInfoViewModel = new StoreInfoViewModel()
            {
                StoreLocationId = store.StoreLocationId,
                StoreLocationName = store.StoreLocationName,
                StoreLocationAddress = store.StoreLocationAddress,
                Inventory = store.Inventory
            };

            return storeInfoViewModel;
        }

        /// <summary>
        /// Takes a product and turns it into a productinfoviewmodel
        /// </summary>
        /// <param name="product">Product we want to convert</param>
        /// <returns>updated productinfoviewmodel</returns>
        public ProductInfoViewModel ConvertProductToProductInfoViewModel(Product product)
        {
            ProductInfoViewModel productInfoViewModel = new ProductInfoViewModel()
            {
                ProductID = product.ProductID,
                ProductName = product.ProductName,
                ProductDesc = product.ProductDesc,
                ProductPrice = product.ProductPrice,
                IsAgeRestricted = product.IsAgeRestricted
            };

            return productInfoViewModel;
        }

        /// <summary>
        /// Takes an inventory object and converts it to a inventoryinfoviewmodel
        /// </summary>
        /// <param name="inventory">inventory we want to convert</param>
        /// <returns>updated inventoryinfoviewmodel</returns>
        public InventoryInfoViewModel ConvertInventoryToInventoryInfoViewModel(Inventory inventory)
        {
            InventoryInfoViewModel infoViewModel = new InventoryInfoViewModel()
            {
                InventoryId = inventory.InventoryId,
                Product = inventory.Product,
                StoreLocation = inventory.StoreLocation,
                ProductQuantity = inventory.ProductQuantity
            };

            return infoViewModel;
        }

        /// <summary>
        /// Takes an order and turns it into a cartinfoviewmodel
        /// </summary>
        /// <param name="cart">order we want to convert</param>
        /// <returns>conveted cartinfoviewmodel</returns>
        public CartInfoViewModel ConvertOrderToCartInfoViewModel(Order cart)
        {
            CartInfoViewModel cartInfoViewModel = new CartInfoViewModel()
            {
                OrderId = cart.OrderId,
                Customer = ConvertCustomerToCustomerInfoViewModel(cart.Customer),
                Store = ConvertStoreToStoreInfoViewModel(cart.Store),
                ProductsInOrder = cart.ProductsInOrder,
                TotalPrice = cart.TotalPrice,
                isCart = cart.isCart,
                isOrdered = cart.isOrdered,
                OrderTime = cart.OrderTime
            };

            return cartInfoViewModel;

        }



    }
}
