/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/7/18 10:54:13
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
using System.Web;
using JIT.Utility.Log;
using JIT.CPOS.BS.BLL.WX;
using JIT.CPOS.BS.BLL.WX.Const;
using JIT.CPOS.DTO.Base;
using System.Data.SqlClient;
using JIT.CPOS.Common;
using System.Configuration;

namespace JIT.CPOS.BS.BLL
{
    /// <summary>
    /// ҵ����  
    /// </summary>
    public partial class LEventsBLL
    {
        #region ��ȡ���ƾƽ���
        /// <summary>
        /// ��ȡ���ƾƽ���
        /// </summary>
        /// <returns></returns>
        public DataSet GetSkus()
        {
            return this._currentDAO.GetSkus();
        }
        #endregion

        #region ��ȡ���ص�ͳ����Ϣ
        /// <summary>
        /// ��ȡ���ص�ͳ����Ϣ
        /// </summary>
        /// <param name="WeiXinId">΢�Ź��ںű�ʶ</param>
        /// <param name="EventId">���ʶ</param>
        /// <param name="loggingSessionInfo">��¼</param>
        /// <returns></returns>
        public LEventsEntity GetEventTotalInfo(string WeiXinId
                                            , string EventId
                                            , LoggingSessionInfo loggingSessionInfo
                                            , out string strError)
        {
            LEventsEntity eventInfo = new LEventsEntity();
            #region
            if (EventId == null || EventId.Equals(""))
            {
                strError = "����ѡ����Ʒ";
                return eventInfo;
            }
            if (WeiXinId == null || WeiXinId.Equals(""))
            {
                strError = "΢�Ź��ںŲ���Ϊ��";
                return eventInfo;
            }
            #endregion
            try
            {
                //1.��ȡ�ѹ�ע��Ա����
                VipBLL vipServer = new VipBLL(loggingSessionInfo);
                eventInfo.hasVipCount = vipServer.GetHasVipCount(WeiXinId);
                //2.��ȡ�²ɼ���Ա����
                eventInfo.newVipCount = vipServer.GetNewVipCount(WeiXinId);
                //3.��ȡ���µ�����
                InoutService inoutServer = new InoutService(loggingSessionInfo);
                eventInfo.hasOrderCount = inoutServer.GetHasOrderCount(EventId);
                //4.��ȡ�Ѹ������
                eventInfo.hasPayCount = inoutServer.GetHasPayCount(EventId);
                //5.��ȡ�����۶�����
                eventInfo.hasSalesAmount = inoutServer.GetHasSalesAmount(EventId);
                strError = "ok";
                return eventInfo;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        #endregion

        #region ��ȡ���ƾ�����
        /// <summary>
        /// ��ȡ���ƾ�����
        /// </summary>
        /// <returns></returns>
        public DataSet GetSkuDetail(string skuId)
        {
            return this._currentDAO.GetSkuDetail(skuId);
        }
        #endregion

        #region ��ȡ�����(�����)
        /// <summary>
        /// ��ȡ�����(�����)
        /// </summary>
        /// <returns></returns>
        public DataSet GetEventInfo(string eventId)
        {
            return this._currentDAO.GetEventInfo(eventId);
        }
        #endregion
        public DataSet GetNewEventInfo(string eventId)
        {
            return this._currentDAO.GetNewEventInfo(eventId);
        }
        public DataSet GetMaterialTextInfo(string eventId)
        {
            return this._currentDAO.GetMaterialTextInfo(eventId);
        }


        #region �Ƿ����ֳ�
        /// <summary>
        /// �Ƿ����ֳ�
        /// </summary>
        /// <param name="eventId">�ID</param>
        /// <param name="latitude">γ��</param>
        /// <param name="longitude">����</param>
        /// <returns></returns>
        public int IsSite(string eventId, string latitude, string longitude, float distance)
        {
            return this._currentDAO.IsSite(eventId, latitude, longitude, distance);
        }
        #endregion

        #region GetMessageEventList
        /// <summary>
        /// ��ȡ�б���Ϣ
        /// </summary>
        public LEventsEntity GetMessageEventList(LEventsEntity entity)
        {
            try
            {
                LEventsEntity obj = new LEventsEntity();

                IList<LEventsEntity> list = new List<LEventsEntity>();
                DataSet ds = _currentDAO.GetMessageEventList(entity);
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    list = DataTableToObject.ConvertToList<LEventsEntity>(ds.Tables[0]);
                }

                obj.EventList = list;

                return obj;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        #endregion

        #region getEventAlbums
        /// <summary>
        /// ���Ἧ��
        /// </summary>
        public ActivityMediaEntity getEventAlbums(ActivityMediaEntity entity, int page, int pageSize)
        {
            try
            {
                var result = new ActivityMediaEntity();

                IList<ActivityMediaEntity> list = new List<ActivityMediaEntity>();
                DataSet ds = _currentDAO.getEventAlbums(entity, page, pageSize);
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    list = DataTableToObject.ConvertToList<ActivityMediaEntity>(ds.Tables[0]);
                }

                result.ICount = _currentDAO.getEventAlbumsCount(entity);
                result.EntityList = list;
                return result;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        #endregion

        #region getEventOrders
        /// <summary>
        /// ���������
        /// </summary>
        public InoutInfo getEventOrders(InoutInfo entity, int page, int pageSize)
        {
            try
            {
                var result = new InoutInfo();

                IList<InoutInfo> list = new List<InoutInfo>();
                DataSet ds = _currentDAO.getEventOrders(entity, page, pageSize);
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    list = DataTableToObject.ConvertToList<InoutInfo>(ds.Tables[0]);
                }

                result.ICount = _currentDAO.getEventOrdersCount(entity);
                result.InoutInfoList = list;
                return result;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        #endregion

        #region getEventItemSales
        /// <summary>
        /// ��Ʒ��������
        /// </summary>
        public InoutInfo getEventItemSales(InoutInfo entity, int page, int pageSize)
        {
            try
            {
                var result = new InoutInfo();

                IList<InoutDetailInfo> list = new List<InoutDetailInfo>();
                DataSet ds = _currentDAO.getEventItemSales(entity, page, pageSize);
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    list = DataTableToObject.ConvertToList<InoutDetailInfo>(ds.Tables[0]);
                }

                result.ICount = _currentDAO.getEventItemSalesCount(entity);
                result.InoutDetailList = list;
                return result;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        #endregion

        #region ͳ�ƻ��Ϣ����
        /// <summary>
        /// ͳ�ƻ��Ϣ����
        /// </summary>
        /// <param name="yearMonth">�����(��ʽ��2013-07)</param>
        /// <param name="eventStatus">�״̬(1=�ѽ�����0=δ����)</param>
        /// <returns></returns>
        public int GetEventListCount(string yearMonth, string eventStatus)
        {
            return this._currentDAO.GetEventListCount(yearMonth, eventStatus);
        }
        #endregion

        #region ��ȡͳ�ƻ��Ϣ�б�
        /// <summary>
        /// ��ȡͳ�ƻ��Ϣ�б�
        /// </summary>
        /// <param name="yearMonth">�����(��ʽ��2013-07)</param>
        /// <param name="eventStatus">�״̬(1=�ѽ�����0=δ����)</param>
        /// <param name="Page">ҳ��</param>
        /// <param name="PageSize">ҳ������</param>
        /// <returns></returns>
        public DataSet GetEventList(string yearMonth, string eventStatus, int Page, int PageSize)
        {
            return this._currentDAO.GetEventList(yearMonth, eventStatus, Page, PageSize);
        }
        #endregion

        #region ��ȡ���ϸ��Ϣ
        /// <summary>
        /// ��ȡ���ϸ��Ϣ
        /// </summary>
        /// <param name="eventId">���ʶ</param>
        /// <returns></returns>
        public DataSet GetEventDetail(string eventId)
        {
            return this._currentDAO.GetEventDetail(eventId);
        }
        #endregion

        #region ͳ�ƻ������Ա����
        /// <summary>
        /// ͳ�ƻ������Ա����
        /// </summary>
        /// <param name="eventId">���ʶ</param>
        /// <returns></returns>
        public int GetEventSignUpUserInfoCount(string eventId)
        {
            return this._currentDAO.GetEventSignUpUserInfoCount(eventId);
        }
        #endregion

        #region ��ȡ�������Ա�б�
        /// <summary>
        /// ��ȡ�������Ա�б�
        /// </summary>
        /// <param name="eventId">���ʶ</param>
        /// <param name="Page">ҳ��</param>
        /// <param name="PageSize">ҳ������</param>
        /// <returns></returns>
        public DataSet GetEventSignUpUserInfo(string eventId, int Page, int PageSize)
        {
            return this._currentDAO.GetEventSignUpUserInfo(eventId, Page, PageSize);
        }
        #endregion

        #region Web��б��ȡ
        /// <summary>
        /// ��б��ȡ
        /// </summary>
        public IList<LEventsEntity> WEventGetWebEvents(LEventsEntity eventsEntity, int Page, int PageSize)
        {
            if (PageSize <= 0) PageSize = 15;

            IList<LEventsEntity> eventsList = new List<LEventsEntity>();
            DataSet ds = new DataSet();
            ds = _currentDAO.WEventGetWebEvents(eventsEntity, Page, PageSize);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                LEventSignUpDAO signServer = new LEventSignUpDAO(this.CurrentUserInfo);
                eventsList = DataTableToObject.ConvertToList<LEventsEntity>(ds.Tables[0]);


                if (CurrentUserInfo.ClientID != "a2573925f3b94a32aca8cac77baf6d33" && CurrentUserInfo.ClientID != "75a232c2cf064b45b1b6393823d2431e" && CurrentUserInfo.ClientID != "376f4455b43647fd8bda39a3bb39eb73" && CurrentUserInfo.ClientID != "1c6a39e4a9e54fecb508abfa5cda9eaa" && CurrentUserInfo.ClientID != "56319f7e9c74424dba95b8e96d2487bc")
                {
                    foreach (var eventInfo in eventsList)
                    {
                        eventInfo.AppliesCount = signServer.GetEventAppliesCount2(eventInfo.EventID);
                    }
                }

            }
            return eventsList;
        }
        /// <summary>
        /// ��б�������ȡ
        /// </summary>
        public int WEventGetWebEventsCount(LEventsEntity eventsEntity)
        {
            return _currentDAO.WEventGetWebEventsCount(eventsEntity);
        }
        #endregion

        #region ��ȡ�ҵ��Ϲ�����
        public int GetMyOrderCount(string eventId, string openId)
        {
            return _currentDAO.GetMyOrderCount(eventId, openId);
        }
        #endregion

        #region ��ȡwap��Ҫ�Ļ��Ϣ Jermyn20130813
        /// <summary>
        /// ��ȡ���ϸ
        /// </summary>
        /// <param name="EventID"></param>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public GetResponseParams<LEventsEntity> WEventGetEventDetail(string EventID, string UserID)
        {
            #region �ж϶�����Ϊ��
            if (EventID == null || EventID.ToString().Equals(""))
            {
                return new GetResponseParams<LEventsEntity>
                {
                    Flag = "0",
                    Code = "419",
                    Description = "���ʶ����Ϊ��",
                };
            }
            #endregion

            GetResponseParams<LEventsEntity> response = new GetResponseParams<LEventsEntity>();
            response.Flag = "1";
            response.Code = "200";
            response.Description = "�ɹ�";
            try
            {
                LEventsEntity eventsInfo = new LEventsEntity();
                DataSet ds = new DataSet();
                ds = this._currentDAO.GetEventDetailById(EventID); ;
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    eventsInfo = DataTableToObject.ConvertToObject<LEventsEntity>(ds.Tables[0].Rows[0]);
                }
                response.Params = eventsInfo;
                return response;
            }
            catch (Exception ex)
            {
                response.Flag = "0";
                response.Code = "103";
                response.Description = "ʧ��" + ":" + ex.ToString();
                return response;
            }
        }

        #region ������������ύ
        /// <summary>
        /// ������������ύ
        /// </summary>
        public GetResponseParams<bool> WEventSubmitEventApply(string EventID, string UserID,
            WEventUserMappingEntity userMappingEntity, IList<QuesAnswerEntity> quesAnswerList)
        {
            #region �ж϶�����Ϊ��
            if (EventID == null || EventID.ToString().Equals(""))
            {
                return new GetResponseParams<bool>
                {
                    Flag = "0",
                    Code = "419",
                    Description = "���ʶΪ��",
                };
            }
            #endregion

            GetResponseParams<bool> response = new GetResponseParams<bool>();
            response.Flag = "1";
            response.Code = "200";
            response.Description = "�ɹ�";
            try
            {
                var quesAnswerBLL = new QuesAnswerBLL(CurrentUserInfo);

                // WEventUserMapping
                var wEventUserMappingBLL = new WEventUserMappingBLL(CurrentUserInfo);
                //if (!wEventUserMappingBLL.ExsitWEventUserMapping(userMappingEntity))
                //{
                userMappingEntity.Mapping = Common.Utils.NewGuid();
                userMappingEntity.EventID = EventID;
                userMappingEntity.UserID = UserID;
                wEventUserMappingBLL.Create(userMappingEntity);
                //}

                // QuesAnswer
                if (quesAnswerList != null)
                {
                    //���ݻɾ���������д�
                    bool bRetun = quesAnswerBLL.DeleteQuesAnswerByEventID(EventID, UserID);
                    string createBy = BaseService.NewGuidPub();
                    foreach (var quesAnswerItem in quesAnswerList)
                    {
                        quesAnswerBLL.SubmitQuesQuestionAnswerWEvent(userMappingEntity.UserID,
                            quesAnswerItem.QuestionID, quesAnswerItem.QuestionValue, createBy);
                    }
                }
                //int iBool = _currentDAO.SetEventApplyCount(EventID, UserID);
                response.Params = true;
                return response;
            }
            catch (Exception ex)
            {
                response.Flag = "0";
                response.Code = "103";
                response.Description = "ʧ��";//+ ":" + ex.ToString();
                return response;
            }

        }
        #endregion

        #endregion

        #region ��ȡ��У�ջ
        /// <summary>
        /// ��ȡ��У�ջ
        /// </summary>
        /// <param name="eventId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public LEventsEntity getSchoolEventList(string eventId, string userId)
        {
            LEventsEntity eventInfo = new LEventsEntity();
            DataSet ds = _currentDAO.GetEventFXInfo(eventId, userId);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                eventInfo = DataTableToObject.ConvertToObject<LEventsEntity>(ds.Tables[0].Rows[0]);
                eventInfo.IsSignUp = 1;
                DataSet ds1 = _currentDAO.getSchoolEventList(eventId, userId);
                if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
                {
                    IList<LEventsEntity> eventList = new List<LEventsEntity>();
                    eventList = DataTableToObject.ConvertToList<LEventsEntity>(ds1.Tables[0]);
                    eventInfo.EventList = eventList;
                }
            }


            return eventInfo;
        }

        /// <summary>
        /// ɾ�������
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public bool SetApplyEventDelete(string userId)
        {
            _currentDAO.SetApplyEventDelete(userId);
            return true;
        }
        #endregion

        #region ����΢�ŵĹ̶���ά���ȡ���Ϣ
        /// <summary>
        /// ����΢�ŵĹ̶���ά���ȡ���Ϣ
        /// </summary>
        /// <param name="wxCode"></param>
        /// <returns></returns>
        public LEventsEntity GetEventInfoByWX(string wxCode)
        {
            DataSet ds = new DataSet();
            LEventsEntity info = new LEventsEntity();
            IList<LEventsEntity> list = new List<LEventsEntity>();
            ds = _currentDAO.GetEventInfoByWX(wxCode);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                info = DataTableToObject.ConvertToObject<LEventsEntity>(ds.Tables[0].Rows[0]);
            }
            return info;
        }
        #endregion

        #region ����΢�ŵĹ̶���ά���ȡ���Ϣ Jermyn20140530
        /// <summary>
        /// ����΢�ŵĹ̶���ά���ȡ���Ϣ
        /// </summary>
        /// <param name="wxCode"></param>
        /// <returns></returns>
        public LEventsEntity GetEventInfoByWX111(string wxCode)
        {
            DataSet ds = new DataSet();
            LEventsEntity info = new LEventsEntity();
            IList<LEventsEntity> list = new List<LEventsEntity>();
            ds = _currentDAO.GetEventInfoByWX(wxCode);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                info = DataTableToObject.ConvertToObject<LEventsEntity>(ds.Tables[0].Rows[0]);
            }
            return info;
        }
        #endregion

        #region ��ע�̶���ά�룬���ͻ��Ϣ Jermyn20131209
        public bool SetEventWXPush(LEventsEntity eventInfo, string WeiXin, string OpenId, string VipId, string msgUrl, out string strError, string AuthUrl, int iRad)
        {
            try
            {
                MarketSendLogBLL logServer = new MarketSendLogBLL(this.CurrentUserInfo);
                Random rad = new Random();
                if (eventInfo == null || eventInfo.ModelId == null || eventInfo.ModelId.Equals(""))
                {
                    strError = "��ȡ��Ϣ��ȫ��ȱ��ģ�塣";
                    return false;
                }
                #region
                WEventUserMappingBLL eventUserMapping = new WEventUserMappingBLL(CurrentUserInfo);
                int eventPersonCount = 0;
                eventPersonCount = eventUserMapping.GetEventSignInCount(eventInfo.EventID);


                #endregion
                WApplicationInterfaceBLL wAServer = new WApplicationInterfaceBLL(this.CurrentUserInfo);
                var wxArray = wAServer.QueryByEntity(new WApplicationInterfaceEntity
                {
                    WeiXinID = WeiXin
                    ,
                    IsDelete = 0
                    ,
                    CustomerId = this.CurrentUserInfo.CurrentUser.customer_id
                }, null);
                if (wxArray == null || wxArray.Length == 0 || wxArray[0].AppID == null || wxArray[0].AppID.Equals(""))
                {
                    strError = "�����ڶ�Ӧ��΢���ʺ�";
                    return false;
                }
                else
                {
                    WApplicationInterfaceEntity wxInfo = wxArray[0];
                    WX.CommonBLL server = new WX.CommonBLL();
                    JIT.CPOS.BS.Entity.WX.SendMessageEntity sendMessageInfo = new Entity.WX.SendMessageEntity();

                    WMaterialTextBLL wTextServer = new WMaterialTextBLL(this.CurrentUserInfo);
                    IList<WMaterialTextEntity> textlist = new List<WMaterialTextEntity>();
                    textlist = wTextServer.GetMaterialTextListByModelId(eventInfo.ModelId);

                    if (textlist != null && textlist.Count > 0 && textlist[0].TextId != null)
                    {
                        #region
                        VipBLL vipServer = new VipBLL(CurrentUserInfo);
                        VipEntity vipInfo = vipServer.GetByID(VipId);
                        sendMessageInfo.msgtype = "news";
                        sendMessageInfo.touser = OpenId;
                        List<JIT.CPOS.BS.Entity.WX.NewsEntity> newsList = new List<JIT.CPOS.BS.Entity.WX.NewsEntity>();
                        foreach (var info in textlist)
                        {
                            JIT.CPOS.BS.Entity.WX.NewsEntity newsInfo = new Entity.WX.NewsEntity();
                            newsInfo.title = info.Title;
                            if (vipInfo != null && !vipInfo.VIPID.Equals(""))
                            {
                                newsInfo.description = info.Author.Replace("#VIPNAME#", vipInfo.VipName);
                            }
                            else
                            {
                                newsInfo.description = info.Author;
                            }

                            newsInfo.description = newsInfo.description.Replace("#PERSONCOUNT#", Convert.ToString(eventPersonCount));
                            //string url = info.OriginalUrl;
                            //JIT.Utility.Log.Loggers.Debug(new DebugLogInfo()
                            //{
                            //    Message = string.Format("����ԭ�����ӳ���{0},url:{1};Status:{2};",)
                            //});
                            if (info.OriginalUrl != null && !info.OriginalUrl.Equals("") && vipInfo.Status != null && !vipInfo.Status.ToString().Equals(""))
                            {
                                if (vipInfo.Status.Equals(1) && info.OriginalUrl.IndexOf("Fuxing") > 0)
                                {
                                    newsInfo.description = info.Text;
                                }
                                else
                                {

                                }
                            }

                            if (info.OriginalUrl.IndexOf("?") > 0)
                            {
                                newsInfo.url = info.OriginalUrl + "&rnd=" + rad.Next(1000, 100000) + "";
                            }
                            else
                            {
                                string goUrl = info.OriginalUrl + "?1=1&applicationId=" + wxInfo.ApplicationId + "&eventId=" + eventInfo.EventID + "&openId=" + OpenId + "&userId=" + VipId + "";
                                goUrl = HttpUtility.UrlEncode(goUrl);

                                newsInfo.url = AuthUrl + "OnlineClothing/go.htm?customerId=" + this.CurrentUserInfo.CurrentUser.customer_id
                                                                    + "&applicationId=" + wxInfo.ApplicationId
                                                                    + "&openId=" + OpenId
                                                                    + "&userId=" + VipId
                                                                    + "&backUrl=" + goUrl + "";
                            }
                            //OnlineClothing/go.htm?customerId=" + customerId + "&openId=" + OpenId + "&userId=" + vipId + "&backUrl=" + HttpUtility.UrlEncode(goUrl) + "";
                            newsInfo.picurl = info.CoverImageUrl;
                            newsList.Add(newsInfo);
                        }
                        sendMessageInfo.articles = newsList;
                        #endregion
                        #region ������־

                        MarketSendLogEntity logInfo1 = new MarketSendLogEntity();
                        logInfo1.LogId = BaseService.NewGuidPub();
                        logInfo1.IsSuccess = 1;
                        logInfo1.MarketEventId = eventInfo.EventID;
                        logInfo1.SendTypeId = "2";
                        logInfo1.Phone = iRad.ToString();
                        if (sendMessageInfo.ToJSON().ToString().Length > 2000)
                        {
                            logInfo1.TemplateContent = sendMessageInfo.ToJSON().ToString().Substring(1, 1999);
                        }
                        else
                        {
                            logInfo1.TemplateContent = sendMessageInfo.ToJSON().ToString();
                        }
                        logInfo1.VipId = VipId;
                        logInfo1.WeiXinUserId = OpenId;
                        logInfo1.CreateTime = System.DateTime.Now;
                        logServer.Create(logInfo1);
                        #endregion
                    }

                    var ResultEntity = server.SendMessage(sendMessageInfo, wxInfo.AppID, wxInfo.AppSecret, this.CurrentUserInfo, true);


                    #region Jermyn20140110 ������������λ��Ϣ������ʱ��
                    //FStaffBLL staffServer = new FStaffBLL(this.CurrentUserInfo); 
                    //bool bReturn = staffServer.SetStaffSeatsPush(VipId, eventInfo.EventID, out strError);
                    //MarketSendLogEntity logInfo2 = new MarketSendLogEntity();
                    //logInfo2.LogId = BaseService.NewGuidPub();
                    //logInfo2.IsSuccess = 1;
                    //logInfo2.MarketEventId = eventInfo.EventID;
                    //logInfo2.SendTypeId = "2";
                    //logInfo2.TemplateContent = strError;
                    //logInfo2.Phone = iRad.ToString();
                    //logInfo2.VipId = VipId;
                    //logInfo2.WeiXinUserId = OpenId;
                    //logInfo2.CreateTime = System.DateTime.Now;
                    //logServer.Create(logInfo2);

                    #endregion
                    strError = "ok";
                    return true;
                }

            }
            catch (Exception ex)
            {
                strError = ex.ToString();
                return false;
            }
        }
        #endregion

        public Guid SaveEventQRCode(string EventID, LEventsEntity eventsEntity, LEventsEntity tmpObj)
        {
            Guid QRCodeId = Guid.NewGuid();
            #region ���ɶ�ά��
            if (!string.IsNullOrEmpty(eventsEntity.WXCodeImageUrl))
            {
                var QRCodeManagerentity = new WQRCodeManagerBLL(this.CurrentUserInfo).QueryByEntity(new WQRCodeManagerEntity
                {
                    ObjectId = EventID
                }, null).FirstOrDefault();
                if (QRCodeManagerentity != null)
                {
                    QRCodeId = (Guid)QRCodeManagerentity.QRCodeId;
                }
                if (QRCodeManagerentity == null)
                {
                    var wqrentity = new WQRCodeTypeBLL(this.CurrentUserInfo).QueryByEntity(
                        new WQRCodeTypeEntity { TypeCode = "EventQrcode" }
                    , null).FirstOrDefault();

                    var wapentity = new WApplicationInterfaceBLL(this.CurrentUserInfo).QueryByEntity(new WApplicationInterfaceEntity
                    {
                        CustomerId = this.CurrentUserInfo.ClientID,
                        IsDelete = 0
                    }, null).FirstOrDefault();

                    var WQRCodeManagerbll = new WQRCodeManagerBLL(this.CurrentUserInfo);
                    if (tmpObj == null)
                    {
                        if (!string.IsNullOrEmpty(eventsEntity.WXCodeImageUrl))
                        {
                            WQRCodeManagerbll.Create(new WQRCodeManagerEntity
                            {
                                QRCodeId = QRCodeId,
                                QRCode = eventsEntity.MaxWQRCod,
                                QRCodeTypeId = wqrentity.QRCodeTypeId,
                                IsUse = 1,
                                ObjectId = EventID,
                                CreateBy = this.CurrentUserInfo.UserID,
                                ApplicationId = wapentity.ApplicationId,
                                IsDelete = 0,
                                ImageUrl = eventsEntity.WXCodeImageUrl,
                                CustomerId = this.CurrentUserInfo.ClientID

                            });
                        }
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(eventsEntity.WXCodeImageUrl))
                        {
                            var entity = WQRCodeManagerbll.QueryByEntity(new WQRCodeManagerEntity() { ObjectId = EventID }, null).FirstOrDefault();
                            if (entity == null)
                            {
                                if (!string.IsNullOrEmpty(eventsEntity.WXCodeImageUrl))
                                {
                                    WQRCodeManagerbll.Create(new WQRCodeManagerEntity
                                    {
                                        QRCodeId = QRCodeId,
                                        QRCode = eventsEntity.MaxWQRCod,
                                        QRCodeTypeId = wqrentity.QRCodeTypeId,
                                        IsUse = 1,
                                        ObjectId = EventID,
                                        CreateBy = this.CurrentUserInfo.UserID,
                                        ApplicationId = wapentity.ApplicationId,
                                        IsDelete = 0,
                                        ImageUrl = eventsEntity.WXCodeImageUrl,
                                        CustomerId = this.CurrentUserInfo.ClientID

                                    });
                                }
                            }

                        }

                    }
                }
            }

            #endregion
            return QRCodeId;
        }

