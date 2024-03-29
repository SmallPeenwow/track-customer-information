﻿using track_customer_information.Models;

namespace track_customer_information.Interfaces
{
    public interface ICustomerService
    {
        Task<List<CustomerModel>> FetchAllCustomersAsync();

        Task DeleteCustomer(CustomerModel customer);

        Task<CustomerModel> GetCustomerById(int id);

        Task EditCustomer(CustomerModel customer);

        Task CreateNewCustomer(CustomerModel customer);

        Task<CustomerModel?> GetCustomerByEmail(string email);
    }
}
