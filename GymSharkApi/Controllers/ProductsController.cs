using AutoMapper;
using GymSharkApi.DTOs;
using GymSharkApi.Entities;
using GymSharkApi.Interfaces;
using GymSharkApi.Services;
using GymSharkAPI.Data;
using Microsoft.AspNetCore.Http;
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
        private readonly IPhotoService _photoService;
        public ProductsController(DataContext context, IProductRepository productRepository, IMapper mapper, IPhotoService photoService)
        {
            _context = context;
            _productRepository = productRepository;
            _mapper = mapper;
            _photoService = photoService;
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

        [HttpPost("{productName}/add-photo", Name ="GetProduct")]
        public async Task<ActionResult<PhotoDto>> AddPhoto(IFormFile file, string productName)
        {
            var product = await _productRepository.GetProductByName(productName);

            var result = await _photoService.AddPhotoAsync(file);

            if (result.Error != null) return BadRequest(result.Error.Message);

            var photo = new Photo
            {
                Url = result.SecureUrl.AbsoluteUri,
                PublicId = result.PublicId
            };

            if(product.Photos.Count == 0)
            {
                photo.isMain = true;
            }

            product.Photos.Add(photo);

            if(await _productRepository.SaveAllAsync())
            {
                return CreatedAtRoute("GetProduct",new { productName = product.ProductName},_mapper.Map<PhotoDto>(photo));
            }
            return BadRequest("Problem with adding photo!");
        }
    }
}
