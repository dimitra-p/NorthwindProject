using Data_Access_Layer.Models;

namespace Data_Access_Layer.DALs.Interfaces
{
    internal interface ICustomerRepository
    {
        public Task<List<Customer>> GetCustomers();

        public Customer GetCustomer(string id);

        public Task<List<Customer>> GetCustomersByCompanyName(string companyName);

        public bool CustomerExists(string id);

        public bool CreateCustomer(Customer customer);

        public bool UpdateCustomer(Customer customer);

        public bool DeleteCustomer(string id);

        public bool Save();


    }
}
