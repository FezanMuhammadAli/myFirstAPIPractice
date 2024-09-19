using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace myFirstAPI.DTOs.Comment
{
    public class UpdateCommentRequestDto
    {
        public int? StockId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; }=string.Empty;
    }
}