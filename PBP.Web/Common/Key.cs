namespace PBP.Web.Common
{
    public static class Key
    {
        public const string DefaultPwd = "123456";
        public const int PageSize = 8;
    }

    public static class CKey
    {
        public const string UNNOTNULL = "注：账户名称不能为空！";
        public const string PDNOTNULL = "注：账户密码不能为空！";
        public const string UDNOTNULL = "注：账户密码输入不正确!";
        public const string ACCOUNTEXIST = "注：该账户名已存在!";

        public const string ONAMENOTNULL = "注：组织名称不能为空！";
        public const string OSNAMENOTNULL = "注：组织简称不能为空！";
        public const string OTYPENOTNULL = "注：组织类型不能为空！";
    }
    public static class VKey
    {
        public const string LOGINFAILED = "LOGINFAILED";
        public const string ACCOUNTEXIST = "ACCOUNTEXIST";
    }
}
