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

        public async Task<IEnumerable<CommentDao>> GetComments()
        {
            var comments = await _commentRepository.GetComments();
            return comments.Select(p => new CommentDao(p)).ToList();
        }

        public async Task<CommentDao> GetComment(int id)
        {
            var commentFound = await _commentRepository.GetComment(id);

            if (commentFound == null)
                throw new Exception("Comment does not exist");

            return new CommentDao(commentFound);
        }

        public async Task InsertComment(Comment comment)
        {
            await _commentRepository.InsertComment(comment);
        }

        public async Task<bool> UpdateComment(Comment comment)
        {
            var commentFound = await _commentRepository.GetComment(comment.CommentId);

            if (commentFound == null)
                throw new Exception("Comment does not exist");

            return await _commentRepository.UpdateComment(comment);
        }

        public async Task<bool> DeleteComment(int id)
        {
            var commentFound = await _commentRepository.GetComment(id);

            if (commentFound == null)
                throw new Exception("Comment does not exist");

            return await _commentRepository.DeleteComment(id);
        }
    }
}
