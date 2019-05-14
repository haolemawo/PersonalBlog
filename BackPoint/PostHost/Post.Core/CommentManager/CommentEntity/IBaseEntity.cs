using System;
using System.Collections.Generic;
using System.Text;

namespace Post.Core.CommentManager.CommentEntity
{
    public interface IBaseEntity<TPrimaryKey>
    {
        TPrimaryKey ArticleId { get; set; }
    }
}
