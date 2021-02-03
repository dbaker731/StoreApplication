using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ModelLayer.Models;

namespace RepositoryLayer
{
    public class Repository
    {
        private readonly StoreDbContext _context;

        DbSet<Customer> customers;
        DbSet<StoreLocation> stores;
        DbSet<Product> products;
        DbSet<Inventory> inventories;
        DbSet<Order> orders;
        DbSet<OrderLineDetails> orderLines;

        public Repository(StoreDbContext storeDbContext)
        {
            _context = storeDbContext;
            customers = _context.customers;
            stores = _context.stores;
            products = _context.products;
            inventories = _context.inventories;
            orders = _context.orders;
            orderLines = _context.orderLineDetails;
        }

        /// <summary>
        /// Checks to see if customer exists and can log in
        /// </summary>
        /// <param name="user">Customer trying to log in</param>
        /// <returns>The customer logging in</returns>
        public Customer LoginUser(Customer user)
        {
            Customer c = customers.Include(c => c.PerferedStore).FirstOrDefault(c => c.CustomerUserName == user.CustomerUserName && c.CustomerPassword == user.CustomerPassword);
            return c;
        }

        /// <summary>
        /// Creates a new user base on new customer given
        /// </summary>
        /// <param name="newUser">The new customer to be made</param>
        /// <returns>The new customer</returns>
        public Customer CreateUser(Customer newUser)
        {
            Customer c = customers.FirstOrDefault(c => c.CustomerUserName == newUser.CustomerUserName);
            if (c == null)
            {
                Customer newCustomer = new Customer()
                {
                    CustomerUserName = newUser.CustomerUserName,
                    CustomerPassword = newUser.CustomerPassword,
                    CustomerFName = newUser.CustomerFName,
                    CustomerLName = newUser.CustomerLName,
                    CustomerAge = newUser.CustomerAge,
                    CustomerBirthday = newUser.CustomerBirthday,
                    PerferedStore = newUser.PerferedStore
                };

                customers.Add(newCustomer);
                _context.SaveChanges();
                return newCustomer;
            }

            //returning null if a player with that user name already exists
            return null;
        }

        /// <summary>
        /// Gets a customer by id
        /// </summary>
        /// <param name="id">Id of the customer</param>
        /// <returns>customer</returns>
        public Customer GetCustomerById(Guid id)
        {
            Customer customer = customers.Include(c => c.PerferedStore).FirstOrDefault(c => c.CustomerID == id);
            return customer;
        }

        /// <summary>
        /// Gets a list of all customers
        /// </summary>
        /// <returns>List of customers</returns>
        public List<Customer> GetCustomerList()
        {
            return customers.Include(c => c.PerferedStore).ToList();
        }

        /// <summary>
        /// Edits a customer
        /// </summary>
        /// <param name="c">Customer to be edited</param>
        /// <returns>newly edited customer</returns>
        public Customer EditCustomer(Customer c)
        {
            Customer editedCustomer = GetCustomerById(c.CustomerID);

            if (editedCustomer == null)
            {
                return null;
            }

            editedCustomer.CustomerFName = c.CustomerFName;
            editedCustomer.CustomerLName = c.CustomerLName;
            editedCustomer.CustomerAge = c.CustomerAge;
            editedCustomer.CustomerBirthday = c.CustomerBirthday;
            editedCustomer.PerferedStore = c.PerferedStore;

            _context.SaveChanges();

            return editedCustomer;

        }

        /// <summary>
        /// Gets a list of all stores
        /// </summary>
        /// <returns>List of all stores</returns>
        public List<StoreLocation> GetStoreList()
        {
            return stores.ToList();
        }

