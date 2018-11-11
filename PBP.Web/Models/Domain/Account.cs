using PBP.Web.Models.Common;

namespace PBP.Web.Models.Domain
{
    public class Account : Entity<Account>
    {
        /// <summary>
        ///     用户名称
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        ///     用户密码
        /// </summary>
        public string Password { get; set; }
    }
}