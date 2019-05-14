using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Post.Core.ArticleManager.ArticleEntity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Post.Core.ArticleManager.ArticleTypeEntity
{
    public class ArticleType : Entity, IHasCreationTime
    {
        /// <summary>
        /// 标签名字
        /// </summary>
        public string TypeName { get; set; }

        /// <summary>
        /// 标签值
        /// </summary>
        public int TypeValue { get; set; }

        /// <summary>
        /// 类型头型路径
        /// </summary>
        public string TypeAvator { get; set; } 

        /// <summary>
        /// 标签创建时间
        /// </summary>
        public DateTime CreationTime { get; set; }

        /// <summary>
        /// 外键，一个Type对应多个Article
        /// </summary>
        public virtual ICollection<Article> Articles { get; set; }
    }
}
