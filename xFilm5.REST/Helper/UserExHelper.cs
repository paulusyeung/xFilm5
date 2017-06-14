using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xFilm5.REST.Helper
{
    public class UserExHelper
    {
        // 2017.05.10 paulus: 因為 User to UserPreferences = 1 tp 0 or Many, 用 USEREX_OBJECTID 嚟代表 UserExtended Data
        public static Guid USEREX_OBJECTID = Guid.Parse("5FECE9FD-C4C9-4FF1-B8DC-5EBC3CD172C8");

        /// <summary>
        /// 每個 User 嘅 Extended data 會以 json format 存放在 MetadataXml 之內
        /// </summary>
        public class Preferences
        {
            #region private variables
            private static String json_defaults = @"
{
    ""Receipt"": {
    ""Grouping"": true,
    ""SmallFont"": true,
    ""Email"": false,
    ""Slip"": true
    },
    ""Notification"": {
    ""Email"": false,
    ""MobileApp"": false,
    ""OnOrder"": false,
    ""OnReady"": false
    },
    ""EmailRecipient"": """"
}";
            private int _UserId = 0;
            private Receipt _Receipt = new Receipt();
            private Notification _Notification = new Notification();
            private String _EmailRecipient = "";
            #endregion

            public Receipt Receipt { get { return _Receipt; } set { _Receipt = value; } }
            public Notification Notification { get { return _Notification; } set { _Notification = value; } }
            public string EmailRecipient { get { return _EmailRecipient; } set { _EmailRecipient = value; } }

            public Preferences()
            {

            }

            public bool Read(int userId)
            {
                bool result = false;

                using (var ctx = new EF6.xFilmEntities())
                {
                    var userExist = ctx.User.Where(x => x.UserId == userId).Any();
                    if (userExist)
                    {
                        var up = ctx.UserPreference.Where(x => x.UserId == userId).SingleOrDefault();
                        if (up != null)
                        {
                            var p = JsonConvert.DeserializeObject<Preferences>(up.MetadataXml);
                            _Receipt = p.Receipt;
                            _Notification = p.Notification;
                            _EmailRecipient = p.EmailRecipient;
                            _UserId = userId;

                            result = true;
                        }
                    }
                }

                return result;
            }

            public bool Save(int userId = 0)
            {
                bool result = false;

                if (userId != 0) _UserId = userId;

                if (_UserId != 0)
                {
                    using (var ctx = new EF6.xFilmEntities())
                    {
                        var userExist = ctx.User.Where(x => x.UserId == _UserId).Any();
                        if (userExist)
                        {
                            var up = ctx.UserPreference.Where(x => x.UserId == _UserId).SingleOrDefault();
                            if (up != null)
                            {
                                string json = JsonConvert.SerializeObject(this, Formatting.Indented);
                                up.MetadataXml = json;
                                ctx.SaveChanges();

                                result = true;
                            }
                        }
                    }
                }

                return result;
            }

            public static Preferences GetDefaults()
            {
                Preferences result = JsonConvert.DeserializeObject<Preferences>(json_defaults);

                return result;
            }
        }

        public class Receipt
        {
            public bool Grouping { get; set; }
            public bool SmallFont { get; set; }
            public bool Email { get; set; }
            public bool Slip { get; set; }
        }

        public class Notification
        {
            public bool Email { get; set; }
            public bool MobileApp { get; set; }
            public bool OnOrder { get; set; }
            public bool OnReady { get; set; }
        }

    }
}
