using System.ComponentModel.DataAnnotations;
using PBP.Web.Models.Common;

namespace PBP.Web.Models.Domain
{
    public class PartyCost:Entity<PartyCost>
    {
        /// <summary>
        /// 党费ID
        /// </summary>
        public string PartyCostID { get; set; }
        /// <summary> 
        /// 党员ID 
        /// </summary>
        public string PartyMemberID { get; set; }
        /// <summary>
        /// 岗位工资
        /// </summary>
        public decimal PostWage { get; set; }
        /// <summary>
        /// 薪级工资
        /// </summary>
        public decimal SalaryRankWage { get; set; }
        /// <summary>
        /// 津贴补贴
        /// </summary>
        public decimal Allowance { get; set; }
        /// <summary>
        /// 绩效工资
        /// </summary>
        public decimal PerformanceWage { get; set; }
        /// <summary>
        /// 工会会费
        /// </summary>
        public decimal UnionCost{ get; set; }
        /// <summary>
        /// 医疗保险
        /// </summary>
        public decimal MedicalInsurance { get; set; }
        /// <summary>
        /// 失业保险
        /// </summary>
        public decimal UnemploymentInsurance { get; set; }
        /// <summary>
        /// 养老保险
        /// </summary>
        public decimal OldAgeInsurance { get; set; }
        /// <summary>
        /// 职业年金
        /// </summary>
        public decimal JobAnnuity { get; set; }
        /// <summary>
        /// 个人所得税
        /// </summary>
        public decimal IndividualIncomeTax { get; set; }
        /// <summary>
        /// 审核状态
        /// </summary>
        public Verify State { get; set; }
    }

    public enum Verify
    {
        [Display(Name = "未审核")]
        Unaudited,

        [Display(Name = "已审核")]
        Audited
    }
}
