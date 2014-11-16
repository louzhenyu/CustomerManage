/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/7/25 10:37:49
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
    /// 表t_unit的数据访问类 
    /// TODO:
    /// 1.实现ICRUDable接口
    /// 2.实现IQueryable接口
    /// 3.实现Load方法
    /// </summary>
    public partial class TUnitDAO : Base.BaseCPOSDAO, ICRUDable<TUnitEntity>, IQueryable<TUnitEntity>
    {
        /// <summary>
        /// 获取直接子部门
        /// </summary>
        /// <param name="pParentUnitID"></param>
        /// <returns></returns>
        public DataTable GetDirectSubDept(object pParentUnitID)
        {
            string sql = "SELECT tur.src_unit_id AS ParentUnitID,unit_id AS UnitID, unit_name AS UnitName,type_id AS TypeID,unit_contact AS Leader";
            sql += " ,unit_remark AS DeptDesc FROM t_unit AS tu INNER JOIN T_Unit_Relation AS tur ON tu.unit_id=tur.dst_unit_id ";
            sql += " WHERE src_unit_id=@parentUnitID AND customer_id=@customer_id AND tu.Status=1";
            List<SqlParameter> para = new List<SqlParameter>();
            para.Add(new SqlParameter("@parentUnitID", pParentUnitID));
            para.Add(new SqlParameter("@customer_id", this.CurrentUserInfo.CurrentUser.customer_id));
            DataSet ds = this.SQLHelper.ExecuteDataset(CommandType.Text, sql, para.ToArray());
            if (ds != null && ds.Tables != null && ds.Tables.Count > 0)
                return ds.Tables[0];
            return null;
        }
        /// <summary>
        /// 获取部门-管理
        /// </summary>
        /// <param name="pUnitID"></param>
        /// <param name="pTypeID"></param>
        /// <param name="pPageIndex"></param>
        /// <param name="pPageSize"></param>
        /// <returns></returns>
        public DataTable GetUnitList(string pUnitID, string pTypeID, int pPageIndex, int pPageSize, string pUnitName, out int totalPage)
        {
            int begin = pPageIndex * pPageSize + 1;
            int end = (pPageIndex + 1) * pPageSize;

            StringBuilder pagedSql = new StringBuilder();
            StringBuilder totalSql = new StringBuilder();

            pagedSql.AppendFormat("SELECT * FROM (SELECT ROW_NUMBER() OVER");
            totalSql.AppendFormat("select count(1) ");

            pagedSql.AppendFormat(" (ORDER BY unit_name) AS rowid,unit_id AS UnitID,unit_name AS UnitName,unit_code AS UnitCode");
            pagedSql.AppendFormat(" ,unit_contact AS Leader,unit_remark AS DeptDesc,Status,if_flag,type_id AS TypeID ");

            string commSql = " FROM t_unit WHERE customer_id=@CustomerID AND type_id=@TypeID AND Status=1 ";
            pagedSql.AppendFormat(commSql);
            totalSql.AppendFormat(commSql);

            List<SqlParameter> param = new List<SqlParameter>();
            param.Add(new SqlParameter("@CustomerID", this.CurrentUserInfo.CurrentUser.customer_id));
            param.Add(new SqlParameter("@TypeID", pTypeID));

            if (!string.IsNullOrEmpty(pUnitID))
            {
                totalSql.AppendFormat(" AND unit_id=@UnitID ");
                pagedSql.AppendFormat(" AND unit_id=@UnitID ");
                param.Add(new SqlParameter("@UnitID", pUnitID));
            }

            if (!string.IsNullOrEmpty(pUnitName))
            {
                totalSql.AppendFormat(" AND unit_name like @UnitName ");
                pagedSql.AppendFormat(" AND unit_name like @UnitName ");
                param.Add(new SqlParameter("@UnitName", "%" + pUnitName + "%"));
            }

            pagedSql.AppendFormat(") tt WHERE tt.rowid BETWEEN @begin AND @end ");
            //PageCount
            int totalCount = Convert.ToInt32(this.SQLHelper.ExecuteScalar(CommandType.Text, totalSql.ToString(), param.ToArray()));    //计算总行数
            int remainder = 0;
            totalPage = Math.DivRem(totalCount, pPageSize, out remainder);
            if (remainder > 0)
                totalPage++;

            param.Add(new SqlParameter("@begin", begin));
            param.Add(new SqlParameter("@end", end));
            DataSet ds = this.SQLHelper.ExecuteDataset(CommandType.Text, pagedSql.ToString(), param.ToArray());
            if (ds != null)
                return ds.Tables[0];
            return null;
        }
    }
}
