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
    /// ҵ����  
    /// </summary>
    public partial class TInoutStatusBLL
    {
        #region GetList
        /// <summary>
        /// ��ѯ������־
        /// </summary>
        /// <param name="entity">������ʵ��</param>
        /// <param name="pageIndex">��������ǰҳ</param>
        /// <param name="pageSize">��������ҳ����</param>
        /// <param name="rowCount">���������ݼ�������</param>
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
        /// �����ڵ�
        /// </summary>
        public enum OrdersNode
        {
            /// <summary>
            /// �ύ
            /// </summary>
            Submit,
            /// <summary>
            /// ���
            /// </summary>
            Approve
        }
        #endregion

        #region OrdersStatus
        /// <summary>
        /// ����״̬�Լ���Ӧ��ֵ
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