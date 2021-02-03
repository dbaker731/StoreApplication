using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogicLayer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ModelLayer.Models;
using ModelLayer.ViewModels;

namespace StoreApp.Controllers
{
    public class CustomerController : Controller
    {
        private BusinessLogicClass _logic;

        public CustomerController(BusinessLogicClass logic)
        {
            _logic = logic;
        }

        // GET: CustomerController
        public ActionResult Index(CustomerInfoViewModel loggedInView)
        {

            if (loggedInView.CustomerFName == null)
            {
                Guid id = new Guid(HttpContext.Session.GetString("customerId"));
                CustomerInfoViewModel customerInfo = _logic.GetCustomerById(id);

                if (customerInfo == null)
                {
                    RedirectToAction("Index", "Home");
                }

                customerInfo.PerferedStore = _logic.GetNewStoreById(customerInfo.StoreId);

                return View(customerInfo);
            }

            loggedInView.PerferedStore = _logic.GetNewStoreById(loggedInView.StoreId);

            return View(loggedInView);
        }

        // GET: CustomerController/Details/5
        [HttpGet]
        public ActionResult Details(Guid id)
        {
            CustomerInfoViewModel customerInfo = _logic.GetCustomerById(id);

            if (customerInfo == null)
            {
                ModelState.AddModelError("Failure", "Customer does not exist");
                return View(customerInfo);
            }

            return View(customerInfo);
        }

        [HttpGet]
        public IActionResult GetAllCustomers()
        {
            CustomerListViewModel customerList = _logic.GetCustomerList();
            return View(customerList);
        }


        // GET: CustomerController/Edit/5
        public ActionResult Edit(Guid id)
        {
            CustomerInfoViewModel customerToEdit = _logic.GetCustomerById(id);

            if (customerToEdit == null)
            {
                ModelState.AddModelError("Failure", "Customer does not exist to edit");
                return View(customerToEdit);
            }

            List<string> storeNames = _logic.GetStoreNames();

            ViewBag.StoreNames = new SelectList(storeNames);

            return View(customerToEdit);
        }

        public ActionResult EditPlayer(CustomerInfoViewModel customerToEdit)
        {
            CustomerInfoViewModel editedCustomer = _logic.EditCustomer(customerToEdit);

            if (editedCustomer == null)
            {
                ModelState.AddModelError("Failure", "Customer does not exist");
                return View("Edit", editedCustomer);
            }

            return View("Details", editedCustomer);
        }

        // POST: CustomerController/Edit/5
        // LOOK INTO THIS???
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        

       
    }
}
