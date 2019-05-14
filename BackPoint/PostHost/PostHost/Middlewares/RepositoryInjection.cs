using Microsoft.Extensions.DependencyInjection;
using Post.Core.CommentManager.ICommentsRepository;
using Post.EntityFrameworkCore.Comments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PostHost.Middlewares
{
    public static class RepositoryInjection
    {
        /// <summary>
        /// 为外部提供访问服务的接口
        /// </summary>
        public static IServiceProvider GetServiceProvider { get; private set; }

        public static void AddRepositories(this IServiceCollection services)
        {
            //依赖注入---MongoDB
            services.AddTransient<ICommentRepository, CommentRepository>();

            GetServiceProvider = services.BuildServiceProvider();
        }
    }
}