        public string SaveWKeywordReply(LEventsEntity eventsEntity, Guid QRCodeId)
        {
            #region ����ؼ��ֻظ�
            var WKeywordReplyentity = new WKeywordReplyBLL(this.CurrentUserInfo).QueryByEntity(new WKeywordReplyEntity()
            {
                Keyword = QRCodeId.ToString()

            }, null).FirstOrDefault();
            var ReplyBLL = new WKeywordReplyBLL(this.CurrentUserInfo);
            var ReplyId = Guid.NewGuid().ToString();
            if (WKeywordReplyentity == null)
            {
                //΢�� ����ƽ̨
                var wapentity = new WApplicationInterfaceBLL(this.CurrentUserInfo).QueryByEntity(new WApplicationInterfaceEntity
                {
                    CustomerId = this.CurrentUserInfo.ClientID,
                    IsDelete = 0
                }, null).FirstOrDefault();

                if (eventsEntity.ReplyType == 1)
                {
                    ReplyBLL.Create(new WKeywordReplyEntity()
                    {
                        ReplyId = ReplyId,
                        Keyword = QRCodeId.ToString(),
                        ReplyType = 1,
                        Text = eventsEntity.Text,
                        CreateBy = this.CurrentUserInfo.UserID,
                        ApplicationId = wapentity.ApplicationId,
                        KeywordType = 4
                    });
                }
                else
                {
                    ReplyBLL.Create(new WKeywordReplyEntity()
                    {
                        ReplyId = ReplyId,
                        Keyword = QRCodeId.ToString(),
                        ReplyType = 3,
                        KeywordType = 4,
                        IsDelete = 0,
                        CreateBy = this.CurrentUserInfo.UserID,
                        ApplicationId = wapentity.ApplicationId,
                    });
                }

            }
            else
            {
                ReplyId = WKeywordReplyentity.ReplyId;

                if (eventsEntity.ReplyType == 1)
                {
                    WKeywordReplyentity.Text = eventsEntity.Text;
                    WKeywordReplyentity.ReplyType = 1;
                    ReplyBLL.Update(WKeywordReplyentity);
                }
                else
                {
                    WKeywordReplyentity.Text = "";
                    WKeywordReplyentity.ReplyType = 3;
                    ReplyBLL.Update(WKeywordReplyentity);
                }
            }
            #endregion
            return ReplyId;
        }

