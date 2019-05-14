using Microsoft.AspNetCore.Mvc;
using Post.Application.CommentManager.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Post.Application.CommentManager
{
    public interface ICommentAppService
    {
        /// <summary>
        /// 创建一篇文章的评论区
        /// </summary>
        Task<bool> CreateCommentArea([FromBody] ArticleIdDto articleId);

        /// <summary>
        /// 发表评论
        /// </summary>
        /// <param name="userCommentDto">用户评论</param>
        /// <returns>评论是否成功</returns>
        Task<OutputUserCommentDto> CreateComment(UserCommentDto userCommentDto);

        /// <summary>
        /// 评论列表获取
        /// </summary>
        /// <param name="articleId">文章Id</param>
        /// <returns>评论列表</returns>
        Task<List<OutputUserCommentDto>> GetCommentList(string articleId);
    }
}
