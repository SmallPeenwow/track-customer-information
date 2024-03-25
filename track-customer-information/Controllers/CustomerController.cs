using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using track_customer_information.Data;
using track_customer_information.Interfaces;
using track_customer_information.Models;

namespace track_customer_information.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        public async Task<IActionResult> Index(string filterType, string filterValue, int pageNumber = 1, int pageSize = 10)
        {
            List<CustomerModel> customers = await _customerService.FetchAllCustomersAsync();

            if (!string.IsNullOrEmpty(filterType) && !string.IsNullOrEmpty(filterValue))
            {
                switch (filterType)
                {
                    case "Name":
                        customers = customers.Where(c => c.Name.Contains(filterValue)).ToList();
                        break;
                    case "VATNumber":
                        customers = customers.Where(c => c.VATNumber == filterValue).ToList();
                        break;
                }
            }

            int totalCustomers = customers.Count();

            IEnumerable<CustomerModel> paginatedCustomers = customers.Skip((pageNumber - 1) * pageSize).Take(pageSize);

            // populate the view model
            var paginateModel = new PaginateModel
            {
                Customers = paginatedCustomers,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalCustomers = totalCustomers
            };

            var viewModel = new CustomerListViewModel
            {
                Customers = paginatedCustomers,
                Pagination = paginateModel
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            CustomerModel customer = await _customerService.GetCustomerById(id);

            if (customer == null)
            {
                return NotFound();
            }

            await _customerService.DeleteCustomer(customer);

            TempData["SuccessMessage"] = "Customer successfully deleted.";

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            CustomerModel editCustomer = await _customerService.GetCustomerById(id);

            if (editCustomer == null)
            {
                return NotFound();
            }

            return PartialView("Edit", editCustomer);
        }

        public IActionResult Create()
        {
            return PartialView();
        }

        [HttpPost]
        public async Task<IActionResult> CreateCustomer(CustomerModel customer)
        {
            if (ModelState.IsValid)
            {
                customer.ContactPersonEmail = customer.ContactPersonEmail?.ToLower();

                // Check if the email is already in use
                var existingCustomer = await _customerService.GetCustomerByEmail(customer.ContactPersonEmail);
               
                if (existingCustomer != null)
                {
                    ModelState.AddModelError("ContactPersonEmail", "The email address is already in use.");
                }
                else
                {
                    await _customerService.CreateNewCustomer(customer);

                    return Json(new { redirect = true });
                }
            }

            return PartialView("Create", customer);
        }

        public IActionResult DynamicView(int? customerId)
        {
            string viewName = customerId.HasValue ? $"Edit/{customerId}" : "Create";
            
            ViewBag.ViewName = viewName;
            
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UpdateCustomer(CustomerModel customer)
        {
            if (ModelState.IsValid)
            {
                await _customerService.EditCustomer(customer);

                return Json(new { redirect = true });
            }
            else
            {
                return PartialView("Edit", customer);
            }
        }
    }
}
