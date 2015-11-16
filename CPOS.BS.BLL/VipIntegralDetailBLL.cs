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
        /// 事务
        /// </summary>
        /// <returns></returns>
        public SqlTransaction GetTran()
        {
            return this._currentDAO.GetTran();
        }
    }
}