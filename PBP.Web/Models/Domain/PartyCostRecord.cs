using System;
using System.ComponentModel.DataAnnotations;
using PBP.Web.Models.Common;

namespace PBP.Web.Models.Domain
{
    public class PartyCostRecord:Entity<PartyCostRecord>
    {
        /// <summary>
        /// 党员代码
        /// </summary>
        public string PartyMemberID { get; set; }

        /// <summary>
        /// 党员姓名
        /// </summary>
        public string PartyMemberName { get; set; }

        /// <summary>
        /// 党费代码
        /// </summary>
        public string PartyCostID { get; set; }

        /// <summary>
        /// 缴纳时间
        /// </summary>
        [DataType(DataType.Date)]
        public DateTime PayTime { get; set; }

        /// <summary>
        /// 支付方式
        /// </summary>
        public PayMethod PayFunc { get; set; }

        /// <summary>
        /// 支付金额
        /// </summary>
        public decimal PayAmount { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        [DataType(DataType.Date)]
        public DateTime BeginTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        [DataType(DataType.Date)]
        public DateTime EndTime { get; set; }

        /// <summary>
        /// 审核状态
        /// </summary>
        public Verify State { get; set; }

        public enum PayMethod
        {
            [Display(Name = "支付宝")] AliPay,

            [Display(Name = "微信")] WeChatPay
        }
    }
}
