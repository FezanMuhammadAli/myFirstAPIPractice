using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace myFirstAPI.DTOs.Comment
{
    public class CommentDto
    {
        public int? StockId { get; set; }
        public string CommentTitle { get; set; } = string.Empty;
        public string CommentContent { get; set; }=string.Empty;
        public DateTime CommentCreatedOn { get; set; }=DateTime.Now;
    }
}