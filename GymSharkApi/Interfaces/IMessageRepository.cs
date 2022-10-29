using GymSharkApi.DTOs;
using GymSharkApi.Entities;
using GymSharkApi.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GymSharkApi.Interfaces
{
    public interface IMessageRepository
    {
        void AddOpinion(Messages message);
        void DeleteOpinion(Messages message);
        Task<Messages> GetOpinion(int id);
        Task<PagedList<MessageDto>> GetOpinionsForProduct(OpinionParams opinionParams);
        Task<IEnumerable<MessageDto>> GetOpinionThread(int currentUserId, int recipientId);

        Task<bool> SaveAllAsync();
    }
}
