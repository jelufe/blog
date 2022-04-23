using Blog.Domain.DAOs;
using Blog.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Blog.Domain.Interfaces.Services
{
    public interface IPostService
    {
        Task<IEnumerable<PostDao>> GetPosts(int userId = 0);
        Task<PostDao> GetPost(int id);
        Task<IEnumerable<CommentDao>> GetComments(int id);
        Task<bool> InsertPost(Post post);
        Task<bool> UpdatePost(Post post, bool isAdmin, int currentUserId);
        Task<bool> DeletePost(int id, bool isAdmin, int currentUserId);
    }
}
