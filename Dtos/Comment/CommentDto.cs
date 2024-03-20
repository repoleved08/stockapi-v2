namespace Api.Dtos.Comment;

public class CommentDto
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public DateTime CreatedOn { get; set; } = DateTime.UtcNow.AddHours(3);
    public string CreatedBy { get; set; } = string.Empty;
    public int? StockId { get; set; }
    //Nav Property
    //public Models.Stock? Stock { get; set; }
}