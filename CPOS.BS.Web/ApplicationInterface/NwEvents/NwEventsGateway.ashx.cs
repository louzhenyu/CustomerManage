using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Web.Session;
using JIT.CPOS.DTO.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using JIT.Utility.ExtensionMethod;
using JIT.Utility.Log;
using System.Data;
using JIT.CPOS.Web.ApplicationInterface.Util.SMS;
using JIT.Utility;
using JIT.CPOS.BS.Web.Base.Excel;
using JIT.CPOS.Common;
using Aspose.Cells;

namespace JIT.CPOS.BS.Web.ApplicationInterface.NwEvents
{
    /// <summary>
    /// NwEventsGateway 的摘要说明
    /// </summary>
    public partial class NwEventsGateway : BaseGateway
    {
        private JIT.CPOS.BS.Entity.LoggingSessionInfo PrivateLoggingSessionInfo { get { return new SessionManager().CurrentUserLoginInfo; } }
        //private JIT.CPOS.BS.Entity.LoggingSessionInfo PrivateLoggingSessionInfo = new Entity.LoggingSessionInfo()
        //{
        //    CurrentUser = new Entity.User.UserInfo() { User_Id = "7c292994c45143028cbf0b60c9555aec", customer_id = "e703dbedadd943abacf864531decdac1" },
        //    CurrentLoggingManager = new Entity.LoggingManager() { Connection_String = "user id=dev;password=jit!2014;data source=112.124.68.147;database=cpos_bs_lj;", Customer_Id = "e703dbedadd943abacf864531decdac1", Customer_Code = "Lzlj", Customer_Name = "泸州老窖", User_Id = "null},'CurrentLoggingManager':{'Customer_Id':'e703dbedadd943abacf864531decdac1','Customer_Code':'Lzlj','Customer_Name':'泸州老窖','User_Id':'7c292994c45143028cbf0b60c9555aec", User_Name = "管理员" },
        //    ClientID = "e703dbedadd943abacf864531decdac1"
        //};
        private LEventSignUpBLL PrivateLEventSignUpBLL { get { return new LEventSignUpBLL(PrivateLoggingSessionInfo); } }
        private LEventsBLL PrivateLEventsBLL { get { return new LEventsBLL(PrivateLoggingSessionInfo); } }

        #region 接口请求及响应数据结构
        #region 1.1 获取报名列表
        /// <summary>
        /// 接口请求信息
        /// </summary>
        class GetEventSignUpRP : IAPIRequestParameter
        {
            /// <summary>
            /// 校验请求参数
            /// </summary>
            public void Validate()
            {
                if (string.IsNullOrEmpty(EventId))
                {
                    throw new ArgumentNullException("活动ID不能为空!");
                }
            }
            /// <summary>
            /// 活动ID
            /// </summary>
            public string EventId { get; set; }
            /// <summary>
            /// 状态值：未确认(1)、已确认(10)
            /// </summary>
            public int Status { get; set; }
            /// <summary>
            /// 当前页
            /// </summary>
            public int PageIndex { get; set; }
            /// <summary>
            /// 页大小
            /// </summary>
            public int PageSize { get; set; }
        }
        /// <summary>
        /// 接口响应信息
        /// </summary>
        class GetEventSignUpRD : IAPIResponseData
        {
            public Dictionary<string, string> DicColNames = new Dictionary<string, string>();
            /// <summary>
            /// 报名人员信息集合
            /// </summary>
            public DataTable SignUpList { get; set; }
            /// <summary>
            /// 未确认报名总数
            /// </summary>
            public int TotalCountUn { get; set; }
            /// <summary>
            /// 已确认报名总数
            /// </summary>
            public int TotalCountYet { get; set; }
            /// <summary>
            /// 总页数
            /// </summary>
            public int TotalPage { get; set; }
        }
        #endregion

        #region 1.2 确定参加报名
        /// <summary>
        /// 接口请求信息
        /// </summary>
        class SetEventSignUpRP : IAPIRequestParameter
        {
            /// <summary>
            /// 校验请求参数
            /// </summary>
            public void Validate()
            {
                if (string.IsNullOrEmpty(SignUpId))
                {
                    throw new ArgumentNullException("活动报名ID不能为空!");
                }
            }
            /// <summary>
            /// 活动报名ID
            /// </summary>
            public string SignUpId { get; set; }
        }
        /// <summary>
        /// 接口响应信息
        /// </summary>
        class SetEventSignUpRD : EmptyResponseData
        {
        }
        #endregion

