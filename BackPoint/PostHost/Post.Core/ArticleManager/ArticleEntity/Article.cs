using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Post.Core.ArticleManager.ArticleTypeEntity;
using System;
using System.Collections.Generic;

namespace Post.Core.ArticleManager.ArticleEntity
{
    public class Article : Entity, IHasCreationTime, IHasModificationTime
    {
        /// <summary>
        /// 外键，TypeId
        /// </summary>
        public int TypeId { get; set; }

        /// <summary>
        /// 文章名
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
        /// 文章介绍
        /// </summary>
        public string Introduce { get; set; }

        /// <summary>
        /// 文章浏览量
        /// </summary>
        public int PageView { get; set; }

        /// <summary>
        /// 是否推荐该文章
        /// </summary>
        public string IsRecommend { get; set; }

        /// <summary>
        /// 文章标题图示路径
        /// </summary>
        public string Avator { get; set; }

        /// <summary>
        /// 文章标签
        /// eg、C#,ASP.NET Core
        /// </summary>
        public string ArticleTags { get; set; }

        /// <summary>
        /// 文章发布时间
        /// </summary>
        public DateTime CreationTime { get; set; }

        /// <summary>
        /// 文章修改时间
        /// </summary>
        public DateTime? LastModificationTime { get; set; }

        /// <summary>
        /// 外键，一对多
        /// 一个Type对应多个Article
        /// 一个Article对应一个Type
        /// </summary>
        public virtual ArticleType ArticleType { get; set; }
    }
}
