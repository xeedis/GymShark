using AutoMapper;
using AutoMapper.QueryableExtensions;
using GymSharkApi.DTOs;
using GymSharkApi.Entities;
using GymSharkApi.Helpers;
using GymSharkApi.Interfaces;
using GymSharkAPI.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GymSharkApi.Data
{
    public class ProductRepository : IProductRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public ProductRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<IEnumerable<Product>> GetProductsAsync()
        {
            return await _context.Products
                .Include(p=>p.Photos)
                .ToListAsync();
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            return await _context.Products.FindAsync(id);
        }

        public async Task<Product> GetProductByName(string name)
        {
            return await _context.Products
                .Include(p=>p.Photos)
                .SingleOrDefaultAsync(x => x.ProductName == name);
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public void Update(Product product)
        {
            _context.Entry(product).State = EntityState.Modified;
        }

        public async Task<ItemDto> GetItemAsync(string name)
        {
            return await _context.Products
                .Where(x => x.ProductName == name)
                .ProjectTo<ItemDto>(_mapper.ConfigurationProvider)
                .SingleOrDefaultAsync();
        }

        public async Task<PagedList<ItemDto>> GetItemsAsync(ProductsParams productParams)
        {
            var query = _context.Products.AsQueryable();
            if(productParams.Category != "all")
            {
                query = query.Where(p => p.Category == productParams.Category);
            }
            query = query.Where(p => p.Price >= productParams.minPrice && p.Price <= productParams.maxPrice);

            return await PagedList<ItemDto>.CreateAsync(query.ProjectTo<ItemDto>(_mapper.ConfigurationProvider)
                .AsNoTracking(), productParams.PageNumber, productParams.PageSize);    
        }
    }
}
