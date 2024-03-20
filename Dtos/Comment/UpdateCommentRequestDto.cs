using System.ComponentModel.DataAnnotations;

namespace Api;

public class UpdateCommentRequestDto
{
    [Required]
    [MinLength(2, ErrorMessage = "Title Must be more than two characters!")]
    [MaxLength(200, ErrorMessage = "Title Cannot be more than 200 characters")]
    public string Title { get; set; } = string.Empty;
    [Required]
     [MinLength(10, ErrorMessage = "Content Cannot be less than 10 characters")]
    [MaxLength(200, ErrorMessage = "Content cannot exceed 200 characters")]
    public string Content { get; set; } = string.Empty;
}
