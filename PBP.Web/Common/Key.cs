namespace PBP.Web.Common
{
    public static class Key
    {
        public const string DefaultPwd = "123456";
        public const int PageSize = 8;
        public const string CurrentSort = "CurrentSort";
        public const string NameSortParm = "NameSortParm";
        public const string NameDesc = "name_desc";
        public const string DateSortParm = "DateSortParm";
        public const string Date = "Date";
        public const string DateDesc = "Date_desc";
        public const string Total = "Total";
        public const string CurrentPage = "CurrentPage";
        public const string CurrentFilter = "CurrentFilter";
        public static string CurrentUser = "CurrentUser";
        public static string CurrentRole = "CurrentRole";
        public static string CurrentEmail = "CurrentRole";
        public const string Admin = "Admin";
        public const string OrgPre = "OG";
        public const string PMPre = "PM";
        public const string PCPre = "PC";
        public const string CurrentPartyMember = "CurrentPartyMember";
        public const string Amount = "Amount";
    }

    public static class CKey
    {
        public const string UNNOTNULL = "注：账户名称不能为空！";
        public const string PDNOTNULL = "注：账户密码不能为空！";
        public const string EMNOTNULL = "注：账户邮箱不能为空！";
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
    public static class PKey
    {
        public const string PMPram = 
            "PartyMemberID,Name,Sex,IDCard,Birthday,National,Phone,Adress,Stage,BeginDate,PrepareDate,FormalDate,OrgID,State,Guid,CreateTime,UpdateTime";
        public const string PCPram =
            "PartyCostID,PartyMemberID,PostWage,SalaryRankWage,Allowance,PerformanceWage,UnionCost,MedicalInsurance,UnemploymentInsurance,OldAgeInsurance,JobAnnuity,IndividualIncomeTax,State,CostBase,Payable,Guid,CreateTime,UpdateTime,";
        public const string PCRPram =
            "PartyMemberID,PartyMemberName,PartyCostID,PayTime,PayFunc,PayAmount,BeginTime,EndTime,State,Guid,CreateTime,UpdateTime";
        public const string LFPram =
            "Name,Description,Address,Person,Time,State,FoundPerson,FoundTime,FoundAddress,Guid,CreateTime,UpdateTime";
    }
}
