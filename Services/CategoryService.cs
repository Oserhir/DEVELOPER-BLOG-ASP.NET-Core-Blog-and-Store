﻿using Microsoft.EntityFrameworkCore;
using TheBlogProject.Data;
using TheBlogProject.Models;
using TheBlogProject.Services.Interfaces;

namespace TheBlogProject.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ApplicationDbContext _context;

        public CategoryService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddNewCategoryAsync(Category category)
        {
            _context.Add(category);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Category>> GetAllCategoriesAsync()
        {
            return await _context.Categories.ToListAsync();
        }

        public async Task<IQueryable<Category>> GetCategoriesAsync()
        {
            return  _context.Categories;
        }

        public async Task<Category> GetCategoryByIdAsync(int CategoryId)
        {
            Category category = await _context.Categories
                .FirstOrDefaultAsync(p => p.Id == CategoryId);

            return category;

        }

        public async Task<List<Post>> GetPostsByCategory(int CategoryId)
        {
            var posts = await _context.Posts
                 .Include(p => p.Category)
                 .Where( p => p.CategoryId == CategoryId ).ToListAsync();
            return posts;
        }
    }
}
