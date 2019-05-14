using System;
using System.Collections.Generic;
using System.Text;

namespace Post.Application.CommentManager.Dtos
{
    public class DeleteCommentDto
    {
        /// <summary>
        /// 文章ID
        /// </summary>
        public string ArticleId { get; set; }

        /// <summary>
        /// 评论楼层
        /// </summary>
        public string Floor { get; set; }
    }
}