        #region 1.3 发送通知
        /// <summary>
        /// 接口请求信息
        /// </summary>
        class NotifyEventSignUpRP : IAPIRequestParameter
        {
            /// <summary>
            /// 校验请求参数
            /// </summary>
            public void Validate()
            {
                if (string.IsNullOrEmpty(SignUpIds))
                {
                    throw new ArgumentNullException("活动报名IDS不能为空!");
                }
            }
            /// <summary>
            /// 活动报名IDS
            /// </summary>
            public string SignUpIds { get; set; }
        }
        /// <summary>
        /// 接口响应信息
        /// </summary>
        class NotifyEventSignUpRD : EmptyResponseData
        {
        }
        #endregion

        #region 1.4 设置分组
        /// <summary>
        /// 接口请求信息
        /// </summary>
        class SetSignUpGroupRP : IAPIRequestParameter
        {
            /// <summary>
            /// 校验请求参数
            /// </summary>
            public void Validate()
            {
                if (string.IsNullOrEmpty(SignUpId))
                {
                    throw new ArgumentNullException("活动报名ID不能为空!");
                }
            }
            /// <summary>
            /// 活动报名人员ID
            /// </summary>
            public string SignUpId { get; set; }
            /// <summary>
            /// 分组名
            /// </summary>
            public string GroupName { get; set; }
        }
        /// <summary>
        /// 接口响应信息
        /// </summary>
        class SetSignUpGroupRD : EmptyResponseData
        {
        }
        #endregion

        #region 1.5 获取签到列表
        /// <summary>
        /// 接口请求信息
        /// </summary>
        class GetEventUserMappingRP : GetEventSignUpRP
        {

        }
        /// <summary>
        /// 接口响应信息
        /// </summary>
        class GetEventUserMappingRD : IAPIResponseData
        {
            /// <summary>
            /// 返回数据集
            /// </summary>
            public DataTable MappingList { get; set; }
            /// <summary>
            /// 总记录条数
            /// </summary>
            public int TotalCount { get; set; }
            /// <summary>
            /// 总页数
            /// </summary>
            public int TotalPage { get; set; }
        }
        #endregion

        #region 1.6 签到
        /// <summary>
        /// 接口请求信息
        /// </summary>
        class EventCheckInRP : IAPIRequestParameter
        {
            /// <summary>
            /// 校验请求参数
            /// </summary>
            public void Validate()
            {
                if (string.IsNullOrEmpty(RegCode) && string.IsNullOrEmpty(Mobile))
                {
                    throw new ArgumentNullException("签到码或手机号码不能同时为空！");
                }
            }
            /// <summary>
            /// 签到码，6位数字
            /// </summary>
            public string RegCode { get; set; }
            /// <summary>
            /// 手机号码
            /// </summary>
            public string Mobile { get; set; }
        }
        /// <summary>
        /// 接口响应信息
        /// </summary>
        class EventCheckInRD : IAPIResponseData
        {
            /// <summary>
            /// True 代表签到成功，False 代表签到失败
            /// </summary>
            public bool IsOk { get; set; }
            /// <summary>
            /// 签到返回结果：成功则有返回数据，失败则返回空。
            /// </summary>
            public Dictionary<string, string> CheckinInfos { get; set; }
        }
        #endregion

        #region 1.7 签到成功获取组信息
        /// <summary>
        /// 接口请求信息
        /// </summary>
        class GetCheckInGroupsRP : IAPIRequestParameter
        {
            /// <summary>
            /// 校验请求参数
            /// </summary>
            public void Validate()
            {
                if (string.IsNullOrEmpty(EventId))
                {
                    throw new ArgumentNullException("活动ID不能为空！");
                }
                if (string.IsNullOrEmpty(GroupName))
                {
                    throw new ArgumentNullException("组名不能为空！");
                }
            }
            /// <summary>
            /// 活动ID
            /// </summary>
            public string EventId { get; set; }
            /// <summary>
            /// 组名
            /// </summary>
            public string GroupName { get; set; }
        }
        /// <summary>
        /// 接口响应信息
        /// </summary>
        class GetCheckInGroupsRD : IAPIResponseData
        {
            /// <summary>
            /// 分组信息列表
            /// </summary>
            public DataTable GroupList { get; set; }
        }
        #endregion
        #endregion

