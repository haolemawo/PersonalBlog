using Abp.EntityFrameworkCore;
using Abp.Modules;
using Abp.Reflection.Extensions;
using Post.Core;

namespace Post.EntityFrameworkCore.EntityFrameworkCore
{
    [DependsOn(
        typeof(PostCoreModule),
        typeof(AbpEntityFrameworkCoreModule))]
    public class PostEntityFrameworkCoreModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(PostEntityFrameworkCoreModule).GetAssembly());
        }
    }
}
