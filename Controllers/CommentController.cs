using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using myFirstAPI.Data;
using myFirstAPI.DTOs.Comment;
using myFirstAPI.Mappers;

namespace myFirstAPI.Controllers
{
    [Route("api/comment")]
    [ApiController]
    public class CommentController:ControllerBase
    {
        private readonly ApplicationDBContext _context;
        public CommentController(ApplicationDBContext context)
        {
            _context=context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var comment=await _context.Comments.ToListAsync();
            var commentDto=comment.Select(s=>s.ToCommentDto());
            return Ok(comment);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute]int id)
        {
            var comment=await _context.Comments.FindAsync(id);
            if(comment==null)
            {
                return NotFound();
            }
            return Ok(comment.ToCommentDto());
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCommentRequestDto commentDto){
            var commentModel=commentDto.ToCommentFromCreateDTO();
            await _context.Comments.AddAsync(commentModel);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById),new {id=commentModel.Id},commentModel.ToCommentDto());
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id,[FromBody] UpdateCommentRequestDto updateDto){
            var commentModel=await _context.Comments.FirstOrDefaultAsync(x=>x.Id==id);
            if(commentModel==null){
                return NotFound();
            }
            commentModel.StockId=updateDto.StockId;
            commentModel.Content=updateDto.Content;
            commentModel.Title=updateDto.Title;

            _context.SaveChanges();
            return Ok(commentModel.ToCommentDto());
        }


        [HttpDelete("{id}")]
        // [Route("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id){
            var commentModel=await _context.Comments.FirstOrDefaultAsync(x=>x.Id==id);
            if(commentModel==null){
                return NotFound();
            }
            _context.Comments.Remove(commentModel);
            await _context.SaveChangesAsync();
            return Ok(commentModel.ToCommentDto());
        }
    }    
}