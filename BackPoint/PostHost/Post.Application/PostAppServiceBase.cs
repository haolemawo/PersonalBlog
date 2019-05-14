using Abp.Application.Services;
using Post.Core;

namespace Post.Application
{
    /// <summary>
    /// 将该类作为应用层继承的基类，可以自动发现它的路由
    /// </summary>
    public class PostAppServiceBase : ApplicationService
    {
        protected PostAppServiceBase()
        {
            LocalizationSourceName = PostConsts.LocalizationSourceName;
        }
    }
}
