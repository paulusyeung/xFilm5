using Hangfire;
using log4net;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Principal;
using System.Web.Http;
using xFilm5.Bot.Helper;
using xFilm5.EF6;

namespace xFilm5.Bot.Controllers
{
    public class BotController : ApiController
    {
        #region Instead of naming my invoking class, I started using the following:
        //private static log4net.ILog Log { get; set; }
        //ILog log = log4net.LogManager.GetLogger(typeof(BotController));

        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        // In this way, I can use the same line of code in every class that uses log4net without having to remember to change code when I copy and paste.
        // Alternatively, i could create a logging class, and have every other class inherit from my logging class.
        // Refer: https://stackoverflow.com/questions/7089286/correct-way-of-using-log4net-logger-naming
        #endregion

        /// <summary>
        /// 抄藍紙檔案去打藍紙
        /// </summary>
        /// <param name="jsonData"></param>
        /// <returns></returns>
        [Route("blueprint")]
        public IHttpActionResult PostBlueprint([FromBody] JObject jsonData)
        {
            if (jsonData == null)
            {
                log.Error("[bot, blueprint] jsonData == null");
                return NotFound();
            }
            else
            {
                int vpsPqId = jsonData["PrintQueueVpsId"].Value<int>();

                using (var ctx = new xFilmEntities())
                {
                    var vps = ctx.PrintQueue_VPS.FirstOrDefault(v => v.ID == vpsPqId);
                    if (vps != null)
                    {
                        #region 用 NetworkConnection: 來自於 NetworkConnection.cs 用嚟做 impersonation
                        String serverUri = ConfigurationManager.AppSettings["Blueprint_ServerUri"];
                        String userName = ConfigurationManager.AppSettings["Blueprint_UserName"];
                        String userPassword = ConfigurationManager.AppSettings["Blueprint_UserPassword"];
                        String sourecPath = ConfigurationManager.AppSettings["Blueprint_SourcePath"];
                        String destPath = ConfigurationManager.AppSettings["Blueprint_DestinationPath"];
                        String filePrefix = ConfigurationManager.AppSettings["Blueprint_FileNamePrefix"];

                        var uri = new Uri(serverUri);
                        System.Net.NetworkCredential readCredentials = new System.Net.NetworkCredential(userName, userPassword);
                        #endregion

                        using (new NetworkConnection(String.Format(@"\\{0}", uri.Host), readCredentials))
                        {
                            // 參考：https://bitbucket.org/nxstudio/xfilm5/wiki/xFilm5%20Plate5%20Order%20Form

                            // 2017.04.11 paulus: 要用 wildcard 抄
                            //String fileName = String.Format("{0}.{1}.{2}.tif", filePrefix, vps.PrintQueue.ClientID.ToString(), vps.VpsFileName.Substring(0, vps.VpsFileName.Length - 4));


                            String aliasname = vps.VpsFileName.Substring(0, vps.VpsFileName.Length - 4);    // ignor suffix (file type)
                            aliasname = aliasname.Substring(0, aliasname.LastIndexOf('('));                 // ignor "(color)"
                            String fileName = String.Format("{0}.{1}.{2}*", filePrefix, vps.PrintQueue.ClientID.ToString(), aliasname);     // wildcard

                            //String source = Path.Combine(@"\\192.168.12.230\DirectPrint\EfiProof", "*" + aliasname + "*");
                            //FileInfo[] files = Helper.FileHelper.FileUtils.GetFilesMatchWildCard(source, "MonAgent", "nx-9602");

                            //String filePath_Source = Path.Combine(serverUri + sourecPath, fileName);
                            //String filePath_Dest = Path.Combine(serverUri + destPath, ".");

                            DirectoryInfo info = new DirectoryInfo(serverUri + sourecPath);
                            if (info.Exists)
                            {
                                FileInfo[] files = info.GetFiles(fileName);
                                if (files.Length > 0)
                                {
                                    try
                                    {
                                        foreach (FileInfo item in files)
                                        {
                                            String filePath_Source = item.FullName;
                                            String filePath_Dest = Path.Combine(serverUri + destPath, item.Name);
                                            File.Copy(filePath_Source, filePath_Dest, true);

                                            // 2018.07.14 paulus: 叫 Hangfire 抄去 Cloud Disk
                                            BackgroundJob.Enqueue(() => CloudDiskHelper.UploadBlueprintFile(filePath_Source));      // filePath_Source format "\\192.168.12.230\DirectPrint\EfiProof\efi.202706.MA7222-op180910-F01-Chiyu-BOOK.p1(K).TIF"
                                            //CloudDiskHelper.UploadBlueprintFile(filePath_Source);
                                        }
                                        log.Info(String.Format("[bot, blueprint, copied] \r\nFile Name = {0}\r\nFilePath_Source = {1}{2}", fileName, serverUri, sourecPath));

                                        return Ok();
                                    }
                                    catch (Exception e)
                                    {
                                        log.Fatal("[bot, blueprint, Copy] \r\n", e);
                                        return NotFound();
                                    }
                                }
                                else
                                {
                                    log.Error(String.Format("[bot, blueprint, source file not exist] \r\nFile Name = {0}\r\nFilePath_Source = {1}\\{2}", fileName, serverUri, sourecPath));
                                    return NotFound();
                                }
                            }
                            else
                            {
                                log.Error(String.Format("[bot, blueprint, source directory not exist] \r\nFile Name = {0}\r\nFilePath_Source = {1}\\{2}", fileName, serverUri, sourecPath));
                                return NotFound();
                            }
                        }
                    }
                    else
                    {
                        log.Error("[bot, blueprint, PrintQueue_VPS not found] \r\n" + vps.ToString());
                        return NotFound();
                    }
                }
            }
        }

