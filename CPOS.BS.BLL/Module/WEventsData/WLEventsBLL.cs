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

namespace JIT.CPOS.BS.BLL
{
    /// <summary>
    /// ҵ����  
    /// </summary>
    public partial class WLEventsBLL
    {
        private LoggingSessionInfo CurrentUserInfo;
        private WLEventsDAO _currentDAO;
        #region ���캯��
        /// <summary>
        /// ���캯�� 
        /// </summary>
        public WLEventsBLL(LoggingSessionInfo pUserInfo)
        {
            this.CurrentUserInfo = pUserInfo;
            this._currentDAO = new WLEventsDAO(pUserInfo);
        }
        #endregion

        #region ICRUDable ��Ա
        /// <summary>
        /// ����һ����ʵ��
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        public void Create(LEventsEntity pEntity)
        {
            _currentDAO.Create(pEntity);
        }


        /// <summary>
        /// �������ڴ���һ����ʵ��
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        /// <param name="pTran">����ʵ��,��Ϊnull,���Ϊnull,��ʹ������������</param>
        public void Create(LEventsEntity pEntity, IDbTransaction pTran)
        {
            _currentDAO.Create(pEntity, pTran);
        }

        /// <summary>
        /// ���ݱ�ʶ����ȡʵ��
        /// </summary>
        /// <param name="pID">��ʶ����ֵ</param>
        public LEventsEntity GetByID(object pID)
        {
            return _currentDAO.GetByID(pID);
        }

        /// <summary>
        /// ��ȡ����ʵ��
        /// </summary>
        /// <returns></returns>
        public LEventsEntity[] GetAll()
        {
            return _currentDAO.GetAll();
        }

        /// <summary>
        /// ����
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        /// <param name="pTran">����ʵ��,��Ϊnull,���Ϊnull,��ʹ������������</param>        
        public void Update(LEventsEntity pEntity, IDbTransaction pTran)
        {
            Update(pEntity, true, pTran);
        }
        public void Update(LEventsEntity pEntity, bool pIsUpdateNullField, IDbTransaction pTran)
        {
            _currentDAO.Update(pEntity, pIsUpdateNullField, pTran);
        }


        /// <summary>
        /// ����
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        public void Update(LEventsEntity pEntity)
        {
            Update(pEntity, true);
        }
        public void Update(LEventsEntity pEntity, bool pIsUpdateNullField)
        {
            _currentDAO.Update(pEntity, pIsUpdateNullField);
        }

        /// <summary>
        /// ɾ��
        /// </summary>
        /// <param name="pEntity"></param>
        public void Delete(LEventsEntity pEntity)
        {
            _currentDAO.Delete(pEntity);
        }

        /// <summary>
        /// ɾ��
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        /// <param name="pTran">����ʵ��,��Ϊnull,���Ϊnull,��ʹ������������</param>
        public void Delete(LEventsEntity pEntity, IDbTransaction pTran)
        {
            _currentDAO.Delete(pEntity, pTran);
        }

        /// <summary>
        /// ɾ��
        /// </summary>
        /// <param name="pID">��ʶ����ֵ</param>
        /// <param name="pTran">����ʵ��,��Ϊnull,���Ϊnull,��ʹ������������</param>
        public void Delete(object pID, IDbTransaction pTran)
        {
            _currentDAO.Delete(pID, pTran);
        }

        /// <summary>
        /// ����ɾ��
        /// </summary>
        /// <param name="pEntities">ʵ��ʵ������</param>
        /// <param name="pTran">����ʵ��,��Ϊnull,���Ϊnull,��ʹ������������</param>
        public void Delete(LEventsEntity[] pEntities, IDbTransaction pTran)
        {
            _currentDAO.Delete(pEntities, pTran);
        }

        /// <summary>
        /// ����ɾ��
        /// </summary>
        /// <param name="pEntities">ʵ��ʵ������</param>
        public void Delete(LEventsEntity[] pEntities)
        {
            _currentDAO.Delete(pEntities);
        }

        /// <summary>
        /// ����ɾ��
        /// </summary>
        /// <param name="pIDs">��ʶ��ֵ����</param>
        public void Delete(object[] pIDs)
        {
            _currentDAO.Delete(pIDs);
        }

        /// <summary>
        /// ����ɾ��
        /// </summary>
        /// <param name="pIDs">��ʶ��ֵ����</param>
        /// <param name="pTran">����ʵ��,��Ϊnull,���Ϊnull,��ʹ������������</param>
        public void Delete(object[] pIDs, IDbTransaction pTran)
        {
            _currentDAO.Delete(pIDs, pTran);
        }
        #endregion

