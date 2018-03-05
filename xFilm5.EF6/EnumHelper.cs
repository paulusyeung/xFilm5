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

        public enum PrintQSubitemType
        {
            Ps,             // 收到 ps
            Vps,            // 有 vps
            Tiff,           // 有 tiff
            Cip3,           // 有 cip3
            Blueprint,      // 有 藍紙
            Plate,          // 有 鋅
            Order,          // 落咗荷打
            Receipt,        // 收咗貸
            Invoice,        // 開咗單
            Film            // 2017 追加
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
                OnReady_KF,
                OnOrder = 30,
                OnVps,
                OnReady,
                OnCompleted
            }
        }
    }
}
