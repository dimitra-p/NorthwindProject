using Data_Access_Layer.Models;

namespace Data_Access_Layer.DALs.Interfaces
{
    internal interface IProductRepository
    {
        public ICollection<Product> GetProducts();

        public Product GetProduct(int id);

        public bool ProductExists(int id);

        public bool CreateProduct(Product product);

        public bool UpdateProduct(Product product);

        public bool Save();

    }
}
