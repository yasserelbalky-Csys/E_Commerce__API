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
        private readonly IUnitOfWork _unitofwork;


        public CategoryService(IUnitOfWork unitofwork)
        {
            _unitofwork = unitofwork;
        }

        

        public IEnumerable<CategoryListDto> GetCategories()
        {

            
            return _unitofwork.Category.GetAll().Select(cat => new CategoryListDto
            {
                CategoryId = cat.CategoryId,
                CategoryName = cat.CategoryName,
                CategoryDescription = cat.CategoryDescription
            });
        }

        public CategoryListDto GetCategory(int id)
        {
            var entity= _unitofwork.Category.GetById(id);
            return new CategoryListDto
            {
                CategoryId= entity.CategoryId,
                CategoryName= entity.CategoryName,
                CategoryDescription= entity.CategoryDescription
            };
        }

        public void InsertCategory(CategoryInsertDto category)
        {
            _unitofwork.Category.Insert(new Categories
            {
                CategoryName= category.CategoryName, CategoryDescription= category.CategoryDescription
            });
            _unitofwork.save();
        }

        public void UpdateCategory(CategoryUpdateDto category)
        {
            _unitofwork.Category.Update(new Categories { 
            CategoryId=category.CategoryId,
            CategoryName= category.CategoryName,    
            CategoryDescription= category.CategoryDescription
            });
            _unitofwork.save();

        }


        public void DeleteCategory(int id)
        {
            _unitofwork.Category.Delete(id);
            _unitofwork.save();
        }
    }
}
