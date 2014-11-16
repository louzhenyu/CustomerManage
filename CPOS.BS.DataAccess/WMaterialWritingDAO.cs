/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/8/29 16:13:18
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
    /// ��WMaterialWriting�����ݷ����� 
    /// TODO:
    /// 1.ʵ��ICRUDable�ӿ�
    /// 2.ʵ��IQueryable�ӿ�
    /// 3.ʵ��Load����
    /// </summary>
    public partial class WMaterialWritingDAO : Base.BaseCPOSDAO, ICRUDable<WMaterialWritingEntity>, IQueryable<WMaterialWritingEntity>
    {
        #region ��̨Web��ȡ�б�
        /// <summary>
        /// ��ȡ�б�����
        /// </summary>
        public int GetWebListCount(WMaterialWritingEntity entity)
        {
            string sql = GetWebListSql(entity);
            sql = sql + " select count(*) as icount From #tmp; ";
            return Convert.ToInt32(this.SQLHelper.ExecuteScalar(sql));
        }
        /// <summary>
        /// ��ȡ�б�
        /// </summary>
        public DataSet GetWebList(WMaterialWritingEntity entity, int Page, int PageSize)
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
        private string GetWebListSql(WMaterialWritingEntity entity)
        {
            string sql = string.Empty;
            sql = "select a.* ";
            sql += " ,DisplayIndexLast = row_number() over(order by a.[CreateTime] desc) ";
            sql += " ,b.User_Name CreateByName ";
            sql += " into #tmp ";
            sql += " from [WMaterialWriting] a ";
            sql += " left join [t_user] b on a.createBy=b.User_Id ";
            sql += " inner join [WModelWritingMapping] c on (c.isDelete='0' and a.WritingId=c.WritingId) ";
            sql += " inner join [WModel] d on (d.isDelete='0' and c.ModelId=d.ModelId) ";
            sql += " where a.IsDelete='0' ";

            sql += " and d.ModelId = '" + entity.ModelId + "' ";

            return sql;
        }
        #endregion

        #region ����ID��ȡ�ı���Ϣ

        /// <summary>
        /// ����ID��ȡ�ı���Ϣ
        /// </summary>
        /// <param name="textId">��ϢID</param>
        /// <returns></returns>
        public DataSet GetWMaterialWritingByID(string textId)
        {
            string sql = " SELECT * FROM dbo.WMaterialWriting WHERE IsDelete = 0 "
                + " AND WritingId = '" + textId + "' ";

            return this.SQLHelper.ExecuteDataset(sql);
        }

        #endregion

        #region ����ModelId��ȡ�ı���Ϣ
        public DataSet GetWMaterialWritingByModelId(string eventId)
        {
            DataSet ds = new DataSet();

            ds = this.SQLHelper.ExecuteDataset(CommandType.StoredProcedure, "GetWelcomeMessageByEventId"
                , new SqlParameter[] { new SqlParameter("@EventId", eventId) });

            return ds;
        }
        #endregion
    }
}
