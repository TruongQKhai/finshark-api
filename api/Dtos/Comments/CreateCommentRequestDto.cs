using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata.Ecma335;

namespace api.Dtos.Comments;

public class CreateCommentRequestDto
{
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public DateTime CreateOn { get; set; } = DateTime.Now;

    [Required]
    public int StockId { get; set; }
}
