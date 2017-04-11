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

namespace xFilm5.Bot.Controllers
{
    public class BotController : ApiController
    {
        private static log4net.ILog Log { get; set; }
        ILog log = log4net.LogManager.GetLogger(typeof(BotController));

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

                using (Models.SysDb context = new Models.SysDb())
                {
                    Models.PrintQueue_VPS vps = context.PrintQueue_VPS.FirstOrDefault(v => v.ID == vpsPqId);
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
                            String fileName = String.Format("{0}.{1}.{2}.tif", filePrefix, vps.PrintQueue.ClientID.ToString(), vps.VpsFileName.Substring(0, vps.VpsFileName.Length - 4));
                            String filePath_Source = Path.Combine(serverUri + sourecPath, fileName);
                            String filePath_Dest = Path.Combine(serverUri + destPath, fileName);

                            if (File.Exists(filePath_Source))
                            {
                                //IEnumerable<string> files = Directory.EnumerateFiles(bpFilePath_Source, "*.dll", SearchOption.AllDirectories);

                                //if (!(Directory.Exists(filePath_Dest))) Directory.CreateDirectory(filePath_Dest);

                                //if (!File.Exists(filePath_Dest))
                                //{
                                try
                                {
                                    File.Copy(filePath_Source, filePath_Dest, true);
                                    return Ok();
                                }
                                catch (Exception e)
                                {
                                    log.Fatal("[bot, blueprint, Copy] \r\n", e);
                                    return NotFound();
                                }
                                //}
                                //else
                                //{
                                //    log.Error(String.Format("[bot, blueprint, dest file exist] \r\nFile Name = {0}\r\nFilePath_Source = {1}\r\nFilePath_Dest = {2}", fileName, filePath_Source, filePath_Dest));
                                //    return NotFound();
                                //}
                            }
                            else
                            {
                                log.Error(String.Format("[bot, blueprint, source file not exist] \r\nFile Name = {0}\r\nFilePath_Source = {1}\r\nFilePath_Dest = {2}", fileName, filePath_Source, filePath_Dest));
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

                using (Models.SysDb context = new Models.SysDb())
                {
                    Models.PrintQueue_VPS vps = context.PrintQueue_VPS.FirstOrDefault(v => v.ID == vpsPqId);
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

                using (Models.SysDb context = new Models.SysDb())
                {
                    Models.PrintQueue_VPS vps = context.PrintQueue_VPS.FirstOrDefault(v => v.ID == vpsPqId);
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

                using (Models.SysDb context = new Models.SysDb())
                {
                    Models.PrintQueue_VPS vps = context.PrintQueue_VPS.FirstOrDefault(v => v.ID == vpsPqId);
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
                                //IEnumerable<string> files = Directory.EnumerateFiles(bpFilePath_Source, "*.dll", SearchOption.AllDirectories);
                                if (!(Directory.Exists(destUri))) Directory.CreateDirectory(destUri);

                                if (!File.Exists(filePath_Dest))
                                {
                                    try
                                    {
                                        File.Copy(filePath_Source, filePath_Dest);
                                        return Ok();
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
    }
}