        /// <summary>
        /// Creates a new store location
        /// </summary>
        /// <param name="newStore">New store object to be made</param>
        /// <returns>New stor object</returns>
        public StoreLocation CreateStore(StoreLocation newStore)
        {
            StoreLocation s = stores.FirstOrDefault(s => s.StoreLocationName == newStore.StoreLocationName);
            if (s == null)
            {
                StoreLocation newStoreLocation = new StoreLocation()
                {
                    StoreLocationName = newStore.StoreLocationName,
                    StoreLocationAddress = newStore.StoreLocationAddress,
  
                };

                stores.Add(newStoreLocation);
                _context.SaveChanges();
                return newStoreLocation;
            }

            //returning null if a store with that user name already exists
            return null;
        }

        /// <summary>
        /// List of store names
        /// </summary>
        /// <returns>List of store names only</returns>
        public List<string> GetStoreNames()
        {
            List<string> storeNames = new List<string>();

            foreach (StoreLocation s in stores)
            {
                storeNames.Add(s.StoreLocationName);
            }

            return storeNames;
        }

        /// <summary>
        /// Gets a store by name
        /// </summary>
        /// <param name="faveStore">Name of the store</param>
        /// <returns>Store object found by name</returns>
        public StoreLocation GetStoreByName(string faveStore)
        {
            StoreLocation store = stores.FirstOrDefault(s => s.StoreLocationName == faveStore);
            return store;
        }

        /// <summary>
        /// Gets a store object by id
        /// </summary>
        /// <param name="id">Id of the store we want to find</param>
        /// <returns>Store object</returns>
        public StoreLocation GetStoreById(Guid id)
        {
            StoreLocation store = stores.FirstOrDefault(s => s.StoreLocationId == id);
            return store;
        }

        /// <summary>
        /// Edits a store
        /// </summary>
        /// <param name="storeToEdit">Store information to edit</param>
        /// <returns>Newly edited store</returns>
        public StoreLocation EditStore(StoreLocation storeToEdit)
        {
            StoreLocation store = stores.FirstOrDefault(s => s.StoreLocationId == storeToEdit.StoreLocationId);

            if (store == null)
            {
                return null;
            }

            store.StoreLocationName = storeToEdit.StoreLocationName;
            store.StoreLocationAddress = storeToEdit.StoreLocationAddress;

            _context.SaveChanges();

            return store;
        }

        /// <summary>
        /// Gets a list of all products
        /// </summary>
        /// <returns>List of products</returns>
        public List<Product> GetProductList()
        {
            return products.ToList();
        }

        /// <summary>
        /// Creates a new product
        /// </summary>
        /// <param name="newProduct">New product information</param>
        /// <returns>Newly created product</returns>
        public Product CreateProduct(Product newProduct)
        {

            Product p = products.FirstOrDefault(p => p.ProductName == newProduct.ProductName);
            if (p == null)
            {
                Product product = new Product()
                {
                    ProductName = newProduct.ProductName,
                    ProductDesc = newProduct.ProductDesc,
                    ProductPrice = newProduct.ProductPrice,
                    IsAgeRestricted = newProduct.IsAgeRestricted
                };

                products.Add(product);
                _context.SaveChanges();
                return product;
            }

            //returning null if a products with that user name already exists
            return null;
        }

        /// <summary>
        /// Get product by id
        /// </summary>
        /// <param name="id">Product we want to find</param>
        /// <returns>Product</returns>
        public Product GetProductById(Guid id)
        {
           Product product = products.FirstOrDefault(p => p.ProductID == id);
           return product;
        }

        /// <summary>
        /// Gets a product by name
        /// </summary>
        /// <param name="name">name of the product</param>
        /// <returns>product retrieved</returns>
        public Product GetProductByName(string name)
        {
            Product product = products.FirstOrDefault(p => p.ProductName == name);
            return product;
        }