        public DataSet GetLeventsInfoDataSet(string eventId)
        {
            return this._currentDAO.GetLeventsInfoDataSet(eventId);
        }

        public DataSet GetBootUrlByEventId(string eventId)
        {
            return this._currentDAO.GetBootUrlByEventId(eventId);
        }

        public void SendQrCodeWxMessage(LoggingSessionInfo loggingSessionInfo, string customerId, 
            string weixinId,string qrCode,string openId, HttpContext httpContext)
        {

            try
            {

                var qrCodeBll = new WQRCodeManagerBLL(loggingSessionInfo);

                var qrCodeEntity = qrCodeBll.QueryByEntity(new WQRCodeManagerEntity()
                {
                    CustomerId = customerId,
                    QRCode = qrCode
                }, null).FirstOrDefault();
                Loggers.Debug(new DebugLogInfo() { Message = string.Format("zk qrCodeEntity != null:{0},customerId:{1},qrCode:{2}", qrCodeEntity != null,customerId,qrCode) });
                if (qrCodeEntity != null)
                {
                    #region Jermyn20140819 �ж϶�ά������
                    WQRCodeTypeBLL wqrcodeTypServer = new WQRCodeTypeBLL(loggingSessionInfo);
                    var qrCodeTypeInfo = wqrcodeTypServer.QueryByEntity(new WQRCodeTypeEntity()
                    {
                        QRCodeTypeId = qrCodeEntity.QRCodeTypeId
                    }, null).FirstOrDefault();
                    Loggers.Debug(new DebugLogInfo()
                    {
                        Message = string.Format(@"zk qrCodeTypeInfo != null:{0},
                                qrCodeTypeInfo.TypeCode != null:{1},!qrCodeTypeInfo.TypeCode.Equals(""):{2},
                                qrCodeEntity.ObjectId != null:{3},!qrCodeEntity.ObjectId.ToString().Equals(""):{4}", qrCodeTypeInfo != null, qrCodeTypeInfo.TypeCode != null,
                            !qrCodeTypeInfo.TypeCode.Equals(""), qrCodeEntity.ObjectId != null, !qrCodeEntity.ObjectId.ToString().Equals(""))
                    });
                    if (qrCodeTypeInfo != null && qrCodeTypeInfo.TypeCode != null
                        && !qrCodeTypeInfo.TypeCode.Equals("")
                        && qrCodeEntity.ObjectId != null
                        && !qrCodeEntity.ObjectId.ToString().Equals(""))
                    {
                        switch (qrCodeTypeInfo.TypeCode.ToString().ToLower())
                        {
                            case "userqrcode"://��Ա��ά��
                                #region �󶨻Ἦ��
                                VipBLL vipBll = new VipBLL(loggingSessionInfo);

                                var vipEntity = vipBll.QueryByEntity(new VipEntity()
                                {
                                    WeiXinUserId = openId
                                }, null).FirstOrDefault();
                                if (vipEntity != null && (string.IsNullOrEmpty(vipEntity.CouponInfo) || vipEntity.CouponInfo.Length != 32))
                                {
                                    //UnitService unitServer = new UnitService(loggingSessionInfo);
                                    //var userOnUnit = unitServer.GetUnitListByUserId(qrCodeEntity.ObjectId);
                                    var unitid = vipBll.GetUnitByUserId(qrCodeEntity.ObjectId);
                                    vipEntity.CouponInfo = !string.IsNullOrEmpty(unitid) ? unitid : vipEntity.CouponInfo;//��ȡ�Ἧ��//userOnUnit[0].unit_id;//�Ἦ��ID
                                    vipEntity.SetoffUserId = qrCodeEntity.ObjectId;//��ԱID�����ߣ�
                                    vipBll.Update(vipEntity);
                                    //var bll = new VipOrderSubRunObjectMappingBLL(loggingSessionInfo);
                                    //dynamic o = bll.SetVipOrderSubRun(loggingSessionInfo.ClientID, vipEntity.VIPID, 3, userOnUnit[0].Id);
                                    //Type t = o.GetType();
                                    //var Desc = t.GetProperty("Desc").GetValue(o, null).ToString();
                                    //var IsSuccess = t.GetProperty("IsSuccess").GetValue(o, null).ToString();
                                    //if (IsSuccess == "0")
                                    //{
                                    //    Loggers.Debug(new DebugLogInfo()
                                    //    {
                                    //        Message = string.Format("����󶨻Ἦ��ʱ����{0} ", Desc)
                                    //    });
                                    //}
                                }
                                #endregion
                                break;
                            case "unitqrcode"://�ŵ��ά��
                                #region �󶨻Ἦ��
                                VipBLL vipServer = new VipBLL(loggingSessionInfo);
                                
                                var vipInfo = vipServer.QueryByEntity(new VipEntity()
                                {
                                    WeiXinUserId = openId
                                }, null).FirstOrDefault();
                                Loggers.Debug(new DebugLogInfo() { Message = string.Format("vipInfo!=null:{0},openid:{1},vipInfo.CouponInfo:{2}", vipInfo != null, openId, vipInfo.CouponInfo) });
                                if (vipInfo != null && (string.IsNullOrEmpty(vipInfo.CouponInfo) || vipInfo.CouponInfo.Length != 32))
                                {
                                    //vipInfo.CouponInfo = qrCodeEntity.ObjectId.ToString();
                                    //vipInfo.Col50 = "�Ѿ��󶨻Ἦ��:" + Convert.ToString(System.DateTime.Now);
                                    //vipServer.Update(vipInfo, false);
                                    var bll = new VipOrderSubRunObjectMappingBLL(loggingSessionInfo);
                                    dynamic o = bll.SetVipOrderSubRun(loggingSessionInfo.ClientID, vipInfo.VIPID, 3, qrCodeEntity.ObjectId.ToString());
                                    Type t = o.GetType();
                                    var Desc = t.GetProperty("Desc").GetValue(o, null).ToString();
                                    var IsSuccess = t.GetProperty("IsSuccess").GetValue(o, null).ToString();
                                    if (IsSuccess == "0")
                                    {
                                        Loggers.Debug(new DebugLogInfo()
                                        {
                                            Message = string.Format("����󶨻Ἦ��ʱ����{0} ", Desc)
                                        });
                                    }
                                }
                                #endregion
                                break;
                            case "retailqrcode"://��������Ϣ
                                #region �󶨷���������
                                VipBLL vipBll2 = new VipBLL(loggingSessionInfo);

                                var vipEntity2 = vipBll2.QueryByEntity(new VipEntity()
                                {
                                    WeiXinUserId = openId
                                }, null).FirstOrDefault();
                                if (vipEntity2 != null && (string.IsNullOrEmpty(vipEntity2.CouponInfo) || vipEntity2.CouponInfo.Length != 32))
                                {
                                    RetailTraderBLL retailTraderBLL = new RetailTraderBLL(loggingSessionInfo);

                                    var RetailTraderInfo = retailTraderBLL.GetByID(qrCodeEntity.ObjectId);  //���ݷ�����ID��ȡ�������ŵ���Ϣ������Ա��Ϣ
                                    vipEntity2.CouponInfo = RetailTraderInfo.UnitID;//�Ἦ��ID
                                    // vipEntity2.SetoffUserId = qrCodeEntity.ObjectId;//��ԱID�����ߣ�
                                    vipEntity2.Col20 = qrCodeEntity.ObjectId;//�Ἦ��ID
                                    vipBll2.Update(vipEntity2);

                                }

                                //�������̺�����Ա����

                                #endregion
                                break;
                            default:
                                break;
                        }
                    }
                    #endregion

                    if (weixinId != "")//�����زĵ�
                    {
                        QrCodeHandlerText(qrCodeEntity.QRCodeId.ToString(), loggingSessionInfo,
                            weixinId, 4, openId, httpContext);
                    }
                }
            }
            catch (Exception ex) {
                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format(
                        "ɨ��΢�Ŷ�ά��ʱ,����������� ", ex.ToString())
                });
            }
        }


        public void QrCodeHandlerText(string content, LoggingSessionInfo loggingSessionInfo,
            string weixinId, int keywordType, string openId, HttpContext httpContext)
        {
            var keywordDAO = new WKeywordReplyDAO(loggingSessionInfo);


            var ds = keywordDAO.GetMaterialByKeywordJermyn(content, weixinId, keywordType);

            BaseService.WriteLogWeixin("��ά�룺");

            BaseService.WriteLogWeixin("content:" + content);
            BaseService.WriteLogWeixin("weixinId:" + weixinId);
            BaseService.WriteLogWeixin(ds.Tables[0].Rows.Count.ToString());

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)//ֻ�ظ�һ���ؼ���
            {
                string Text = ds.Tables[0].Rows[0]["Text"].ToString();  //�ز�����
                string ReplyId = ds.Tables[0].Rows[0]["ReplyId"].ToString();  //�ز�ID
                string typeId = ds.Tables[0].Rows[0]["ReplyType"].ToString();  //�ز�ID

                BaseService.WriteLogWeixin("ReplyId��" + ReplyId);
                BaseService.WriteLogWeixin("typeId��" + typeId);
                var commonService = new CommonBLL();

                switch (typeId)
                {
                    case MaterialType.TEXT:         //�ظ�������Ϣ 
                        commonService.ResponseTextMessage(weixinId, openId, Text, httpContext);
                        break;
                    case MaterialType.IMAGE_TEXT:   //�ظ�ͼ����Ϣ 
                        //ReplyNews(materialId);
                        ReplyNewsJermyn(ReplyId, keywordType, 1, openId, weixinId, loggingSessionInfo, httpContext);
                        break;
                    default:
                        break;
                }
            }
        }

        public void ReplyNewsJermyn(string objectId, int KeywordType, int ObjectDataFrom, string openId,
            string weixinId, LoggingSessionInfo loggingSessionInfo, HttpContext httpContext)
        {
            var dsMaterialText = new WMaterialTextDAO(loggingSessionInfo).GetMaterialTextByIDJermyn(objectId, ObjectDataFrom);

            if (dsMaterialText != null && dsMaterialText.Tables.Count > 0 && dsMaterialText.Tables[0].Rows.Count > 0)
            {
                var newsList = new List<WMaterialTextEntity>();

                foreach (DataRow dr in dsMaterialText.Tables[0].Rows)
                {
                    var url = dr["OriginalUrl"].ToString();

                    #region ����ҵ�� ���Ӻ������openId��userId

                    if (url.IndexOf("ParAll=") != -1)
                    {
                        var vipId = string.Empty;

                        VipBLL vipService = new VipBLL(loggingSessionInfo);
                        var vipEntity = vipService.QueryByEntity(new VipEntity { WeiXinUserId = openId, WeiXin = weixinId }, null);
                        if (vipEntity != null && vipEntity.Length > 0)
                        {
                            vipId = vipEntity.FirstOrDefault().VIPID;
                        }

                        url += "&openId=" + openId + "&userId=" + vipId;
                    }

                    #endregion

                    newsList.Add(new WMaterialTextEntity()
                    {
                        Title = dr["Title"].ToString(),
                        Text = dr["Author"].ToString(),
                        CoverImageUrl = dr["CoverImageUrl"].ToString(),
                        OriginalUrl = url
                    });
                }
                var commonService = new CommonBLL();
                commonService.ResponseNewsMessage(weixinId, openId, newsList, httpContext);
            }
        }

        #region ����
        public SuccessResponse<IAPIResponseData> EventSave(EventSaveRP eventSaveRP)
        {
            MobileModuleBLL mobileModuleBLL = new MobileModuleBLL(CurrentUserInfo);
            MobileModuleObjectMappingBLL mobileModuleObjectMappingBLL = new MobileModuleObjectMappingBLL(CurrentUserInfo);

            var rd = new EmptyResponseData();
            SuccessResponse<IAPIResponseData> successResponse = new SuccessResponse<IAPIResponseData>(rd);

            if (!string.IsNullOrEmpty(eventSaveRP.EventEntity.IsDelete) && eventSaveRP.EventEntity.IsDelete == "1")
                _currentDAO.Delete(eventSaveRP.EventEntity.EventID, null);
            else
            {
                SqlTransaction tran = _currentDAO.GetTran();
                using (tran.Connection)
                {
                    #region Prepare LEventsEntity
                    LEventsEntity lEventsEntity = new LEventsEntity();
                    lEventsEntity.EventID = eventSaveRP.EventEntity.EventID;
                    lEventsEntity.Title = eventSaveRP.EventEntity.EventTitle;
                    lEventsEntity.EventLevel = 1;
                    lEventsEntity.BeginTime = eventSaveRP.EventEntity.BeginTime;
                    lEventsEntity.EndTime = eventSaveRP.EventEntity.EndTime;
                    lEventsEntity.Address = eventSaveRP.EventEntity.Address;
                    lEventsEntity.Description = eventSaveRP.EventEntity.Description;
                    lEventsEntity.Longitude = eventSaveRP.EventEntity.Longitude;
                    lEventsEntity.Latitude = eventSaveRP.EventEntity.Latitude;
                    //10=δ��ʼ 20=������ 30=��ͣ 40=����
                    if (DateTime.Parse(eventSaveRP.EventEntity.BeginTime) <= DateTime.Now)
                        lEventsEntity.EventStatus = 20;
                    else
                        lEventsEntity.EventStatus = 10;

                    if (DateTime.Parse(eventSaveRP.EventEntity.EndTime) <= DateTime.Now)
                        lEventsEntity.EventStatus = 40;

                    lEventsEntity.IsDelete = 0;
                    lEventsEntity.CustomerId = CurrentUserInfo.ClientID;
                    lEventsEntity.Organizer = eventSaveRP.EventEntity.Sponsor;
                    lEventsEntity.EventTypeID = eventSaveRP.EventEntity.EventTypeID;
                    //1	Online	���ϻ    2	Offline	���»
                    lEventsEntity.EventGenreId = 2;
                    if (!string.IsNullOrEmpty(eventSaveRP.EventEntity.PersonLimit))
                        lEventsEntity.CanSignUpCount = int.Parse(eventSaveRP.EventEntity.PersonLimit);

                    if (!string.IsNullOrEmpty(eventSaveRP.EventEntity.BeginPersonCount))
                        lEventsEntity.BeginPersonCount = int.Parse(eventSaveRP.EventEntity.BeginPersonCount);

                    if (!string.IsNullOrEmpty(eventSaveRP.EventEntity.EventFee))
                        lEventsEntity.EventFee = decimal.Parse(eventSaveRP.EventEntity.EventFee);

                    if (!string.IsNullOrEmpty(eventSaveRP.EventEntity.IsSignUpList))
                        lEventsEntity.IsSignUpList = int.Parse(eventSaveRP.EventEntity.IsSignUpList);
                    #endregion

                    #region new
                    if (string.IsNullOrEmpty(lEventsEntity.EventID))
                    {
                        //create form
                        MobileModuleEntity mobileModuleEntity = new MobileModuleEntity();
                        mobileModuleEntity.MobileModuleID = Guid.NewGuid();
                        mobileModuleEntity.ModuleCode = eventSaveRP.EventEntity.EventID;
                        mobileModuleEntity.ModuleName = eventSaveRP.EventEntity.EventTitle;
                        mobileModuleEntity.CustomerID = CurrentUserInfo.ClientID;
                        mobileModuleEntity.CreateTime = DateTime.Now;
                        mobileModuleEntity.CreateBy = CurrentUserInfo.UserID;
                        mobileModuleEntity.IsDelete = 0;

                        mobileModuleBLL.Create(mobileModuleEntity, tran);

                        //save form
                        if (eventSaveRP.EventEntity.FieldList != null && eventSaveRP.EventEntity.FieldList.Length > 0)
                            mobileModuleBLL.DynamicFormSave(eventSaveRP.EventEntity.FieldList, mobileModuleEntity.MobileModuleID.ToString(), "LEventSignUp", tran);

                        //create event
                        lEventsEntity.EventID = Guid.NewGuid().ToString();
                        lEventsEntity.CreateTime = DateTime.Now;
                        lEventsEntity.CreateBy = CurrentUserInfo.UserID;
                        _currentDAO.Create(lEventsEntity, tran);

                        //save event&form mapping
                        MobileModuleObjectMappingEntity mobileModuleObjectMappingEntity = new MobileModuleObjectMappingEntity();
                        mobileModuleObjectMappingEntity.MobileModuleID = mobileModuleEntity.MobileModuleID;
                        mobileModuleObjectMappingEntity.ObjectID = lEventsEntity.EventID;
                        mobileModuleObjectMappingEntity.ObjectType = 1;
                        mobileModuleObjectMappingEntity.CustomerID = CurrentUserInfo.ClientID;
                        mobileModuleObjectMappingEntity.CreateBy = CurrentUserInfo.UserID;
                        mobileModuleObjectMappingEntity.CreateTime = DateTime.Now;
                        mobileModuleObjectMappingEntity.IsDelete = 0;

                        mobileModuleObjectMappingBLL.Create(mobileModuleObjectMappingEntity, tran);
                    }
                    #endregion

                    #region update
                    else
                    {
                        lEventsEntity.LastUpdateBy = CurrentUserInfo.UserID;
                        lEventsEntity.LastUpdateTime = DateTime.Now;
                        _currentDAO.Update(lEventsEntity, false, tran);

                        var mobileModuleObjectMappingEntity = mobileModuleObjectMappingBLL.Query(new IWhereCondition[] { 
                        new EqualsCondition() { FieldName = "ObjectID", Value = lEventsEntity.EventID }
                    }, null);

                        if (mobileModuleObjectMappingEntity.Length > 0)
                        {
                            MobileModuleEntity mobileModuleEntity = mobileModuleBLL.GetByID(mobileModuleObjectMappingEntity[0].MobileModuleID);
                            if (mobileModuleEntity != null)
                            {
                                //save form
                                if (eventSaveRP.EventEntity.FieldList != null && eventSaveRP.EventEntity.FieldList.Length > 0)
                                    mobileModuleBLL.DynamicFormSave(eventSaveRP.EventEntity.FieldList, mobileModuleEntity.MobileModuleID.ToString(), "LEventSignUp", tran);
                            }
                        }
                    }
                    #endregion

                    tran.Commit();
                }
            }

            return successResponse;
        }
        #endregion

        #region ��ȡ�״̬�б�
        public SuccessResponse<IAPIResponseData> EventStatusList()
        {
            OptionsBLL optionsBLL = new OptionsBLL(CurrentUserInfo);
            var rd = new JIT.CPOS.BS.BLL.OptionsBLL.OptionListRD();

            var optionDataSet = optionsBLL.GetOptionByName("EventStatus", true);
            if (Utils.IsDataSetValid(optionDataSet))
            {
                rd.OptionList = (from o in optionDataSet.Tables[0].AsEnumerable()
                          select new OptionsEntity() {
                              OptionValue = int.Parse(o["OptionID"].ToString()),
                              OptionText = o["OptionText"].ToString()
                          }).ToArray();
            }

            var rsp = new SuccessResponse<IAPIResponseData>(rd);
            return rsp;
        }
        #endregion

        #region ��ȡ��б�
        public SuccessResponse<IAPIResponseData> EventList(EventListRP eventListRP)
        {
            var rd = new JIT.CPOS.BS.BLL.LEventsBLL.EventListRD();
            var rsp = new SuccessResponse<IAPIResponseData>(rd);
            DataSet dataSet = this._currentDAO.EventList(eventListRP.BeginTime, eventListRP.EndTime, eventListRP.EventTypeID, eventListRP.Sponsor, eventListRP.EventStatus, eventListRP.EventTitle, eventListRP.PageSize, eventListRP.PageIndex);
            
            Loggers.Debug(new DebugLogInfo() { Message = dataSet.ToJSON() });
            
            if(Utils.IsDataSetValid(dataSet))
            {
                rd.EventList = (from e in dataSet.Tables[0].AsEnumerable()
                                select new EventEntity() {
                                    EventID = e["EventID"].ToString(),
                                    EventTitle = e["Title"].ToString(),
                                    Sponsor = e["Sponsor"].ToString(),
                                    BeginTime = e["EventBeginTime"].ToString(),
                                    EventStatus = e["EventStatusText"].ToString(),
                                    QRCodeImageUrl = e["PosterImageUrl"].ToString(),
                                    ReaderCount = "0",
                                    SignupCount = e["SignupCount"].ToString(),
                                    CheckInCount = e["CheckInCount"].ToString()
                                }).ToArray();
                rd.TotalPage = int.Parse(dataSet.Tables[1].Rows[0]["PageCount"].ToString());
                rd.TotalCount = int.Parse(dataSet.Tables[1].Rows[0]["RecordCount"].ToString());

                rsp = new SuccessResponse<IAPIResponseData>(rd);
            }
            return rsp;
        }
        #endregion

        #region ��ȡ�����
        public SuccessResponse<IAPIResponseData> EventGet(EventGetRP eventGetRP)
        {
            var rd = new JIT.CPOS.BS.BLL.LEventsBLL.EventRD();
            rd.Event = new EventEntity();
            var rsp = new SuccessResponse<IAPIResponseData>(rd);

            LEventsEntity lEventsEntity = new LEventsEntity();
            lEventsEntity = this._currentDAO.GetByID(eventGetRP.EventID);

            if (!string.IsNullOrEmpty(lEventsEntity.EventID))
            {
                rd.Event.EventID = eventGetRP.EventID;
                rd.Event.EventTitle = lEventsEntity.Title;    //�����
                rd.Event.Sponsor = lEventsEntity.Organizer;   //���췽��OptionValue��
                rd.Event.BeginTime = lEventsEntity.BeginTime; //���ʼʱ��
                rd.Event.EndTime = lEventsEntity.EndTime;     //�����ʱ��
                rd.Event.EventTypeID = lEventsEntity.EventTypeID;   //�����ID
                rd.Event.Description = lEventsEntity.Description;   //�����
                rd.Event.Address = lEventsEntity.Address;           //��ص�
                rd.Event.PersonLimit = lEventsEntity.CanSignUpCount.ToString();     //��������
                rd.Event.BeginPersonCount = lEventsEntity.BeginPersonCount.ToString(); //��ʼ����
                rd.Event.EventFee = lEventsEntity.EventFee.ToString();         //�����
                rd.Event.IsSignUpList = lEventsEntity.IsSignUpList.ToString(); //������ʾ������Ա��0��1��
                rd.Event.EventStatus = lEventsEntity.EventStatus.ToString();
                rd.Event.CustomerId = lEventsEntity.CustomerId.ToString();//�ͻ�id
                
                LEventSignUpBLL lEventSignUpBLL = new LEventSignUpBLL(CurrentUserInfo);
                DataSet formDataSet = lEventSignUpBLL.DynamicFormLoadByEventID(eventGetRP.EventID);

                if (Utils.IsDataSetValid(formDataSet))
                {
                    //remove form name table
                    formDataSet.Tables.RemoveAt(0);
                    rd.Event.FieldList = GetFieldList(formDataSet);
                    rsp = new SuccessResponse<IAPIResponseData>(rd);
                }
            }
            else
                rsp.Message = "������ڣ�";

            return rsp;
        }
        #endregion
        /// <summary>
        /// ��б�
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="strTitle"></param>
        /// <param name="strDrawMethodName"></param>
        /// <param name="strBeginTime"></param>
        /// <param name="strEndTime"></param>
        /// <returns></returns>
        public DataSet GetEventList(int pageIndex, int pageSize, string strTitle, string strDrawMethodName, string strBeginTime, string strEndTime)
        {
            return _currentDAO.GetEventList(pageIndex, pageSize, strTitle, strDrawMethodName, strBeginTime, strEndTime);
        }


        /// <summary>
        /// ɾ���(ͨ���洢���̲���)
        /// </summary>
        /// <param name="pEntity"></param>
        public void DeleteByProc(string strEventId)
        {
            this._currentDAO.DeleteByProc(strEventId);
        }
        #region � ����ͣ��
        public void UpdateEventStatus(string strEventId, int intEventStatus)
        {
            this._currentDAO.UpdateEventStatus(strEventId, intEventStatus);
        }
        #endregion
        /// <summary>
        /// ���»�Ƿ����
        /// </summary>
        /// <param name="strEventId"></param>
        public void UpdateEventIsShare(string strEventId)
        {
            this._currentDAO.UpdateEventIsShare(strEventId);
        }


        #region ��ȡ�����еĻ
        public DataSet GetWorkingEventList()
        {
           return this._currentDAO.GetWorkingEventList();
        }

        #endregion
   
        public JIT.CPOS.BS.BLL.MobileModuleBLL.Field[] GetFieldList(DataSet formDataSet)
        {
            return (from f in formDataSet.Tables[0].AsEnumerable()
                    select new JIT.CPOS.BS.BLL.MobileModuleBLL.Field()
                    {
                        PublicControlID = f["ClientBussinessDefinedID"].ToString(),
                        FormControlID = f["MobileBussinessDefinedID"].ToString(),
                        ColumnDesc = f["ColumnDesc"].ToString(),
                        ControlType = int.Parse(f["ControlType"].ToString()),
                        DisplayType = int.Parse(f["DisplayType"].ToString()),
                        IsMustDo = int.Parse(f["IsMustDo"].ToString()),
                        EditOrder = int.Parse(f["EditOrder"].ToString()),
                        IsUsed = int.Parse(f["IsUsed"].ToString()),
                        Hierarchy = f["Hierarchy"].ToString()
                    }).ToArray();
        }

        #region �����Զ�����ά��
        public SuccessResponse<DTO.Base.IAPIResponseData> EventQRCodeGenerate(EventGetRP eventGetRP)
        {
            EventQRCodeGenerateRD rd = new EventQRCodeGenerateRD();

            Random random = new Random();

            string htmlDomain = ConfigurationManager.AppSettings["AuthUrl"];
            string managementDomain = ConfigurationManager.AppSettings["DownloadImageUrl"];
            string sourcePath = HttpContext.Current.Server.MapPath("/QRCodeImage/qrcode.jpg");
            string targetPath = HttpContext.Current.Server.MapPath("/QRCodeImage/");

            string url = htmlDomain + "/" + "/HtmlApps/html/public/xiehuibao/signin.html?rootPage=true&customerId={0}&eventID={1}&rid={2}&ver={3}";
            url = string.Format(url, CurrentUserInfo.ClientID, eventGetRP.EventID, random.Next(), random.Next());
            rd.ImageURL = Utils.GenerateQRCode(url, managementDomain, sourcePath, targetPath);

            _currentDAO.Update(new LEventsEntity() { EventID = eventGetRP.EventID, PosterImageUrl = rd.ImageURL }, false);

            var rsp = new SuccessResponse<IAPIResponseData>(rd);
            return rsp;
        }
        #endregion

        public SuccessResponse<IAPIResponseData> EventDeleteBatch(EventGetRP eventGetRP)
        {
            var rd = new EmptyResponseData();
            SuccessResponse<IAPIResponseData> successResponse = new SuccessResponse<IAPIResponseData>(rd);

            string[] eventIDs = eventGetRP.EventID.Split(',');
            _currentDAO.Delete(eventIDs);

            return successResponse;
        }

        #region RP
        public class EventSaveRP : IAPIRequestParameter
        {
            public EventEntity EventEntity{get;set;}

            public void Validate()
            {
                if (!string.IsNullOrEmpty(EventEntity.BeginTime))
                {
                    try
                    {
                        DateTime.Parse(EventEntity.BeginTime);
                    }
                    catch (Exception)
                    {
                        throw new APIException(201, "���ʼʱ���ʽ����ȷ��");
                    }
                }

                if (!string.IsNullOrEmpty(EventEntity.EndTime))
                {
                    try
                    {
                        DateTime.Parse(EventEntity.EndTime);
                    }
                    catch (Exception)
                    {
                        throw new APIException(202, "�����ʱ���ʽ����ȷ��");
                    }
                }

                if (EventEntity.PersonLimit != "")
                {
                    try
                    {
                        int.Parse(EventEntity.PersonLimit);
                    }
                    catch (Exception)
                    {
                        throw new APIException(203, "�������Ʋ���ȷ��");
                    }
                }

                if (EventEntity.BeginPersonCount != "")
                {
                    try
                    {
                        int.Parse(EventEntity.BeginPersonCount);
                    }
                    catch (Exception)
                    {
                        throw new APIException(204, "��ʼ��������ȷ��");
                    }
                }

                if (EventEntity.EventFee != "")
                {
                    try
                    {
                        decimal.Parse(EventEntity.EventFee);
                    }
                    catch (Exception)
                    {
                        throw new APIException(205, "����ò���ȷ��");
                    }
                }
            }
        }

        public class EventListRP : IAPIRequestParameter
        {
            public string EventTitle { get; set; }
            public string Sponsor { get; set; }
            public string BeginTime { get; set; }
            public string EndTime { get; set; }
            public string EventTypeID { get; set; }
            public string EventStatus { get; set; }
            public string PageSize { get; set; }
            public string PageIndex { get; set; }

            public void Validate()
            {
                if (!string.IsNullOrEmpty(BeginTime))
                {
                    try
                    {
                        DateTime.Parse(BeginTime);
                    }
                    catch (Exception)
                    {
                        throw new APIException(201, "���ʼʱ���ʽ����ȷ��");
                    }
                }

                if (!string.IsNullOrEmpty(EndTime))
                {
                    try
                    {
                        DateTime.Parse(EndTime);
                    }
                    catch (Exception)
                    {
                        throw new APIException(202, "�����ʱ���ʽ����ȷ��");
                    }
                }

                if (PageIndex != "")
                {
                    try
                    {
                        int.Parse(PageIndex);
                    }
                    catch (Exception)
                    {
                        throw new APIException(203, "ҳ�벻��ȷ��");
                    }
                }
                else
                    PageIndex = "0";

                if (PageSize != "")
                {
                    try
                    {
                        int.Parse(PageSize);
                    }
                    catch (Exception)
                    {
                        throw new APIException(204, "ÿҳ��ʾ������ȷ��");
                    }
                }
                else
                    PageIndex = "15";
                  
            }
        }

        public class EventGetRP : IAPIRequestParameter
        {
            public string EventID { get; set; }
            public string CustomerId { get; set; }
            public void Validate()
            { 
                if(string.IsNullOrEmpty(EventID))
                    throw new APIException(201, "�ID����Ϊ�գ�");
            }
        }


        #endregion

        #region RD
        public class EventListRD : IAPIResponseData
        {
            public EventEntity[] EventList { get; set; }
            public int TotalCount { get; set; }
            public int TotalPage { get; set; }
        }

        public class EventRD : IAPIResponseData
        {
            public EventEntity Event {get;set;}
        }

        public class EventEntity
        {
            public string EventID { get; set; }
            public string EventTitle { get; set; }
            public string Sponsor { get; set; }
            public string BeginTime { get; set; }
            public string EndTime { get; set; }
            public string EventTypeID { get; set; }
            public string Description { get; set; }
            public string Address { get; set; }
            public string PersonLimit { get; set; }
            public string BeginPersonCount { get; set; }
            public string EventFee { get; set; }
            public string IsSignUpList { get; set; }
            public string Longitude { get; set; }
            public string Latitude { get; set; }
            public string EventStatus { get; set; }
            public string ReaderCount { get; set; }
            public string SignupCount { get; set; }
            public string CheckInCount { get; set; }
            public string QRCodeImageUrl { get; set; }
            public string IsDelete { get; set; }
            public string CustomerId { get; set; }

            public JIT.CPOS.BS.BLL.MobileModuleBLL.Field[] FieldList { get; set; }
        }

        public class EventQRCodeGenerateRD : IAPIResponseData
        {
            public string ImageURL { get; set; }
        }
        #endregion
    }
}