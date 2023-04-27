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
    public class OpinionRepository : IOpinionRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public OpinionRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public void AddOpinion(Opinion message)
        {
            _context.Messages.Add(message);
        }

        public void DeleteOpinion(Opinion message)
        {
            _context.Messages.Remove(message);
        }

        public async Task<Opinion> GetOpinion(int id)
        {
            return await _context.Messages.FindAsync(id);
        }

        public async Task<PagedList<OpinionDto>> GetOpinionsForProduct(OpinionParams opinionParams)
        {
            var query = _context.Messages
                .Where(p => p.Recipient.ProductName == opinionParams.ProductName)
                .OrderByDescending(m => m.MessageSent)
                .AsQueryable();
            var messages = query.ProjectTo<OpinionDto>(_mapper.ConfigurationProvider);

            return await PagedList<OpinionDto>.CreateAsync(messages, opinionParams.PageNumber, opinionParams.PageSize);
        }

        public Task<IEnumerable<OpinionDto>> GetOpinionThread(int currentUserId, int recipientId)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
