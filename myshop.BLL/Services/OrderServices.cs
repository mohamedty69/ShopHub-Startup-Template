using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Formatters.Internal;
using myshop.BLL.DTOs.Order;
using myshop.BLL.IServices;
using myshop.DAL.Iconfiguration;
using myshop.Entities.Models;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;

namespace myshop.BLL.Services
{
    public class OrderServices : IOrderServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ICartService _cartService;

        public OrderServices(IUnitOfWork unitOfWork, IMapper mapper, ICartService cartService
         )
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _cartService = cartService;
        }

        public async Task<IEnumerable<SummaryOrderDTO>> GetAllUsersOrder(string userId)
        {
            var usersOrderList = await _unitOfWork.orderHeaderRepo.GetAllOrdersByUserIdAsync( userId );
            var mappedOrders = _mapper.Map<IEnumerable<SummaryOrderDTO>>( usersOrderList );
            return mappedOrders;
        }

        public async Task<SummaryOrderDTO> GetUserOrdersAsync(int orderId)
        {
            var userOrder =await _unitOfWork.orderHeaderRepo.GetOrderByUserIdAsync(orderId);
            var mappedOrder = _mapper.Map<SummaryOrderDTO>(userOrder);
            return mappedOrder;
        }
        public async Task<int> PlaceOrderAsync(string userId, CheckOutOrderDTO checkoutInfo)
        {
            var mappedOrder = _mapper.Map<OrderHeader>(checkoutInfo);
            var cartItems = _cartService.GetCartItems();
            if (cartItems == null)
                throw new NullReferenceException("The cart item is empty");
            mappedOrder.ApplicationUserId = userId;
            mappedOrder.TotalPrice = cartItems.Sum(t => t.TotalPrice);
            mappedOrder.OrderDate = DateTime.UtcNow;
            var check = await _unitOfWork.orderHeaderRepo.Add(mappedOrder);
            if (check)
            {
                await _unitOfWork.CompleteTask();
                bool orderDetailCheck = false;
                foreach (var cartItem in cartItems)
                {
                    OrderDetail orderDetail = new OrderDetail();
                    orderDetail.OrderHeaderId = mappedOrder.Id;
                    orderDetail.ProductId = cartItem.ProductId;
                    orderDetail.Count = cartItem.Quantity;
                    orderDetail.Price = cartItem.Price;
                    orderDetailCheck = await _unitOfWork.orderDetailRepo.Add(orderDetail);
                    if (!orderDetailCheck) {
                        break;
                    }
                }
                if(!orderDetailCheck)
                    return 0;
                await _unitOfWork.CompleteTask();
                _cartService.DeleteCart();
                return mappedOrder.Id;
            }
            return 0;
        }
    }
}
