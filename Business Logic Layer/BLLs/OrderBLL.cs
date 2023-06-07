using AutoMapper;
using Business_Logic_Layer.DTO;
using Data_Access_Layer.DALs;
using Data_Access_Layer.Data;
using Data_Access_Layer.Models;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Business_Logic_Layer.BLLs
{
    public class OrderBLL
    {
        private OrderDAL _DAL;
        private readonly IMapper _mapper;

        public OrderBLL(NorthwindContext context, IMapper mapper)
        {
            _DAL = new OrderDAL(context);
            _mapper = mapper;
        }

        public async Task<List<OrderDto>> GetOrders()
        {
            var orders = await _DAL.GetOrders();
            var orderDtos = _mapper.Map<List<OrderDto>>(orders);

            return orderDtos;
        }

        public async Task<OrderDto> GetOrder(int id)
        {
            var order = _DAL.GetOrder(id);
            var orderDto = _mapper.Map<OrderDto>(order);

            return orderDto;
        }

        public async Task<List<OrderDto>> GetOrdersByCustomer(string customerId)
        {
            var orders = _DAL.GetOrdersByCustomer(customerId);
            var orderDtos = _mapper.Map<List<OrderDto>>(orders);

            return orderDtos;
        }

        public async Task<List<OrderDto>> GetByCustomerAndEmployee(string customerId, int employeeId)
        {
            var orders = _DAL.GetByCustomerAndEmployee(customerId, employeeId);
            var orderDtos = _mapper.Map<List<OrderDto>>(orders);

            return orderDtos;
        }

        public bool OrderExists(int id)
        {
            return _DAL.OrderExists(id);
        }

        public bool CreateOrder(OrderCreateDto neworder)
        {
            var order = _mapper.Map<Order>(neworder);

            try
            {
                _DAL.CreateOrder(order);
            }
            catch (DbUpdateException)
            {
                if (OrderExists(order.OrderId))
                    return false;
                else
                    throw;
            }

            return true;
        }

        public bool UpdateOrder(OrderDto orderDto)
        {
            var order = _mapper.Map<Order>(orderDto);

            try
            {
                _DAL.UpdateOrder(order);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderExists(order.OrderId))
                    return false;
                else
                    throw;
            }

            return true;
        }

        public bool DeleteOrder(int id)
        {
            try
            {
                _DAL.DeleteOrder(id);
            }
            catch (DBConcurrencyException)
            {
                if (!OrderExists(id))
                    return false;
                else
                    throw;
            }

            return true;
        }
    }
}
