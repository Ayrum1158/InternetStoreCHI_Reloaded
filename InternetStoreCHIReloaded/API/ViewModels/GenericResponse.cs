using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.ViewModels
{
    public class GenericResponse
    {
        public bool IsSuccessful { get; set; }
        public string Message { get; set; }
    }

    public class GenericResponse<T> : GenericResponse
    {
        public T Data { get; set; }
    }
}
