using System;
using System.Collections.Generic;
using System.Text;

namespace Post.Application.ArticleManage.Dtos
{
    public class ArticleDetailDto
    {
        /// <summary>
        /// 文章类型ID
        /// </summary>
        public int TypeId { get; set; }

        /// <summary>
        /// 文章名
        /// </summary>
        public string ArticleName { get; set; }

        /// <summary>
        /// 文章作者
        /// </summary>
        public string Author { get; set; }

        /// <summary>
        /// 文章内容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 文章介绍
        /// </summary>
        public string Introduce { get; set; }

        /// <summary>
        /// 文章浏览量
        /// </summary>
        public int PageView { get; set; }

        /// <summary>
        /// 是否作者推荐
        /// </summary>
        public string IsRecommend { get; set; }

        /// <summary>
        /// 文章标签
        /// </summary>
        public string ArticleTags { get; set; }

        /// <summary>
        /// 发布时间
        /// </summary>
        public DateTime CreationTime { get; set; }
    }
}
