using System;
using PBP.Web.Models.Common;

namespace PBP.Web.Models.Domain
{
    public class AccountPartyMember:Entity<AccountPartyMember>
    {
        public AccountPartyMember()
        {
        }

        public AccountPartyMember(string accountID,string partyMemberID)
        {
            AccountID = accountID;
            PartyMemberID = partyMemberID;
        }

        /// <summary>
        /// 账户名
        /// </summary>
        public string AccountID { get; set; }

        /// <summary>
        /// 党员代码
        /// </summary>
        public string PartyMemberID { get; set; }
    }
}
