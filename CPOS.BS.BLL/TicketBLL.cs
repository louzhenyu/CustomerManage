/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/3/7 15:07:52
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
using JIT.CPOS.BS.DataAccess.Base;

namespace JIT.CPOS.BS.BLL
{
    /// <summary>
    /// ҵ����  
    /// </summary>
    public partial class TicketBLL
    {

        #region GetList
        /// <summary>
        /// ��ȡ�Ʊ���б�
        /// </summary>
        /// <param name="entity">������ʵ��</param>
        /// <param name="pageIndex">��������ǰҳ</param>
        /// <param name="pageSize">��������ҳ����</param>
        /// <param name="rowCount">���������ݼ�������</param>
        /// <returns></returns>
        public TicketEntity[] GetList(TicketEntity entity, int pageIndex, int pageSize, out int rowCount)
        {
            List<IWhereCondition> wheres = new List<IWhereCondition>();

            if (entity != null && !string.IsNullOrEmpty(entity.EventID))
            {
                wheres.Add(new EqualsCondition() { FieldName = "T.EventID", Value = entity.EventID });
            }
            if (entity != null && !string.IsNullOrEmpty(entity.TicketName))
            {
                wheres.Add(new LikeCondition() { FieldName = "T.TicketName", HasLeftFuzzMatching = true, HasRightFuzzMathing = true, Value = entity.TicketName });
            }


            List<OrderBy> orderbys = new List<OrderBy>();
            orderbys.Add(new OrderBy() { FieldName = "TicketID", Direction = OrderByDirections.Asc });

            PagedQueryResult<TicketEntity> pEntity = new TicketDAO(this.CurrentUserInfo).GetList(wheres.ToArray(), orderbys.ToArray(), pageIndex, pageSize);

            rowCount = pEntity.RowCount;
            return pEntity.Entities;
        }
        #endregion   


        #region DeleteTicket
        /// <summary>
        /// ͨ������ɾ���Ʊ��
        /// </summary>
        /// <param name="id">����</param>
        public void DeleteTicket(string id)
        {

            IDbTransaction tran = new TransactionHelper(this.CurrentUserInfo).CreateTransaction();
            using (tran.Connection)
            {
                try
                {
                    this.Delete(Guid.Parse(id), tran);
                    tran.Commit();
                }
                catch
                {
                    tran.Rollback();
                    throw;
                }
            }

        }
        #endregion


    }
}