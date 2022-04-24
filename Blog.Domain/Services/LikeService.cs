using Blog.Domain.DAOs;
using Blog.Domain.Entities;
using Blog.Domain.Interfaces.Repositories;
using Blog.Domain.Interfaces.Services;
using System;
using System.Threading.Tasks;

namespace Blog.Domain.Services
{
    public class LikeService : ILikeService
    {
        private readonly ILikeRepository _likeRepository;

        public LikeService(ILikeRepository likeRepository)
        {
            _likeRepository = likeRepository;
        }

        public async Task<LikeDao> GetLike(int postId, int userId)
        {
            var likeFound = await _likeRepository.GetLike(postId, userId);

            if (likeFound == null)
                throw new Exception("Like does not exist");

            return new LikeDao(likeFound);
        }

        public async Task<bool> InsertLike(Like like, int userId)
        {
            like.UserId = userId;

            var likeFound = await _likeRepository.GetLike(like.PostId, like.UserId);

            if (likeFound is not null)
                throw new Exception("Like already exists");

            like.CreatedAt = DateTime.Now;

            return await _likeRepository.InsertLike(like);
        }

        public async Task<bool> DeleteLike(int postId, int userId)
        {
            var likeFound = await _likeRepository.GetLike(postId, userId);

            if (likeFound == null)
                throw new Exception("Like does not exist");

            return await _likeRepository.DeleteLike(postId, userId);
        }
    }
}
