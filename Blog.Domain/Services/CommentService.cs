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
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository _commentRepository;

        public CommentService(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }

        public async Task<IEnumerable<CommentDao>> GetComments(int userId = 0)
        {
            var comments = await _commentRepository.GetComments(userId);
            return comments.Select(p => new CommentDao(p)).ToList();
        }

        public async Task<CommentDao> GetComment(int id)
        {
            var commentFound = await _commentRepository.GetComment(id);

            if (commentFound == null)
                throw new Exception("Comment does not exist");

            return new CommentDao(commentFound);
        }

        public async Task<bool> InsertComment(Comment comment)
        {
            comment.CreatedAt = DateTime.Now;

            return await _commentRepository.InsertComment(comment);
        }

        public async Task<bool> UpdateComment(Comment comment, bool isAdmin, int currentUserId)
        {
            var commentFound = await _commentRepository.GetComment(comment.CommentId);

            if (commentFound == null)
                throw new Exception("Comment does not exist");

            if (!isAdmin && commentFound.User.UserId != currentUserId)
                throw new Exception("User does not have permission to perform this action");

            return await _commentRepository.UpdateComment(comment);
        }

        public async Task<bool> DeleteComment(int id, bool isAdmin, int currentUserId)
        {
            var commentFound = await _commentRepository.GetComment(id);

            if (commentFound == null)
                throw new Exception("Comment does not exist");

            if (!isAdmin && commentFound.User.UserId != currentUserId)
                throw new Exception("User does not have permission to perform this action");

            return await _commentRepository.DeleteComment(id);
        }
    }
}
