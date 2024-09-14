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
                Title=CommentModel.Title,
                Content=CommentModel.Content,
                CreatedOn=CommentModel.CreatedOn
            };
        }

        public static Comment ToCommentFromCreateDTO(this CreateCommentRequestDto commentDto){
            return new Comment{
                StockId=commentDto.StockId,
                Title=commentDto.Title,
                Content=commentDto.Content,
                CreatedOn=commentDto.CreatedOn
            };
        }
    }
}