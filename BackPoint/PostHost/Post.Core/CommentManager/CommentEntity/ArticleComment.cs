using Abp.Domain.Entities;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace Post.Core.CommentManager.CommentEntity
{
    /// <summary>
    /// 该类用于获取一篇文章下的评论
    /// </summary>
    [BsonIgnoreExtraElements]
    public class ArticleComment : IBaseEntity<string>
    {
        /// <summary>
        /// 文章Id，一篇文章对应一个Collection，属于一对一
        /// </summary>
        public ArticleComment()
        {
            UserComments = new List<UserComment>();
        }

        //重写主键
        //主键默认ObjectId类型
        [BsonId]
        //[BsonRepresentation(BsonType.ObjectId)]
        public string ArticleId { get; set; }

        /// <summary>
        /// 一篇文章可能对应多个用户的评论，即一个Collection中包含嵌套的Json组合
        /// 结构如下
        /* {
                articleId: String,
	            commentList: [
                    userName: String,
                    userAvatorUrl: String,
                    content: String,
                    likes: Int,
                    time: Datetime,
                    reply: [
			            ...
		            ]
	            ]
            }
        */
        /// </summary>
        [BsonElement("usercomments")]
        public List<UserComment> UserComments { get; set; }
    }
}