        #region 接口处理逻辑
        #region 1.1 获取报名列表
        protected string DoGetEventSignUp(string pRequest)
        {
            //1、请求参数反序列化
            var rp = pRequest.DeserializeJSONTo<APIRequest<GetEventSignUpRP>>();

            //2、参数校验
            rp.Parameters.Validate();

            //3、构造响应数据
            var rd = new APIResponse<GetEventSignUpRD>(new GetEventSignUpRD());
            try
            {
                //4、获取当前用户登录信息

                //5、获取数据信息
                int pageCount = 0;
                int rowCountUn = 0;
                int rowCountYet = 0;
                Dictionary<string, string> dicCloumnNames = new Dictionary<string, string>();
                rd.Data.SignUpList = PrivateLEventSignUpBLL.GetEventSignUpList(rp.Parameters.EventId, rp.Parameters.Status, rp.Parameters.PageIndex, rp.Parameters.PageSize, out pageCount, out rowCountUn, out rowCountYet, out dicCloumnNames);
                rd.Data.TotalPage = pageCount;
                rd.Data.TotalCountUn = rowCountUn;
                rd.Data.TotalCountYet = rowCountYet;
                rd.Data.DicColNames = dicCloumnNames;
            }
            catch (Exception ex)
            {
                Loggers.Exception(new ExceptionLogInfo(ex));
                throw new Exception(ex.Message, ex);
            }
            return rd.ToJSON();
        }
        #endregion

        #region 1.2 确定参加报名
        protected string DoSetEventSignUp(string pRequest)
        {
            //1、请求参数反序列化
            var rp = pRequest.DeserializeJSONTo<APIRequest<SetEventSignUpRP>>();

            //2、参数校验
            rp.Parameters.Validate();

            //3、构造响应数据
            var rd = new APIResponse<SetEventSignUpRD>(new SetEventSignUpRD());
            try
            {
                //4、获取当前用户登录信息

                //5、确定
                PrivateLEventSignUpBLL.SetEventSignUpOper(10, rp.Parameters.SignUpId, rp.UserID, CharsFactory.Create6Char());
            }
            catch (Exception ex)
            {
                Loggers.Exception(new ExceptionLogInfo(ex));
                throw new Exception(ex.Message, ex);
            }
            return rd.ToJSON();
        }
        #endregion

        #region 1.3 发送通知
        protected string DoNotifyEventSignUp(string pRequest)
        {
            //1、请求参数反序列化
            var rp = pRequest.DeserializeJSONTo<APIRequest<NotifyEventSignUpRP>>();

            //2、参数校验
            rp.Parameters.Validate();

            //3、构造响应数据
            var rd = new APIResponse<NotifyEventSignUpRD>(new NotifyEventSignUpRD());
            try
            {
                //4、获取当前用户登录信息

                //5、获取
                DataTable dtRes = PrivateLEventSignUpBLL.GetNotifyEventSignUpList(rp.Parameters.SignUpIds);
                if (dtRes != null)
                {
                    foreach (DataRow row in dtRes.Rows)
                    {
                        #region 发送短信
                        string msg;

                        if (!SMSHelper.Send(row["Phone"].ToString(), row["RegistrationCode"].ToString(), "阿拉丁", out msg))
                        {
                            throw new Exception("短信发送失败:" + msg);
                        }

                        PrivateLEventSignUpBLL.SetEventSignUpStatus(row["SignUpID"].ToString(), 1, 1, rp.UserID);
                        #endregion
                    }
                }
            }
            catch (Exception ex)
            {
                Loggers.Exception(new ExceptionLogInfo(ex));
                throw new Exception(ex.Message, ex);
            }
            return rd.ToJSON();
        }
        #endregion

