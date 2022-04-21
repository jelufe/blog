using Blog.Domain.DAOs;
using Blog.Domain.Entities;
using Blog.Domain.Interfaces.Repositories;
using Blog.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Domain.Services
{
    public class ImageService : IImageService
    {
        public static IWebHostEnvironment _environment;
        private readonly IImageRepository _imageRepository;

        public ImageService(
            IWebHostEnvironment environment,
            IImageRepository imageRepository)
        {
            _environment = environment;
            _imageRepository = imageRepository;
        }

        public async Task<IEnumerable<ImageDao>> GetImages(int userId = 0)
        {
            var images = await _imageRepository.GetImages(userId);
            return images.Select(i => new ImageDao(i)).ToList();
        }

        public async Task<byte[]> GetImage(int id)
        {
            var image = await _imageRepository.GetImage(id);

            if (image is null)
                throw new Exception("Image does not exist");

            byte[] fileBytes = await File.ReadAllBytesAsync($"{image.Path}{image.Name}");

            return fileBytes;
        }

        public async Task InsertImage(IFormFile file, int currentUserId)
        {
            if (file is not null && file.Length > 0)
            {
                string path = $"{_environment.ContentRootPath}\\images\\";

                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                Image imageFound;
                string extension = Path.GetExtension(file.FileName);
                string newFileName = file.FileName.Remove(file.FileName.Length - extension.Length);
                int newFileNameNumber = 0;
                string newFileNameConcatenated;

                do
                {
                    if (newFileNameNumber > 0)
                        imageFound = await _imageRepository.GetImage($"{newFileName}{newFileNameNumber}{extension}");
                    else
                        imageFound = await _imageRepository.GetImage($"{newFileName}{extension}");

                    if (imageFound is not null)
                        newFileNameNumber++;
                } while (imageFound is not null);

                if (newFileNameNumber > 0)
                    newFileNameConcatenated = $"{newFileName}{newFileNameNumber}{extension}";
                else
                    newFileNameConcatenated = $"{newFileName}{extension}";

                using (FileStream filestream = File.Create($"{path}\\{newFileNameConcatenated}"))
                {
                    await file.CopyToAsync(filestream);
                    filestream.Flush();
                }

                var image = new Image
                {
                    Name = newFileNameConcatenated,
                    Path = path,
                    UserId = currentUserId
                };

                await _imageRepository.InsertImage(image);
            }
            else
            {
                throw new Exception("Ocorreu uma falha no envio do arquivo...");
            }
        }

        public async Task<bool> DeleteImage(int id, bool isAdmin, int currentUserId)
        {
            var imageFound = await _imageRepository.GetImage(id);

            if (imageFound == null)
                throw new Exception("Image does not exist");

            if (!isAdmin && imageFound.User.UserId != currentUserId)
                throw new Exception("User does not have permission to perform this action");

            if (File.Exists($"{imageFound.Path}{imageFound.Name}"))
            {
                File.Delete($"{imageFound.Path}{imageFound.Name}");
            }

            return await _imageRepository.DeleteImage(id);
        }
    }
}
