/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/9/1 11:46:47
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
    /// 表Url的数据访问类 
    /// TODO:
    /// 1.实现ICRUDable接口
    /// 2.实现IQueryable接口
    /// 3.实现Load方法
    /// </summary>
    public partial class UrlDAO : Base.BaseCPOSDAO, ICRUDable<UrlEntity>, IQueryable<UrlEntity>
    {
        /// <summary>
        /// 根据长链接 获取 实例
        /// </summary>
        /// <param name="longUrl"></param>
        /// <returns></returns>
        public UrlEntity GetByLongUrl(string longUrl)
        {
            if (string.IsNullOrEmpty(longUrl)) return null;
            var sb = new StringBuilder();
            sb.AppendFormat(@"select * from [Url] where clientId='{1}' and LongUrl='{0}'", longUrl,this.CurrentUserInfo.ClientID);
            UrlEntity m = null;
            using(SqlDataReader sr = this.SQLHelper.ExecuteReader(sb.ToString()))
            {
                while (sr.Read())
                {
                    this.Load(sr, out m);
                    break;
                }
            }
            return m;
        }
        /// <summary>
        /// 根据短链接链接 获取 实例
        /// </summary>
        /// <param name="longUrl"></param>
        /// <returns></returns>
        public UrlEntity GetByShortUrl(string shortUrl)
        {
            if (string.IsNullOrEmpty(shortUrl)) return null;
            var sb = new StringBuilder();
            sb.AppendFormat(@"select * from [Url] where clientId='{1}' and ShortUrl='{0}'", shortUrl, this.CurrentUserInfo.ClientID);
            UrlEntity m = null;
            using (SqlDataReader sr = this.SQLHelper.ExecuteReader(sb.ToString()))
            {
                while (sr.Read())
                {
                    this.Load(sr, out m);
                    break;
                }
            }
            return m;
        }
        /// <summary>
        /// 根据短链接去实体集
        /// </summary>
        /// <param name="shortUrls"></param>
        /// <returns></returns>
        public UrlEntity[] GetUrlsByShortUrl(string[] shortUrls)
        {
            if (shortUrls.Length == 0) return null;
            var sb = new StringBuilder();
            sb.AppendFormat(@"select * from [Url] where clientId='{1}' and ShortUrl in ({0})", MapUrls2Sql(shortUrls), this.CurrentUserInfo.ClientID);
            List<UrlEntity> list = new List<UrlEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sb.ToString()))
            {
                while (rdr.Read())
                {
                    UrlEntity m;
                    this.Load(rdr, out m);
                    list.Add(m);
                }
            }
            return list.ToArray();
        }
        /// <summary>
        /// 根据链接字符串数组查找实体集
        /// </summary>
        /// <param name="longUrls"></param>
        /// <returns></returns>
        public UrlEntity[] GetUrlsByLongUrl(string[] longUrls)
        {
            if (longUrls.Length == 0) return null;
            var sb = new StringBuilder();
            sb.AppendFormat(@"select * from [Url] where LongUrl in ({0})", MapUrls2Sql(longUrls));
            List<UrlEntity> list = new List<UrlEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sb.ToString()))
            {
                while (rdr.Read())
                {
                    UrlEntity m;
                    this.Load(rdr, out m);
                    list.Add(m);
                }
            }
            return list.ToArray();
        }
        /// <summary>
        /// 根据链接字符串数组生成对应查找sql
        /// </summary>
        /// <param name="longUrls"></param>
        /// <returns></returns>
        private string MapUrls2Sql(string[] longUrls)
        {
            if (0 == longUrls.Length) return null;
            string sql = longUrls.ToJoinString("','");
            sql = "'" + sql + "'";
            return sql;
        }
    }
}