        /// <summary>
        /// 抄 Tiff 去出鋅
        /// </summary>
        /// <param name="jsonData"></param>
        /// <returns></returns>
        [Route("plate")]
        public IHttpActionResult PostPlate([FromBody] JObject jsonData)
        {
            if (jsonData == null)
            {
                log.Error("[bot, plate] jsonData == null");
                return NotFound();
            }
            else
            {
                int vpsPqId = jsonData["PrintQueueVpsId"].Value<int>();

                using (var ctx = new xFilmEntities())
                {
                    var vps = ctx.PrintQueue_VPS.FirstOrDefault(v => v.ID == vpsPqId);
                    if (vps != null)
                    {
                        #region 用 NetworkConnection: 來自於 NetworkConnection.cs 用嚟做 impersonation
                        String serverUri = ConfigurationManager.AppSettings["Plate_ServerUri"];
                        String userName = ConfigurationManager.AppSettings["Plate_UserName"];
                        String userPassword = ConfigurationManager.AppSettings["Plate_UserPassword"];
                        String sourecPath = ConfigurationManager.AppSettings["Plate_SourcePath"];
                        String destPath = ConfigurationManager.AppSettings["Plate_DestinationPath"] + "\\" + vps.PrintQueue.PlateSize;
                        String filePrefix = ConfigurationManager.AppSettings["Plate_FileNamePrefix"];

                        var uri = new Uri(serverUri);
                        System.Net.NetworkCredential readCredentials = new System.Net.NetworkCredential(userName, userPassword);
                        #endregion

                        //using (new WindowsIdentity(userName, userPassword))
                        //{

                        //}
                        using (new NetworkConnection(String.Format(@"\\{0}", uri.Host), readCredentials))
                        {
                            // 參考：https://bitbucket.org/nxstudio/xfilm5/wiki/xFilm5%20Plate5%20Order%20Form
                            String destUri = serverUri + destPath;
                            String fileName = String.Format("{0}.{1}.{2}.tif", filePrefix, vps.PrintQueue.ClientID.ToString(), vps.VpsFileName.Substring(0, vps.VpsFileName.Length - 4));
                            String filePath_Source = Path.Combine(serverUri + sourecPath, fileName);
                            String filePath_Dest = Path.Combine(destUri, fileName);

                            if (File.Exists(filePath_Source))
                            {
                                //IEnumerable<string> files = Directory.EnumerateFiles(bpFilePath_Source, "*.dll", SearchOption.AllDirectories);
                                if (!(Directory.Exists(destUri))) Directory.CreateDirectory(destUri);

                                if (!File.Exists(filePath_Dest))
                                {
                                    try
                                    {
                                        File.Copy(filePath_Source, filePath_Dest);
                                        log.Info(String.Format("[bot, plate, copied] \r\nFile Name = {0}\r\nFilePath_Source = {1}\r\nFilePath_Dest = {2}", fileName, filePath_Source, filePath_Dest));

                                        #region 2018.06.30 paulus: 抄一份去 CloudDisk
                                        BackgroundJob.Enqueue(() => CloudDiskHelper.UploadPlateFile(filePath_Source));
                                        //CloudDiskHelper.UploadPlateFile(filePath_Source);
                                        #endregion

                                        return Ok();
                                    }
                                    catch (Exception e)
                                    {
                                        log.Fatal("[bot, plate, Copy] \r\n", e);
                                        return NotFound();
                                    }
                                }
                                else
                                {
                                    log.Error(String.Format("[bot, plate, dest file exist] \r\nFile Name = {0}\r\nFilePath_Source = {1}\r\nFilePath_Dest = {2}", fileName, filePath_Source, filePath_Dest));
                                    return NotFound();
                                }
                            }
                            else
                            {
                                log.Error(String.Format("[bot, plate, source file not exist] \r\nFile Name = {0}\r\nFilePath_Source = {1}\r\nFilePath_Dest = {2}", fileName, filePath_Source, filePath_Dest));
                                return NotFound();
                            }
                        }
                    }
                    else
                    {
                        log.Error("[bot, plate, PrintQueue_VPS not found] \r\n" + vps.ToString());
                        return NotFound();
                    }
                }
            }
        }

