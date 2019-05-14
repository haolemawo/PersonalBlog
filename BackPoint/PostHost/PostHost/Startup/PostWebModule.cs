using Abp.AspNetCore;
using Abp.Modules;
using Abp.Reflection.Extensions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Post.Core;
using Post.Core.Configuration;
using Post.Application;
using Abp.AspNetCore.Configuration;
using Post.EntityFrameworkCore.EntityFrameworkCore;

namespace PostHost.Startup
{
    [DependsOn(
        typeof(PostApplicationModule),
        typeof(PostEntityFrameworkCoreModule),
        typeof(AbpAspNetCoreModule))]
    public class PostWebModule : AbpModule
    {
        private readonly IConfigurationRoot _appConfiguration;

        public PostWebModule(IHostingEnvironment env)
        {
            _appConfiguration = AppConfigurations.Get(env.ContentRootPath, env.EnvironmentName);
        }

        public override void PreInitialize()
        {
            Configuration.DefaultNameOrConnectionString = _appConfiguration.GetConnectionString(PostConsts.ConnectionStringName);

            //配置接口路由
            Configuration.Modules.AbpAspNetCore()
                .CreateControllersForAppServices(
                    typeof(PostApplicationModule).GetAssembly());
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(PostWebModule).GetAssembly());
        }
    }
}
