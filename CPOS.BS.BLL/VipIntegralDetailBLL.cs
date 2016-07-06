/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/5/14 11:13:49
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
using System.Data.SqlClient;
using JIT.CPOS.DTO.Base;

namespace JIT.CPOS.BS.BLL
{
    /// <summary>
    /// 业务处理：  
    /// </summary>
    public partial class VipIntegralDetailBLL
    {
        #region GetVipIntegralDetailList
        /// <summary>
        /// GetVipIntegralDetailList
        /// </summary>
        /// <param name="entity">entity</param>
        /// <param name="Page">分页页码。从0开始</param>
        /// <param name="PageSize">每页的数量。未指定时默认为15</param>
        /// <returns></returns>
        public IList<VipIntegralDetailEntity> GetVipIntegralDetailList(VipIntegralDetailEntity entity, int Page, int PageSize)
        {
            if (PageSize <= 0) PageSize = 15;

            IList<VipIntegralDetailEntity> list = new List<VipIntegralDetailEntity>();
            DataSet ds = new DataSet();
            ds = _currentDAO.GetVipIntegralDetailList(entity, Page, PageSize);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                list = DataTableToObject.ConvertToList<VipIntegralDetailEntity>(ds.Tables[0]);
            }
            return list;
        }
        /// <summary>
        /// GetVipIntegralDetailListCount
        /// </summary>
        /// <param name="entity">entity</param>
        public int GetVipIntegralDetailListCount(VipIntegralDetailEntity entity)
        {
            return _currentDAO.GetVipIntegralDetailListCount(entity);
        }
        #endregion

        /// <summary>
        /// 获取总消费金额
        /// </summary>
        public decimal GetVipSalesAmount(string vipId)
        {
            return _currentDAO.GetVipSalesAmount(vipId);
        }

        /// <summary>
        /// 获取总产生积分
        /// </summary>
        public decimal GetVipIntegralAmount(string vipId)
        {
            return _currentDAO.GetVipIntegralAmount(vipId);
        }

        /// <summary>
        /// 下线用户关注获取积分总数
        /// </summary>
        public decimal GetVipNextLevelIntegralAmount(string vipId)
        {
            return _currentDAO.GetVipNextLevelIntegralAmount(vipId);
        }

        public decimal GetVipIntegralByOrder(string orderId, string userId)
        {
            return this._currentDAO.GetVipIntegralByOrder(orderId, userId);
        }
        /// <summary>
        /// 积分变更业务处理
        /// </summary>
        /// <param name="pEntity"></param>
        /// <param name="pTran"></param>
        public bool AddIntegral(VipIntegralDetailEntity pEntity, SqlTransaction tran)
        {
            bool b = true;
            //更新个人账户的可使用余额 
            var vipBLL = new VipBLL(CurrentUserInfo);
            var vipIntegralBLL = new VipIntegralBLL(CurrentUserInfo);
            var vipIntegralDetailBLL = new VipIntegralDetailBLL(CurrentUserInfo);
            var unitBLL = new t_unitBLL(CurrentUserInfo);
            var vipInfo = vipBLL.GetByID(pEntity.VIPID);
            var vipIntegralInfo = vipIntegralBLL.GetByID(pEntity.VIPID);
            var vipCardBLL = new VipCardBLL(CurrentUserInfo);

            //创建会员积分记录信息
            if (vipIntegralInfo == null)
            {
                vipIntegralInfo = new VipIntegralEntity
                {
                    VipID = pEntity.VIPID,
                    BeginIntegral = pEntity.Integral,
                    InIntegral = pEntity.Integral,
                    EndIntegral = pEntity.Integral,
                    ValidIntegral = pEntity.Integral,
                    CumulativeIntegral = pEntity.Integral,
                    VipCardCode = pEntity.VipCardCode,
                    OutIntegral = 0,
                    InvalidIntegral = 0,
                    CustomerID = pEntity.CustomerID
                };
                vipIntegralBLL.Create(vipIntegralInfo, tran);
            }
            else//修改会员积分记录信息
            {
                vipIntegralInfo.EndIntegral = (vipIntegralInfo.EndIntegral == null ? 0 : vipIntegralInfo.EndIntegral.Value) + pEntity.Integral;
                vipIntegralInfo.ValidIntegral = (vipIntegralInfo.ValidIntegral == null ? 0 : vipIntegralInfo.ValidIntegral.Value) + pEntity.Integral;
                if (pEntity.Integral > 0)
                {
                    vipIntegralInfo.InIntegral = (vipIntegralInfo.InIntegral == null ? 0 : vipIntegralInfo.InIntegral.Value) + pEntity.Integral;
                    vipIntegralInfo.CumulativeIntegral = (vipIntegralInfo.CumulativeIntegral == null ? 0 : vipIntegralInfo.CumulativeIntegral.Value) + pEntity.Integral;
                }
                else
                {
                    if (vipInfo.Integration < Math.Abs(pEntity.Integral.Value))
                        throw new APIException("扣减的积分不能大于当前积分") { ErrorCode = ERROR_CODES.INVALID_BUSINESS };
                    vipIntegralInfo.OutIntegral = (vipIntegralInfo.OutIntegral == null ? 0 : vipIntegralInfo.OutIntegral.Value) + pEntity.Integral;
                }
                vipIntegralBLL.Update(vipIntegralInfo, tran);
            }
            //修改会员信息剩余积分
            if (vipInfo != null)
            {
                vipInfo.Integration = (vipInfo.Integration == null ? 0 : vipInfo.Integration.Value) + pEntity.Integral;
                vipBLL.Update(vipInfo, tran);
            }

            // 更新VIPCARD表积分余额
            vipCardBLL.UpdateIntegral(pEntity.VipCardCode, vipIntegralInfo.ValidIntegral, tran);

            //增加记录
            VipIntegralDetailEntity detail = new VipIntegralDetailEntity() { };
            detail.VipIntegralDetailID = Guid.NewGuid().ToString();
            detail.VIPID = pEntity.VIPID;
            detail.VipCardCode = pEntity.VipCardCode;
            detail.ObjectId = pEntity.ObjectId;
            detail.Integral = pEntity.Integral;
            detail.IntegralSourceID = pEntity.IntegralSourceID;
            detail.Reason = pEntity.Reason;
            detail.Remark = pEntity.Remark;
            detail.CustomerID = pEntity.CustomerID;
            detail.UsedIntegral = 0;

            detail.UnitID = pEntity.UnitID;
            var unitInfo = unitBLL.GetByID(pEntity.UnitID);
            detail.UnitName = unitInfo != null ? unitInfo.unit_name : null;

            vipIntegralDetailBLL.Create(detail, tran);
            pEntity.VipIntegralDetailID = detail.VipIntegralDetailID;
            return b;

        }

        /// <summary>
        /// 事务
        /// </summary>
        /// <returns></returns>
        public SqlTransaction GetTran()
        {
            return this._currentDAO.GetTran();
        }
    }
}