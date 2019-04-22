using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PBP.Web.Common
{
    public class SeriaNumber
    {
        public string Seria(int number)
        {
            if (number == 0)
            {
                return "001";
            }
            else if (number < 10 && number > 0)
            {
                return "00" + number;
            }

            else if (number < 100 && number > 9)
            {
                return "0" + number;
            }
            else
            {
                return number.ToString();
            }
        }
    }
}
