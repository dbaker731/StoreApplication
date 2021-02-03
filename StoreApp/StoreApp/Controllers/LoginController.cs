using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogicLayer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ModelLayer.ViewModels;

namespace StoreApp.Controllers
{
    public class LoginController : Controller
    {
        private BusinessLogicClass _logic;

        public object Session { get; private set; }

        public LoginController(BusinessLogicClass logic)
        {
            _logic = logic;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult LoginCustomer(LoginViewModel loginView)
        {
            CustomerInfoViewModel customerLoggingIn = _logic.LoginUser(loginView);
            if (customerLoggingIn == null)
            {
                ModelState.AddModelError("Failure", "Wrong Username and password combination!");
                return View("Index");
            }

            HttpContext.Session.SetString("customerId", customerLoggingIn.CustomerID.ToString());
            HttpContext.Session.SetString("isAdmin", customerLoggingIn.isAdmin.ToString());

            return RedirectToAction("Index", "Customer", customerLoggingIn);
        }

        [ActionName("NewCustomer")]
        public IActionResult LoginCustomer(CustomerInfoViewModel newUser)
        {
            if (newUser == null)
            {
                ModelState.AddModelError("Failure", "Wrong Username and password combination!");
                return View("Index");
            }

            HttpContext.Session.SetString("customerId", newUser.CustomerID.ToString());
            HttpContext.Session.SetString("isAdmin", newUser.isAdmin.ToString());
            return RedirectToAction("Index", "Customer", newUser);
        }


        public IActionResult CreateCustomer()
        {
            List<string> storeNames = _logic.GetStoreNames();

            ViewBag.StoreNames = new SelectList(storeNames);

            return View();
        }

        public IActionResult CreateNewCustomer(CreateCustomerViewModel signUpView)
        {
            CustomerInfoViewModel customerCreated = _logic.CreateUser(signUpView);

            if (customerCreated == null)
            {
                ModelState.AddModelError("Failure", "Username already exists");
                return View("CreateCustomer");
            }

            return RedirectToAction("NewCustomer", customerCreated);
        }

       public IActionResult Logout()
        {
            HttpContext.Session.Clear();

            return RedirectToAction("Index", "Home");
        }
    }
}
