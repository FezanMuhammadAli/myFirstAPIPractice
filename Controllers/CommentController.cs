using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
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
        public IActionResult GetAll()
        {
            var comment= _context.Comments.ToList().Select(s=>s.ToCommentDto());
            return Ok(comment);
        }

        [HttpGet("{id}")]
        public IActionResult GetById([FromRoute]int id)
        {
            var comment= _context.Comments.Find(id);
            if(comment==null)
            {
                return NotFound();
            }
            return Ok(comment.ToCommentDto());
        }

        [HttpPost]
        public IActionResult Create([FromBody] CreateCommentRequestDto commentDto){
            var commentModel=commentDto.ToCommentFromCreateDTO();
            _context.Comments.Add(commentModel);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetById),new {id=commentModel.Id},commentModel.ToCommentDto());
        }


        [HttpPut("{id}")]
        public IActionResult Update(int id,[FromBody] UpdateCommentRequestDto updateDto){
            var commentModel=_context.Comments.FirstOrDefault(x=>x.Id==id);
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
        public IActionResult Delete([FromRoute] int id){
            var commentModel=_context.Comments.FirstOrDefault(x=>x.Id==id);
            if(commentModel==null){
                return NotFound();
            }
            _context.Comments.Remove(commentModel);
            _context.SaveChanges();
            return Ok(commentModel.ToCommentDto());
        }
    }    
}