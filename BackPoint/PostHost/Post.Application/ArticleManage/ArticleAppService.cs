using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.AutoMapper;
using Abp.Linq.Extensions;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Post.Application.ArticleManage.Dtos;
using Post.Core.ArticleManager.ArticleEntity;
using Post.Core.Tools;
using System.Linq.Dynamic.Core;
using Abp.Application.Services.Dto;
using System.IO;
using Abp.Runtime.Caching;
using Post.Core.ArticleManager.ArticleTypeEntity;
using Post.Core;
using Post.Core.CommentManager.ICommentsRepository;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;

namespace Post.Application.ArticleManage
{
    public class ArticleAppService : PostAppServiceBase, IArticleAppService
    {
        private readonly IRepository<Article> _articleRepository;
        private readonly IRepository<ArticleType> _articleTypeRepository;
        private readonly ICommentRepository _commentRepository;
        private readonly ICacheManager _cacheManager;
        private readonly ILogger _logger;

        public ArticleAppService(IRepository<Article> articleRepository,
            IRepository<ArticleType> articleTypeRepository,
            ICommentRepository commentRepository,
            ICacheManager cacheManager,
            ILoggerFactory loggerFactory)
        {
            _articleRepository = articleRepository;
            _articleTypeRepository = articleTypeRepository;
            _commentRepository = commentRepository;
            _cacheManager = cacheManager;
            _logger = loggerFactory.CreateLogger<ArticleAppService>();
        }

        /// <summary>
        /// 博客管理者上传博客
        /// 2019/5/8
        /// </summary>
        /// <param name="newArticleDto">展现层Model，新的文章内容</param>
        /// <returns></returns>
        /// 需要身份验证
        [Authorize]
        public async Task<string> PostArticle([FromBody] NewArticleDto newArticleDto)
        {
            //获取到文章后，首先将文章内容存储到对应路径的硬盘上
            //文章内容以test-editormd-markdown-doc=开头，格式为Json字符串
            //切割文章内容
            string splitString = SplitString.SplitStringWithStart(newArticleDto.ArticleContent);

            if (splitString.IsNullOrWhiteSpace())
            {
                _logger.LogError("文章内容为空");
                return "文章内容为空";
            }

            var result = await FileOperate.StoreToTxtFileAsync(splitString, newArticleDto.ArticleName, _logger);

            if (result != "")
            {
                //不为空，表示存储成功，返回了存储路径
                newArticleDto.ArticleUrl = result;
                
                //AutoMapper映射
                Article newArticle = newArticleDto.MapTo<Article>();

                //默认浏览次数为1
                newArticle.PageView = 1;              

                //存储数据库
                try
                {
                    //存入数据库，并获取文章Id
                    var ArticleId =  await _articleRepository.InsertAndGetIdAsync(newArticle);
                    //将该文章Id和RedisArticle组合，作为Redis键名
                    string redisArticleKey = PostConsts.ArticleBaseKey + ArticleId;
                    //存入Redis缓存
                    await _cacheManager.GetCache(PostConsts.RedisForArticleStore).SetAsync(redisArticleKey, newArticle);
                    return ArticleId.ToString();
                }
                catch (Exception ex)
                {
                    _logger.LogError(exception: ex, "错误信息：" + ex.Message);
                    return "发布文章出现异常";
                }
            }
            else {
                _logger.LogError("存储文章到硬盘时出现错误");
                return "发布文章失败";
            }
        }

        /// <summary>
        /// 获取文章列表
        /// 2019/5/9
        /// </summary>
        /// <param name="currentPage">当前页</param>
        /// <param name="pageSize">每页数目</param>
        /// <param name="type">文章类型</param>
        /// <returns>分页排序后的文章列表</returns>
        public async Task<PagedResultDto<ArticlesDto>> GetArticles(GetArticleInputDto input)
        {
            //存储查询结果
            List<Article> articleList = new List<Article>();

            //分页查询文章
            var query = _articleRepository.GetAll()
                .AsNoTracking()
                .WhereIf(input.TypeId > 0,
                    a => a.TypeId == input.TypeId)
                .WhereIf(!input.IsRecommend.IsNullOrWhiteSpace(),
                    b => b.IsRecommend == input.IsRecommend);

            //文章总数
            var count = await query.CountAsync();

            //获取文章
            if (input.Sorting == "Id")
            {
                //默认按Id倒序排序
                 articleList = await query
                    .OrderByDescending(s => s.Id)
                    .PageBy(input)
                    .ToListAsync();
            }
            else {
                //按规定方式排序
                 articleList = await query
                .OrderBy(input.Sorting)
                .PageBy(input)
                .ToListAsync();
            }

            //AutoMapper
            var articleListDto = articleList.MapTo<List<ArticlesDto>>();
            return new PagedResultDto<ArticlesDto>(count, articleListDto);
        }

