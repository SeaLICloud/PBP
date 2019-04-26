using System.Text;
using Microsoft.EntityFrameworkCore.Internal;

namespace PBP.Web.Common
{
    public class Parameter
    {
        public static string Param<T>(T @class)
        {
            var result = new StringBuilder();
            if (@class == null)
            {
                return string.Empty;
            }

            var properties = @class.GetType().GetProperties(
                System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public);

            if (properties.Length <= 0)
            {
                return string.Empty;
            }

            var count = properties.Length;

            foreach (var item in properties)
            {
                result.Append(item.Name);
                if (properties.IndexOf(item) <= count - 1) 
                {
                    result.Append(",");
                }
            }
            return result.ToString();
        }

    }
}
