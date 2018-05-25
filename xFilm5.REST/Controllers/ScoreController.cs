using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using xFilm5.EF6;
using xFilm5.REST.Filters;

namespace xFilm5.REST.Controllers
{
    public class ScoreController : ApiController
    {
        private DateTime _DateZero = new DateTime(2017, 5, 1);

        [HttpGet]
        [Route("api/Score/ByMonth/{id:int}/{date:DateTime}/{workshop?}")]
        [JwtAuthentication]
        public IHttpActionResult GetScoreByMonth(int id, DateTime date, String workshop = null)
        {
            var pClient = new SqlParameter("@ClientId", id);
            var pYear = new SqlParameter("@Year", date.Year);
            var pMonth = new SqlParameter("@Month", date.Month);
            var pWorkshop = new SqlParameter("@Workshop", workshop);

            if (id == 0)
            {
                #region All distinct clients within the same month
                using (var ctx = new xFilmEntities())
                {
                    try
                    {
                        #region 睇吓使唔使 filter by workshop
                        var addFilter = false;
                        if (!(String.IsNullOrEmpty(workshop)))
                        {
                            //如果 workshop 係 exist 嘅，淨係 return 同一個 workshop 嘅 order
                            addFilter = ctx.vwWorkshopList.Where(x => x.WorkshopName == workshop).Any();
                        }
                        #endregion

                        if (addFilter)
                        {
                            var list = ctx.Database.SqlQuery<Score>("dbo.apRESTscoreByMonthByWorkshop @Year, @Month, @Workshop", pYear, pMonth, pWorkshop).ToList();
                            return Json(list);
                        }
                        else
                        {
                            var list = ctx.Database.SqlQuery<Score>("dbo.apRESTscoreByMonth @Year, @Month", pYear, pMonth).ToList(); ;
                            return Json(list);
                        }
                    }
                    catch (Exception e)
                    {
                        throw e;
                    }
                }
                #endregion
            }
            else
            {
                #region All receipts of a single client within the same month
                using (var ctx = new xFilmEntities())
                {
                    try
                    {
                        var list = ctx.Database.SqlQuery<Score>("dbo.apRESTscoreByMonthByClient @ClientId, @Year, @Month", pClient, pYear, pMonth).ToList();
                        return Json(list);
                    }
                    catch (Exception e)
                    {
                        throw e;
                    }
                }
                #endregion
            }
        }

        [HttpGet]
        [Route("api/Score/ByYear/{id:int}/{date:DateTime}/{workshop?}")]
        [JwtAuthentication]
        public IHttpActionResult GetScoreByYear(int id, DateTime date, String workshop = null)
        {
            var pClient = new SqlParameter("@ClientId", id);
            var pYear = new SqlParameter("@Year", date.Year);
            var pMonth = new SqlParameter("@Month", Convert.ToInt32(0));
            var pWorkshop = new SqlParameter("@Workshop", workshop);

            if (id == 0)
            {
                #region All distinct clients within the same month
                using (var ctx = new xFilmEntities())
                {
                    try
                    {
                        #region 睇吓使唔使 filter by workshop
                        var addFilter = false;
                        if (!(String.IsNullOrEmpty(workshop)))
                        {
                            //如果 workshop 係 exist 嘅，淨係 return 同一個 workshop 嘅 order
                            addFilter = ctx.vwWorkshopList.Where(x => x.WorkshopName == workshop).Any();
                        }
                        #endregion

                        if (addFilter)
                        {
                            var list = ctx.Database.SqlQuery<Score>("dbo.apRESTscoreByMonthByWorkshop @Year, @Month, @Workshop", pYear, pMonth, pWorkshop).ToList(); ;
                            return Json(list);
                        }
                        else
                        {
                            var list = ctx.Database.SqlQuery<Score>("dbo.apRESTscoreByMonth @Year, @Month", pYear, pMonth).ToList(); ;
                            return Json(list);
                        }
                    }
                    catch (Exception e)
                    {
                        throw e;
                    }
                }
                #endregion
            }
            else
            {
                #region All receipts of a single client within the same month
                using (var ctx = new xFilmEntities())
                {
                    try
                    {
                        var list = ctx.Database.SqlQuery<Score>("dbo.apRESTscoreByMonthByClient @ClientId, @Year, @Month", pClient, pYear, pMonth).ToList();
                        return Json(list);
                    }
                    catch (Exception e)
                    {
                        throw e;
                    }
                }
                #endregion
            }
        }
    }
}
