using AutoMapper;
using GymSharkApi.DTOs;
using GymSharkApi.Entities;
using GymSharkApi.Extensions;
using GymSharkApi.Helpers;
using GymSharkApi.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace GymSharkApi.Controllers
{
    public class OpinionController : BaseApiController
    {
        private readonly IUserRepository _userRepository;
        private readonly IOpinionRepository _messageRepository;
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public OpinionController(IUserRepository userRepository, IProductRepository productRepository,
                IOpinionRepository messageRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _productRepository = productRepository;
            _messageRepository = messageRepository;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult<OpinionDto>> CreateOpinion(CreateMessageDto createMessageDto)
        {
            var username = User.FindFirst(ClaimTypes.Name)?.Value;

            var sender = await _userRepository.GetUserByUsername(username);
            var recipient = await _productRepository.GetProductByName(createMessageDto.RecipientName);

            if (recipient == null) return NotFound();

            var message = new Opinion
            {
                Sender = sender,
                Recipient = recipient,
                SenderUsername = sender.UserName,
                RecipientName = recipient.ProductName,
                Content = createMessageDto.Content
            };

            _messageRepository.AddOpinion(message);

            if (await _messageRepository.SaveAllAsync()) return Ok(_mapper.Map<OpinionDto>(message));

            return BadRequest("Could not send opinion");
        }

        [HttpGet("{productName}")]
        public async Task<ActionResult<IEnumerable<OpinionDto>>> GetOpinionsForProduct([FromQuery] OpinionParams opinionParams, string productName)
        {
            opinionParams.ProductName = productName;
            var opinions = await _messageRepository.GetOpinionsForProduct(opinionParams);

            Response.AddPaginationHeader(opinions.CurrentPage, opinions.PageSize, opinions.TotalCount, opinions.TotalPages);

            return opinions;
        }
    }
}
