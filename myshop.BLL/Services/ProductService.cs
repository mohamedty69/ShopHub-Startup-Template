using AutoMapper;
using myshop.BLL.DTOs.Product;
using myshop.BLL.IServices;
using myshop.DAL.Iconfiguration;
using myshop.DAL.IRepository;
using myshop.Entities.Models;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace myshop.BLL.Services
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public ProductService(IUnitOfWork unitOfWork,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
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
    }
}
