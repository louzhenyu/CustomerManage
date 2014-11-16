/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/1/7 11:10:18
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
using System.Collections;
using System.Configuration;

namespace JIT.CPOS.BS.BLL
{
    /// <summary>
    /// 业务处理：  
    /// </summary>
    public partial class FStaffBLL
    {
        #region GetList
        /// <summary>
        /// GetList
        /// </summary>
        /// <param name="entity">entity</param>
        /// <param name="Page">分页页码。从0开始</param>
        /// <param name="PageSize">每页的数量。未指定时默认为15</param>
        /// <returns></returns>
        public IList<FStaffEntity> GetList(FStaffEntity entity, int Page, int PageSize)
        {
            if (PageSize <= 0) PageSize = 15;

            IList<FStaffEntity> list = new List<FStaffEntity>();
            DataSet ds = new DataSet();
            ds = _currentDAO.GetList(entity, Page, PageSize);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                list = DataTableToObject.ConvertToList<FStaffEntity>(ds.Tables[0]);
            }
            return list;
        }
        /// <summary>
        /// GetListCount
        /// </summary>
        /// <param name="entity">entity</param>
        public int GetListCount(FStaffEntity entity)
        {
            return _currentDAO.GetListCount(entity);
        }
        #endregion

        #region 消息发送,发送微信消息
        /// <summary>
        /// 发送微信消息
        /// </summary>
        /// <param name="VipId">用户标识</param>
        /// <param name="EventId">活动标识</param>
        /// <param name="strError">错误提示</param>
        /// <returns></returns>
        public bool SetStaffSeatsPush(string VipId,string EventId,out string strError)
        {
            strError = "成功.";
            #region //1.判断用户是否关注过活动
            WEventUserMappingBLL eventServer = new WEventUserMappingBLL(this.CurrentUserInfo);
            var eventList = eventServer.QueryByEntity(new WEventUserMappingEntity
            {
                UserID = VipId
                ,IsDelete = 0
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
                FStaffEntity staffInfo = new FStaffEntity();
                var staffList = _currentDAO.QueryByEntity(new FStaffEntity
                {
                    Phone = vipInfo.Phone
                    ,
                    IsDelete = 0
                }, null);
                if (staffList == null || staffList.Length == 0)
                {
                    strError = "没有合适的员工.";
                    return false;
                }
                else
                {
                    staffInfo = staffList[0];
                }
                #endregion


                string message = "尊贵的" + staffInfo.StaffName + "先生/女士:\r\n 诚挚地欢迎您参加复星集团2014年全球工作会议，本次会议将于8：30正式开始。请您至5楼静安大宴会厅" + staffInfo.Seats + "区域就坐,或参见胸卡背面提示.";

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

            return _currentDAO.IsVipStaffMapping(VipId,Phone,OpenId);
        }
        #endregion

        #region 搜索员工
        public DataSet GetStaffJoinVip(string eventId, string staffName, string phone, string register, string sign, int pageNo, int pageSize)
        {
            return this._currentDAO.GetStaffJoinVip(eventId, staffName, phone, register, sign, pageNo, pageSize);
        }
        #endregion
    }
}