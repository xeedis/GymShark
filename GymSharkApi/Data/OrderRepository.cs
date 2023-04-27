using GymSharkApi.DTOs;
using GymSharkApi.Entities;
using GymSharkApi.Helpers;
using GymSharkApi.Interfaces;
using GymSharkAPI.Data;
using GymSharkAPI.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GymSharkApi.Data
{
    public class OrderRepository : IOrderRepository
    {
        private readonly DataContext _context;
        public OrderRepository(DataContext context)
        {
            _context = context;
        }
        public async Task<ProductOrder> GetProductOrder(int sourceUserId, int orderedProductId)
        {
            return await _context.Orders.FindAsync(sourceUserId, orderedProductId);
        }

        public async Task<PagedList<OrderDto>> GetUserOrders(OrderParams orderParams)
        {
            var products = _context.Products.OrderBy(p => p.ProductName).AsQueryable();
            var orders = _context.Orders.AsQueryable();

            if(orderParams.Predicate == "liked")
            {
                orders = orders.Where(order => order.SourceUsertId == orderParams.UserId);
                products = orders.Select(order => order.OrderedProduct);
            }

            var orderedItems =  products.Select(product => new OrderDto
            {
                ProductName = product.ProductName,
                Price = product.Price,
                PhotoUrl = product.Photos.FirstOrDefault(p=>p.isMain).Url,
                Id = product.Id
            });

            return await PagedList<OrderDto>.CreateAsync(orderedItems, orderParams.PageNumber, orderParams.PageSize); 
        }

        public async Task<AppUser> GetUserWithOrders(int userId)
        {
            return await _context.Users
                .Include(x => x.OrderedProducts)
                .FirstOrDefaultAsync(x => x.Id == userId);
        }
    }
}
