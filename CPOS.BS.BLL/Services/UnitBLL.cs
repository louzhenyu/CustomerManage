using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Reflection;
using JIT.Utility;
using JIT.Utility.ExtensionMethod;
using JIT.Utility.DataAccess;
using JIT.Utility.DataAccess.Query;
using JIT.CPOS.BS.Entity;
using System.Collections;
using JIT.CPOS.BS.DataAccess;

namespace JIT.CPOS.BS.BLL
{
    public partial class UnitBLL
    {
        private BasicUserInfo CurrentUserInfo;
        private UnitDAO _currentDAO;
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public UnitBLL(LoggingSessionInfo pUserInfo)
        {
            this._currentDAO = new UnitDAO(pUserInfo);
            this.CurrentUserInfo = pUserInfo;
        }
        #endregion
        /// <summary>
        /// 获取门店列表
        /// </summary>
        /// <param name="ht"></param>
        /// <returns></returns>
        public DataSet GetUnitList(Hashtable ht)
        {
            return this._currentDAO.GetUnitList(ht);
        }
        /// <summary>
        /// 获取门店详情
        /// </summary>
        /// <param name="memberId"></param>
        /// <param name="unitId"></param>
        /// <returns></returns>
        public DataSet GetUnitDetail(string memberId, string unitId)
        {
            return this._currentDAO.GetUnitDetail(memberId, unitId);
        }

        /// <summary>
        /// 获取门店详情
        /// </summary>
        /// <param name="memberId"></param>
        /// <param name="unitId"></param>
        /// <returns></returns>
        public t_unitEntity GetUnitDetail(string unitId)
        {
            return this._currentDAO.GetUnitDetail(unitId);
        }

        #region 积分列表与汇总
        /// <summary>
        /// 获取个人积分列表
        /// </summary>
        /// <param name="CustomerId">商户编号</param>
        /// <param name="wherecondition">条件汇总</param>
        /// <param name="PageIndex">当前页码</param>
        /// <param name="PageSize">每页显示条数</param>
        /// <returns></returns>
        public PagedQueryResult<VipIntegralDetailEntity> GetMyIntegral(IWhereCondition[] wherecondition, int PageIndex, int PageSize)
        {
            return this._currentDAO.GetMyIntegral(wherecondition, PageIndex, PageSize);
        }

        /// <summary>
        /// 统计收入积分 支出积分 总积分信息
        /// </summary>
        /// <param name="CustomerId"></param>
        /// <param name="VipCardCode">会员卡编号</param>
        /// <param name="VipId"></param>
        /// <returns>
        /// decimal[0]=总积分
        /// decimal[1]=收入积分
        /// decimal[2]=支出积分
        /// </returns>
        public decimal[] GetMyTotalIntegral(string CustomerId, string VipId, string VipCardCode)
        {
            return this._currentDAO.GetMyTotalIntegral(CustomerId, VipId, VipCardCode);
        }
        #endregion

        #region 余额列表与汇总
        /// <summary>
        /// 获取账户余额
        /// </summary>
        /// <param name="hsPara"></param>
        /// <returns></returns>
        public DataSet GetMyAccount(Hashtable hsPara)
        {
            return this._currentDAO.GetMyAccount(hsPara);
        }
        #endregion

        /// <summary>
        /// 获取店铺优惠券
        /// </summary>
        /// <param name="hsPara"></param>
        /// <returns></returns>
        public DataSet GetCouponList(Hashtable hsPara)
        {
            return this._currentDAO.GetCouponList(hsPara);
        }
        /// <summary>
        /// 根据商品业务类型获取商品信息
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="itemSortId"></param>
        /// <returns></returns>
        public DataSet GetItemBySortId(string customerId, int itemSortId)
        {
            return this._currentDAO.GetItemBySortId(customerId, itemSortId);
        }
        /// <summary>
        /// 根据SkuID获取价格信息
        /// </summary>
        /// <param name="skuId"></param>
        /// <returns></returns>
        public DataSet GetSkuPirce(string skuId)
        {
            return this._currentDAO.GetSkuPirce(skuId);
        }
    }
}
