using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Entity;
using JIT.Utility.ExtensionMethod;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Text;
using System.Web;

using JIT.CPOS.BS.BLL.Module.NoticeEmail;
using JIT.Utility.Notification;
using JIT.CPOS.Web.Module.XieHuiBao;


namespace JIT.CPOS.Web.DynamicInterface.data
{
    public partial class Data : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Clear();
            string content = string.Empty;

            try
            {
                if (!string.IsNullOrEmpty(Request["action"]))
                {
                    string dataType = Request["action"].ToString().Trim();
                    JIT.CPOS.Web.Module.Log.InterfaceWebLog.Logger.Log(Context, Request, dataType);
                    switch (dataType)
                    {
                        #region 老版接口
                        case "getUserIDByOpenID": //4.1	根据微信OpenID获取用户信息
                            content = getUserIDByOpenID();
                            break;
                        case "getCodeByPhone":  //4.2 根据手机获取验证码
                            content = getCodeByPhone();
                            break;
                        case "getUserByPhoneAndCode":  //4.3 根据手机和验证码判断输入是否正常,获取用户信息
                            content = getUserByPhoneAndCode();
                            break;
                        case "getUserDefinedByUserID": //4.4 根据用户获取字段配置信息
                            content = new XieHuiBaoHandler().getUserDefinedByUserID();
                            break;
                        case "submitUserInfo":  //4.5 提交用户信息
                            content = submitUserInfo();
                            break;
                        case "getNewsType":  //4.6 所有新闻类型
                            content = getNewsType();
                            break;
                        case "getNewsList":  //4.6 新闻列表信息
                            content = getNewsList();
                            break;
                        case "getNewsDetailByNewsID":  //4.7 新闻详细信息
                            content = getNewsDetailByNewsID();
                            break;
                        case "getTopList":  //4.8 banner显示新闻和活动
                            content = getTopList();
                            break;
                        case "getActivityList":  //4.9 活动列表
                            content = getActivityList();
                            break;
                        case "getActivityByActivityID":  //4.10 活动详细信息
                            content = getActivityByActivityID();
                            break;
                        case "submitActivityInfo":  //4.11报名活动 
                            content = submitActivityInfo();
                            break;
                        case "getUserByLogin":  //4.12根据用户名和密码登陆,获取用户信息
                            content = new XieHuiBaoHandler().getUserByLogin();
                            break;
                        case "submintNewsCountByID":
                            content = submintNewsCountByID();
                            break;

                        case "getEventTypeList":
                            content = getEventTypeList();
                            break;
                        case "getMyEventList":
                            content = getMyEventList();
                            break;
                        case "getEventList":  //4.9 活动列表
                            content = new XieHuiBaoHandler().getEventList();
                            break;
                        case "getEventByEventID":  //4.10 活动详细信息
                            content = new XieHuiBaoHandler().getEventByEventID();
                            break;
                        case "submitEventInfo":  //4.11报名活动 
                            content = submitEventInfo();
                            break;
                        case "register":
                            content = new XieHuiBaoHandler().register();
                            break;
                        case "addEventInfo":
                            content = new XieHuiBaoHandler().addEventInfo();
                            break;
                        #endregion
                        #region 2014-05-09 新加接口
                         case "GetEventAlbumByAlbumID":  //视频详细
                            content = GetEventAlbumByAlbumID();
                            break;
                        #endregion
                        default:
                            throw new Exception("未定义的接口:" + dataType);
                    }
                }
                else
                {
                    throw new Exception("未传递的接口名action");
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

        #region 老版接口
        #region getUserIDByOpenID     4.1 根据微信OpenID获取用户信息
        public string getUserIDByOpenID()
        {
            string res = string.Empty;
            string reqContent = string.Empty;
            RequestEntity<reqGetUserIDByOpenIDEntity> requestEntity = new RequestEntity<reqGetUserIDByOpenIDEntity>();
            if (!string.IsNullOrEmpty(Request["ReqContent"]))
            {
                reqContent = Request["ReqContent"];
                var reqObj = reqContent.DeserializeJSONTo<getUserIDByOpenIDEntity>();
                if (reqObj != null)
                {
                    var loggingSessionInfo = Default.GetBSLoggingSession(reqObj.common.customerId, "1");
                    string UserID = new DynamicInterfaceBLL(loggingSessionInfo).getUserIDByOpenID(reqObj);
                    if (!string.IsNullOrEmpty(UserID))
                    {
                        reqGetUserIDByOpenIDEntity pEntity = new reqGetUserIDByOpenIDEntity();
                        pEntity.userId = UserID;
                        requestEntity.code = "200";
                        requestEntity.description = "操作成功";
                        requestEntity.content = pEntity;
                    }
                    else
                    {
                        requestEntity.code = "1";
                        requestEntity.description = "操作失败";
                        requestEntity.content = null;
                    }
                }
            }
            res = requestEntity.ToJSON();
            return res;
        }
        #endregion

        #region getCodeByPhone  4.2 根据手机获取验证码
        public string getCodeByPhone()
        {
            string res = string.Empty;
            string reqContent = string.Empty;
            RequestEntity<string> requestEntity = new RequestEntity<string>();
            if (!string.IsNullOrEmpty(Request["ReqContent"]))
            {
                reqContent = Request["ReqContent"];
                var reqObj = reqContent.DeserializeJSONTo<getCodeByPhoneEntity>();
                if (reqObj != null)
                {
                    var loggingSessionInfo = Default.GetBSLoggingSession(reqObj.common.customerId, "1");
                    Random rd = new Random();
                    string Code = rd.Next(99999, 1000000).ToString();
                    reqObj.special.Code = Code;
                    string sign = new DynamicInterfaceBLL(loggingSessionInfo).getSign(reqObj);
                    var service = new SendSMSService.ReceiveService();
                    var results = service.Recieve(
                    reqObj.special.Phone,  //手机
                    "您的验证码是：" + reqObj.special.Code + "",     //短信内容
                    sign);     //签名
                    int result = new DynamicInterfaceBLL(loggingSessionInfo).getCodeByPhone(reqObj);
                    if (result > 0)
                    {
                        if (results == "T")
                        {
                            requestEntity.code = "200";
                            requestEntity.description = "操作成功";
                            requestEntity.content = "";
                        }
                        else
                        {
                            requestEntity.code = "2";
                            requestEntity.description = "验证码发送失败";
                            requestEntity.content = "";
                        }
                    }
                    else
                    {
                        requestEntity.code = "1";
                        requestEntity.description = "操作失败";
                        requestEntity.content = "";
                    }
                }
            }
            res = requestEntity.ToJSON();
            return res;
        }
        #endregion

        #region getUserByPhoneAndCode 4.3 根据手机和验证码判断输入是否正常,获取用户信息
        public string getUserByPhoneAndCode()
        {
            string res = string.Empty;
            string reqContent = string.Empty;
            RequestEntity<reqGetUserIDByOpenIDEntity> requestEntity = new RequestEntity<reqGetUserIDByOpenIDEntity>();
            if (!string.IsNullOrEmpty(Request["ReqContent"]))
            {
                reqContent = Request["ReqContent"];
                var reqObj = reqContent.DeserializeJSONTo<getUserByPhoneAndCodeEntity>();
                if (reqObj != null)
                {
                    var loggingSessionInfo = Default.GetBSLoggingSession(reqObj.common.customerId, "1");
                    string UserID = new DynamicInterfaceBLL(loggingSessionInfo).getUserByPhoneAndCode(reqObj);
                    if (!string.IsNullOrEmpty(UserID))
                    {
                        reqGetUserIDByOpenIDEntity pEntity = new reqGetUserIDByOpenIDEntity();
                        pEntity.userId = UserID;
                        requestEntity.code = "200";
                        requestEntity.description = "操作成功";
                        requestEntity.content = pEntity;
                    }
                    else
                    {
                        requestEntity.code = "1";
                        requestEntity.description = "操作失败";
                        requestEntity.content = null;
                    }
                }
            }
            res = requestEntity.ToJSON();
            return res;
        }
        #endregion

        #region getUserDefinedByUserID 4.4 根据用户获取字段配置信息
        public string getUserDefinedByUserID()
        {
            string res = string.Empty;
            string reqContent = string.Empty;
            RequestEntity<reqPageListEntity> requestEntity = new RequestEntity<reqPageListEntity>();
            if (!string.IsNullOrEmpty(Request["ReqContent"]))
            {
                reqContent = Request["ReqContent"];
                var reqObj = reqContent.DeserializeJSONTo<ReqData<getUserDefinedByUserIDEntity>>();
                if (reqObj.special.TypeID < 1)
                {
                    reqObj.special.TypeID = 1;
                }
                if (reqObj != null)
                {
                    var loggingSessionInfo = Default.GetBSLoggingSession(reqObj.common.customerId, "1");
                    List<PageEntity> list = new DynamicInterfaceBLL(loggingSessionInfo).getUserDefinedByUserID(reqObj);
                    reqPageListEntity pageList = new reqPageListEntity();
                    pageList.pageList = list;
                    if (list != null && list.Count > 0)
                    {
                        requestEntity.code = "200";
                        requestEntity.description = "操作成功";
                        requestEntity.content = pageList;
                    }
                    else
                    {
                        requestEntity.code = "1";
                        requestEntity.description = "操作失败";
                        requestEntity.content = null;
                    }
                }
            }
            res = requestEntity.ToJSON();
            return res;
        }
        #endregion

        #region submitUserInfo 4.5 提交用户信息
        public string submitUserInfo()
        {
            string res = string.Empty;
            string reqContent = string.Empty;
            string email = string.Empty;
            RequestEntity<reqGetUserIDByOpenIDEntity> requestEntity = new RequestEntity<reqGetUserIDByOpenIDEntity>();
            if (!string.IsNullOrEmpty(Request["ReqContent"]))
            {
                reqContent = Request["ReqContent"];
                var reqObj = reqContent.DeserializeJSONTo<submitUserInfoEntity>();
                if (reqObj != null)
                {
                    var loggingSessionInfo = Default.GetBSLoggingSession(reqObj.common.customerId, "1");

                    //是否有Email
                    bool noEmail = true;
                    //是否有Phone
                    bool noPhone = true;
                    //是否有VipPasswrod
                    bool noVipPasswrod = true;

                    if (reqObj != null && reqObj.special.Control != null && reqObj.special.Control.Count > 0)
                    {
                        foreach (ControlUpdateEntity cEntity in reqObj.special.Control)
                        {
                            if (cEntity != null)
                            {
                                if (cEntity.ColumnName.ToLower() == "email")
                                {
                                    if (string.IsNullOrEmpty(cEntity.Value))
                                    {
                                        requestEntity.code = "2";
                                        requestEntity.description = "操作失败，Email不能为空";
                                        requestEntity.content = null;

                                        res = requestEntity.ToJSON();
                                        return res;
                                    }
                                    else
                                    {
                                        if (new DynamicInterfaceBLL(loggingSessionInfo).checkUserEmail(cEntity.Value, reqObj.common.userId, reqObj.common.customerId))
                                        {
                                            requestEntity.code = "3";
                                            requestEntity.description = "操作失败，Email已存在";
                                            requestEntity.content = null;

                                            res = requestEntity.ToJSON();
                                            return res;
                                        }
                                    }

                                    email = cEntity.Value;
                                    noEmail = false;
                                }
                                else if (cEntity.ColumnName.ToLower() == "phone")
                                {
                                    if (string.IsNullOrEmpty(cEntity.Value))
                                    {
                                        requestEntity.code = "4";
                                        requestEntity.description = "操作失败，Phone不能为空";
                                        requestEntity.content = null;

                                        res = requestEntity.ToJSON();
                                        return res;
                                    }
                                    else
                                    {
                                        if (new DynamicInterfaceBLL(loggingSessionInfo).checkUserPhone(cEntity.Value, reqObj.common.userId, reqObj.common.customerId))
                                        {
                                            requestEntity.code = "5";
                                            requestEntity.description = "操作失败，Phone已存在";
                                            requestEntity.content = null;

                                            res = requestEntity.ToJSON();
                                            return res;
                                        }
                                    }

                                    noPhone = false;
                                }
                                else if (cEntity.ColumnName.ToLower() == "vippasswrod")
                                {
                                    if (string.IsNullOrEmpty(cEntity.Value))
                                    {
                                        requestEntity.code = "6";
                                        requestEntity.description = "操作失败，密码不能为空";
                                        requestEntity.content = null;

                                        res = requestEntity.ToJSON();
                                        return res;
                                    }

                                    noVipPasswrod = false;
                                }

                            }
                        }
                    }

                    //若Email、Phone和密码全部没有，且存在用户ID，则跳过这步验证
                    if (!(noEmail && noPhone && noVipPasswrod && !string.IsNullOrEmpty(reqObj.common.userId)))
                    {
                        if (noEmail || noPhone || noVipPasswrod)
                        {
                            requestEntity.code = "7";
                            requestEntity.description = "操作失败，Email、Phone或密码配置不正确";
                            requestEntity.content = null;

                            res = requestEntity.ToJSON();
                            return res;
                        }
                    }

                    string pUserID = reqObj.common.userId;

                    int result = new DynamicInterfaceBLL(loggingSessionInfo).submitUserInfo(reqObj);


                    if (result > 0)
                    {
                        if (string.IsNullOrEmpty(pUserID))
                        {
                            string pContent = null;
                            VipInfoMaillBLL vipInfoMaillBll = new VipInfoMaillBLL(loggingSessionInfo);
                            XmlManager xml = new XmlManager(ConfigurationManager.AppSettings["xmlFile"]);
                            if (loggingSessionInfo.ClientID == "a2573925f3b94a32aca8cac77baf6d33")
                            {
                                pContent = xml.GetNodeText("//Root//XieHuiBaoMailTemplate");
                            }
                            else if (loggingSessionInfo.ClientID == "e137e8e040bb4db3be17d90feeefa7bf")
                            {
                                pContent = xml.GetNodeText("//Root//XWTMailTemplate");
                            }
                            else if (loggingSessionInfo.ClientID == "75a232c2cf064b45b1b6393823d2431e")
                            {
                                pContent = xml.GetNodeText("//Root//EMBAMailTemplate");
                            }
                            vipInfoMaillBll.SendNoticeMail(email,pContent);
                        }
                        reqGetUserIDByOpenIDEntity reqEntity = new BS.Entity.reqGetUserIDByOpenIDEntity();
                        reqEntity.userId = reqObj.common.userId;
                        requestEntity.code = "200";
                        requestEntity.description = "操作成功";
                        requestEntity.content = reqEntity;
                    }
                    else
                    {
                        requestEntity.code = "1";
                        requestEntity.description = "操作失败";
                        requestEntity.content = null;
                    }
                }
            }
            res = requestEntity.ToJSON();
            return res;
        }
        #endregion

        #region getNewsList    4.6 咨询列表
        public string getNewsList()
        {
            string res = string.Empty;
            string reqContent = string.Empty;
            RequestEntity<SearchListEntity<DataTable>> requestEntity = new RequestEntity<SearchListEntity<DataTable>>();
            if (!string.IsNullOrEmpty(Request["ReqContent"]))
            {
                reqContent = Request["ReqContent"];
                var reqObj = reqContent.DeserializeJSONTo<ReqData<getNewsListEntity>>();
                if (reqObj != null)
                {
                    var loggingSessionInfo = Default.GetBSLoggingSession(reqObj.common.customerId, "1");
                    SearchListEntity<DataTable> searchListEntity = new DynamicInterfaceBLL(loggingSessionInfo).getNewsList(reqObj);
                    if (searchListEntity != null)
                    {
                        requestEntity.code = "200";
                        requestEntity.description = "操作成功";
                        requestEntity.content = searchListEntity;
                    }
                    else
                    {
                        requestEntity.code = "1";
                        requestEntity.description = "操作失败";
                        requestEntity.content = null;
                    }
                }
            }
            res = requestEntity.ToJSON();
            return res;
        }
        #endregion

        #region getNewsDetailByNewsID    4.7 咨询详细
        public string getNewsDetailByNewsID()
        {
            string res = string.Empty;
            string reqContent = string.Empty;
            RequestEntity<reqNewsEntity> requestEntity = new RequestEntity<reqNewsEntity>();
            if (!string.IsNullOrEmpty(Request["ReqContent"]))
            {
                reqContent = Request["ReqContent"];
                var reqObj = reqContent.DeserializeJSONTo<ReqData<getNewsDetailByNewsIDEntity>>();
                if (reqObj != null)
                {
                    var loggingSessionInfo = Default.GetBSLoggingSession(reqObj.common.customerId, "1");
                    reqNewsEntity pReqNewsEntity = new DynamicInterfaceBLL(loggingSessionInfo).getNewsDetailByNewsID(reqObj);
                    if (pReqNewsEntity != null)
                    {
                        if (!string.IsNullOrEmpty(pReqNewsEntity.News.Content))
                        {
                            pReqNewsEntity.News.Content = HttpUtility.HtmlDecode(pReqNewsEntity.News.Content);
                        }
                        requestEntity.code = "200";
                        requestEntity.description = "操作成功";
                        requestEntity.content = pReqNewsEntity;
                    }
                    else
                    {
                        requestEntity.code = "1";
                        requestEntity.description = "操作失败";
                        requestEntity.content = null;
                    }
                }
            }
            res = requestEntity.ToJSON();
            return res;
        }
        #endregion

        #region getUserByLogin    4.12 登陆
        public string getUserByLogin()
        {
            string res = string.Empty;
            string reqContent = string.Empty;
            RequestEntity<reqGetUserIDByOpenIDEntity> requestEntity = new RequestEntity<reqGetUserIDByOpenIDEntity>();
            if (!string.IsNullOrEmpty(Request["ReqContent"]))
            {
                reqContent = Request["ReqContent"];
                var reqObj = reqContent.DeserializeJSONTo<ReqData<getUserByLoginEntity>>();
                if (reqObj != null)
                {
                    var loggingSessionInfo = Default.GetBSLoggingSession(reqObj.common.customerId, "1");
                    string pUserID = new DynamicInterfaceBLL(loggingSessionInfo).getUserByLogin(reqObj);
                    if (!string.IsNullOrEmpty(pUserID))
                    {
                        if (pUserID == "-1")
                        {
                            requestEntity.code = "2";
                            requestEntity.description = "账号不存在或密码错误";
                            requestEntity.content = null;
                        }
                        else if (pUserID == "-2")
                        {
                            requestEntity.code = "3";
                            requestEntity.description = "账号已存在";
                            requestEntity.content = null;
                        }
                        else
                        {
                            reqGetUserIDByOpenIDEntity pEntity = new reqGetUserIDByOpenIDEntity();
                            pEntity.userId = pUserID;
                            requestEntity.code = "200";
                            requestEntity.description = "操作成功";
                            requestEntity.content = pEntity;
                        }
                    }
                    else
                    {
                        requestEntity.code = "1";
                        requestEntity.description = "操作失败";
                        requestEntity.content = null;
                    }
                }
            }
            res = requestEntity.ToJSON();
            return res;
        }
        #endregion

        #region getActivityList 4.9 活动列表
        public string getActivityList()
        {
            string res = string.Empty;
            string reqContent = string.Empty;
            RequestEntity<SearchListEntity<reqActivityListEntity[]>> requestEntity = new RequestEntity<SearchListEntity<reqActivityListEntity[]>>();
            if (!string.IsNullOrEmpty(Request["ReqContent"]))
            {
                reqContent = Request["ReqContent"];
                var reqObj = reqContent.DeserializeJSONTo<ReqData<getActivityListEntity>>();
                if (reqObj != null)
                {
                    var loggingSessionInfo = Default.GetBSLoggingSession(reqObj.common.customerId, "1");
                    SearchListEntity<reqActivityListEntity[]> searchListEntity = new DynamicInterfaceBLL(loggingSessionInfo).getActivityList(reqObj);
                    if (searchListEntity != null)
                    {
                        requestEntity.code = "200";
                        requestEntity.description = "操作成功";
                        requestEntity.content = searchListEntity;
                    }
                    else
                    {
                        requestEntity.code = "1";
                        requestEntity.description = "操作失败";
                        requestEntity.content = null;
                    }
                }
            }
            res = requestEntity.ToJSON();
            return res;
        }
        #endregion

        #region getTopList    4.8
        public string getTopList()
        {
            string res = string.Empty;
            string reqContent = string.Empty;
            RequestEntity<SearchListEntity<DataTable>> requestEntity = new RequestEntity<SearchListEntity<DataTable>>();
            if (!string.IsNullOrEmpty(Request["ReqContent"]))
            {
                reqContent = Request["ReqContent"];
                var reqObj = reqContent.DeserializeJSONTo<ReqData<getTopListEntity>>();
                if (reqObj != null)
                {
                    var loggingSessionInfo = Default.GetBSLoggingSession(reqObj.common.customerId, "1");
                    SearchListEntity<DataTable> searchListEntity = new DynamicInterfaceBLL(loggingSessionInfo).getTopList(reqObj);
                    if (searchListEntity != null)
                    {
                        requestEntity.code = "200";
                        requestEntity.description = "操作成功";
                        requestEntity.content = searchListEntity;
                    }
                    else
                    {
                        requestEntity.code = "1";
                        requestEntity.description = "操作失败";
                        requestEntity.content = null;
                    }
                }
            }
            res = requestEntity.ToJSON();
            return res;
        }
        #endregion

        #region getActivityByActivityID    4.10
        public string getActivityByActivityID()
        {
            string res = string.Empty;
            string reqContent = string.Empty;
            RequestEntity<reqActivityEntity> requestEntity = new RequestEntity<reqActivityEntity>();
            if (!string.IsNullOrEmpty(Request["ReqContent"]))
            {
                reqContent = Request["ReqContent"];
                var reqObj = reqContent.DeserializeJSONTo<ReqData<getActivityByActivityIDEntity>>();
                if (reqObj != null)
                {
                    var loggingSessionInfo = Default.GetBSLoggingSession(reqObj.common.customerId, "1");
                    reqActivityEntity pReqNewsEntity = new DynamicInterfaceBLL(loggingSessionInfo).getActivityByActivityID(reqObj);

                    if (pReqNewsEntity != null)
                    {
                        if (!string.IsNullOrEmpty(pReqNewsEntity.Activity.ActivityContent))
                        {
                            pReqNewsEntity.Activity.ActivityContent = HttpUtility.HtmlDecode(pReqNewsEntity.Activity.ActivityContent);
                        }
                        if (!string.IsNullOrEmpty(pReqNewsEntity.Activity.BeginTime))
                        {
                            DateTime dt = new DateTime();
                            if (DateTime.TryParse(pReqNewsEntity.Activity.BeginTime, out dt))
                            {
                                string pWeek = "";
                                switch (dt.DayOfWeek)
                                {
                                    case DayOfWeek.Friday:
                                        pWeek = "星期五";
                                        break;
                                    case DayOfWeek.Monday:
                                        pWeek = "星期一";
                                        break;
                                    case DayOfWeek.Saturday:
                                        pWeek = "星期六";
                                        break;
                                    case DayOfWeek.Sunday:
                                        pWeek = "星期天";
                                        break;
                                    case DayOfWeek.Thursday:
                                        pWeek = "星期四";
                                        break;
                                    case DayOfWeek.Tuesday:
                                        pWeek = "星期二";
                                        break;
                                    case DayOfWeek.Wednesday:
                                        pWeek = "星期三";
                                        break;
                                    default:
                                        pWeek = "";
                                        break;
                                }
                                pReqNewsEntity.Activity.ActivityTime = dt.ToString("yyyy-MM-dd") + " " + pWeek + " " + dt.ToString("HH:ss");
                            }

                        }
                        requestEntity.code = "200";
                        requestEntity.description = "操作成功";
                        requestEntity.content = pReqNewsEntity;
                    }
                    else
                    {
                        requestEntity.code = "1";
                        requestEntity.description = "操作失败";
                        requestEntity.content = null;
                    }
                }
            }
            res = requestEntity.ToJSON();
            return res;
        }
        #endregion

        #region submitActivityInfo 4.11报名活动
        public string submitActivityInfo()
        {
            string res = string.Empty;
            string reqContent = string.Empty;
            string email = string.Empty;
            RequestEntity<reqActivityInfoEntity> requestEntity = new RequestEntity<reqActivityInfoEntity>();
            if (!string.IsNullOrEmpty(Request["ReqContent"]))
            {
                reqContent = Request["ReqContent"];
                var reqObj = reqContent.DeserializeJSONTo<ReqData<submitActivityInfoEntity>>();
                if (reqObj != null)
                {
                    var loggingSessionInfo = Default.GetBSLoggingSession(reqObj.common.customerId, "1");
                    string pEventVipTicketID = new DynamicInterfaceBLL(loggingSessionInfo).submitActivityInfo(reqObj);
                    if (!string.IsNullOrEmpty(pEventVipTicketID))
                    {
                        if (string.IsNullOrEmpty(email))
                        {
                            //VipInfoMaillBLL vipInfoMaillBll = new VipInfoMaillBLL(loggingSessionInfo);
                            //vipInfoMaillBll.SendNoticeMail(email);
                        }
                        reqActivityInfoEntity reqEntity = new BS.Entity.reqActivityInfoEntity();
                        reqEntity.ActivityVipID = pEventVipTicketID;
                        requestEntity.code = "200";
                        requestEntity.description = "操作成功";
                        requestEntity.content = reqEntity;
                    }
                    else
                    {
                        requestEntity.code = "1";
                        requestEntity.description = "操作失败";
                        requestEntity.content = null;
                    }
                }
            }
            res = requestEntity.ToJSON();
            return res;
        }
        #endregion

        #region submintNewsCountByID 4.13
        public string submintNewsCountByID()
        {
            string res = string.Empty;
            string reqContent = string.Empty;
            string email = string.Empty;
            RequestEntity<string> requestEntity = new RequestEntity<string>();
            if (!string.IsNullOrEmpty(Request["ReqContent"]))
            {
                reqContent = Request["ReqContent"];
                var reqObj = reqContent.DeserializeJSONTo<ReqData<submintNewsCountByIDEntity>>();
                if (reqObj != null)
                {
                    var loggingSessionInfo = Default.GetBSLoggingSession(reqObj.common.customerId, "1");
                    int result = new DynamicInterfaceBLL(loggingSessionInfo).submintNewsCountByID(reqObj);
                    if (result > 0)
                    {
                        requestEntity.code = "200";
                        requestEntity.description = "操作成功";
                        requestEntity.content = "";
                    }
                    else
                    {
                        requestEntity.code = "1";
                        requestEntity.description = "操作失败";
                        requestEntity.content = "";
                    }
                }
            }
            res = requestEntity.ToJSON();
            return res;
        }
        #endregion

        #region getMyEventList
        public string getMyEventList()
        {
            string reqContent = string.Empty;
            var reqObj = reqContent.DeserializeJSONTo<ReqData<EventsTypeEntity>>();

            RequestEntity<EventsTypeEntity[]> responseEntity = new BS.Entity.RequestEntity<BS.Entity.EventsTypeEntity[]>();
            var loggingSessionInfo = Default.GetBSLoggingSession(reqObj.common.customerId, "1");
            //responseEntity.content = new DynamicInterfaceBLL(loggingSessionInfo).getMyEventList();
            new EclubMyFootPrintBLL(loggingSessionInfo).RecordSpoorInfo("000001", "1", "1", 11, 1);
            return "";
        }
        #endregion

        #region getEventTypeList
        public string getEventTypeList()
        {
            string reqContent = Request["ReqContent"];
            var reqObj = reqContent.DeserializeJSONTo<ReqData<EventsTypeEntity>>();

            RequestEntity<EventsTypeEntity[]> responseEntity = new BS.Entity.RequestEntity<BS.Entity.EventsTypeEntity[]>();
            var loggingSessionInfo = Default.GetBSLoggingSession(reqObj.common.customerId, "1");
            responseEntity.content = new DynamicInterfaceBLL(loggingSessionInfo).getEventTypeList();

            responseEntity.code = "200";
            responseEntity.description = "操作成功";
            return responseEntity.ToJSON();
        }
        #endregion

        #region getNewsType
        public string getNewsType()
        {
            string reqContent = Request["ReqContent"];
            var reqObj = reqContent.DeserializeJSONTo<ReqData<EventsTypeEntity>>();
            if (reqObj.special.ChannelCode == 0 )
            {
                reqObj.special.ChannelCode = 1;//如果不传的话，默认为1
            }

            RequestEntity<LNewsTypeEntity[]> responseEntity = new BS.Entity.RequestEntity<LNewsTypeEntity[]>();//返回的是数组
            var loggingSessionInfo = Default.GetBSLoggingSession(reqObj.common.customerId, "1");
            responseEntity.content = new DynamicInterfaceBLL(loggingSessionInfo).getNewsType(reqObj.special.ChannelCode);

            responseEntity.code = "200";
            responseEntity.description = "操作成功";
            return responseEntity.ToJSON();
        }
        #endregion

        #region register
        /// <summary>
        /// 中欧注册信息
        /// </summary>
        /// <returns></returns>
        public string register()
        {
            string res = string.Empty;
            string reqContent = string.Empty;
            string email = string.Empty;
            RequestEntity<reqGetUserIDByOpenIDEntity> requestEntity = new RequestEntity<reqGetUserIDByOpenIDEntity>();
            if (!string.IsNullOrEmpty(Request["ReqContent"]))
            {
                reqContent = Request["ReqContent"];
                var reqObj = reqContent.DeserializeJSONTo<submitUserInfoEntity>();
                if (reqObj != null)
                {
                    var loggingSessionInfo = Default.GetBSLoggingSession(reqObj.common.customerId, "1");

                    //是否有Email
                    bool noEmail = true;
                    //是否有Phone
                    bool noPhone = true;
                    //是否有VipPasswrod
                    bool noVipPasswrod = true;

                    if (reqObj != null && reqObj.special.Control != null && reqObj.special.Control.Count > 0)
                    {
                        foreach (ControlUpdateEntity cEntity in reqObj.special.Control)
                        {
                            if (cEntity != null)
                            {
                                if (cEntity.ColumnName.ToLower() == "email")
                                {
                                    if (string.IsNullOrEmpty(cEntity.Value))
                                    {
                                        requestEntity.code = "2";
                                        requestEntity.description = "操作失败，Email不能为空";
                                        requestEntity.content = null;

                                        res = requestEntity.ToJSON();
                                        return res;
                                    }
                                    else
                                    {
                                        if (new DynamicInterfaceBLL(loggingSessionInfo).checkUserEmail(cEntity.Value, reqObj.common.userId, reqObj.common.customerId))
                                        {
                                            requestEntity.code = "3";
                                            requestEntity.description = "操作失败，Email已存在";
                                            requestEntity.content = null;

                                            res = requestEntity.ToJSON();
                                            return res;
                                        }
                                    }
                                    email = cEntity.Value;
                                    noEmail = false;
                                }
                                else if (cEntity.ColumnName.ToLower() == "phone")
                                {
                                    if (string.IsNullOrEmpty(cEntity.Value))
                                    {
                                        requestEntity.code = "4";
                                        requestEntity.description = "操作失败，Phone不能为空";
                                        requestEntity.content = null;

                                        res = requestEntity.ToJSON();
                                        return res;
                                    }
                                    else
                                    {
                                        if (new DynamicInterfaceBLL(loggingSessionInfo).checkUserPhone(cEntity.Value, reqObj.common.userId, reqObj.common.customerId))
                                        {
                                            requestEntity.code = "5";
                                            requestEntity.description = "操作失败，Phone已存在";
                                            requestEntity.content = null;

                                            res = requestEntity.ToJSON();
                                            return res;
                                        }
                                    }

                                    noPhone = false;
                                }
                                else if (cEntity.ColumnName.ToLower() == "vippasswrod")
                                {
                                    if (string.IsNullOrEmpty(cEntity.Value))
                                    {
                                        requestEntity.code = "6";
                                        requestEntity.description = "操作失败，密码不能为空";
                                        requestEntity.content = null;

                                        res = requestEntity.ToJSON();
                                        return res;
                                    }

                                    noVipPasswrod = false;
                                }
                            }
                        }

                        //获取前台传入的第一个控件ID
                        var pDefindInfo = new DynamicInterfaceBLL(loggingSessionInfo).GetPageBlockID(reqObj.special.Control[0].ControlID);
                        string status = null;
                        if (pDefindInfo != null && pDefindInfo.Count > 0)
                        {
                            //获取订单的状态
                            status = pDefindInfo[0].Remark.Split(',')[0];
                            //判断是否需要发送邮件
                            if (pDefindInfo[0].Remark.Contains("email"))
                            {
                                #region 邮件发送
                                try
                                {
                                    var pEmail = new DynamicInterfaceBLL(loggingSessionInfo).GetUserEmail(reqObj.common.userId);

                                    XmlManager xml = new XmlManager(ConfigurationManager.AppSettings["xmlFile"]);

                                    FromSetting fs = new FromSetting();
                                    fs.SMTPServer = xml.SelectNodeText("//Root/AssociationMail//SMTPServer", 0);
                                    fs.SendFrom = xml.SelectNodeText("//Root/AssociationMail//MailSendFrom", 0);
                                    fs.UserName = xml.SelectNodeText("//Root/AssociationMail//MailUserName", 0);
                                    fs.Password = xml.SelectNodeText("//Root/AssociationMail//MailUserPassword", 0);
                                    Mail.SendMail(fs, pEmail + "," + xml.SelectNodeText("//Root/AssociationMail//MailTo", 0), xml.SelectNodeText("//Root/AssociationMail//MailTitle", 0), xml.SelectNodeText("//Root/AssociationMail//MailContent", 0), null);
                                }
                                catch
                                {
                                    requestEntity.code = "1";
                                    requestEntity.description = "邮件发送操作失败";
                                    requestEntity.content = null;
                                }
                                #endregion
                            }
                            List<ControlUpdateEntity> cEntity = reqObj.special.Control;
                            ControlUpdateEntity entity = new BS.Entity.ControlUpdateEntity();
                            entity.ColumnName = "Status";
                            entity.Value = status;
                            cEntity.Add(entity);
                            reqObj.special.Control = cEntity;
                        }
                    }

                    //若Email、Phone和密码全部没有，且存在用户ID，则跳过这步验证
                    if (!(noEmail && noPhone && noVipPasswrod && !string.IsNullOrEmpty(reqObj.common.userId)))
                    {
                        if (noEmail || noPhone || noVipPasswrod)
                        {
                            requestEntity.code = "7";
                            requestEntity.description = "操作失败，Email、Phone或密码配置不正确";
                            requestEntity.content = null;

                            res = requestEntity.ToJSON();
                            return res;
                        }
                    }

                    string pUserID = reqObj.common.userId;

                    int result = new DynamicInterfaceBLL(loggingSessionInfo).register(reqObj);

                    if (result > 0)
                    {
                        if (string.IsNullOrEmpty(pUserID))
                        {
                            string pContent = null;
                            VipInfoMaillBLL vipInfoMaillBll = new VipInfoMaillBLL(loggingSessionInfo);
                            XmlManager xml = new XmlManager(ConfigurationManager.AppSettings["xmlFile"]);
                            if (loggingSessionInfo.ClientID == "a2573925f3b94a32aca8cac77baf6d33")
                            {
                                pContent = xml.GetNodeText("//Root//XieHuiBaoMailTemplate");
                            }
                            else if (loggingSessionInfo.ClientID == "e137e8e040bb4db3be17d90feeefa7bf")
                            {
                                pContent = xml.GetNodeText("//Root//XWTMailTemplate");
                            }
                            else if (loggingSessionInfo.ClientID == "75a232c2cf064b45b1b6393823d2431e")
                            {
                                pContent = xml.GetNodeText("//Root//EMBAMailTemplate");
                            }
                            vipInfoMaillBll.SendNoticeMail(email, pContent);
                        }
                        reqGetUserIDByOpenIDEntity reqEntity = new BS.Entity.reqGetUserIDByOpenIDEntity();
                        reqEntity.userId = reqObj.common.userId;
                        requestEntity.code = "200";
                        requestEntity.description = "操作成功";
                        requestEntity.content = reqEntity;
                    }
                    else
                    {
                        requestEntity.code = "1";
                        requestEntity.description = "操作失败";
                        requestEntity.content = null;
                    }
                }
            }
            res = requestEntity.ToJSON();
            return res;
        }
        #endregion

        #region addEventInfo
        //public string addEventInfo()
        //{
        //    string res = string.Empty;
        //    string reqContent = string.Empty;
        //    string email = string.Empty;
        //    RequestEntity<reqEventInfoEntity> requestEntity = new RequestEntity<reqEventInfoEntity>();
        //    if (!string.IsNullOrEmpty(Request["ReqContent"]))
        //    {
        //        reqContent = Request["ReqContent"];
        //        var reqObj = reqContent.DeserializeJSONTo<ReqData<submitActivityInfoEntity>>();
        //        if (reqObj != null)
        //        {
        //            var loggingSessionInfo = Default.GetBSLoggingSession(reqObj.common.customerId, "1");
        //            string pEventVipTicketID = new DynamicInterfaceBLL(loggingSessionInfo).submitEventsInfo(reqObj);
        //            if (!string.IsNullOrEmpty(pEventVipTicketID))
        //            {
        //                reqEventInfoEntity reqEntity = new BS.Entity.reqEventInfoEntity();
        //                reqEntity.OrderID = pEventVipTicketID.Split('|')[0];
        //                reqEntity.VipID = pEventVipTicketID.Split('|')[1];
        //                requestEntity.code = "200";
        //                requestEntity.description = "操作成功";
        //                requestEntity.content = reqEntity;
        //            }
        //            else
        //            {
        //                requestEntity.code = "1";
        //                requestEntity.description = "操作失败";
        //                requestEntity.content = null;
        //            }
        //        }
        //    }
        //    res = requestEntity.ToJSON();
        //    return res;
        //}

        public string addEventInfo()
        {
            string res = string.Empty;
            string reqContent = string.Empty;
            string email = string.Empty;
            RequestEntity<reqEventInfoEntity> requestEntity = new RequestEntity<reqEventInfoEntity>();
            if (!string.IsNullOrEmpty(Request["ReqContent"]))
            {
                reqContent = Request["ReqContent"];
                var reqObj = reqContent.DeserializeJSONTo<ReqData<submitActivityInfoEntity>>();
                if (reqObj != null)
                {
                    var loggingSessionInfo = Default.GetBSLoggingSession(reqObj.common.customerId, "1");
                    int result = new DynamicInterfaceBLL(loggingSessionInfo).submitEventsInfo(reqObj);
                    if (result > 0)
                    {
                        requestEntity.code = "200";
                        requestEntity.description = "操作成功";
                        requestEntity.content = null;
                    }
                    else
                    {
                        requestEntity.code = "1";
                        requestEntity.description = "操作失败";
                        requestEntity.content = null;
                    }
                }
            }
            res = requestEntity.ToJSON();
            return res;
        }
        #endregion

        #region 中欧接口
        #region getEventList 4.9 活动列表
        public string getEventList()
        {
            string res = string.Empty;
            string reqContent = string.Empty;
            RequestEntity<SearchListEntity<reqActivityListEntity[]>> requestEntity = new RequestEntity<SearchListEntity<reqActivityListEntity[]>>();
            if (!string.IsNullOrEmpty(Request["ReqContent"]))
            {
                reqContent = Request["ReqContent"];
                var reqObj = reqContent.DeserializeJSONTo<ReqData<getActivityListEntity>>();
                if (reqObj != null)
                {
                    var loggingSessionInfo = Default.GetBSLoggingSession(reqObj.common.customerId, "1");
                    SearchListEntity<reqActivityListEntity[]> searchListEntity = new DynamicInterfaceBLL(loggingSessionInfo).getEventList(reqObj);
                    if (searchListEntity != null)
                    {
                        requestEntity.code = "200";
                        requestEntity.description = "操作成功";
                        requestEntity.content = searchListEntity;
                    }
                    else
                    {
                        requestEntity.code = "1";
                        requestEntity.description = "操作失败";
                        requestEntity.content = null;
                    }
                }
            }
            res = requestEntity.ToJSON();
            return res;
        }
        #endregion

        #region getEventByEventID    4.10
        public string getEventByEventID()
        {
            string res = string.Empty;
            string reqContent = string.Empty;
            RequestEntity<reqActivityEntity> requestEntity = new RequestEntity<reqActivityEntity>();
            if (!string.IsNullOrEmpty(Request["ReqContent"]))
            {
                reqContent = Request["ReqContent"];
                var reqObj = reqContent.DeserializeJSONTo<ReqData<getActivityByActivityIDEntity>>();
                if (reqObj != null)
                {
                    var loggingSessionInfo = Default.GetBSLoggingSession(reqObj.common.customerId, "1");
                    reqActivityEntity pReqNewsEntity = new DynamicInterfaceBLL(loggingSessionInfo).getEventByEventID(reqObj);

                    if (pReqNewsEntity != null)
                    {
                        if (!string.IsNullOrEmpty(pReqNewsEntity.Activity.ActivityContent))
                        {
                            pReqNewsEntity.Activity.ActivityContent = HttpUtility.HtmlDecode(pReqNewsEntity.Activity.ActivityContent);
                        }
                        if (!string.IsNullOrEmpty(pReqNewsEntity.Activity.BeginTime))
                        {
                            DateTime dt = new DateTime();
                            if (DateTime.TryParse(pReqNewsEntity.Activity.BeginTime, out dt))
                            {
                                string pWeek = "";
                                switch (dt.DayOfWeek)
                                {
                                    case DayOfWeek.Friday:
                                        pWeek = "星期五";
                                        break;
                                    case DayOfWeek.Monday:
                                        pWeek = "星期一";
                                        break;
                                    case DayOfWeek.Saturday:
                                        pWeek = "星期六";
                                        break;
                                    case DayOfWeek.Sunday:
                                        pWeek = "星期天";
                                        break;
                                    case DayOfWeek.Thursday:
                                        pWeek = "星期四";
                                        break;
                                    case DayOfWeek.Tuesday:
                                        pWeek = "星期二";
                                        break;
                                    case DayOfWeek.Wednesday:
                                        pWeek = "星期三";
                                        break;
                                    default:
                                        pWeek = "";
                                        break;
                                }
                                pReqNewsEntity.Activity.ActivityTime = dt.ToString("yyyy-MM-dd") + " " + pWeek + " " + dt.ToString("HH:ss");
                            }

                        }
                        requestEntity.code = "200";
                        requestEntity.description = "操作成功";
                        requestEntity.content = pReqNewsEntity;
                    }
                    else
                    {
                        requestEntity.code = "1";
                        requestEntity.description = "操作失败";
                        requestEntity.content = null;
                    }
                }
            }
            res = requestEntity.ToJSON();
            return res;
        }
        #endregion

        #region submitEventInfo 4.11报名活动
        public string submitEventInfo()
        {
            string res = string.Empty;
            string reqContent = string.Empty;
            string email = string.Empty;
            RequestEntity<reqEventInfoEntity> requestEntity = new RequestEntity<reqEventInfoEntity>();
            if (!string.IsNullOrEmpty(Request["ReqContent"]))
            {
                reqContent = Request["ReqContent"];
                var reqObj = reqContent.DeserializeJSONTo<ReqData<submitActivityInfoEntity>>();
                if (reqObj != null)
                {
                    var loggingSessionInfo = Default.GetBSLoggingSession(reqObj.common.customerId, "1");
                    string pEventVipTicketID = new DynamicInterfaceBLL(loggingSessionInfo).submitEventInfo(reqObj);
                    if (!string.IsNullOrEmpty(pEventVipTicketID))
                    {
                        reqEventInfoEntity reqEntity = new BS.Entity.reqEventInfoEntity();
                        reqEntity.OrderID = pEventVipTicketID;
                        requestEntity.code = "200";
                        requestEntity.description = "操作成功";
                        requestEntity.content = reqEntity;
                    }
                    else
                    {
                        requestEntity.code = "1";
                        requestEntity.description = "操作失败";
                        requestEntity.content = null;
                    }
                }
            }
            res = requestEntity.ToJSON();
            return res;
        }
        #endregion
        #endregion
        #endregion

        #region 新版接口 2014-05-09
        #region GetEventAlbumByAlbumID    4.7 咨询详细
        public string GetEventAlbumByAlbumID()
        {
            string res = string.Empty;
            string reqContent = string.Empty;
            RequestEntity<reqLEventsAlbumEntity> requestEntity = new RequestEntity<reqLEventsAlbumEntity>();
            if (!string.IsNullOrEmpty(Request["ReqContent"]))
            {
                reqContent = Request["ReqContent"];
                var reqObj = reqContent.DeserializeJSONTo<ReqData<getLEventsAlbumEntity>>();
                if (reqObj != null)
                {
                    var loggingSessionInfo = Default.GetBSLoggingSession(reqObj.common.customerId, "1");
                    reqLEventsAlbumEntity pReqAlbumsEntity = new DynamicInterfaceBLL(loggingSessionInfo).GetEventAlbumByAlbumID(reqObj);
                    if (pReqAlbumsEntity != null)
                    {
                        requestEntity.code = "200";
                        requestEntity.description = "操作成功";
                        requestEntity.content = pReqAlbumsEntity;
                    }
                    else
                    {
                        requestEntity.code = "1";
                        requestEntity.description = "操作失败";
                        requestEntity.content = null;
                    }
                }
            }
            res = requestEntity.ToJSON();
            return res;
        }
        #endregion
        #endregion
        
    }
}
