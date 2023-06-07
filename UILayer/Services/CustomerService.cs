using Business_Logic_Layer.DTO;

namespace UILayer.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly HttpClient httpClient;

        public CustomerService(HttpClient _httpClient)
        {
            this.httpClient = _httpClient;
        }

        public Task<List<CustomerDto>> GetCustomers()
        {
            return httpClient.GetFromJsonAsync<List<CustomerDto>>("https://localhost:7196/api/Customer");
        }

        public async Task<CustomerDto> GetCustomer(string customerId)
        {
            return await httpClient.GetFromJsonAsync<CustomerDto>($"https://localhost:7196/api/Customer/{customerId}");
        }

        public async Task EditCustomer(CustomerDto newcustomer)
        {
            var response = await httpClient.PutAsJsonAsync($"https://localhost:7196/api/Customer/{newcustomer.CustomerId}", newcustomer);
            response.EnsureSuccessStatusCode();
        }

        public async Task AddCustomer(CustomerDto newcustomer)
        {
                var response = await httpClient.PostAsJsonAsync("https://localhost:7196/api/Customer", newcustomer);
                response.EnsureSuccessStatusCode();
        }

        public void DeleteCustomer(string id)
        {
            httpClient.DeleteAsync($"https://localhost:7196/api/Customer/{id}");
        }
    }
}
