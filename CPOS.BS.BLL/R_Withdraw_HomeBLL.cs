/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2016/6/22 17:09:07
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
    public partial class R_Withdraw_HomeBLL
    {
        /// <summary>
        /// �����̻������ ��Ա���� ��ȡ���һ��ͳ����Ϣ
        /// </summary>
        /// <param name="CustomerId">�̻�����</param>
        /// <param name="VipTypeId">1=��Ա 2=Ա�� 3=�ɷ����� 4=����������</param>
        /// <returns></returns>
        public R_Withdraw_HomeEntity GetTopListByCustomer(string CustomerId, int VipTypeId)
        {
            return _currentDAO.GetTopListByCustomer(CustomerId, VipTypeId);
        }
    }
}