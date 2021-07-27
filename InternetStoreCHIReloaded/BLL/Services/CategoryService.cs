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
using System.Text.RegularExpressions;

namespace BLL.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IGenericRepository<CategoryEntity> categoryRepository;
        private readonly IMapper mapper;

        public CategoryService(
            IGenericRepository<CategoryEntity> categoryRepository,
            IMapper mapper)
        {
            this.categoryRepository = categoryRepository;
            this.mapper = mapper;
        }

        private bool Validate(Category category)// only name and description
        {
            bool validated = false;

            Regex reg;

            reg = new Regex("^[^ ][a-zA-Z ]{3,20}");
            validated &= reg.IsMatch(category.CategoryName);

            reg = new Regex("^[^ ][a-zA-Z0-9. -]{3,200}");
            validated &= reg.IsMatch(category.CategoryDescription);

            return validated;
        }

        private bool IsPresentInDB(string categoryName)
        {
            return categoryRepository.FindFirst(c => c.Name == categoryName) != null;
        }

        public ResultContract AddCategory(Category newCategory)
        {
            var result = new ResultContract();

            if (Validate(newCategory))
            {
                if (!IsPresentInDB(newCategory.CategoryName))
                {
                    var category = mapper.Map<CategoryEntity>(newCategory);
                    category.CreatedDate = category.UpdatedDate = DateTime.Now;

                    categoryRepository.Add(category);

                    bool success = categoryRepository.Save() > 0;

                    if (success)
                        result.Message = "Category added successfully!";
                    else
                        result.Message = "Unexpected error occured during adding new category.";

                    result.IsSuccessful = success;
                }
                else
                {
                    result.IsSuccessful = false;
                    result.Message = "Category name is already in database.";
                }
            }
            else
            {
                result.IsSuccessful = false;
                result.Message = "Inappropriate category name or description";
            }

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

        public ResultContract<List<Category>> GetCategories()
        {
            var categories = categoryRepository.GetAll();

            bool success = categories != null;

            var result = new ResultContract<List<Category>>() { IsSuccessful = success };

            if (success == true)
            {
                var categoriesList = mapper.Map<List<Category>>(categories);

                result.Data = categoriesList;

                result.Message = "Categories retrieval success!";
            }
            else
            {
                result.Message = "Unexpected error occured.";
            }

            return result;
        }

        public ResultContract<Category> GetCategory(int id)
        {
            var category = categoryRepository.FindFirst(c => c.Id == id);

            bool success = true;

            if (category == null)
                success = false;

            var result = new ResultContract<Category>() { IsSuccessful = success };

            if (success)
            {
                result.Data = mapper.Map<Category>(category);

                result.Message = "Category retrieval success!";
            }
            else// success == false
            {
                result.Message = "Category retrieval failed.";
            }

            return result;
        }

        public ResultContract UpdateCategory(Category updatedCategory)
        {
            var result = new ResultContract();

            if (Validate(updatedCategory))
            {
                var category = categoryRepository.FindFirst(c => c.Id == updatedCategory.CategoryId);

                category.Name = updatedCategory.CategoryName;
                category.Description = updatedCategory.CategoryDescription;
                category.UpdatedDate = DateTime.Now;

                categoryRepository.Update(category);

                var success = categoryRepository.Save() > 0;

                result.IsSuccessful = success;

                if (success)
                    result.Message = "Category updated successfully!";
                else// success == false
                    result.Message = "Unexpected error occured during updated category.";
            }
            else
            {
                result.IsSuccessful = false;
                result.Message = "Inappropriate category name or description";
            }

            return result;
        }
    }
}
