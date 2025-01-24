using BLL.Contracts;
using BLL.DTOs.BrandDtos;
using DAL.Contracts;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    internal class BrandService : IBrandService
    {
        private readonly IBrandRepository _brandRepository;
        public BrandService(IBrandRepository brandRepository)
        {
            _brandRepository = brandRepository;
        }

        public void DeleteBrand(int id)
        {
            _brandRepository.Delete(id);
        }

        public BrandListDto GetBrand(int id)
        {
            var temp = _brandRepository.GetById(id);
            return new BrandListDto
            {
                BrandId = temp.BrandId,
                BrandName = temp.BrandName,
                BrandDescription = temp.BrandDescription,
            };
        }

        public IEnumerable<BrandListDto> GetBrands()
        {
            return _brandRepository.GetAll().Select(b => new BrandListDto
            {
                BrandId = b.BrandId,
                BrandName = b.BrandName,
                BrandDescription = b.BrandDescription,
                
            });
        }

        //public IEnumerable<BrandListAllProductsDto> GetBrandWithProducts(int id)
        //{
            
        //    return _brandRepository.getBrandsWithItsProducts(id).Select(b=> 
        //    new BrandListAllProductsDto
        //    {
        //        BrandId= b.BrandId,
        //        BrandName= b.BrandName,
        //        BrandDescription= b.BrandDescription,
        //       // ProductId=b.Products.Select(p => p.ProductId).FirstOrDefault(),
        //    }
            
        //    );
        //}

        public void InsertBrand(BrandInsertDto brand)
        {
            _brandRepository.Insert(
                new Brands
                {
                    BrandName = brand.BrandName,
                    BrandDescription = brand.BrandDescription
                }
                );
        }

        public void UpdateBrand(BrandUpdateDto brand)
        {
            var temp = _brandRepository.GetById(brand.BrandId);
            if (temp != null )
            {
                temp.BrandName = brand.BrandName;
                temp.BrandDescription = brand.BrandDescription;
                _brandRepository.Update(temp);
            }
            else
            {
                
            }
        }
    }
}
