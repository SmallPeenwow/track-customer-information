namespace track_customer_information.Models
{
    public class CustomerListViewModel
    {
        public IEnumerable<CustomerModel> Customers { get; set; }
        public PaginateModel Pagination { get; set; }
    }
}
