using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Security.Principal;
using System.Web;

using xFilm5.DAL;

namespace xFilm5.Controls
{
    public class Utility
    {
        public class Config
        {
            // 冇用
            public static string Plate5_Shuffle_Blueprint_SourceFolder
            {
                get
                {
                    String Blueprint_SourceFolder = String.Empty;
                    if (ConfigurationManager.AppSettings["Blueprint_SourceFolder"] != null)
                    {
                        Blueprint_SourceFolder = ConfigurationManager.AppSettings["Blueprint_SourceFolder"];
                    }
                    return Blueprint_SourceFolder;
                }
            }
            public static string Plate5_Shuffle_Blueprint_DestinationFolder
            {
                get
                {
                    String Blueprint_DestinationFolder = String.Empty;
                    if (ConfigurationManager.AppSettings["Blueprint_DestinationFolder"] != null)
                    {
                        Blueprint_DestinationFolder = ConfigurationManager.AppSettings["Blueprint_DestinationFolder"];
                    }
                    return Blueprint_DestinationFolder;
                }
            }
            public static string Plate5_Shuffle_Tiff_SourceFolder
            {
                get
                {
                    String Tiff_SourceFolder = String.Empty;
                    if (ConfigurationManager.AppSettings["Tiff_SourceFolder"] != null)
                    {
                        Tiff_SourceFolder = ConfigurationManager.AppSettings["Tiff_SourceFolder"];
                    }
                    return Tiff_SourceFolder;
                }
            }
            public static string Plate5_Shuffle_Tiff_DestinationFolder
            {
                get
                {
                    String Tiff_DestinationFolder = String.Empty;
                    if (ConfigurationManager.AppSettings["Tiff_DestinationFolder"] != null)
                    {
                        Tiff_DestinationFolder = ConfigurationManager.AppSettings["Tiff_DestinationFolder"];
                    }
                    return Tiff_DestinationFolder;
                }
            }
            public static string Plate5_Shuffle_ImpersonateUserName
            {
                get
                {
                    String ImpersonateUserName = String.Empty;
                    if (ConfigurationManager.AppSettings["ImpersonateUserName"] != null)
                    {
                        ImpersonateUserName = ConfigurationManager.AppSettings["ImpersonateUserName"];
                    }
                    return ImpersonateUserName;
                }
            }
            public static string Plate5_Shuffle_ImpersonatePassword
            {
                get
                {
                    String ImpersonatePassword = String.Empty;
                    if (ConfigurationManager.AppSettings["ImpersonatePassword"] != null)
                    {
                        ImpersonatePassword = ConfigurationManager.AppSettings["ImpersonatePassword"];
                    }
                    return ImpersonatePassword;
                }
            }
        }

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

