using Blog.Api.Responses;
using Blog.Domain.DAOs;
using Blog.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Blog.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : CustomControllerBase
    {
        private readonly IImageService _imageService;

        public ImageController(IImageService imageService)
        {
            _imageService = imageService;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetImages()
        {
            IEnumerable<ImageDao> images = new List<ImageDao>();

            if (IsAdmin)
                images = await _imageService.GetImages();
            else if (IsWriter)
                images = await _imageService.GetImages(CurrentUserId);
            else if (IsReader)
                return Forbid();

            var response = new ApiResponse<IEnumerable<ImageDao>>(images);
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetImage([FromRoute] int id)
        {
            return File(await _imageService.GetImage(id), "image/jpg");
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Upload([FromForm] IFormFile file)
        {
            if (!IsAdmin && !IsWriter)
                return Forbid();

            await _imageService.InsertImage(file, CurrentUserId);

            return Ok();
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteImage([FromRoute] int id)
        {
            if (!IsAdmin && !IsWriter)
                return Forbid();

            var result = await _imageService.DeleteImage(id, IsAdmin, CurrentUserId);
            var response = new ApiResponse<bool>(result);
            return Ok(response);
        }
    }
}
