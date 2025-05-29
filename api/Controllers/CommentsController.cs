using api.Dtos.Comments;
using api.Helpers;
using api.Mappers;
using api.Repositories.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using Microsoft.OpenApi.Writers;

namespace api.Controllers
{
    [ApiController]
    [Route("api/comments")]
    public class CommentsController : ControllerBase
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IStockRepository _stockRepository;

        public CommentsController(ICommentRepository commentRepository, IStockRepository stockRepository)
        {
            this._commentRepository = commentRepository;
            this._stockRepository = stockRepository;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCommentRequestDto req)
        {
            if (await _stockRepository.StockExists(req.StockId))
                return BadRequest("Stock does not exist");

            var comment = req.ToEntity();
            await _commentRepository.CreateAsync(comment);

            return CreatedAtAction(nameof(GetById), new { id = comment.Id }, comment.ToDto());
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateCommentRequestDto req)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var comment = await _commentRepository.UpdateAsync(id, req.ToEntity());
            if (comment == null)
            {
                return NotFound("Comment not found");
            }

            return Ok(comment.ToDto());
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] CommentQueryObject queryObject)
        {

            var comments = await _commentRepository.GetAllAsync(queryObject);
            var commentsDto = comments.Select(c => c.ToDto());

            return Ok(commentsDto);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var comment = await _commentRepository.GetByIdAsync(id);
            if (comment is null)
                return NotFound();

            return Ok(comment.ToDto());
        }


    }
}
