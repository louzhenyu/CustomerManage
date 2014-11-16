using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.DTO.Base;
using JIT.CPOS.Web.ApplicationInterface;
using JIT.Utility.Log;
using JIT.Utility.ExtensionMethod;
using JIT.Utility;
using JIT.CPOS.BS.BLL.CS;
using JIT.Utility.DataAccess.Query;
using System.Data;
using System.Configuration;

namespace JIT.CPOS.Web.RateLetterInterface
{
    /// <summary>
    /// QiXinHandler 的摘要说明
    /// </summary>
    public class QiXinHandler : BaseGateway
    {
        protected override string ProcessAction(string pType, string pAction, string pRequest)
        {
            var rst = string.Empty;
            switch (pAction)
            {
                case "SetIOSDeviceToken":
                    rst = SetIOSDeviceToken(pRequest);
                    break;
                case "SendIOSMessage":
                    rst = SendIOSMessage(pRequest);
                    break;
                //case "GetNewDetail":
                //    rst = GetNewDetail(pRequest);
                //    break;
                case "GetNewList":
                    rst = GetNewList(pRequest);
                    break;
                case "UpdateVersion"://升级
                    rst = UpdateVersion(pRequest);
                    break;
                default:
                    throw new APIException(string.Format("未实现Action名为{0}的处理.", pAction)) { ErrorCode = 201 };
            }
            return rst;
        }

        #region 1.0 APP push devicetoken
        public string SetIOSDeviceToken(string reqContent)
        {
            var rd = new APIResponse<SetIOSDeviceTokenRD>();
            var rdData = new SetIOSDeviceTokenRD();

            try
            {
                #region
                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format("setSignUp: {0}", reqContent)
                });

                //解析请求字符串
                var rp = reqContent.DeserializeJSONTo<APIRequest<SetIOSDeviceTokenRP>>();
                if (rp.Parameters != null)
                    rp.Parameters.Validate();

                string customerID = string.Empty;

                //判断客户ID是否传递
                if (!string.IsNullOrEmpty(rp.CustomerID))
                {
                    customerID = rp.CustomerID;
                }
                var loggingSessionInfo = Default.GetBSLoggingSession(customerID, "1");

                if (rp.Parameters == null)
                {
                    rp.Parameters.DeviceToken = string.Empty;
                    rd.ResultCode = 102;
                    rd.Message = "没有特殊参数";
                    return rd.ToJSON();
                }

                if (string.IsNullOrEmpty(rp.UserID))
                {
                    rd.ResultCode = 2201;
                    rd.Message = "userId不能为空";
                    return rd.ToJSON();
                }

                if (string.IsNullOrEmpty(rp.Parameters.DeviceToken))
                {
                    rd.ResultCode = 2201;
                    rd.Message = "deviceToken不能为空";
                    return rd.ToJSON().ToString();
                }
                #endregion

                PushUserBasicBLL service = new PushUserBasicBLL(loggingSessionInfo);
                PushUserBasicEntity basicInfo = new PushUserBasicEntity();
                basicInfo = service.GetByID(rp.UserID);
                if (basicInfo == null)
                {
                    PushUserBasicEntity basicInfo1 = new PushUserBasicEntity();
                    basicInfo1.UserId = rp.UserID;
                    basicInfo1.DeviceToken = ToStr(rp.Parameters.DeviceToken);
                    basicInfo1.CustomerId = ToStr(rp.CustomerID);
                    basicInfo1.Channel = ToStr("");
                    basicInfo1.Plat = ToStr("");
                    service.Create(basicInfo1);
                }
                else
                {
                    basicInfo.DeviceToken = rp.Parameters.DeviceToken;
                    basicInfo.Channel = ToStr("");
                    service.Update(basicInfo);
                }
                rdData.DeviceToken = rp.Parameters.DeviceToken;
                rd.Data = rdData;
                rd.ResultCode = 0;
            }
            catch (Exception ex)
            {
                rd.ResultCode = 103;
                rd.Message = ex.ToString();
            }
            Loggers.Debug(new DebugLogInfo()
            {
                Message = string.Format("setSignUp content: {0}", rd.Message)
            });

