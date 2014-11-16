/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/6/17 14:37:48
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
    /// ��PgUser�����ݷ����� 
    /// TODO:
    /// 1.ʵ��ICRUDable�ӿ�
    /// 2.ʵ��IQueryable�ӿ�
    /// 3.ʵ��Load����
    /// </summary>
    public partial class PgUserDAO : Base.BaseCPOSDAO, ICRUDable<PgUserEntity>, IQueryable<PgUserEntity>
    {
        #region ��֤�û��Ƿ����ض����еĹ�����ϯ
        /// <summary>
        /// ��֤�û��Ƿ����ض����еĹ�����ϯ
        /// ������ϯ��ʶ��LocalLUOwner
        /// </summary>
        /// <param name="pUserID"></param>
        /// <param name="pCustomerID"></param>
        /// <param name="pCityName"></param>
        /// <returns></returns>
        public bool VerifyIsLocalLuOwner(string pUserID, string pCustomerID, string pCityName)
        {
            string sql = "IF EXISTS (SELECT * FROM PgUser pu INNER JOIN City AS city ON pu.City=city.CityID";
            sql += " WHERE CustomerID=@CustomerID AND USER_ID=@USER_ID AND city.CityName=@CityName AND SpecialTitle='LocalLUOwner' AND city.IsDelete=0 ) ";
            sql += "SELECT 'true' ELSE SELECT 'false' ";
            List<SqlParameter> para = new List<SqlParameter>();
            para.Add(new SqlParameter("@CustomerID", pCustomerID));
            para.Add(new SqlParameter("@USER_ID", pUserID));
            para.Add(new SqlParameter("@CityName", pCityName));
            object obj = this.SQLHelper.ExecuteScalar(CommandType.Text, sql, para.ToArray());
            return Convert.ToBoolean(obj);
        }
        #endregion
    }
}
