using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using xFilm5.DAL;

namespace xFilm5.Controls
{
    public class Utility
    {
        public class Owner
        {
            public static int GetOwnerId()
            {
                int result = 0;

                string sql = String.Format("Status = {0}", 2);
                DAL.Client client = DAL.Client.LoadWhere(sql);
                if (client != null)
                {
                    result = client.ID;
                }

                return result;
            }

            public static string GetOwnerName()
            {
                string result = String.Empty;

                DAL.Client client = DAL.Client.Load(GetOwnerId());
                if (client != null)
                {
                    result = client.Name;
                }

                return result;
            }

            public static string GetOwnerAddress()
            {
                string result = String.Empty;

                string sql = String.Format("ClientID = {0} AND PrimaryAddr = 1", GetOwnerId().ToString());
                Client_AddressBook address = Client_AddressBook.LoadWhere(sql);
                if (address != null)
                {
                    result = address.Address;
                }

                return result;
            }
        }

        public class User
        {
            /// <summary>
            /// Is Current User a cashier?
            /// </summary>
            /// <returns></returns>
            public static bool IsCashier()
            {
                return IsCashier(xFilm5.DAL.Common.Config.CurrentUserId);
            }

            public static bool IsCashier(int userId)
            {
                bool result = false;

                Client_User user = Client_User.Load(userId);
                if (user != null)
                {
                    if (user.SecurityLevel == (int)xFilm5.DAL.Common.Enums.UserRole.Cashier)
                        result = true;
                }

                return result;
            }

            /// <summary>
            /// Is Current User a client?
            /// </summary>
            /// <returns></returns>
            public static bool IsClient()
            {
                return IsClient(xFilm5.DAL.Common.Config.CurrentUserId);
            }

            public static bool IsClient(int userId)
            {
                bool result = false;

                Client_User user = Client_User.Load(userId);
                if (user != null)
                {
                    if (user.SecurityLevel == (int)xFilm5.DAL.Common.Enums.UserRole.Customer)
                        result = true;
                }

                return result;
            }

            /// <summary>
            /// Is Current User a staff?
            /// </summary>
            /// <returns></returns>
            public static bool IsStaff()
            {
                return IsStaff(xFilm5.DAL.Common.Config.CurrentUserId);
            }

            public static bool IsStaff(int userId)
            {
                bool result = false;

                Client_User user = Client_User.Load(userId);
                if (user != null)
                {
                    if ((user.SecurityLevel != (int)xFilm5.DAL.Common.Enums.UserRole.Customer) && (user.SecurityLevel != (int)xFilm5.DAL.Common.Enums.UserRole.Cashier))
                        result = true;
                }

                return result;
            }

            public static string GetUserMenu(int userId)
            {
                string result = String.Empty;

                Client_User user = Client_User.Load(userId);
                if (user != null)
                {
                    switch (user.SecurityLevel)
                    {
                        case (int)xFilm5.DAL.Common.Enums.UserRole.Customer:
                            result = "~/Resources/Menu/NavMenu4Customer.xml";
                            break;
                        case (int)xFilm5.DAL.Common.Enums.UserRole.Operator:
                            result = "~/Resources/Menu/NavMenu4Operator.xml";
                            break;
                        case (int)xFilm5.DAL.Common.Enums.UserRole.Sales:
                            result = "~/Resources/Menu/NavMenu4Sales.xml";
                            break;
                        case (int)xFilm5.DAL.Common.Enums.UserRole.Account:
                            result = "~/Resources/Menu/NavMenu4Account.xml";
                            break;
                        case (int)xFilm5.DAL.Common.Enums.UserRole.Admin:
                            result = "~/Resources/Menu/NavMenu4Admin.xml";
                            break;
                        case (int)xFilm5.DAL.Common.Enums.UserRole.Workshop:
                        default:
                            result = "~/Resources/Menu/NavMenu4Workshop.xml";
                            break;
                    }
                }

                return result;
            }

            /// <summary>
            /// Get Client Id of the Current User
            /// </summary>
            /// <returns></returns>
            public static int GetClientId()
            {
                return GetClientId(xFilm5.DAL.Common.Config.CurrentUserId);
            }
            public static int GetClientId(int userId)
            {
                int result = 0;

                Client_User user = Client_User.Load(userId);
                if (user != null)
                {
                    result = user.ClientID;
                }

                return result;
            }

            public static int UserRole()
            {
                return UserRole(DAL.Common.Config.CurrentUserId);
            }

            public static int UserRole(int userId)
            {
                int role = 0;

                DAL.Client_User user = DAL.Client_User.Load(userId);
                if (user != null)
                {
                    role = user.SecurityLevel;
                }

                return role;
            }
        }

        public class Client
        {
            public static bool IsSuspended(int clientId)
            {
                bool result = false;

                DAL.Client client = DAL.Client.Load(clientId);
                if (client != null)
                {
                    if (client.Status == (int)DAL.Common.Enums.Status.Inactive)
                    {
                        result = true;
                    }
                }

                return result;
            }
        }

        public class ClientAddress
        {
            public static bool IsPrimary(int addressId)
            {
                bool result = false;

                xFilm5.DAL.Client_AddressBook address = xFilm5.DAL.Client_AddressBook.Load(addressId);
                if (address != null)
                {
                    result = address.PrimaryAddr;
                }

                return result;
            }
        }

        public class ClientUser
        {
            public static bool IsPrimary(int addressId)
            {
                bool result = false;

                xFilm5.DAL.Client_User user = xFilm5.DAL.Client_User.Load(addressId);
                if (user != null)
                {
                    result = user.PrimaryUser;
                }

                return result;
            }
        }
    }
}
