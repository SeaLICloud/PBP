using System.ComponentModel.DataAnnotations;
using PBP.Web.Common;
using PBP.Web.Models.Common;

namespace PBP.Web.Models.Domain
{
    public class Account : Entity<Account>
    {
        /// <summary>
        /// 用户名称
        /// </summary>
        [FRequired(CKey.UNNOTNULL)]
        public string UserName { get; set; }

        /// <summary>
        /// 用户密码
        /// </summary>
        [FRequired(CKey.PDNOTNULL)]
        public string Password { get; set; }

        /// <summary>
        /// 用户角色
        /// </summary>
        public UserRole Role { get; set; }

        /// <summary>
        /// 用户邮箱
        /// </summary>
        [FRequired(CKey.EMNOTNULL)]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }


    public enum UserRole
    {
        [Display(Name = "管理员")]
        Admin,

        [Display(Name = "党员用户")]
        Ordinary
    }

}