        #region 1.4 设置分组
        protected string DoSetSignUpGroup(string pRequest)
        {
            //1、请求参数反序列化
            var rp = pRequest.DeserializeJSONTo<APIRequest<SetSignUpGroupRP>>();

            //2、参数校验
            rp.Parameters.Validate();

            //3、构造响应数据
            var rd = new APIResponse<SetSignUpGroupRD>(new SetSignUpGroupRD());
            try
            {
                //4、获取当前用户登录信息

                //5、设置分组
                PrivateLEventSignUpBLL.SetSignUpGroup(rp.Parameters.SignUpId, rp.Parameters.GroupName, rp.UserID);
            }
            catch (Exception ex)
            {
                Loggers.Exception(new ExceptionLogInfo(ex));
                throw new Exception(ex.Message, ex);
            }
            return rd.ToJSON();
        }
        #endregion

        #region 1.5 获取签到列表
        protected string DoGetEventUserMapping(string pRequest)
        {
            //1、请求参数反序列化
            var rp = pRequest.DeserializeJSONTo<APIRequest<GetEventUserMappingRP>>();

            //2、参数校验
            rp.Parameters.Validate();

            //3、构造响应数据
            var rd = new APIResponse<GetEventUserMappingRD>(new GetEventUserMappingRD());
            try
            {
                //4、获取当前用户登录信息

                //5、获取签到列表信息
                int pageCount = 0;
                int rowCount = 0;
                rd.Data.MappingList = PrivateLEventSignUpBLL.GetEventUserMappingList(rp.Parameters.EventId, rp.Parameters.PageIndex, rp.Parameters.PageSize, out rowCount, out pageCount);
                rd.Data.TotalPage = pageCount;
                rd.Data.TotalCount = rowCount;
            }
            catch (Exception ex)
            {
                Loggers.Exception(new ExceptionLogInfo(ex));
                throw new Exception(ex.Message, ex);
            }
            return rd.ToJSON();
        }
        #endregion

        #region 1.6 签到
        protected string DoEventCheckIn(string pRequest)
        {
            //1、请求参数反序列化
            var rp = pRequest.DeserializeJSONTo<APIRequest<EventCheckInRP>>();

            //2、参数校验
            rp.Parameters.Validate();

            //3、构造响应数据
            var rd = new APIResponse<EventCheckInRD>(new EventCheckInRD());
            try
            {
                //4、获取当前用户登录信息
                var loggingSessionInfo = Default.GetBSLoggingSession(rp.CustomerID, rp.UserID);

                //5、签到
                Dictionary<string, string> dicInfo = new Dictionary<string, string>();
                rd.Data.IsOk = new LEventSignUpBLL(loggingSessionInfo).EventCheckInInfo("", rp.Parameters.RegCode, rp.Parameters.Mobile, out dicInfo);
                rd.Data.CheckinInfos = dicInfo;
            }
            catch (Exception ex)
            {
                Loggers.Exception(new ExceptionLogInfo(ex));
                throw new Exception(ex.Message, ex);
            }
            return rd.ToJSON();
        }
        #endregion

        #region 1.7 签到成功获取组信息
        protected string DoGetCheckInGroups(string pRequest)
        {
            //1、请求参数反序列化
            var rp = pRequest.DeserializeJSONTo<APIRequest<GetCheckInGroupsRP>>();

            //2、参数校验
            rp.Parameters.Validate();

            //3、构造响应数据
            var rd = new APIResponse<GetCheckInGroupsRD>(new GetCheckInGroupsRD());
            try
            {
                //4、获取当前用户登录信息
                var loggingSessionInfo = Default.GetBSLoggingSession(rp.CustomerID, rp.UserID);

                //5、签到
                rd.Data.GroupList = new LEventSignUpBLL(loggingSessionInfo).GetCheckInGroupsList(rp.Parameters.EventId, rp.Parameters.GroupName);
            }
            catch (Exception ex)
            {
                Loggers.Exception(new ExceptionLogInfo(ex));
                throw new Exception(ex.Message, ex);
            }
            return rd.ToJSON();
        }
        #endregion

