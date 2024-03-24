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

            return View(editCustomer);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateCustomer(CustomerModel customer)
        {
            if (ModelState.IsValid)
            {
                await _customerService.EditCustomer(customer);

                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View(customer);
            }
        }
    }
}
