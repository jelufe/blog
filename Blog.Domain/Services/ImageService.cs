using Blog.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Blog.Domain.Services
{
    public class ImageService : IImageService
    {
        public static IWebHostEnvironment _environment;

        public ImageService(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        public async Task InsertImage(IFormFile file)
        {
            if (file is not null && file.Length > 0)
            {
                if (!Directory.Exists(_environment.ContentRootPath + "\\images\\"))
                {
                    Directory.CreateDirectory(_environment.ContentRootPath + "\\images\\");
                }

                using (FileStream filestream = System.IO.File.Create(_environment.ContentRootPath + "\\images\\" + file.FileName))
                {
                    await file.CopyToAsync(filestream);
                    filestream.Flush();
                }
            }
            else
            {
                throw new Exception("Ocorreu uma falha no envio do arquivo...");
            }
        }
    }
}
