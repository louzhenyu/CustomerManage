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
using System.Linq;
using System.Text;
using System.Data;
using System.Reflection;
using JIT.Utility;
using JIT.Utility.ExtensionMethod;
using JIT.CPOS.BS.DataAccess;
using JIT.CPOS.BS.Entity;
using JIT.Utility.DataAccess;
using JIT.Utility.DataAccess.Query;
using System.Data.SqlClient;

namespace JIT.CPOS.BS.BLL
{
    /// <summary>
    /// ҵ����  
    /// </summary>
    public partial class EclubValidationBLL
    {

        #region ����ָ���û���֤�����
        /// <summary>
        /// ����ָ���û���֤�����
        /// </summary>
        /// <param name="vipID">�û�ID</param>
        /// <param name="tran"></param>
        public void TimeOutByValidation(string vipID, SqlTransaction tran = null)
        {
            _currentDAO.TimeOutByValidation(vipID, tran);
        }
        #endregion

        //��ȡ��ǰ��¼
        public object GetUserInfo(string userName, string password)
        {
            //У���û���¼��Ϣ
            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password))
            {
                return null;
            }
            return _currentDAO.GetUserInfo(userName, password);
        }
        /// <summary>
        /// ��ȡ�û�ͨ��֤��VipId
        /// </summary>
        /// <param name="email">����</param>
        /// <returns>ͨ��֤</returns>
        public object GetUserID(string email)
        {
            return _currentDAO.GetUserID(email);
        }
    }
}