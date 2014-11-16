/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/5/26 14:59:01
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
    /// ��IMGroupUser�����ݷ����� 
    /// TODO:
    /// 1.ʵ��ICRUDable�ӿ�
    /// 2.ʵ��IQueryable�ӿ�
    /// 3.ʵ��Load����
    /// </summary>
    public partial class IMGroupUserDAO : Base.BaseCPOSDAO, ICRUDable<IMGroupUserEntity>, IQueryable<IMGroupUserEntity>
    {
        #region ��ȡȺ���Ա
        public DataSet GetIMGroupUser(string userID, string chatGroupID, string customerID)
        {
            DataSet ds = new DataSet();
            string sql = @"SELECT  *
                            FROM    IMGroupUser
                            WHERE   IMGroupID = @IMGroupID
                                    AND CustomerID = @CustomerID
                                    AND IsDelete = 0";
            List<SqlParameter> para = new List<SqlParameter>();
            para.Add(new SqlParameter("@IMGroupID", chatGroupID));
            para.Add(new SqlParameter("@CustomerID", customerID));
            if (!string.IsNullOrEmpty(userID))
            {
                sql += " and UserID=@UserID ";
                para.Add(new SqlParameter("@UserID", userID));
            }
            ds = this.SQLHelper.ExecuteDataset(CommandType.Text, sql, para.ToArray());
            return ds;
        }

        public DataSet GetIMGroupUser(string chatGroupID, string customerID, int pageIndex, int pageSize)
        {
            DataSet ds = new DataSet();
            string sql = @"SELECT TOP {0} * FROM ( SELECT    ROW_NUMBER() OVER ( ORDER BY IMGroupUserID ) AS RowID,
                    ISNULL(( SELECT TOP 1 oi.ImageURL
                      FROM      ObjectImages oi
                      WHERE     oi.ObjectId = tu.user_id
                                AND oi.IsDelete = '0'
                      ORDER BY  oi.DisplayIndex
                    ),'') highImageUrl ,
                    imgu.* ,
                    tu.* ,
                    VoipAccount
          FROM      IMGroupUser AS imgu
                    INNER JOIN T_User AS tu ON imgu.UserID = tu.user_id
                    INNER JOIN TUserThirdPartyMapping tp ON tu.user_id = tp.UserId
          WHERE     IMGroupID = @IMGroupID
                    AND CustomerID = @CustomerID
                    AND imgu.IsDelete = 0";

            List<SqlParameter> para = new List<SqlParameter>();
            para.Add(new SqlParameter("@IMGroupID", chatGroupID));
            para.Add(new SqlParameter("@CustomerID", customerID));

            sql += " ) tt  WHERE   RowID > {1} * {2}";
            sql = string.Format(sql, pageSize, pageIndex, pageSize);
            ds = this.SQLHelper.ExecuteDataset(CommandType.Text, sql, para.ToArray());
            return ds;
        }

        #endregion

        /// <summary>
        /// ����Ⱥ��ID��ȡȺ���û�
        /// </summary>
        /// <param name="groupID">Ⱥ��ID</param>
        /// <param name="customerID">�ͻ�ID</param>
        /// <returns></returns>
        public DataSet GetGroupUserByGroupID(string groupID, string customerID)
        {
            DataSet ds = new DataSet();
            string sql = @"select UserID from IMGroupUser  where ImGroupID=@IMGroupID  AND CustomerID = @CustomerID AND IsDelete = 0";
            List<SqlParameter> para = new List<SqlParameter>();
            para.Add(new SqlParameter("@IMGroupID", groupID));
            para.Add(new SqlParameter("@CustomerID", customerID));

            ds = this.SQLHelper.ExecuteDataset(CommandType.Text, sql, para.ToArray());
            return ds;
        }
        #region Ⱥ����������
        public void AddUsersGroup(List<string> userIDList, string chatGroupID, string customerID)
        {
            var tran = this.SQLHelper.CreateTransaction();
            using (tran.Connection)
            {
                try
                {
                    DataSet ds = null;
                    IMGroupUserEntity imgue = null;
                    foreach (var itemUserID in userIDList)
                    {
                        ds = GetIMGroupUser(itemUserID, chatGroupID, customerID);
                        if (ds == null || ds.Tables.Count <= 0 || ds.Tables[0] == null || ds.Tables[0].Rows.Count <= 0)
                        {
                            imgue = new IMGroupUserEntity();
                            imgue.IMGroupUserID = Guid.NewGuid();
                            imgue.UserID = itemUserID;
                            imgue.IMGroupID = chatGroupID;
                            imgue.CustomerID = customerID;
                            imgue.Status = 0;
                            Create(imgue);
                        }
                    }
                    //TO-DO:ʵ���Լ���ҵ��
                    tran.Commit();
                }
                catch
                {
                    //�ع�&ת���쳣
                    tran.Rollback();
                    throw;
                }
            }
        }
        #endregion

        #region Ⱥ����������
        public void DelUsersGroup(List<string> userIDList, string chatGroupID, string customerID)
        {
            StringBuilder keys = new StringBuilder();
            if (userIDList != null)
            {
                foreach (object item in userIDList)
                {
                    keys.AppendFormat("'{0}',", item.ToString());
                }
            }
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("update [IMGroupUser] set LastUpdateTime='" + DateTime.Now.ToString() + "',LastUpdateBy='" + CurrentUserInfo.UserID + "',IsDelete=1  where 1=1  ");

            if (keys.Length > 0)
                sql.AppendLine("and UserID in (" + keys.ToString().Substring(0, keys.ToString().Length - 1) + ")");

            sql.AppendLine(" and IMGroupID='" + chatGroupID + "'");

            if (!string.IsNullOrEmpty(customerID))
                sql.AppendLine(" and CustomerID='" + customerID + "'");

            sql.AppendLine(" and IsDelete=0;");
            int result = 0;
            result = this.SQLHelper.ExecuteNonQuery(CommandType.Text, sql.ToString(), null);
        }
        #endregion
    }
}
