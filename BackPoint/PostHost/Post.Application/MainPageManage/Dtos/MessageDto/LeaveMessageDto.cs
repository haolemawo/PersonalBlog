using System;
using System.Collections.Generic;
using System.Text;

namespace Post.Application.MainPageManage.Dtos.MessageDto
{
    public class LeaveMessageDto
    {
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 留言信息
        /// </summary>
        public string MessageContent { get; set; }

        /// <summary>
        /// 用户头像路径
        /// </summary>
        public string UserAvator { get; set; }
    }
}
