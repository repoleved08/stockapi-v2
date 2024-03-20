using Api.Dtos.Comment;
using Api.Models;

namespace Api.Mappers;

public static class CommentMapper
{
    public static CommentDto ToCommentFromCommentDto(this Comment commentDto)
    {
        ArgumentNullException.ThrowIfNull(commentDto);
        return new CommentDto
        {
            Id = commentDto.Id,
            Content = commentDto.Content,
            Title = commentDto.Title,
            CreatedOn = commentDto.CreatedOn ,
            CreatedBy = commentDto.AppUser.UserName?? string.Empty,
            StockId = commentDto.StockId
        };
    }

    public static Comment FromCreateCommentDtoToComment(this CreateCommentDto createCommentDto, int stockId)
    {
        return new Comment
        {
            Content = createCommentDto.Content,
            Title = createCommentDto.Title,
            StockId = stockId
        };
    }

    public static Comment FromUpdateCommentToComment(this UpdateCommentRequestDto commentDto)
    {
        return new Comment
        {
            Content = commentDto.Content,
            Title = commentDto.Title
        };
    }
}
