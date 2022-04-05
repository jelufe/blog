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

        public async Task<IEnumerable<PostDao>> GetPosts()
        {
            var posts = await _postRepository.GetPosts();
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

        public async Task InsertPost(Post post)
        {
            await _postRepository.InsertPost(post);
        }

        public async Task<bool> UpdatePost(Post post)
        {
            var postFound = await _postRepository.GetPost(post.PostId);

            if (postFound == null)
                throw new Exception("Post does not exist");

            return await _postRepository.UpdatePost(post);
        }

        public async Task<bool> DeletePost(int id)
        {
            var postFound = await _postRepository.GetPost(id);

            if (postFound == null)
                throw new Exception("Post does not exist");

            return await _postRepository.DeletePost(id);
        }
    }
}
