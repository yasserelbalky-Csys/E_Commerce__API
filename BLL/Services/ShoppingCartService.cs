using BLL.Contracts;
using BLL.DTOs.ProductDtos;
using BLL.DTOs.ShoppingCartDtos;
using DAL.Contracts;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    internal class ShoppingCartService : IShoppingCartService
    {

        protected readonly IShoppingCartRepository _ShoppingCartrepository;

        public ShoppingCartService(IShoppingCartRepository ShoppingCartrepository)
        {
            _ShoppingCartrepository = ShoppingCartrepository;
        }

       

      

        public IEnumerable<ShoppingCartListDto> GetShoppingCarts()
        {

            return _ShoppingCartrepository.GetAll().Select(cart =>
                new ShoppingCartListDto
                {
                    UserId = cart.UserId,
                    Count = cart.Count,
                    ShoppingCartId = cart.ShoppingCartId

                }
                );
        }

        public ShoppingCartListDto GetShoppingCart(int id)
        {
            throw new NotImplementedException();
        }

        public void InsertShoppingCart(ShoppingCartInsertDto category)
        {
            throw new NotImplementedException();
        }

        public void UpdateShoppingCart(ShoppingCartUpdateDto category)
        {
            throw new NotImplementedException();
        }

        public void DeleteShoppingCart(int id)
        {
            throw new NotImplementedException();
        }
    }
}
