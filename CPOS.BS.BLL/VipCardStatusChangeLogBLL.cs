/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/6/20 11:22:29
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
    public partial class VipCardStatusChangeLogBLL
    {
        #region 列表获取
        /// <summary>
        /// 列表获取
        /// </summary>
        /// <param name="Page">分页页码。从0开始</param>
        /// <param name="PageSize">每页的数量。未指定时默认为15</param>
        /// <returns></returns>
        public List<VipCardStatusChangeLogEntity> GetList(VipCardStatusChangeLogEntity entity, int Page, int PageSize)
        {
            if (PageSize <= 0) PageSize = 15;

            List<VipCardStatusChangeLogEntity> list = new List<VipCardStatusChangeLogEntity>();

            DataSet ds = _currentDAO.GetList(entity, Page, PageSize);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                list = DataTableToObject.ConvertToList<VipCardStatusChangeLogEntity>(ds.Tables[0]);
            }

            return list;
        }
        /// <summary>
        /// 列表数量获取
        /// </summary>
        public int GetListCount(VipCardStatusChangeLogEntity entity)
        {
            return _currentDAO.GetListCount(entity);
        }
        #endregion

        #region 状态变更
        /// <summary>
        /// 状态变更
        /// </summary>
        /// <param name="VipCardID">会员卡标识</param>
        /// <param name="StatusIDNow">当前状态标识</param>
        /// <param name="StatusIDNext">更改之后的状态标识</param>
        /// <param name="UnitID">门店标识</param>
        /// <param name="strError"></param>
        /// <returns></returns>
        public bool SetVipCardStatusChange(string VipCardID
                                        , int StatusIDNow
                                        , int StatusIDNext
                                        , string UnitID
                                        , out string strError
                                        )
        {
            try
            {
                #region
                if (VipCardID == null || VipCardID.Trim().Equals(""))
                {
                    strError = "会员卡号标识不能为空.";
                    return false;
                }
                if (StatusIDNow == 0 || StatusIDNow.ToString().Trim().Equals(""))
                {
                    strError = "当前状态标识不能为空.";
                    return false;
                }
                if (StatusIDNext == 0 || StatusIDNext.ToString().Trim().Equals(""))
                {
                    strError = "更改之后的状态标识不能为空.";
                    return false;
                }
                #endregion
                #region 1.获取修改会员卡状态信息
                VipCardEntity vipCardInfo = new VipCardEntity();
                vipCardInfo.VipCardID = VipCardID;
                vipCardInfo.VipCardStatusId = StatusIDNext;
                vipCardInfo.LastUpdateBy = this.CurrentUserInfo.UserID;
                vipCardInfo.LastUpdateTime = System.DateTime.Now;
                #endregion
                #region 2.设置状态变更记录
                VipCardStatusChangeLogEntity statusChangeInfo = new VipCardStatusChangeLogEntity();
                statusChangeInfo.LogID = JIT.CPOS.BS.BLL.BaseService.NewGuidPub();
                statusChangeInfo.OldStatusID = StatusIDNow;
                statusChangeInfo.VipCardStatusID = StatusIDNext;
                statusChangeInfo.VipCardID = VipCardID;
                statusChangeInfo.CreateBy = CurrentUserInfo.UserID;
                statusChangeInfo.LastUpdateBy = this.CurrentUserInfo.UserID;
                statusChangeInfo.UnitID = UnitID;
                #endregion
                return _currentDAO.SetVipCardStatusChange(vipCardInfo, statusChangeInfo, out strError);
            }
            catch (Exception ex) {
                throw (ex);
            }
        }
        #endregion

        #region 批量处理变更
        /// <summary>
        /// 批量处理变更
        /// </summary>
        /// <param name="vipCardIds"></param>
        /// <param name="StatusIDNext"></param>
        /// <param name="strError"></param>
        /// <returns></returns>
        public bool SetVipCardStatusChangeBatch(string vipCardIds, int StatusIDNext, out string strError)
        {
            #region
            if (vipCardIds == null || vipCardIds.Trim().Equals(""))
            {
                strError = "会员卡号标识不能为空.";
                return false;
            }
            if (StatusIDNext == 0 || StatusIDNext.ToString().Trim().Equals(""))
            {
                strError = "更改之后的状态标识不能为空.";
                return false;
            }
            #endregion
            bool bReturn = true;
            string[] vipCardIdArr = vipCardIds.Split(',');
            VipCardBLL vipCardServer = new VipCardBLL(this.CurrentUserInfo);
            foreach (string id in vipCardIdArr)
            {
                VipCardEntity vipCardInfo = new VipCardEntity();
                vipCardInfo = vipCardServer.GetByID(id);
                bReturn = SetVipCardStatusChange(id
                                ,Convert.ToInt32(vipCardInfo.VipCardStatusId)
                                , StatusIDNext
                                , this.CurrentUserInfo.CurrentUserRole.UnitId
                                , out strError);
                if (!bReturn) break;
            }
            strError = "ok";
            return bReturn;
        }
        #endregion

    }
}