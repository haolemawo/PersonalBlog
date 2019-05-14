using System;
using System.Collections.Generic;
using System.Text;

namespace Post.Application.ArticleManage.Dtos
{
    public class ArticleTypeDto
    {
        /// <summary>
        /// Type Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Type图片路径
        /// </summary>
        public string TypeAvator { get; set; }

        /// <summary>
        /// Type值
        /// </summary>
        public string TypeName { get; set; }
    }
}
