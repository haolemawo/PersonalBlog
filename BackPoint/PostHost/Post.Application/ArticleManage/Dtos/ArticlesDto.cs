using System;
using System.Collections.Generic;
using System.Text;

namespace Post.Application.ArticleManage.Dtos
{
    public class ArticlesDto
    {
        /// <summary>
        /// 文章Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 文章类型
        /// </summary>
        public int TypeId { get; set; }

        /// <summary>
        /// 文章名字
        /// </summary>
        public string ArticleName { get; set; }

        /// <summary>
        /// 文章路径
        /// </summary>
        public string ArticleUrl { get; set; }

        /// <summary>
        /// 文章作者
        /// </summary>
        public string Author { get; set; }

        /// <summary>
        /// 作者头像
        /// </summary>
        public string AuthorAvator { get; set; }

        /// <summary>
        /// 文章简介
        /// </summary>
        public string Introduce { get; set; }

        /// <summary>
        /// 文章图片
        /// </summary>
        public string Avator { get; set; }

        /// <summary>
        /// 文章标签
        /// </summary>
        public string ArticleTags { get; set; }

        /// <summary>
        /// 文章浏览量
        /// </summary>
        public int PageView { get; set; }

        /// <summary>
        /// 文章是否推荐
        /// </summary>
        public string IsRecommend { get; set; }

        /// <summary>
        /// 发布时间
        /// </summary>
        public DateTime CreationTime { get; set; }
    }
}
