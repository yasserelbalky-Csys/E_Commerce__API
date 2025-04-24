using BLL.DTOs.BrandDtos;
using BLL.DTOs.CategoryDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Contracts
{
    public interface IBrandService
    {
        public IEnumerable<BrandListDto> GetBrands();
        public BrandListDto GetBrand(int id);
        public void InsertBrand(BrandInsertDto brand);

        public int UpdateBrand(BrandUpdateDto brand);

        // public IEnumerable<BrandListAllProductsDto> GetBrandWithProducts(int id);

        public void DeleteBrand(int id);
    }
}