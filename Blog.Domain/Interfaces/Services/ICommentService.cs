using Blog.Domain.DAOs;
using Blog.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Blog.Domain.Interfaces.Services
{
    public interface ICommentService
    {
        Task<IEnumerable<CommentDao>> GetComments();
        Task<CommentDao> GetComment(int id);
        Task InsertComment(Comment comment);
        Task<bool> UpdateComment(Comment comment);
        Task<bool> DeleteComment(int id);
    }
}
