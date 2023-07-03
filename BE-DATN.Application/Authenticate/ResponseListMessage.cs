using System;
using System.Collections.Generic;
using System.Text;

namespace BE_DATN.Application.Authenticate
{
    public class ResponseListMessage
    {
        public int page { get; set; }
        public int pageSize { get; set; }
        public long totalItem { get; set; }
        public dynamic data { get; set; }
    }
}
