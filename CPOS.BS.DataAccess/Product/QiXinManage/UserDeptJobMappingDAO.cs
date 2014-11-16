/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/7/25 16:26:27
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
    /// ��UserDeptJobMapping�����ݷ����� 
    /// TODO:
    /// 1.ʵ��ICRUDable�ӿ�
    /// 2.ʵ��IQueryable�ӿ�
    /// 3.ʵ��Load����
    /// </summary>
    public partial class UserDeptJobMappingDAO : Base.BaseCPOSDAO, ICRUDable<UserDeptJobMappingEntity>, IQueryable<UserDeptJobMappingEntity>
    {
        public UserDeptJobMappingEntity GetByUserID(object pUserID)
        {
            //�������
            if (pUserID == null)
                return null;
            string id = pUserID.ToString();
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [UserDeptJobMapping] where UserID='{0}' and IsDelete=0 ", id.ToString());
            //��ȡ����
            UserDeptJobMappingEntity m = null;
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
        /// <summary>
        /// ��ȡ����ֱ�ӳ�Ա
        /// </summary>
        /// <param name="pUnitID"></param>
        /// <returns></returns>
        public DataTable GetDirectPersMembers(object pUnitID)
        {
            string sql = "SELECT tu.user_id AS UserID,tu.user_code AS UserCode,tu.user_name AS UserName, tu.user_email AS UserEmail";
            sql += " ,tu.user_gender AS UserGender,tu.user_telephone AS UserTelephone,tu.user_cellphone AS UserCellphone";
            sql += " ,tun.unit_name AS UnitName,tun.unit_id AS UnitID,jf.JobFunctionID,jf.Name AS JobFuncName,tu.customer_id AS CustomerID ";
            sql += " FROM dbo.T_User AS tu INNER JOIN dbo.UserDeptJobMapping AS map ON tu.user_id=map.UserID";
            sql += " INNER JOIN dbo.t_unit AS tun ON tun.unit_id=map.UnitID ";
            sql += " INNER JOIN dbo.JobFunction AS jf ON jf.JobFunctionID=map.JobFunctionID";
            sql += " WHERE UnitID=@UnitID AND tu.customer_id=@customer_id ";
            sql += " AND tu.user_status=1 AND tun.Status=1 AND jf.Status=1";
            List<SqlParameter> para = new List<SqlParameter>();
            para.Add(new SqlParameter("@UnitID", pUnitID));
            para.Add(new SqlParameter("@customer_id", this.CurrentUserInfo.CurrentUser.customer_id));
            DataSet ds = this.SQLHelper.ExecuteDataset(CommandType.Text, sql, para.ToArray());
            if (ds != null && ds.Tables != null && ds.Tables.Count > 0)
                return ds.Tables[0];
            return null;
        }
    }
}
