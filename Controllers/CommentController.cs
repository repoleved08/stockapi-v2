using Api.Dtos.Comment;
using Api.Extensions;
using Api.Interface;
using Api.Mappers;
using Api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CommentController: ControllerBase
{
    private readonly ICommentInterface _commentInterface;
    private readonly IStockInterface _stockInterface;
    private readonly UserManager<AppUser> _userManager;
    public CommentController(ICommentInterface commentInterface,UserManager<AppUser> userManager, IStockInterface stockInterface)
    {
        _commentInterface = commentInterface;
        _stockInterface = stockInterface;
        _userManager = userManager;
    }
    [HttpGet]
    public async Task<IActionResult> GetComments()
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var comments = await _commentInterface.GetAllComments();
        var commentDto = comments.Select(s => s.ToCommentFromCommentDto());
        return Ok(commentDto);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetCommentById([FromRoute] int id)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
            
        var comment = await _commentInterface.GetCommentById(id);
        if (comment == null)
        {
            return NotFound();
        }

        return Ok(comment.ToCommentFromCommentDto());
    }

    [HttpPost("{stockId:int}")]
    public async Task<IActionResult> PostComment([FromRoute] int stockId, CreateCommentDto createComment)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        if (!await _stockInterface.StockExist(stockId))
        {
            return BadRequest("Stock Does Not Exist");
        }

        var username = User.GetByUserName();
        var appUser = await _userManager.FindByNameAsync(username);
        var commentModel = createComment.FromCreateCommentDtoToComment(stockId);
        commentModel.AppUserId = appUser.Id;
        await _commentInterface.CreateComment(commentModel);
        return CreatedAtAction(nameof(GetCommentById), new { id = commentModel.Id },
            commentModel.ToCommentFromCommentDto());
    }

    [HttpPut]
    [Route("{id:int}")]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateCommentRequestDto updateDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var commentU = await _commentInterface.UpdateComment(id, updateDto.FromUpdateCommentToComment());
        if (commentU != null)
        {
            return Ok(commentU.ToCommentFromCommentDto());
        }
        return NotFound("Comment Not Found");
    }

    [HttpDelete]
    [Route("{id:int}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState); 

        var commentModel = await _commentInterface.DeleteComment(id);

        if (commentModel == null)
        {
            return NotFound("Comment Not Found");
        }
        return Ok(commentModel);
    }

}
