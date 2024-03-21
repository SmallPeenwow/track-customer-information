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

        public async Task<IActionResult> Index()
        {
            //var customers = await _customerService.FetchAllCustomersAsync();
            //return View(customers);
            return View(await _customerService.FetchAllCustomersAsync());
        }
    }
}
