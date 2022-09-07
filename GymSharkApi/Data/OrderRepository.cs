using GymSharkApi.DTOs;
using GymSharkApi.Entities;
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

        public async Task<IEnumerable<OrderDto>> GetUserOrders(string predicate, int userId)
        {
            var products = _context.Products.OrderBy(p => p.ProductName).AsQueryable();
            var orders = _context.Orders.AsQueryable();

            if(predicate == "ordered")
            {
                orders = orders.Where(order => order.SourceUsertId == userId);
                products = orders.Select(order => order.OrderedProduct);
            }

            return await products.Select(product => new OrderDto
            {
                ProductName = product.ProductName,
                Price = product.Price,
                PhotoUrl = product.Photos.FirstOrDefault(p=>p.isMain).Url,
                Id = product.Id
            }).ToListAsync();
        }

        public async Task<AppUser> GetUserWithOrders(int userId)
        {
            return await _context.Users
                .Include(x => x.OrderedProducts)
                .FirstOrDefaultAsync(x => x.Id == userId);
        }
    }
}
