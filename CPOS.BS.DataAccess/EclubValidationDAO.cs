/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/5/19 13:53:57
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
    /// ��EclubValidation�����ݷ����� 
    /// TODO:
    /// 1.ʵ��ICRUDable�ӿ�
    /// 2.ʵ��IQueryable�ӿ�
    /// 3.ʵ��Load����
    /// </summary>
    public partial class EclubValidationDAO : Base.BaseCPOSDAO, ICRUDable<EclubValidationEntity>, IQueryable<EclubValidationEntity>
    {

        #region ����ָ���û���֤�����
        /// <summary>
        /// ����ָ���û���֤�����
        /// </summary>
        /// <param name="vipID">�û�ID</param>
        /// <param name="tran"></param>
        public void TimeOutByValidation(string vipID, SqlTransaction tran)
        {
            string sql = string.Format("update EclubValidation set LoginStatus=2 where LoginStatus=0 and VipID='{0}'", vipID);
            if (tran == null)
                this.SQLHelper.ExecuteNonQuery(sql);
            else
                this.SQLHelper.ExecuteNonQuery(tran, CommandType.Text, sql);
        }
        #endregion


        //��ȡ��ǰ��¼
        public object GetUserInfo(string userName, string password)
        {
            //Create SQL Text
            StringBuilder sbSQL = new StringBuilder();
            sbSQL.Append("select top 1 VipID from Vip ");
            sbSQL.AppendFormat("where IsDelete = 0 and ClientID = '{0}' ", CurrentUserInfo.CurrentLoggingManager.Customer_Id);
            sbSQL.AppendFormat("and VipName = '{0}' ", userName);
            sbSQL.AppendFormat("and VipPasswrod='{0}' ;", password);

            //Execute
            return this.SQLHelper.ExecuteScalar(sbSQL.ToString());
        }

        /// <summary>
        /// ��ȡ�û�ͨ��֤��VipId
        /// </summary>
        /// <param name="email">����</param>
        /// <returns>ͨ��֤</returns>
        public object GetUserID(string email)
        {
            //Create SQL Text
            StringBuilder sbSQL = new StringBuilder();
            sbSQL.Append("select top 1 VipID from Vip ");
            sbSQL.AppendFormat("where IsDelete = 0 and ClientID = '{0}' ", CurrentUserInfo.CurrentLoggingManager.Customer_Id);
            sbSQL.AppendFormat("and Col38 = '{0}' ", email);

            //Execute
            return this.SQLHelper.ExecuteScalar(sbSQL.ToString());
        }
    }
}
