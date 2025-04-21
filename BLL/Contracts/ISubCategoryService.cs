using BLL.DTOs.CategoryDTOs;
using BLL.DTOs.SubCategoryDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Contracts
{
	public interface ISubCategoryService
	{
		public IEnumerable<SubCategoryListDto> GetSubCategories();
		public SubCategoryListDto GetSubCategory(int id);
		public void InsertSubCategory(SubCategoryInsertDto subcategory);
		public void UpdateSubCategory(SubCategoryUpdateDto category);
		public void DeleteCategory(int id);
	}
}