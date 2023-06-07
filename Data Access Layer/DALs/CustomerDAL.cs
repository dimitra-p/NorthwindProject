using Data_Access_Layer.Data;
using Data_Access_Layer.Models;
using Data_Access_Layer.DALs.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Data_Access_Layer.DALs 
{
    public class CustomerDAL : ICustomerRepository
    {
        private readonly NorthwindContext _context;

        public CustomerDAL(NorthwindContext context) 
        {
            _context = context;
        }

        public async Task<List<Customer>> GetCustomers()
        {
            return await _context.Customers.ToListAsync();
        }
        public Customer GetCustomer(string id)
        {
            return _context.Customers.Where(c => c.CustomerId == id).FirstOrDefault();
        }

        public async Task<List<Customer>> GetCustomersByCompanyName(string companyName)
        {
            return await _context.Customers.Where(c => c.CompanyName.Contains(companyName)).ToListAsync();
        }

        public bool CustomerExists(string id)
        {
            return (_context.Customers?.Any(e => e.CustomerId == id)).GetValueOrDefault();
        }

        public bool CreateCustomer(Customer customer)
        {
            _context.Customers.Add(customer);
            return Save();
        }

        public bool UpdateCustomer(Customer customer)
        {
            _context.Update(customer);
            return Save();
        }

        public bool DeleteCustomer(string id)
        {
            Customer customer = GetCustomer(id);
            _context.Remove(customer);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0;
        }


    }
}