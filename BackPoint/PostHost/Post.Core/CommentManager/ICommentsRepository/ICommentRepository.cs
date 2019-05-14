using Post.Core.CommentManager.CommentEntity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Post.Core.CommentManager.ICommentsRepository
{
    public interface ICommentRepository
    {
        /// <summary>
        /// 创建评论区
        /// 2019/5/11
        /// </summary>
        /// <param name="articleId">文章Id</param>
        /// <returns>空</returns>
        Task<bool> CreateCommentsAreaAsync(string articleId);

        /// <summary>
        /// 判断文章的评论区是否存在
        /// 2019/5/11
        /// </summary>
        /// <param name="articleId">文章Id</param>
        /// <returns>评论区</returns>
        Task<ArticleComment> GetCommentAreaByIdAsync(string articleId);

        /// <summary>
        /// 写评论
        /// 2019/5/12
        /// </summary>
        /// <param name="userComment">用户评论</param>
        /// <param name="articleId">文章Id</param>
        /// <returns>评论是否成功</returns>
        Task<UserComment> AddCommentToAreaAsync(UserComment userComment, string articleId);

        /// <summary>
        /// 获取评论列表
        /// 2019/5/12
        /// </summary>
        /// <param name="articleId">文章Id</param>
        /// <returns>评论列表</returns>
        Task<List<UserComment>> GetCommentListAsync(string articleId);

        /// <summary>
        /// 评论点赞
        /// 2019/5/12
        /// </summary>
        /// <param name="articleId">文章Id</param>
        /// <param name="floor">评论楼层</param>
        /// <param name="likes">点赞数</param>
        /// <returns>点赞数</returns>
        Task<int> AddAdmireAsync(string articleId, string floor, int likes);

        /// <summary>
        /// 删除评论
        /// 置为null
        /// 2019/5/12
        /// </summary>
        /// <param name="articleId">文章Id</param>
        /// <param name="floor">评论楼层</param>
        /// <returns>删除结果</returns>
        Task<bool> DeleteCommentAsync(string articleId, string floor);

        /// <summary>
        /// 删除评论区，用于文章删除时删除相关文章的评论区
        /// 2019/5/13
        /// </summary>
        /// <param name="articleId">文章Id</param>
        /// <returns>null</returns>
        Task DeleteCommentAreaAsync(string articleId);
    }
}