        /// <summary>
        /// Edits a product
        /// </summary>
        /// <param name="product">Product we are edited</param>
        /// <returns>Product newly edited</returns>
        public Product EditProduct(Product product)
        {
            Product editedProduct = products.FirstOrDefault(p => p.ProductID == product.ProductID);
            
            if (editedProduct == null)
            {
                return null;
            }

            editedProduct.ProductName = product.ProductName;
            editedProduct.ProductDesc = product.ProductDesc;
            editedProduct.ProductPrice = product.ProductPrice;
            editedProduct.IsAgeRestricted = product.IsAgeRestricted;

            _context.SaveChanges();

            return editedProduct;

        }

        /// <summary>
        /// Gets a list of a stores inventory
        /// </summary>
        /// <param name="store">Store we want inventory from</param>
        /// <returns>List of inventory</returns>
        public List<Inventory> GetStoreInventory(StoreLocation store)
        {
            List<Inventory> storeInventory = inventories
                .Include(i => i.StoreLocation)
                .Include(i => i.Product)
                .Where(i => i.StoreLocation.StoreLocationId == store.StoreLocationId).ToList();
           
            return storeInventory;
        }

        /// <summary>
        /// Gets inventory of a product at a store
        /// </summary>
        /// <param name="store">Store we want to check inventory</param>
        /// <param name="product">Product we want to see</param>
        /// <returns>Inventory of a product at a store</returns>
        public Inventory GetInventoryByStoreAndName(StoreLocation store, Product product)
        {
            Inventory newInventory = inventories
                .Include(i => i.StoreLocation)
                .Include(i => i.Product)
                .Where(i => i.StoreLocation.StoreLocationId == store.StoreLocationId && i.Product.ProductID == product.ProductID)
                .FirstOrDefault();
            return newInventory;
        }

        /// <summary>
        /// Adds inventory to a store
        /// </summary>
        /// <param name="addInventory">Inventory we want to add</param>
        /// <returns>The new inventory</returns>
        public Inventory AddNewInventory(Inventory addInventory)
        {
            Inventory inventory = inventories.FirstOrDefault(i => i.Product.ProductID == addInventory.Product.ProductID
                                       && i.StoreLocation.StoreLocationId == addInventory.StoreLocation.StoreLocationId);
            if (inventory == null)
            {
                Inventory addedInventory = new Inventory()
                {
                    Product = addInventory.Product,
                    StoreLocation = addInventory.StoreLocation,
                    ProductQuantity = addInventory.ProductQuantity
                };
                inventories.Add(addedInventory);
                _context.SaveChanges();
                return addedInventory;
            }
            inventory.ProductQuantity += addInventory.ProductQuantity;
            _context.SaveChanges();
            return addInventory;
        }

        /// <summary>
        /// Creates a new order
        /// </summary>
        /// <param name="order">The order we want to create</param>
        /// <returns>The newly created order</returns>
        public Order CreateOrder(Order order)
        {
            Order o = orders.FirstOrDefault(c => c.Store == order.Store && c.Customer == order.Customer && c.isCart == true);
            
            if (o == null)
            {
                Order newOrder = new Order()
                {
                    Store = order.Store,
                    Customer = order.Customer,
                    isCart = order.isCart
                };
                orders.Add(newOrder);
                _context.SaveChanges();
                return newOrder;
            }

            return null;
        }

        /// <summary>
        /// Get an order by id
        /// </summary>
        /// <param name="id">id of the order we want</param>
        /// <returns>order found</returns>
        public Order GetOrderById(Guid id)
        {
            Order order = orders
                .Include(o => o.Store)
                .Include(o => o.Customer)
                .ThenInclude(c => c.PerferedStore)
                .Where(o => o.OrderId == id)
                .FirstOrDefault();

            return order;
        }

        /// <summary>
        /// Get an order by store and by customer
        /// </summary>
        /// <param name="storeId">Store location</param>
        /// <param name="customerId">Current Customer</param>
        /// <returns>The order that was found</returns>
        public Order GetOrderByStoreAndCustomer(Guid storeId, Guid customerId)
        {
            Order order = orders
                .Include(o => o.Store)
                .Include(o => o.Customer)
                .ThenInclude(o => o.PerferedStore)
                .Where(o => o.Store.StoreLocationId == storeId && o.isCart == true && o.Customer.CustomerID == customerId)
                .FirstOrDefault();

            return order;
        }

