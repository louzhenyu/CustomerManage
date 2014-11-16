/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/7/26 17:11:20
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
    /// ��ObjectImages�����ݷ����� 
    /// TODO:
    /// 1.ʵ��ICRUDable�ӿ�
    /// 2.ʵ��IQueryable�ӿ�
    /// 3.ʵ��Load����
    /// </summary>
    public partial class ObjectImagesDAO : Base.BaseCPOSDAO, ICRUDable<ObjectImagesEntity>, IQueryable<ObjectImagesEntity>
    {
        #region ���ݶ����ȡͼƬ
        public DataSet GetObjectImagesByObjectId(string ObjectId)
        {
            string sql = "SELECT * FROM ObjectImages a WHERE a.ObjectId = '" + ObjectId + "' AND IsDelete ='0' ORDER BY DisplayIndex ; ";//��������update by Henry 2014-11-11
            return this.SQLHelper.ExecuteDataset(sql);
        }
        #endregion

        #region ��ȡͬ������ͼƬ

        /// <summary>
        /// ��ȡͬ������ͼƬ
        /// </summary>
        /// <param name="latestTime">���ͬ��ʱ��</param>
        /// <returns></returns>
        public DataSet GetSynWelfareObjectImageList(string latestTime)
        {
            string sql = string.Empty;
            sql += " SELECT imageid = a.ImageId ";
            sql += " , objectid = a.ObjectId ";
            sql += " , imageurl = a.ImageURL ";
            sql += " , isdelete = a.IsDelete ";
            sql += " FROM dbo.ObjectImages a ";
            sql += " WHERE 1 = 1 ";

            if (!string.IsNullOrEmpty(latestTime))
            {
                sql += " AND a.LastUpdateTime >= '" + latestTime + "' ";
            }

            return this.SQLHelper.ExecuteDataset(sql);
        }

        #endregion

        #region ��ȡ�б�
        /// <summary>
        /// ��ȡ�б�����
        /// </summary>
        public int GetListCount(ObjectImagesEntity entity)
        {
            string sql = GetListSql(entity);
            sql = sql + " select count(*) as icount From #tmp; ";
            return Convert.ToInt32(this.SQLHelper.ExecuteScalar(sql));
        }
        /// <summary>
        /// ��ȡ�б�
        /// </summary>
        public DataSet GetList(ObjectImagesEntity entity, int Page, int PageSize)
        {
            int beginSize = (Page - 1) * PageSize + 1;
            int endSize = beginSize + PageSize - 1;
            DataSet ds = new DataSet();
            string sql = GetListSql(entity);
            sql += " select * From #tmp a where 1=1 and a.DisplayIndexLast between '" +
                beginSize + "' and '" + endSize + "' order by  a.DisplayIndexLast ";
            ds = this.SQLHelper.ExecuteDataset(sql);
            return ds;
        }
        private string GetListSql(ObjectImagesEntity entity)
        {
            string sql = string.Empty;
            sql = "select a.* ";
            sql += " ,DisplayIndexLast = row_number() over(order by a.DisplayIndex desc) ";
            sql += " into #tmp ";
            sql += " from [ObjectImages] a ";
            sql += " where 1=1 ";
            sql += " and a.IsDelete='0' ";
            if (entity.ObjectId != null && entity.ObjectId.Length > 0)
            {
                sql += " and a.ObjectId = '" + entity.ObjectId + "' ";
            }
            return sql;
        }
        #endregion

        #region ���ݿͻ���ȡ�ŵ�ͼƬ����
        public DataSet GetObjectImagesByCustomerId(string CustomerId)
        {
            string sql = "SELECT top 5 a.* FROM ObjectImages a WHERE a.ObjectId IN (SELECT unit_id FROM dbo.t_unit where customer_id= '" + CustomerId + "' and status=1 and type_id = 'EB58F1B053694283B2B7610C9AAD2742' ) AND IsDelete ='0' ; ";
            DataSet ds = this.SQLHelper.ExecuteDataset(sql);
            return ds;
        }
        #endregion


        #region ͼƬͬ��
        /// <summary>
        /// ������ѯsql
        /// </summary>
        /// <param name="queryInfo"></param>
        /// <returns></returns>
        public string GetClientImageListSql(ObjectImagesEntity queryInfo)
        {
            if (queryInfo.DisplayIndexLast == null)
                queryInfo.DisplayIndexLast = -1;
            string sql = string.Empty;
            #region
            sql += " select a.* ";
            sql += " ,DisplayIndexByTime = (row_number() over(order by a.lastUpdateTime desc)) ";
            sql += " ,DisplayIndexLast = (row_number() over(order by a.lastUpdateTime desc))+" + queryInfo.DisplayIndexLast;
            sql += " into #tmp from (select a.* ";
            sql += " ,u.user_Name CreateByName";
            sql += " ,(SELECT dbo.DateToTimestamp(a.lastUpdateTime)) timestampValue";
            sql += " from ObjectImages a";
            sql += " left join t_user u on a.createBy=u.user_id";
            sql += " where 1=1 ";
            sql += string.Format(" and a.CustomerId='{0}'", queryInfo.CustomerId);
            sql += string.Format(" and (a.lastUpdateTime > (SELECT dbo.TimestampToDate('{0}')) )", queryInfo.timestamp);
            sql += " ) a;";

            #endregion
            return sql;
        }

        /// <summary>
        /// ���������б��ȡ
        /// </summary>
        public DataSet GetClientImageList(ObjectImagesEntity queryInfo)
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
            string sql = GetClientImageListSql(queryInfo);
            sql = sql + "select * From #tmp a where 1=1 and a.DisplayIndexByTime between '" + beginSize + "' and '" + endSize + "' order by a.DisplayIndexByTime";
            ds = this.SQLHelper.ExecuteDataset(sql);
            return ds;
        }

        /// <summary>
        /// ��ȡ��ѯ������
        /// </summary>
        /// <param name="queryInfo"></param>
        /// <returns></returns>
        public int GetClientImageListCount(ObjectImagesEntity queryInfo)
        {
            string sql = GetClientImageListSql(queryInfo);
            sql = sql + " select count(*) as icount From #tmp; ";
            return Convert.ToInt32(this.SQLHelper.ExecuteScalar(sql));

        }
        #endregion


        #region  ��ȡ������ϸUrl��
        /// <summary>
        /// ����customerID��objectID,ȡ�÷�����Ӧ�ķ�����ϸ��Ϣ��
        /// </summary>
        /// <param name="customerId">�ͻ�ID��</param>
        /// <param name="objectID">����ID��</param>
        /// <returns>������ϸҳ���ַ��</returns>
        public DataSet GetObjectImagesByCustomerId(string customerId, string objectID)
        {
            string sql = "select  * from ObjectImages  where CustomerId=@customerID and ObjectId=@objectID";
            List<SqlParameter> para = new List<SqlParameter>() { 
                    new SqlParameter("@customerId",customerId),
                    new SqlParameter("@objectID",objectID)
            };

            DataSet ds = this.SQLHelper.ExecuteDataset(CommandType.Text, sql, para.ToArray());

            return ds;
        }

        #endregion
    }
}
