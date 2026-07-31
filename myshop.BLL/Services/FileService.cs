using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using myshop.BLL.IServices;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myshop.BLL.Services
{
    public class FileService : IFileService
    {
        public bool DeleteFile(string pathRoute)
        {
            if (string.IsNullOrWhiteSpace(pathRoute)) return false;
            try
            {
                if (pathRoute.IndexOfAny(Path.GetInvalidPathChars()) >= 0) return false;
                if (System.IO.File.Exists(pathRoute))
                {
                    System.IO.File.Delete(pathRoute);
                    return true;
                }
            }
            catch
            {
                return false;
            }
            return false;
        }

        private string GetSafeExtension(IFormFile file)
        {
            if (file == null) return "";
            try
            {
                string rawName = file.FileName ?? "";
                if (rawName.IndexOfAny(Path.GetInvalidFileNameChars()) < 0)
                {
                    var ext = Path.GetExtension(rawName);
                    if (!string.IsNullOrEmpty(ext)) return ext.ToLower();
                }
            }
            catch { }

            return file.ContentType?.ToLower() switch
            {
                "image/jpeg" => ".jpg",
                "image/jpg" => ".jpg",
                "image/png" => ".png",
                "image/webp" => ".webp",
                _ => ""
            };
        }

        public async Task<string> SaveFileAsync(IFormFile file, string routePath)
        {
            List<string> exts = new List<string>()
            {
                ".jpg", ".jpeg", ".png", ".webp"
            };

            if (file != null)
            {
                var ext = GetSafeExtension(file);
                if (string.IsNullOrEmpty(ext) || !exts.Contains(ext))
                    throw new InvalidOperationException($"You can not upload a file that extension of it is {ext}");

                if (file.Length > 2 * 1024 * 1024)
                    throw new InvalidOperationException($"The max size of the file that allowed is 2 MB your file size is {file.Length}.");

                try
                {
                    var fileName = Guid.NewGuid().ToString();

                    if (string.IsNullOrWhiteSpace(routePath) || routePath.IndexOfAny(Path.GetInvalidPathChars()) >= 0)
                    {
                        routePath = Directory.GetCurrentDirectory();
                    }

                    var upload = Path.Combine(routePath, @"Images\Products");
                    if (!Directory.Exists(upload))
                    {
                        Directory.CreateDirectory(upload);
                    }

                    var fullPath = Path.Combine(upload, fileName + ext);
                    using (var fileStream = new FileStream(fullPath, FileMode.Create))
                    {
                        await file.CopyToAsync(fileStream);
                    }

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