        #region 下载报名模板
        private string DownEnrollTpl(string pRequest)
        {
            var rp = pRequest.DeserializeJSONTo<APIRequest<JIT.CPOS.BS.BLL.LEventSignUpBLL.DownEnrollTplRP>>();
            rp.Parameters.Validate();

            var rd = new EmptyRD();   // new JIT.CPOS.BS.BLL.LEventSignUpBLL.DownEnrollTplRD();
            DataTable dataTable = PrivateLEventSignUpBLL.DownEnrollTpl(rp.Parameters.EventID, rp.Parameters.Status);

            //数据获取
            Workbook wb = DataTableExporter.WriteXLS(dataTable, 0);
            string savePath = HttpContext.Current.Server.MapPath(@"~/File/Excel");
            if (!System.IO.Directory.Exists(savePath))
            {
                System.IO.Directory.CreateDirectory(savePath);
            }
            savePath = savePath + "\\报名人员模板-" + dataTable.TableName + ".xls";
            wb.Save(savePath);//保存Excel文件
            new ExcelCommon().OutPutExcel(HttpContext.Current, savePath);
            HttpContext.Current.Response.End();

            var rsp = new SuccessResponse<IAPIResponseData>(rd);

            if (dataTable == null || !(dataTable.Columns.Count > 0))
            {
                rsp.ResultCode = 201;
                rsp.Message = "生成失败!";
            }

            return rsp.ToJSON();
        }
        #endregion

        #region 导入报名列表
        private string ImportEnrollData(string pRequest)
        {
            HttpFileCollection files = HttpContext.Current.Request.Files;
            string errorMessage = "";
            //1. 保存上传的文件
            //string fileName = Utils.SaveUploadAttachment(files, PrivateLoggingSessionInfo.ClientID, PrivateLoggingSessionInfo.UserID, out errorMessage);
            string fileName = Utils.SaveUploadAttachment(files, "PrivateLoggingSessionInfo.ClientID", "PrivateLoggingSessionInfo.UserID", out errorMessage);

            var rp = pRequest.DeserializeJSONTo<APIRequest<JIT.CPOS.BS.BLL.LEventSignUpBLL.DownEnrollTplRP>>();
            rp.Parameters.Validate();

            var rd = new JIT.CPOS.BS.BLL.LEventSignUpBLL.DownEnrollTplRD();
            int total = 0, add = 0, update = 0;
            string result = PrivateLEventSignUpBLL.ImportEnrollData(fileName, rp.Parameters, out total, out add, out update);

            var rsp = new SuccessResponse<JIT.CPOS.BS.BLL.LEventSignUpBLL.DownEnrollTplRD>(rd);

            if (!string.IsNullOrEmpty(result))
            {
                rsp.ResultCode = 201;
                rsp.Message = result;
                rsp.Data.Total = total;
                rsp.Data.Add = add;
                rsp.Data.Update = update;
            }

            return rsp.ToJSON();
        }
        #endregion

        #region 获取活动类型列表
        private string EventTypeList(string pRequest)
        {
            var rp = pRequest.DeserializeJSONTo<APIRequest<EmptyRequestParameter>>();
            rp.Parameters.Validate();

            var rd = new JIT.CPOS.BS.BLL.LEventSignUpBLL.EventTypeListRD();
            rd.EventTypeList = PrivateLEventSignUpBLL.EventTypeList();

            var rsp = new SuccessResponse<IAPIResponseData>(rd);
            return rsp.ToJSON();
        }
        #endregion

        #region 获取活动主办方列表
        private string EventSponsorList(string pRequest)
        {
            var rp = pRequest.DeserializeJSONTo<APIRequest<EmptyRequestParameter>>();
            rp.Parameters.Validate();

            var rd = new JIT.CPOS.BS.BLL.LEventSignUpBLL.EventSponsorListRD();
            rd.EventSponsorList = PrivateLEventSignUpBLL.EventSponsorList();

            var rsp = new SuccessResponse<IAPIResponseData>(rd);
            return rsp.ToJSON();
        }
        #endregion

        #region 获取活动状态列表
        private string EventStatusList(string pRequest)
        {
            var rp = pRequest.DeserializeJSONTo<APIRequest<EmptyRequestParameter>>();
            rp.Parameters.Validate();

            var rsp = PrivateLEventsBLL.EventStatusList();

            return rsp.ToJSON();
        }
        #endregion

