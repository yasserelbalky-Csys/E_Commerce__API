using BLL.Contracts;
using BLL.DTOs.CategoryDtos;
using BLL.DTOs.CategoryDTOs;
using DAL.Contracts;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    internal class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitofwork;

        public CategoryService(IUnitOfWork unitofwork)
        {
            _unitofwork = unitofwork;
        }

        public IEnumerable<CategoryListDto> GetCategories()
        {
            return _unitofwork.Categories.GetAll().Select(cat => new CategoryListDto
            {
                CategoryId = cat.CategoryId,
                CategoryName = cat.CategoryName,
                CategoryDescription = cat.CategoryDescription,
                b_deleted = cat.b_deleted
            }).Where(m => m.b_deleted == false);
        }

        public CategoryListDto GetCategory(int id)
        {
            var entity = _unitofwork.Categories.GetById(id);
            return new CategoryListDto
            {
                CategoryId = entity.CategoryId,
                CategoryName = entity.CategoryName,
                CategoryDescription = entity.CategoryDescription,
                b_deleted = entity.b_deleted
            };
        }

        public void InsertCategory(CategoryInsertDto category)
        {
            _unitofwork.Categories.Insert(new Categories
            {
                CategoryName = category.CategoryName,
                CategoryDescription = category.CategoryDescription,
                b_deleted = false
            });
            _unitofwork.save();
        }

        public void UpdateCategory(CategoryUpdateDto category)
        {
            var cat = _unitofwork.Categories.GetById(category.CategoryId);
            if (cat != null)
            {
                cat.CategoryId = category.CategoryId;
                cat.CategoryName = category.CategoryName;
                cat.CategoryDescription = category.CategoryDescription;
                cat.b_deleted = category.b_deleted;
                _unitofwork.Categories.Update(cat);
            }
            else
            {
                throw new KeyNotFoundException($"Category with ID {category.CategoryId} not found.");
            }

            _unitofwork.save();
        }

        public void DeleteCategory(int id)
        {
            var cat = _unitofwork.Categories.GetById(id);
            if (cat != null)
            {
                cat.b_deleted = true;
            }
            else
            {
                throw new KeyNotFoundException($"Category with ID {id} not found.");
            }

            _unitofwork.save();
        }

        public IEnumerable<SubCategoryByMainCategoryDto> GetSubCategoriesBymainCategoryId(int mainCategoryId)
        {
            return _unitofwork.Categories
        .GetSubCategoriesByMainCategory(mainCategoryId)
        .SelectMany(c => c.SubCategories.Select(sub => new SubCategoryByMainCategoryDto
        {
            CategoryId = c.CategoryId,
            CategoryName = c.CategoryName,
            CategoryDescription = c.CategoryDescription,
            SubCategoryId = sub.SubCategoryId,
            SubCategoryName = sub.SubCategoryName,
            SubCategoryDescription = sub.SubCategoryDescription,
            b_deleted_sub = sub.b_deleted
        })).Where(sub => sub.b_deleted_sub == false);
        }
    }
}