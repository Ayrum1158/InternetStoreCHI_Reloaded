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
        private readonly IGenericRepository<CategoryEntity> _categoryRepository;
        private readonly IMapper _mapper;

        public CategoryService(
            IGenericRepository<CategoryEntity> categoryRepository,
            IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        private bool IsValid(Category category)// only name and description
        {
            bool validated = true;

            Regex reg;

            reg = new Regex("^[^ ][a-zA-Z ]{3,20}");
            validated &= reg.IsMatch(category.CategoryName);

            reg = new Regex("^[^ ][a-zA-Z0-9. -]{3,200}");
            validated &= reg.IsMatch(category.CategoryDescription);

            return validated;
        }

        private bool IsPresentInDb(string categoryName)
        {
            return _categoryRepository.FindFirstOrDefault(c => c.Name == categoryName) != null;
        }

        public ResultContract AddCategory(Category newCategory)
        {
            var result = new ResultContract();

            if (IsValid(newCategory))
            {
                if (!IsPresentInDb(newCategory.CategoryName))
                {
                    var category = _mapper.Map<CategoryEntity>(newCategory);
                    category.CreatedDate = category.UpdatedDate = DateTime.UtcNow;

                    _categoryRepository.Add(category);

                    bool success = _categoryRepository.Save() > 0;

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
            _categoryRepository.Remove(id);

            bool success = _categoryRepository.Save() > 0;

            var result = new ResultContract() { IsSuccessful = success };

            if (success)
                result.Message = "Category deleted successfully!";
            else
                result.Message = "No changes were made.";

            return result;
        }

        public ResultContract<List<Category>> GetCategories()
        {
            var categories = _categoryRepository.GetAll();

            bool success = categories != null;

            var result = new ResultContract<List<Category>>() { IsSuccessful = success };

            if (success == true)
            {
                var categoriesList = _mapper.Map<List<Category>>(categories);

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
            var category = _categoryRepository.FindFirstOrDefault(c => c.Id == id);

            bool success = true;

            if (category == null)
                success = false;

            var result = new ResultContract<Category>() { IsSuccessful = success };

            if (success)
            {
                result.Data = _mapper.Map<Category>(category);

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

            if (IsValid(updatedCategory))
            {
                var category = _categoryRepository.FindFirstOrDefault(c => c.Id == updatedCategory.CategoryId);

                category.Name = updatedCategory.CategoryName;
                category.Description = updatedCategory.CategoryDescription;
                category.UpdatedDate = DateTime.UtcNow;

                _categoryRepository.Update(category);

                var success = _categoryRepository.Save() > 0;

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
