using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Contracts
{
    public class ServiceResult
    {
        public bool IsSuccessful { get; set; }
        public string Message { get; set; }
    }

    public class ServiceResult<T> : ServiceResult
    {
        public T Data { get; set; }
    }
}
