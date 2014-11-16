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
    /// 数据访问：  
    /// 表IMGroup的数据访问类 
    /// TODO:
    /// 1.实现ICRUDable接口
    /// 2.实现IQueryable接口
    /// 3.实现Load方法
    /// </summary>
    public partial class IMGroupDAO : Base.BaseCPOSDAO, ICRUDable<IMGroupEntity>, IQueryable<IMGroupEntity>
    {
        /// <summary>
        /// 获取群组列表
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="customerID"></param>
        /// <param name="groupName"></param>
        /// <param name="chatGroupId"></param>
        /// <returns></returns>
        public DataSet GetUserInGroups(string userID, string customerID, int pageIndex, int pageSize)
        {
            DataSet ds = new DataSet();
            string sql = @"  SELECT TOP {0}
                                        *
                               FROM     ( SELECT    ROW_NUMBER() OVER ( ORDER BY ChatGroupID ) AS RowId ,
                                                    *
                                          FROM      dbo.IMGroup
                                          WHERE     ChatGroupID IN (
                                                    SELECT  IMGroupID
                                                    FROM    dbo.IMGroupUser
                                                    WHERE   UserID = @UserID
                                                            AND CustomerID = @CustomerID
                                                            AND IsDelete = 0 ) ";
            List<SqlParameter> para = new List<SqlParameter>();
            para.Add(new SqlParameter("@UserID", userID));
            para.Add(new SqlParameter("@CustomerID", customerID));
            //if (!string.IsNullOrEmpty(chatGroupId))
            //{
            //    sql += @" AND ChatGroupId=@chatGroupId";
            //    para.Add(new SqlParameter("@chatGroupId", chatGroupId));
            //}
            sql += " ) tt WHERE   rowId > {1}*{2} ";
            sql = string.Format(sql, pageSize, pageIndex, pageSize);
            ds = this.SQLHelper.ExecuteDataset(CommandType.Text, sql, para.ToArray());
            return ds;
        }
        /// <summary>
        /// 获取群组成员
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="customerID"></param>
        /// <param name="chatGroupId"></param>
        /// <returns></returns>
        public DataSet GetIMGroupUser(string userID, string customerID, string chatGroupId)
        {
            DataSet ds = new DataSet();
            string sql = @"  SELECT    *
                              FROM      dbo.IMGroupUser
                              WHERE     UserID = @UserID
                                        AND CustomerID = @CustomerID
                                        AND IMGroupID = @IMGroupID
                                        AND IsDelete = 0";
            List<SqlParameter> para = new List<SqlParameter>();
            para.Add(new SqlParameter("@UserID", userID));
            para.Add(new SqlParameter("@CustomerID", customerID));
            para.Add(new SqlParameter("@IMGroupID", chatGroupId));

            ds = this.SQLHelper.ExecuteDataset(CommandType.Text, sql, para.ToArray());
            return ds;
        }

        public void DeleteGroup(string userID, string customerID, string chatGroupID)
        {
            if (chatGroupID == null)
                return;
            //组织参数化SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("update [IMGroup] set LastUpdateTime='" + DateTime.Now.ToString() + "',LastUpdateBy='" + CurrentUserInfo.UserID + "',IsDelete=1 where ChatGroupID ='" + chatGroupID + "';");
            //执行语句
            int result = 0;
            result = this.SQLHelper.ExecuteNonQuery(CommandType.Text, sql.ToString(), null);
        }

        #region 根据创建人Id获取该用户创建的群组集合
        /// <summary>
        /// 根据创建人Id获取该用户创建的群组集合
        /// </summary>
        /// <param name="userID">用户ID</param>
        /// <param name="customerID">客户ID</param>
        /// <returns></returns>
        public DataSet GetIMGroupUser(string userID, string customerID)
        {
            DataSet ds = new DataSet();
            string sql = @"Select ChatGroupID,BindGroupID from IMGroup  where CreateBy=@CreateBy AND CustomerID = @CustomerID AND IsDelete = 0";

            List<SqlParameter> para = new List<SqlParameter>();
            para.Add(new SqlParameter("@CreateBy", userID));
            para.Add(new SqlParameter("@CustomerID", customerID));

            ds = this.SQLHelper.ExecuteDataset(CommandType.Text, sql, para.ToArray());
            return ds;
        }
        #endregion

        #region 群组用户数量更改
        /// <summary>
        /// 群组用户数量更改
        /// count 正加负减
        /// </summary>
        /// <param name="count"></param>
        /// <param name="chatGroupID"></param>
        /// <returns></returns>
        public void UpdateGroupUserCount(int count, string chatGroupID)
        {
            if (chatGroupID == null)
                return;
            //组织参数化SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("update [IMGroup] set LastUpdateTime=@LastUpdateTime,LastUpdateBy=@LastUpdateBy,UserCount=UserCount+(@UserCount) where ChatGroupID=@ChatGroupID;");
            SqlParameter[] parameters = new SqlParameter[] 
            { 
                new SqlParameter{ParameterName="@LastUpdateTime",SqlDbType=SqlDbType.DateTime,Value=DateTime.Now},
                new SqlParameter{ParameterName="@LastUpdateBy",SqlDbType=SqlDbType.VarChar,Value=Convert.ToString(CurrentUserInfo.UserID)},
                new SqlParameter{ ParameterName="@UserCount",SqlDbType=SqlDbType.Int,Value=count},
                new SqlParameter{ParameterName="@ChatGroupID",SqlDbType=SqlDbType.VarChar,Value=chatGroupID}
            };
            //执行语句
            int result = 0;
            result = this.SQLHelper.ExecuteNonQuery(CommandType.Text, sql.ToString(), parameters);
        }
        #endregion

        #region 获取群组信息
        /// <summary>
        /// 获取群组信息
        /// </summary>
        /// <param name="userID">组创建UserID</param>
        /// <param name="customerID">商户ID</param>
        /// <param name="groupName">群组名称模糊查询</param>
        /// <param name="chatGroupId">群组ID</param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public DataSet GetIMGroups(string userID, string customerID, string groupName, string chatGroupId, string bindGroupID, int pageIndex, int pageSize)
        {
            DataSet ds = new DataSet();
            string sql = @" SELECT TOP {0} * FROM ( SELECT ROW_NUMBER() OVER ( ORDER BY ChatGroupID ) AS RowId,* FROM IMGroup WHERE
                    CustomerID =@CustomerID ";
            List<SqlParameter> para = new List<SqlParameter>();
            para.Add(new SqlParameter("@CustomerID", customerID));

            if (!string.IsNullOrEmpty(bindGroupID))
            {
                sql += @" AND BindGroupID=@BindGroupID";
                para.Add(new SqlParameter("@BindGroupID", bindGroupID));
            }
            if (!string.IsNullOrEmpty(userID))
            {
                sql += @" AND CreateBy=@CreateBy";
                para.Add(new SqlParameter("@CreateBy", userID));
            }
            if (!string.IsNullOrEmpty(chatGroupId))
            {
                sql += @" AND ChatGroupId=@chatGroupId";
                para.Add(new SqlParameter("@chatGroupId", chatGroupId));
            }
            if (!string.IsNullOrEmpty(groupName))
            {
                sql += @" AND GroupName like @GroupName";
                para.Add(new SqlParameter("@GroupName", "%" + groupName + "%"));
            }
            sql += "  AND IsDelete = 0 ) tt WHERE   RowId > {1}*{2} ";
            sql = string.Format(sql, pageSize, pageIndex, pageSize);
            ds = this.SQLHelper.ExecuteDataset(CommandType.Text, sql, para.ToArray());
            return ds;
        }

        public DataSet GetIMGroups(string customerID, string chatGroupId, string bindGroupID)
        {
            DataSet ds = new DataSet();
            string sql = @" SELECT ChatGroupID,GroupName,ISNULL(LogoUrl,'') AS LogoUrl,Description,CustomerID,ISNULL(Telephone,'') AS Telephone
,UserCount,GroupLevel,ISNULL(IsPublic,'') AS IsPublic,ApproveNeededLevel,ISNULL(InvitationLevel,'') AS InvitationLevel
,ISNULL(ChatLevel,'') AS ChatLevel,ISNULL(QuitLevel,'') AS QuitLevel,BindGroupID,CreateBy,CreateTime,LastUpdateBy,LastUpdateTime,IsDelete  FROM IMGroup WHERE CustomerID =@CustomerID ";

            List<SqlParameter> para = new List<SqlParameter>();
            para.Add(new SqlParameter("@CustomerID", customerID));

            if (!string.IsNullOrEmpty(bindGroupID))
            {
                sql += @" AND BindGroupID=@BindGroupID";
                para.Add(new SqlParameter("@BindGroupID", bindGroupID));
            }
            if (!string.IsNullOrEmpty(chatGroupId))
            {
                sql += @" AND ChatGroupId=@chatGroupId";
                para.Add(new SqlParameter("@chatGroupId", chatGroupId));
            }

            sql += "  AND IsDelete = 0 ";
            ds = this.SQLHelper.ExecuteDataset(CommandType.Text, sql, para.ToArray());
            return ds;
        }
        #endregion

        /// <summary>
        /// 查询制定的群是否是该用户创建的。
        /// </summary>
        /// <param name="userID">用户ID</param>
        /// <param name="groupID">群组ID</param>
        /// <param name="customerID">客户ID</param>
        /// <returns></returns>
        public DataSet GetIMGroupUserByGroupID(string userID, string groupID, string customerID)
        {
            DataSet ds = new DataSet();
            string sql = @" Select ChatGroupID,BindGroupID,CreateBy from IMGroup where CreateBy=@CreateBy AND ChatGroupID=@ChatGroupID and  CustomerID = @CustomerID AND IsDelete = 0";

            List<SqlParameter> para = new List<SqlParameter>();
            para.Add(new SqlParameter("@CreateBy", userID));
            para.Add(new SqlParameter("@ChatGroupID", groupID));
            para.Add(new SqlParameter("@CustomerID", customerID));
            ds = this.SQLHelper.ExecuteDataset(CommandType.Text, sql, para.ToArray());

            return ds;
        }

        #region 根据群组名称过滤-企信后台
        /// <summary>
        /// 根据群组名称过滤
        /// </summary>
        /// <param name="pGroupName"></param>
        /// <returns></returns>
        public DataTable GetIMGroupByGroupName(string pGroupName)
        {
            pGroupName = pGroupName.Replace("'", "''");

            DataSet ds = new DataSet();
            string sql = "SELECT ChatGroupID, GroupName,GroupLevel,Description,UserCount,BindGroupID,CreateBy FROM IMGroup ";
            sql += " WHERE GroupName LIKE '%{0}%' AND CustomerID = '{1}' AND IsDelete = 0 ORDER BY GroupName";
            sql = string.Format(sql, pGroupName, this.CurrentUserInfo.CurrentUser.customer_id);
            ds = this.SQLHelper.ExecuteDataset(CommandType.Text, sql);
            if (ds != null && ds.Tables != null && ds.Tables.Count > 0)
                return ds.Tables[0];
            return null;
        }
        #endregion

    }
}
