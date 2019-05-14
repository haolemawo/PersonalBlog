using Abp.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using Post.Core;
using Post.Core.CommentManager.CommentEntity;
using Post.Core.CommentManager.ICommentsRepository;
using Post.EntityFrameworkCore.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Post.EntityFrameworkCore.Comments
{
    /// <summary>
    /// 评论区的仓储实现
    /// </summary>
    public class CommentRepository : ICommentRepository
    {
        //注入MongoDB数据库
        private readonly IMongoDatabase _db;
        private readonly IMongoCollection<ArticleComment> _collections;
        private readonly ILogger _logger;

        public CommentRepository(IDbContextProvider<PostDbContext> dbContextProvider,
            ILoggerFactory loggerFactory) 
        {
            var client = new MongoClient(PostConsts.MongoDBConnectionStr);
            _db = client.GetDatabase(PostConsts.MongoDBForComment);
            _collections = _db.GetCollection<ArticleComment>(PostConsts.MongoDBForComment);
            _logger = loggerFactory.CreateLogger<CommentRepository>();
        }

        /// <summary>
        /// 创建文章评论区，用于文章发布阶段
        /// 2019/5/11
        /// </summary>
        /// <param name="articleId">文章Id</param>
        /// <returns></returns>
        public async Task<bool> CreateCommentsAreaAsync(string articleId)
        {
            //创建新对象
            try
            {
                ArticleComment articleComment = new ArticleComment()
                {
                    ArticleId = articleId
                };
                await _collections.InsertOneAsync(articleComment);
            }
            catch(Exception ex)
            {
                //异常记录
                _logger.LogError(ex, ex.Message);
                return false;
            }
            return true;
        }

        /// <summary>
        /// 判断文章评论区是否存在
        /// 2019/5/11
        /// </summary>
        /// <param name="articleId">文章Id</param>
        /// <returns>文章评论区</returns>
        public async Task<ArticleComment> GetCommentAreaByIdAsync(string articleId)
        {
            var articleCommentArea = await _collections
                .Find(x => x.ArticleId == articleId)
                .FirstOrDefaultAsync();
            return articleCommentArea;
        }

        /// <summary>
        /// 评论
        /// 2019/5/11
        /// </summary>
        /// <param name="userComment">用户评论</param>
        /// <param name="articleId">文章Id</param>
        /// <returns>评论内容</returns>
        public async Task<UserComment> AddCommentToAreaAsync(UserComment userComment, string articleId)
        {
            //查询现在的数量
            var articleCommentArea = await _collections
                .Find(s => s.ArticleId == articleId)
                .FirstOrDefaultAsync();

            if (articleCommentArea != null)
            {
                //计算当前是第几楼
                lock (PostConsts.lockCommentFloorObj)
                {
                    userComment.Floor = (articleCommentArea.UserComments.Count + 1).ToString();
                }
            }

            //限定查询条件，查询articleId相等的集合
            var filter = Builders<ArticleComment>.Filter.Eq(c => c.ArticleId, articleId);
            //限定更新条件，将userComment加载到评论区中
            var update = Builders<ArticleComment>.Update.AddToSet(c => c.UserComments, userComment);

            var result = await _collections.UpdateOneAsync(filter, update, null);

            if (result.MatchedCount == result.ModifiedCount && result.ModifiedCount == 1)
            {
                return userComment;
            }
            return null;
        }

        /// <summary>
        /// 根据ArticleId查询
        /// 2019/5/11
        /// </summary>
        /// <param name="articleId">文章Id</param>
        /// <returns></returns>
        public async Task<List<UserComment>> GetCommentListAsync(string articleId)
        {
            //查询文章评论
            ArticleComment commentList = await _collections
                .Find(x => x.ArticleId == articleId)
                .FirstOrDefaultAsync();

            if (commentList != null)
            {
                return commentList.UserComments;
            }
            else {
                return null;
            }
        }

        /// <summary>
        /// 为喜欢的评论点赞
        /// 2019/5/12
        /// </summary>
        /// <param name="articleId">文章Id</param>
        /// <param name="likes">点赞数</param>
        /// <param name="floor">回复楼层数</param>
        /// <returns></returns>
        public async Task<int> AddAdmireAsync(string articleId, string floor, int likes)
        {
            //过滤域
            var filter = Builders<ArticleComment>.Filter
                .And(
                    Builders<ArticleComment>.Filter.Eq(c => c.ArticleId, articleId),
                    Builders<ArticleComment>.Filter.Eq("UserComments.Floor", floor)
                );

            //原子递增操作
            Interlocked.Increment(ref likes);

            //更新
            var update = Builders<ArticleComment>
                .Update
                .Set("UserComments.$.likes", likes);

            var result = await _collections.UpdateOneAsync(filter, update, null);

            //返回更新结果
            if (result.MatchedCount == result.ModifiedCount && result.MatchedCount == 1)
            {
                return likes;
            }
            else {
                //更新失败，返回原样
                Interlocked.Decrement(ref likes);
                return likes;
            }
        }

        /// <summary>
        /// 删除评论
        /// 注意这里的删除只是在Mongp数据库中将相应字段设置为了null
        /// 2019/5/12
        /// </summary>
        /// <param name="articleId">文章Id</param>
        /// <param name="floor">楼层</param>
        /// <returns></returns>
        public async Task<bool> DeleteCommentAsync(string articleId, string floor)
        {
            var filter = Builders<ArticleComment>.Filter
                .And(
                    Builders<ArticleComment>.Filter.Eq(c => c.ArticleId, articleId),
                    Builders<ArticleComment>.Filter.Eq("UserComments.Floor", floor)
                );

            //删除
            var update = Builders<ArticleComment>
                .Update
                .Unset("UserComments.$");

            var result = await _collections.UpdateOneAsync(filter, update, null);

            return (result.MatchedCount == result.ModifiedCount && result.MatchedCount == 1);
        }

        /// <summary>
        /// 删除评论区域，用于文章删除时调用
        /// 2019/5/13
        /// </summary>
        /// <param name="articleId">文章Id</param>
        /// <returns>null</returns>
        public async Task DeleteCommentAreaAsync(string articleId)
        {
            var deleteResult = await _collections.DeleteOneAsync(s => s.ArticleId == articleId);
            if (deleteResult.DeletedCount != 1)
            {
                //记录删除异常
                _logger.LogError("删除评论区异常");
            }
        }
    }
}
