
/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2015-06-08 20:59:54
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
using JIT.Utility.DataAccess;
using JIT.Utility.DataAccess.Query;
using JIT.CPOS.BS.Entity;

namespace JIT.CPOS.BS.BLL
{
    /// <summary>
    /// ҵ����  
    /// </summary>
    public partial class T_UserBLL
    {
        /// <summary>
        /// �õ��ŵ�Ա���б�
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="unitId"></param>
        /// <returns></returns>
        public T_UserEntity[] GetEntitiesByCustomerIdUnitId(string customerId, string unitId)
        {
            //��ѯ
            return _currentDAO.GetEntitiesByCustomerIdUnitId(customerId, unitId);
        }
    }
}