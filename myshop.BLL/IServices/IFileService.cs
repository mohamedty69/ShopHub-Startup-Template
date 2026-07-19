using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace myshop.BLL.IServices
{
    public interface IFileService
    {
        public Task<string> SaveFileAsync(IFormFile file,string pathRoute);
        public bool DeleteFile(string pathRoute);
    }
}
