namespace PBP.Web.Common
{
    public class SeriaNumber
    {
        public string Seria(int number,string pre)
        {
            if (number == 0)
            {
                return pre + "000001";
            }
            else if (number < 10 && number > 0)
            {
                return pre+ "00000" + number;
            }
            else if (number < 100 && number > 9)
            {
                return pre + "0000" + number;
            }
            else if(number < 1000 && number > 99)
            {
                return pre + "000" + number;
            }

            else if (number < 10000 && number > 999)
            {
                return pre + "00" + number;
            }
            else if (number < 100000 && number > 9999)
            {
                return pre + "0" + number;
            }
            else
            {
                return pre + number;
            }
        }
    }
}