            return rd.ToJSON();
        }

        #region  RD & RP
        public class SetIOSDeviceTokenRD : IAPIResponseData
        {
            public string DeviceToken { get; set; }
        }

        public class SetIOSDeviceTokenRP : IAPIRequestParameter
        {
            public string DeviceToken { get; set; }

            #region IAPIRequestParameter 成员

            public void Validate()
            {

            }

            #endregion
        }

        #endregion

        #endregion

        #region  2.0  IOS消息推送

        public string SendIOSMessage(string reqContent)
        {
            var rd = new APIResponse<SendIOSDeviceTokenRD>();

            try
            {
                #region
                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format("setSignUp: {0}", reqContent)
                });

                //解析请求字符串
                var rp = reqContent.DeserializeJSONTo<APIRequest<SendIOSDeviceTokenRP>>();
                if (rp.Parameters != null)
                    rp.Parameters.Validate();

                string customerID = string.Empty;

                //判断客户ID是否传递
                if (!string.IsNullOrEmpty(rp.CustomerID))
                {
                    customerID = rp.CustomerID;
                }
                var loggingSessionInfo = Default.GetBSLoggingSession(customerID, "1");

                //if (rp.Parameters == null)
                //{
                //    rp.Parameters.DeviceToken = string.Empty;
                //    rd.ResultCode = 102;
                //    rd.Message = "没有特殊参数";
                //    return rd.ToJSON();
                //}

                //if (string.IsNullOrEmpty(rp.UserID))
                //{
                //    rd.ResultCode = 2201;
                //    rd.Message = "userId不能为空";
                //    return rd.ToJSON();
                //}
                #endregion

                IPushMessage pushIOSMessage = new PushIOSMessage(loggingSessionInfo);
                string msg = "您有一条新消息";

                IPushMessage pushAndroidMessage = new PushAndroidMessage(loggingSessionInfo);
                pushAndroidMessage.PushMessage(rp.Parameters.OtherUserID, msg);

                pushIOSMessage.PushMessage(rp.Parameters.OtherUserID, msg);

                var rdData = new SendIOSDeviceTokenRD() { IsSuccess = true };
                rd.Data = rdData;
                rd.ResultCode = 0;
            }
            catch (Exception ex)
            {
            }
            return "";
        }

        #region  RD & RP
        public class SendIOSDeviceTokenRD : IAPIResponseData
        {
            public string DeviceToken { get; set; }
            public bool IsSuccess { set; get; }
        }

        public class SendIOSDeviceTokenRP : IAPIRequestParameter
        {
            /// <summary>
            /// 用户ID
            /// </summary>
            public string OtherUserID { get; set; }

            #region IAPIRequestParameter 成员

            public void Validate()
            {
                if (string.IsNullOrEmpty(OtherUserID)) throw new APIException("【OtherUserID】不能为空") { ErrorCode = 102 };
            }

            #endregion
        }

        #endregion

        /// <summary>
        /// 外发
        /// </summary>
        public void Send(string pContent)
        {
            //解析请求字符串
            var rp = pContent.DeserializeJSONTo<APIRequest<SetIOSDeviceTokenRP>>();

            var loggingSessionInfo = Default.GetBSLoggingSession(rp.CustomerID, "1");
            CSConversationBLL conversationBll = new CSConversationBLL(loggingSessionInfo);
            var conversations = conversationBll.Query(new IWhereCondition[]
                {
                    new DirectCondition("IsPush=0 or IsPush is NULL") 
                }, new[]
                {
                    new OrderBy
                        {
                            FieldName = "CreateTime",
                            Direction = OrderByDirections.Desc
                        }
                });
            foreach (var conversationEntity in conversations)
            {
                CSMessageBLL messageBll = new CSMessageBLL(loggingSessionInfo);
                CSMessageEntity messageEntity = messageBll.GetByID(conversationEntity.CSMessageID);
                try
                {
                    switch (messageEntity.CSPipelineID)
                    {
                        //微信
                        case 1:
                            IPushMessage pushWXMessage = new PushWeiXinMessage(loggingSessionInfo);
                            pushWXMessage.PushMessage(conversationEntity.PersonID, conversationEntity.Content);
                            break;
                        //短信
                        case 2:
                            IPushMessage pushSMSMessage = new PushSMSMessage(loggingSessionInfo);
                            pushSMSMessage.PushMessage(conversationEntity.PersonID, conversationEntity.Content);
                            break;
                        //IOS
                        case 3:
                            IPushMessage pusIOSMessage = new PushIOSMessage(loggingSessionInfo);
                            pusIOSMessage.PushMessage(conversationEntity.PersonID, conversationEntity.Content);
                            break;
                        //Android
                        case 4:
                            IPushMessage pusAndroidMessage = new PushAndroidMessage(loggingSessionInfo);
                            pusAndroidMessage.PushMessage(conversationEntity.PersonID, conversationEntity.Content);
                            break;
                    }
                    //更新已经推送
                    conversationEntity.IsPush = 1;
                    conversationBll.Update(conversationEntity);
                }
                catch (Exception ex)
                {
                    Loggers.DEFAULT.Debug(new DebugLogInfo
                    {
                        Message = "离线消息推送错误：" + ex.Message + "|" + conversationEntity.PersonID + "/" + messageEntity.CSPipelineID + "/" + conversationEntity.ToJSON()
                    });
                }

            }
        }
        #endregion

        #region GetNewsList    3.0 咨询列表
        public string GetNewList(string pContent)
        {
            var rd = new APIResponse<NewListRD>();
            try
            {
                //解析请求字符串
                var rp = pContent.DeserializeJSONTo<APIRequest<NewListRP>>();
                if (rp.Parameters != null)
                    rp.Parameters.Validate();

                #region
                if (rp.Parameters == null)
                {
                    rd.ResultCode = 101;
                    rd.Message = "没有特殊参数";
                    return rd.ToJSON();
                }

                string customerID = rp.CustomerID;
                //判断客户ID是否传递
                if (string.IsNullOrEmpty(customerID))
                {
                    rd.ResultCode = 100;
                    rd.Message = "customerID不能为空";
                    return rd.ToJSON();
                }

                if (string.IsNullOrEmpty(rp.UserID))
                {
                    rd.ResultCode = 102;
                    rd.Message = "userId不能为空";
                    return rd.ToJSON();
                }
                #endregion

                var loggingSessionInfo = Default.GetBSLoggingSession(rp.CustomerID, "1");
                LNewsBLL bll = new LNewsBLL(loggingSessionInfo);
                DataSet ds = bll.GetLNews(rp.CustomerID, rp.Parameters.PageSize, rp.Parameters.PageIndex);
                List<NewModel> list = new List<NewModel>();
                if (ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                {
                    list = DataTableToObject.ConvertToList<NewModel>(ds.Tables[0]);
                }

                var rdData = new NewListRD();
                rdData.NewList = list;
                string newListUrl = ConfigurationManager.AppSettings["NewListUrl"];
                rdData.NewListUrl = newListUrl;
                rd.Data = rdData;
                rd.Message = "获取咨询成功";

                return rd.ToJSON();
            }
            catch (Exception ex)
            {
                rd.ResultCode = 104;
                rd.Message = ex.ToString();
            }

            return string.Empty;
        }


        #region  RD & RP
        public class NewListRD : IAPIResponseData
        {
            public List<NewModel> NewList { get; set; }

            public string NewListUrl { get; set; }
        }

        public class NewModel
        {
            /// <summary>
            /// 新闻ID
            /// </summary>
            public string NewsId { get; set; }
            /// <summary>
            /// 新闻标题
            /// </summary>
            public string NewsTitle { get; set; }
            /// <summary>
            /// 内容
            /// </summary>
            public string Content { get; set; }
            /// <summary>
            /// 截取的标题
            /// </summary>
            public string NewsSubTitle { get; set; }
            /// <summary>
            /// 发布时间
            /// </summary>
            public DateTime PublishTime { get; set; }
            /// <summary>
            /// ImageUrl
            /// </summary>
            public string ImageUrl { get; set; }
            /// <summary>
            /// 新闻类型名称
            /// </summary>
            public string NewsTypeName { get; set; }

            public Int64 RowNumber { get; set; }
            /// <summary>
            /// 新闻详细Url
            /// </summary>
            public string ContentUrl { get; set; }
        }

        public class NewListRP : IAPIRequestParameter
        {
            public int PageIndex { get; set; }

            public int PageSize { get; set; }

            #region IAPIRequestParameter 成员

            public void Validate()
            {

            }

            #endregion
        }

        //#endregion
        #endregion
        #endregion

        #region GetNewDetail    4.0 获取咨询详情
        public string GetNewDetail(string pContent)
        {
            var rd = new APIResponse<NewDetailRD>();
            try
            {
                //解析请求字符串
                var rp = pContent.DeserializeJSONTo<APIRequest<NewDetailRP>>();
                if (rp.Parameters != null)
                    rp.Parameters.Validate();

                #region
                if (rp.Parameters == null)
                {
                    rd.ResultCode = 101;
                    rd.Message = "没有特殊参数";
                    return rd.ToJSON();
                }

                string customerID = rp.CustomerID;
                //判断客户ID是否传递
                if (string.IsNullOrEmpty(customerID))
                {
                    rd.ResultCode = 100;
                    rd.Message = "CustomerID不能为空";
                    return rd.ToJSON();
                }

                if (string.IsNullOrEmpty(rp.Parameters.NewID))
                {
                    rd.ResultCode = 102;
                    rd.Message = "NewsID不能为空";
                    return rd.ToJSON();
                }
                #endregion

                var loggingSessionInfo = Default.GetBSLoggingSession(rp.CustomerID, "1");
                DataSet ds = new LNewsBLL(loggingSessionInfo).GetNewDetail(customerID, rp.Parameters.NewID);
                NewModel model = new NewModel();
                if (ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                {
                    model = DataTableToObject.ConvertToList<NewModel>(ds.Tables[0]).FirstOrDefault();
                }

                var rdData = new NewDetailRD();
                rdData.New = model;
                rd.Data = rdData;
                rd.Message = "获取咨询详情成功";

                return rd.ToJSON();
            }
            catch (Exception ex)
            {
                rd.ResultCode = 104;
                rd.Message = ex.ToString();
            }

            return string.Empty;
        }


        #region  RD & RP
        public class NewDetailRD : IAPIResponseData
        {
            public NewModel New { get; set; }
        }

        public class NewDetailRP : IAPIRequestParameter
        {
            public string NewID { get; set; }

            #region IAPIRequestParameter 成员

            public void Validate()
            {

            }

            #endregion
        }

        //#endregion
        #endregion


        #endregion


        public string UpdateVersion(string pContent)
        {
            var rd = new APIResponse<UpdateVersionRD>();
            var rdData = new UpdateVersionRD();
            try
            {
                //解析请求字符串
                var rp = pContent.DeserializeJSONTo<EMAPIRequest<UpdateVersionRP>>();
                if (rp.Parameters != null)
                    rp.Parameters.Validate();

                #region
                string customerID = rp.CustomerID;
                //判断客户ID是否传递
                if (string.IsNullOrEmpty(rp.Plat))
                {
                    rd.ResultCode = 100;
                    rd.Message = "平台【Plat】不能为空";
                    return rd.ToJSON();
                }

                if (string.IsNullOrEmpty(rp.Version))
                {
                    rd.ResultCode = 102;
                    rd.Message = "版本号【Version】不能为空";
                    return rd.ToJSON();
                }
                #endregion

                var loggingSessionInfo = Default.GetBSLoggingSession(rp.CustomerID, "1");
                APPUpgradeBLL bll = new APPUpgradeBLL(loggingSessionInfo);
                T_UserBLL tubll = new T_UserBLL(loggingSessionInfo);
                DataSet ds = bll.GetAppUpgrade(rp.CustomerID, rp.Parameters.AppName, rp.Version);
                if (ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                {
                    DataTable dTable = ds.Tables[0];
                    DataRow lastRow = dTable.Rows[0];
                    //是否强制升级
                    DataRow[] drs = dTable.Select("IsMandatoryUpdate=1");
                    if (drs != null && drs.Length > 0)
                        lastRow["IsMandatoryUpdate"] = 1;
                    //rdData = DataTableToObject.ConvertToObject<UpdateVersionRD>(lastRow);

                    rdData.AppName = lastRow["AppName"].ToString();
                    rdData.Version = lastRow["Versions"].ToString();
                    rdData.IsMandatoryUpdate = lastRow["IsMandatoryUpdate"].ToString();
                    if (rp.Plat.ToLower() == Plat.iPhone.ToString().ToLower())
                    {
                        rdData.UpdateURL = lastRow["IOSUpgradeUrl"] == null ? "" : lastRow["IOSUpgradeUrl"].ToString();
                        rdData.UpdateContent = lastRow["IOSUpgradeCon"] == null ? "" : lastRow["IOSUpgradeCon"].ToString();
                    }
                    else
                    {
                        rdData.UpdateURL = lastRow["AndroidUpgradeUrl"] == null ? "" : lastRow["AndroidUpgradeUrl"].ToString();
                        rdData.UpdateContent = lastRow["AndroidUpgradeCon"] == null ? "" : lastRow["AndroidUpgradeCon"].ToString();
                    }

                    //rdData.ServerUrl = lastRow["ServerUrl"] == null ? "" : lastRow["ServerUrl"].ToString();

                    T_UserEntity tue = tubll.GetUserEntityByID(rp.UserID);
                    if (tue != null)
                        rdData.LoginName = tue.user_email;
                }
                else
                {
                    rd.Message = "已是最新版本";
                    rd.ResultCode = 101;
                }
                rdData.ServerUrl = System.Configuration.ConfigurationManager.AppSettings["PGServerUrl"].ToString();
                rd.Data = rdData;
                return rd.ToJSON();
            }
            catch (Exception ex)
            {
                rd.ResultCode = 104;
                rd.Message = ex.ToString();
            }
            return string.Empty;
        }

        #region 公共方法

        #region ToStr

        public string ToStr(string obj)
        {
            if (obj == null) return string.Empty;
            return obj.ToString();
        }

        public string ToStr(float obj)
        {
            //if (obj == null) return "0";
            return obj.ToString();
        }

        public string ToStr(double obj)
        {
            //if (obj == null) return "0";
            return obj.ToString();
        }

        public string ToStr(double? obj)
        {
            if (obj == null) return "0";
            return obj.ToString();
        }

        public string ToStr(decimal? obj)
        {
            if (obj == null) return "0";
            return obj.ToString();
        }

        public string ToStr(long obj)
        {
            //if (obj == null) return "0";
            return obj.ToString();
        }

        public string ToStr(int obj)
        {
            //if (obj == null) return "0";
            return obj.ToString();
        }

        public string ToStr(int? obj)
        {
            if (obj == null) return "0";
            return obj.ToString();
        }

        public string ToStr(DateTime? obj)
        {
            if (obj == null) return null;
            return obj.ToString();
        }

        #endregion

        public int ToInt(object obj)
        {
            if (obj == null) return 0;
            else if (obj.ToString() == "") return 0;
            return Convert.ToInt32(obj);
        }

        public Int64 ToInt64(object obj)
        {
            if (obj == null) return 0;
            else if (obj.ToString() == "") return 0;
            return Convert.ToInt64(obj);
        }

        public double ToDouble(object obj)
        {
            if (obj == null) return 0;
            else if (obj.ToString() == "") return 0;
            return Convert.ToDouble(obj);
        }

        public float ToFloat(object obj)
        {
            if (obj == null) return 0;
            else if (obj.ToString() == "") return 0;
            return float.Parse(obj.ToString());
        }

        #endregion

    }

    #region 请求参数或返回数据结构
    public class UpdateVersionRP : IAPIRequestParameter
    {
        public string AppName { set; get; }
        public void Validate()
        {
            if (string.IsNullOrEmpty(AppName))
                throw new APIException("应用名称【AppName】不能为空");
        }
    }
    public class UpdateVersionRD : IAPIResponseData
    {
        /// <summary>
        /// 强制升级标识
        /// 1强制升级
        /// 0非强制升级
        /// </summary>
        public string IsMandatoryUpdate { set; get; }
        /// <summary>
        /// 升级URL
        /// </summary>
        public string UpdateURL { set; get; }
        /// <summary>
        ///  用户
        /// </summary>
        public string LoginName { set; get; }
        /// <summary>
        /// 升级版本号
        /// </summary>
        public string Version { set; get; }
        /// <summary>
        /// 应用名称
        /// </summary>
        public string AppName { set; get; }
        /// <summary>
        /// 更新内容
        /// </summary>
        public string UpdateContent { set; get; }
        /// <summary>
        /// 服务url（宝洁）
        /// </summary>
        public string ServerUrl { set; get; }
    }
    #endregion
}