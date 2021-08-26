﻿using BLL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface ICategoriesService
    {
        Task<ServiceResult<Category>> AddCategoryAsync(Category category);
        Task<ServiceResult> DeleteCategoryAsync(int id);
        Task<ServiceResult<Category>> GetCategoryAsync(int id);
        Task<ServiceResult<List<Category>>> GetCategoriesAsync();
        Task<ServiceResult<Category>> UpdateCategoryAsync(Category category);
    }
}
