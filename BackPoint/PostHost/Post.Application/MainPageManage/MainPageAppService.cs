using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Runtime.Caching;
using Microsoft.EntityFrameworkCore;
using Post.Application.MainPageManage.Dtos;
using Post.Application.MainPageManage.Dtos.MessageDto;
using Post.Core;
using Post.Core.ArticleManager.ArticleEntity;
using Post.Core.MessageManager.LeaveMessageEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Post.Application.MainPageManage
{
    public class MainPageAppService : PostAppServiceBase, IMainPageAppService
    {
        //注入仓储
        private readonly IRepository<Article> _articleRepository;
        private readonly IRepository<LeaveMessage> _leavemessageRepository;
        private readonly ICacheManager _cacheManager;

        public MainPageAppService(IRepository<Article> articleRepository,
            IRepository<LeaveMessage> leavemessageRepository,
            ICacheManager cacheManager)
        {
            _articleRepository = articleRepository;
            _leavemessageRepository = leavemessageRepository;
            _cacheManager = cacheManager;
        }

        /// <summary>
        /// 获取首页信息
        /// 2019/5/8
        /// </summary>
        /// <returns></returns>
        public async Task<MainPageDto> GetMainPageAsync()
        {
            //从缓存读取访客数，如果不存在则为1
            var viewCount = await _cacheManager.GetCache(PostConsts.RedisForViewerStore).GetAsync(PostConsts.ViewerCountKey, _ => Task.FromResult(1));
            //访客数加1
            viewCount += 1;
            //更新
            await _cacheManager.GetCache(PostConsts.RedisForViewerStore).SetAsync(PostConsts.ViewerCountKey, viewCount);

            //考虑到一个人的文章不会太多，所以先把文章全部加载到内存
            var articles = await _articleRepository.GetAll()
                .AsNoTracking()//不跟踪实体
                .ToListAsync();

            int articleCount = articles.Count;

            //获取近一周的前10篇文章
            List<LatestArticlesDto> latestArticles = articles
                .Where(t => t.CreationTime > DateTime.Now.AddDays(-7))
                .OrderByDescending(t=>t.CreationTime)
                .Take(10)
                .MapTo<List<LatestArticlesDto>>();

            //获取最热文章，根据文章阅读量从高到低排序并获取前10篇文章
            List<HotArticlesDto> hotArticles = articles
                .OrderByDescending(o => o.PageView)
                .Take(10)
                .MapTo<List<HotArticlesDto>>();

            //获取作者推荐的前6篇文章
            List<RecommendArticlesDto> recommendArticles = articles
                .Where(a => a.IsRecommend == "true")
                .OrderByDescending(a => a.CreationTime)
                .Take(6)
                .MapTo<List<RecommendArticlesDto>>();

            //获取留言总数
            int leavemessageCount = await _leavemessageRepository.CountAsync();

            //获取最近20条留言
            List<LeaveMessage> leaveMessages = await _leavemessageRepository
                .GetAll()
                .AsNoTracking()
                .OrderByDescending(l => l.CreationTime)
                .Take(6)
                .ToListAsync();

            return new MainPageDto()
            {
                articleCount = articleCount,
                latestArticlesDtos = latestArticles,
                hotArticlesDtos = hotArticles,
                recommendArticlesDtos = recommendArticles,
                leaveMessageCount = leavemessageCount,
                leaveMessageDtos = leaveMessages.MapTo<List<LeaveMessageDto>>(),
                viewerCount = viewCount
            };
        }
    }
}
