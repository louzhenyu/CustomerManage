/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2016/6/27 14:14:27
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
    /// ҵ���� 10��֧���ɹ�   90��֧��ʧ�� 
    /// </summary>
    public partial class ReceiveAmountOrderBLL
    {

        /// <summary>
        /// ��ȡ��������,Ӧ���ܽ�ʵ���ܽ��
        /// </summary>
        /// <param name="ServiceUserId"></param>
        /// <returns></returns>
        public List<ReceiveAmountOrderEntity> GetOrderCount(string ServiceUserId)
        {
            return _currentDAO.GetOrderCount(ServiceUserId);
        }
    }
}