        #region 保存活动
        private string EventSave(string pRequest)
        {
            var rp = pRequest.DeserializeJSONTo<APIRequest<JIT.CPOS.BS.BLL.LEventsBLL.EventSaveRP>>();
            if (rp.Parameters.EventEntity.IsDelete != "1")
                rp.Parameters.Validate();

            var rsp = new SuccessResponse<IAPIResponseData>();

            rsp = PrivateLEventsBLL.EventSave(rp.Parameters);

            return rsp.ToJSON();
        }
        #endregion

        #region 活动列表
        private string EventList(string pRequest)
        {
            var rp = pRequest.DeserializeJSONTo<APIRequest<JIT.CPOS.BS.BLL.LEventsBLL.EventListRP>>();
            rp.Parameters.Validate();

            var rsp = PrivateLEventsBLL.EventList(rp.Parameters);

            return rsp.ToJSON();
        }
        #endregion

        #region 获取活动详情
        private string EventGet(string pRequest)
        {
            var rp = pRequest.DeserializeJSONTo<APIRequest<JIT.CPOS.BS.BLL.LEventsBLL.EventGetRP>>();
            rp.Parameters.Validate();

            var rsp = PrivateLEventsBLL.EventGet(rp.Parameters);
            return rsp.ToJSON();
        }
        #endregion

        #region 获取动态属性
        private string DynamicFormLoad(string pRequest)
        {
            MobileModuleBLL mobileModuleBLL = new MobileModuleBLL(PrivateLoggingSessionInfo);
            DataSet dataSet = mobileModuleBLL.DynamicFormLoad("", "LEventSignUp");
            dataSet.Tables.RemoveAt(0); //remove the form name table

            var rd = new JIT.CPOS.BS.BLL.LEventsBLL.EventRD();
            rd.Event = new LEventsBLL.EventEntity();
            rd.Event.FieldList = PrivateLEventsBLL.GetFieldList(dataSet);
            var rsp = new SuccessResponse<IAPIResponseData>(rd);

            return rsp.ToJSON();
        }        
        #endregion

        #endregion

        /// <summary>
        /// 处理请求
        /// </summary>
        /// <param name="pType"></param>
        /// <param name="pAction"></param>
        /// <param name="pRequest"></param>
        /// <returns></returns>
        protected override string ProcessAction(string pType, string pAction, string pRequest)
        {
            string rst = string.Empty;
            switch (pAction)
            {
                //Test
                case "GetEventSignUp":
                    rst = this.DoGetEventSignUp(pRequest);
                    break;
                case "SetEventSignUp":
                    rst = this.DoSetEventSignUp(pRequest);
                    break;
                case "NotifyEventSignUp":
                    rst = this.DoNotifyEventSignUp(pRequest);
                    break;
                case "SetSignUpGroup":
                    rst = this.DoSetSignUpGroup(pRequest);
                    break;
                case "DownEnrollTpl"://下载报名模板
                    rst = this.DownEnrollTpl(pRequest);
                    break;
                case "GetEventUserMapping":
                    rst = this.DoGetEventUserMapping(pRequest);
                    break;
                case "EventCheckIn":
                    rst = this.DoEventCheckIn(pRequest);
                    break;
                case "GetCheckInGroups":
                    rst = this.DoGetCheckInGroups(pRequest);
                    break;
                case "ImportEnrollData"://下载报名模板
                    rst = this.ImportEnrollData(pRequest);
                    break;
                case "EventTypeList"://获取活动类型列表
                    rst = this.EventTypeList(pRequest);
                    break;
                case "EventSponsorList"://获取活动主办方列表
                    rst = this.EventSponsorList(pRequest);
                    break;
                case "EventStatusList"://获取活动主办方列表
                    rst = this.EventStatusList(pRequest);
                    break;
                case "EventSave"://保存活动
                    rst = this.EventSave(pRequest);
                    break;
                case "EventList"://活动列表
                    rst = this.EventList(pRequest);
                    break;
                case "EventGet"://获取活动
                    rst = this.EventGet(pRequest);
                    break;
                case "DynamicFormLoad": //获取动态属性
                    rst = this.DynamicFormLoad(pRequest);
                    break;
                default:
                    throw new APIException(string.Format("找不到名为：{0}的action处理方法.", pAction))
                    {
                        ErrorCode = ERROR_CODES.INVALID_REQUEST_CAN_NOT_FIND_ACTION_HANDLER
                    };
            }

            return rst;
        }

    }
}