using System;
using System.Collections.Generic;
using System.Text;

namespace Post.Core
{
    public class PostConsts
    {
        public const string LocalizationSourceName = "Post";
        public const string ConnectionStringName = "Default";
        /// <summary>
        /// 基础路径
        /// </summary>
        public static string BaseTrail = "";

        /// <summary>
        /// Redis缓存路径
        /// </summary>
        public static string RedisUrl = "";

        /// <summary>
        /// 存储文章的Redis数据库名
        /// </summary>
        public static string RedisForArticleStore = "";

        /// <summary>
        /// 存储访问量的Redis数据库名
        /// </summary>
        public static string RedisForViewerStore = "";

        /// <summary>
        /// 存储文章的Redis基础键
        /// </summary>
        public static string ArticleBaseKey = "";

        /// <summary>
        /// 存储浏览量的Redis键
        /// </summary>
        public static string ViewerCountKey = "";

        /// <summary>
        /// MongoDB连接字符串
        /// </summary>
        public static string MongoDBConnectionStr = "";

        /// <summary>
        /// 使用的MongoDB名
        /// </summary>
        public static string MongoDBForComment = "";

        /// <summary>
        /// 用于在计算评论楼层时保证线程同步
        /// </summary>
        public static Object lockCommentFloorObj = "";
    }
}
