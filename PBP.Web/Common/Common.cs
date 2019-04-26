using System;
using System.Reflection;

namespace PBP.Web.Common
{
    public static class Common
    {
        public static string GetEnumDisplay(Enum e)
        {
            Type type = e.GetType();
            MemberInfo[] memInfo = type.GetMember(e.ToString());
            if (memInfo != null && memInfo.Length > 0)
            {
                object[] attrs = memInfo[0].
                    GetCustomAttributes(typeof(System.ComponentModel.DataAnnotations.DisplayAttribute), false);
                if (attrs != null && attrs.Length > 0)
                {
                    return ((System.ComponentModel.DataAnnotations.DisplayAttribute)attrs[0]).Name;
                }
            }
            return e.ToString();
        }
    }
}
