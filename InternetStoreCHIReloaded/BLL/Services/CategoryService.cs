using AutoMapper;
using BLL.Contracts;
using BLL.Extensions;
using BLL.Interfaces;
using DAL.Entities;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IGenericRepository<Category> categoryRepository;
        private readonly IMapper mapper;

        public CategoryService(
            IGenericRepository<Category> categoryRepository,
            IMapper mapper)
        {
            this.categoryRepository = categoryRepository;
            this.mapper = mapper;
        }

        public ResultContract AddCategory(CategoryContract newCategory)
        {
            if (newCategory.HasContent())
            {
                var category = mapper.Map<Category>(newCategory);
                category.CreatedDate = category.UpdatedDate = DateTime.Now;

                categoryRepository.Add(category);
            }

            var success = categoryRepository.Save() > 0;

            var result = new ResultContract() { IsSuccessful = success };

            if (success == true)
                result.Message = "Category added successfully!";
            else// success == false
                result.Message = "Unexpected error occured during adding new category.";

            return result;
        }

        public ResultContract DeleteCategory(int id)
        {
            categoryRepository.Remove(id);

            bool success = categoryRepository.Save() > 0;

            var result = new ResultContract() { IsSuccessful = success };

            if (success)
                result.Message = "Category deleted successfully!";
            else
                result.Message = "No changes were made.";

            return result;
        }

        public ResultContract<List<CategoryContract>> GetCategories()
        {
            var categories = categoryRepository.GetAll();

            bool success = categories != null;

            var result = new ResultContract<List<CategoryContract>>() { IsSuccessful = success };

            if (success == true)
            {
                var categoriesList = mapper.Map<List<CategoryContract>>(categories);

                result.Data = categoriesList;

                result.Message = "Categories retrieval success!";
            }
            else
            {
                result.Message = "Unexpected error occured.";
            }

            return result;
        }

        public ResultContract<CategoryContract> GetCategory(int id)
        {
            var category = categoryRepository.FindFirst(c => c.Id == id);

            bool success = true;

            if (category == null)
                success = false;

            var result = new ResultContract<CategoryContract>() { IsSuccessful = success };

            if (success)
            {
                result.Data = mapper.Map<CategoryContract>(category);

                result.Message = "Category retrieval success!";
            }
            else// success == false
            {
                result.Message = "Category retrieval failed.";
            }

            return result;
        }

        public ResultContract UpdateCategory(CategoryContract updatedCategory)
        {
            if (updatedCategory.HasContent())
            {
                var category = categoryRepository.FindFirst(c => c.Id == updatedCategory.CategoryId);

                category.Name = updatedCategory.CategoryName;
                category.Description = updatedCategory.CategoryDescription;
                category.UpdatedDate = DateTime.Now;

                categoryRepository.Update(category);
            }

            var success = categoryRepository.Save() > 0;

            var result = new ResultContract() { IsSuccessful = success };

            if (success)
                result.Message = "Category updated successfully!";
            else// success == false
                result.Message = "Unexpected error occured during updated category.";

            return result;
        }
    }
}