        [Route("cip3")]
        public IHttpActionResult PostCip3([FromBody] JObject jsonData)
        {
            if (jsonData == null)
            {
                log.Error("[bot, cip3] jsonData == null");
                return NotFound();
            }
            else
            {
                int vpsPqId = jsonData["PrintQueueVpsId"].Value<int>();

                using (var ctx = new xFilmEntities())
                {
                    var vps = ctx.PrintQueue_VPS.FirstOrDefault(v => v.ID == vpsPqId);
                    if (vps != null)
                    {
                        #region 用 NetworkConnection: 來自於 NetworkConnection.cs 用嚟做 impersonation
                        String serverUri = ConfigurationManager.AppSettings["Cip3_ServerUri"];
                        String userName = ConfigurationManager.AppSettings["Cip3_UserName"];
                        String userPassword = ConfigurationManager.AppSettings["Cip3_UserPassword"];
                        String sourecPath = ConfigurationManager.AppSettings["Cip3_SourcePath"];
                        String destPath = ConfigurationManager.AppSettings["Cip3_DestinationPath"] + "\\" + vps.PrintQueue.PlateSize;
                        String filePrefix = ConfigurationManager.AppSettings["Cip3_FileNamePrefix"];

                        var uri = new Uri(serverUri);
                        System.Net.NetworkCredential readCredentials = new System.Net.NetworkCredential(userName, userPassword);
                        #endregion

                        using (new NetworkConnection(String.Format(@"\\{0}", uri.Host), readCredentials))
                        {
                            // 參考：https://bitbucket.org/nxstudio/xfilm5/wiki/xFilm5%20Plate5%20Order%20Form
                            String destUri = serverUri + destPath;
                            String fileName = String.Format("{0}.{1}.{2}.tif", filePrefix, vps.PrintQueue.ClientID.ToString(), vps.VpsFileName.Substring(0, vps.VpsFileName.Length - 4));
                            String filePath_Source = Path.Combine(serverUri + sourecPath, fileName);
                            String filePath_Dest = Path.Combine(destUri, fileName);

                            if (File.Exists(filePath_Source))
                            {
                                //IEnumerable<string> files = Directory.EnumerateFiles(bpFilePath_Source, "*.dll", SearchOption.AllDirectories);
                                if (!(Directory.Exists(destUri))) Directory.CreateDirectory(destUri);

                                if (!File.Exists(filePath_Dest))
                                {
                                    try
                                    {
                                        File.Copy(filePath_Source, filePath_Dest);
                                        log.Info(String.Format("[bot, cip3, copied] \r\nFile Name = {0}\r\nFilePath_Source = {1}\r\nFilePath_Dest = {2}", fileName, filePath_Source, filePath_Dest));
                                        return Ok();
                                    }
                                    catch (Exception e)
                                    {
                                        log.Fatal("[bot, cip3, Copy] \r\n", e);
                                        return NotFound();
                                    }
                                }
                                else
                                {
                                    log.Error(String.Format("[bot, cip3, dest file exist] \r\nFile Name = {0}\r\nFilePath_Source = {1}\r\nFilePath_Dest = {2}", fileName, filePath_Source, filePath_Dest));
                                    return NotFound();
                                }
                            }
                            else
                            {
                                log.Error(String.Format("[bot, cip3, source file not exist] \r\nFile Name = {0}\r\nFilePath_Source = {1}\r\nFilePath_Dest = {2}", fileName, filePath_Source, filePath_Dest));
                                return NotFound();
                            }
                        }
                    }
                    else
                    {
                        log.Error("[bot, cip3, PrintQueue_VPS not found] \r\n" + vps.ToString());
                        return NotFound();
                    }
                }
            }
        }

