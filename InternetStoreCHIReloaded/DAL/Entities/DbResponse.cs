using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Entities
{
    public class DbResponse<T>
    {
        public bool IsSuccessful { get; set; }
        public T Data { get; set; }
    }
}
