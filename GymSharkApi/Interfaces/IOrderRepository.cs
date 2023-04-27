using GymSharkApi.DTOs;
using GymSharkApi.Entities;
using GymSharkApi.Helpers;
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
        Task<PagedList<OrderDto>> GetUserOrders(OrderParams orderParams);
    }
}
