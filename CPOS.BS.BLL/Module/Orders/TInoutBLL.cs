using System;
using System.Collections.Generic;

using JIT.CPOS.BS.Entity;

using JIT.Utility.DataAccess;
using System.Text;
using JIT.Utility.DataAccess.Query;
using System.Data;
using System.Data.SqlClient;

namespace JIT.CPOS.BS.BLL
{
    /// <summary>
    /// 业务处理： 订单
    /// </summary>
    public partial class TInoutBLL
    {
        #region GetOrderList
        /// <summary>
        /// 获取订单列表
        /// </summary>
        /// <param name="pParems"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public PagedQueryResult<TInoutViewEntity> GetOrdersList(Dictionary<string, object> pParems, int pageIndex, int pageSize)
        {
            return _currentDAO.GetOrdersList(pParems, pageIndex, pageSize);
        }
        #endregion

        #region OrdersApprove
        /// <summary>
        /// 订单审批
        /// </summary>
        /// <param name="pParams"></param>
        public void OrdersApprove(Dictionary<string, string> pParams)
        {
            this._currentDAO.OrdersApprove(pParams);
        }
        #endregion

        #region Complete
        /// <summary>
        /// 订单完成
        /// </summary>
        /// <param name="pOrdersID">订单ID</param>
        /// <param name="pStatus">订单状态</param>
        public void Complete(string pOrdersID, string pStatus)
        {
            this._currentDAO.Complete(pOrdersID, pStatus);
        }
        #endregion

        #region GetOrdersListCount
        /// <summary>
        /// 获取订单各个状态的数量
        /// </summary>
        /// <param name="pParams"></param>
        /// <returns></returns>
        public DataSet GetOrdersListCount(Dictionary<string, string> pParams)
        {
            return this._currentDAO.GetOrdersListCount(pParams);
        }
        #endregion

        #region GetOrdersListCountFHotels
        /// <summary>
        /// 获取订单各个状态的数量
        /// </summary>
        /// <param name="pParams"></param>
        /// <returns></returns>
        public DataSet GetOrdersListCountFHotels(Dictionary<string, string> pParams)
        {
            return this._currentDAO.GetOrdersListCountFHotels(pParams);
        }
        #endregion

        public SqlTransaction GetTran()
        {
            return this._currentDAO.GetTran();
        }

        public DataSet GetStoreInfo(string storeId)
        {
            return this._currentDAO.GetStoreInfo(storeId);
        }
    }
}
