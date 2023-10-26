using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace xFilm5.SpeedBox.Helper
{
    public class Common
    {
        /// <summary>
        /// 復原到 install 之初
        /// </summary>
        public static void FactoryReset()
        {
            var response = HttpContext.Current.Response;
            response.Cookies.Remove("xFilm5_SpeedBox_CurrentTheme");
            response.Cookies.Remove("xFilm5_SpeedBox_CurrentPage");
            response.Flush();
        }

        /// <summary>
        /// 用戶登入
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        public static void Login(String username, String password)
        {

        }
    }
}
