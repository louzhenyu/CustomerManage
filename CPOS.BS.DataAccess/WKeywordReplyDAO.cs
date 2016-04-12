/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/9/3 9:52:34
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
    /// ���ݷ��ʣ�  
    /// ��WKeywordReply�����ݷ����� 
    /// TODO:
    /// 1.ʵ��ICRUDable�ӿ�
    /// 2.ʵ��IQueryable�ӿ�
    /// 3.ʵ��Load����
    /// </summary>
    public partial class WKeywordReplyDAO : Base.BaseCPOSDAO, ICRUDable<WKeywordReplyEntity>,
        IQueryable<WKeywordReplyEntity>
    {
        #region ��̨Web��ȡ�б�

        /// <summary>
        /// ��ȡ�б�����
        /// </summary>
        public int GetWebWKeywordReplyCount(WKeywordReplyEntity entity)
        {
            string sql = GetWebWKeywordReplySql(entity);
            sql = sql + " select count(*) as icount From #tmp; ";
            return Convert.ToInt32(this.SQLHelper.ExecuteScalar(sql));
        }

        /// <summary>
        /// ��ȡ�б�
        /// </summary>
        public DataSet GetWebWKeywordReply(WKeywordReplyEntity entity, int Page, int PageSize)
        {
            int beginSize = Page*PageSize + 1;
            int endSize = Page*PageSize + PageSize;
            DataSet ds = new DataSet();
            string sql = GetWebWKeywordReplySql(entity);
            sql += " select * From #tmp a where 1=1 and a.displayindexPage between '" +
                   beginSize + "' and '" + endSize + "' order by  a.displayindexPage ";
            ds = this.SQLHelper.ExecuteDataset(sql);
            return ds;
        }

        private string GetWebWKeywordReplySql(WKeywordReplyEntity entity)
        {
            string sql = string.Empty;
            sql = "select a.* ";
            sql += " ,DisplayIndexPage = row_number() over(order by a.CreateTime desc) ";
            sql += " ,b.User_Name CreateByName ";
            sql += " ,c.ModelName ModelName ";
            sql += " ,d.WeiXinName WeiXinName ";
            sql += " ,f.MaterialTypeName MaterialTypeName ";
            sql += " into #tmp ";
            sql += " from [WKeywordReply] a ";
            sql += " left join [t_user] b on a.createBy=b.User_Id ";
            sql += " left join [WModel] c on a.ModelId=c.ModelId ";
            sql += " inner join [WApplicationInterface] d on a.ApplicationId=d.ApplicationId ";
            sql += " left join [WMaterialType] f on c.MaterialTypeId=f.MaterialTypeId ";
            sql += " where a.IsDelete='0' and d.customerId = '" + this.CurrentUserInfo.CurrentUser.customer_id + "' ";
            if (entity.Keyword != null && entity.Keyword.Trim().Length > 0)
            {
                sql += " and a.Keyword like '%" + entity.Keyword.Trim() + "%' ";
            }
            if (entity.ModelId != null && entity.ModelId.Trim().Length > 0)
            {
                sql += " and a.ModelId = '" + entity.ModelId.Trim() + "' ";
            }
            if (entity.ApplicationId != null && entity.ApplicationId.Trim().Length > 0)
            {
                sql += " and a.ApplicationId = '" + entity.ApplicationId.Trim() + "' ";
            }
            return sql;
        }

        public bool IsExistKeyword(string ApplicationId, string Keyword, string ReplyId)
        {
            string sql = "select count(*) From WKeywordReply where 1=1 and Keyword = '" + Keyword + "'";
            sql += " and ApplicationId='" + ApplicationId + "' ";
            if (ReplyId != null && ReplyId.Trim().Length > 0)
            {
                sql += " and ReplyId != '" + ReplyId.Trim() + "' ";
            }
            int n = Convert.ToInt32(this.SQLHelper.ExecuteScalar(sql));
            return n > 0 ? true : false;
        }

        #endregion

        #region ͨ���ؼ��ֻ�ȡ�ز����ͺ��ز�ID

        /// <summary>
        /// ͨ���ؼ��ֻ�ȡ�ز����ͺ��ز�ID
        /// </summary>
        /// <param name="keyword">ģ��ID</param>
        /// <returns></returns>
        public DataSet GetMaterialByKeyword(string keyword)
        {
            string sql = " SELECT MaterialTypeId, MaterialId FROM dbo.WModel WHERE ModelId =   "
                         + " (SELECT ModelId FROM dbo.WKeywordReply WHERE Keyword = '" + keyword + "') ";

            return this.SQLHelper.ExecuteDataset(sql);
        }

        /// <summary>
        /// ͨ���ؼ��ֻ�ȡ��Ϣ
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="KeywordType">1=�ؼ��ֻظ� 2=��ע�ظ� 3=�Զ��ظ� 4=��ά��</param>
        /// <returns></returns>
        public DataSet GetMaterialByKeywordJermyn(string keyword,string weixinId,int KeywordType)
        {
            string sql = string.Empty;
            switch (KeywordType)
            { 
                case 1:
                    sql = "select top 1 a.* From WKeywordReply a "
                        + " inner join WApplicationInterface b  "
                        + " on(a.ApplicationId = b.ApplicationId) "
                        + " where a.IsDelete = '0' and b.IsDelete = '0' "
                        + " and b.WeiXinID = '"+weixinId+"' "
                        + " and a.KeywordType = '"+KeywordType+"' and ','+a.Keyword+',' like '%,"+keyword.Replace("'","''")+",%' "
                        + " order by a.DisplayIndex";
                    break;
                case 3:
                    sql = "select top 1 a.* From WKeywordReply a "
                        + " inner join WApplicationInterface b  "
                        + " on(a.ApplicationId = b.ApplicationId) "
                        + " where a.IsDelete = '0' and b.IsDelete = '0' "
                        + " and b.WeiXinID = '" + weixinId + "' "
                        + " and a.KeywordType = '" + KeywordType + "' "
                        + " order by a.DisplayIndex";
                    break;
                case 4:
                    sql = "select top 1 a.* From WKeywordReply a "
                        + " inner join WApplicationInterface b  "
                        + " on(a.ApplicationId = b.ApplicationId) "
                        + " where a.IsDelete = '0' and b.IsDelete = '0' "
                        + " and b.WeiXinID = '" + weixinId + "' "
                        + " and a.KeywordType = '" + KeywordType + "' and a.Keyword like '%" + keyword + "%' "
                        + " order by a.DisplayIndex";
                    break;
                default:
                    sql = "";
                    break;
            }

            //string sql = " SELECT MaterialTypeId, MaterialId FROM dbo.WModel WHERE ModelId =   "
            //    + " (SELECT ModelId FROM dbo.WKeywordReply WHERE Keyword = '" + keyword + "') ";

            return this.SQLHelper.ExecuteDataset(sql);
        }
        #endregion

        public DataSet GetKeyWordList(string applicationId, string keyword, int pageSize, int pageIndex)
        {
            var paras = new List<SqlParameter>
            {
                new SqlParameter() {ParameterName = "@pApplicationId", Value = applicationId}
            };
            var sqlWhere = new StringBuilder();
            if (!string.IsNullOrEmpty(keyword))
            {
                sqlWhere.Append(" and keyword like +'%' + @pKeyword + '%'");

                paras.Add(new SqlParameter() {ParameterName = "@pKeyword", Value = keyword});
            }
            var sql = new StringBuilder();
            sql.Append("select * from (");
            sql.Append(" select row_number() over( order by createTime desc) _row,keyword,replyId from WKeywordReply");
            sql.Append(" where isdelete = 0 and applicationId = @pApplicationId and KeywordType = 1");
            sql.Append(sqlWhere);
            sql.Append(") t");
            sql.AppendFormat(" where _row>={0} and _row<={1}"
                , pageIndex*pageSize + 1, (pageIndex + 1)*pageSize);

            return this.SQLHelper.ExecuteDataset(CommandType.Text, sql.ToString(), paras.ToArray());
        }

        public DataSet GetKeyWordListByReplyId(string replyId)
        {
            var paras = new List<SqlParameter>
            {
                new SqlParameter() {ParameterName = "@pReplyId", Value = replyId}
            };
            var sql = new StringBuilder();

            sql.Append(" select ReplyId,Keyword,ReplyType,text,ApplicationId,KeywordType,DisplayIndex,");
            sql.Append(" BeLinkedType from WKeywordReply");
            sql.Append(" where replyId = @pReplyId");

            return this.SQLHelper.ExecuteDataset(CommandType.Text, sql.ToString(), paras.ToArray());
        }

        public DataSet GetWMaterialTextByReplyId(string replyId)
        {
            var paras = new List<SqlParameter>
            {
                new SqlParameter() {ParameterName = "@pReplyId", Value = replyId}
            };
            var sql = new StringBuilder();

            sql.Append(" select c.ReplyId,a.TextId,a.Title,a.CoverImageUrl,b.DisplayIndex,a.Text,a.OriginalUrl,a.Author ");
            sql.Append(" from WMaterialText a,WMenuMTextMapping b,WKeywordReply c");
            sql.Append(" where a.TextId = b.TextId and b.MenuId = c.ReplyId");
            sql.Append(" and c.ReplyId =  @pReplyId and a.isdelete = 0 and b.isdelete = 0 order by b.DisplayIndex");

            return this.SQLHelper.ExecuteDataset(CommandType.Text, sql.ToString(), paras.ToArray());
        }

        public void UpdateWkeywordReplyByReplyId(string replyId, int beLinkType, int keywordType, int displayIndex)
        {
            var paras = new List<SqlParameter>
            {
                new SqlParameter() {ParameterName = "@pReplyId", Value = replyId},
                new SqlParameter() {ParameterName = "@pBeLinkType", Value = beLinkType},
                new SqlParameter() {ParameterName = "@pKeywordType", Value = keywordType},
                new SqlParameter() {ParameterName = "@pDisplayIndex", Value = displayIndex}
            };
            var sql = new StringBuilder();

            sql.Append(
                "update WKeywordReply set belinkedType = @pBeLinkType,keywordType = @pKeywordType,displayIndex = @pDisplayIndex");
            sql.Append(" where replyId = @pReplyId");

            this.SQLHelper.ExecuteNonQuery(CommandType.Text, sql.ToString(), paras.ToArray());
        }


        public DataSet GetDefaultKeyword(string applicationId, int keywordType)
        {
            var paras = new List<SqlParameter>
            {
                new SqlParameter() {ParameterName = "@pApplicationId", Value = applicationId},
                new SqlParameter() { ParameterName = "@pKeywordType", Value = keywordType }
            };
            var sql = new StringBuilder();

            sql.Append(" select ReplyId,Keyword,ReplyType,text,ApplicationId,KeywordType,DisplayIndex,");
            sql.Append(" BeLinkedType from WKeywordReply");
            sql.Append(" where isdelete = 0 and applicationId = @pApplicationId");
            sql.Append(" and keywordType = @pKeywordType");
   

            return this.SQLHelper.ExecuteDataset(CommandType.Text, sql.ToString(), paras.ToArray());
        }

        //public string GetReplyIdByQrcode(string qrCode, string customerId)
        //{
        //    var sql = new StringBuilder();
        //    sql.Append(" select top(1) ReplyId from WKeywordReply ");
        //    sql.Append(" where keyword = ");
        //    sql.Append(" (select top(1) convert(nvarchar(50),qrcodeid) from ");
        //    sql.AppendFormat(" WQRCodeManager where qrcode = '{0}'", qrCode);
        //    sql.AppendFormat(" and customerid = '{0}')", customerId);

        //    var result = this.SQLHelper.ExecuteScalar(sql.ToString());

        //    if (result == null || string.IsNullOrEmpty(result.ToString()) || result.ToString() == "")
        //    {
        //        return "";
        //    }
        //    else
        //    {
        //        return result.ToString();
        //    }
        //}

    }
}