        /// <summary>
        /// Get the list of carts a customer has
        /// </summary>
        /// <param name="customerId">Customer we want to find carts</param>
        /// <returns>List of a customers orders</returns>
        public List<Order> GetCartsByCustomerId(Guid customerId)
        {
            List<Order> orderList = orders
                .Include(o => o.Customer)
                .Include(o => o.Store)
                .Where(o => o.Customer.CustomerID == customerId && o.isCart == true && o.isOrdered == false)
                .ToList();

            return orderList;
        }

        /// <summary>
        /// Gets a list of orders a customer has
        /// </summary>
        /// <param name="customerId">Customer we want to find orders</param>
        /// <returns>Customers past orders</returns>
        public List<Order> GetOrdersByCustomerId(Guid customerId)
        {
            List<Order> orderList = orders
                .Include(o => o.Customer)
                .Include(o => o.Store)
                .Where(o => o.Customer.CustomerID == customerId && o.isCart == false && o.isOrdered == true)
                .ToList();

            return orderList;
        }

        /// <summary>
        /// Gets all order from a store
        /// </summary>
        /// <param name="storeId">Store we want to find orders from</param>
        /// <returns>List of orders at a store</returns>
        public List<Order> GetOrdersByStoreId(Guid storeId)
        {
            List<Order> orderList = orders
                .Include(o => o.Customer)
                .ThenInclude(c => c.PerferedStore)
                .Include(o => o.Store)
                .Where(o => o.Store.StoreLocationId == storeId && o.isCart == false && o.isOrdered == true)
                .ToList();

            return orderList;
        }

        /// <summary>
        /// Adds an order line detail to an order
        /// </summary>
        /// <param name="inventory">The inventory we are using in an order line</param>
        /// <param name="cart">The cart we are adding to</param>
        /// <param name="quantity">The quantity of inventory we are adding</param>
        /// <returns>The new order line</returns>
        public OrderLineDetails AddOrderLineDetail(Inventory inventory, Order cart, int quantity)
        {
            OrderLineDetails orderLineDetail = orderLines
                .Include(o => o.Item)
                .ThenInclude(i => i.Product)
                .Include(o => o.Order)
                .Where(o => o.Item.InventoryId == inventory.InventoryId && o.Order.OrderId == cart.OrderId)
                .FirstOrDefault();

            if (orderLineDetail == null)
            {
                OrderLineDetails orderLine = new OrderLineDetails()
                {
                    Item = inventory,
                    Order = cart,
                    OrderDetailsQuantity = quantity,
                };

                if (orderLine.OrderDetailsQuantity > inventory.ProductQuantity)
                {
                    throw new Exception($"Ordering too many {inventory.Product.ProductName}");
                }
                double totalCost = (double)orderLine.Item.Product.ProductPrice * orderLine.OrderDetailsQuantity;
                orderLine.OrderDetailsPrice = (decimal)totalCost;

                orderLines.Add(orderLine);
                _context.SaveChanges();
                return orderLine;
            }

            orderLineDetail.OrderDetailsQuantity += quantity;

            if (orderLineDetail.OrderDetailsQuantity > inventory.ProductQuantity)
            {
                throw new Exception($"Ordering too many {inventory.Product.ProductName}");
            }

            double cost = (double)orderLineDetail.Item.Product.ProductPrice * orderLineDetail.OrderDetailsQuantity;
            orderLineDetail.OrderDetailsPrice = (decimal)cost;
            _context.SaveChanges();

            return orderLineDetail;
        }

