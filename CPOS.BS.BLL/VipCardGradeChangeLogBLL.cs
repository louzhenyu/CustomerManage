/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/6/20 11:41:32
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
    public partial class VipCardGradeChangeLogBLL
    {
        #region 列表获取
        /// <summary>
        /// 列表获取
        /// </summary>
        /// <param name="Page">分页页码。从0开始</param>
        /// <param name="PageSize">每页的数量。未指定时默认为15</param>
        /// <returns></returns>
        public List<VipCardGradeChangeLogEntity> GetList(VipCardGradeChangeLogEntity entity, int Page, int PageSize)
        {
            if (PageSize <= 0) PageSize = 15;

            List<VipCardGradeChangeLogEntity> list = new List<VipCardGradeChangeLogEntity>();

            DataSet ds = _currentDAO.GetList(entity, Page, PageSize);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                list = DataTableToObject.ConvertToList<VipCardGradeChangeLogEntity>(ds.Tables[0]);
            }

            return list;
        }
        /// <summary>
        /// 列表数量获取
        /// </summary>
        public int GetListCount(VipCardGradeChangeLogEntity entity)
        {
            return _currentDAO.GetListCount(entity);
        }
        #endregion

        #region 升降级
        /// <summary>
        /// 保存升降级记录
        /// </summary>
        /// <param name="VipCardID">会员卡标识</param>
        /// <param name="ChangeBeforeGradeID">变动前等级</param>
        /// <param name="NowGradeID">最终卡等级</param>
        /// <param name="ChangeReason">变动原因</param>
        /// <param name="UnitID">操作门店</param>
        /// <param name="strError"></param>
        /// <returns></returns>
        public bool SetVipCardGradeChange(string VipCardID
                                        , int ChangeBeforeGradeID
                                        , int NowGradeID
                                        , string ChangeReason
                                        , string UnitID
                                        , out string strError)
        {
            try
            {
                #region
                if (VipCardID == null || VipCardID.Trim().Equals(""))
                {
                    strError = "会员卡号标识不能为空.";
                    return false;
                }
                if (ChangeBeforeGradeID == 0 || ChangeBeforeGradeID.ToString().Trim().Equals(""))
                {
                    strError = "变动前等级标识不能为空.";
                    return false;
                }
                if (NowGradeID == 0 || NowGradeID.ToString().Trim().Equals(""))
                {
                    strError = "最终卡等级标识不能为空.";
                    return false;
                }
                if (UnitID == null || UnitID.Trim().Equals(""))
                {
                    strError = "操作门店标识不能为空.";
                    return false;
                }
                #endregion
                #region 1.设置会员卡主信息的卡等级
                VipCardEntity vipCardInfo = new VipCardEntity();
                vipCardInfo.VipCardID = VipCardID;
                vipCardInfo.VipCardGradeID = NowGradeID;
                vipCardInfo.LastUpdateBy = this.CurrentUserInfo.UserID;
                vipCardInfo.LastUpdateTime = System.DateTime.Now;
                #endregion
                #region 2.设置卡等级变更记录
                VipCardGradeChangeLogEntity vipCardGCInfo = new VipCardGradeChangeLogEntity();
                vipCardGCInfo.ChangeLogID = JIT.CPOS.BS.BLL.BaseService.NewGuidPub();
                vipCardGCInfo.VipCardID = VipCardID;
                vipCardGCInfo.ChangeBeforeGradeID = ChangeBeforeGradeID;
                vipCardGCInfo.NowGradeID = NowGradeID;
                vipCardGCInfo.ChangeReason = ChangeReason;
                vipCardGCInfo.ChangeTime = System.DateTime.Now;
                vipCardGCInfo.UnitID = UnitID;
                vipCardGCInfo.OperationUserID = this.CurrentUserInfo.UserID;
                vipCardGCInfo.OperationType = 2;
                vipCardGCInfo.CreateBy = this.CurrentUserInfo.UserID;
                vipCardGCInfo.LastUpdateBy = this.CurrentUserInfo.UserID;
                #endregion
                return _currentDAO.SetVipCardStatusChange(vipCardInfo, vipCardGCInfo,out strError);
            }
            catch (Exception ex) {
                strError = ex.ToString();
                return false;
            }
        }
        #endregion

    }
}