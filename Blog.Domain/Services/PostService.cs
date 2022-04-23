using Blog.Domain.DAOs;
using Blog.Domain.Entities;
using Blog.Domain.Interfaces.Repositories;
using Blog.Domain.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Domain.Services
{
    public class PostService : IPostService
    {
        private readonly IPostRepository _postRepository;

        public PostService(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }

        public async Task<IEnumerable<PostDao>> GetPosts(int userId = 0)
        {
            var posts = await _postRepository.GetPosts(userId);
            return posts.Select(p => new PostDao(p)).ToList();
        }

        public async Task<PostDao> GetPost(int id)
        {
            var postFound = await _postRepository.GetPost(id);

            if (postFound == null)
                throw new Exception("Post does not exist");

            return new PostDao(postFound);
        }

        public async Task<IEnumerable<CommentDao>> GetComments(int id)
        {
            var postFound = await _postRepository.GetPost(id);

            if (postFound == null)
                throw new Exception("Post does not exist");

            var comments = await _postRepository.GetComments(id);

            return comments.Select(p => new CommentDao(p)).ToList();
        }

        public async Task<bool> InsertPost(Post post)
        {
            return await _postRepository.InsertPost(post);
        }

        public async Task<bool> UpdatePost(Post post, bool isAdmin, int currentUserId)
        {
            var postFound = await _postRepository.GetPost(post.PostId);

            if (postFound == null)
                throw new Exception("Post does not exist");

            if (!isAdmin && postFound.User.UserId != currentUserId)
                throw new Exception("User does not have permission to perform this action");

            return await _postRepository.UpdatePost(post);
        }

        public async Task<bool> DeletePost(int id, bool isAdmin, int currentUserId)
        {
            var postFound = await _postRepository.GetPost(id);

            if (postFound == null)
                throw new Exception("Post does not exist");

            if (!isAdmin && postFound.User.UserId != currentUserId)
                throw new Exception("User does not have permission to perform this action");

            return await _postRepository.DeletePost(id);
        }
    }
}
