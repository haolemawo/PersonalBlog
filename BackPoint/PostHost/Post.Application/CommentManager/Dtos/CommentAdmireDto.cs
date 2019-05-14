using Abp.Runtime.Validation;
using System.ComponentModel.DataAnnotations;

namespace Post.Application.CommentManager.Dtos
{
    public class CommentAdmireDto : ICustomValidate
    {
        /// <summary>
        /// 文章ID
        /// </summary>
        public string ArticleId { get; set; }

        /// <summary>
        /// 评论楼层
        /// </summary>
        public string Floor { get; set; }

        /// <summary>
        /// 点赞数
        /// </summary>
        public int Likes { get; set; }

        public void AddValidationErrors(CustomValidationContext context)
        {
            if (string.IsNullOrWhiteSpace(Floor))
            {
                string error = "楼层数不能为空";
                context.Results.Add(new ValidationResult(error));
            }
        }
    }
}
