/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/2/27 13:31:25
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
    public partial class TInoutStatusBLL
    {
        #region GetList
        /// <summary>
        /// 查询订单日志
        /// </summary>
        /// <param name="entity">参数：实体</param>
        /// <param name="pageIndex">参数：当前页</param>
        /// <param name="pageSize">参数：分页个数</param>
        /// <param name="rowCount">参数：数据集的总数</param>
        /// <returns></returns>
        public TInoutStatusEntity[] GetList(TInoutStatusEntity entity, int pageIndex, int pageSize, out int rowCount)
        {
            List<IWhereCondition> wheres = new List<IWhereCondition>();

            if (entity != null && entity.OrderID != null)
            {
                wheres.Add(new EqualsCondition() { FieldName = "T.OrderID", Value = entity.OrderID });
            }

            List<OrderBy> orderbys = new List<OrderBy>();
            orderbys.Add(new OrderBy() { FieldName = "[CreateTime]", Direction = OrderByDirections.Desc });

            PagedQueryResult<TInoutStatusEntity> pEntity = new TInoutStatusDAO(this.CurrentUserInfo).GetList(wheres.ToArray(), orderbys.ToArray(), pageIndex, pageSize);

            rowCount = pEntity.RowCount;
            return pEntity.Entities;
        }
        #endregion

        #region OrdersNode
        /// <summary>
        /// 订单节点
        /// </summary>
        public enum OrdersNode
        {
            /// <summary>
            /// 提交
            /// </summary>
            Submit,
            /// <summary>
            /// 审核
            /// </summary>
            Approve
        }
        #endregion

        #region OrdersStatus
        /// <summary>
        /// 订单状态以及对应的值
        /// </summary>
        public enum OrdersStatus
        {
            ApproveFailure = -2,
            Cancel = -1,
            WaitApprove = 1,
            WaitPay = 2,
            WaitStay = 3,
            Complete = 4
        }
        #endregion
    }
}