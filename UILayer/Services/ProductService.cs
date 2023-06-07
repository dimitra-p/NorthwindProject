using Business_Logic_Layer.DTO;

namespace UILayer.Services
{
    public class ProductService: IProductService
    {
        private readonly HttpClient httpClient;

        public ProductService(HttpClient _httpClient)
        {
            this.httpClient = _httpClient;
        }

        public Task<List<ProductDto>> GetProducts()
        {
            return httpClient.GetFromJsonAsync<List<ProductDto>>("https://localhost:7196/api/Product");
        }

        public async Task<ProductDto> GetProduct(int productId)
        {
            return await httpClient.GetFromJsonAsync<ProductDto>($"https://localhost:7196/api/Product/{productId}");
        }

        public async Task EditProduct(ProductDto newproduct)
        {
            var response = await httpClient.PutAsJsonAsync($"https://localhost:7196/api/Product/{newproduct.ProductId}", newproduct);
            response.EnsureSuccessStatusCode();
        }

        public async Task AddProduct(ProductCreateDto newproduct)
        {
            var response = await httpClient.PostAsJsonAsync("https://localhost:7196/api/Product", newproduct);
            response.EnsureSuccessStatusCode();
        }

        public void DeleteProduct(int productId)
        {
            httpClient.DeleteAsync($"https://localhost:7196/api/Product/{productId}");
        }
    }
}
