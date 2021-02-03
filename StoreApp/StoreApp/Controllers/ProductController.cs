using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogicLayer;
using Microsoft.AspNetCore.Mvc;
using ModelLayer.ViewModels;

namespace StoreApp.Controllers
{
    public class ProductController : Controller
    {
  
        private BusinessLogicClass _logic;

        public ProductController(BusinessLogicClass logic)
        {
            _logic = logic;
        }

        public IActionResult Index()
        {
            ProductListViewModel productList = _logic.GetProductList();
            return View(productList);
        }

        public IActionResult CreateProduct()
        {
            return View();
        }

        public IActionResult CreateNewProduct(ProductInfoViewModel newProduct)
        {

            ProductInfoViewModel productCreated = _logic.CreateProduct(newProduct);

            if (productCreated == null)
            {
                ModelState.AddModelError("Failure", "Product already exists");
                return View("CreateProduct");
            }

            return RedirectToAction("Index");
        }

        public IActionResult Details(Guid id)
        {
            ProductInfoViewModel productDetails = _logic.GetProductById(id);

            if (productDetails == null)
            {
                ModelState.AddModelError("Failure", "Product does not exist");
                return View(productDetails);
            }

            return View(productDetails);
        }

        public IActionResult Edit(Guid id)
        {
            ProductInfoViewModel productToEdit = _logic.GetProductById(id);
            if (productToEdit == null)
            {
                ModelState.AddModelError("Failure", "Product does not exist");
                return View(productToEdit);
            }

            return View(productToEdit);
        }

        public IActionResult EditProduct(ProductInfoViewModel productToEdit)
        {
            ProductInfoViewModel editedProduct = _logic.EditProduct(productToEdit);

            if (editedProduct == null)
            {
                ModelState.AddModelError("Failure", "Product does not exist");
                return View("Edit", editedProduct);
            }

            return View("Details", editedProduct);
        }



    }
}
