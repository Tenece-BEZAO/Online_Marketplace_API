﻿using AutoMapper;
using Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Online_Marketplace.BLL.Interface;
using Online_Marketplace.DAL.Entities.Models;
using Online_Marketplace.DAL.Entities;
using Online_Marketplace.Logger.Logger;
using Online_Marketplace.Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Online_Marketplace.DAL.Migrations;

namespace Online_Marketplace.BLL.Implementation
{
    public class OrderService: IOrderService
    {

        private readonly IMapper _mapper;
        private readonly IRepository<Product> _productRepo;
        private readonly IRepository<Buyer> _buyerRepo;
        private readonly IRepository<Seller> _sellerRepo;
        private readonly IRepository<Order> _orderRepo;
        private readonly IRepository<OrderItem> _orderitemRepo;
        private readonly IRepository<ProductReviews> _productreivewRepo;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILoggerManager _logger;
        private readonly UserManager<User> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;



        public  OrderService (IHttpContextAccessor httpContextAccessor, ILoggerManager logger, IUnitOfWork unitOfWork, UserManager<User> userManager, IMapper mapper)
        {
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _mapper = mapper;
            _productRepo = _unitOfWork.GetRepository<Product>();
            _sellerRepo = _unitOfWork.GetRepository<Seller>();
            _buyerRepo = _unitOfWork.GetRepository<Buyer>();
            _orderRepo = _unitOfWork.GetRepository<Order>();
            _orderitemRepo = _unitOfWork.GetRepository<OrderItem>();
            _productreivewRepo = unitOfWork.GetRepository<ProductReviews>();
        }



        public async Task<List<OrderDto>> GetOrderHistoryAsync()
        {
            try
            {
                var userId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
                var buyer = await _buyerRepo.GetSingleByAsync(b => b.UserId == userId);

                var orders = await _orderRepo.GetAllAsync(o => o.BuyerId == buyer.Id,
                    include: o => o.Include(o => o.OrderItems).ThenInclude(oi => oi.Product));

                var orderDtos = _mapper.Map<List<OrderDto>>(orders);

                return orderDtos;
            }
            catch (Exception ex)
            {
                var sb = new StringBuilder();
                sb.AppendLine("An error occurred while getting order history:");
                sb.AppendLine(ex.Message);
                sb.AppendLine(ex.StackTrace);
                sb.AppendLine("Inner exception:");
                sb.AppendLine(ex.InnerException?.Message ?? "No inner exception");

                _logger.LogError(sb.ToString());

                throw;
            }
        }



    }




}