using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Identity.Client;
using myshop.BLL.DTOs.Cart;
using myshop.BLL.IServices;
using myshop.DAL.Data;
using myshop.DAL.Iconfiguration;
using myshop.Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace myshop.BLL.Services
{
    public class CartService : ICartService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CartService(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<bool> AddProductToCartAsync(int id)
        {
            try
            {
                var product = await _unitOfWork.Products.GetById(id);
                var mappedProduct = _mapper.Map<CartItem>(product);
                var check = _httpContextAccessor.HttpContext.Session.GetString("Customer Product");
                if (string.IsNullOrEmpty(check))
                {
                    List<CartItem> listOfItems = new List<CartItem>();
                    mappedProduct.Quantity = 1;
                    mappedProduct.TotalPrice = mappedProduct.Price;
                    listOfItems.Add(mappedProduct);
                    var jsonProduct = JsonSerializer.Serialize(listOfItems);
                    _httpContextAccessor.HttpContext.Session.SetString("Customer Product", jsonProduct);
                }

                else
                {
                    var deserializedListOfProducts = JsonSerializer.Deserialize<List<CartItem>>(check) ?? throw new NullReferenceException();
                    var exstPtoduct = deserializedListOfProducts.FirstOrDefault(p => p.ProductId == mappedProduct.ProductId);
                    if (exstPtoduct != null)
                    {
                        IncreaseQuantityOfItem(exstPtoduct.ProductId);
                        return true;
                    }
                    else
                    {
                        mappedProduct.Quantity = 1;
                        mappedProduct.TotalPrice = mappedProduct.Price;
                        deserializedListOfProducts.Add(mappedProduct);
                    }
                    var serializeListOfProducts = JsonSerializer.Serialize(deserializedListOfProducts);
                    _httpContextAccessor.HttpContext.Session.SetString("Customer Product", serializeListOfProducts);

                }
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            }

        public  IEnumerable<CartItem> DecreaseQuantityOfItem(int id)
        {
            var cartItems = _httpContextAccessor.HttpContext.Session.GetString("Customer Product");
            if (string.IsNullOrEmpty(cartItems))
                throw new NullReferenceException("The Cart is empty add items now");
            var deserializeCartItems = JsonSerializer.Deserialize<List<CartItem>>(cartItems);
            var item = deserializeCartItems.FirstOrDefault(c => c.ProductId == id);
            if (item != null)
            {
                item.Quantity--;
                if(item.Quantity == 0)
                {
                    var cartList =  RemoveItemFromCart(id);
                    if(cartList != null) return cartList;
                    throw new NullReferenceException("The cart now is empty add items to it");
                }    
                item.TotalPrice = item.Price * item.Quantity;
                var index = deserializeCartItems.IndexOf(item);
                deserializeCartItems[index] = item;
                var serializeCartItem = JsonSerializer.Serialize(deserializeCartItems);
                _httpContextAccessor.HttpContext.Session.SetString("Customer Product", serializeCartItem);
                return deserializeCartItems;
            }
            throw new NullReferenceException("The item is not found");
        }

        public bool DeleteCart()
        {
            _httpContextAccessor.HttpContext.Session.Remove("Customer Product");
            return true;
        }

        public IEnumerable<CartItem> GetCartItems()
        {
            try
            {
                var jsonCartItems = _httpContextAccessor.HttpContext.Session.GetString("Customer Product");
                if (!string.IsNullOrEmpty(jsonCartItems))
                {
                    var deserializedListOfItems = JsonSerializer.Deserialize<IEnumerable<CartItem>>(jsonCartItems);
                    return deserializedListOfItems ?? throw new NullReferenceException();
                }
                return Enumerable.Empty<CartItem>();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public IEnumerable<CartItem> IncreaseQuantityOfItem(int id)
        {
            try
            {
                var cartItems = _httpContextAccessor.HttpContext.Session.GetString("Customer Product");
                if (string.IsNullOrEmpty(cartItems))
                    throw new NullReferenceException("The cart is empty");
                var deserializeCartItems = JsonSerializer.Deserialize<List<CartItem>>(cartItems);
                var item = deserializeCartItems.FirstOrDefault(c => c.ProductId == id);
                if (item != null)
                {
                    item.Quantity++;
                    item.TotalPrice = item.Price * item.Quantity;
                    var index = deserializeCartItems.IndexOf(item);
                    deserializeCartItems[index] = item;
                    var serializeCartItem = JsonSerializer.Serialize(deserializeCartItems);
                    _httpContextAccessor.HttpContext.Session.SetString("Customer Product", serializeCartItem);
                    return deserializeCartItems;
                }
                throw new NullReferenceException("The item is not found");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public IEnumerable<CartItem> RemoveItemFromCart(int id)
        {
            try
            { 
            var cartItems = _httpContextAccessor.HttpContext.Session.GetString("Customer Product");
                if (string.IsNullOrEmpty(cartItems))
                    throw new NullReferenceException("The Cart is empty add items now");
            var deserializeCartItems = JsonSerializer.Deserialize<List<CartItem>>(cartItems);
            var check = deserializeCartItems.FirstOrDefault(c => c.ProductId == id);
            if (check != null)
            {
                deserializeCartItems.Remove(check);
                var serializeCartItem = JsonSerializer.Serialize(deserializeCartItems);
                _httpContextAccessor.HttpContext.Session.SetString("Customer Product", serializeCartItem);
                return deserializeCartItems;
            }
               throw new NullReferenceException("The item is not found");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
