/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2015-1-15 20:06:20
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
using JIT.Utility.DataAccess.Query;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.BS.DataAccess.Base;

namespace JIT.CPOS.BS.DataAccess
{
    
    /// <summary>
    /// ���ݷ��ʣ�  
    /// ��CouponUse�����ݷ����� 
    /// TODO:
    /// 1.ʵ��ICRUDable�ӿ�
    /// 2.ʵ��IQueryable�ӿ�
    /// 3.ʵ��Load����
    /// </summary>
    public partial class CouponUseDAO : Base.BaseCPOSDAO, ICRUDable<CouponUseEntity>, IQueryable<CouponUseEntity>
    {
        /// <summary>
        /// ����ʹ�õ��Ż�ȯ�б�
        /// </summary>
        /// <param name="vipId">�û�ID</param>
        /// <param name="orderId">����ID</param>
        /// <returns></returns>
        public DataSet GetOrderCouponUseList(string vipId, string orderId)
        {
            string sql = string.Empty;
            sql += " SELECT a.couponId, a.CouponDesc, b.ParValue, b.ConditionValue, c.CouponSource ";
            sql += " FROM Coupon a ";
            sql += " INNER JOIN CouponType b ON a.CouponTypeID = b.CouponTypeID ";
            sql += " LEFT JOIN CouponSource c ON b.CouponSourceID = c.CouponSourceID ";
            sql += " INNER JOIN dbo.CouponUse d ON a.CouponID = d.CouponID ";
            sql += " WHERE a.IsDelete = 0 AND b.IsDelete = 0 AND d.IsDelete = 0 ";
            sql += " AND a.EndDate > GETDATE() ";
            sql += " AND a.[Status] = 1 ";
            sql += " AND d.VIPID = '" + vipId + "' and d.OrderId = '" + orderId + "' ";
            sql += " ORDER BY b.ParValue DESC";

            var ds = this.SQLHelper.ExecuteDataset(sql);
            return ds;
        }

        public DataSet GetCouponUseListByCouponID(string vipID, string CouponID)
        {
            string sql = string.Empty;
            sql += " SELECT a.couponId, a.CouponDesc, b.ParValue, b.ConditionValue, c.CouponSource ";
            sql += " FROM Coupon a ";
            sql += " INNER JOIN CouponType b ON a.CouponTypeID = b.CouponTypeID ";
            sql += " LEFT JOIN CouponSource c ON b.CouponSourceID = c.CouponSourceID ";
            sql += " INNER JOIN dbo.CouponUse d ON a.CouponID = d.CouponID ";
            sql += " WHERE a.IsDelete = 0 AND b.IsDelete = 0 AND d.IsDelete = 0 ";
            sql += " AND a.EndDate > GETDATE() ";
            sql += " AND a.[Status] = 1 ";
            sql += " AND d.VIPID = '" + vipID + "' and d.couponId = '" + CouponID + "' ";
            sql += " ORDER BY b.ParValue DESC";

            var ds = this.SQLHelper.ExecuteDataset(sql);
            return ds;
        }

    }
}
