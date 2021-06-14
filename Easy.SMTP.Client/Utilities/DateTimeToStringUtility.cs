using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Easy.SMTP.Client.Utilities
{
    public static class DateTimeToStringUtility
    {
        public static string AddDateTime(string text)
        {
            return string.Format(" {0:yyy/MM/dd HH:mm:ss}", DateTime.Now).ToString() + " " + text;
        }
            
    }
}
