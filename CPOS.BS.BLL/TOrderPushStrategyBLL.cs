/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/9/1 18:49:15
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
    public partial class TOrderPushStrategyBLL
    {

        #region ��ȡ�ͻ������б�
       /// <summary>
        /// ��ȡ�ͻ������б�
       /// </summary>
        /// <param name="pCustomer_id">�ͻ�ID ���� f6a7da3d28f74f2abedfc3ea0cf65c01</param>
       /// <param name="pOrderStatus">����״̬</param>
       /// <returns>��ǰ�ͻ���Ӧ�����Ĳ����б�</returns>
        public IList<TOrderPushStrategyEntity> GetTOrderPushStrategyEntityList(string pCustomer_id,string pOrderStatus)
        {
          return  _currentDAO.GetTOrderPushStrategyEntityList(pCustomer_id,pOrderStatus);
        }
        #endregion

    }
}