using Abp.Configuration.Startup;
using Abp.Modules;
using Abp.Reflection.Extensions;
using Abp.Runtime.Caching.Redis;
using Castle.MicroKernel.Registration;
using Post.Core.CommentManager.ICommentsRepository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Post.Core
{
    [DependsOn(typeof(AbpRedisCacheModule))]
    public class PostCoreModule : AbpModule
    {
        public override void PreInitialize()
        {
            //配置Redis路径
            Configuration.Caching.UseRedis(options =>
            {
                //配置Redis路径
                options.ConnectionString = PostConsts.RedisUrl;
            });

            //配置某个Redis数据库的缓存超时时间
            Configuration.Caching.Configure(PostConsts.RedisForArticleStore, cache =>
             {
                 //缓存过期时间3天
                 cache.DefaultAbsoluteExpireTime = TimeSpan.FromDays(365); 
             });

            Configuration.Caching.Configure(PostConsts.RedisForViewerStore, cache =>
            {
                //缓存过期时间365天
                cache.DefaultAbsoluteExpireTime = TimeSpan.FromDays(365);
            });
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(PostCoreModule).GetAssembly());
        }
    }
}
