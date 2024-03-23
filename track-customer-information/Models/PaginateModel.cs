using track_customer_information.Models;

namespace track_customer_information.Models
{
    public class PaginateModel
    {
        public IEnumerable<CustomerModel>? Customers { get; set; }

        public int PageNumber { get; set; }

        public int PageSize { get; set; }

        public int TotalCustomers { get; set; }

        public int TotalPages => (int)Math.Ceiling((double)TotalCustomers / PageSize);

        public bool HasPreviousPage => PageNumber > 1;

        public bool HasNextPage => PageNumber < TotalPages;
    }
}
