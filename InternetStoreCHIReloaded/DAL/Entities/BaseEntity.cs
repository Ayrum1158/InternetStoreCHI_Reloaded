using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Entities
{
    public class BaseEntity
    {
        public int Id { get; set; }

        public BaseEntity()// empty ctor is needed to make generic repository delete method work
        {  }
    }
}
