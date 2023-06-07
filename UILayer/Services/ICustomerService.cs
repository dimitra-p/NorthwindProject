using Business_Logic_Layer.DTO;

namespace UILayer.Services
{
    public interface ICustomerService
    {
        public Task<List<CustomerDto>> GetCustomers();

        public Task<CustomerDto> GetCustomer(string customerId);

        public Task EditCustomer(CustomerDto newcustomer);

        public Task AddCustomer(CustomerDto newcustomer);

        public void DeleteCustomer(string id);
    }
}
