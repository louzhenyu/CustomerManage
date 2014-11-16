/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/5/23 14:33:03
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
    /// 表T_User的数据访问类 
    /// TODO:
    /// 1.实现ICRUDable接口
    /// 2.实现IQueryable接口
    /// 3.实现Load方法
    /// </summary>
    public partial class T_UserDAO : Base.BaseCPOSDAO, ICRUDable<T_UserEntity>, IQueryable<T_UserEntity>
    {
        #region 获取后台管理人信息
        /// <summary>
        /// 获取后台管理人信息
        /// </summary>
        /// <param name="pCustomerID"></param>
        /// <param name="pEmail"></param>
        /// <returns></returns>
        public DataSet ManageUserInfo(string pEmail)
        {
            string sql = "SELECT USER_ID AS UserID,user_code AS UserCode,user_name AS UserName,user_gender AS UserGender,user_email AS UserEmail";
            sql += ",user_telephone AS UserTelephone,user_cellphone AS UserCellphone ,user_password AS UserPassword FROM T_User ";
            sql += " WHERE user_email=@user_email ";
            sql += " AND customer_id=@customer_id ";
            sql += " AND user_status=1 ";
            List<SqlParameter> para = new List<SqlParameter>();
            para.Add(new SqlParameter("@user_email", pEmail));
            para.Add(new SqlParameter("@customer_id", this.CurrentUserInfo.CurrentUser.customer_id));
            return this.SQLHelper.ExecuteDataset(CommandType.Text, sql, para.ToArray());
        }
        #endregion

        #region 搜索拥有建群权限的员工-企信后台
        /// <summary>
        /// 搜索拥有建群权限的员工
        /// </summary>
        /// <param name="pGroupName"></param>
        /// <returns></returns>
        public DataTable GetIMGroupCreatorByUserName(string pUserName, string pRightCode)
        {
            pUserName = pUserName.Replace("'", "''");

            DataSet ds = new DataSet();
            string sql = "SELECT tu.user_id AS UserID,tu.user_code AS UserCode,tu.user_name AS UserName, tu.user_email AS UserEmail";
            sql += " ,tu.user_gender AS UserGender,tu.user_telephone AS UserTelephone,tu.user_cellphone AS UserCellphone,tun.unit_name AS UnitName";
            sql += " ,tun.unit_id AS UnitID,jf.JobFunctionID,jf.Name AS JobFunName,tu.customer_id AS CustomerID ";
            sql += " FROM T_User AS tu INNER JOIN UserDeptJobMapping AS map ON tu.user_id=map.UserID";
            sql += " INNER JOIN t_unit AS tun ON tun.unit_id=map.UnitID ";
            sql += " INNER JOIN JobFunction AS jf ON jf.JobFunctionID=map.JobFunctionID";
            sql += " INNER JOIN T_User_Role AS tur ON tur.user_id=tu.user_id";
            sql += " WHERE tu.customer_id=@customer_id_fir AND tu.user_status=1 AND tun.Status=1 AND jf.Status=1";
            sql += " AND user_name LIKE '%{0}%' AND tur.role_id=(";
            sql += " SELECT TOP 1 tr.role_id FROM T_Role AS tr INNER JOIN dbo.T_Role_Menu AS trm ON tr.role_id=trm.role_id";
            sql += " INNER JOIN T_Menu tm ON tm.menu_id=trm.menu_id WHERE  menu_code=@menu_code AND tm.customer_id=@customer_id_sec)";
            sql = string.Format(sql, pUserName);
            List<SqlParameter> para = new List<SqlParameter>();
            para.Add(new SqlParameter("@customer_id_fir", this.CurrentUserInfo.CurrentUser.customer_id));//'e703dbedadd943abacf864531decdac1'
            para.Add(new SqlParameter("@customer_id_sec", this.CurrentUserInfo.CurrentUser.customer_id));
            para.Add(new SqlParameter("@menu_code", pRightCode));
            ds = this.SQLHelper.ExecuteDataset(CommandType.Text, sql, para.ToArray());
            if (ds != null && ds.Tables != null && ds.Tables.Count > 0)
                return ds.Tables[0];
            return null;
        }
        #endregion
    }
}
