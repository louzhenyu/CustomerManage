/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013-12-18 17:58
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
    /// ��LEventsAlbum�����ݷ����� 
    /// TODO:
    /// 1.ʵ��ICRUDable�ӿ�
    /// 2.ʵ��IQueryable�ӿ�
    /// 3.ʵ��Load����
    /// </summary>
    public partial class LEventsAlbumDAO : Base.BaseCPOSDAO, ICRUDable<LEventsAlbumEntity>, IQueryable<LEventsAlbumEntity>
    {
        #region ��ȡ����б�

        /// <summary>
        /// ��ȡ����б�����
        /// </summary>
        public int GetAlbumCount(LEventsAlbumEntity albumEntity)
        {
            string sql = GetAlbumSql(albumEntity);
            sql = sql + " select count(*) as icount From #tmp; ";
            return Convert.ToInt32(this.SQLHelper.ExecuteScalar(sql));
        }

        /// <summary>
        /// ��ȡ����б�
        /// </summary>
        public DataSet GetAlbumList(LEventsAlbumEntity albumEntity, int Page, int PageSize)
        {
            int beginSize = Page * PageSize + 1;
            int endSize = Page * PageSize + PageSize;

            string sql = GetAlbumSql(albumEntity);
            sql += " select * From #tmp a where 1=1 and a.displayindex between '" +
                beginSize + "' and '" + endSize + "' order by a.displayindex ";
            var ds = this.SQLHelper.ExecuteDataset(sql);
            return ds;
        }

        /// <summary>
        /// ������ѯsql
        /// </summary>
        /// <returns></returns>
        private string GetAlbumSql(LEventsAlbumEntity albumEntity)
        {
            string sql = string.Empty;
            sql = " SELECT a.* ";
            sql += " ,TypeName = CASE a.TYPE WHEN '1' THEN '��Ƭ' WHEN '2' THEN '��Ƶ' ELSE '' END ";
            sql += " ,ModuleTypeName = CASE a.ModuleType WHEN '1' THEN '�ģ��' ELSE '' END ";
            sql += " ,DisplayIndex = ROW_NUMBER() OVER(ORDER BY a.ModuleName, a.SortOrder) ";
            sql += " ,(select count(*) from LEventsAlbumPhoto where AlbumId=a.AlbumId) [Count]";
            sql += " INTO #tmp ";
            sql += " FROM dbo.LEventsAlbum a ";
            sql += " WHERE a.IsDelete = 0 ";

            if (!string.IsNullOrEmpty(albumEntity.Title))
            {
                sql += " AND a.Title LIKE '%" + albumEntity.Title + "%' ";
            }
            if (!string.IsNullOrEmpty(albumEntity.Type))
            {
                sql += " AND a.Type = '" + albumEntity.Type + "' ";
            }
            if (!string.IsNullOrEmpty(albumEntity.ModuleType))
            {
                sql += " AND a.ModuleType = '" + albumEntity.ModuleType + "' ";
            }
            if (!string.IsNullOrEmpty(albumEntity.ModuleName))
            {
                sql += " AND a.ModuleName LIKE '%" + albumEntity.ModuleName + "%' ";
            }

            return sql;
        }
        #endregion

        #region ��ȡ��ģ���б�

        /// <summary>
        /// ��ȡ��ģ���б�����
        /// </summary>
        public int GetAlbumModuleCount(string moduleType, string moduleName)
        {
            string sql = GetAlbumModuleSql(moduleType, moduleName);
            sql = sql + " select count(*) as icount From #tmp; ";
            return Convert.ToInt32(this.SQLHelper.ExecuteScalar(sql));
        }

        /// <summary>
        /// ��ȡ��ģ���б�
        /// </summary>
        public DataSet GetAlbumModuleList(string moduleType, string moduleName, int Page, int PageSize)
        {
            int beginSize = Page * PageSize + 1;
            int endSize = Page * PageSize + PageSize;

            string sql = GetAlbumModuleSql(moduleType, moduleName);
            sql += " select * From #tmp a where 1=1 and a.displayindex between '" +
                beginSize + "' and '" + endSize + "' order by a.displayindex ";
            var ds = this.SQLHelper.ExecuteDataset(sql);
            return ds;
        }

        /// <summary>
        /// ������ѯsql
        /// </summary>
        /// <param name="moduleType">1: �ģ��</param>
        /// <param name="moduleName">ģ�����</param>
        /// <returns></returns>
        private string GetAlbumModuleSql(string moduleType, string moduleName)
        {
            string sql = string.Empty;

            if (moduleType.Equals("1"))   //1: �ģ��
            {
                sql += " SELECT ID = a.EventID, a.Title, a.CreateTime ";
                sql += " ,DisplayIndex = ROW_NUMBER() OVER(ORDER BY a.CreateTime DESC) ";
                sql += " INTO #tmp ";
                sql += " FROM dbo.LEvents a ";
                sql += " WHERE a.IsDelete = 0 ";

                if (!string.IsNullOrEmpty(moduleName))
                {
                    sql += " AND a.Title LIKE '%" + moduleName + "%' ";
                }
            }

            return sql;
        }
        #endregion

        #region ��ȡ��Ƭ�б�
        /// <summary>
        /// ��ȡ��Ƭ����
        /// </summary>
        public int GetAlbumImageCount(string albumId)
        {
            string sql = GetImageSql(albumId);
            sql = sql + " select count(*) as icount From #tmp; ";
            return Convert.ToInt32(this.SQLHelper.ExecuteScalar(sql));
        }
        /// <summary>
        /// ��ȡ��Ƭ�б�
        /// </summary>
        public DataSet GetAlbumImageList(string albumId, int Page, int PageSize)
        {
            int beginSize = Page * PageSize + 1;
            int endSize = Page * PageSize + PageSize;

            string sql = GetImageSql(albumId);
            sql += " select * From #tmp a where 1=1 and a.DisplayIndex between '" +
                beginSize + "' and '" + endSize + "' order by  a.DisplayIndex ";
            var ds = this.SQLHelper.ExecuteDataset(sql);
            return ds;
        }
        private string GetImageSql(string albumId)
        {
            string sql = string.Empty;
            sql = " SELECT a.* ";
            sql += " ,DisplayIndex = row_number() over(order by a.SortOrder) ";
            sql += " INTO #tmp ";
            sql += " FROM dbo.LEventsAlbumPhoto a ";
            sql += " WHERE a.IsDelete = 0 ";
            sql += " AND a.AlbumId = '" + albumId + "'";

            return sql;
        }
        #endregion

        #region 2014-04-23 �޸���:tiansheng.zhu
        #region ִ�з�ҳ��ѯ
        public DataSet GetLEventsAlbumList(string pWhere, int pPageSize, int pCurrentPageIndex, out int pPageCount)
        {
            //��֯SQL
            StringBuilder pagedSql = new StringBuilder();
            StringBuilder totalCountSql = new StringBuilder();
            //��ҳSQL
            pagedSql.AppendFormat(@" select *  from (select row_number()over( order by la.SortOrder,la.CreateTime desc
                                    ) as ___rn,la.* ,es.BrowseNum,es.BookmarkNum,es.PraiseNum,es.ShareNum
                                    from LEventsAlbum as la
                                    left join EventStats as  es
                                    on es.ObjectID=la.AlbumId and es.IsDelete=la.IsDelete
                                    where la.isdelete=0 and la.CustomerId='{0}' {1}
                                    ) as A", this.CurrentUserInfo.ClientID, pWhere);

            //�ܼ�¼��SQL
            totalCountSql.AppendFormat("select count(1) from [LEventsAlbum]  as la where isdelete=0 and CustomerId='{0}'  {1}", this.CurrentUserInfo.ClientID, pWhere);
            //ȡָ��ҳ������
            pagedSql.AppendFormat(" where ___rn >{0} and ___rn <={1}", pPageSize * (pCurrentPageIndex - 1), pPageSize * (pCurrentPageIndex));
            //ִ����䲢���ؽ��

            using (DataSet ds = this.SQLHelper.ExecuteDataset(pagedSql.ToString()))
            {
                int totalCount = Convert.ToInt32(this.SQLHelper.ExecuteScalar(totalCountSql.ToString()));    //����������
                pPageCount = totalCount;
                return ds;
            }
        }

        #endregion
        #endregion


        #region ������Ƶģ�����ͻ�ȡ���� Add by changjian.tian 2014-6-3
        public DataSet GetLEventsAlbumByType(string pModuleType)
        {
            StringBuilder sbSQL = new StringBuilder();
            sbSQL.Append(string.Format(" select L2.ModuleName,L1.[Description] as VideoURL,L3.LinkUrl as ImageUrl,L3.Title,L3.Description as Intro,L3.LinkUrl as BigImageUrl,L3.Title as BigImageTitle,L3.Description as BigImageDescription     from LEventsAlbum	 L1 right join LEventsAlbumType L2 on L1.ModuleType=L2.ModuleType left join LEventsAlbumPhoto L3 on L1.AlbumId=L3.AlbumId where L2.ModuleType ='{0}'  and L3.IsDelete=0    order by L3.SortOrder ", pModuleType));
            return SQLHelper.ExecuteDataset(sbSQL.ToString());
        }
        #endregion
    }
}