        /// <summary>
        /// 抄 Tiff 去出菲林
        /// </summary>
        /// <param name="jsonData"></param>
        /// <returns></returns>
        [Route("film")]
        public IHttpActionResult PostFilm([FromBody] JObject jsonData)
        {
            if (jsonData == null)
            {
                log.Error("[bot, plate] jsonData == null");
                return NotFound();
            }
            else
            {
                int vpsPqId = jsonData["PrintQueueVpsId"].Value<int>();

                using (var ctx = new xFilmEntities())
                {
                    var vps = ctx.PrintQueue_VPS.FirstOrDefault(v => v.ID == vpsPqId);
                    if (vps != null)
                    {
                        #region 用 NetworkConnection: 來自於 NetworkConnection.cs 用嚟做 impersonation
                        String serverUri = ConfigurationManager.AppSettings["Film_ServerUri"];
                        String userName = ConfigurationManager.AppSettings["Film_UserName"];
                        String userPassword = ConfigurationManager.AppSettings["Film_UserPassword"];
                        String sourecPath = ConfigurationManager.AppSettings["Film_SourcePath"];
                        String destPath = ConfigurationManager.AppSettings["Film_DestinationPath"];     // + "\\" + vps.PrintQueue.PlateSize;
                        String filePrefix = ConfigurationManager.AppSettings["Film_FileNamePrefix"];

                        var uri = new Uri(serverUri);
                        System.Net.NetworkCredential readCredentials = new System.Net.NetworkCredential(userName, userPassword);
                        #endregion

                        using (new NetworkConnection(String.Format(@"\\{0}", uri.Host), readCredentials))
                        {
                            // 參考：https://bitbucket.org/nxstudio/xfilm5/wiki/xFilm5%20Plate5%20Order%20Form
                            String destUri = serverUri + destPath;

                            #region prepare the filename
                            // VpsFileName = CupsJobId-OriginalFileName.PageNumber(color).VPS
                            // filename = CustomerNUmber.CupsJobId-OriginalFileName.ps
                            String filename = "";
                            filename = vps.VpsFileName.Substring(0, vps.VpsFileName.LastIndexOf('.'));      // cut suffix
                            filename = filename.Substring(0, filename.LastIndexOf('.'));                    // cut PageNumber(color)
                            filename = String.Format("{0}.{1}.ps", vps.PrintQueue.ClientID.ToString(), filename);
                            #endregion

                            String filePath_Source = Path.Combine(serverUri + sourecPath, filename);
                            String filePath_Dest = Path.Combine(destUri, filename);

                            if (File.Exists(filePath_Source))
                            {
                                if (!(Directory.Exists(destUri))) Directory.CreateDirectory(destUri);

                                #region 抄 PS 去 output，衹抄１次
                                if (!File.Exists(filePath_Dest))
                                {
                                    try
                                    {
                                        File.Copy(filePath_Source, filePath_Dest);
                                        log.Info(String.Format("[bot, film, copied] \r\nFile Name = {0}\r\nFilePath_Source = {1}\r\nFilePath_Dest = {2}", filename, filePath_Source, filePath_Dest));

                                        // 2018.07.14 paulus: 叫 Hangfire 抄去 Cloud Disk
                                        //BackgroundJob.Enqueue(() => CloudDiskHelper.UploadFilmFile(filePath_Source));
                                        //
                                        //return Ok();
                                    }
                                    catch (Exception e)
                                    {
                                        log.Fatal("[bot, film, Copy] \r\n", e);
                                        return NotFound();
                                    }
                                }
                                else
                                {
                                    log.Error(String.Format("[bot, film, dest file exist] \r\nFile Name = {0}\r\nFilePath_Source = {1}\r\nFilePath_Dest = {2}", filename, filePath_Source, filePath_Dest));
                                    return NotFound();
                                }
                                #endregion

                                // 2018.08.29 paulus: 抄 VPS 去 CloudDisk /film，用 VPS 方便 re-output，PS 可以喺 /cups 度搵
                                log.Info(String.Format("[bot, film, copied] \r\nVPS File Name = {0}\r\nClient Id = {1}", vps.VpsFileName, vps.PrintQueue.ClientID.ToString()));
                                BackgroundJob.Enqueue(() => CloudDiskHelper.UploadFilmFile_VPS(vps.VpsFileName, vps.PrintQueue.ClientID));
                                return Ok();
                            }
                            else
                            {
                                log.Error(String.Format("[bot, film, source file not exist] \r\nFile Name = {0}\r\nFilePath_Source = {1}\r\nFilePath_Dest = {2}", filename, filePath_Source, filePath_Dest));
                                return NotFound();
                            }
                        }
                    }
                    else
                    {
                        log.Error("[bot, plate, PrintQueue_VPS not found] \r\n" + vps.ToString());
                        return NotFound();
                    }
                }
            }
        }

