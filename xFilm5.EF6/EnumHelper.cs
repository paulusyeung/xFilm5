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
                OnReady_KF,
                OnOrder = 30,
                OnVps,
                OnReady,
                OnCompleted
            }

            public enum UserRole
            {
                Customer = 1,
                Operator,
                Sales,
                Account,
                Admin,
                Workshop,
                Cashier
            }

            public enum UserType
            {
                Staff,
                Customer,
                Supplier
            }
        }

        public class Order
        {
            public enum OrderType
            {
                UploadFile = 1,
                DirectPrint,
                PsFile,
                Others,
                Plate,
                Plate5,
                Film5,
                Vps5
            }

            public enum Priority
            {
                Rush = 1,
                Express,
                Regular
            }

            public enum Workflow
            {
                Cancelled = 1,
                Queuing,
                Retouch,
                Printing,
                ProofingOutgoing,
                ProofingIncoming,
                Ready,
                Dispatch,
                Completed
            }

            public enum DeliveryMethod
            {
                PickUp = 1,
                DeliverTo
            }

            public enum PrintQSubitemType
            {
                Ps,             // 0 = 收到 ps
                Vps,            // 1 = 有 vps
                Tiff,           // 2 = 有 tiff
                Cip3,           // 3 = 有 cip3
                Blueprint,      // 4 = 有 藍紙
                Plate,          // 5 = 有 鋅
                Order,          // 6 = 落咗荷打
                Receipt,        // 7 = 收咗貸
                Invoice,        // 8 = 開咗單
                Film            // 9 = 2017 追加
            }
        }

        public class Common
        {
            public enum Status
            {
                Inactive = -1,
                Draft = 0,
                Active,
                Power
            }
        }
    }
}
