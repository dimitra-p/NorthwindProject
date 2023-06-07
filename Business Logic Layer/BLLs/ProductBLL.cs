using AutoMapper;
using Business_Logic_Layer.DTO;
using Data_Access_Layer.DALs;
using Data_Access_Layer.Data;
using Data_Access_Layer.Models;
using Microsoft.EntityFrameworkCore;

namespace Business_Logic_Layer.BLLs
{
    public class ProductBLL
    {
        private ProductDAL _DAL;
        private readonly IMapper _mapper;

        public ProductBLL(NorthwindContext context, IMapper mapper)
        {
            _DAL = new ProductDAL(context);
            _mapper = mapper;
        }

        public ICollection<ProductDto> GetProducts()
        {
            var products = _DAL.GetProducts();
            var productDtos = _mapper.Map<List<ProductDto>>(products);

            return productDtos;
        }

        public ProductDto GetProduct(int id)
        {
            var product = _DAL.GetProduct(id);
            var productDto = _mapper.Map<ProductDto>(product);

            return productDto;
        }

        public bool ProductExists(int id)
        {
            return _DAL.ProductExists(id);
        }

        public bool CreateProduct(ProductCreateDto newproduct)
        {
            var product = _mapper.Map<Product>(newproduct);

            try
            {
                _DAL.CreateProduct(product);
            }
            catch (DbUpdateException)
            {
                if (ProductExists(product.ProductId))
                    return false; 
                else
                    throw;
            }

            return true;
        }

        public bool UpdateProduct(ProductDto productDto)
        {
            var product = _mapper.Map<Product>(productDto);

            try
            {
                _DAL.UpdateProduct(product);
            }
            catch (DbUpdateException)
            {
                if (!ProductExists(product.ProductId))
                    return false;
                else
                    throw;
            }

            return true;
        }

    }
}
