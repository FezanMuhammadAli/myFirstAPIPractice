using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using myFirstAPI.Data;

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
            var comment= _context.Comments.ToList();
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
            return Ok(comment);
        }
    }
}