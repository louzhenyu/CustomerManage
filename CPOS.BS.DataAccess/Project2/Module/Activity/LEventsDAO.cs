/*
 * Author		:Jack.Chen
 * EMail		:jack.chen@jitmarketing.cn
 * Company		:JIT
 * Create On	:2014/4/17 15:42:28
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
    /// 表LEvents的数据访问类     
    /// 1.实现ICRUDable接口
    /// 2.实现IQueryable接口
    /// 3.实现Load方法
    /// </summary>
    public partial class LEventsDAO
    {
        /// <summary>
        /// 获取泸州老窖客户的首页活动列表 
        /// </summary>
        /// <returns></returns>
        public LEventsEntity[] GetHomePageActivityList(int pPageIndex, int pPageSize)
        {
            StringBuilder sql = new StringBuilder();
            List<SqlParameter> ps = new List<SqlParameter>();
            sql.Append("select * from (select row_number()over(order by createtime desc) as _row, * from LEvents where 1=1 and isdelete=0 and isdefault=1");
            //1
            sql.Append(" and customerid=@customerid ) a ");
            sql.AppendFormat(" where _row>={0} and _row<={1}", pPageIndex * pPageSize + 1, (pPageIndex + 1) * pPageSize);
            ps.Add(new SqlParameter() { ParameterName = "@customerid", Value = this.CurrentUserInfo.ClientID });
            //2
            sql.Append(" order by createtime desc");
            List<LEventsEntity> list = new List<LEventsEntity>();
            using (var rdr = this.SQLHelper.ExecuteReader(CommandType.Text, sql.ToString(), ps.ToArray()))
            {
                while (rdr.Read())
                {
                    LEventsEntity m;
                    this.Load(rdr, out m);
                    list.Add(m);
                }
            }
            //
            return list.ToArray();
        }

        public DataSet GetEventList(string customerId, string eventTypeId, string eventName,int drawMethodId, bool? beginFlag,
            bool? endFlag,int eventStatus, int pPageIndex, int pPageSize)
        {
            var sqlWhere = new StringBuilder();


            sqlWhere.Append(" and 1 = 1 and IsCTW=0");

            if (!string.IsNullOrEmpty(beginFlag.ToString()))
            {
                sqlWhere.Append(beginFlag == true ? " and a.beginTime<=getdate()" : " and a.beginTime>getdate()");
            }


            if (!string.IsNullOrEmpty(endFlag.ToString()))
            {
                sqlWhere.Append(endFlag ==true ? " and a.endTime <getdate()" : " and a.endTime >getdate()");
            }
            

            if (!string.IsNullOrEmpty(eventName))
            {
                eventName = "%" + eventName + "%";
                sqlWhere.Append(" and a.Title like @pEventName ");
            }

            if (eventStatus != 0)
            {
                sqlWhere.Append(" and eventStatus = @pEventStatus");
            }
            //var sqlDrawJoin = new StringBuilder();
            
            var sql = new StringBuilder();

            sql.Append("select * from (");
            sql.Append(
                "select  row_number()over(order by a.beginTime desc) as _row,a.cityId,isnull(a.EventStatus,10) as EventStatus,'' DrawMethodName," +
                "a.EventId,isnull(a.Title,'') Title,isnull(a.EventTypeId,0) EventTypeId,isnull(b.GenreName,'') as EventTypeName," +
                "isnull(BeginTime,null) BeginTime,isnull(EndTime,'') EndTime");
            sql.Append(
                " ,EventStatusName = case a.EventStatus when 10 then '未开始' when 20 then '运行中' when 30 then '暂停' when 40 then '结束' end ");
            sql.Append(" from Levents a left join LEventsGenre b on a.EventGenreId = b.EventGenreId ");
            //sql.Append(" left join LEventDrawMethodMapping c on a.eventId = c.EventId and c.isdelete = 0");
            //sql.Append(" left join LEventDrawMethod d on c.DrawMethodId = d.DrawMethodID and d.isdelete = 0");

           // sql.Append(sqlDrawJoin);
            sql.Append(" where ");
            sql.Append(" a.customerId = @pCustomerId");
            sql.Append(" and a.isdelete =0 ");
           // sql.Append(" and (a.EventTypeId = @pEventTypeId or isnull(@pEventTypeId,'') = '')");
            //sql.Append(" and (d.DrawMethodId = @pDrawMethodId or @pDrawMethodId = 0)");
            sql.Append(sqlWhere);

            sql.Append(") t");

            sql.AppendFormat(" where _row>={0} and _row<={1}"
                , pPageIndex * pPageSize + 1, (pPageIndex + 1) * pPageSize);

            var paras = new List<SqlParameter>
            {
                new SqlParameter {ParameterName = "@pCustomerId", Value = customerId},
                new SqlParameter {ParameterName = "@pEventTypeId", Value = eventTypeId},
                new SqlParameter {ParameterName = "@pEventName", Value = eventName},
                new SqlParameter {ParameterName = "@pEventStatus", Value = eventStatus},
                new SqlParameter {ParameterName = "@pDrawMethodId", Value = drawMethodId}


            };

            return this.SQLHelper.ExecuteDataset(CommandType.Text, sql.ToString(), paras.ToArray());

        }


        public int GetEventListCount(string customerId, string eventTypeId, string eventName, int drawMethodId, bool? beginFlag,
          bool? endFlag, int eventStatus)
        {
            var sqlWhere = new StringBuilder();


            sqlWhere.Append(" and 1 = 1 ");

            if (!string.IsNullOrEmpty(beginFlag.ToString()))
            {
                sqlWhere.Append(beginFlag == true ? " and a.beginTime<=getdate()" : " and a.beginTime>getdate()");
            }


            if (!string.IsNullOrEmpty(endFlag.ToString()))
            {
                sqlWhere.Append(endFlag == true ? " and a.endTime <getdate()" : " and a.endTime >getdate()");
            }


            if (!string.IsNullOrEmpty(eventName))
            {
                eventName = "%" + eventName + "%";
                sqlWhere.Append(" and a.Title like @pEventName ");
            }

            if (eventStatus != 0)
            {
                sqlWhere.Append(" and eventStatus = @pEventStatus");
            }
            var sqlDrawJoin = new StringBuilder();

            if (!string.IsNullOrEmpty(drawMethodId.ToString()))
            {
               
                
            }
            var sql = new StringBuilder();

           
             
            sql.Append(
                " select isnull(count(1),null) as num ");
            //sql.Append(" from Levents a inner join LEventsGenre b on a.EventGenreId = b.EventGenreId ");
            sql.Append(" from Levents a left join LEventsGenre b on a.EventGenreId = b.EventGenreId ");
            sql.Append(" where ");
            sql.Append(" a.customerId = @pCustomerId");
            sql.Append(" and a.isdelete =0 and IsCTW=0 ");
            //sql.Append(" inner join LEventDrawMethodMapping c on a.eventId = c.EventId");
            //sql.Append(" inner join LEventDrawMethod d on c.DrawMethodId = d.DrawMethodID");

            //sql.Append(" where ");
            //sql.Append(" a.customerId = @pCustomerId");
            //sql.Append(" and (b.EventTypeId = @pEventTypeId or isnull(@pEventTypeId,'') = '')");
            //sql.Append(" and c.isdelete = 0 and d.isdelete = 0 and (d.DrawMethodId = @pDrawMethodId or @pDrawMethodId = 0) ");

            sql.Append(sqlWhere);

            var paras = new List<SqlParameter>
            {
                new SqlParameter {ParameterName = "@pCustomerId", Value = customerId},
                new SqlParameter {ParameterName = "@pEventTypeId", Value = eventTypeId},
                new SqlParameter {ParameterName = "@pEventName", Value = eventName},
                new SqlParameter {ParameterName = "@pEventStatus", Value = eventStatus},
                new SqlParameter {ParameterName = "@pDrawMethodId", Value = drawMethodId}


            };

            return Convert.ToInt32(this.SQLHelper.ExecuteScalar(CommandType.Text, sql.ToString(), paras.ToArray()));

        }




        public string GetRecommendId(string customerId)
        {
            var sql = new StringBuilder();
            sql.Append(" select top(1) isnull(eventid,'') EventId from LEvents a ");
            sql.Append(" inner join LEventsGenre b on a.EventGenreId = b.EventGenreId");
            sql.Append(" where b.GenreCode = 'Online004' order by a.BeginTime desc");

            return this.SQLHelper.ExecuteScalar(sql.ToString()).ToString();
        }


        public int GetIsShareByEventId(string eventId)
        {
            var sql = new StringBuilder();
            sql.Append("select isshare from levents where eventid = @pEventId");

            var paras = new List<SqlParameter>
            {
                new SqlParameter() {ParameterName = "@pEventId", Value = eventId}
            };

            var result = this.SQLHelper.ExecuteScalar(CommandType.Text, sql.ToString(), paras.ToArray());

            if (result == null || string.IsNullOrEmpty(result.ToString()) || result.ToString() == "")
                return 0;
            else
                return Convert.ToInt32(result);
        }

        public string GetEnableFlagByEventId(string eventId)
        {
            var sql = new StringBuilder();
            sql.Append("select eventFlag from levents where eventid = @pEventId");

            var paras = new List<SqlParameter>
            {
                new SqlParameter() {ParameterName = "@pEventId", Value = eventId}
            };
            var result = this.SQLHelper.ExecuteScalar(CommandType.Text, sql.ToString(), paras.ToArray());
            if (result == null || string.IsNullOrEmpty(result.ToString()) || result.ToString() == "")
                return "0";
            else
                return result.ToString();
        }
    }
}