        /// <summary>
        /// 查看某篇文章内容
        /// 2019/5/9
        /// </summary>
        /// <param name="articleID"></param>
        /// <returns>文章信息</returns>
        public async Task<ArticleDetailDto> GetArticleDetail(int articleID)
        {
            Article article = null;
            //Id不能小于等于0
            if (articleID <= 0)
            {
                _logger.LogError("传入文章Id格式错误：" + articleID);
                return null;
            }

            //将该文章Id和RedisArticle组合，作为Redis键名
            string redisArticleKey = PostConsts.ArticleBaseKey + articleID;

            //存入Redis缓存
            //如果不存在，则执行查询数据库的操作
            article = (Article)await _cacheManager.GetCache(PostConsts.RedisForArticleStore).GetAsync(redisArticleKey, async _ => await _articleRepository
                     .FirstOrDefaultAsync(s => s.Id == articleID));

            //更新浏览量
            article.PageView++;
            await _cacheManager.GetCache(PostConsts.RedisForArticleStore).SetAsync(redisArticleKey, article);

            //浏览量每上涨20，更新数据库
            //可切换后台作业程序，一天同步一次数据
            if (article.PageView % 7 == 0)
            {
                await _articleRepository.UpdateAsync(article);
            }

            if (article == null)
            {
                _logger.LogError("查看文章不存在，查看失败");
                return null;
            }

            //获取文章的路径并提取文件内容、标题等信息
            //AutoMapper
            var articleDetailDto = article.MapTo<ArticleDetailDto>();
            //判断文件是否为txt文档
            if (Path.GetExtension(article.ArticleUrl) != ".txt")
            {
                _logger.LogError("文章格式不支持，查看失败");
                return null;
            }

            string contentResult = await FileOperate.GetContentByPathAsync(article.ArticleUrl, _logger);

            if (contentResult == "false")
            {
                _logger.LogError("从硬盘读取文章错误，查看失败");
                return null;
            }

            //在使用decodeURIComponent()函数时，所有的空格会变为加号，所以这里将加号变换为空格
            contentResult = contentResult.Replace("+", " ");

            articleDetailDto.Content = contentResult;
            return articleDetailDto;
        }

        /// <summary>
        /// 更新文章
        /// 一致性问题比较复杂，有待解决
        /// 2019/5/12
        /// </summary>
        /// <param name="updateArticleDto">更新的文档</param>
        /// <returns>更新结果</returns>
        /// 需要身份验证
        [Authorize]
        public async Task<bool> UpdateArticle(UpdateArticleDto updateArticleDto)
        {
            int articleId = updateArticleDto.ArticleId;

            //从数据库查询该Id的文章
            var article = await _articleRepository.FirstOrDefaultAsync(a => a.Id == articleId);
            if (article == null)
            {
                //文章不存在
                _logger.LogError("文章不存在，更新失败");
                return false;
            }

            //获取文章原本的完整的路径
            string completePath = article.ArticleUrl;
            //获取文章新的内容
            string newArticleContent = updateArticleDto.ArticleContent;
            //切割文章内容，去掉首字符串
            string articleContentNormal = SplitString.SplitStringWithStart(newArticleContent);

            //将新的内容更新至文件
            var result = await FileOperate.UpdateArticleContentAsync(completePath, articleContentNormal, _logger);

            if (result == false)
            {
                //更新硬盘文档失败
                _logger.LogError("从硬盘更新文章失败，更新失败");
                return false;
            }

            //硬盘上的文件更新成功，更新数据库
            article.ArticleName = updateArticleDto.ArticleName;
            article.ArticleTags = updateArticleDto.ArticleTags;
            article.TypeId = updateArticleDto.TypeId;
            article.LastModificationTime = updateArticleDto.LastModificationTime;
            article.Introduce = updateArticleDto.Introduce;
            article.IsRecommend = updateArticleDto.IsRecommend;

            //更新数据库
            Article updateMysqlResult = await _articleRepository.UpdateAsync(article);

            if (updateMysqlResult == null)
            {
                //更新数据库失败，可以尝试重试机制
                _logger.LogError("数据库更新失败，更新失败");
                return false;
            }
            //更新成功，更新缓存
            //将该文章Id和RedisArticle组合，作为Redis键名
            string redisArticleKey = PostConsts.ArticleBaseKey + articleId;
            await _cacheManager.GetCache(PostConsts.RedisForArticleStore).SetAsync(redisArticleKey, updateMysqlResult);

            return true;
        }

        /// <summary>
        /// 删除文章
        /// 2019/5/13
        /// </summary>
        /// <param name="articleId">文章Id</param>
        /// <returns>删除结果</returns>
        /// 需要身份验证
        [Authorize]
        [HttpDelete("/api/services/app/Article/DeleteArticle/{articleId}")]
        public async Task<bool> DeleteArticle(int articleId)
        {
            //文章Id必须要符合规范
            if (articleId <= 0)
            {
                _logger.LogError("传入文章Id不合规范");
                return false;
            }

            //首先删除Redis缓存中的记录
            string redisArticleKey = PostConsts.ArticleBaseKey + articleId;
            await _cacheManager
                .GetCache(PostConsts.RedisForArticleStore)
                .RemoveAsync(redisArticleKey);

            //获取指定文章
            Article article = await _articleRepository
                .FirstOrDefaultAsync(a => a.Id == articleId);

            if (article != null)
            {
                //删除数据库中的记录
                await _articleRepository.DeleteAsync(article);
            }

            //删除指定路径的存储在硬盘上的文章
            await FileOperate.DeleteArticleAsync(article.ArticleUrl, _logger);

            //删除评论区
            await _commentRepository.DeleteCommentAreaAsync(articleId.ToString());

            return true;
        }

        /// <summary>
        /// 获取文章类型列表
        /// 2019/5/10
        /// </summary>
        /// <returns></returns>
        public async Task<List<ArticleTypeDto>> GetArticleType()
        {
            var typeList = await _articleTypeRepository.GetAll()
                .AsNoTracking()
                .ToListAsync();
            return typeList.MapTo<List<ArticleTypeDto>>();
        }
    }
}
