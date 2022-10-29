using AutoMapper;
using AutoMapper.QueryableExtensions;
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
        private readonly IMapper _mapper;
        public MessageRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
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

        public async Task<PagedList<MessageDto>> GetOpinionsForProduct(OpinionParams opinionParams)
        {
            var query = _context.Messages
                .OrderByDescending(m => m.MessageSent)
                .AsQueryable();
            var messages = query.ProjectTo<MessageDto>(_mapper.ConfigurationProvider);

            return await PagedList<MessageDto>.CreateAsync(messages, opinionParams.PageNumber, opinionParams.PageSize);
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
