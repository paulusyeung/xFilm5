using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using xFilm5.EF6;
using xFilm5.REST.Filters;
using xFilm5.REST.Helper;

namespace xFilm5.REST.Controllers
{
    public class RegistrationController : ApiController
    {
        [HttpPost]
        [Route("api/Registration/Register/{deviceId}")]
        [AllowAnonymous]
        public async Task<IHttpActionResult> PostRegister(string deviceId)
        {
            var json = Request.Content.ReadAsStringAsync().Result;
            var data = JsonConvert.DeserializeObject<Models.Registration>(json);

            if (data != null)
            {
                using (var ctx = new xFilmEntities())
                {
                    var deviceIdInUse = ctx.UserAuth.Where(x => x.DeviceId == deviceId).Any();
                    if (!deviceIdInUse)
                    {

                        var padmin = UserHelper.GetPrimaryAdminUser();

                        using (var scope = ctx.Database.BeginTransaction())
                        {
                            try
                            {
                                var workshop = ctx.vwWorkshopList.Where(x => x.WorkshopName == data.Workshop).SingleOrDefault();
                                if (workshop != null)
                                {
                                    #region create dbo.Client
                                    var client = new Client();
                                    client.Name = data.BusinessName;
                                    client.Status = (int)EnumHelper.Common.Status.Draft;
                                    client.CreatedOn = DateTime.Now;
                                    client.CreditLimit = 0;
                                    client.PaymentTerms = 0;
                                    client.PaymentType = 0;     // 0 = On Account, 1 = C.O.D.
                                    client.PIN = "";
                                    client.Branch = workshop.WorkshopId;

                                    ctx.Client.Add(client);
                                    ctx.SaveChanges();
                                    #endregion

                                    #region create dbo.ClientAddress
                                    var address = new Client_AddressBook();
                                    address.ClientID = client.ID;
                                    address.PrimaryAddr = true;
                                    address.CreatedOn = DateTime.Now;
                                    address.Name = data.BusinessName;
                                    address.Address = data.BusinessAddress;
                                    address.Tel = data.BusinessTel;
                                    address.Fax = "";
                                    address.Pager = "";
                                    address.ContactPerson = data.ContactPerson;
                                    address.Mobile = data.Mobile;
                                    address.SMS = "";
                                    address.SMS_Lang = data.Locale.Contains("en") ? 0 : data.Locale.ToLower() == "zh-chs" ? 1 : 2;

                                    ctx.Client_AddressBook.Add(address);
                                    ctx.SaveChanges();
                                    #endregion

                                    #region create dbo.ClientUser
                                    var cuser = new Client_User();
                                    cuser.ClientID = client.ID;
                                    cuser.PrimaryUser = true;
                                    cuser.FullName = data.ContactPerson;
                                    cuser.Email = data.Email;
                                    cuser.Password = data.Password;
                                    cuser.SecurityLevel = (int)EnumHelper.User.UserRole.Customer;
                                    cuser.LastIP = "";
                                    cuser.LastVisit = DateTime.Parse("1900-01-01 00:00:00");
                                    cuser.Branch = workshop.WorkshopId;

                                    ctx.Client_User.Add(cuser);
                                    ctx.SaveChanges();
                                    #endregion

                                    #region create dbo.User
                                    // refer: https://stackoverflow.com/questions/13086006/how-can-i-force-entity-framework-to-insert-identity-columns
                                    ctx.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [dbo].[User] ON");

                                    var user = new EF6.User();
                                    user.UserId = cuser.ID;
                                    user.UserType = (int)EnumHelper.User.UserType.Customer;
                                    user.UserSid = Guid.NewGuid();
                                    user.LoginName = data.Email;
                                    user.LoginPassword = data.Password;
                                    user.Alias = data.ContactPerson;
                                    user.Status = (int)EnumHelper.Common.Status.Draft;
                                    user.CreatedOn = DateTime.Now;
                                    user.CreatedBy = padmin;
                                    user.ModifiedOn = DateTime.Now;
                                    user.ModifiedBy = padmin;
                                    user.Retired = false;
                                    user.RetiredBy = 0;
                                    user.RetiredOn = DateTime.Parse("1900-01-01 00:00:00");

                                    ctx.User.Add(user);
                                    ctx.SaveChanges();

                                    ctx.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [dbo].[User] OFF");
                                    #endregion

                                    scope.Commit();

                                    #region send activation code via email
                                    var recipient = data.Email;
                                    var subject = data.Locale.Contains("en") ?
                                        "x5 Activation Code" :
                                         data.Locale.ToLower() == "zh-chs" ?
                                         "x5 激活密码" :
                                         "x5 激活密碼";
                                    var message = data.Locale.Contains("en") ?
                                        String.Format(@"Dear Customer ({0}): This is your activation code {1}", data.BusinessName, client.ID.ToString()) :
                                        data.Locale.ToLower() == "zh-chs" ?
                                         String.Format(@"尊敬的客户（{0}）：阁下的激活密码　{1}", data.BusinessName, client.ID.ToString()) :
                                         String.Format(@"尊敬的客戶（{0}）：閣下的激活密碼　{1}", data.BusinessName, client.ID.ToString());

                                    BotHelper.PostEmailSmtp(recipient, subject, message);
                                    #endregion

                                    return Ok();
                                }
                            }
                            catch (Exception)
                            {
                                scope.Rollback();
                            }
                        }
                    }
                }
            }

            return NotFound();
        }

