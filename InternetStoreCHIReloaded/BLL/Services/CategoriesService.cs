using AutoMapper;
using BLL.Contracts;
using BLL.Interfaces;
using DAL.Entities;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class CategoriesService : ICategoriesService
    {
        private readonly IGenericRepository<CategoryEntity> _categoryRepository;
        private readonly IMapper _mapper;

        public CategoriesService(
            IGenericRepository<CategoryEntity> categoryRepository,
            IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        private bool IsValid(Category category)// only name and description
        {
            bool valid = true;

            Regex reg;

            reg = new Regex("^[^ ][a-zA-Z ]{3,20}");
            valid = valid && reg.IsMatch(category.CategoryName);

            reg = new Regex("^[^ ][a-zA-Z0-9. -]{3,200}");
            valid = valid && reg.IsMatch(category.CategoryDescription);

            return valid;
        }

        private bool IsPresentInDb(string categoryName)
        {
            return _categoryRepository.FindFirstOrDefault(c => c.Name == categoryName) != null;
        }

        public async Task<ResultContract<Category>> AddCategoryAsync(Category newCategory)
        {
            var result = new ResultContract<Category>();

            if (IsValid(newCategory))
            {
                if (!IsPresentInDb(newCategory.CategoryName))
                {
                    var categoryEntity = _mapper.Map<CategoryEntity>(newCategory);
                    categoryEntity.CreatedDate = categoryEntity.UpdatedDate = DateTime.UtcNow;

                    bool success = _categoryRepository.Add(categoryEntity);

                    if (success)
                    {
                        result.Message = "Category added successfully!";
                        result.Data = _mapper.Map<Category>(categoryEntity);
                    }
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

        public async Task<ResultContract> DeleteCategoryAsync(int id)
        {
            var result = new ResultContract();
            bool success;

            success = _categoryRepository.Remove(id);

            result.IsSuccessful = success;

            if (success)
                result.Message = "Category deleted successfully!";
            else
                result.Message = "No changes were made.";

            return result;
        }

        public async Task<ResultContract<List<Category>>> GetCategoriesAsync()
        {
            var categories = _categoryRepository.GetAll();

            bool success = categories != null;// no Count() > 0 because return of 0 categories is ok for logic

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

        public async Task<ResultContract<Category>> GetCategoryAsync(int id)
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

        public async Task<ResultContract<Category>> UpdateCategoryAsync(Category updatedCategory)
        {
            var result = new ResultContract<Category>();

            if (IsValid(updatedCategory))
            {
                var category = _categoryRepository.FindFirstOrDefault(c => c.Id == updatedCategory.CategoryId);

                //don't use automapper here because we don't want to accidentally change CreatedDate and UpdateDate
                category.Name = updatedCategory.CategoryName;
                category.Description = updatedCategory.CategoryDescription;
                category.UpdatedDate = DateTime.UtcNow;

                var success = _categoryRepository.Update(category);

                result.IsSuccessful = success;

                if (success)
                {
                    result.Message = "Category updated successfully!";
                    result.Data = _mapper.Map<Category>(category);
                }
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
