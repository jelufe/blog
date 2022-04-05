using Blog.Domain.DAOs;
using Blog.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Blog.Domain.Interfaces.Services
{
    public interface IPostService
    {
        Task<IEnumerable<PostDao>> GetPosts();
        Task<PostDao> GetPost(int id);
        Task<IEnumerable<CommentDao>> GetComments(int id);
        Task InsertPost(Post post);
        Task<bool> UpdatePost(Post post);
        Task<bool> DeletePost(int id);
    }
}
