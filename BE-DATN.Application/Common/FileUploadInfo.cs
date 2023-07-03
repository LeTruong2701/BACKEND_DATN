using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace BE_DATN.Application.Common
{
    public class FileUploadInfo
    {
        public IFormFile file { get; set; }
        public string folder { get; set; }
        public string Description { get; set; }
    }
}
