/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/5/30 16:00:39
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
    /// 表MarketPerson的数据访问类 
    /// TODO:
    /// 1.实现ICRUDable接口
    /// 2.实现IQueryable接口
    /// 3.实现Load方法
    /// </summary>
    public partial class MarketPersonDAO : Base.BaseCPOSDAO, ICRUDable<MarketPersonEntity>, IQueryable<MarketPersonEntity>
    {
        #region 获取活动人群信息
        public int GetMarketPersonByEventIDCount(string EventID)
        {
            string sql = GetMarketPersonByEventIDSql(EventID);
            sql += "select count(*) From #tmp";
            return Convert.ToInt32(this.SQLHelper.ExecuteScalar(sql));
        }
        public DataSet GetMarketPersonByEventID(string EventID, int Page, int PageSize)
        {
            int beginSize = Page * PageSize;
            int endSize = Page * PageSize + PageSize;
            DataSet ds = new DataSet();
            string sql = GetMarketPersonByEventIDSql(EventID);
            sql += " select * From #tmp a where 1=1 and a.displayindex between '" +
                beginSize + "' and '" + endSize + "' order by  a.displayindex ";
            ds = this.SQLHelper.ExecuteDataset(sql);
            return ds;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="EventID">活动标识</param>
        /// <returns></returns>
        private string GetMarketPersonByEventIDSql(string EventID)
        {
            string sql = "SELECT b.*,DisplayIndex = row_number() over(order by a.CreateTime desc)  "
                       + " FROM dbo.MarketPerson a INNER JOIN dbo.Vip b ON(a.VIPID = b.VIPID) WHERE a.IsDelete = 0 AND a.MarketEventID = '" + EventID + "' ;";
            return sql;
        }
        #endregion


        #region 获取列表
        /// <summary>
        /// 获取列表数量
        /// </summary>
        public int GetListCount(MarketPersonEntity entity)
        {
            string sql = GetListSql(entity);
            sql = sql + " select count(*) as icount From #tmp; ";
            return Convert.ToInt32(this.SQLHelper.ExecuteScalar(sql));
        }
        /// <summary>
        /// 获取列表
        /// </summary>
        public DataSet GetList(MarketPersonEntity entity, int Page, int PageSize)
        {
            int beginSize = Page * PageSize + 1;
            int endSize = Page * PageSize + PageSize;
            DataSet ds = new DataSet();
            string sql = GetListSql(entity);
            sql += " select * From #tmp a where 1=1 and a.displayindex between '" +
                beginSize + "' and '" + endSize + "' order by  a.displayindex ";
            ds = this.SQLHelper.ExecuteDataset(sql);
            return ds;
        }
        private string GetListSql(MarketPersonEntity entity)
        {
            string sql = string.Empty;
            sql = "SELECT a.MarketPersonID,a.MarketEventID, b.* ";
            sql += " ,DisplayIndex = row_number() over(order by a.CreateTime desc) ";
            sql += " into #tmp ";
            sql += " from MarketPerson a ";
            sql += " inner join Vip b on a.VipID=b.VipID ";
            sql += " where a.IsDelete='0' ";
            if (entity.MarketEventID != null && entity.MarketEventID.Trim().Length > 0)
            {
                sql += " and a.MarketEventID = '" + entity.MarketEventID + "' ";
            }
            if (entity.MarketPersonID != null && entity.MarketPersonID.Trim().Length > 0)
            {
                sql += " and a.MarketPersonID = '" + entity.MarketPersonID + "' ";
            }
            sql += " order by a.CreateTime desc ";
            return sql;
        }

        public string GetPersonListSql(MarketPersonEntity entity)
        {
            string sql = "SELECT * FROM ( "
                    + " SELECT  a.MarketPersonID,a.MarketEventID, b.*  "
                    + " ,CASE WHEN b.Gender = '1' THEN '男' ELSE '女' END GenderInfo "
                    + " ,(SELECT AnswerID FROM MarketQuesAnswer x WHERE x.MarketEventID='4' AND QuestionID = '41' AND x.OpenID = b.WeiXinUserId) UserName "
                    + " ,(SELECT AnswerID FROM MarketQuesAnswer x WHERE x.MarketEventID='4' AND QuestionID = '42' AND x.OpenID = b.WeiXinUserId) Enterprice "
                    + " ,(SELECT y.OptionsText FROM MarketQuesAnswer x INNER JOIN dbo.QuesOption y  "
                    + " ON(x.AnswerID =y.OptionsID)  WHERE x.MarketEventID='4' AND x.QuestionID = '43' AND x.OpenID = b.WeiXinUserId) IsChainStores "
                    + " ,(SELECT y.OptionsText FROM MarketQuesAnswer x INNER JOIN dbo.QuesOption y  "
                    + " ON(x.AnswerID =y.OptionsID)  WHERE x.MarketEventID='4' AND x.QuestionID = '44' AND x.OpenID = b.WeiXinUserId) IsWeiXinMarketing "
                    + " FROM dbo.MarketPerson a "
                    + " INNER JOIN dbo.Vip b ON (a.VIPID = b.VIPID) "
                    + " WHERE a.IsDelete = 0 AND b.IsDelete = 0 "
                    + " AND a.MarketEventID = '"+entity.MarketEventID+"' "
                    + " ) x WHERE 1=1 "
                    + " ";

            return sql;
        }
        #endregion

        public void WebDelete(MarketPersonEntity entity)
        {
            string sql = "";
            sql += " delete MarketPerson where 1=1 ";
            sql += " and MarketEventID='" + entity.MarketEventID + "' ";

            if (entity.MarketPersonID != null && entity.MarketPersonID.Length > 0)
            {
                sql += " and MarketStoreID='" + entity.MarketPersonID + "' ";
            }
            this.SQLHelper.ExecuteScalar(sql);
        }

        #region 获取活动人群发布集合
        public DataSet GetPersonPushInfo(string EventID)
        {
            DataSet ds = new DataSet();
            string sql = "SELECT distinct b.WeiXinUserId OpenID,b.VIPID,isnull(c.TemplateContent,'') TemplateContent,isnull(c.TemplateContentSMS,'') TemplateContentSMS,b.VipName,isnull(b.Phone,'') Phone,(SELECT BrandName FROM MarketBrand x WHERE x.BrandID = c.brandid) Brand FROM dbo.MarketPerson a "
                        + " INNER JOIN dbo.Vip b ON(a.VIPID = b.VIPID) "
                        + " INNER JOIN dbo.MarketEvent c ON(a.MarketEventID = c.MarketEventID) "
                        //+ " INNER JOIN dbo.MarketTemplate d ON(c.TemplateID = d.TemplateID) "
                        + " WHERE a.IsDelete = '0' AND b.IsDelete = '0' AND c.IsDelete = '0'  " //AND d.IsDelete = '0'
                        + " AND c.MarketEventID = '"+EventID+"'";
            ds = this.SQLHelper.ExecuteDataset(sql);
            return ds;
        }
        #endregion

        public int GetMarketPersonByEventID(string EventID)
        {
            string sql = "SELECT count(*) FROM dbo.MarketPerson WHERE IsDelete = 0 AND MarketEventID = '" + EventID + "'";
            return Convert.ToInt32(this.SQLHelper.ExecuteScalar(sql));
        }

        /// <summary>
        /// 获取发送消息人数
        /// </summary>
        public int GetMarketPersonSendCount(string EventID, int type)
        {
            string sql = "SELECT count(*) FROM MarketPerson a ";
            sql += " inner join vw_vipCenterInfo b on a.vipId=b.vipId ";
            sql += " WHERE a.IsDelete = 0 AND a.MarketEventID = '" + EventID + "'";
            if (type == 1) sql += " and b.isWXPush='1'";
            else if (type == 2) sql += " and b.isSMSPush='1'";
            else if (type == 3) sql += " and b.isAppPush='1'";
            return Convert.ToInt32(this.SQLHelper.ExecuteScalar(sql));
        }
    }
}
