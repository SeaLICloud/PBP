using System;
using System.ComponentModel.DataAnnotations;
using PBP.Web.Models.Common;

namespace PBP.Web.Models.Domain
{
    public class Suggestion:Entity<Suggestion>
    {
        /// <summary>
        /// 发送人邮箱
        /// </summary>
        public string SendEmail { get; set; }
        /// <summary>
        /// 接收人邮箱
        /// </summary>
        [DataType(DataType.EmailAddress)]
        public string AcceptEmail { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 发送时间
        /// </summary>
        [DataType(DataType.Date)]
        public DateTime SendTime { get; set; }
    }
}
