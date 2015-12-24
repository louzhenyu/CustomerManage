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
using System.Data;
using System.Data.SqlClient;
using System.Text;

using JIT.Utility;
using JIT.Utility.Entity;
using JIT.Utility.ExtensionMethod;
using JIT.Utility.DataAccess;
using JIT.Utility.Log;
using JIT.CPOS.BS.Entity;
using JIT.Utility.DataAccess.Query;
using JIT.CPOS.BS.DataAccess.Base;

namespace JIT.CPOS.BS.DataAccess
{
    
    /// <summary>
    /// 数据访问：  
    /// 表VipCouponMapping的数据访问类 
    /// TODO:
    /// 1.实现ICRUDable接口
    /// 2.实现IQueryable接口
    /// 3.实现Load方法
    /// </summary>
    public partial class VipCouponMappingDAO : Base.BaseCPOSDAO, ICRUDable<VipCouponMappingEntity>, IQueryable<VipCouponMappingEntity>
    {
        /// <summary>
        /// 判断发起分享的用户的优惠券是否被赠送
        /// 
        /// </summary>
        /// <param name="strCouponId"></param>
        /// <returns></returns>
        public int HadBeGranted(string strCouponId,string strGiver)
        {
            string strSql = string.Format(@"SELECT COUNT (1) FROM   [dbo].[VipCouponMapping] WITH(NOLOCK)
                                            WHERE  CouponID='{0}' AND IsDelete=0 AND VIPID='{1}'
            ", strCouponId, strGiver);
            return Convert.ToInt32(this.SQLHelper.ExecuteScalar(strSql));
        }
        /// <summary>
        /// 优惠券转增
        /// </summary>
        /// <param name="strGiver">赠送者</param>
        /// <param name="strGrantee">被赠送</param>
        /// <param name="strCouponId">优惠券ID</param>
        /// <returns></returns>
        public int  GrantCoupon(string strGiver,string strGrantee,string strCouponId)
        {
            string strSql = string.Format(@"Update  [dbo].[VipCouponMapping] Set VIPID='{0}'
                                               ,FromVipId='{1}',CouponSourceId='22E189E1-57C2-488E-A1DA-C42AEBAF3766'
                                            WHERE  CouponID='{2}' AND IsDelete=0
            ", strGrantee,strGiver,strCouponId);
            return this.SQLHelper.ExecuteNonQuery(strSql);
        }
    }
}
