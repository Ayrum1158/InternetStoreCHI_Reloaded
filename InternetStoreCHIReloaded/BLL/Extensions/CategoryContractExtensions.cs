using BLL.Contracts;
using Common.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Extensions
{
    public static class CategoryContractExtensions
    {
        public static bool HasContent(this Category category)
        {
            bool hasContent = true;

            if (category.CategoryName.IsNullEmptyOrWhiteSpace())
                hasContent = false;
            if (category.CategoryDescription.IsNullEmptyOrWhiteSpace())
                hasContent = false;

            return hasContent;
        }
    }
}
