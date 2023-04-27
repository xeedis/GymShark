using GymSharkApi.DTOs;
using GymSharkApi.Entities;
using GymSharkApi.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GymSharkApi.Interfaces
{
    public interface IOpinionRepository
    {
        void AddOpinion(Opinion message);
        void DeleteOpinion(Opinion message);
        Task<Opinion> GetOpinion(int id);
        Task<PagedList<OpinionDto>> GetOpinionsForProduct(OpinionParams opinionParams);
        Task<IEnumerable<OpinionDto>> GetOpinionThread(int currentUserId, int recipientId);

        Task<bool> SaveAllAsync();
    }
}
