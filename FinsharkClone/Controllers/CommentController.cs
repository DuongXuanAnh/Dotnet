using FinsharkClone.Interfaces;
using FinsharkClone.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace FinsharkClone.Controllers
{
    [Route("api/comments")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentRepository _commentRepository;

        public CommentController(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }
        
        [HttpGet]
        public async Task<IActionResult> GetAll(){
            var comments = await _commentRepository.GetAllAsync();
            var commentDtos = comments.Select(c => c.ToCommentDto());
            return Ok(commentDtos);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id){
            var comment = await _commentRepository.GetByIdAsync(id);

            if(comment == null){
                return NotFound();
            }

            return Ok(comment.ToCommentDto());
        }
    }
}