/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/5/30 16:00:39
 * Description	:
 * 1st Modified On	:
 * 1st Modified By	:
 * 1st Modified Desc:
 * 1st Modifier EMail	:
 * 2st Modified On	:
 * 2st Modified By	:
 * 2st Modifier EMail	:
 * 2st Modified Desc:
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Reflection;
using JIT.Utility;
using JIT.Utility.ExtensionMethod;
using JIT.CPOS.BS.DataAccess;
using JIT.CPOS.BS.Entity;
using JIT.Utility.DataAccess;
using JIT.Utility.DataAccess.Query;
using JIT.Utility.Log;

namespace JIT.CPOS.BS.BLL
{
    /// <summary>
    /// 业务处理：  
    /// </summary>
    public partial class MarketPersonBLL
    {
        #region 获取活动人群信息
        public MarketPersonEntity GetMarketPersonByEventID(string EventID, int Page, int PageSize)
        {
            MarketPersonEntity marketPersonInfo = new MarketPersonEntity();
            try
            {
                DataSet ds = new DataSet();
                ds = _currentDAO.GetMarketPersonByEventID(EventID, Page, PageSize);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    marketPersonInfo.vipInfoList = DataTableToObject.ConvertToList<VipEntity>(ds.Tables[0]);
                    marketPersonInfo.ICount = _currentDAO.GetMarketPersonByEventIDCount(EventID);
                }
                return marketPersonInfo;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        #endregion

        #region GetList
        /// <summary>
        /// GetList
        /// </summary>
        /// <param name="entity">entity</param>
        /// <param name="Page">分页页码。从0开始</param>
        /// <param name="PageSize">每页的数量。未指定时默认为15</param>
        /// <returns></returns>
        public IList<MarketPersonEntity> GetList(MarketPersonEntity entity, int Page, int PageSize)
        {
            if (PageSize <= 0) PageSize = 15;

            IList<MarketPersonEntity> list = new List<MarketPersonEntity>();
            DataSet ds = new DataSet();
            ds = _currentDAO.GetList(entity, Page, PageSize);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                list = DataTableToObject.ConvertToList<MarketPersonEntity>(ds.Tables[0]);
            }
            return list;
        }
        /// <summary>
        /// GetListCount
        /// </summary>
        /// <param name="entity">entity</param>
        public int GetListCount(MarketPersonEntity entity)
        {
            return _currentDAO.GetListCount(entity);
        }
        #endregion

        public void WebDelete(MarketPersonEntity entity)
        {
            _currentDAO.WebDelete(entity);
        }

