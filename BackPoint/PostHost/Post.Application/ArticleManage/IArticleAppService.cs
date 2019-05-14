using Abp.Application.Services.Dto;
using Microsoft.AspNetCore.Mvc;
using Post.Application.ArticleManage.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Post.Application.ArticleManage
{
    public interface IArticleAppService
    {
        //接口负责文章的查看、发布、修改等

        //上传新的文章
        Task<string> PostArticle([FromBody] NewArticleDto newArticleDto);

        //查询文章列表
        Task<PagedResultDto<ArticlesDto>> GetArticles(GetArticleInputDto input);

        //查询文章类型列表
        Task<List<ArticleTypeDto>> GetArticleType();
    }
}
