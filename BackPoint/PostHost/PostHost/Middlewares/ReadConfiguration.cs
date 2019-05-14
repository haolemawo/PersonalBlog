using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Post.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PostHost.Middlewares
{
    public static class ReadConfiguration
    {
        /// <summary>
        /// 读取json文件中定义的配置信息，赋值到Core项目的Const类
        /// 2019/5/10
        /// </summary>
        /// <param name="services">配置集</param>
        /// <param name="_appConfiguration">扩展配置</param>
        public static void ReadConfigurations(this IServiceCollection services, IConfigurationRoot _appConfiguration)
        {
            //日志保存的基础路径
            PostConsts.BaseTrail = _appConfiguration["Post:BaseUrl"];

            //Redis缓存路径
            PostConsts.RedisUrl = _appConfiguration["RedisCache:ConnectionStrings"];
            //Redis存储文章使用数据库名
            PostConsts.RedisForArticleStore = _appConfiguration["RedisCache:RedisForArticleStore"];
            //Redis存储访问量使用数据库名
            PostConsts.RedisForViewerStore = _appConfiguration["RedisCache:RedisForViewerStore"];
            //文章键+Id=Redis键
            PostConsts.ArticleBaseKey = _appConfiguration["RedisCache:RedisBaseKeyForArticle"];
            //浏览量键
            PostConsts.ViewerCountKey = _appConfiguration["RedisCache:RedisKeyForViewerCount"];

            //MongoDB路径
            PostConsts.MongoDBConnectionStr = _appConfiguration["MongoDB:ConnectionStrings"];
            //评论的MongoDB使用数据库名
            PostConsts.MongoDBForComment = _appConfiguration["MongoDB:MongoDBForComment"];
        }
    }
}
