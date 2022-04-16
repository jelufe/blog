using Blog.Domain.DAOs;
using Blog.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Blog.Domain.Interfaces.Services
{
    public interface ICommentService
    {
        Task<IEnumerable<CommentDao>> GetComments(int userId = 0);
        Task<CommentDao> GetComment(int id);
        Task InsertComment(Comment comment);
        Task<bool> UpdateComment(Comment comment, bool isAdmin, int currentUserId);
        Task<bool> DeleteComment(int id, bool isAdmin, int currentUserId);
    }
}
