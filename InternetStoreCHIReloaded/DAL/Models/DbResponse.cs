using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Models
{
    public class DbResponse
    {
        public bool IsSuccessful { get; set; }
        public string Message { get; set; }
    }

    public class DbResponse<T> : DbResponse
    {
        public T Data { get; set; }
    }
}
