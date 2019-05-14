using Post.Application.MainPageManage.Dtos.MessageDto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Post.Application.MainPageManage.Dtos
{
    public class MainPageDto
    {
        //文章
        public List<LatestArticlesDto> latestArticlesDtos = new List<LatestArticlesDto>();
        public List<HotArticlesDto> hotArticlesDtos = new List<HotArticlesDto>();
        public List<RecommendArticlesDto> recommendArticlesDtos = new List<RecommendArticlesDto>();

        //留言
        public List<LeaveMessageDto> leaveMessageDtos = new List<LeaveMessageDto>();

        //文章数
        public int articleCount = 0;

        //留言数
        public int leaveMessageCount = 0;

        //访客数
        public int viewerCount = 0;
    }
}
