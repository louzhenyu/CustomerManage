/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/8/5 9:34:33
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
    /// ��ItemComment�����ݷ����� 
    /// TODO:
    /// 1.ʵ��ICRUDable�ӿ�
    /// 2.ʵ��IQueryable�ӿ�
    /// 3.ʵ��Load����
    /// </summary>
    public partial class ItemCommentDAO : Base.BaseCPOSDAO, ICRUDable<ItemCommentEntity>, IQueryable<ItemCommentEntity>
    {
        #region ��ȡ�û�����
        /// <summary>
        /// ��ȡ�û����۶���
        /// </summary>
        /// <param name="pItemId"></param>
        /// <param name="pVidId"></param>
        /// <returns></returns>
        public ItemCommentEntity GetItemCommentEntityByUser(string pItemId, string pVidId)
        {
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [ItemComment] where ItemId='{0}' and VipId='{1}' and IsDelete=0 ", pItemId, pVidId);
            //��ȡ����
            ItemCommentEntity m = null;
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    this.Load(rdr, out m);
                    break;
                }
            }
            //����
            return m;
        }

        #endregion

        #region ��ȡ�û�����
        /// <summary>
        /// ��ȡ�û����۱��
        /// </summary>
        /// <param name="pItemId"></param>
        /// <param name="pVidId"></param>
        /// <returns></returns>
        public DataTable GetItemCommentByUser(string pItemId, string pVidId)
        {
            string sql = "SELECT ItemCommentId,ItemId,ItemType,Topic,CommentContent,Star,VipId AS ReviewerID";
            sql += " ,CAST(YEAR(LastUpdateTime) AS VARCHAR(4))+'��'+ CAST(MONTH(LastUpdateTime) AS VARCHAR(2))+'��'+ CAST(DAY(LastUpdateTime) AS VARCHAR(2))+'��' AS CommentDate";
            sql += " ,(SELECT user_name FROM dbo.T_User WHERE user_status=1 AND user_id=VipId AND customer_id=CustomerId) AS Reviewer";
            sql += " FROM ItemComment WHERE ItemId=@ItemId AND VipId=@UserID AND CustomerId=@CustomerID AND IsDelete=0";

            List<SqlParameter> param = new List<SqlParameter>();
            param.Add(new SqlParameter("@ItemId", pItemId));
            param.Add(new SqlParameter("@UserID", this.CurrentUserInfo.CurrentUser.User_Id));
            param.Add(new SqlParameter("@CustomerID", this.CurrentUserInfo.CurrentUser.customer_id));

            DataSet ds = this.SQLHelper.ExecuteDataset(CommandType.Text, sql, param.ToArray());
            if (ds != null && ds.Tables.Count > 0)
                return ds.Tables[0];
            return null;
        }

        #endregion

        #region ��ȡ�γ������б�
        /// <summary>
        /// ��ȡ�γ������б�
        /// </summary>
        /// <param name="pItemId"></param>
        /// <returns></returns>
        public DataTable GetCourseComment(string pItemId, int pPageIndex, int pPageSize)
        {
            int begin = pPageIndex * pPageSize + 1;
            int end = (pPageIndex + 1) * pPageSize;

            string sql = "SELECT * FROM (SELECT ROW_NUMBER() OVER ( ORDER BY lastUpdateTime DESC) AS rowid,ItemCommentId,ItemId,ItemType,Topic,CommentContent,Star,VipId AS ReviewerID";
            sql += " ,CAST(YEAR(LastUpdateTime) AS VARCHAR(4))+'��'+ CAST(MONTH(LastUpdateTime) AS VARCHAR(2))+'��'+ CAST(DAY(LastUpdateTime) AS VARCHAR(2))+'��' AS CommentDate";
            sql += " ,(SELECT user_name FROM T_User WHERE user_status=1 AND user_id=VipId AND customer_id=CustomerId) AS Reviewer";
            sql += " FROM ItemComment WHERE ItemId=@ItemId AND CustomerId=@CustomerID AND IsDelete=0";
            sql += ") tt WHERE tt.rowid BETWEEN @begin AND @end ";

            List<SqlParameter> param = new List<SqlParameter>();
            param.Add(new SqlParameter("@ItemId", pItemId));
            param.Add(new SqlParameter("@CustomerID", this.CurrentUserInfo.CurrentUser.customer_id));
            param.Add(new SqlParameter("@begin", begin));
            param.Add(new SqlParameter("@end", end));

            DataSet ds = this.SQLHelper.ExecuteDataset(CommandType.Text, sql, param.ToArray());
            if (ds != null && ds.Tables.Count > 0)
                return ds.Tables[0];
            return null;
        }
        #endregion

        #region ��ȡ�γ�������ƽ����
        /// <summary>
        /// ��ȡ�γ�������ƽ����
        /// </summary>
        /// <param name="pItemId"></param>
        /// <returns></returns>
        public int GetCourseAvgStar(string pItemId)
        {
            string sql = "SELECT FLOOR(ROUND(AVG(CAST(Star AS DECIMAL)),0)) AS AvgStar from dbo.ItemComment ";
            sql += " WHERE ItemId=@ItemId AND CustomerId=@CustomerID  AND IsDelete=0 GROUP BY ItemId";

            List<SqlParameter> param = new List<SqlParameter>();
            param.Add(new SqlParameter("@ItemId", pItemId));
            param.Add(new SqlParameter("@CustomerID", this.CurrentUserInfo.CurrentUser.customer_id));
            object obj = this.SQLHelper.ExecuteScalar(CommandType.Text, sql, param.ToArray());

            return Convert.ToInt32(obj);
        }
        #endregion
    }
}
