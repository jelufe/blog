using Blog.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Blog.Domain.Interfaces.Repositories
{
    public interface ICommentRepository
    {
        Task<IEnumerable<Comment>> GetComments(int userId);
        Task<Comment> GetComment(int id);
        Task<bool> InsertComment(Comment comment);
        Task<bool> UpdateComment(Comment comment);
        Task<bool> DeleteComment(int id);
    }
}