using BLL.Contracts;
using BLL.DTOs.ProductDtos;
using BLL.DTOs.ShoppingCartDtos;
using DAL.Contracts;
using DAL.Entities;
using Microsoft.AspNetCore.Http;
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
                    ProductId = cart.ProductId,
                    ProductName = cart.Product.ProductName,
                    Count = cart.Count,
                    ShoppingCartId = cart.ShoppingCartId

                }
                );





        }

        public ShoppingCartListDto GetShoppingCart(int id)
        {
            var temp = _ShoppingCartrepository.GetById(id);
            return new ShoppingCartListDto
            {
                ShoppingCartId = temp.ShoppingCartId,
                Count = temp.Count,
                ProductId = temp.ProductId,
                UserId = temp.UserId
            };
        }

        public void InsertShoppingCart(ShoppingCartInsertDto cart)
        {
           // var temp = _ShoppingCartrepository.GetAll().OrderByDescending(c => c.ShoppingCartId).FirstOrDefault();

            
            _ShoppingCartrepository.Insert(new ShoppingCart
            {
                Count = cart.Count,
                ProductId = cart.ProductId,
                UserId = cart.UserId,

            });
          
        }

        public void UpdateShoppingCart(ShoppingCartUpdateDto shoppingcart)
        {
            //var existingone = _ShoppingCartrepository.GetById(shoppingcart.ShoppingCartId);

            //var cartfromdatabase = _ShoppingCartrepository.GetAll().Select(cart1 =>
            //  cart1.UserId == shoppingcart.UserId && cart1.ProductId == shoppingcart.ProductId);

            var cartfromdatabase = _ShoppingCartrepository.GetByuserid(shoppingcart.UserId,shoppingcart.ProductId);
            if (cartfromdatabase != null) {

                cartfromdatabase.ProductId= shoppingcart.ProductId;
                cartfromdatabase.UserId = shoppingcart.UserId;
                cartfromdatabase.Count  = shoppingcart.Count;
                _ShoppingCartrepository.Update(cartfromdatabase);
            }
            else
            {
                throw new KeyNotFoundException("Shopping cart item not found.");
            }
           
        }

        public void DeleteShoppingCart(int id)
        {
            _ShoppingCartrepository.Delete(id);
        }
    }
}