        [HttpPost]
        [Route("api/Registration/Activate/{activationCode}")]
        [AllowAnonymous]
        public async Task<IHttpActionResult> PostActivate(string activationCode)
        {
            try
            {
                if (!(String.IsNullOrEmpty(activationCode)))
                {
                    int clientId = Int32.Parse(activationCode);
                    using (var ctx = new xFilmEntities())
                    {
                        var client = ctx.Client.Where(x => x.ID == clientId).SingleOrDefault();
                        if (client != null)
                        {
                            client.Status = (int)EnumHelper.Common.Status.Active;
                            ctx.SaveChanges();

                            #region activate mobile user
                            var cuser = ctx.Client_User.Where(x => x.ClientID == client.ID && x.PrimaryUser == true).SingleOrDefault();
                            if (cuser != null)
                            {
                                var user = ctx.User.Where(x => x.UserId == cuser.ID).SingleOrDefault();
                                if (user != null)
                                {
                                    user.Status = (int)EnumHelper.Common.Status.Active;
                                    ctx.SaveChanges();
                                }
                            }
                            #endregion

                            var padmin = UserHelper.GetPrimaryAdminUser();
                            #region create cloud disk account
                            BotHelper.PostCloudDisk_CreateClient(client.ID, padmin);      // 假設 CreatedBy 係 Primary Admin user
                            #endregion

                            return Ok();
                        }
                    }
                }
            }
            catch { }

            return NotFound();
        }

        [HttpPost]
        [Route("api/Registration/Activate/Resend/{registeredName}")]
        [AllowAnonymous]
        public async Task<IHttpActionResult> PostActivateResend(string registeredName)
        {
            try
            {
                if (!(String.IsNullOrEmpty(registeredName)))
                {
                    using (var ctx = new xFilmEntities())
                    {
                        var client = ctx.vwClientAddressList.Where(x => x.Name == registeredName && x.Status == 0).SingleOrDefault();
                        if (client != null)
                        {
                            #region send activation code via email
                            var recipient = client.Email;
                            var subject = client.SMS_Lang == 0 ?
                                "x5 Activation Code" :
                                 client.SMS_Lang == 1 ?
                                 "x5 激活密码" :
                                 "x5 激活密碼";
                            var message = client.SMS_Lang == 0 ?
                                String.Format(@"Dear Customer ({0}): This is your activation code {1}", client.Name, client.ClientId.ToString()) :
                                client.SMS_Lang == 1 ?
                                 String.Format(@"尊敬的客户（{0}）：阁下的激活密码　{1}", client.Name, client.ClientId.ToString()) :
                                 String.Format(@"尊敬的客戶（{0}）：閣下的激活密碼　{1}", client.Name, client.ClientId.ToString());

                            BotHelper.PostEmailSmtp(recipient, subject, message);
                            #endregion
                            return Ok();
                        }
                    }
                }
            }
            catch { }

            return NotFound();
        }
    }
}
