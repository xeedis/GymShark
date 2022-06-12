using AutoMapper;
using GymSharkApi.DTOs;
using GymSharkApi.Entities;
using GymSharkApi.Interfaces;
using GymSharkAPI.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GymSharkApi.Controllers
{
    public class ProductsController: BaseApiController
    {
        private readonly DataContext _context;
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        public ProductsController(DataContext context, IProductRepository productRepository, IMapper mapper)
        {
            _context = context;
            _productRepository = productRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ItemDto>>> GetProducts()
        {
            var products = await _productRepository.GetItemsAsync();
            return Ok(products);
        }

        [HttpGet("{name}")]
        public async Task<ActionResult<ItemDto>> GetProduct(string name)
        {
            return await _productRepository.GetItemAsync(name);
        }

        [HttpPut("{name}")]
        public async Task<ActionResult<ItemUpdateDto>> UpdateUser(ItemUpdateDto itemUpdateDto, string name)
        {
            var product = await _productRepository.GetProductByName(name);

            _mapper.Map(itemUpdateDto, product);

            _productRepository.Update(product);

            if (await _productRepository.SaveAllAsync()) return NoContent();
            return BadRequest("Failed to update item!");
        }
    }
}
