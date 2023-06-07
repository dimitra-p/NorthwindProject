using Business_Logic_Layer.DTO;

namespace UILayer.Services
{
    public class OrderService : IOrderService
    {
        private readonly HttpClient httpClient;

        public OrderService(HttpClient _httpClient)
        {
            this.httpClient = _httpClient;
        }

        public Task<List<OrderDto>> GetOrders()
        {
            return httpClient.GetFromJsonAsync<List<OrderDto>>("https://localhost:7196/api/Order");
        }

        public async Task<OrderDto> GetOrder(int orderId)
        {
            return await httpClient.GetFromJsonAsync<OrderDto>($"https://localhost:7196/api/Order/{orderId}");
        }

        public async Task EditOrder(OrderDto neworder)
        {
            var response = await httpClient.PutAsJsonAsync($"https://localhost:7196/api/Order/{neworder.OrderId}", neworder);
            response.EnsureSuccessStatusCode();
        }

        public async Task AddOrder(OrderCreateDto neworder)
        {
            var response = await httpClient.PostAsJsonAsync("https://localhost:7196/api/Order", neworder);
            response.EnsureSuccessStatusCode();
        }

        public void DeleteOrder(int orderId)
        {
            httpClient.DeleteAsync($"https://localhost:7196/api/Order/{orderId}");
        }
    }
}
