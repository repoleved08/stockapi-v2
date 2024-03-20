using Api.Data;
using Api.Interface;
using Api.Mappers;
using Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Api.Service;

public class CommentService(AppDbContext context):ICommentInterface
{
    public async Task<List<Comment>> GetAllComments()
    {
        return await context.Comments.Include(a=>a.AppUser).ToListAsync();
    }

    public async Task<Comment?> GetCommentById(int id)
    {
        var comment = await context.Comments.Include(a=>a.AppUser).FirstOrDefaultAsync(x => x.Id == id);
        return comment;
    }

    public async Task<Comment> CreateComment(Comment comment)
    {
        await context.Comments.AddAsync(comment);
        await context.SaveChangesAsync();
        return comment;
    }

    public async Task<Comment> UpdateComment(int id, Comment comment)
    {
        var existingCommentText = await context.Comments.FindAsync(id);
        try
        {
            if (existingCommentText == null)
            {
                return null;
            }
            existingCommentText.Title = comment.Title;
            existingCommentText.Content = comment.Content;

            await context.SaveChangesAsync();
            return existingCommentText;
        }
        catch (Exception e)
        {
            
            throw new Exception(e.InnerException.Message);
        }

    }

    public async Task<string> DeleteComment(int id)
    {
        var comment = await context.Comments.FirstOrDefaultAsync(x=>x.Id == id);
        if (comment != null)
        {
            context.Comments.Remove(comment);
            await context.SaveChangesAsync();
            return "Comment was successfully deleted!";
        }
        
        return "Comment Not Found!";
    }
}
