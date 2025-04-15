using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.DTOs.ProductDtos;
using BLL.DTOs.SubCategoryDtos;

namespace BLL.Contracts {
	public interface IProductService {
		public IEnumerable<ProductListDto> GetProducts();
		public ProductListDto GetProduct(int id);
		public void InsertProduct(ProductInsertDto product);
		public void UpdateProduct(ProductUpdateDto product);
		public void DeleteProduct(int id);
	}
}