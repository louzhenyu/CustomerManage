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
    /// ҵ����  
    /// </summary>
    public partial class FStaffBLL
    {
        #region GetList
        /// <summary>
        /// GetList
        /// </summary>
        /// <param name="entity">entity</param>
        /// <param name="Page">��ҳҳ�롣��0��ʼ</param>
        /// <param name="PageSize">ÿҳ��������δָ��ʱĬ��Ϊ15</param>
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

        #region ��Ϣ����,����΢����Ϣ
        /// <summary>
        /// ����΢����Ϣ
        /// </summary>
        /// <param name="VipId">�û���ʶ</param>
        /// <param name="EventId">���ʶ</param>
        /// <param name="strError">������ʾ</param>
        /// <returns></returns>
        public bool SetStaffSeatsPush(string VipId,string EventId,out string strError)
        {
            strError = "�ɹ�.";
            #region //1.�ж��û��Ƿ��ע���
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
                FStaffEntity staffInfo = new FStaffEntity();
                var staffList = _currentDAO.QueryByEntity(new FStaffEntity
                {
                    Phone = vipInfo.Phone
                    ,
                    IsDelete = 0
                }, null);
                if (staffList == null || staffList.Length == 0)
                {
                    strError = "û�к��ʵ�Ա��.";
                    return false;
                }
                else
                {
                    staffInfo = staffList[0];
                }
                #endregion


                string message = "����" + staffInfo.StaffName + "����/Ůʿ:\r\n ��ֿ�ػ�ӭ���μӸ��Ǽ���2014��ȫ�������飬���λ��齫��8��30��ʽ��ʼ��������5¥�����������" + staffInfo.Seats + "�������,��μ��ؿ�������ʾ.";

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

            return _currentDAO.IsVipStaffMapping(VipId,Phone,OpenId);
        }
        #endregion

        #region ����Ա��
        public DataSet GetStaffJoinVip(string eventId, string staffName, string phone, string register, string sign, int pageNo, int pageSize)
        {
            return this._currentDAO.GetStaffJoinVip(eventId, staffName, phone, register, sign, pageNo, pageSize);
        }
        #endregion
    }
}