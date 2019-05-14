using Abp.Runtime.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Post.Application.CommentManager.Dtos
{
    public class UserCommentDto : ICustomValidate
    {
        /// <summary>
        /// 前端传递的文章Id
        /// </summary>
        public string ArticleId { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 用户头像
        /// </summary>
        public string UserAvatorUrl { get; set; }

        /// <summary>
        /// 评论内容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 楼层数，几楼回复
        /// </summary>
        public string Floor { get; set; }

        /// <summary>
        /// 是否为回复某人
        /// </summary>
        public string ReplySomebody { get; set; } = "";

        /// <summary>
        /// 点赞数
        /// </summary>
        public int Likes { get; set; }

        /// <summary>
        /// 评论时间
        /// </summary>
        public DateTime Time { get; set; } = DateTime.Now;

        public void AddValidationErrors(CustomValidationContext context)
        {
            if (string.IsNullOrWhiteSpace(Content))
            {
                string error = "评论内容不能为空";
                context.Results.Add(new ValidationResult(error));
            }
        }
    }
}
