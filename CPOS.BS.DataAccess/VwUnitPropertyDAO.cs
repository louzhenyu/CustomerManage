/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/9/10 10:27:47
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
    /// 表VwUnitProperty的数据访问类 
    /// TODO:
    /// 1.实现ICRUDable接口
    /// 2.实现IQueryable接口
    /// 3.实现Load方法
    /// </summary>
    public partial class VwUnitPropertyDAO : Base.BaseCPOSDAO, ICRUDable<VwUnitPropertyEntity>, IQueryable<VwUnitPropertyEntity>
    {
        public DataSet GetUnitPropertyByOrderId(string OrderId)
        {
            string sql = "SELECT a.*  "
                    + " ,CASE WHEN ISNULL((SELECT COUNT(*) FROM dbo.VipUnitMapping x INNER JOIN dbo.T_Inout y ON(x.VIPID=y.vip_no) "
                    + " WHERE x.IsDelete = '0' AND x.UnitId = a.UnitId AND y.order_id = '" + OrderId + "'),0) = 0 THEN '否' ELSE '是' END IsVipStore "
                    + " FROM dbo.VwUnitProperty a "
                    + " INNER JOIN dbo.t_unit b ON(a.UnitId = b.unit_id) "
                    + " WHERE a.IsDelete = '0' "
                    + " AND b.type_id = 'EB58F1B053694283B2B7610C9AAD2742' "
                    + " AND b.Status = '1' "
                    + " AND b.customer_id = '"+this.CurrentUserInfo.CurrentUser.customer_id+"' "
                    + " ORDER BY Distance";
            return this.SQLHelper.ExecuteDataset(sql);
        }

        public string GetUnitWXCode(string UnitId)
        {
            string sql = "select isnull(MAX(WeiXinUnitCode),0) WeiXinUnitCode From VwUnitProperty where CustomerId = '"+this.CurrentUserInfo.CurrentUser.customer_id+"' and WeiXinUnitCode is not null and WeiXinUnitCode<>'' group by CustomerId";
            return Convert.ToString( this.SQLHelper.ExecuteScalar(sql));
        }
    }

    
}
