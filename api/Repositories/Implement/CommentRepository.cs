using api.Database;
using api.Helpers;
using api.Models;
using api.Repositories.Interface;

namespace api.Repositories.Implement;

public class CommentRepository : ICommentRepository
{
    private readonly AppDbContext _context;

    public CommentRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Comment> CreateAsync(Comment commentModel)
    {
        await _context.Comments.AddAsync(commentModel);
        await _context.SaveChangesAsync();
        return commentModel;
    }

    public async Task<Comment?> UpdateAsync(int id, Comment comment)
    {
        var existingComment = await _context.Comments.FindAsync(id);

        if (existingComment == null)
        {
            return null;
        }

        existingComment.Title = comment.Title;
        existingComment.Content = comment.Content;

        await _context.SaveChangesAsync();

        return existingComment;
    }

    public Task<Comment?> DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<List<Comment>> GetAllAsync(CommentQueryObject queryObject)
    {
        throw new NotImplementedException();
    }

    public async Task<Comment?> GetByIdAsync(int id)
    {
        return await _context.Comments.FindAsync(id);
    }

}
