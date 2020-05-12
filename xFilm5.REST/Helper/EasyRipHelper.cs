using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using xFilm5.EF6;

namespace xFilm5.REST.Helper
{
    public static class EasyRipHelper
    {
        public static int PrasePlatform(string source)
        {
            int result = 0;

            switch (source.ToLower())
            {
                case "windows":
                    result = (int)EnumHelper.FCM.DevicePlatform.Windows;
                    break;
                case "mac os":
                    result = (int)EnumHelper.FCM.DevicePlatform.macOS;
                    break;
                case "ios":
                    result = (int)EnumHelper.FCM.DevicePlatform.iOS;
                    break;
                case "android":
                    result = (int)EnumHelper.FCM.DevicePlatform.Android;
                    break;
                case "default":
                    result = (int)EnumHelper.FCM.DevicePlatform.Unknown;
                    break;
            }

            return result;
        }
    }
}