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
        protected readonly ISubCategoryRepository _subcategoryRepository;

        public SubCategoryService(ISubCategoryRepository subcategoryRepository)
        {
            _subcategoryRepository = subcategoryRepository;
        }

       
        public IEnumerable<SubCategoryListDto> GetSubCategories()
        {
            return _subcategoryRepository.GetAll().Select(x => new SubCategoryListDto
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
            var enitiy = _subcategoryRepository.GetById(id);

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
            _subcategoryRepository.Insert(new SubCategories
            {
                SubCategoryName = subcategory.SubCategoryName,
                SubCategoryDescription = subcategory.SubCategoryDescription,
                CategoryId = subcategory.MainCategoryId,
            });
        }
        
        public void UpdateSubCategory(SubCategoryUpdateDto subcategory)
        {
            _subcategoryRepository.Update(new SubCategories
            {
                SubCategoryName = subcategory.SubCategoryName,
                SubCategoryDescription = subcategory.SubCategoryDescription,
                CategoryId = subcategory.CategoryId,
                SubCategoryId = subcategory.SubCategoryId
            });



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
            _subcategoryRepository.Delete(id);
        }

    }
}


