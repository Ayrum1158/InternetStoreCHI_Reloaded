using BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Models
{
    public class RemoveFromCartModel : ICartServiceValidatable
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
