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

namespace JIT.CPOS.BS.BLL
{
    /// <summary>
    /// 业务处理：  
    /// </summary>
    public partial class VipCouponMappingBLL
    {
        /// <summary>
        /// 判断发起分享的用户的优惠券是否被赠送 
        /// </summary>
        /// <param name="strCouponId"></param>
        /// <returns></returns>
        public int HadBeGranted(string strCouponId, string strGiver)
        {
            return this._currentDAO.HadBeGranted(strCouponId, strGiver);
        }
        /// <summary>
        /// 优惠券转增
        /// </summary>
        /// <param name="strGiver">赠送者</param>
        /// <param name="strGrantee">被赠送</param>
        /// <param name="strCouponId">优惠券ID</param> 
        /// 
        /// <returns></returns>
        public  int  GrantCoupon(string strGiver,string strGrantee,string strCouponId)
        {
            return this._currentDAO.GrantCoupon(strGiver, strGrantee, strCouponId);
        }
    }
}