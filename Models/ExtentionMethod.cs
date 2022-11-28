using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace NewZapures_V2.Models
{
    public static class ExtentionMethod
    {
        public static string ToNormalDateTime(this DateTime? value, int ShowTime = 0)
        {
            try
            {
                if (ShowTime == 0)
                {
                    return value.Value.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                }
                else
                    return value.Value.ToString("dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture);
            }
            catch
            {
                return "";
            }
        }
    }
}