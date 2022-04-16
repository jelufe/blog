using Blog.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Blog.Domain.Interfaces.Repositories
{
    public interface IPostRepository
    {
        Task<IEnumerable<Post>> GetPosts(int userId);
        Task<Post> GetPost(int id);
        Task<IEnumerable<Comment>> GetComments(int id);
        Task InsertPost(Post post);
        Task<bool> UpdatePost(Post post);
        Task<bool> DeletePost(int id);
    }
}
