using AutoMapper;
using Business_Logic_Layer.DTO;
using Data_Access_Layer.DALs;
using Data_Access_Layer.Data;
using Data_Access_Layer.Models;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Business_Logic_Layer.BLLs
{
    public class CustomerBLL
    {
        private CustomerDAL _DAL;
        private readonly IMapper _mapper;

        public CustomerBLL(NorthwindContext context, IMapper mapper)
        {
            _DAL = new CustomerDAL(context);
            _mapper = mapper;
        }

        public async Task<List<CustomerDto>> GetCustomers()
        {
            var customers = await _DAL.GetCustomers();
            var customerDtos = _mapper.Map<List<CustomerDto>>(customers);

            return customerDtos; 

        }

        public async Task<CustomerDto> GetCustomer(string id)
        {
            var customer = _DAL.GetCustomer(id);
            var customerDto = _mapper.Map<CustomerDto>(customer);

            return customerDto;
        }

        public async Task<List<CustomerDto>> GetCustomersByCompanyName(string companyName)
        {
            var customers = await _DAL.GetCustomersByCompanyName(companyName);
            var customerDtos = _mapper.Map<List<CustomerDto>>(customers);

            return customerDtos;
        }

        public bool CustomerExists(string id)
        {
            return _DAL.CustomerExists(id);
        }

        public bool CreateCustomer(CustomerDto newcustomer)
        {
            var customer = _mapper.Map<Customer>(newcustomer);

            try
            {
                _DAL.CreateCustomer(customer);
            }
            catch (DbUpdateException)
            {
                if (CustomerExists(newcustomer.CustomerId))
                    return false;
                else
                    throw;
            }

            return true;
        }

        public bool UpdateCustomer(CustomerDto customerDto)
        {
            var customer = _mapper.Map<Customer>(customerDto);

            try
            {
                _DAL.UpdateCustomer(customer);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerExists(customer.CustomerId))
                {
                    return false;
                }
                else
                {
                    throw;
                }
            }

            return true;
        }

        public bool DeleteCustomer(string id)
        {
            try
            {
                _DAL.DeleteCustomer(id);
            }
            catch (DBConcurrencyException)
            {
                if (!CustomerExists(id))
                    return false;
                else
                    throw;
            }

            return true;
        }

    }
}