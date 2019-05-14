﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Post.Application.MainPageManage.Dtos
{
    public class LatestArticlesDto
    {
        /// <summary>
        /// 文章Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 作者名
        /// </summary>
        public string Author { get; set; }

        /// <summary>
        /// 文章头像
        /// </summary>
        public string Avator { get; set; }

        /// <summary>
        /// 文章名
        /// </summary>
        public string ArticleName { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public string CreationTime { get; set; }
    }
}
