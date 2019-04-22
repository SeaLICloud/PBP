using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using PBP.Web.Common;
using PBP.Web.Models.Common;

namespace PBP.Web.Models.Domain
{
    public class Organization:Entity<Organization>
    {
        /// <summary>
        /// 组织代码
        /// </summary>
        public string OrgID { get; set; }
        /// <summary>
        /// 组织名称
        /// </summary>
        [FRequired(CKey.ONAMENOTNULL)]
        public string Name { get; set; }
        /// <summary>
        /// 组织简称
        /// </summary>
        [FRequired(CKey.OSNAMENOTNULL)]
        public string ShortName { get; set; }
        /// <summary>
        /// 组织类型
        /// </summary>
        [FRequired(CKey.OTYPENOTNULL)]
        public OrgType OrgType { get; set; }
    }

    public enum OrgType
    {
        [Display(Name = "党委")]
        PartyCommittee,

        [Display(Name = "党总支部")]
        GeneralPartyBranch,

        [Display(Name = "党支部")]
        PartyBranch,

        [Display(Name = "党小组")]
        PartyGroup
    }
}
