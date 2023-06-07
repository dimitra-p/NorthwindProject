using Data_Access_Layer.Data;
using Data_Access_Layer.Models;
using Microsoft.EntityFrameworkCore;

namespace Data_Access_Layer.DALs
{
    public class OrderDAL
    {
        private readonly NorthwindContext _context;

        public OrderDAL(NorthwindContext context)
        {
            _context = context;
        }

        public async Task<List<Order>> GetOrders()
        {
            return await _context.Orders.ToListAsync();
        }

        public Order GetOrder(int id)
        {
            return _context.Orders.Where(o => o.OrderId == id).FirstOrDefault();
        }

        public ICollection<Order> GetOrdersByCustomer(string customerId)
        {
            return _context.Orders.Where(o => o.CustomerId == customerId).ToList();
        }

        public ICollection<Order> GetByCustomerAndEmployee(string customerId, int employeeId)
        {
            return _context.Orders.Where(o => o.CustomerId == customerId && o.EmployeeId == employeeId).ToList();
        }

        public bool OrderExists(int id)
        {
            return (_context.Orders?.Any(o => o.OrderId == id)).GetValueOrDefault();
        }

        public bool CreateOrder(Order order)
        {
            _context.Orders.Add(order);
            return Save();
        }

        public bool UpdateOrder(Order order)
        {
            _context.Update(order);
            return Save();
        }

        public bool DeleteOrder(int id)
        {
            Order order = GetOrder(id);
            _context.Remove(order);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0;
        }
    }
}
