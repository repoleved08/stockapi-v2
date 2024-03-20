using System.ComponentModel.DataAnnotations.Schema;

namespace Api.Models;

[Table("Comments")]
public class Comment
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public DateTime CreatedOn { get; set; } = DateTime.UtcNow.AddHours(3);
    public int? StockId { get; set; }
    public string AppUserId { get; set; } = String.Empty;
    public AppUser AppUser { get; set; } = null!;
}