using GymSharkApi.DTOs;
using GymSharkApi.Entities;
using GymSharkApi.Helpers;
using GymSharkApi.Interfaces;
using GymSharkAPI.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GymSharkApi.Data
{
    public class MessageRepository : IMessageRepository
    {
        private readonly DataContext _context;
        public MessageRepository(DataContext context)
        {
            _context = context;
        }
        public void AddOpinion(Messages message)
        {
            _context.Messages.Add(message);
        }

        public void DeleteOpinion(Messages message)
        {
            _context.Messages.Remove(message);
        }

        public async Task<Messages> GetOpinion(int id)
        {
            return await _context.Messages.FindAsync(id);
        }

        public Task<PagedList<MessageDto>> GetOpinionsForProduct()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<MessageDto>> GetOpinionThread(int currentUserId, int recipientId)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