        #region IQueryable ��Ա
        /// <summary>
        /// ִ�в�ѯ
        /// </summary>
        /// <param name="pWhereConditions">ɸѡ����</param>
        /// <param name="pOrderBys">����</param>
        /// <returns></returns>
        public LEventsEntity[] Query(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys)
        {
            return _currentDAO.Query(pWhereConditions, pOrderBys);
        }

        /// <summary>
        /// ִ�з�ҳ��ѯ
        /// </summary>
        /// <param name="pWhereConditions">ɸѡ����</param>
        /// <param name="pOrderBys">����</param>
        /// <param name="pPageSize">ÿҳ�ļ�¼��</param>
        /// <param name="pCurrentPageIndex">��0��ʼ�ĵ�ǰҳ��</param>
        /// <returns></returns>
        public PagedQueryResult<LEventsEntity> PagedQuery(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
        {
            return _currentDAO.PagedQuery(pWhereConditions, pOrderBys, pPageSize, pCurrentPageIndex);
        }

        /// <summary>
        /// ����ʵ��������ѯʵ��
        /// </summary>
        /// <param name="pQueryEntity">��ʵ����ʽ����Ĳ���</param>
        /// <param name="pOrderBys">�������</param>
        /// <returns>����������ʵ�弯</returns>
        public LEventsEntity[] QueryByEntity(LEventsEntity pQueryEntity, OrderBy[] pOrderBys)
        {
            return _currentDAO.QueryByEntity(pQueryEntity, pOrderBys);
        }

        /// <summary>
        /// ��ҳ����ʵ��������ѯʵ��
        /// </summary>
        /// <param name="pQueryEntity">��ʵ����ʽ����Ĳ���</param>
        /// <param name="pOrderBys">�������</param>
        /// <returns>����������ʵ�弯</returns>
        public PagedQueryResult<LEventsEntity> PagedQueryByEntity(LEventsEntity pQueryEntity, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
        {
            return _currentDAO.PagedQueryByEntity(pQueryEntity, pOrderBys, pPageSize, pCurrentPageIndex);
        }

        #endregion

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
        public DataTable WEventGetWebEvents(LEventsEntity eventsEntity, int Page, int PageSize, out int pPageCount)
        {
            if (PageSize <= 0) PageSize = 15;
            DataSet ds = new DataSet();
            pPageCount = 0;
            ds = _currentDAO.WEventGetWebEvents(eventsEntity, Page, PageSize);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables.Count > 1 && ds.Tables[1].Rows.Count > 0)
                {
                    pPageCount = Convert.ToInt32(ds.Tables[1].Rows[0][0]);
                }
                return ds.Tables[0];
            }
            return null;
        }
         
   
        /// <summary>
        /// ��б�������ȡ
        /// </summary>
        public int WEventGetWebEventsCount(LEventsEntity eventsEntity)
        {
            return _currentDAO.WEventGetWebEventsCount(eventsEntity);
        }
        #endregion

        #region Web��б��ȡ
        /// <summary>
        /// ��б��ȡ
        /// </summary>
        public DataTable GetEventsVipData(string pEventID, int Page, int PageSize, out int pPageCount)
        {
            if (PageSize <= 0) PageSize = 15;
            DataSet ds = new DataSet();
            pPageCount = 0;
            ds = _currentDAO.GetEventsVipData(pEventID, Page, PageSize);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables.Count > 1 && ds.Tables[1].Rows.Count > 0)
                {
                    pPageCount = Convert.ToInt32(ds.Tables[1].Rows[0][0]);
                }
                return ds.Tables[0];
            }
            return null;
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
        public LEventsEntity getSchoolEventList(string eventId,string userId)
        {
            LEventsEntity eventInfo = new LEventsEntity();
            DataSet ds = _currentDAO.GetEventFXInfo(eventId,userId);
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

        #region ��ע�̶���ά�룬���ͻ��Ϣ Jermyn20131209
        public bool SetEventWXPush(LEventsEntity eventInfo, string WeiXin, string OpenId, string VipId, string msgUrl, out string strError, string AuthUrl, int iRad)
        {
            try
            {
                MarketSendLogBLL logServer = new MarketSendLogBLL(this.CurrentUserInfo);
                Random rad = new Random();
                if (eventInfo == null || eventInfo.ModelId == null || eventInfo.ModelId.Equals("") )
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
                            else {
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
                            logInfo1.TemplateContent = sendMessageInfo.ToJSON().ToString().Substring(1,1999);
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
            catch (Exception ex) {
                strError = ex.ToString();
                return false;
            }
        }
        #endregion
    }
}