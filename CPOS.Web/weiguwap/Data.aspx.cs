using System;
using System.Collections.Generic;
using System.Text;
using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Entity;
using JIT.Utility.DataAccess.Query;
using JIT.Utility.ExtensionMethod;

namespace JIT.CPOS.Web.weiguwap
{
    public partial class Data : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Clear();
            string content = string.Empty;
            try
            {
                string dataType = Request["action"].Trim();
                switch (dataType)
                {
                    case "getServiceList":
                        content = getServiceList();
                        break;
                    case "getServiceDateList":
                        content = getServiceDateList();
                        break;
                    case "getBigClassTermList":
                        content = getBigClassTermList();
                        break;
                    case "getSmallClassTermList":
                        content = getSmallClassTermList();
                        break;
                    case "getReservationList":
                        content = getReservationList();
                        break;
                    case "cancelReservation":
                        content = cancelReservation();
                        break;
                    case "validLogin":
                        content = validLogin();
                        break;
                    case "userLogin":
                        content = userLogin();
                        break;
                    case "submitReservation":
                        content = submitReservation();
                        break;
                }
            }
            catch (Exception ex)
            {
                content = ex.Message;
            }
            Response.ContentEncoding = Encoding.UTF8;
            Response.Write(content);
            Response.End();
        }

        private string submitReservation()
        {
            var respObj = new RespData();
            try
            {
                string reqContent = Request["ReqContent"];
                var reqObj = reqContent.DeserializeJSONTo<ReqContent>();
                var service = new ReservationServiceScheduleBLL(Default.GetLoggingSession());
                ReservationServiceScheduleEntity entity =new ReservationServiceScheduleEntity();
                
                entity.ReserveDate = DateTime.Parse(reqObj.Special.currentDate);
                entity.ReservationServiceBigClassTermID = int.Parse(reqObj.Special.reservationBigClassTermId);
                entity.ReservationServiceSmallClassTermID = int.Parse(reqObj.Special.reservationSmallClassTermId);
                entity.ReservationServiceID = int.Parse(reqObj.Special.reservationServiceId);
                entity.VIPID = reqObj.Common.userId;
                entity.ReservationServiceScheduleID = Guid.NewGuid().ToString().Replace("-", "");
                
                service.Create(entity);
                respObj.code = "200";
                respObj.description = "操作成功";
                ValidLogin(reqObj, respObj);

            }
            catch (Exception ex)
            {
                //throw ex;
                respObj.code = "103";
                respObj.description = "数据库操作错误";
            }
            string content = respObj.ToJSON();
            return content;
        }

        private string userLogin()
        {
            var respObj = new RespData();
            try
            {
                string reqContent = Request["ReqContent"];
                var reqObj = reqContent.DeserializeJSONTo<ReqContent>();
                //TODO:登录成功！
                if (reqObj.Common.codeId.Equals(""))
                {

                }
                respObj.code = "200";
                respObj.description = "操作成功";
                respObj.userId = "1";
                respObj.codeId = "8uc3f45";
            }
            catch (Exception ex)
            {
                //throw ex;
                respObj.code = "103";
                respObj.description = "数据库操作错误";
            }
            string content = respObj.ToJSON();
            return content;
        }


        private string validLogin()
        {
            var respObj = new RespData();
            try
            {
                string reqContent = Request["ReqContent"];
                var reqObj = reqContent.DeserializeJSONTo<ReqContent>();
                //TODO:验证登录，默认登录成功！
                if (reqObj.Common.codeId.Equals(""))
                {
                    
                }
                respObj.code = "200";
                respObj.description = "操作成功";
                respObj.userId = "1";
                respObj.codeId = "8uc3f45";
            }
            catch (Exception ex)
            {
                //throw ex;
                respObj.code = "103";
                respObj.description = "数据库操作错误";
            }
            string content = respObj.ToJSON();
            return content;
        }
        private bool ValidLogin(ReqContent req, RespData resp)
        {
            
            if (req.Common.codeId.Equals(""))
            {
                resp.codeId = "";
                resp.userId = "";
                return false;
            }
            //TODO:用户验证客户端缓存是否过期
            if (req.Common.codeId == "8uc3f45")
            {
                resp.userId = "1";
                resp.codeId = "8uc3f45";
                return true;
            }
            resp.codeId = "";
            resp.userId = "";
            return false;
        }

        private string cancelReservation()
        {
            var respObj = new RespData();
            try
            {
                string reqContent = Request["ReqContent"];
                var reqObj = reqContent.DeserializeJSONTo<ReqContent>();
                var service = new ReservationServiceScheduleBLL(Default.GetLoggingSession());
                ReservationServiceScheduleEntity entity = service.GetByID(reqObj.Special.ReservationServiceScheduleID);
                if (entity != null && entity.VIPID.Equals(reqObj.Common.userId))
                {
                    //TODO:取消时的状态转换
                    entity.StatusID = 0;
                }
                service.Update(entity);
                respObj.code = "200";
                respObj.description = "操作成功";
                ValidLogin(reqObj, respObj);

            }
            catch (Exception ex)
            {
                //throw ex;
                respObj.code = "103";
                respObj.description = "数据库操作错误";
            }
            string content = respObj.ToJSON();
            return content;
        }

        private string getReservationList()
        {
            var respObj = new RespData();
            try
            {
                string reqContent = Request["ReqContent"];
                var reqObj = reqContent.DeserializeJSONTo<ReqContent>();
                //TODO:判断登陆
                
                var service = new ReservationServiceScheduleBLL(Default.GetLoggingSession());
                IWhereCondition[] whereCondition = new IWhereCondition[] {
                    new EqualsCondition() { FieldName = "a.VIPID", Value = reqObj.Common.userId },
                    //new EqualsCondition() { FieldName = "a.ReservationStoreID", Value = reqObj.Common.reservationStoreId },
                    //TODO：维护好状态数据字典
                    //new EqualsCondition() { FieldName = "a.StatusID", Value = reqObj.Special.statusId }
                };
                ReservationServiceScheduleEntity[] entities = service.QueryAllField(whereCondition, null);
                respObj.content = string.Format("DataItems:{{{0}}}", entities.ToJSON());
                respObj.code = "200";
                respObj.description = "操作成功";
                ValidLogin(reqObj, respObj);
            }
            catch (Exception ex)
            {
                //throw ex;
                respObj.code = "103";
                respObj.description = "数据库操作错误";
            }
            string content = respObj.ToJSON();
            return content;
        }

        private string getSmallClassTermList()
        {
            var respObj = new RespData();
            try
            {
                string reqContent = Request["ReqContent"];
                var reqObj = reqContent.DeserializeJSONTo<ReqContent>();
                var service = new ReservationServiceSmallClassTermStatusBLL(Default.GetLoggingSession());
                IWhereCondition[] whereCondition = new IWhereCondition[] {
                    new EqualsCondition() { FieldName = "reservationServiceId ", Value = reqObj.Special.reservationServiceId },
                    new EqualsCondition() { FieldName = "reservationServiceBigClassTermId ", Value = reqObj.Special.reservationBigClassTermId },
                    new EqualsCondition() { FieldName = "StatusID", Value = reqObj.Special.statusId }
                };
                ReservationServiceSmallClassTermStatusEntity[] entities = service.Query(whereCondition, null);
                respObj.content = string.Format("DataItems:{{{0}}}", entities.ToJSON());
                respObj.code = "200";
                respObj.description = "操作成功";
                ValidLogin(reqObj, respObj);
            }
            catch (Exception ex)
            {
                //throw ex;
                respObj.code = "103";
                respObj.description = "数据库操作错误";
            }
            string content = respObj.ToJSON();
            return content;
        }

        private string getBigClassTermList()
        {
            var respObj = new RespData();
            try
            {
                string reqContent = Request["ReqContent"];
                var reqObj = reqContent.DeserializeJSONTo<ReqContent>();

                var service = new ReservationServiceBigClassTermStatusBLL(Default.GetLoggingSession());
                IWhereCondition[] whereCondition = new IWhereCondition[] {
                    //new EqualsCondition() { FieldName = "ReservationStoreID", Value = reqObj.Common.reservationStoreId },
                    new EqualsCondition() { FieldName = "reservationServiceId ", Value = reqObj.Special.reservationServiceId },
                    //new EqualsCondition() { FieldName = "ReserveDate ", Value = reqObj.Special.currentDate },
                    new EqualsCondition() { FieldName = "StatusID", Value = reqObj.Special.statusId }
                };
                ReservationServiceBigClassTermStatusEntity[] entities = service.Query(whereCondition, null);
                respObj.content = string.Format("DataItems:{{{0}}}", entities.ToJSON());
                respObj.code = "200";
                respObj.description = "操作成功";
                ValidLogin(reqObj, respObj);
            }
            catch (Exception ex)
            {
                //throw ex;
                respObj.code = "103";
                respObj.description = "数据库操作错误";
            }
            string content = respObj.ToJSON();
            return content;
        }
        /// <summary>
        /// 获取可选日期列表
        /// </summary>
        /// <returns></returns>
        private static string getServiceDateList()
        {
            var respObj = new RespData();
            try
            {
                IList<DateList> entities = new List<DateList>();
                for (int i = 0; i < 20; i++)
                {
                    DateList dateList = new DateList
                        {
                            CurrentDate = DateTime.Now.AddDays(i).ToShortDateString(),
                            Status = "0",
                            MonthAndDay = DateTime.Now.AddDays(i).ToString("MM月dd日"),
                            Week = ToChineseWeek(DateTime.Now.AddDays(i).DayOfWeek.ToString())
                        };
                    entities.Add(dateList);
                }
                respObj.content = string.Format("DataItems:{{{0}}}", entities.ToJSON());
                respObj.code = "200";
                respObj.description = "操作成功";
            }
            catch (Exception ex)
            {
                respObj.code = "103";
                respObj.description = "数据库操作错误";
            }
            string content = respObj.ToJSON();
            return content;
        }

       
        /// <summary>
        /// 获取服务信息列表
        /// </summary>
        /// <returns></returns>
        private static string getServiceList()
        {
            var respObj = new RespData();
            try
            {
                var service = new ReservationServiceBLL(Default.GetLoggingSession());
                ReservationServiceEntity[] entities = service.GetAll();
                respObj.content = string.Format("DataItems:{{{0}}}", entities.ToJSON());
                respObj.code = "200";
                respObj.description = "操作成功";
                
            }
            catch (Exception ex)
            {
                respObj.code = "103";
                respObj.description = "数据库操作错误";
            }
            string content = respObj.ToJSON();
            return content;
        }
        private static string ToChineseWeek(string XingQi)
        {
            switch (XingQi)
            {
                case "Monday":
                    return "星期一";
                case "Tuesday":
                    return "星期二";
                case "Wednesday":
                    return "星期三";
                case "Thursday":
                    return "星期四";
                case "Friday":
                    return "星期五";
                case "Saturday":
                    return "星期六";
                case "Sunday":
                    return "星期日";
            }
            throw new Exception("转换星期错误！");
        }
    }

    #region 类定义
    /// <summary>
    /// 输出类
    /// </summary>
    public class RespData
    {
        public string code;
        public string description;
        public string codeId;
        public string userId;
        public string content;
    }
    /// <summary>
    /// 请求类
    /// </summary>
    public class ReqContent
    {
        public common Common;
        public special Special;
    }
    public class common
    {
        public string locale { get; set; }
        public string userId { get; set; }
        public string openId { get; set; }
        public string reservationStoreId { get; set; }
        public string codeId { get; set; }
    }
    public class special
    {
        public string reservationServiceId { get; set; }
        public string currentDate { get; set; }
        public string statusId { get; set; }
        public string reservationBigClassTermId { get; set; }
        public string reservationSmallClassTermId { get; set; }

        public string ReservationServiceScheduleID { get; set; }

    }
    public class DateList
    {
        public string CurrentDate;
        public string MonthAndDay;
        public string Week;
        public string Status;
    }
    #endregion
}