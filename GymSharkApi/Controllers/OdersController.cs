using GymSharkApi.DTOs;
using GymSharkApi.Entities;
using GymSharkApi.Extensions;
using GymSharkApi.Helpers;
using GymSharkApi.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace GymSharkApi.Controllers
{   
    [Authorize]
    public class OrdersController:BaseApiController
    {
        private readonly IProductRepository _productRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IUserRepository _userRepository;

        public OrdersController(IProductRepository productRepository, IOrderRepository orderRepository, IUserRepository userRepository)
        {
            _orderRepository = orderRepository;
            _productRepository = productRepository;
            _userRepository = userRepository;
        }

        [HttpPost("{productname}")]
        public async Task<ActionResult> AddOrder(string productname)
        {
            var sourceUserName = User.FindFirst(ClaimTypes.Name).Value;
            var user = await _userRepository.GetUserByUsername(sourceUserName);
            var sourceUserId = user.Id;
            var orderedProduct = await _productRepository.GetProductByName(productname);
            var sourceUser = await _orderRepository.GetUserWithOrders(sourceUserId);//source product?

            if (orderedProduct == null) return NotFound();

            var userOrder = await _orderRepository.GetProductOrder(sourceUserId, orderedProduct.Id);

            if (userOrder != null) return BadRequest("You already ordered this product!");

            userOrder = new ProductOrder
            {
                SourceUsertId = sourceUserId,
                OrderedProductId = orderedProduct.Id
            };

            sourceUser.OrderedProducts.Add(userOrder);

            if (await _productRepository.SaveAllAsync()) return Ok();

            return BadRequest("Failed to order product");
        }

        [HttpGet]
        public async Task<ActionResult<PagedList<OrderDto>>> GetUserOrders([FromQuery]OrderParams orderParams)
        {
            orderParams.UserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            var products = await _orderRepository.GetUserOrders(orderParams);

            Response.AddPaginationHeader(products.CurrentPage, products.PageSize,
                    products.TotalCount, products.TotalPages);
            return Ok(products);
        }
    }
}
