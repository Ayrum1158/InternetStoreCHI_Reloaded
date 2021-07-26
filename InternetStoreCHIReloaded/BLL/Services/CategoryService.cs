using BLL.Contracts;
using BLL.Extensions;
using BLL.Interfaces;
using DAL.Entities;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IGenericRepository<Category> categoryRepository;

        public CategoryService(IGenericRepository<Category> categoryRepository)
        {
            this.categoryRepository = categoryRepository;
        }

        public ResultContract AddCategory(CategoryContract newCategory)
        {
            if(newCategory.IsFormatted())
            {
                var timeNow = DateTime.Now;

                categoryRepository.Add(new Category
                {
                    Name = newCategory.CategoryName,
                    Description = newCategory.CategoryDescription,
                    CreatedDate = timeNow,
                    UpdatedDate = timeNow
                });
            }

            var success = categoryRepository.Save() > 0;

            var result = new ResultContract() { IsSuccessful = success };

            if (success == true)
                result.Message = "Category added successfully!";
            else// success == false
                result.Message = "Unexpected error occured during adding new category.";

            return result;
        }

        public ResultContract UpdateCategory(CategoryContract updatedCategory)
        {
            if(updatedCategory.IsFormatted())
            {
                var category = categoryRepository.FindFirst(c => c.Id == updatedCategory.Id);

                category.Name = updatedCategory.CategoryName;
                category.Description = updatedCategory.CategoryDescription;
                category.UpdatedDate = DateTime.Now;

                categoryRepository.Update(category);
            }

            var success = categoryRepository.Save() > 0;

            var result = new ResultContract() { IsSuccessful = success };

            if (success == true)
                result.Message = "Category updated successfully!";
            else// success == false
                result.Message = "Unexpected error occured during updated category.";

            return result;
        }
    }
}
