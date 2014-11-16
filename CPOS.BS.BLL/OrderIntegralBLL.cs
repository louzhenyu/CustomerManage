﻿/*
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
    public partial class OrderIntegralBLL
    {

        #region GetList
        /// <summary>
        /// 查询积分订单
        /// </summary>
        /// <param name="entity">参数：实体</param>
        /// <param name="pageIndex">参数：当前页</param>
        /// <param name="pageSize">参数：分页个数</param>
        /// <param name="rowCount">参数：数据集的总数</param>
        /// <returns></returns>
        public OrderIntegralEntity[] GetList(OrderIntegralEntity entity, int pageIndex, int pageSize, out int rowCount)
        {
            List<IWhereCondition> wheres = new List<IWhereCondition>();

            if (entity != null && !string.IsNullOrEmpty(entity.item_code))
            {
                wheres.Add(new LikeCondition() { FieldName = "ti.item_code", HasLeftFuzzMatching = true, HasRightFuzzMathing = true, Value = entity.item_code });
            }
            if (entity != null && !string.IsNullOrEmpty(entity.item_name))
            {
                wheres.Add(new LikeCondition() { FieldName = "ti.item_name", HasLeftFuzzMatching = true, HasRightFuzzMathing = true, Value = entity.item_name });
            }
            if (entity != null && !string.IsNullOrEmpty(entity.VipName))
            {
                wheres.Add(new LikeCondition() { FieldName = "vp.VipName", HasLeftFuzzMatching = true, HasRightFuzzMathing = true, Value = entity.VipName });
            }

            List<OrderBy> orderbys = new List<OrderBy>();
            orderbys.Add(new OrderBy() { FieldName = "[CreateTime]", Direction = OrderByDirections.Desc });

            PagedQueryResult<OrderIntegralEntity> pEntity = new OrderIntegralDAO(this.CurrentUserInfo).GetList(wheres.ToArray(), orderbys.ToArray(), pageIndex, pageSize);

            rowCount = pEntity.RowCount;
            return pEntity.Entities;
        }


        /// <summary>
        /// 查询全部积分订单
        /// </summary>
        /// <param name="entity">参数：实体</param>
        /// <returns></returns>
        public OrderIntegralEntity[] GetAllList(OrderIntegralEntity entity)
        {
            List<IWhereCondition> wheres = new List<IWhereCondition>();

            if (entity != null && !string.IsNullOrEmpty(entity.item_code))
            {
                wheres.Add(new LikeCondition() { FieldName = "ti.item_code", HasLeftFuzzMatching = true, HasRightFuzzMathing = true, Value = entity.item_code });
            }
            if (entity != null && !string.IsNullOrEmpty(entity.item_name))
            {
                wheres.Add(new LikeCondition() { FieldName = "ti.item_name", HasLeftFuzzMatching = true, HasRightFuzzMathing = true, Value = entity.item_name });
            }
            if (entity != null && !string.IsNullOrEmpty(entity.VipName))
            {
                wheres.Add(new LikeCondition() { FieldName = "vp.VipName", HasLeftFuzzMatching = true, HasRightFuzzMathing = true, Value = entity.VipName });
            }

            return new OrderIntegralDAO(this.CurrentUserInfo).GetAllList(wheres.ToArray());
        }

        #endregion



    }
}