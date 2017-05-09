using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xFilm5.QRStation
{
    public class Common
    {

        public class Enums
        {
            public enum Status
            {
                Inactive = -1,
                Draft = 0,
                Active,
                Power
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
        }
    }
}
