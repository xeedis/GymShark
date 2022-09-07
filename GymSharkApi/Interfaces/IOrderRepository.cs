using GymSharkApi.DTOs;
using GymSharkApi.Entities;
using GymSharkAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GymSharkApi.Interfaces
{
    public interface IOrderRepository
    {
        Task<ProductOrder> GetProductOrder(int sourceUserId, int orderedProductId);
        Task<AppUser> GetUserWithOrders(int productId);
        Task<IEnumerable<OrderDto>> GetUserOrders(string predicate, int productId);
    }
}