        /// <summary>
        /// 打印 Xprinter 收貨單 (Delivery Note)
        /// </summary>
        /// <param name="jsonData"></param>
        /// <returns></returns>
        [Route("xprinter")]
        public IHttpActionResult PostXprinter([FromBody] JObject jsonData)
        {
            if (jsonData == null)
            {
                log.Error("[bot, xprinter] jsonData == null");
                return NotFound();
            }
            else
            {
                int receiptId = jsonData["ReceiptId"].Value<int>();
                int languageId = jsonData["LanguageId"].Value<int>();
                string printerName = jsonData["PrinterName"].Value<string>();
                bool smallFont = jsonData["SmallFont"].Value<bool>();

                using (var ctx = new xFilmEntities())
                {
                    var hasRows = ctx.vwReceiptDetailsList_Ex.Where(x => x.ReceiptHeaderId == receiptId).Any();
                    if (hasRows)
                    {
                        try
                        {
                            var xp80 = new PrinterHelper();
                            xp80.Print(receiptId, languageId, printerName, smallFont);

                            log.Info(String.Format("[bot, xprinter, receipt printed] \r\nReceipt Number = {0}\r\nLanguage Id = {1}\r\nPrinter Name = {2}", receiptId.ToString(), languageId.ToString(), printerName));
                            return Ok();
                        }
                        catch (Exception ex)
                        {
                            log.Error(String.Format("[bot, xprinter, print error] \r\nExceptional Error = {0}", ex.ToString()));
                            return NotFound();
                        }
                    }
                    else
                    {
                        log.Error("[bot, xprinter, Receipt not found] \r\n" + receiptId.ToString());
                        return NotFound();
                    }
                }
            }
        }