        #region 发布活动信息
        /// <summary>
        /// 发布活动
        /// </summary>
        /// <param name="EventID">活动标识</param>
        /// <param name="msgUrl">微信调用接口地址譬如：http://IP/ialumni/sendmessage.aspx </param>
        /// <returns></returns>
        public bool SetEventPush(string EventID, string msgUrl, string SendTypeId, bool sendWX, bool sendSMS, bool sendAPP)
        {
            try
            {
                if (sendAPP)
                {
                    PushIOSMessageBLL pushIOSMessageBLL = new PushIOSMessageBLL(CurrentUserInfo);
                    pushIOSMessageBLL.SetMarketPushApp(EventID);
                }

                DataSet ds = new DataSet();
                ds = _currentDAO.GetPersonPushInfo(EventID);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    WeiXinPushServer pushServer = new WeiXinPushServer();
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        string msgText = dr["TemplateContent"].ToString().Trim();
                        string msgSMSText = dr["TemplateContentSMS"].ToString().Trim();
                        //string msgText = BaseService.unHtml(dr["TemplateContent"].ToString().Trim());

                        //string msgText = BaseService.unHtml(dr["TemplateContent"].ToString().Trim());
                        //msgText = BaseService.NoHTML(msgText);

                        //BaseService.WriteLog("发布活动");
                        //BaseService.WriteLog("TemplateContent:  " + msgText);

                        string OpenID = dr["OpenID"].ToString().Trim();
                        msgText = msgText.Replace("#VIPNAME#", dr["VipName"].ToString().Trim());
                        msgText = msgText.Replace("#BRAND#", dr["Brand"].ToString().Trim());
                        msgText = msgText.Replace("#OPENID#", OpenID.Trim());
                        msgText = msgText.Replace("#USERID#", dr["VIPID"].ToString().Trim());
                        msgText = msgText.Replace("#CUSTOMERID#", CurrentUserInfo.CurrentUser.customer_id.Trim());

                        msgSMSText = msgSMSText.Replace("#VIPNAME#", dr["VipName"].ToString().Trim());
                        msgSMSText = msgSMSText.Replace("#BRAND#", dr["Brand"].ToString().Trim());
                        msgSMSText = msgSMSText.Replace("#OPENID#", OpenID.Trim());
                        msgSMSText = msgSMSText.Replace("#USERID#", dr["VIPID"].ToString().Trim());
                        msgSMSText = msgSMSText.Replace("#CUSTOMERID#", CurrentUserInfo.CurrentUser.customer_id.Trim());

                        if (SendTypeId == "1")
                        {
                            if (sendWX)
                            {
                                var sendFlag = pushServer.SetPushServer(msgUrl, msgText, OpenID);
                                (new MarketSendLogBLL(CurrentUserInfo)).Create(new MarketSendLogEntity()
                                {
                                    LogId = Common.Utils.NewGuid(),
                                    VipId = dr["VIPID"].ToString(),
                                    MarketEventId = EventID,
                                    TemplateContent = msgText,
                                    SendTypeId = SendTypeId,
                                    WeiXinUserId = OpenID,
                                    IsSuccess = sendFlag ? 1 : 0
                                });
                            }

                            if (sendSMS)
                            {
                                if (dr["Phone"] != DBNull.Value && dr["Phone"] != null && dr["Phone"].ToString().Trim().Length > 0)
                                {
                                    var sendFlag2 = Common.Utils.SMSSend(dr["Phone"].ToString().Trim(), msgSMSText);
                                    (new MarketSendLogBLL(CurrentUserInfo)).Create(new MarketSendLogEntity()
                                    {
                                        LogId = Common.Utils.NewGuid(),
                                        VipId = dr["VIPID"].ToString(),
                                        MarketEventId = EventID,
                                        TemplateContent = msgSMSText,
                                        SendTypeId = SendTypeId,
                                        Phone = dr["Phone"].ToString().Trim(),
                                        IsSuccess = sendFlag2 ? 1 : 0
                                    });
                                }
                            }

                        }
                        else
                        {
                            if (sendWX && msgText != null && msgText.Length > 0)
                            {
                                var sendFlag = pushServer.SetPushServer(msgUrl, msgText, OpenID);
                                (new MarketSendLogBLL(CurrentUserInfo)).Create(new MarketSendLogEntity()
                                {
                                    LogId = Common.Utils.NewGuid(),
                                    VipId = dr["VIPID"].ToString(),
                                    MarketEventId = EventID,
                                    TemplateContent = msgText,
                                    SendTypeId = SendTypeId,
                                    WeiXinUserId = OpenID,
                                    IsSuccess = sendFlag ? 1 : 0
                                });
                            }
                            else
                            {
                                if (sendSMS)
                                {
                                    if (dr["Phone"] != DBNull.Value && dr["Phone"] != null && dr["Phone"].ToString().Trim().Length > 0)
                                    {
                                        var sendFlag2 = Common.Utils.SMSSend(dr["Phone"].ToString().Trim(), msgSMSText);
                                        (new MarketSendLogBLL(CurrentUserInfo)).Create(new MarketSendLogEntity()
                                        {
                                            LogId = Common.Utils.NewGuid(),
                                            VipId = dr["VIPID"].ToString(),
                                            MarketEventId = EventID,
                                            TemplateContent = msgSMSText,
                                            SendTypeId = SendTypeId,
                                            Phone = dr["Phone"].ToString().Trim(),
                                            IsSuccess = sendFlag2 ? 1 : 0
                                        });
                                    }
                                }
                            }
                        }
                    }
                }
                else {
                    Loggers.Debug(new DebugLogInfo()
                    {
                        Message = string.Format("发送失败的客户标识:{0}", "不存在相关记得记录")
                    });
                }
                
                return true;
            }
            catch (Exception ex)
            {
                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format("SetPushServerError:{0}", ex.ToString())
                });
                return false;
            }
        }
        #endregion

        #region 获取邀约人数 Jermyn20130613
        /// <summary>
        /// 获取活动的邀约人数
        /// </summary>
        /// <param name="EventID"></param>
        /// <returns></returns>
        public int GetMarketPersonByEventID(string EventID)
        {
            return _currentDAO.GetMarketPersonByEventID(EventID);
        }
        #endregion

        #region 获取发送消息人数
        /// <summary>
        /// 获取发送消息人数
        /// </summary>
        public int GetMarketPersonSendCount(string EventID, int type)
        {
            return _currentDAO.GetMarketPersonSendCount(EventID, type);
        }
        #endregion

    }
}