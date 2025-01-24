using BLL.Contracts;
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
        private readonly ICategoryRepository _repositoryCategory;


        public CategoryService(ICategoryRepository repositoryCategory)
        {
            _repositoryCategory = repositoryCategory;
        }

        

        public IEnumerable<CategoryListDto> GetCategories()
        {

            
            return _repositoryCategory.GetAll().Select(cat => new CategoryListDto
            {
                CategoryId = cat.CategoryId,
                CategoryName = cat.CategoryName,
                CategoryDescription = cat.CategoryDescription
            });
        }

        public CategoryListDto GetCategory(int id)
        {
            var entity= _repositoryCategory.GetById(id);
            return new CategoryListDto
            {
                CategoryId= entity.CategoryId,
                CategoryName= entity.CategoryName,
                CategoryDescription= entity.CategoryDescription
            };
        }

        public void InsertCategory(CategoryInsertDto category)
        {
            _repositoryCategory.Insert(new Categories
            {
                CategoryName= category.CategoryName, CategoryDescription= category.CategoryDescription
            });
        }

        public void UpdateCategory(CategoryUpdateDto category)
        {
            _repositoryCategory.Update(new Categories { 
            CategoryId=category.CategoryId,
            CategoryName= category.CategoryName,    
            CategoryDescription= category.CategoryDescription
            });

        }


        public void DeleteCategory(int id)
        {
            _repositoryCategory.Delete(id);
        }
    }
}