        /// <summary>
        /// 2017.06.15 paulus:
        /// 將 email receipt 集中由 xFilm5.Bot 處理，
        /// 於是，xFilm5.VWG 同 xFilm5.REST 都可以用
        /// </summary>
        /// <param name="jsonData"></param>
        /// <returns></returns>
        [Route("email/receipt")]
        public IHttpActionResult PostEmailReceipt([FromBody] JObject jsonData)
        {
            if (jsonData == null)
            {
                log.Fatal("[bot, email receipt] jsonData == null");
                return NotFound();
            }
            else
            {
                #region extra parameters from json data
                int receiptId = jsonData["ReceiptId"].Value<int>();
                int languageId = jsonData["LanguageId"].Value<int>();
                string recipient = jsonData["Recipient"].Value<string>();
                #endregion

                Config.CurrentLanguageId = languageId;

                using (var ctx = new xFilmEntities())
                {
                    var hasRows = ctx.vwReceiptDetailsList_Ex.Where(x => x.ReceiptHeaderId == receiptId).Any();
                    if (hasRows)
                    {
                        try
                        {
                            EmailHelper.EmailReceipt(receiptId, recipient);

                            log.Info(String.Format("[bot, email receipt, success] \r\nReceipt Number = {0}\r\nLanguage Id = {1}\r\nRecipients = {2}", receiptId.ToString(), languageId.ToString(), recipient));
                            return Ok();
                        }
                        catch (Exception ex)
                        {
                            log.Error(String.Format("[bot, email receipt, error found] \r\nExceptional Error = {0}", ex.ToString()));
                            return NotFound();
                        }
                    }
                    else
                    {
                        log.Error("[bot, email receipt, Receipt not found] \r\n" + receiptId.ToString());
                        return NotFound();
                    }
                }
            }
        }

        /// <summary>
        /// 2018.09.17 paulus:
        /// 將 email send via smtp 集中由 xFilm5.Bot 處理，
        /// </summary>
        /// <param name="jsonData"></param>
        /// <returns></returns>
        [Route("email/smtp")]
        public IHttpActionResult PostEmailSmtp([FromBody] JObject jsonData)
        {
            if (jsonData == null)
            {
                log.Fatal("[bot, email smtp] jsonData == null");
                return BadRequest();    // 400
            }
            else
            {
                #region extra parameters from json data
                var recipientStr = jsonData["Recipient"].Value<string>();
                var recipients = EmailHelper.SplitRecipient(recipientStr);
                var subject = jsonData["Subject"].Value<string>();
                var message = jsonData["Message"].Value<string>();
                #endregion

                if (recipients != null)
                {
                    using (var ctx = new xFilmEntities())
                    {
                        if (recipients.Length > 0)
                        {
                            var result = false;
                            foreach (var recipient in recipients)
                            {
                                result = EmailHelper.EmailMessageSMTP(recipient, subject, message);
                                if (!result) break;
                            }
                            if (result)
                            {
                                log.Info(String.Format("[bot, email smtp, success] \r\nRecipients = {0}\r\nSubject = {1}\r\nMessage = {2}", recipientStr, subject, message));
                                return Ok();
                            }
                            else
                            {
                                log.Error(String.Format("[bot, email smtp, error found] \r\n{0}", jsonData.ToString()));
                                return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "error found...aborted"));
                                //return NotFound();
                            }
                        }
                        else
                        {
                            log.Error("[bot, email smtp, no smtp recipient] \r\n" + jsonData.ToString());
                            return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "empty recipient"));
                            //return NotFound();  // 404
                        }
                    }
                }
                else
                {
                    log.Error("[bot, email smtp, no smtp recipient] \r\n" + jsonData.ToString());
                    return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "null recipient"));
                    //return NotFound();      // 404
                }
            }
        }
    }
}
