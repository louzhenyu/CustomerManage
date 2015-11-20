/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013-12-14 15:57
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

namespace JIT.CPOS.BS.BLL
{
    /// <summary>
    /// 业务处理：  
    /// </summary>
    public partial class CouponTypeBLL
    {
        #region GetCouponType
        /// <summary>
        /// 获取所有优惠劵类别
        /// </summary>
        /// <returns></returns>
        public DataSet GetCouponType()
        {
            return this._currentDAO.GetCouponType();
        }
        #endregion

        #region GetCouponTypeList
        public DataSet GetCouponTypeList()
        {
            return this._currentDAO.GetCouponTypeList();
        }
        #endregion

        /// <summary>
        /// 事务
        /// </summary>
        /// <returns></returns>
        public SqlTransaction GetTran()
        {
            return this._currentDAO.GetTran();
        }
        /// <summary>
        /// 在事务内创建一个新实例
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        /// <param name="pTran">事务实例,可为null,如果为null,则不使用事务来更新</param>
        public Guid CreateReturnID(CouponTypeEntity pEntity, IDbTransaction pTran)
        {
            return this._currentDAO.CreateReturnID(pEntity, pTran);
        }
        /// <summary>
        /// 优惠券使用情况
        /// </summary>
        /// <returns></returns>
        public DataSet GetCouponTypeCount()
        {
            return this._currentDAO.GetCouponTypeCount();
        }
    }
}