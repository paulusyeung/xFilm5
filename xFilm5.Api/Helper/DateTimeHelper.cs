﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xFilm5.Api.Helper
{
    public class DateTimeHelper
    {
        public static DateTime GetZeroDate()
        {
            return DateTime.Parse("1900-01-01 00:00:00.000");
        }

        public static DateTime GetDateTimeNow()
        {
            var format = "yyyy-MM-dd HH:mm:ss:fff";
            var stringDate = DateTime.Now.ToString(format);
            var convertedBack = DateTime.ParseExact(stringDate, format, CultureInfo.InvariantCulture);
            return convertedBack;
        }
    }
}
