using AutoMapper;
using GymSharkApi.DTOs;
using GymSharkApi.Entities;
using GymSharkApi.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace GymSharkApi.Controllers
{
    public class MessageController: BaseApiController
    {
        private readonly IUserRepository _userRepository;
        private readonly IMessageRepository _messageRepository;
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public MessageController(IUserRepository userRepository,IProductRepository productRepository, 
                IMessageRepository messageRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _productRepository = productRepository;
            _messageRepository = messageRepository;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult<MessageDto>> CreateOpinion(CreateMessageDto createMessageDto)
        {
            var username = User.FindFirst(ClaimTypes.Name)?.Value;

            var sender = await _userRepository.GetUserByUsername(username);
            var recipient = await _productRepository.GetProductByName(createMessageDto.RecipientName);

            if (recipient == null) return NotFound();

            var message = new Messages
            {
                Sender = sender,
                Recipient = recipient,
                SenderUsername = sender.UserName,
                RecipientName = recipient.ProductName,
                Content = createMessageDto.Content
            };

            _messageRepository.AddOpinion(message);

            if (await _messageRepository.SaveAllAsync()) return Ok(_mapper.Map<MessageDto>(message));

            return BadRequest("Could not send opinion");
        }
    }
}
