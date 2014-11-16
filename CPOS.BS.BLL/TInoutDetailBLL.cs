using System;
using System.Data;
using System.Collections.Generic;

using JIT.CPOS.BS.Entity;

using JIT.Utility.DataAccess;

namespace JIT.CPOS.BS.BLL
{
    /// <summary>
    /// 业务处理： 订单明细
    /// </summary>
    public partial class TInoutDetailBLL
    {
        #region GetOrdersDetail
        /// <summary>
        /// 根据订单ID获取订单明细 和 订单流水信息
        /// </summary>
        /// <param name="pEntity"></param>
        /// <returns></returns>
        public DataSet GetOrdersDetail(TInoutDetailEntity pEntity, string isHotel)
        {
            return this._currentDAO.GetOrderDetail(pEntity,isHotel);
        }
        #endregion

        #region HS_GetOrdersDetail
        /// <summary>
        /// 根据订单ID获取订单明细 和 订单流水信息
        /// </summary>
        /// <param name="pEntity"></param>
        /// <returns></returns>
        public DataSet HS_GetOrdersDetail(TInoutEntity pEntity)
        {
            return this._currentDAO.HS_GetOrderDetail(pEntity);
        }
        #endregion

        public DataSet GetOrderDetailImageList(string itemIdList)
        {
            return this._currentDAO.GetOrderDetailImageList(itemIdList);
        }


        #region 未付款时，取消订单，重算积分，余额，优惠券

        #endregion
    }
}