        /// <summary>
        /// Gets the order lines for an order
        /// </summary>
        /// <param name="cart">The order we want orderlines</param>
        /// <returns>List of orderlines</returns>
        public List<OrderLineDetails> GetOrderLineListByCart(Order cart)
        {
            List<OrderLineDetails> orderLineDetails = orderLines
                .Include(o => o.Order)
                .Include(o => o.Item)
                .ThenInclude(i => i.Product)
                .Where(o => o.Order.OrderId == cart.OrderId)
                .ToList();

            return orderLineDetails;
        }

        /// <summary>
        /// Updates a cart price
        /// </summary>
        /// <param name="cart">The cart we want to updates price</param>
        /// <returns>The order with the updated price</returns>
        public Order UpdateCartPrice(Order cart)
        {
            Order editCart = orders
                .Include(o => o.Customer)
                .ThenInclude(c => c.PerferedStore)
                .FirstOrDefault(o => o.OrderId == cart.OrderId);
           
            if (editCart == null)
            {
                return null;
            }

            List<OrderLineDetails> orderLineDetails = orderLines
                .Include(o => o.Order)
                .Where(o => o.Order.OrderId == editCart.OrderId)
                .ToList();

            editCart.TotalPrice = 0;

            foreach (OrderLineDetails orderLine in orderLineDetails)
            {
                editCart.TotalPrice += orderLine.OrderDetailsPrice;
            }

            _context.SaveChanges();
            return editCart;
        }

        /// <summary>
        /// Removes inventory from store when order is purchased
        /// </summary>
        /// <param name="orderLines">The order lines being taken away from inventory</param>
        /// <param name="inventory">The inventory we are editing</param>
        public void RemoveInventoryBasedOnOrder(List<OrderLineDetails> orderLines, List<Inventory> inventory)
        {
      
            foreach (Inventory i in inventory)
            {
                foreach (OrderLineDetails orderLine in orderLines)
                {
                    if (orderLine.Item.InventoryId == i.InventoryId)
                    {
                        if (i.ProductQuantity < orderLine.OrderDetailsQuantity)
                        {
                            throw new Exception($"Buying too many {i.Product.ProductName}");
                        }

                        i.ProductQuantity -= orderLine.OrderDetailsQuantity;
                    }
                }
            }
            _context.SaveChanges();
        }

        /// <summary>
        /// Checks out a cart
        /// </summary>
        /// <param name="cart">The cart we want to check out</param>
        /// <returns>The checked out cart</returns>
        public Order CheckoutCart(Order cart)
        {
            Order checkOutOrder = orders.FirstOrDefault(o => o.OrderId == cart.OrderId);
            checkOutOrder.isCart = false;
            checkOutOrder.isOrdered = true;
            checkOutOrder.OrderTime = DateTime.Now;

            _context.SaveChanges();
            return checkOutOrder;
        }

        /// <summary>
        /// Find a cart based on an orderline
        /// </summary>
        /// <param name="orderLineId">Order line we want to find cart by</param>
        /// <returns>Order bassed on orderline</returns>
        public Order GetCartByOrderLineId(Guid orderLineId)
        {
            OrderLineDetails orderLine = orderLines
                .Include(o => o.Order)
                .FirstOrDefault(o => o.OrderDetailsId == orderLineId);

            Order order = orders
                .Include(o => o.Store)
                .Include(o => o.Customer)
                .FirstOrDefault(o => o.OrderId == orderLine.Order.OrderId);

            return order;
        }

        /// <summary>
        /// Deletes an order line from an order
        /// </summary>
        /// <param name="orderLineId">Order line we want to delete</param>
        /// <returns>True or false if it deletes</returns>
        public bool DeleteOrderLineById(Guid orderLineId)
        {
            OrderLineDetails orderLine = orderLines.FirstOrDefault(o => o.OrderDetailsId == orderLineId);
            var success = orderLines.Remove(orderLine);
            _context.SaveChanges();

            if (success != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }

}
