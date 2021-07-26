using BLL.Contracts;
using Common.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Extensions
{
    public static class CategoryContractExtensions
    {
        public static bool IsFormatted(this CategoryContract category)
        {
            bool isFormatted = true;

            if (category.CategoryName.IsNullEmptyOrWhiteSpace())
                isFormatted = false;
            if (category.CategoryDescription.IsNullEmptyOrWhiteSpace())
                isFormatted = false;

            return isFormatted;
        }
    }
}
