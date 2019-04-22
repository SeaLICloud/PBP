using System;
using PBP.Web.Models.Common;

namespace PBP.Web.Models.Domain
{
    public class PartyMember:Entity<PartyMember>
    {
        /// <summary>
        /// 党员Id
        /// </summary>
        public string PartyMemberID { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        public string Sex { get; set; }
        /// <summary>
        /// 身份证号
        /// </summary>
        public string IDCard { get; set; }
        /// <summary>
        /// 出生年月
        /// </summary>
        public DateTime Birthday { get; set; }
        /// <summary>
        /// 民族
        /// </summary>
        public string National { get; set; }
        /// <summary>
        /// 手机号码
        /// </summary>
        public string Phone { get; set; }
        /// <summary>
        /// 地址
        /// </summary>
        public string Adress { get; set; }
        /// <summary>
        /// 发展阶段
        /// </summary>
        public string Stage { get; set; }
        /// <summary>
        /// 申请入党日期
        /// </summary>
        public DateTime BeginDate { get; set; }
        /// <summary>
        /// 成为预备党员日期
        /// </summary>
        public DateTime PrepareDate { get; set; }
        /// <summary>
        /// 成为正式党员日期
        /// </summary>
        public DateTime FormalDate { get; set; }
        /// <summary>
        /// 所属组织Id
        /// </summary>
        public string OrgID { get; set; }
    }
}
