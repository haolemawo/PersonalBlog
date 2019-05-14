using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Post.Application.CommentManager.Dtos;
using Post.Core.ArticleManager.ArticleEntity;
using Post.Core.CommentManager.CommentEntity;
using Post.Core.CommentManager.ICommentsRepository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Post.Application.CommentManager
{
    public class CommentAppService : PostAppServiceBase, ICommentAppService
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IRepository<Article> _articleRepository;
        private readonly ILogger _logger;

        public CommentAppService(ICommentRepository commentRepository,
            IRepository<Article> articleRepository,
            ILoggerFactory loggerFactory)
        {
            _commentRepository = commentRepository;
            _articleRepository = articleRepository;
            _logger = loggerFactory.CreateLogger<CommentAppService>();
        }

        /// <summary>
        /// 在创建文章的时候，创建对应该文章的新的评论区
        /// 2019/5/11
        /// </summary>
        /// <param name="article"></param>
        /// 受保护的资源
        [Authorize]
        public async Task<bool> CreateCommentArea([FromBody] ArticleIdDto articleId)
        {
            string articleIdStr = articleId.ArticleId.ToString();

            //判断数据库中是否存在该文章信息
            Article article = await _articleRepository.FirstOrDefaultAsync(articleId.ArticleId);

            if (article == null)
            {
                _logger.LogError("文章不存在，创建评论区失败");
                return false;
            }
            //判断是否已存在评论区，直接返回
            var commentArea = await _commentRepository.GetCommentAreaByIdAsync(articleIdStr);
            if (commentArea != null)
            {
                _logger.LogError("评论区已经存在，创建评论区失败");
                return false;
            }
            ////创建对应文章Id的评论区
            return await _commentRepository.CreateCommentsAreaAsync(articleIdStr);
        }

        /// <summary>
        /// 用户写评论
        /// 2019/5/11
        /// </summary>
        /// <returns></returns>
        public async Task<OutputUserCommentDto> CreateComment([FromBody]UserCommentDto userCommentDto)
        {
            string articleId = userCommentDto.ArticleId;

            //查询评论区
            var commentArea = await _commentRepository
                .GetCommentAreaByIdAsync(articleId);

            //如果评论区不存在，直接返回false
            if (commentArea == null)
            {
                _logger.LogError("评论区不存在，用户写评论失败");
                return null;
            }

            //如果评论区存在，写入评论(1级评论)
            //AutoMapper映射
            UserComment userComment = userCommentDto.MapTo<UserComment>();

            //如果留言成功,会返回留言内容
            UserComment successCommentContent = await _commentRepository.AddCommentToAreaAsync(userComment, articleId);

            //向客户端返回留言内容
            return successCommentContent.MapTo<OutputUserCommentDto>();
        }

        /// <summary>
        /// 根据文章Id获取该文章对应的评论列表
        /// 2019/5/11
        /// </summary>
        /// <param name="articleId">文章Id</param>
        /// <returns>评论列表</returns>
        public async Task<List<OutputUserCommentDto>> GetCommentList(string articleId)
        {
            var comments = await _commentRepository.GetCommentListAsync(articleId);
            return comments.MapTo<List<OutputUserCommentDto>>();
        }

        /// <summary>
        /// 评论点赞
        /// 2019/5/12
        /// </summary>
        /// <param name="articleId">文章Id</param>
        /// <param name="floor">楼层数</param>
        /// <param name="likes">点赞数</param>
        /// <returns></returns>
        public async Task<int> PostLikes([FromBody] CommentAdmireDto commentAdmireDto)
        {
            return await _commentRepository
                .AddAdmireAsync(commentAdmireDto.ArticleId, commentAdmireDto.Floor, commentAdmireDto.Likes);
        }

        /// <summary>
        /// 删除评论
        /// 2019/5/12
        /// </summary>
        /// <param name="articleId">文章Id</param>
        /// <param name="floor">楼层数</param>
        /// <returns>删除是否成功</returns>
        /// 受保护的资源
        [Authorize]
        [HttpDelete("/api/services/app/Comment/PostRemoveComment/{articleId}/{floor}")]
        public async Task<bool> RemoveComment(string articleId, string floor)
        {
            return await _commentRepository.DeleteCommentAsync(articleId, floor);
        }
    }
}
