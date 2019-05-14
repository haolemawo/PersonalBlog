using Abp.Runtime.Validation;
using Post.Application.BaseDtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Post.Application.ArticleManage.Dtos
{
    public class GetArticleInputDto : PagedAndFilteredInputDto, IShouldNormalize
    {
        public void Normalize()
        {
            if (string.IsNullOrWhiteSpace(Sorting))
            {
                Sorting = "Id";
            }
        }

        /// <summary>
        /// 以Type过滤
        /// </summary>
        public int TypeId { get; set; }

        /// <summary>
        /// 以是否推荐过滤
        /// </summary>
        public string IsRecommend { get; set; }
    }
}