            public static String GetWorkshopAddress(int workshopId)
            {
                String result = String.Empty;

                DAL.Client_User user = DAL.Client_User.Load(workshopId);
                if (user != null)
                {
                    result = ConfigurationManager.AppSettings[user.FullName] != null ? ConfigurationManager.AppSettings[user.FullName].ToString() : user.FullName;
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

            public static int GetDefaultBranch(int clientId)
            {
                int result = 0;

                DAL.Client client = DAL.Client.Load(clientId);
                if (client != null)
                {
                    result = client.Branch;
                }

                return result;
            }

            public static String GetDefaultBranchName(int clientId)
            {
                String result = String.Empty;

                int branchId = GetDefaultBranch(clientId);
                if (branchId != 0)
                {
                    DAL.Client_User branch = DAL.Client_User.Load(branchId);
                    if (branch != null)
                    {
                        result = branch.FullName;
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

        public class JobOrder
        {
            /// <summary>
            /// 將落咗 order 嘅 Direct Print 檔案 (Tiff 同 Blueprint) 由 storage 送去指定嘅 hot folder
            /// </summary>
            /// <param name="OrderId"></param>
            /// <returns></returns>
            public static bool Plate5_Shuffle(int OrderId)
            {
                bool result = true;

                String sql = String.Format("OrderID = {0}", OrderId.ToString());
                DAL.PrintQueueCollection PQs = DAL.PrintQueue.LoadCollection(sql);
                if (PQs.Count > 0)
                {
                    foreach (DAL.PrintQueue pq in PQs)
                    {
                        String oldBlueprint = String.Empty;         // 唔理拆幾多隻色，每 page 祇出一張藍紙
                        sql = String.Format("PrintQueueID = {0} AND PlateOrdered = 1", pq.ID.ToString());
                        DAL.PrintQueue_VPSCollection VPSs = DAL.PrintQueue_VPS.LoadCollection(sql);
                        if (VPSs.Count > 0)
                        {
                            foreach (DAL.PrintQueue_VPS vps in VPSs)
                            {
                                #region Shuffle Tiff
                                String tiff = String.Format("TG-A.{0}.{1}", pq.ClientID.ToString(), vps.VpsFileName.Replace(".VPS", ".TIFF"));
                                result = result && Plate5_ShuffleTiff(tiff);
                                #endregion

                                if (pq.BlueprintOrdered)
                                {
                                    #region Shuffle Blueprint
                                    String blueprint = String.Format("{0}.{1}", pq.ClientID.ToString(), vps.VpsFileName.Substring(0, vps.VpsFileName.IndexOf('(')) + "CMYK).TIFF");
                                    if (blueprint != oldBlueprint)  // 唔理拆幾多隻色，每 page 祇出一張藍紙
                                    {
                                        result = result && Plate5_ShuffleBlueprint(blueprint);
                                        oldBlueprint = blueprint;
                                    }
                                    #endregion
                                }
                            }
                        }
                    }
                }

                return result;
            }

            /// <summary>
            /// 將 Blueprint 放去 hot folder 排隊
            /// </summary>
            public static bool Plate5_ShuffleBlueprint(String filename)
            {
                bool result = false;
                String source = Path.Combine(Config.Plate5_Shuffle_Blueprint_SourceFolder, filename);
                String dest = Path.Combine(Config.Plate5_Shuffle_Blueprint_DestinationFolder, filename);

                using (new WindowsIdentity(Config.Plate5_Shuffle_ImpersonateUserName, Config.Plate5_Shuffle_ImpersonatePassword))
                {
                    if (File.Exists(source))
                    {
                        File.Copy(source, dest, true);
                        result = true;
                    }
                }

                return result;
            }

            /// <summary>
            /// 將 Tiff 放去 hot folder 排隊
            /// </summary>
            public static bool Plate5_ShuffleTiff(String filename)
            {
                bool result = false;
                String source = Path.Combine(Config.Plate5_Shuffle_Tiff_SourceFolder, filename);
                String dest = Path.Combine(Config.Plate5_Shuffle_Tiff_DestinationFolder, filename);

                using (new WindowsIdentity(Config.Plate5_Shuffle_ImpersonateUserName, Config.Plate5_Shuffle_ImpersonatePassword))
                {
                    if (File.Exists(source))
                    {
                        File.Copy(source, dest, true);
                        result = true;
                    }
                }

                return result;
            }

            public static bool IsPlate5(int orderId)
            {
                bool result = false;

                OrderHeader oOrder = OrderHeader.Load(orderId);
                if (oOrder != null)
                {
                    if (oOrder.ServiceType == (int)Common.Enums.OrderType.Plate5)
                    {
                        result = true;
                    }
                }

                    return result;
            }

            public static bool SetAsCompleted(List<int> orderIdList)
            {
                bool result = false;

                foreach(int id in orderIdList)
                {
                    result = SetAsCompleted(id);
                }

                return result;
            }

            // depricated
            public static bool SetAsCompleted_x5(List<int> orderIdList)
            {
                bool result = false;

                foreach (int id in orderIdList)
                {
                    result = IsAllPkItemsCompleted(id) ? SetAsCompleted(id) : false;
                }

                return result;
            }

            public static bool SetAsCompleted(int orderId)
            {
                bool result = false;

                OrderHeader oOrder = OrderHeader.Load(orderId);
                if (oOrder != null)
                {
                    oOrder.DateCompleted = DateTime.Now;
                    oOrder.Status = (int)DAL.Common.Enums.Workflow.Completed;
                    oOrder.Save();

                    result = true;
                }

                return result;
            }

            public static bool IsAllPkItemsCompleted(int orderId)
            {
                bool result = false;

                String sql = String.Format("OrderHeaderId = {0} AND IsReceived = 1", orderId.ToString());
                DAL.OrderPkPrintQueueVpsCollection pks = DAL.OrderPkPrintQueueVps.LoadCollection(sql);
                result = (pks.Count == 1) ? true : false;

                return result;
            }

            public static bool WriteJournal(List<int> orderIdList, DAL.Common.Enums.Workflow status)
            {
                bool result = false;

                foreach (int id in orderIdList)
                {
                    result = WriteJournal(id, status);
                }

                return result;
            }

            public static bool WriteJournal(int orderId, DAL.Common.Enums.Workflow status)
            {
                bool result = false;

                try
                {
                    DAL.Order_Journal log = new Order_Journal();
                    log.OrderID = orderId;
                    log.Status = (int)status;
                    log.UserID = DAL.Common.Config.CurrentUserId;
                    log.DateUpdated = DateTime.Now;
                    log.Save();
                    result = true;
                }
                catch
                {
                    result = false;
                }

                return result;
            }
        }

        public class PrintQueue_VPS
        {
            /// <summary>
            /// 用 dbo.vwPrintQueueVpsList_Availalbe 搵出 ClientID 1, ClientID 2...
            /// </summary>
            /// <param name="clientId"></param>
            /// <returns></returns>
            public static String AvailableClientDelimitedList()
            {
                String result = String.Empty;

                String sql = @"
DECLARE @listStr VARCHAR(MAX) 
SELECT @listStr = COALESCE(@listStr+',' ,'') + CONVERT(varchar(6), ClientID) 
FROM (SELECT DISTINCT ClientID FROM vwPrintQueueVpsList_Available) t
SELECT @listStr";
                SqlDataReader reader = SqlHelper.Default.ExecuteReader(CommandType.Text, sql);

                while (reader.Read())
                {
                    if (!(reader.IsDBNull(0)))
                        result = reader.GetString(0);
                }

                return result;
            }
            public static String AvailablePlateClientDelimitedList()
            {
                String result = String.Empty;

                String sql = @"
DECLARE @listStr VARCHAR(MAX) 
SELECT @listStr = COALESCE(@listStr+',' ,'') + CONVERT(varchar(6), ClientID) 
FROM (SELECT DISTINCT ClientID FROM vwPrintQueueVpsList_AvailablePlate) t
SELECT @listStr";
                SqlDataReader reader = SqlHelper.Default.ExecuteReader(CommandType.Text, sql);

                while (reader.Read())
                {
                    if (!(reader.IsDBNull(0)))
                        result = reader.GetString(0);
                }

                return result;
            }
            public static String AvailableFilmClientDelimitedList()
            {
                String result = String.Empty;

                String sql = @"
DECLARE @listStr VARCHAR(MAX) 
SELECT @listStr = COALESCE(@listStr+',' ,'') + CONVERT(varchar(6), ClientID) 
FROM (SELECT DISTINCT ClientID FROM vwPrintQueueVpsList_AvailableFilm) t
SELECT @listStr";
                SqlDataReader reader = SqlHelper.Default.ExecuteReader(CommandType.Text, sql);

                while (reader.Read())
                {
                    if (!(reader.IsDBNull(0)))
                        result = reader.GetString(0);
                }

                return result;
            }
        }

        public class PrintQueue
        {
            /// <summary>
            /// 當 Plate5 order cancel 的時候，把 order 點選的 dbo.PrintQueueVPS reset，於是可以重新再落 order
            /// </summary>
            /// <param name="orderId"></param>
            /// <returns></returns>
            public static bool ResetOrder(int orderId)
            {
                bool result = false;

                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "apPrintQueue_ResetOrder";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("orderId", orderId));

                    SqlHelper.Default.ExecuteNonQuery(cmd);
                    result = true;
                }

                return result;
            }
        }

        public class PrintQueue_LifeCycle
        {
            public static bool WriteLogWithPqId(int pqId, DAL.Common.Enums.PrintQSubitemType type)
            {
                bool result = false;

                DAL.PrintQueue pq = DAL.PrintQueue.Load(pqId);
                if (pq != null)
                {
                    DAL.PrintQueue_LifeCycle log = new DAL.PrintQueue_LifeCycle();
                    log.PrintQueueId = pq.ID;
                    log.PrintQSubitemType = (int)type;
                    log.Status = (int)DAL.Common.Enums.Status.Active;
                    log.CreatedOn = DateTime.Now;
                    log.CreatedBy = DAL.Common.Config.CurrentUserId;
                    log.Save();

                    result = true;
                }

                return result;
            }

            public static bool WriteLogWithVpsId(int vpsId, DAL.Common.Enums.PrintQSubitemType type)
            {
                bool result = false;

                DAL.PrintQueue_VPS vps = DAL.PrintQueue_VPS.Load(vpsId);
                if (vps != null)
                {
                    DAL.PrintQueue_LifeCycle log = new DAL.PrintQueue_LifeCycle();
                    log.PrintQueueId = vps.PrintQueueID;
                    log.PrintQueueVpsId = vps.ID;
                    log.PrintQSubitemType = (int)type;
                    log.Status = (int)DAL.Common.Enums.Status.Active;
                    log.CreatedOn = DateTime.Now;
                    log.CreatedBy = DAL.Common.Config.CurrentUserId;
                    log.Save();

                    result = true;
                }

                return result;
            }
        }

        public class Product
        {
            public static bool IsX5Item(int id)
            {
                bool result = false;

                DAL.T_BillingCode_Item item = DAL.T_BillingCode_Item.Load(id);
                if (item != null)
                {
                    DAL.T_BillingCode_Dept dept = DAL.T_BillingCode_Dept.Load(item.DeptID);
                    if (dept != null)
                    {
                        if (dept.Code == "X5") result = true;
                    }
                }

                return result;
            }
        }

        public class System
        {
            public static int GetNextInvoiceNumber()
            {
                int result = 0;

                DAL.X_CounterCollection items = DAL.X_Counter.LoadCollection();
                if (items.Count > 0)
                {
                    DAL.X_Counter sys = items[0];
                    result = sys.InvoiceNo;
                    sys.InvoiceNo++;
                    sys.Save();
                }

                return result;
            }

            public static bool x5OnAir()
            {
                bool result = false;

                if (ConfigurationManager.AppSettings["x5OnAir"] != null)
                {
                    String onAir = ConfigurationManager.AppSettings["x5OnAir"].ToString().ToLower();
                    result = Convert.ToBoolean(onAir);
                }
                //result = ConfigurationManager.AppSettings["x5OnAir"] != null ? Boolean.TryParse(ConfigurationManager.AppSettings["x5OnAir"].ToString().ToLower(), out result) : false;

                return result;
            }
        }

        public class Invoice
        {
            public static bool SetInvoiceToVoid(int invoiceId)
            {
                bool result = false;

                DAL.Acct_INMaster inv = DAL.Acct_INMaster.Load(invoiceId);
                if (inv != null)
                {
                    inv.Status = (int)DAL.Common.Enums.Status.Inactive;
                    inv.LastModifiedOn = DateTime.Now;
                    inv.LastModifiedBy = DAL.Common.Config.CurrentUserId;
                    inv.Save();

                    result = true;
                }

                return result;
            }
        }
    }

    public class ListViewHelper
    {
        public static int GetHitColumn(Gizmox.WebGUI.Forms.ListView listview, Gizmox.WebGUI.Forms.MouseEventArgs mea)
        {
            int result = -1;

            // ignor 空白的 area, HACK: last column 會搞錯，要避免用到 last column
            Gizmox.WebGUI.Forms.ListViewItem item = listview.GetItemAt(mea.X, mea.Y);
            if (item != null)
            {
                int mousex = mea.X;
                int x = 0;
                const int gridLine = 3;
                const int checkbox = 24;
                int subitemindex = 0;
                if (listview.CheckBoxes)
                    x += checkbox + gridLine;
                for (int i = 0; i < listview.Columns.Count; i++)
                {
                    if (listview.Columns[i].Visible)    // 唔計 hidden column
                    {
                        x += (listview.Columns[i].Width + gridLine);
                        subitemindex = i;
                        if (mousex < x)
                        {
                            break;
                        }
                    }
                }
                result = subitemindex;
            }

            return result;
        }
    }
}
