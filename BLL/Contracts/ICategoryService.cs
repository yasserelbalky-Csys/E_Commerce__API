using BLL.DTOs.CategoryDTOs;
using DAL.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Contracts
{
	public interface ICategoryService
	{
		public IEnumerable<CategoryListDto> GetCategories();
		public CategoryListDto GetCategory(int id);
		public void InsertCategory(CategoryInsertDto category);
		public void UpdateCategory(CategoryUpdateDto category);
		public void DeleteCategory(int id);
	}
}