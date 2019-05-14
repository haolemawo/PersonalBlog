using System;
using System.Collections.Generic;
using System.Text;

namespace Post.Application.MainPageManage.Dtos
{
    public class RecommendArticlesDto
    {
        /// <summary>
        /// 文章Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 作者名
        /// </summary>
        public string Author { get; set; }

        /// <summary>
        /// 文章头像
        /// </summary>
        public string Avator { get; set; }

        /// <summary>
        /// 作者头像
        /// </summary>
        public string AuthorAvator { get; set; }

        /// <summary>
        /// 文章名
        /// </summary>
        public string ArticleName { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public string CreationTime { get; set; }

        /// <summary>
        /// 文章简介
        /// </summary>
        public string Introduce { get; set; }

        /// <summary>
        /// 文章标签
        /// </summary>
        public string ArticleTags { get; set; }
    }
}
