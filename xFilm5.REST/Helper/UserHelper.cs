﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using xFilm5.EF6;

namespace xFilm5.REST.Helper
{
    public class UserHelper
    {
        /// <summary>
        /// refer to Common.Enums.UserRole
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static int GetSecurityLevel(int userId)
        {
            int result = 0;

            using (var ctx = new EF6.xFilmEntities())
            {
                var cUser = ctx.Client_User.Where(x => x.ID == userId).SingleOrDefault();
                if (cUser != null)
                {
                    result = (int)cUser.SecurityLevel;
                }
            }

            return result;
        }

        public static Guid GetUserSid(int userId)
        {
            Guid result = Guid.Empty;

            using (var ctx = new EF6.xFilmEntities())
            {
                var cUser = ctx.User.Where(x => x.UserId == userId).SingleOrDefault();
                if (cUser != null)
                {
                    result = cUser.UserSid;
                }
            }

            return result;
        }

        public static int GetPrimaryAdminUser()
        {
            var result = 0;

            using (var ctx = new EF6.xFilmEntities())
            {
                var admin = (int)EnumHelper.User.UserRole.Admin;
                var user = ctx.Client_User.Where(x => x.PrimaryUser == true && x.SecurityLevel == admin).SingleOrDefault();
                if (user != null)
                {
                    result = user.ID;
                }
            }

            return result;
        }
    }
}
