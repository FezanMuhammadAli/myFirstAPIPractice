using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Web;
using myFirstAPI.DTOs.Comment;
using myFirstAPI.Models;

namespace myFirstAPI.Mappers
{
    public static class CommentMapper
    {
        public static CommentDto ToCommentDto(this Comment CommentModel){
            return new CommentDto{
                StockId=CommentModel.StockId,
                CommentTitle=CommentModel.Title,
                CommentContent=CommentModel.Content,
                CommentCreatedOn=CommentModel.CreatedOn
            };
        }
    }
}