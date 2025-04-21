using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Contracts;
using BLL.DTOs.BrandDtos;
using DAL.Contracts;
using DAL.Entities;

namespace BLL.Services
{
	internal class BrandService : IBrandService
	{
		private readonly IUnitOfWork _unitofwork;

		public BrandService(IUnitOfWork unitofwork)
		{
			_unitofwork = unitofwork;
		}

		public void DeleteBrand(int id)
		{
			var brandd = _unitofwork.brands.GetById(id);
			if (brandd != null) {
				brandd.b_deleted = true;
			} else {
				throw new KeyNotFoundException($"Brand with ID {id} not found.");
			}

			_unitofwork.save();
		}

		public BrandListDto GetBrand(int id)
		{
			var temp = _unitofwork.brands.GetById(id);
			if (temp != null) {
				return new BrandListDto {
					BrandId = temp.BrandId,
					BrandName = temp.BrandName,
					BrandDescription = temp.BrandDescription,
					b_deleted = temp.b_deleted,
				};
			} else {
				return new BrandListDto {
					BrandId = 0,
					BrandName = "",
					BrandDescription = "",
					b_deleted = true,
				};
			}
		}

		public IEnumerable<BrandListDto> GetBrands()
		{
			return _unitofwork.brands.GetAll().Select(b => new BrandListDto {
				BrandId = b.BrandId,
				BrandName = b.BrandName,
				BrandDescription = b.BrandDescription,
				b_deleted = b.b_deleted
			}).Where(m => m.b_deleted == false);
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
			_unitofwork.brands.Insert(new Brands {
				BrandName = brand.BrandName,
				BrandDescription = brand.BrandDescription,
				b_deleted = false
			});
			_unitofwork.save();
		}

		public int UpdateBrand(BrandUpdateDto brand)
		{
			var temp = _unitofwork.brands.GetById(brand.BrandId);
			if (temp != null) {
				temp.BrandName = brand.BrandName;
				temp.BrandDescription = brand.BrandDescription;
				temp.b_deleted = brand.b_deleted;
				_unitofwork.brands.Update(temp);
				_unitofwork.save();
				return 1;
			} else {
				return 0;
			}
		}
	}
}