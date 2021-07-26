using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Contracts
{
    public class ResultContract
    {
        public bool IsSuccessful { get; set; }
        public string Message { get; set; }
    }

    public class ResultContract<T> : ResultContract
    {
        public T Data { get; set; }
    }
}
