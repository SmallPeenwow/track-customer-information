using track_customer_information.Models;

namespace track_customer_information.Interfaces
{
    public interface ICustomerService
    {
        Task<List<Customer>> FetchAllCustomersAsync();
    }
}
