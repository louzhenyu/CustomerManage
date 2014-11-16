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

namespace JIT.CPOS.BS.BLL
{
    /// <summary>
    /// ҵ����  
    /// </summary>
    public partial class T_UserBLL
    {
        #region ��ȡ��̨��������Ϣ
        /// <summary>
        /// ��ȡ��̨��������Ϣ
        /// </summary>
        /// <param name="pCustomerID"></param>
        /// <param name="pEmail"></param>
        /// <returns></returns>
        public DataSet ManageUserInfo(string pEmail)
        {
            return _currentDAO.ManageUserInfo(pEmail);
        }
        #endregion

        #region ����ӵ�н�ȺȨ�޵�Ա��-���ź�̨
        /// <summary>
        /// ����ӵ�н�ȺȨ�޵�Ա��
        /// </summary>
        /// <param name="pGroupName"></param>
        /// <returns></returns>
        public DataTable GetIMGroupCreatorByUserName(string pUserName, string pRightCode)
        {
            return this._currentDAO.GetIMGroupCreatorByUserName(pUserName, pRightCode);
        }
        #endregion
    }
}