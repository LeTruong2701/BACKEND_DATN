using System;
using System.Collections.Generic;
using System.Text;

namespace BE_DATN.Application.Common
{
    public class Result<T>
    {
        public List<T> data { get; set; }
        
    }
}
