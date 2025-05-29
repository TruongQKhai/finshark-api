using api.Dtos.Comments;
using api.Models;

namespace api.Mappers;

public static class CommentMappers
{
    public static CommentDto ToDto(this Comment comment)
    {
        return new CommentDto
        {
            Id = comment.Id,
            Title = comment.Title,
            Content = comment.Content,
            CreateOn = comment.CreateOn,
            StockId = comment.StockId
        };
    }

    public static Comment ToEntity(this CreateCommentRequestDto req)
    {
        return new Comment
        {
            Title = req.Title,
            Content = req.Content,
            CreateOn = req.CreateOn,
            StockId = req.StockId
        };
    }

    public static Comment ToEntity(this UpdateCommentRequestDto req)
    {
        return new Comment
        {
            Title = req.Title,
            Content = req.Content,
        };
    }
}
