using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xFilm5.EF6
{
    public class EnumHelper
    {
        public class FCM
        {
            public enum DevicePlatform
            {
                Android,
                iOS,
                WindowsPhone,
                Windows,
                WindowsTablet,
                SurfaceHub,
                Xbox,
                IoT,
                Unknown,
                tvOS,
                watchOS,
                macOS
            }
        }

        public class User
        {
            public enum AuthType
            {
                Anonymous,
                Legacy,       // database: Name + Password
                PhoneNumber,  // SIM phone number
                Firebase,
                Google,
                Facebook
            }

            public enum NotifyType
            {
                None = 0,
                Everyone,
                StaffOnly,
                OnOrder_TW = 10,
                OnOrder_KT,
                OnOrder_KF,
                OnReady_TW = 20,
                OnReady_KT,
                OnReady_KF
            }
        }
    }
}
