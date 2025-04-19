using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.DTOs.CategoryDTOs;
using BLL.DTOs.ShoppingCartDtos;
using DAL.Contracts;

namespace BLL.Contracts
{
	public interface IShoppingCartService
	{
		public IEnumerable<ShoppingCartListDto> GetShoppingCarts();
		public ShoppingCartListDto GetShoppingCart(int id);
		public void InsertShoppingCart(ShoppingCartInsertDto category);
		public void UpdateShoppingCart(ShoppingCartUpdateDto category);
		public void DeleteShoppingCart(int id);
		public decimal GetTotalCartPrice(string userId);

		//bool ProductExistsInCart(string userId, int productId);
		IEnumerable<ShoppingCartListDto> GetUserCart(string userId);
		void ClearUserCart(string userId);
	}
}