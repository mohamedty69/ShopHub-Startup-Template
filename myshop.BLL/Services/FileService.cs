using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using myshop.BLL.IServices;
using System;
using System.Collections.Generic;
using System.Text;

namespace myshop.BLL.Services
{
    public class FileService : IFileService
    {
        public bool DeleteFile(string pathRoute)
        {
            
            if (System.IO.File.Exists(pathRoute))
            {
                File.Delete(pathRoute);
                return true;
            }
            return false;
        }

        public async Task<string> SaveFileAsync(IFormFile file,string routePath)
        {
            List<string> exts = new List<string>()
            {
                ".jpg",".jpeg",".png",".webp"
            };
            if (file != null)
            {
                var ext = Path.GetExtension(file.FileName);
                if (!exts.Contains(ext))
                    throw new InvalidOperationException($"You can not upload a file that extension of it is{ext}");
                if (file.Length > 2 * 1024 * 1024)
                    throw new InvalidOperationException($"The max size of the file that allowed is 2 MB your file size is {file.Length}.");
                try
                {
                    var fileName = Guid.NewGuid().ToString();
                    var upload = Path.Combine(routePath, @"Images\Products");
                    using (var fileStream = new FileStream(Path.Combine(upload, fileName + ext), FileMode.Create))
                    {
                        await file.CopyToAsync(fileStream);
                    }
                    ;
                    return @"Images\Products\" + fileName + ext;
                }
                catch (Exception ex)
                {
                    throw new InvalidOperationException(ex.Message);
                }
            }
            throw new NullReferenceException("The file is not exist");
        }
        
    }
}
