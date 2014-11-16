/*
 * Author		:jun.tian
 * EMail		:jun.tian@jitmarketing.cn
 * Company		:JIT
 * Create On	:2013/3/2 15:50:27
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
    /// UnitBLL 
    /// </summary>
    public partial class UnitBLL
    {
        #region GetList
        /// <summary>
        /// 获取客户参数单位列表
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public UnitEntity[] GetList(UnitEntity entity)
        {
            IWhereCondition[] whereCondition = new IWhereCondition[2];

            EqualsCondition Conditioin = new EqualsCondition();
            Conditioin.FieldName = "ClientID";
            Conditioin.Value = CurrentUserInfo.ClientID;
            whereCondition[0] = Conditioin;

            EqualsCondition Conditioin1 = new EqualsCondition();
            Conditioin1.FieldName = "ClientDistributorID";
            Conditioin1.Value = CurrentUserInfo.ClientDistributorID;
            whereCondition[1] = Conditioin1;

            OrderBy[] orderByCondition = new OrderBy[1];
            OrderBy orderBy = new OrderBy();
            orderBy.Direction = OrderByDirections.Asc;
            orderBy.FieldName = "CreateTime";
            orderByCondition[0] = orderBy;
            return this.PagedQuery(whereCondition, orderByCondition, 1000, 1).Entities;
        }
        #endregion
    }
}