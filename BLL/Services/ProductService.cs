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

        protected readonly IUnitOfWork _uow;

        public ProductService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public IEnumerable<ProductListDto> GetProducts()
        {
            return _uow.products.GetAll().Select( prod=>
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
            var prod= _uow.products.GetById(id);
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
            _uow.products.Insert(new Products
            {
                ProductDiscription=product.ProductDiscription,
                ProductName=product.ProductName,
                SubcategoryId=product.SubcategoryId,
                ProductPrice=product.ProductPrice,
                BrandId = product.BrandId
            }
            );
            _uow.save();
        }

        public void UpdateProduct(ProductUpdateDto product)
        {
            var upproduct=_uow.products.GetById(product.ProductId);
            if (upproduct != null)
            {
                upproduct.ProductId = product.ProductId;
                upproduct.ProductName = product.ProductName;
                upproduct.ProductDiscription = product.ProductDiscription;
                upproduct.ProductPrice = product.ProductPrice;
                upproduct.SubcategoryId = product.SubcategoryId;
                upproduct.BrandId = product.BrandId;
                _uow.products.Update(upproduct);
            }
            else
            {
                throw new KeyNotFoundException($"Product with ID {product.ProductId} not found.");
            }
            _uow.save();
        }

        public void DeleteProduct(int id)
        {
            _uow.products.Delete(id);
            _uow.save();
        }

    }
}
