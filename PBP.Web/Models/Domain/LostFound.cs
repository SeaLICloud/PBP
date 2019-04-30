using System;
using System.ComponentModel.DataAnnotations;
using PBP.Web.Models.Common;

namespace PBP.Web.Models.Domain
{
    public class LostFound:Entity<LostFound>
    {
        /// <summary>
        /// 物品名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 物品描述
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 拾取处
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// 拾取人
        /// </summary>
        public string Person { get; set; }

        /// <summary>
        /// 拾取时间
        /// </summary>
        [DataType(DataType.Date)]
        public DateTime Time { get; set; }

        /// <summary>
        /// 招领处
        /// </summary>
        public string FoundAddress { get; set; }

        /// <summary>
        /// 领取人
        /// </summary>
        public string FoundPerson { get; set; }

        /// <summary>
        /// 领取时间
        /// </summary>
        [DataType(DataType.Date)]
        public DateTime FoundTime { get; set; }

        /// <summary>
        /// 招领状态
        /// </summary>
        public Lost State { get; set; }

        public enum  Lost
        {
            [Display(Name = "待领取")]
            OreadyLost,
            [Display(Name = "已领取")]
            OreadyFound
        }
    }
}
