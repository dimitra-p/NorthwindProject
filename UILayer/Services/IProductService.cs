using Business_Logic_Layer.DTO;

namespace UILayer.Services
{
    public interface IProductService
    {
        public Task<List<ProductDto>> GetProducts();

        public Task<ProductDto> GetProduct(int productId);

        public Task EditProduct(ProductDto newproduct);

        public Task AddProduct(ProductCreateDto newproduct);

        public void DeleteProduct(int productId);
    }
}
