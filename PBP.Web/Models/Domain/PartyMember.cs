using System;
using System.ComponentModel.DataAnnotations;
using PBP.Web.Models.Common;

namespace PBP.Web.Models.Domain
{
    public class PartyMember:Entity<PartyMember>
    {
        /// <summary>
        /// 党员代码
        /// </summary>
        public string PartyMemberID { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        public PartyMemberSex Sex { get; set; }
        /// <summary>
        /// 身份证号
        /// </summary>
        public string IDCard { get; set; }
        /// <summary>
        /// 出生日期
        /// </summary>
        [DataType(DataType.Date)]
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
        public DevelopmentStage Stage { get; set; }
        /// <summary>
        /// 申请入党日期
        /// </summary>
        [DataType(DataType.Date)]
        public DateTime BeginDate { get; set; }
        /// <summary>
        /// 成为预备党员日期
        /// </summary>
        [DataType(DataType.Date)]
        public DateTime PrepareDate { get; set; }
        /// <summary>
        /// 成为正式党员日期
        /// </summary>
        [DataType(DataType.Date)]
        public DateTime FormalDate { get; set; }
        /// <summary>
        /// 所属组织代码
        /// </summary>
        public string OrgID { get; set; }

        public enum DevelopmentStage
        {
            [Display(Name = "未入党")]
            NotInput,

            [Display(Name = "积极分子")]
            Activist,

            [Display(Name = "预备党员")]
            Prepare,

            [Display(Name = "正式党员")]
            Formal
        }

        public enum PartyMemberSex
        {
            [Display(Name = "男")]
            Male,

            [Display(Name = "女")]
            FaMale
        }
    }
}
