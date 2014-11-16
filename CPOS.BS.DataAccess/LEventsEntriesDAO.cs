/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/9/11 17:00:10
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
    /// ��LEventsEntries�����ݷ����� 
    /// TODO:
    /// 1.ʵ��ICRUDable�ӿ�
    /// 2.ʵ��IQueryable�ӿ�
    /// 3.ʵ��Load����
    /// </summary>
    public partial class LEventsEntriesDAO : Base.BaseCPOSDAO, ICRUDable<LEventsEntriesEntity>, IQueryable<LEventsEntriesEntity>
    {
        #region ��̨Web��ȡ�б�
        /// <summary>
        /// ��ȡ�б�����
        /// </summary>
        public int GetWebListCount(LEventsEntriesEntity entity)
        {
            string sql = GetWebListSql(entity);
            sql = sql + " select count(*) as icount From #tmp; ";
            return Convert.ToInt32(this.SQLHelper.ExecuteScalar(sql));
        }
        /// <summary>
        /// ��ȡ�б�
        /// </summary>
        public DataSet GetWebList(LEventsEntriesEntity entity, int Page, int PageSize)
        {
            int beginSize = Page * PageSize + 1;
            int endSize = Page * PageSize + PageSize;
            DataSet ds = new DataSet();
            string sql = GetWebListSql(entity);
            sql += " select * From #tmp a where 1=1 and a.DisplayIndexLast between '" +
                beginSize + "' and '" + endSize + "' order by  a.DisplayIndexLast ";
            ds = this.SQLHelper.ExecuteDataset(sql);
            return ds;
        }
        private string GetWebListSql(LEventsEntriesEntity entity)
        {
            string sql = string.Empty;
            sql = "select a.* ";
            sql += " ,DisplayIndexLast = row_number() over(order by a.DisplayIndex asc) ";
            sql += " ,(select count(*) from LEventsEntriesPraise c INNER JOIN dbo.LEventSignUp b ON c.SignUpID = b.SignUpID where c.IsDelete='0' and c.EntriesId=a.EntriesId) PraiseCount ";
            sql += " ,(select count(*) from LEventsEntriesComment c INNER JOIN dbo.LEventSignUp b ON c.SignUpID = b.SignUpID where c.IsDelete='0' and c.EntriesId=a.EntriesId) CommentCount ";
            sql += " ,b.User_Name CreateByName ";
            sql += " into #tmp ";
            sql += " from [LEventsEntries] a ";
            sql += " left join [t_user] b on a.createBy=b.User_Id ";
            sql += " where a.IsDelete='0' ";
            if (entity.WorkTitle != null && entity.WorkTitle.Trim().Length > 0)
            {
                sql += " and a.WorkTitle like '%" + entity.WorkTitle.Trim() + "%' ";
            }
            if (entity.Creative != null && entity.Creative.Trim().Length > 0)
            {
                sql += " and a.Creative like '%" + entity.Creative.Trim() + "%' ";
            }
            if (entity.WorkDate != null && entity.WorkDate.Trim().Length > 0)
            {
                sql += " and a.WorkDate = '" + entity.WorkDate.Trim() + "' ";
            }
            if (entity.EventId != null && entity.EventId.Trim().Length > 0)
            {
                sql += " and a.EventId = '" + entity.EventId.Trim() + "' ";
            }
            if (entity.IsWorkDaren != null)
            {
                sql += " and a.IsWorkDaren = '" + entity.IsWorkDaren + "' ";
            }
            if (entity.IsMonthDaren != null)
            {
                sql += " and a.IsMonthDaren = '" + entity.IsMonthDaren + "' ";
            }
            if (entity.EntriesId != null && entity.EntriesId.Trim().Length > 0)
            {
                sql += " and a.EntriesId = '" + entity.EntriesId.Trim() + "' ";
            }
            //sql += " order by a.DisplayIndex asc ";
            return sql;
        }
        #endregion

        #region ��ȡ����Ĳ�����Ʒ

        /// <summary>
        /// ��ȡ����Ĳ�����Ʒ
        /// </summary>
        /// <param name="strDate">��ѯ����</param>
        /// <returns></returns>
        public DataSet GetEventsEntriesList(string strDate)
        {
            string sql = string.Empty;
            sql += " SELECT entriesId = a.EntriesId ";
            sql += " , workTitle = a.WorkTitle ";
            sql += " , workUrl = a.WorkUrl ";
            sql += " , displayIndex = row_number() over(order by a.DisplayIndex )";
            sql += " , praiseCount = (SELECT COUNT(*) FROM LEventsEntriesPraise b WHERE a.EntriesId = b.EntriesId) ";
            sql += " , commentCount = (SELECT COUNT(*) FROM LEventsEntriesComment c WHERE a.EntriesId = c.EntriesId) ";
            //sql += " , strDate = a.WorkDate ";
            sql += " FROM LEventsEntries a ";
            sql += " WHERE 1 = 1 AND a.IsDelete = 0 ";
            sql += " AND a.IsWorkDaren = 1 ";

            if (!string.IsNullOrEmpty(strDate))
            {
                //��ָ�����ڲ�ѯ
                sql += " AND LEFT(a.WorkDate, 10) = '" + strDate + "' ";
            }
            else
            {
                //Ĭ�ϲ�ѯǰһ�����Ʒ
                sql += " AND LEFT(a.WorkDate, 10) = CONVERT(VARCHAR(10), GETDATE()-1, 120) ";
            }

            //����
           // sql += " ORDER BY a.DisplayIndex ";

            return this.SQLHelper.ExecuteDataset(sql);
        }

        #endregion

        #region ��ȡ������Ʒ����������

        /// <summary>
        /// ��ȡ������Ʒ����������
        /// </summary>
        /// <param name="entriesId">��Ʒ��ʶ</param>
        /// <returns></returns>
        public DataSet GetEventsEntriesCommentList(string entriesId)
        {
            string sql = string.Empty;
            sql += " SELECT entriesId = a.EntriesId ";
            sql += " , workTitle = a.WorkTitle ";
            sql += " , workUrl = a.WorkUrl ";
            sql += " , praiseCount = (SELECT COUNT(*) FROM LEventsEntriesPraise b WHERE a.EntriesId = b.EntriesId) ";
            sql += " , commentCount = (SELECT COUNT(*) FROM LEventsEntriesComment c WHERE a.EntriesId = c.EntriesId) ";
            sql += " , nextEntriesId = (SELECT TOP 1 EntriesId FROM dbo.LEventsEntries b WHERE IsWorkDaren = '1' and a.DisplayIndex < b.DisplayIndex ORDER BY b.DisplayIndex) ";
            sql += " , preEntriesId = (SELECT TOP 1 EntriesId FROM dbo.LEventsEntries c WHERE IsWorkDaren = '1' and a.DisplayIndex > c.DisplayIndex ORDER BY c.DisplayIndex DESC ) ";
            sql += " FROM LEventsEntries a ";
            sql += " WHERE 1 = 1 AND a.IsDelete = 0 ";
            sql += " AND a.EntriesId = '" + entriesId + "' ";

            return this.SQLHelper.ExecuteDataset(sql);
        }

        #endregion

        #region ��ȡ������Ʒ�����ۼ���
        /// <summary>
        /// ��ȡ������Ʒ�����ۼ���
        /// </summary>
        /// <param name="entriesId">��Ʒ��ʶ</param>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public DataSet GetEventsEntriesCommentList(string entriesId, int page, int pageSize)
        {
            page = page <= 0 ? 1 : page;
            pageSize = pageSize <= 0 ? 15 : pageSize;
            int beginSize = (page - 1) * pageSize + 1;
            int endSize = (page - 1) * pageSize + pageSize;

            string sql = GetEventsEntriesCommentListSql(entriesId);
            sql += " select * From #tmp a where 1=1 and a.displayIndex between '" + beginSize + "' and '" + endSize + "' order by a.displayindex";
            DataSet ds = new DataSet();
            ds = this.SQLHelper.ExecuteDataset(sql);
            return ds;
        }
        public int GetEventsEntriesCommentListCount(string entriesId)
        {
            string sql = GetEventsEntriesCommentListSql(entriesId);
            sql += " select count(*) count From #tmp a ";
            DataSet ds = new DataSet();
            var obj = this.SQLHelper.ExecuteScalar(sql);
            return obj == null || obj == DBNull.Value ? 0 : Convert.ToInt32(obj);
        }
        public string GetEventsEntriesCommentListSql(string entriesId)
        {
            string sql = string.Empty;
            sql += " SELECT content = a.Content ";
            sql += " , userName = b.UserName ";
            sql += " , phone = b.Phone ";
            sql += " , displayIndex = row_number() over(order by a.CreateTime desc) ";
            sql += " , createTime = CONVERT(VARCHAR, a.CreateTime, 120) ";
            sql += " into #tmp ";
            sql += " FROM dbo.LEventsEntriesComment a ";
            sql += " INNER JOIN LEventSignUp b ON a.SignUpID = b.SignUpID ";
            sql += " WHERE 1 = 1 AND a.IsDelete = 0 ";
            sql += " AND a.EntriesId = '" + entriesId + "' ";

            return sql;
        }
        #endregion

        #region ��ȡ������

        /// <summary>
        /// ��ȡΧ�۴�������
        /// </summary>
        /// <param name="strDate">������</param>
        /// <returns></returns>
        public int GetCrowdDarenCount(string strDate)
        {
            string sql = string.Empty;
            sql += " SELECT COUNT(*) FROM LEventsEntriesComment a ";
            sql += " WHERE IsDelete = 0 AND a.IsCrowdDaren = 1 ";

            if (!string.IsNullOrEmpty(strDate))
            {
                //��ָ�����ڲ�ѯ
                sql += " AND CONVERT(VARCHAR(10), a.CreateTime, 120) = '" + strDate + "' ";
            }
            else
            {
                //Ĭ�ϲ�ѯǰһ�����Ʒ
                sql += " AND CONVERT(VARCHAR(10), a.CreateTime, 120) = CONVERT(VARCHAR(10), GETDATE()-1, 120) ";
            }

            return Convert.ToInt32(this.SQLHelper.ExecuteScalar(sql));
        }

        /// <summary>
        /// ��ȡΧ�۴��˼���
        /// </summary>
        /// <param name="strDate">������</param>
        /// <returns></returns>
        public DataSet GetCrowdDarenList(string strDate)
        {
            string sql = string.Empty;
            sql += " SELECT userName = b.UserName ";
            sql += " , content = a.Content ";
            sql += " FROM LEventsEntriesComment a ";
            sql += " INNER JOIN LEventSignUp b ON a.SignUpID = b.SignUpID ";
            sql += " WHERE a.IsDelete = 0 AND a.IsCrowdDaren = 1 ";

            if (!string.IsNullOrEmpty(strDate))
            {
                //��ָ�����ڲ�ѯ
                sql += " AND CONVERT(VARCHAR(10), a.CreateTime, 120) = '" + strDate + "' ";
            }
            else
            {
                //Ĭ�ϲ�ѯǰһ�����Ʒ
                sql += " AND CONVERT(VARCHAR(10), a.CreateTime, 120) = CONVERT(VARCHAR(10), GETDATE()-1, 120) ";
            }

            return this.SQLHelper.ExecuteDataset(sql);
        }

        /// <summary>
        /// ��ȡ�����������
        /// </summary>
        /// <param name="strDate">������</param>
        /// <returns></returns>
        public int GetWorkDarenCount(string strDate)
        {
            string sql = string.Empty;
            sql += " SELECT COUNT(*) FROM dbo.LEventsEntries a ";
            sql += " WHERE IsDelete = 0 AND a.IsWorkDaren = 1 ";

            if (!string.IsNullOrEmpty(strDate))
            {
                //��ָ�����ڲ�ѯ
                sql += " AND LEFT(a.WorkDate, 10) = '" + strDate + "' ";
            }
            else
            {
                //Ĭ�ϲ�ѯǰһ�����Ʒ
                sql += " AND LEFT(a.WorkDate, 10) = CONVERT(VARCHAR(10), GETDATE()-1, 120) ";
            }

            return Convert.ToInt32(this.SQLHelper.ExecuteScalar(sql));
        }

        /// <summary>
        /// ��ȡ������˼���
        /// </summary>
        /// <param name="strDate">������</param>
        /// <returns></returns>
        public DataSet GetWorkDarenList(string strDate)
        {
            string sql = string.Empty;
            sql += " SELECT  userName = a.Creative, prizeCount = COUNT(a.Creative) FROM dbo.LEventsEntries a ";
            sql += " WHERE IsDelete = 0 AND a.IsWorkDaren = 1 ";

            if (!string.IsNullOrEmpty(strDate))
            {
                //��ָ�����ڲ�ѯ
                sql += " AND LEFT(a.WorkDate, 10) = '" + strDate + "' ";
            }
            else
            {
                //Ĭ�ϲ�ѯǰһ�����Ʒ
                sql += " AND LEFT(a.WorkDate, 10) = CONVERT(VARCHAR(10), GETDATE()-1, 120) ";
            }

            sql += " GROUP BY a.Creative ";

            return this.SQLHelper.ExecuteDataset(sql);
        }

        #endregion

        #region ��ȡƷζ������Ʒ��

        /// <summary>
        /// ��ȡƷζ������Ʒ��
        /// </summary>
        /// <returns></returns>
        public DataSet GetEventsEntriesList()
        {
            string sql = string.Empty;
            sql += " SELECT entriesId = a.EntriesId ";
            sql += " , workTitle = a.WorkTitle ";
            sql += " , workUrl = a.WorkUrl ";
            sql += " , displayIndex = a.DisplayIndex ";
            sql += " , praiseCount = (SELECT COUNT(*) FROM LEventsEntriesPraise b WHERE a.EntriesId = b.EntriesId) ";
            sql += " , commentCount = (SELECT COUNT(*) FROM LEventsEntriesComment c WHERE a.EntriesId = c.EntriesId) ";
            sql += " FROM LEventsEntries a ";
            sql += " WHERE 1 = 1 AND a.IsDelete = 0 ";
            sql += " AND a.IsMonthDaren = 1 ";
            sql += " ORDER BY a.DisplayIndex ";

            return this.SQLHelper.ExecuteDataset(sql);
        }

        #endregion
    }
}
