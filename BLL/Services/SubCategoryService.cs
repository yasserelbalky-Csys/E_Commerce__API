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
    internal class SubCategoryService : ISubCategoryService
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
                MainCategoryName = x.Category.CategoryName,
                b_deleted = x.b_deleted
            }).Where(sub => sub.b_deleted == false);
        }

        public SubCategoryListDto GetSubCategory(int id)
        {
            var enitiy = _unitofwork.subCategories.GetById(id);

            return new SubCategoryListDto
            {
                SubCategoryId = enitiy.SubCategoryId,
                SubCategoryName = enitiy.SubCategoryName,
                SubCategoryDescription = enitiy.SubCategoryDescription,
                MainCategoryId = enitiy.CategoryId,
                MainCategoryName = enitiy.Category.CategoryName,
                b_deleted = enitiy.b_deleted
            };
        }

        public void InsertSubCategory(SubCategoryInsertDto subcategory)
        {
            _unitofwork.subCategories.Insert(new SubCategories
            {
                SubCategoryName = subcategory.SubCategoryName,
                SubCategoryDescription = subcategory.SubCategoryDescription,
                CategoryId = subcategory.MainCategoryId,
                b_deleted = false
            });
            _unitofwork.save();
        }

        public void UpdateSubCategory(SubCategoryUpdateDto subcategory)
        {
            var entity = _unitofwork.subCategories.GetById(subcategory.SubCategoryId);
            if (entity != null)
            {
                entity.SubCategoryName = subcategory.SubCategoryName;
                entity.SubCategoryDescription = subcategory.SubCategoryDescription;
                entity.CategoryId = subcategory.CategoryId;
                entity.SubCategoryId = subcategory.SubCategoryId;
                entity.b_deleted = subcategory.b_deleted;

                _unitofwork.subCategories.Update(entity);
            }
            else
            {
                throw new KeyNotFoundException($"SubCategory with ID {subcategory.SubCategoryId} not found.");
            }

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
            var cat = _unitofwork.subCategories.GetById(id);
            if (cat != null)
            {
                cat.b_deleted = true;
            }
            else
            {
                throw new KeyNotFoundException($"SubCategory with ID {id} not found.");
            }

            _unitofwork.save();
        }
    }
}