using System;
using System.ComponentModel.DataAnnotations;
using PBP.Web.Models.Common;

namespace PBP.Web.Models.Domain
{
    public class ThreeSession:Entity<ThreeSession>
    {
        /// <summary>
        /// 类型
        /// </summary>
        public Session Type { get; set; }
        /// <summary>
        /// 主题
        /// </summary>
        public string Theme { get; set; }
        /// <summary>
        /// 主要内容
        /// </summary>
        public string PrimaryCoverage { get; set; }
        /// <summary>
        /// 应到人数
        /// </summary>
        public int ShouldArrive { get; set; }
        /// <summary>
        /// 实到人数
        /// </summary>
        public int TrueTo { get; set; }

        /// <summary>
        /// 时间
        /// </summary>
        [DataType(DataType.Date)]
        public DateTime Time { get; set; }

        /// <summary>
        /// 地点
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// 记录人
        /// </summary>
        public string Person { get; set; }

        public enum Session
        {
            [Display(Name = "党员大会")]
            PartyCongresses,
            [Display(Name = "支部委员会")]
            BranchCommittees,
            [Display(Name = "党小组会议")]
            PartyGroupMeetings,
            [Display(Name = "党课")]
            PartyLessons
        }
    }
}
