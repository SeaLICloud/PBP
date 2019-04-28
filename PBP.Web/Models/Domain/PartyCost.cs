using System;
using System.ComponentModel.DataAnnotations;
using PBP.Web.Models.Common;

namespace PBP.Web.Models.Domain
{
    public class PartyCost : Entity<PartyCost>
    {
        public PartyCost()
        {
        }

        public PartyCost(string partyCostID, string partyMemberID)
        {
            PartyCostID = partyCostID;
            PartyMemberID = partyMemberID;
        }

        /// <summary>
        ///     党费ID
        /// </summary>
        public string PartyCostID { get; set; }

        /// <summary>
        ///     党员ID
        /// </summary>
        public string PartyMemberID { get; set; }

        /// <summary>
        ///     岗位工资
        /// </summary>
        public decimal PostWage { get; set; }

        /// <summary>
        ///     薪级工资
        /// </summary>
        public decimal SalaryRankWage { get; set; }

        /// <summary>
        ///     津贴补贴
        /// </summary>
        public decimal Allowance { get; set; }

        /// <summary>
        ///     绩效工资
        /// </summary>
        public decimal PerformanceWage { get; set; }

        /// <summary>
        ///     工会会费
        /// </summary>
        public decimal UnionCost { get; set; }

        /// <summary>
        ///     医疗保险
        /// </summary>
        public decimal MedicalInsurance { get; set; }

        /// <summary>
        ///     失业保险
        /// </summary>
        public decimal UnemploymentInsurance { get; set; }

        /// <summary>
        ///     养老保险
        /// </summary>
        public decimal OldAgeInsurance { get; set; }

        /// <summary>
        ///     职业年金
        /// </summary>
        public decimal JobAnnuity { get; set; }

        /// <summary>
        ///     个人所得税
        /// </summary>
        public decimal IndividualIncomeTax { get; set; }

        /// <summary>
        ///     审核状态
        /// </summary>
        public Verify State { get; set; }

        /// <summary>
        ///     党费计算基数
        /// </summary>
        public decimal CostBase { get; set; }

        /// <summary>
        ///     缴纳党费标准
        /// </summary>
        public decimal Payable { get; set; }


        public decimal Pay()
        {
            if (CostBase <= 3000) return CostBase * Convert.ToDecimal(0.005);
            if (CostBase <= 5000 && CostBase > 3000) return CostBase * Convert.ToDecimal(0.01);
            if (CostBase <= 10000 && CostBase > 5000) return CostBase * Convert.ToDecimal(0.015);
            return CostBase * Convert.ToDecimal(0.01);
        }

        public decimal Base()
        {
            return PostWage + SalaryRankWage + Allowance + PerformanceWage -
                   (UnionCost + MedicalInsurance + UnemploymentInsurance +
                    OldAgeInsurance + JobAnnuity + IndividualIncomeTax);
        }
    }

    public enum Verify
    {
        [Display(Name = "未审核")] Unaudited,

        [Display(Name = "已审核")] Audited
    }
}