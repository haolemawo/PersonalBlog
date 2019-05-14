using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;
using Post.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Post.Application
{
    [DependsOn(
        typeof(PostCoreModule),
        typeof(AbpAutoMapperModule))]
    public class PostApplicationModule : AbpModule
    {
        public override void PreInitialize()
        {
            //配置AutoMapper映射
            Configuration.Modules
                .AbpAutoMapper()
                .Configurators
                .Add(configuration =>
                {
                    MapperConfig.CreateMappings(configuration);
                });
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(PostApplicationModule).GetAssembly());
        }
    }
}
