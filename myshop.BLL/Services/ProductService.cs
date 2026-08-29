using AutoMapper;
using Azure.Core.Serialization;
using Microsoft.AspNetCore.Http;
using myshop.BLL.DTOs.Cart;
using myshop.BLL.DTOs.Product.Admin;
using myshop.BLL.DTOs.Product.Customer;
using myshop.BLL.IServices;
using myshop.BLL.PageList;
using myshop.DAL.Iconfiguration;
using myshop.Entities.Models;
using Newtonsoft.Json.Serialization;
using System;
using System.Globalization;
using System.Linq.Expressions;
using System.Text.Json;

namespace myshop.BLL.Services
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public ProductService(IUnitOfWork unitOfWork,IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }
        
        public async Task<bool> AddProductAsync(CreateProductDTO productDTO)
        {
            var mappedProduct = _mapper.Map<CreateProductDTO, Product>(productDTO);
            var check = await _unitOfWork.Products.Add(mappedProduct);
            if (check)
            {
                await _unitOfWork.CompleteTask();
                return true;
            }
            return false;
        }

        public async Task<IEnumerable<DisplayProductDTO>> GetAllProductsAsync()
        {
            var Products = await _unitOfWork.Products.GetAll();
            return _mapper.Map<IEnumerable<Product>, IEnumerable<DisplayProductDTO>>(Products);
        }
        public async Task<EditProductDTO> GetProductByIdAsync(int id)
        {
            var product = await _unitOfWork.Products.GetProductByIdAsync(id);
            var mappedProduct = _mapper.Map<Product, EditProductDTO>(product);
            return mappedProduct;
        }
        public async Task<bool> UpdateProductAsync(EditProductDTO editProductDTO)
        {
            var mappedProduct = _mapper.Map<EditProductDTO, Product>(editProductDTO);
            var check = await _unitOfWork.Products.Update(mappedProduct);
            if(check)
            {
                await _unitOfWork.CompleteTask();
                return true;
            }
            return false;
        }
        public async Task<bool> DeleteProductAsync(int id)
        {
            var check = await _unitOfWork.Products.Delete(id);
            if (check)
            {
                await _unitOfWork.CompleteTask();
                return true;
            }
            return false;
        }

        public async Task<PageList<DisplayAllProducts>> GetAllProductsForCustomersAsync(string searchWord, string sortColumn, String sortOrder, int page, int pagesize)
        {
            try
            {
                var listOfProductForCustomers = await _unitOfWork.Products.GetProductWithPagination( searchWord,  sortColumn,  sortOrder,  page,  pagesize);
                var mappedList = _mapper.Map<IEnumerable<DisplayAllProducts>>(listOfProductForCustomers);
                var pageInfo = new PageList<DisplayAllProducts>(mappedList.ToList(), page, pagesize, mappedList.Count());

                return pageInfo;
            }
            catch (Exception ex)
            {
                throw new NullReferenceException(ex.Message);
            }
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
                        var index = deserializedListOfProducts.IndexOf(exstPtoduct);
                        exstPtoduct.Quantity++;
                        deserializedListOfProducts[index] = exstPtoduct;
                    }
                    else
                    {
                        mappedProduct.Quantity = 1;
                        deserializedListOfProducts.Add(mappedProduct);
                    }
                    var serializeListOfProducts = JsonSerializer.Serialize(deserializedListOfProducts);
                    _httpContextAccessor.HttpContext.Session.SetString("Customer Product", serializeListOfProducts);

                }
                return true;
            }
            catch (Exception ex)
            {
                throw new NullReferenceException(ex.Message);
            }
        }

        public async Task<IEnumerable<DisplayProductDTO>> GetAllDeletedProducts()
        {
            var listOfDeletedProducts = await _unitOfWork.Products.GetDeletedProducts();
            var mappedList = _mapper.Map<IEnumerable<DisplayProductDTO>>(listOfDeletedProducts);
            return mappedList;
        }

        public async Task<bool> RestoreProductAsync(int productId)
        {
            try
            {
                var deletedProduct = await _unitOfWork.Products.GetDeletedProduct(productId);
                deletedProduct?.IsDeleted = false;
                var mappedProduct = _mapper.Map<Product>(deletedProduct);
                var check = await _unitOfWork.Products.Update(mappedProduct);
                if (check)
                    return true;
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
