using System;
using System.Collections.Generic;
using ModelLayer.Models;
using ModelLayer.ViewModels;
using RepositoryLayer;
using System.Linq;


namespace BusinessLogicLayer
{

    public class BusinessLogicClass
    {
        private readonly Repository _repo;
        private readonly MapperClass _mapper;

        public BusinessLogicClass(Repository repo, MapperClass mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        /// <summary>
        /// Takes in a Login View Model and logs in the customer if they exist. Returns null if they do not exist
        /// </summary>
        /// <param name="loginPlayer">Playing logging in username and password</param>
        /// <returns>Customer info view model for the logged in customer</returns>
        public CustomerInfoViewModel LoginUser(LoginViewModel loginPlayer)
        {
            Customer customer = new Customer()
            {
                CustomerUserName = loginPlayer.CustomerUserName,
                CustomerPassword = loginPlayer.CustomerPassword
            };

            // if null????
            Customer c = _repo.LoginUser(customer);

            if (c == null)
            {
                return null;
            }

            CustomerInfoViewModel currentCustomerViewModel = _mapper.ConvertCustomerToCustomerInfoViewModel(c);

            return currentCustomerViewModel;

        }

        /// <summary>
        /// Takes the info from a create customer view model and creates a new customer if that user name is not in use.
        /// </summary>
        /// <param name="createdPlayer">Create customer view model being created</param>
        /// <returns>Returns and logs in a new Customer info view model</returns>
        public CustomerInfoViewModel CreateUser(CreateCustomerViewModel createdPlayer)
        {
            StoreLocation favoriteStore = _repo.GetStoreByName(createdPlayer.StoreNameChosen);

            Customer customer = new Customer()
            {
                CustomerUserName = createdPlayer.CustomerUserName,
                CustomerPassword = createdPlayer.CustomerPassword,
                CustomerFName = createdPlayer.CustomerFName,
                CustomerLName = createdPlayer.CustomerLName,
                CustomerAge = createdPlayer.CustomerAge,
                CustomerBirthday = createdPlayer.CustomerBirthday,
                PerferedStore = favoriteStore
            };

            // if null????
            Customer newCustomer = _repo.CreateUser(customer);
            if (newCustomer == null)
            {
                return null;
            }

            CustomerInfoViewModel newCustomerViewModel = _mapper.ConvertCustomerToCustomerInfoViewModel(newCustomer);

            return newCustomerViewModel;
        }

        /// <summary>
        /// Searches for the customer and grabs them by their id
        /// </summary>
        /// <param name="id">customer id</param>
        /// <returns>Customer info view model based on customer id</returns>
        public CustomerInfoViewModel GetCustomerById(Guid id)
        {
            Customer customer = _repo.GetCustomerById(id);

            if (customer == null)
            {
                return null;
            }

            CustomerInfoViewModel customerInfo = _mapper.ConvertCustomerToCustomerInfoViewModel(customer);

            return customerInfo;
        }

        /// <summary>
        /// Gets a list of all the customers in the database
        /// </summary>
        /// <returns>A list of all customers</returns>
        public CustomerListViewModel GetCustomerList()
        {
            CustomerListViewModel customerList = new CustomerListViewModel();
            List<CustomerInfoViewModel> customerViewModels = new List<CustomerInfoViewModel>();

            List<Customer> customers = _repo.GetCustomerList();


            foreach (Customer c in customers)
            {
                customerViewModels.Add(_mapper.ConvertCustomerToCustomerInfoViewModel(c));
            }

            customerList.Customers = customerViewModels;

            return customerList;
        }

        /// <summary>
        /// Takes in customer info view model, finds the customer it is referencing, and edits them
        /// </summary>
        /// <param name="customerToEdit">Customer we are editing  viewmodel</param>
        /// <returns>Update customer view model</returns>
        public CustomerInfoViewModel EditCustomer(CustomerInfoViewModel customerToEdit)
        {
            Customer customer = _repo.GetCustomerById(customerToEdit.CustomerID);

            StoreLocation favoriteStore = _repo.GetStoreByName(customerToEdit.StoreNameChosen);

            customer.CustomerFName = customerToEdit.CustomerFName;
            customer.CustomerLName = customerToEdit.CustomerLName;
            customer.CustomerAge = customerToEdit.CustomerAge;
            customer.CustomerBirthday = customerToEdit.CustomerBirthday;
            customer.PerferedStore = favoriteStore;

            Customer editedCustomer = _repo.EditCustomer(customer);

            CustomerInfoViewModel editedCustomerViewModel = _mapper.ConvertCustomerToCustomerInfoViewModel(editedCustomer);

            return editedCustomerViewModel;
        }

        /// <summary>
        /// Creates a new store if one by that name does not already exist
        /// </summary>
        /// <param name="createdStore">New store info view model</param>
        /// <returns>Store info view model of the new store</returns>
        public StoreInfoViewModel CreateStore(StoreInfoViewModel createdStore)
        {
            StoreLocation store = new StoreLocation()
            {
                StoreLocationName = createdStore.StoreLocationName,
                StoreLocationAddress = createdStore.StoreLocationAddress
            };

            // if null???? 
            StoreLocation newStore = _repo.CreateStore(store);

            if (newStore == null)
            {
                return null;
            }

            StoreInfoViewModel newStoreViewModel = _mapper.ConvertStoreToStoreInfoViewModel(newStore);

            return newStoreViewModel;
        }

        /// <summary>
        /// Gets a list of all stores in the database
        /// </summary>
        /// <returns>List of all stores</returns>
        public StoreListViewModel GetStoreList()
        {
            StoreListViewModel storeList = new StoreListViewModel();
            List<StoreInfoViewModel> storeViewModels = new List<StoreInfoViewModel>();
            List<StoreLocation> stores = _repo.GetStoreList();


            foreach (StoreLocation s in stores)
            {
                storeViewModels.Add(_mapper.ConvertStoreToStoreInfoViewModel(s));
            }

            storeList.StoreLocations = storeViewModels;

            return storeList;
        }

        /// <summary>
        /// Gets a list of all the store names in the database
        /// </summary>
        /// <returns>List of store names</returns>
        public List<string> GetStoreNames()
        {
            List<string> storeNames = _repo.GetStoreNames();
            return storeNames;
        }

        /// <summary>
        /// Gets store from database by a store id
        /// </summary>
        /// <param name="id">Store location id</param>
        /// <returns>Store view model that was found by the id</returns>
        public StoreInfoViewModel GetStoreById(Guid id)
        {
            StoreLocation storeToEdit = _repo.GetStoreById(id);

            if (storeToEdit == null)
            {
                return null;
            }

            StoreInfoViewModel storeViewToEdit = _mapper.ConvertStoreToStoreInfoViewModel(storeToEdit);

            return storeViewToEdit;
        }

        /// <summary>
        /// Get a store object by id
        /// </summary>
        /// <param name="id">Store location id</param>
        /// <returns>Store object based on id</returns>
        public StoreLocation GetNewStoreById(Guid id)
        {
            StoreLocation store = _repo.GetStoreById(id);
            return store;
        }

        /// <summary>
        /// Grabs a store info view model and edits a store based on whats inputted
        /// </summary>
        /// <param name="storeToEdit">The store info view model we are editing</param>
        /// <returns>An edited store info view model</returns>
        public StoreInfoViewModel EditStore(StoreInfoViewModel storeToEdit)
        {
            StoreLocation store = _repo.GetStoreById(storeToEdit.StoreLocationId);

            store.StoreLocationName = storeToEdit.StoreLocationName;
            store.StoreLocationAddress = storeToEdit.StoreLocationAddress;

            StoreLocation editedStore = _repo.EditStore(store);

            StoreInfoViewModel editedStoreIfoViewModel = _mapper.ConvertStoreToStoreInfoViewModel(editedStore);

            return editedStoreIfoViewModel;
        }

        /// <summary>
        /// Gets a list of all products in the database
        /// </summary>
        /// <returns>A list of all product view models</returns>
        public ProductListViewModel GetProductList()
        {
            ProductListViewModel productList = new ProductListViewModel();
            List<Product> products = _repo.GetProductList();
            List<ProductInfoViewModel> productsInfo = new List<ProductInfoViewModel>();

            foreach (Product p in products)
            {
                productsInfo.Add(_mapper.ConvertProductToProductInfoViewModel(p));
            }

            productList.Products = productsInfo;

            return productList;
        }

        /// <summary>
        /// Gets a product info view model from a product id
        /// </summary>
        /// <param name="id">Product id</param>
        /// <returns>A product view model based on the id</returns>
        public ProductInfoViewModel GetProductById(Guid id)
        {
            Product product = _repo.GetProductById(id);

            if (product == null)
            {
                return null;
            }

            ProductInfoViewModel productDetails = _mapper.ConvertProductToProductInfoViewModel(product);
            return productDetails;
        }

        /// <summary>
        /// Creates a new product if a product of that name does not already exist
        /// </summary>
        /// <param name="newProduct">Product model of the product we want to create</param>
        /// <returns>A new product view model</returns>
        public ProductInfoViewModel CreateProduct(ProductInfoViewModel newProduct)
        {
            Product product = new Product()
            {
                ProductName = newProduct.ProductName,
                ProductDesc = newProduct.ProductDesc,
                ProductPrice = newProduct.ProductPrice,
                IsAgeRestricted = newProduct.IsAgeRestricted
            };


            Product createProduct = _repo.CreateProduct(product);

            if (createProduct == null)
            {
                return null;
            }

            ProductInfoViewModel newProductInfoViewModel = _mapper.ConvertProductToProductInfoViewModel(createProduct);

            return newProductInfoViewModel;
        }

        /// <summary>
        /// Takes in a product to edit, edits it, saves to db, and returns the new edited product
        /// </summary>
        /// <param name="productToEdit">The product view model of the product we want to edit</param>
        /// <returns>Updated product view model</returns>
        public ProductInfoViewModel EditProduct(ProductInfoViewModel productToEdit)
        {
            Product product = _repo.GetProductById(productToEdit.ProductID);

            if (product == null)
            {
                return null;
            }

            product.ProductName = productToEdit.ProductName;
            product.ProductDesc = productToEdit.ProductDesc;
            product.ProductPrice = productToEdit.ProductPrice;
            product.IsAgeRestricted = productToEdit.IsAgeRestricted;

            Product editedProduct = _repo.EditProduct(product);

            ProductInfoViewModel editedProductView = _mapper.ConvertProductToProductInfoViewModel(editedProduct);

            return editedProductView;
        }

        /// <summary>
        /// Gets a stores inventory list based on it a store id
        /// </summary>
        /// <param name="storeId">Store id of the store we want to sees inventory</param>
        /// <returns>A list of inventory at a store</returns>
        public ShoppingListViewModel GetStoreInventory(Guid storeId)
        {
            StoreLocation store = _repo.GetStoreById(storeId);

            // TODO: handle this in controller
            if (store == null)
            {
                return null;
            }

            ShoppingListViewModel storeInventory = new ShoppingListViewModel();

            List<InventoryInfoViewModel> inventoryViewModels = new List<InventoryInfoViewModel>();

            List<Inventory> inventories = _repo.GetStoreInventory(store);

            foreach (Inventory i in inventories)
            {
                inventoryViewModels.Add(_mapper.ConvertInventoryToInventoryInfoViewModel(i));
            }

            storeInventory.StoreLocation = _mapper.ConvertStoreToStoreInfoViewModel(store);
            storeInventory.Inventories = inventoryViewModels;

            return storeInventory;
        }

        /// <summary>
        /// Add inventory piece to a store based on store id
        /// </summary>
        /// <param name="storeId">Store id we want to update inventory at</param>
        /// <returns>The inventory view model we added to the store</returns>
        public AddInventoryViewModel AddInventory(Guid storeId)
        {
            AddInventoryViewModel addInventoryViewModel = new AddInventoryViewModel();

            StoreLocation store = _repo.GetStoreById(storeId);

            // TODO: handle this in controller
            if (store == null)
            {
                return null;
            }

            //handing products
            List<Product> products = _repo.GetProductList();

            List<ProductInfoViewModel> productInfoViews = new List<ProductInfoViewModel>();

            foreach (Product p in products)
            {
                productInfoViews.Add(_mapper.ConvertProductToProductInfoViewModel(p));
            }

            //handling inventory for the store
            List<Inventory> inventories = _repo.GetStoreInventory(store);

            List<InventoryInfoViewModel> inventoryViewModels = new List<InventoryInfoViewModel>();
            foreach (Inventory i in inventories)
            {
                inventoryViewModels.Add(_mapper.ConvertInventoryToInventoryInfoViewModel(i));
            }

            // filling AddInventoryViewModel
            // TODO: Mapper this
            addInventoryViewModel.Store = _mapper.ConvertStoreToStoreInfoViewModel(store);
            addInventoryViewModel.Products = productInfoViews;
            addInventoryViewModel.Inventory = inventoryViewModels;

            return addInventoryViewModel;
        }

        /// <summary>
        /// Adds a new inventory piece to a store based on store and product
        /// </summary>
        /// <param name="inventoryStore">The store we are adding inventory to id</param>
        /// <param name="productName">Name of the product we are adding</param>
        /// <param name="qantityAdded">The number of inventory we are adding</param>
        /// <returns>The updated shopping list with the new inventory</returns>
        public ShoppingListViewModel AddNewInventory(Guid inventoryStore, string productName, int qantityAdded)
        {
            ShoppingListViewModel inventoryList = new ShoppingListViewModel();

            Product product = _repo.GetProductByName(productName);

            if (product == null)
            {
                return null;
            }

            StoreLocation store = _repo.GetStoreById(inventoryStore);

            Inventory inventory = new Inventory()
            {
                Product = product,
                StoreLocation = store,
                ProductQuantity = qantityAdded
            };

            Inventory newInventory = _repo.AddNewInventory(inventory);

            inventoryList = GetStoreInventory(newInventory.StoreLocation.StoreLocationId);

            return inventoryList;
        }

        /// <summary>
        /// Gets the customers cart at a certain store
        /// </summary>
        /// <param name="storeId">Id of store we want</param>
        /// <param name="customerId">Customer we are using</param>
        /// <returns>Returns the cart info view model of the cart we grabbed</returns>
        public CartInfoViewModel GetCustomerCartAtStore(Guid storeId, Guid customerId)
        {
            StoreLocation store = _repo.GetStoreById(storeId);
            Customer customer = _repo.GetCustomerById(customerId);

            if (store == null || customer == null)
            {
                return null;
            }

            Order cart = _repo.GetOrderByStoreAndCustomer(store.StoreLocationId, customer.CustomerID);

            // TODO: creating new order if order doesnt exist. need to abstract out
            if (cart == null)
            {
                Order newCart = new Order()
                {
                    Store = store,
                    Customer = customer,
                    isCart = true
                };

                cart = _repo.CreateOrder(newCart);

                if (cart == null)
                {
                    return null;
                }
            }


            List<OrderLineDetails> orderLineDetails = _repo.GetOrderLineListByCart(cart);

            cart.ProductsInOrder = orderLineDetails;

            CartInfoViewModel cartInfo = _mapper.ConvertOrderToCartInfoViewModel(cart);
            return cartInfo;
        }

        /// <summary>
        /// Gets the current users carts
        /// </summary>
        /// <param name="customerId">Current user id</param>
        /// <returns>A list of the users carts</returns>
        public CartListViewModel GetUserCartList(Guid customerId)
        {
            CartListViewModel cartList = new CartListViewModel();

            List<CartInfoViewModel> carts = new List<CartInfoViewModel>();

            Customer customer = _repo.GetCustomerById(customerId);

            if (customer == null)
            {
                return null;
            }

            List<Order> customerCarts = _repo.GetCartsByCustomerId(customer.CustomerID);

            foreach (Order cart in customerCarts)
            {
                List<OrderLineDetails> orderLineDetails = _repo.GetOrderLineListByCart(cart);
                cart.ProductsInOrder = orderLineDetails;
                carts.Add(_mapper.ConvertOrderToCartInfoViewModel(cart));
            }

            cartList.CurrentCarts = carts;

            return cartList;
        }

        /// <summary>
        /// Adds a piece of inventory from a store to the correct cart for the store
        /// </summary>
        /// <param name="productName">Name of the product being added to the cart</param>
        /// <param name="quantity">Quantity we want to add</param>
        /// <param name="storeId">What store we are shopping at</param>
        /// <param name="customerId">The current customer</param>
        /// <returns>A view of the cart that the line was just added tp</returns>
        public CartInfoViewModel AddToCart(string productName, int quantity, Guid storeId, Guid customerId)
        {

            StoreLocation store = _repo.GetStoreById(storeId);
            Customer customer = _repo.GetCustomerById(customerId);
            Order cart = _repo.GetOrderByStoreAndCustomer(store.StoreLocationId, customer.CustomerID);

            if (cart == null)
            {
                Order newCart = new Order()
                {
                    Store = store,
                    Customer = customer,
                    isCart = true
                };

                cart = _repo.CreateOrder(newCart);

                if (cart == null)
                {
                    return null;
                }
            }

            Product product = _repo.GetProductByName(productName);
            
            Inventory inventory = _repo.GetInventoryByStoreAndName(store, product);

            if (store == null || customer == null || product == null || inventory == null)
            {
                return null;
            }

            try
            {
                OrderLineDetails orderLine = _repo.AddOrderLineDetail(inventory, cart, quantity);
            }
            catch (Exception e)
            {
                CartInfoViewModel badQuantity = new CartInfoViewModel()
                {
                    error = 1,
                    errorMessage = e.Message
                };
                return badQuantity;
            }
           

            cart = _repo.UpdateCartPrice(cart);

            List<OrderLineDetails> orderLineDetails = _repo.GetOrderLineListByCart(cart);

            cart.ProductsInOrder = orderLineDetails;


            CartInfoViewModel cartInfo = _mapper.ConvertOrderToCartInfoViewModel(cart);

            return cartInfo;
        }

        /// <summary>
        /// Checks out the selected cart
        /// </summary>
        /// <param name="customerId">current customer</param>
        /// <param name="cartId">cart we want to check out</param>
        /// <returns>a view mode of the checked out cart</returns>
        public CartInfoViewModel CheckoutCart(Guid customerId, Guid cartId)
        {
            // should validate customer somewhere
            Order order = _repo.GetOrderById(cartId);
            StoreLocation store = order.Store;
            List<OrderLineDetails> orderLineDetails = _repo.GetOrderLineListByCart(order);
            List<Inventory> inventory = _repo.GetStoreInventory(store);

            try
            {
                _repo.RemoveInventoryBasedOnOrder(orderLineDetails, inventory);
            }
            catch (Exception e)
            {
                CartInfoViewModel badQuantity = new CartInfoViewModel()
                {
                    Store = _mapper.ConvertStoreToStoreInfoViewModel(store),
                    error = 1,
                    errorMessage = e.Message
                };
                return badQuantity;
            }
           

            order.ProductsInOrder = orderLineDetails;

            order = _repo.CheckoutCart(order);

            CartInfoViewModel orderInfo = _mapper.ConvertOrderToCartInfoViewModel(order);

            return orderInfo;
        }

        /// <summary>
        /// Gets a completed order by the id of the order
        /// </summary>
        /// <param name="orderId">Order we want to see</param>
        /// <returns>Completed order</returns>
        public CartInfoViewModel GetPastOrderById(Guid orderId)
        {
            Order order = _repo.GetOrderById(orderId);

            if (order == null)
            {
                return null;
            }

            List<OrderLineDetails> orderLineDetails = _repo.GetOrderLineListByCart(order);

            order.ProductsInOrder = orderLineDetails;

            CartInfoViewModel orderInfo = _mapper.ConvertOrderToCartInfoViewModel(order);

            return orderInfo;
        }

        /// <summary>
        /// Gets a list of a users completed orders
        /// </summary>
        /// <param name="customerId">Current user</param>
        /// <returns>List of customers orders</returns>
        public CartListViewModel GetUserPastOrders(Guid customerId)
        {
            CartListViewModel orderList = new CartListViewModel();

            List<CartInfoViewModel> orders = new List<CartInfoViewModel>();

            Customer customer = _repo.GetCustomerById(customerId);

            if (customer == null)
            {
                return null;
            }

            List<Order> customerOrders = _repo.GetOrdersByCustomerId(customer.CustomerID);

            foreach (Order order in customerOrders)
            {
                List<OrderLineDetails> orderLineDetails = _repo.GetOrderLineListByCart(order);
                order.ProductsInOrder = orderLineDetails;
                orders.Add(_mapper.ConvertOrderToCartInfoViewModel(order));
            }

            orderList.CurrentCarts = orders;

            return orderList;
        }

        /// <summary>
        /// Gets a list of a stores completed orders
        /// </summary>
        /// <param name="StoreId">Store id we want to look at</param>
        /// <returns>A list of orders by store id</returns>
        public CartListViewModel GetStorePastOrders(Guid StoreId)
        {
            CartListViewModel orderList = new CartListViewModel();

            List<CartInfoViewModel> orders = new List<CartInfoViewModel>();

            StoreLocation store = _repo.GetStoreById(StoreId);

            if (store == null)
            {
                return null;
            }

            List<Order> storeOrders = _repo.GetOrdersByStoreId(store.StoreLocationId);

            foreach (Order order in storeOrders)
            {
                List<OrderLineDetails> orderLineDetails = _repo.GetOrderLineListByCart(order);
                order.ProductsInOrder = orderLineDetails;

                orders.Add(_mapper.ConvertOrderToCartInfoViewModel(order));
            }

            orderList.CurrentCarts = orders;

            return orderList;
        }


        /// <summary>
        /// Deletes an order line based on id
        /// </summary>
        /// <param name="orderLineId">Id of orderline we want to remove</param>
        /// <returns>Cart with removed orderline</returns>
        public CartInfoViewModel DeleteOrderLineItem(Guid orderLineId)
        {
            Order order = _repo.GetCartByOrderLineId(orderLineId);

            if (order == null)
            {
                return null;
            }

            bool isDeleted = _repo.DeleteOrderLineById(orderLineId);

            if (isDeleted == false)
            {
                return null;
            }

            List<OrderLineDetails> orderLineDetails = _repo.GetOrderLineListByCart(order);

            order.ProductsInOrder = orderLineDetails;

            order = _repo.UpdateCartPrice(order);

            CartInfoViewModel orderInfo = _mapper.ConvertOrderToCartInfoViewModel(order);

            return orderInfo;
        }




    }
}
