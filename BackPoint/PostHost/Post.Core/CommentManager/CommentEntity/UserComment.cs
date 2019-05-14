using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Post.Core.CommentManager.CommentEntity
{
    /// <summary>
    /// 该类表示评论的每一行，即一个用户的评论
    /// </summary>
    public class UserComment
    {
        /// <summary>
        /// 用户名
        /// </summary>
        [BsonElement("username")]
        public string UserName { get; set; }

        /// <summary>
        /// 用户头像
        /// </summary>
        [BsonElement("useravatorurl")]
        public string UserAvatorUrl { get; set; }

        /// <summary>
        /// 评论内容
        /// </summary>
        [BsonElement("content")]
        public string Content { get; set; }

        /// <summary>
        /// 评论的楼层
        /// </summary>
        [BsonElement("floor")]
        public string Floor { get; set; }

        /// <summary>
        /// 是否为回复某人
        /// 如果是，此处为回复人的姓名
        /// </summary>
        [BsonElement("replysomebody")]
        public string ReplySomebody { get; set; } = "";

        /// <summary>
        /// 点赞数
        /// </summary>
        [BsonElement("likes")]
        public int Likes { get; set; } = 0;

        /// <summary>
        /// 评论时间
        /// </summary>
        [BsonElement("time")]
        public DateTime Time { get; set; }
    }
}
