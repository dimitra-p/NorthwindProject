using Data_Access_Layer.Data;
using Data_Access_Layer.Models;
using Data_Access_Layer.DALs.Interfaces;

namespace Data_Access_Layer.DALs
{
    public class ProductDAL : IProductRepository
    {
        private readonly NorthwindContext _context;

        public ProductDAL(NorthwindContext context)
        {
            _context = context;
        }

        public ICollection<Product> GetProducts()
        {
            return _context.Products.ToList();
        }

        public Product GetProduct(int id)
        {
            return _context.Products.Where(p => p.ProductId == id).FirstOrDefault();
        }

        public bool ProductExists(int id)
        {
            return (_context.Products?.Any(p => p.ProductId == id)).GetValueOrDefault();
        }

        public bool CreateProduct(Product product)
        {
            _context.Products.Add(product);
            return Save();
        }

        public bool UpdateProduct(Product product)
        {
            _context.Update(product);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0;
        }
    }
}
