using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Interfaces
{
    public interface ICartServiceValidatable
    {
        int ProductId { get; set; }
        int Quantity { get; set; }
    }
}
