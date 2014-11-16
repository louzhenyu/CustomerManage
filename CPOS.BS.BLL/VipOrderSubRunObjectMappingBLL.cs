/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/9/18 10:28:46
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
    public partial class VipOrderSubRunObjectMappingBLL
    {
        /// <summary>
        /// �����Ա����󷽹�ϵ�洢����
        /// </summary>
        /// <param name="customerId">�ͻ�ID</param>
        /// <param name="vipId">��ԱID</param>
        /// <param name="subRunObjectId">���󷽱�ʶ
        /// ��ѡֵΪ��
        /// 1�����߻�Ա��2�������Ա��3���Ἦ�ꣻ
        /// </param>
        /// <param name="subRunObjectValue">
        /// ��Ӧ����ֵ,���ݷ��󷽱�ʶ��
        /// ��ʶΪ1ʱ��Ϊ��Ӧ���߻�ԱID;
        /// Ϊ2ʱ��Ϊ��Ӧ�����ԱID;
        /// Ϊ3ʱ��Ϊ��Ӧ�Ἦ��ID;
        /// </param>
        /// <returns></returns>
        public dynamic SetVipOrderSubRun(string customerId, string vipId,
            int subRunObjectId, string subRunObjectValue)
        {
             return this._currentDAO.SetVipOrderSubRun(customerId, vipId, subRunObjectId, subRunObjectValue);
        }
    }
}