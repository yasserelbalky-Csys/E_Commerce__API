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
        private readonly IUnitOfWork _uof;

        public ShoppingCartService(IUnitOfWork uof)
        {
            _uof = uof;
        }

        public IEnumerable<ShoppingCartListDto> GetShoppingCarts()
        {
            var Result = _uof.ShoppingCarts.GetAll().Select(cart => new ShoppingCartListDto
            {
                ShoppingCartId = cart.ShoppingCartId,
                UserId = cart.UserId,
                ProductId = cart.ProductId,
                ProductName = cart.Product.ProductName,
                price = cart.Product.ProductPrice,
                Count = cart.Count
            });

            return Result;
        }

        public ShoppingCartListDto GetShoppingCart(int id)
        {
            var temp = _uof.ShoppingCarts.GetById(id);
            return new ShoppingCartListDto
            {
                ShoppingCartId = temp.ShoppingCartId,
                Count = temp.Count,
                ProductId = temp.ProductId,
                UserId = temp.UserId
            };
        }

        public int InsertShoppingCart(ShoppingCartInsertDto cart)
        {
            var qty = _uof.ProductBalances.getProductbalance(cart.ProductId);
            if (cart.Count > qty)
            {
                return 1;
            }
            else
            {
                _uof.ShoppingCarts.Insert(new ShoppingCart
                {
                    Count = cart.Count,
                    ProductId = cart.ProductId,
                    UserId = cart.UserId,
                });
                _uof.save();
                return 0;
            }
        }

        public int UpdateShoppingCart(ShoppingCartUpdateDto shoppingcart)
        {
            var cartfromdatabase = _uof.ShoppingCarts.GetProductByuserid(shoppingcart.UserId, shoppingcart.ProductId);
            if (cartfromdatabase != null)
            {
                var qty = _uof.ProductBalances.getProductbalance(shoppingcart.ProductId);
                if (shoppingcart.Count > qty)
                {
                    return 1;
                }
                else
                {
                    cartfromdatabase.ProductId = shoppingcart.ProductId;
                    cartfromdatabase.UserId = shoppingcart.UserId;
                    cartfromdatabase.Count = shoppingcart.Count;
                    _uof.ShoppingCarts.Update(cartfromdatabase);
                    _uof.save();
                    return 0;
                }
            }
            else
            {
                throw new KeyNotFoundException("Shopping cart item not found.");
            }
        }

        public void DeleteShoppingCart(int id)
        {
            _uof.ShoppingCarts.Delete(id);
            _uof.save();
        }

        public IEnumerable<ShoppingCartListDto> GetUserCart(string userId)
        {
            return _uof.ShoppingCarts.GetByuseridOnly(userId).Select(cart => new ShoppingCartListDto
            {
                ShoppingCartId = cart.ShoppingCartId,
                ProductId = cart.ProductId,
                ProductName = cart.Product.ProductName,
                price = cart.Product.ProductPrice,
                UserId = cart.UserId,
                Count = cart.Count
            });
        }

        public void ClearUserCart(string userId)
        {
            var result = _uof.ShoppingCarts.GetByuseridOnly(userId);
            foreach (var cart in result)
            {
                _uof.ShoppingCarts.Delete(cart.ShoppingCartId);
                _uof.save();
            }
        }

        public decimal GetTotalCartPrice(string userId)
        {
            var result = _uof.ShoppingCarts.GetByuseridOnly(userId).Sum(cart => cart.Product.ProductPrice * cart.Count);
            return result;
        }
    }
}