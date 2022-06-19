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
        public async Task<ActionResult<ItemUpdateDto>> UpdateProduct(ItemUpdateDto itemUpdateDto, string name)
        {
            var product = await _productRepository.GetProductByName(name);

            _mapper.Map(itemUpdateDto, product);

            _productRepository.Update(product);

            if (await _productRepository.SaveAllAsync()) return NoContent();
            return BadRequest("Failed to update item!");
        }

        [HttpPut("productName/set-main-photo/{photoId}")]
        public async Task<ActionResult> SetMainPhoto(int photoId,string productName)
        {
            var product = await _productRepository.GetProductByName(productName);
            var photo = product.Photos.FirstOrDefault(x => x.Id == photoId);

            if (photo.isMain) return BadRequest("This is already your main photo!");

            var currentMain = product.Photos.FirstOrDefault(x => x.isMain);
            if (currentMain != null) currentMain.isMain = false;
            photo.isMain = true;

            if(await _productRepository.SaveAllAsync()) return NoContent();

            return BadRequest("Failed to set as main photo");
        }

        [HttpDelete("productName/delete-photo/{photoId}")]
        public async Task<ActionResult> DeletePhoto(int photoId,string productName)
        {
            var product = await _productRepository.GetProductByName(productName);
            var photo = product.Photos.FirstOrDefault(x=>x.Id== photoId);

            if (photo == null) return NotFound();
            if (photo.isMain) return BadRequest("cannot delete main photo");
            if(photo.PublicId != null)
            {
                /*var result = await _photoService.DeletPhotoAsync(photo.PublicId)
                if(result.Error!=null) return BadRequest(result.Error.Message); */
            }

            product.Photos.Remove(photo);

            if (await _productRepository.SaveAllAsync()) return Ok();

            return BadRequest("Couldn`t delete photo");
        }
    }
}
