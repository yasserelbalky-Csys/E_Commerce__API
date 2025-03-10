using BLL.Contracts;
using BLL.DTOs.SubCategoryDtos;
using DAL.Contracts;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    internal class SubCategoryService:ISubCategoryService
    {
        private readonly IUnitOfWork _unitofwork;

        public SubCategoryService(IUnitOfWork unitofwork)
        {
            _unitofwork = unitofwork;
        }

       
        public IEnumerable<SubCategoryListDto> GetSubCategories()
        {
            return _unitofwork.subCategories.GetAll().Select(x => new SubCategoryListDto
            {
                SubCategoryId = x.SubCategoryId,
                SubCategoryName = x.SubCategoryName,
                SubCategoryDescription = x.SubCategoryDescription,
                MainCategoryId = x.CategoryId,
                MainCategoryName = x.Category.CategoryName
            });
        }

        public SubCategoryListDto GetSubCategory(int id)
        {
            var enitiy = _unitofwork.subCategories.GetById(id);

            return new SubCategoryListDto
            {
                SubCategoryId=enitiy.SubCategoryId,
                SubCategoryName=enitiy.SubCategoryName,
                SubCategoryDescription=enitiy.SubCategoryDescription,
                MainCategoryId =enitiy.CategoryId,
                MainCategoryName=enitiy.Category.CategoryName
            };

        }

        public void InsertSubCategory(SubCategoryInsertDto subcategory)
        {
            _unitofwork.subCategories.Insert(new SubCategories
            {
                SubCategoryName = subcategory.SubCategoryName,
                SubCategoryDescription = subcategory.SubCategoryDescription,
                CategoryId = subcategory.MainCategoryId,
            });
            _unitofwork.save();
        }
        
        public void UpdateSubCategory(SubCategoryUpdateDto subcategory)
        {
            _unitofwork.subCategories.Update(new SubCategories
            {
                SubCategoryName = subcategory.SubCategoryName,
                SubCategoryDescription = subcategory.SubCategoryDescription,
                CategoryId = subcategory.CategoryId,
                SubCategoryId = subcategory.SubCategoryId
            });
            _unitofwork.save();



            //var existingEntity = _subcategoryRepository.GetById(subcategory.SubCategoryId);
            //if (existingEntity == null)
            //{
            //    throw new KeyNotFoundException($"SubCategory with ID {subcategory.SubCategoryId} not found.");
            //}

            //// Update only the necessary fields
            //existingEntity.SubCategoryName = subcategory.SubCategoryName;
            //existingEntity.SubCategoryDescription = subcategory.SubCategoryDescription;
            //existingEntity.CategoryId = subcategory.CategoryId;


            //_subcategoryRepository.Update(existingEntity);
        }

        public void DeleteCategory(int id)
        {
            _unitofwork.subCategories.Delete(id);
            _unitofwork.save();
        }

    }
}


