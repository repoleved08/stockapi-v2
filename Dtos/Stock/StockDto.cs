using System.ComponentModel.DataAnnotations;
using Api.Dtos.Comment;

namespace Api.Dtos.Stock;

public class StockDto
{
    public int Id { get; set; }
    [Required]
    [MaxLength(10, ErrorMessage = "Symbol Cannot exceed 10 characters")]
    public string Symbol { get; set; } = string.Empty;
    [Required]
    [MaxLength(10, ErrorMessage = "Company Name cannot exceed 10 characters")]
    public string CompanyName { get; set; } = string.Empty; 
    [Required]
    [Range(1, 1000000000)]
    public decimal Purchase { get; set; }
    [Required]
    [Range(0.001, 10000000)]
    public decimal LastDiv { get; set; }
    [Required]
    [MaxLength(10, ErrorMessage = "Industry must not exceed 10 characters")]
    public string Industry { get; set; } = string.Empty;
    [Required]
    [Range(1, 4000000000)]
    public long MarketCap { get; set; }

    public List<CommentDto> Comments { get; set; } = new List<CommentDto>();
}
