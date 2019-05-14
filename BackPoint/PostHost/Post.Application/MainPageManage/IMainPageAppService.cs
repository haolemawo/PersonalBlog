using Abp.Application.Services;
using Post.Application.MainPageManage.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Post.Application.MainPageManage
{
    public interface IMainPageAppService
    {
        Task<MainPageDto> GetMainPageAsync();
    }
}
