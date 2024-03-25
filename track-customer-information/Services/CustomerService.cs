using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using track_customer_information.Data;
using track_customer_information.Interfaces;
using track_customer_information.Models;

namespace track_customer_information.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ApplicationDbContext _db;

        public CustomerService(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task CreateNewCustomer(CustomerModel customer)
        {
            _db.Customer.Add(customer);

            await _db.SaveChangesAsync();
        }

        public async Task DeleteCustomer(CustomerModel customer)
        {
            _db.Customer.Remove(customer);

            await _db.SaveChangesAsync();
        }

        public async Task EditCustomer(CustomerModel customer)
        {
            _db.Customer.Update(customer);

            await _db.SaveChangesAsync();
        }

        public async Task<List<CustomerModel>> FetchAllCustomersAsync()
        {
            return await _db.Customer.ToListAsync();
        }

        public async Task<CustomerModel?> GetCustomerByEmail(string email)
        {
            return await _db.Customer.FirstOrDefaultAsync(c => c.ContactPersonEmail == email);
        }

        public async Task<CustomerModel> GetCustomerById(int id)
        {
            return await _db.Customer.FirstOrDefaultAsync(c => c.CustomerID == id);
        }
    }
}
