/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/11/25 15:17:08
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
    /// 表WUserMessage的数据访问类 
    /// TODO:
    /// 1.实现ICRUDable接口
    /// 2.实现IQueryable接口
    /// 3.实现Load方法
    /// </summary>
    public partial class WUserMessageDAO : Base.BaseCPOSDAO, ICRUDable<WUserMessageEntity>, IQueryable<WUserMessageEntity>
    {
        #region 消息同步
        /// <summary>
        /// 公共查询sql
        /// </summary>
        /// <param name="queryInfo"></param>
        /// <returns></returns>
        public string GetClientUserMessageListSql(WUserMessageEntity queryInfo, int AppendType)
        {
            string ascOrDesc = string.Empty;
            string ascOrDescInt = string.Empty;
            if (queryInfo.DisplayIndexLast == -1)
            {
                ascOrDesc = "desc";
                ascOrDescInt = "+";
            }
            else
            {
                if (AppendType == 1)
                {
                    ascOrDesc = "desc";
                    ascOrDescInt = "-";
                }
                else
                {
                    ascOrDesc = "asc";
                    ascOrDescInt = "+";
                }
            }

            //if (queryInfo.DisplayIndexLast == null)
            //    queryInfo.DisplayIndexLast = -1;
            string sql = string.Empty;
            #region
            sql += " select top " + queryInfo.PageSize + " a.* ";
            //sql += " ,DisplayIndexByTime = (row_number() over(order by a.lastUpdateTime desc)) ";
            //sql += " ,DisplayIndexLast = (row_number() over(order by a.lastUpdateTime desc))+" + queryInfo.DisplayIndexLast;
            sql += " into #tmp from (select a.* ";
            sql += " ,u.user_Name CreateByName";
            sql += " ,v.vipName VipName";
            sql += " ,v.HeadImgUrl HeadImgUrl";
            //sql += " ,(SELECT dbo.DateToTimestamp(a.lastUpdateTime)) timestampValue";
            sql += " ,CONVERT(NVARCHAR(200), dbo.DateToTimestamp(a.CreateTime)) timestamp ";
            sql += " ,DisplayIndex  = convert(int, " + ascOrDescInt + "row_number() over(order by a.CreateTime " + ascOrDesc + ")) + " + queryInfo.DisplayIndexLast + "";
            sql += " from WUserMessage a";
            //sql += " inner join VipUnitMapping b on a.VIPID=b.VIPID";
            sql += " left join t_user u on a.createBy=u.user_id";
            sql += " left join vip v on a.vipId=v.vipId";
            sql += " where 1=1 ";
            if (queryInfo.DataFrom != null)
            {
                sql += string.Format(" and a.DataFrom='{0}'", queryInfo.DataFrom);
            }
            if (queryInfo.ToVipType != null)
            {
                sql += string.Format(" and a.ToVipType='{0}'", queryInfo.ToVipType);
            }
            //sql += string.Format(" and b.vip='{0}'", queryInfo.UnitId);
            //sql += string.Format(" and (a.lastUpdateTime > (SELECT dbo.TimestampToDate('{0}')) )", queryInfo.timestamp);

            if (queryInfo.DisplayIndexLast != -1)
            {
                if (AppendType == 1)
                {
                    if (!queryInfo.timestamp.Equals("0"))
                    {
                        sql += " and dbo.DateToTimestamp(a.CreateTime) < '" + queryInfo.timestamp + "'";
                    }
                }
                else { sql += " and dbo.DateToTimestamp(a.CreateTime) > '" + queryInfo.timestamp + "'"; }
            }

            sql += " ) a;";

            #endregion
            return sql;
        }

        /// <summary>
        /// 帖子评论列表获取
        /// </summary>
        public DataSet GetClientUserMessageList(WUserMessageEntity queryInfo, int AppendType)
        {
            if (queryInfo.PageSize == 0) queryInfo.PageSize = 20;
            int beginSize = queryInfo.Page * queryInfo.PageSize + 1;
            int endSize = queryInfo.Page * queryInfo.PageSize + queryInfo.PageSize;
            if (queryInfo.timestamp == null || queryInfo.timestamp == "") queryInfo.timestamp = "0";
            //if (queryInfo.displayIndexLast != null && queryInfo.displayIndexLast.Length > 0)
            //{
            //    beginSize = Convert.ToInt32(queryInfo.displayIndexLast);
            //    endSize = beginSize + queryInfo.PageSize;
            //}
            DataSet ds = new DataSet();
            string sql = GetClientUserMessageListSql(queryInfo, AppendType);
            //sql = sql + "select * From #tmp a where 1=1 and a.DisplayIndexByTime between '" + beginSize + "' and '" + endSize + "' order by a.DisplayIndexByTime";
            sql = sql + "select * From #tmp a where 1=1 order by a.createtime ";
            ds = this.SQLHelper.ExecuteDataset(sql);
            return ds;
        }

        /// <summary>
        /// 获取查询总数量
        /// </summary>
        /// <param name="queryInfo"></param>
        /// <returns></returns>
        public int GetClientUserMessageListCount(WUserMessageEntity queryInfo, int AppendType)
        {
            string sql = GetClientUserMessageListSql(queryInfo, AppendType);
            sql = sql + " select count(*) as icount From #tmp; ";
            return Convert.ToInt32(this.SQLHelper.ExecuteScalar(sql));

        }
        #endregion

        public DataSet GetActiveUserMessageList()
        {
            string sql = @"select a.* from wUserMessage a inner join (
select vipID,max(createtime) as createTime from wUserMessage 
where datediff(hour,createtime,getdate())<=48 and vipid<>''
group by vipid
) b on a.vipid=b.vipid and a.createtime=b.createtime";
            return SQLHelper.ExecuteDataset(sql);
        }
    }
}
