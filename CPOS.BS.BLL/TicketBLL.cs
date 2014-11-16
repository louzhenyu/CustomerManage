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
    /// 业务处理：  
    /// </summary>
    public partial class TicketBLL
    {

        #region GetList
        /// <summary>
        /// 获取活动票务列表
        /// </summary>
        /// <param name="entity">参数：实体</param>
        /// <param name="pageIndex">参数：当前页</param>
        /// <param name="pageSize">参数：分页个数</param>
        /// <param name="rowCount">参数：数据集的总数</param>
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
        /// 通过主键删除活动票务
        /// </summary>
        /// <param name="id">主键</param>
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