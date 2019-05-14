using AutoMapper;
using Post.Application.ArticleManage.Dtos;
using Post.Application.CommentManager.Dtos;
using Post.Application.MainPageManage.Dtos;
using Post.Application.MainPageManage.Dtos.MessageDto;
using Post.Core.ArticleManager.ArticleEntity;
using Post.Core.ArticleManager.ArticleTypeEntity;
using Post.Core.CommentManager.CommentEntity;
using Post.Core.MessageManager.LeaveMessageEntity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Post.Application
{
    public class MapperConfig
    {
        public static void CreateMappings(IMapperConfigurationExpression configuration)
        {
            //Make Mapper Configuration
            configuration.CreateMap<Article, LatestArticlesDto>();
            configuration.CreateMap<Article, HotArticlesDto>();
            configuration.CreateMap<LeaveMessage, LeaveMessageDto>();
            configuration.CreateMap<NewArticleDto, Article>();
            configuration.CreateMap<Article, ArticleDetailDto>();
            configuration.CreateMap<ArticleType, ArticleTypeDto>();
            configuration.CreateMap<UserCommentDto, UserComment>();
            configuration.CreateMap<UserComment, OutputUserCommentDto>();
        }
    }
}
