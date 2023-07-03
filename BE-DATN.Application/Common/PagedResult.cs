using System;
using System.Collections.Generic;
using System.Text;

namespace BE_DATN.Application.Common
{
    public class PagedResult<T>
    {
        public List<T> data { get; set; }
        public int totalItem { get; set; }
        public int page { get; set; }
        public int pageSize { get; set; }
       
    }
}
