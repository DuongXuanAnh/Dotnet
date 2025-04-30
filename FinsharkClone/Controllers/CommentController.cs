using FinsharkClone.Interfaces;
using FinsharkClone.Mappers;
using Microsoft.AspNetCore.Mvc;
using FinsharkClone.Dtos.Comment;

namespace FinsharkClone.Controllers
{
    [Route("api/comments")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IStockRepository _stockRepository;

        public CommentController(ICommentRepository commentRepository, IStockRepository stockRepository)
        {
            _commentRepository = commentRepository;
            _stockRepository = stockRepository;
        }
        
        [HttpGet]
        public async Task<IActionResult> GetAll(){

            if(!ModelState.IsValid){
                return BadRequest(ModelState);
            }

            var comments = await _commentRepository.GetAllAsync();
            var commentDtos = comments.Select(c => c.ToCommentDto());
            return Ok(commentDtos);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id){
            if(!ModelState.IsValid){
                return BadRequest(ModelState);
            }

            var comment = await _commentRepository.GetByIdAsync(id);

            if(comment == null){
                return NotFound();
            }

            return Ok(comment.ToCommentDto());
        }

        [HttpPost("{stockId:int}")]
        public async Task<IActionResult> Create([FromRoute] int stockId, [FromBody] CreateCommentDto commentDto){

            if(!ModelState.IsValid){
                return BadRequest(ModelState);
            }

            if(!await _stockRepository.StockExists(stockId)){
                return NotFound("Stock not found");
            }

            var commentModel = commentDto.ToCommentFromCreate(stockId);
           await _commentRepository.CreateAsync(commentModel);

           return CreatedAtAction(nameof(GetById), new {id = commentModel.Id}, commentModel.ToCommentDto());
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateCommentRequestDto updateDto)
        {
            if(!ModelState.IsValid){
                return BadRequest(ModelState);
            }

            var comment = await _commentRepository.UpdateAsync(id, updateDto.ToCommentFromUpdate(id));

            if (comment == null)
            {
                return NotFound("Comment not found");
            }

            return Ok(comment.ToCommentDto());
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id){
            if(!ModelState.IsValid){
                return BadRequest(ModelState);
            }

            var comment = await _commentRepository.DeleteAsync(id);

            if(comment == null){
                return NotFound("Comment not found");
            }

            return Ok(comment.ToCommentDto());
        }
    }
}