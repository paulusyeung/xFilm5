using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xFilm5.Helper
{
    public class UserHelper
    {
        public static bool UserExist(int userId)
        {
            bool result = false;

            DAL.User user = DAL.User.Load(userId);
            if (user != null)
                result = true;

            return result;
        }
    }
}
