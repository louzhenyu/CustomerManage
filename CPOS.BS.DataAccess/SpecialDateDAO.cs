/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2015-8-14 21:54:10
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
    /// 数据访问：  
    /// 表SpecialDate的数据访问类 
    /// TODO:
    /// 1.实现ICRUDable接口
    /// 2.实现IQueryable接口
    /// 3.实现Load方法
    /// </summary>
    public partial class SpecialDateDAO : Base.BaseCPOSDAO, ICRUDable<SpecialDateEntity>, IQueryable<SpecialDateEntity>
    {
        /// <summary>
        /// 获取特殊日期列表信息
        /// </summary>
        /// <param name="pWhereConditions">筛选条件</param>
        /// <param name="pOrderBys">排序</param>
        /// <returns></returns>
        public SpecialDateEntity[] GetSpecialDateList(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys)
        {
            //组织SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat(" select sd.*,h.HolidayName,h.BeginDate,h.EndDate from [SpecialDate] sd ");
            sql.AppendFormat(" left join [Holiday] h on h.HolidayID=sd.HolidayID ");
            sql.AppendFormat(" where 1=1  and sd.isdelete=0 ");
            if (pWhereConditions != null)
            {
                foreach (var item in pWhereConditions)
                {
                    sql.AppendFormat(" and {0}", item.GetExpression());
                }
            }
            if (pOrderBys != null && pOrderBys.Length > 0)
            {
                sql.AppendFormat(" order by ");
                foreach (var item in pOrderBys)
                {
                    sql.AppendFormat(" {0} {1},", item.FieldName, item.Direction == OrderByDirections.Asc ? "asc" : "desc");
                }
                sql.Remove(sql.Length - 1, 1);
            }
            //执行SQL
            List<SpecialDateEntity> list = new List<SpecialDateEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    SpecialDateEntity m;
                    this.Load(rdr, out m);
                    //假日信息
                    if (rdr["SpecialId"] != DBNull.Value)
                    {
                        m.HolidayName = rdr["HolidayName"].ToString();
                    }
                    if (rdr["BeginDate"] != DBNull.Value)
                    {
                        m.BeginDate =Convert.ToDateTime(rdr["BeginDate"].ToString());
                    }
                    if (rdr["SpecialId"] != DBNull.Value)
                    {
                        m.EndDate = Convert.ToDateTime(rdr["EndDate"].ToString());
                    }
                    list.Add(m);
                }
            }
            //返回结果
            return list.ToArray();
        }
    }
}
