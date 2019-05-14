using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.Text;

namespace Post.Core.MessageManager.LeaveMessageEntity
{
    public class LeaveMessage : Entity, IHasCreationTime
    {
        /// <summary>
        /// 留言时间
        /// </summary>
        public DateTime CreationTime { get; set; }

        /// <summary>
        /// 留言用户
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 消息内容
        /// </summary>
        public string MessageContent { get; set; }

        /// <summary>
        /// 用户头像路径
        /// </summary>
        public string UserAvator { get; set; }
    }
}
