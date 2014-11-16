/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/7/3 18:46:09
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
    /// ��GL_ServicePersonInfo�����ݷ����� 
    /// TODO:
    /// 1.ʵ��ICRUDable�ӿ�
    /// 2.ʵ��IQueryable�ӿ�
    /// 3.ʵ��Load����
    /// </summary>
    public partial class GLServicePersonInfoDAO : Base.BaseCPOSDAO, ICRUDable<GLServicePersonInfoEntity>, IQueryable<GLServicePersonInfoEntity>
    {

        /// <summary>
        /// ��ȡʦ����Ϣ
        /// </summary>
        /// <param name="pCustomerID"></param>
        /// <param name="pServicePersonID"></param>
        /// <returns></returns>
        public DataSet GetServicePerson(string pCustomerID, string pServicePersonID)
        {
            string sql = "SELECT UserID AS ServicePersonID,Name,Mobile,Picture,Star,OrderCount,CustomerID";
            sql += " FROM cpos_demo.dbo.GL_ServicePersonInfo AS glspi";
            sql += " WHERE CustomerID=@CustomerID AND IsDelete=0";

            List<SqlParameter> para = new List<SqlParameter>();
            para.Add(new SqlParameter("@CustomerID", pCustomerID));
            if (!string.IsNullOrEmpty(pServicePersonID))
            {
                sql += " AND UserID=@ServicePersonID";
                para.Add(new SqlParameter("@ServicePersonID", pServicePersonID));
            }
            return this.SQLHelper.ExecuteDataset(CommandType.Text, sql.ToString(), para.ToArray());
        }
    }
}
