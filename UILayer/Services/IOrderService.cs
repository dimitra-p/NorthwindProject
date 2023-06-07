using Business_Logic_Layer.DTO;

namespace UILayer.Services
{
    public interface IOrderService
    {
        public Task<List<OrderDto>> GetOrders();

        public Task<OrderDto> GetOrder(int orderId);

        public Task EditOrder(OrderDto neworder);

        public Task AddOrder(OrderCreateDto neworder);

        public void DeleteOrder(int orderId);
    }
}
