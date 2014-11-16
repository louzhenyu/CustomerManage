/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/5/7 11:10:05
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
    /// 表SysPage的数据访问类 
    /// TODO:
    /// 1.实现ICRUDable接口
    /// 2.实现IQueryable接口
    /// 3.实现Load方法
    /// </summary>
    public partial class SysPageDAO : BaseCPOSDAO, ICRUDable<SysPageEntity>, IQueryable<SysPageEntity>
    {
        /// <summary>
        /// 根据客户来获取配置页模板,如果客户ID为空,则获取所有
        /// </summary>
        /// <param name="pCustomerID"></param>
        /// <returns></returns>
        public SysPageEntity[] GetPagesByCustomerID(string pCustomerID)
        {
            List<SysPageEntity> list = new List<SysPageEntity> { };
            StringBuilder sub = new StringBuilder();
            List<SqlParameter> paras = new List<SqlParameter> { };
            if (!string.IsNullOrEmpty(pCustomerID))
            {
                sub.AppendFormat("and CustomerID=@CustomerID");
                paras.Add(new SqlParameter() { ParameterName = "@CustomerID", Value = pCustomerID });
            }
            string sql = string.Format("select * from vwPageInfo where isentrance = 1 and isdelete=0 {0}", sub);
            using (var rd = this.SQLHelper.ExecuteReader(CommandType.Text, sql, paras.ToArray()))
            {
                while (rd.Read())
                {
                    SysPageEntity m;
                    this.Load(rd, out m);
                    m.Node = rd["Node"].ToString();
                    m.NodeValue = rd["NodeValue"].ToString();
                    list.Add(m);
                }
            }
            return list.ToArray();
        }

        public SysPageEntity[] GetPageByID(Guid? pPageID)
        {
            List<SysPageEntity> list = new List<SysPageEntity> { };
            List<SqlParameter> paras = new List<SqlParameter> { };
            paras.Add(new SqlParameter() { ParameterName = "@PageID", Value = pPageID });
            string sql = "select * from vwPageInfo where isdelete=0 and PageID=@PageID";
            using (var rd = this.SQLHelper.ExecuteReader(CommandType.Text, sql, paras.ToArray()))
            {
                while (rd.Read())
                {
                    SysPageEntity m;
                    this.Load(rd, out m);
                    m.Node = rd["Node"].ToString();
                    m.NodeValue = rd["NodeValue"].ToString();
                    list.Add(m);
                }
            }
            return list.ToArray();
        }
        /// <summary>
        /// 判断数据库中书否存在PageKey
        /// </summary>
        /// <param name="pPageKey"></param>
        /// <returns></returns>
        public bool GetExistsPageKey(string pPageKey)
        {
            StringBuilder sbSQL = new StringBuilder();
            sbSQL.AppendLine(string.Format("select * from SysPage where PageKey='{0}'", pPageKey));
            DataSet ds = this.SQLHelper.ExecuteDataset(sbSQL.ToString());
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                return true;
            }
            else return false;
        }

        /// <summary>
        /// 判断数据库中书否存在PageKey
        /// </summary>
        /// <param name="pPageKey"></param>
        /// <returns></returns>
        public bool GetExistsPageKey(string pPageKey,string PageId)
        {
            StringBuilder sbSQL = new StringBuilder();
            sbSQL.AppendLine(string.Format("select * from SysPage where 1=1 and IsDelete=0 and  PageKey='{0}' and PageID <> '{1}'", pPageKey, PageId));
            DataSet ds = this.SQLHelper.ExecuteDataset(sbSQL.ToString());
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                return true;
            }
            else return false;
        }
        /// <summary>
        /// 获取模板页列表 Add by changjian.tian
        /// </summary>
        /// <param name="PageId"></param>
        /// <returns></returns>
        public DataSet GetSysPageList(string pKey, string pName, int pPageIndex, int pPageSize, string pCustomerId)
        {
            StringBuilder temp = new StringBuilder();
            if (!string.IsNullOrWhiteSpace(pKey))
            {
                temp.AppendFormat(" and  PageKey like '%{0}%'", pKey);
            }
            if (!string.IsNullOrWhiteSpace(pName))
            {
                temp.AppendFormat(" and  ModuleName like '%{0}%' ", pName);
            }
            if (!string.IsNullOrWhiteSpace(pCustomerId))
            {
                temp.AppendFormat(" and ((CustomerId='{0}') or (CustomerId Is NULL))", pCustomerId);
            }
            StringBuilder sql = new StringBuilder(string.Format(@"select * from 
(select row_number()over( order by  [LastUpdateTime] desc) as _row,* from [SysPage] where 1=1  and isdelete=0 " + temp + ") as   t where t._row>{0}*{1} and t._row<= ({0}+1)*{1};", pPageIndex, pPageSize, temp.ToString()));
            sql.AppendLine("select * from SysPage  where 1=1  and isdelete=0 " + temp + " ");
            var ds = this.SQLHelper.ExecuteDataset(sql.ToString());
            return ds;
        }

        /// <summary>
        /// 获取模板客户化配置 Add by changjian.tian
        /// </summary>
        /// <param name="PageId"></param>
        /// <returns></returns>
        public DataSet GetCustomerPageSetting(string pKey, string pCustomerId)
        {
            StringBuilder temp = new StringBuilder();
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select distinct PageID,JsonValue,ModuleName,Version,Author,LastUpdateTime from vwPageInfo where PageKey='{0}' and CustomerID='{1}' order by LastUpdateTime desc ;", pKey, pCustomerId);
            sql.AppendFormat("select * from vwPageInfo where PageKey='{0}' and CustomerID='{1}' order by Node", pKey, pCustomerId);
            var ds = this.SQLHelper.ExecuteDataset(sql.ToString());
            return ds;
        }

        /// <summary>
        /// 设置模板客户化配置 Add by changjian.tian
        /// </summary>
        /// <param name="pCustomerId"></param>
        /// <param name="pMappingId"></param>
        /// <param name="pPageKey"></param>
        /// <param name="pNode"></param>
        /// <param name="pNodeValue"></param>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public int SetCustomerPageSetting(string pCustomerId, string pMappingId, string pPageKey, string pNode, string pNodeValue, string pUserID)
        {
            SqlParameter[] pars = new SqlParameter[] { 
                new SqlParameter("@CustomerID",pCustomerId),
                new SqlParameter("@MappingId",pMappingId),
                new SqlParameter("@PageKey",pPageKey),
                new SqlParameter("@Node",pNode),
                new SqlParameter("@NodeValue",pNodeValue),
                new SqlParameter("@UserID",pUserID),
            };
            int result = this.SQLHelper.ExecuteNonQuery(CommandType.StoredProcedure, "spCustomerPageSetting", pars);
            return result;
        }

        /// <summary>
        /// 获取客户配置列表 Add by changjian.tian
        /// </summary>
        /// <param name="PageId"></param>
        /// <returns></returns>
        public DataSet GetCustomerPageList(string pKey, string pName, int pPageIndex, int pPageSize, string pCustomerId)
        {
            StringBuilder temp = new StringBuilder();
            if (!string.IsNullOrWhiteSpace(pKey))
            {
                temp.AppendFormat(" and  PageKey like '%{0}%'", pKey);
            }
            if (!string.IsNullOrWhiteSpace(pName))
            {
                temp.AppendFormat(" and  ModuleName like '%{0}%'", pName);
            }

//            StringBuilder sql = new StringBuilder(string.Format(@"select distinct PageID,MappingID,Version,LastUpdateTime,PageKey,Title,ModuleName,IsEntrance,URLTemplate,JsonValue from 
//(select row_number()over( order by  [PageID] desc) as _row,* from vwPageInfo where 1=1   and (CustomerId='" + pCustomerId + "')     and isdelete=0  " + temp + ") as   t where t._row>{0}*{1} and t._row<= ({0}+1)*{1};", pPageIndex, pPageSize, temp.ToString()));
//            sql.AppendLine("select distinct PageID,Version,LastUpdateTime,MappingID,PageKey,Title,ModuleName, IsEntrance,URLTemplate,JsonValue from vwPageInfo where 1=1   and (CustomerId='" + pCustomerId + "')   and isdelete=0 " + temp + "");

            StringBuilder sql = new StringBuilder(string.Format(@" select * into  #temp	 from
  (
 select distinct PageID,MappingID,Version,LastUpdateTime,PageKey,Title,
 ModuleName,IsEntrance,URLTemplate,JsonValue,CustomerId from 
( select 
 PageID,MappingID,Version,LastUpdateTime,PageKey,Title,
 ModuleName,IsEntrance,URLTemplate,JsonValue,CustomerID from vwPageInfo where 1=1   and (CustomerId='" + pCustomerId + "')    and isdelete=0 "+temp+" ) as   t) a ;"));
            sql.Append(string.Format(" select * from  (select row_number()over( order by LastUpdateTime desc) _row, * from #temp) t where  t._row>{0}*{1} and t._row<= ({0}+1)*{1}; ",pPageIndex, pPageSize));
            sql.Append(" select * from #temp; ");
            sql.Append("drop table #temp");
            var ds = this.SQLHelper.ExecuteDataset(sql.ToString());
            return ds;
        }
        /// <summary>
        /// 获取全部客户。用于生成全部客户的Config文件
        /// </summary>
        /// <returns></returns>
        public DataSet GetCustomerInfo()
        {
            StringBuilder sql = new StringBuilder(" select customer_id from t_customer_connect intersect select customer_id from t_customer");
            var ds = this.SQLHelper.ExecuteDataset(sql.ToString());
            return ds;
        }
        /// <summary>
        /// 获取客户可配置Config文件
        /// </summary>
        /// <param name="PageId"></param>
        /// <returns></returns>
        public DataSet GetCreateCustomerConfig(string pCustomerId)
        {
            StringBuilder temp = new StringBuilder();
            if (!string.IsNullOrWhiteSpace(pCustomerId))
            {
                temp.AppendFormat(" and CustomerId='{0}'", pCustomerId);
            }
            //StringBuilder sbsql = new StringBuilder("select * from vwPageInfo where 1=1 and Node=1  " + temp + ";");
            //sbsql.Append("select * from vwPageInfo where 1=1 and Node=2  " + temp + ";");
            //sbsql.Append("select * from vwPageInfo where 1=1 and Node=3  " + temp + ";");
            //sbsql.Append(" select distinct PageID,MappingID,Version,LastUpdateTime,PageKey,Title,ModuleName,IsEntrance,URLTemplate,JsonValue,CustomerId from ( select PageID,MappingID,Version,LastUpdateTime,PageKey,Title,ModuleName,IsEntrance,URLTemplate,JsonValue,CustomerID from vwPageInfo where 1=1  "+temp+")    and isdelete=0  ) as   t");

            StringBuilder sbsql = new StringBuilder("select distinct PageKey,Title,ModuleName,IsEntrance,JsonValue,DefaultHtml,CustomerID,Node,NodeValue from vwPageInfo where 1=1  " + temp + ";");
            sbsql.Append(" select distinct PageKey,Title,ModuleName,IsEntrance,JsonValue,DefaultHtml,CustomerID from vwPageInfo where 1=1 " + temp + " ");
            var ds = this.SQLHelper.ExecuteDataset(sbsql.ToString());
            return ds;
        }
        /// <summary>
        /// 获取客户可配置Config文件
        /// </summary>
        /// <param name="PageId"></param>
        /// <returns></returns>
        public DataSet GetCreateCustomerVersion(string pCustomerId)
        {
            StringBuilder temp = new StringBuilder();
            if (!string.IsNullOrWhiteSpace(pCustomerId))
            {
                temp.AppendFormat(" and CustomerId='{0}'", pCustomerId);
            }
            StringBuilder sbsql = new StringBuilder(" select * from CustomerBasicSetting where 1=1 and IsDelete=0 and SettingCode in ('AJAX_PARAMS','APP_JSLIB','APP_TYPE','APP_DEBUG','APP_OPTION_MENU','APP_TOOL_BAR','APP_CACHE','ForwardingMessageTitle','ForwardingMessageSummary','ForwardingMessageLogo')" + temp + ";");
            sbsql.Append("select top 1 (case when(T.WeiXinTypeId=1) then 'SUBSCIBE' when (T.WeiXinTypeId=2) then 'SUBSCIBE' when (T.WeiXinTypeId=3) then 'SERVICE' end) as APP_TYPE from cpos_ap..TCustomerWeiXinMapping T left join WeiXinType W on t.WeiXinTypeId=w.WeiXinTypeId where CustomerId='" + pCustomerId + "'");
            var ds = this.SQLHelper.ExecuteDataset(sbsql.ToString());
            return ds;
        }


        /// <summary>
        /// 获取套餐列表 add by changjian.tian 2014-05-21
        /// </summary>
        /// <param name="PageId"></param>
        /// <returns></returns>
        public DataSet GetVocationVersionMappingList(int pPageIndex,int pPageSize)
        {
            StringBuilder temp = new StringBuilder();
            StringBuilder sql = new StringBuilder(string.Format(@"select VocaVerMappingID,VocationDesc,VersionDesc from
                 (
                 select row_number()over( order by  VocaVerMappingID  desc) as _row, VocaVerMappingID,VocationDesc,VersionDesc from                    SysVocationVersionMapping	s
                 left join	SysVocation	
                 s1 on s.VocationID=s1.VocationID left join 
                 SysVersion	 s2  on s.VersionID=s2.VersionID
                 ) as t  where t._row>{0}*{1} and t._row<= ({0}+1)*{1};", pPageIndex, pPageSize));
            sql.AppendLine(@"select VocaVerMappingID,s1.VocationDesc,s2.VersionDesc, * from   SysVocationVersionMapping	s
                           left join	SysVocation	
                           s1 on s.VocationID=s1.VocationID left join 
                           SysVersion	 s2
                           on s.VersionID=s2.VersionID");
            var ds = this.SQLHelper.ExecuteDataset(sql.ToString());
            return ds;
        }
    }
}
