using PBP.Web.Models.Common;

namespace PBP.Web.Models.Domain
{
    public class AccountPartyMember:Entity<AccountPartyMember>
    {
        /// <summary>
        /// 账户主键
        /// </summary>
        public string AccountID { get; set; }

        /// <summary>
        /// 党员主键
        /// </summary>
        public string PartyMemberID { get; set; }
    }
}
