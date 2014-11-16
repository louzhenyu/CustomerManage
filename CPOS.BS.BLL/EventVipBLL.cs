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
    /// 业务处理：  
    /// </summary>
    public partial class EventVipBLL
    {
        #region 消息发送,发送微信消息
        /// <summary>
        /// 发送微信消息
        /// </summary>
        /// <param name="VipId">用户标识</param>
        /// <param name="EventId">活动标识</param>
        /// <param name="strError">错误提示</param>
        /// <returns></returns>
        public bool SetEventVipSeatPush(string VipId, string EventId, out string strError)
        {
            strError = "成功.";
            #region //1.判断用户是否关注过活动
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

            #region //2.判断用户是否注册
            VipBLL vipServer = new VipBLL(this.CurrentUserInfo);
            VipEntity vipInfo = new VipEntity();
            vipInfo = vipServer.GetByID(VipId);
            if (vipInfo == null || vipInfo.VIPID.Equals(""))
            {
                strError = "用户不存在";
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
                    strError = "没有合适的员工.";
                    return false;
                }
                else
                {
                    eventVipInfo = eventVipList[0];
                }
                #endregion

                LEventsEntity lEventsEntity = new LEventsEntity();
                lEventsEntity = new LEventsBLL(this.CurrentUserInfo).GetByID(EventId);

                //string message = "尊贵的" + eventVipInfo.VipName + "先生/女士:\r\n 诚挚地欢迎您参加复星集团2014年全球工作会议，本次会议将于8：30正式开始。请您至5楼静安大宴会厅" + eventVipInfo.Seat + "区域就坐,或参见胸卡背面提示.";

                string message = new WMaterialWritingBLL(this.CurrentUserInfo).GetWMaterialWritingByModelId(EventId).Content;
                message = ReplaceTemplate(message, eventVipList[0], vipInfo, lEventsEntity);

                //组织消息            
                string code = JIT.CPOS.BS.BLL.WX.CommonBLL.SendWeixinMessage(message, VipId, this.CurrentUserInfo, vipInfo);
                switch (code)
                {
                    case "103":
                        strError = "未查询到匹配的公众账号信息";
                        break;
                    case "203":
                        strError = "发送失败";
                        break;
                    default:
                        break;
                }
            }
            return true;
        }
        #endregion

        #region 替换动态关键字
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

        #region 搜索会员
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
        /// <param name="Page">分页页码。从0开始</param>
        /// <param name="PageSize">每页的数量。未指定时默认为15</param>
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

        #region 盘点用户是否已经关联
        /// <summary>
        /// 复星注册
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