using Api.Models;

namespace Api.Interface;

public interface ICommentInterface
{
    Task<List<Comment>> GetAllComments();
    Task<Comment?> GetCommentById(int id);
    Task<Comment> CreateComment(Comment comment);
    Task<Comment?> UpdateComment(int id, Comment comment);
    Task<string> DeleteComment(int id);
}
