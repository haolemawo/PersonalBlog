using Abp.Runtime.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Post.Application.ArticleManage.Dtos
{
    public class NewArticleDto : ICustomValidate
    {
        /// <summary>
        /// 文章标题
        /// </summary>
        public string ArticleName { get; set; }

        /// <summary>
        /// 文章类型
        /// </summary>
        public int TypeId { get; set; }

        /// <summary>
        /// 文章标签
        /// </summary>
        public string ArticleTags { get; set; }

        /// <summary>
        /// 文章内容
        /// </summary>
        public string ArticleContent { get; set; }

        /// <summary>
        /// 文章头像
        /// </summary>
        public string Avator { get; set; }

        /// <summary>
        /// 作者头像
        /// </summary>
        public string AuthorAvator { get; set; }

        /// <summary>
        /// 文章简介
        /// </summary>
        public string Introduce { get; set; }


        /// <summary>
        /// 是否推荐该文章
        /// </summary>
        public string IsRecommend { get; set; }

        /// <summary>
        /// 作者
        /// </summary>
        public string Author { get; set; }

        /// <summary>
        /// 文章在硬盘上的存储路径
        /// </summary>
        public string ArticleUrl { get; set; }

        /// <summary>
        /// 文章发布时间
        /// </summary>
        public DateTime CreationTime { get; set; } = DateTime.Now;

        /// <summary>
        /// 文章最近修改时间
        /// </summary>
        public DateTime LastModificationTime { get; set; } = DateTime.Now;

        public void AddValidationErrors(CustomValidationContext context)
        {
            if (string.IsNullOrWhiteSpace(ArticleName))
            {
                string error = "文章名不能为空";
                context.Results.Add(new ValidationResult(error));
            }

            if (string.IsNullOrWhiteSpace(ArticleTags))
            {
                string error = "请至少选择一个文章标签";
                context.Results.Add(new ValidationResult(error));
            }

            if (string.IsNullOrWhiteSpace(ArticleContent))
            {
                string error = "文章内容不能为空";
                context.Results.Add(new ValidationResult(error));
            }

            if (string.IsNullOrWhiteSpace(Author))
            {
                string error = "文章作者不能为空";
                context.Results.Add(new ValidationResult(error));
            }
        }
    }
}
