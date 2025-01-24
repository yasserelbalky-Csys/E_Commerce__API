using BLL.Contracts;
using BLL.DTOs.ProductDtos;
using DAL.Contracts;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    internal class ProductService : IProductService
    {

        protected readonly IProductRepository _productrepository;

        public ProductService(IProductRepository productrepository)
        {
            _productrepository = productrepository;
        }

        public IEnumerable<ProductListDto> GetProducts()
        {
            return _productrepository.GetAll().Select( prod=>
                new ProductListDto
                {
                    ProductId=prod.ProductId,
                    ProductName=prod.ProductName,
                    ProductDiscription=prod.ProductDiscription,
                    ProductPrice=prod.ProductPrice,
                    SubcategoryId=prod.SubcategoryId,
                    SubcategoryName = prod.Subcategory.SubCategoryName,
                    BrandId=prod.BrandId
                }
                );
        }

        public ProductListDto GetProduct(int id)
        {
            var prod= _productrepository.GetById(id);
            return new ProductListDto
            {
                ProductId = prod.ProductId,
                ProductName = prod.ProductName,
                ProductDiscription = prod.ProductDiscription,
                ProductPrice = prod.ProductPrice,
                SubcategoryId = prod.SubcategoryId,
                SubcategoryName = prod.Subcategory.SubCategoryName,
                BrandId = prod.BrandId
            };
        }

        public void InsertProduct(ProductInsertDto product)
        {
            _productrepository.Insert(new Products
            {
                ProductDiscription=product.ProductDiscription,
                ProductName=product.ProductName,
                SubcategoryId=product.SubcategoryId,
                ProductPrice=product.ProductPrice,
                BrandId = product.BrandId
            }
            );
        }

        public void UpdateProduct(ProductUpdateDto product)
        {
            var upproduct=_productrepository.GetById(product.ProductId);
            if (upproduct != null)
            {
                upproduct.ProductId = product.ProductId;
                upproduct.ProductName = product.ProductName;
                upproduct.ProductDiscription = product.ProductDiscription;
                upproduct.ProductPrice = product.ProductPrice;
                upproduct.SubcategoryId = product.SubcategoryId;
                upproduct.BrandId = product.BrandId;
                _productrepository.Update(upproduct);
            }
            else
            {
                throw new KeyNotFoundException($"Product with ID {product.ProductId} not found.");
            }
        }

        public void DeleteProduct(int id)
        {
            _productrepository.Delete(id);
        }

    }
}
