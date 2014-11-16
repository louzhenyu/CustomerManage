using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.BS.DataAccess
{
    /// <summary>
    /// 公共方法
    /// </summary>
    public class PublicService
    {
        #region 拼接字符窜
        /// <summary>
        /// 拼接字符窜
        /// </summary>
        /// <param name="sql">sql主语句</param>
        /// <param name="obj">查询字段</param>
        /// <param name="objValue">查询字段对应值</param>
        /// <param name="Symbol">符号</param>
        /// <returns></returns>
        public string GetLinkSql(string sql, string obj, string objValue, string Symbol)
        {
            if (objValue != null && !objValue.Equals(""))
            {
                switch (Symbol){
                    case "%":
                        sql = sql + " and " + obj + " like  '%' + '" + objValue + "' + '%'";
                        break;
                    case "=":
                        sql = sql + " and " + obj + " = '" + objValue + "'";
                        break;
                    case ">=":
                        sql = sql + " and " + obj + " >= '" + objValue + "'";
                        break;
                    case ">":
                        sql = sql + " and " + obj + " > '" + objValue + "'";
                        break;
                    case "<=":
                        sql = sql + " and " + obj + " <= '" + objValue + "'";
                        break;
                    case "<":
                        sql = sql + " and " + obj + " < '" + objValue + "'";
                        break;
                    case "!=":
                        sql = sql + " and " + obj + " != '" + objValue + "'";
                        break;
                    default:
                        break;
                }
                
            }
            return sql;
        }

        /// <summary>
        /// 拼接修改的sql（不是null）
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="obj"></param>
        /// <param name="objValue"></param>
        /// <param name="Symbol"></param>
        /// <returns></returns>
        public string GetIsNotNullUpdateSql(string sql, string obj, string objValue)
        {
            if (objValue != null)
            {
                sql = sql + " , " + obj + " = '" + objValue + "'";
            }
            return sql;
        }
        #endregion
    }
}
