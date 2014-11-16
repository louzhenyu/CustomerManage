/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/2/20 11:45:33
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

namespace JIT.CPOS.BS.BLL
{
    /// <summary>
    /// ҵ����  
    /// </summary>
    public partial class EventVipBLL
    {
        #region ��Ϣ����,����΢����Ϣ
        /// <summary>
        /// ����΢����Ϣ
        /// </summary>
        /// <param name="VipId">�û���ʶ</param>
        /// <param name="EventId">���ʶ</param>
        /// <param name="strError">������ʾ</param>
        /// <returns></returns>
        public bool SetEventVipSeatPush(string VipId, string EventId, out string strError)
        {
            strError = "�ɹ�.";
            #region //1.�ж��û��Ƿ��ע���
            WEventUserMappingBLL eventServer = new WEventUserMappingBLL(this.CurrentUserInfo);
            var eventList = eventServer.QueryByEntity(new WEventUserMappingEntity
            {
                UserID = VipId
                ,
                IsDelete = 0
                ,
                EventID = EventId
            }, null);
            if (eventList == null || eventList.Length == 0)
            {
                #region
                WEventUserMappingEntity eventUserMappingInfo = new WEventUserMappingEntity();
                eventUserMappingInfo.Mapping = Common.Utils.NewGuid();
                eventUserMappingInfo.EventID = EventId;
                eventUserMappingInfo.UserID = VipId;
                eventServer.Create(eventUserMappingInfo);
                #endregion
            }
            #endregion

            #region //2.�ж��û��Ƿ�ע��
            VipBLL vipServer = new VipBLL(this.CurrentUserInfo);
            VipEntity vipInfo = new VipEntity();
            vipInfo = vipServer.GetByID(VipId);
            if (vipInfo == null || vipInfo.VIPID.Equals(""))
            {
                strError = "�û�������";
                return false;
            }
            #endregion
            else
            {
                #region
                EventVipEntity eventVipInfo = new EventVipEntity();
                var eventVipList = _currentDAO.QueryByEntity(new EventVipEntity
                {
                    Phone = vipInfo.Phone
                    ,
                    IsDelete = 0
                }, null);
                if (eventVipList == null || eventVipList.Length == 0)
                {
                    strError = "û�к��ʵ�Ա��.";
                    return false;
                }
                else
                {
                    eventVipInfo = eventVipList[0];
                }
                #endregion

                LEventsEntity lEventsEntity = new LEventsEntity();
                lEventsEntity = new LEventsBLL(this.CurrentUserInfo).GetByID(EventId);

                //string message = "����" + eventVipInfo.VipName + "����/Ůʿ:\r\n ��ֿ�ػ�ӭ���μӸ��Ǽ���2014��ȫ�������飬���λ��齫��8��30��ʽ��ʼ��������5¥�����������" + eventVipInfo.Seat + "�������,��μ��ؿ�������ʾ.";

                string message = new WMaterialWritingBLL(this.CurrentUserInfo).GetWMaterialWritingByModelId(EventId).Content;
                message = ReplaceTemplate(message, eventVipList[0], vipInfo, lEventsEntity);

                //��֯��Ϣ            
                string code = JIT.CPOS.BS.BLL.WX.CommonBLL.SendWeixinMessage(message, VipId, this.CurrentUserInfo, vipInfo);
                switch (code)
                {
                    case "103":
                        strError = "δ��ѯ��ƥ��Ĺ����˺���Ϣ";
                        break;
                    case "203":
                        strError = "����ʧ��";
                        break;
                    default:
                        break;
                }
            }
            return true;
        }
        #endregion

        #region �滻��̬�ؼ���
        public string ReplaceTemplate(string message, EventVipEntity eventVip, VipEntity vip, LEventsEntity eventEntity)
        {
            message = message.Replace("$VipName$", eventVip.VipName);
            message = message.Replace("$Region$", eventVip.Seat);
            message = message.Replace("$Seat$", eventVip.Seat);
            message = message.Replace("$Ver$", new Random().Next().ToString());
            message = message.Replace("$CustomerId$", eventVip.CustomerId);
            message = message.Replace("$UserId$", vip.VIPID);
            message = message.Replace("$OpenId$", vip.WeiXinUserId);
            message = message.Replace("$EventName$", eventEntity.Title);

            return message;
        }
        #endregion

        #region ������Ա
        public DataSet GetEventVipJoinVip(string eventId, string eventVipName, string phone, string register, string sign, int pageNo, int pageSize)
        {
            return this._currentDAO.GetEventVipJoinVip(eventId, eventVipName, phone, register, sign, pageNo, pageSize);
        }
        #endregion

        #region GetList
        /// <summary>
        /// GetList
        /// </summary>
        /// <param name="entity">entity</param>
        /// <param name="Page">��ҳҳ�롣��0��ʼ</param>
        /// <param name="PageSize">ÿҳ��������δָ��ʱĬ��Ϊ15</param>
        /// <returns></returns>
        public IList<EventVipEntity> GetList(EventVipEntity entity, int Page, int PageSize)
        {
            if (PageSize <= 0) PageSize = 15;

            IList<EventVipEntity> list = new List<EventVipEntity>();
            DataSet ds = new DataSet();
            ds = _currentDAO.GetList(entity, Page, PageSize);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                list = DataTableToObject.ConvertToList<EventVipEntity>(ds.Tables[0]);
            }
            return list;
        }
        /// <summary>
        /// GetListCount
        /// </summary>
        /// <param name="entity">entity</param>
        public int GetListCount(EventVipEntity entity)
        {
            return _currentDAO.GetListCount(entity);
        }
        #endregion

        #region �̵��û��Ƿ��Ѿ�����
        /// <summary>
        /// ����ע��
        /// </summary>
        /// <param name="VipId"></param>
        /// <param name="Phone"></param>
        /// <param name="OpenId"></param>
        /// <returns></returns>
        public int IsVipStaffMapping(string VipId, string Phone, string OpenId)
        {
            return _currentDAO.IsVipStaffMapping(VipId, Phone, OpenId);
        }
        #endregion
    }
}