/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013-05-31 20:42
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
using JIT.CPOS.BS.Entity;
using JIT.CPOS.BS.DataAccess;
using JIT.Utility.DataAccess;
using JIT.Utility.DataAccess.Query;

namespace JIT.CPOS.BS.BLL
{
    /// <summary>
    /// ҵ����  
    /// </summary>
    public partial class AlipayWapTradeResponseBLL
    {  
        /// <summary>
        /// �����̻���վΨһ�����Ÿ���֧�������׼�¼
        /// </summary>
        /// <param name="pEntity"></param>
        public void UpdateAlipayWapTrade(AlipayWapTradeResponseEntity pEntity)
        {
            this._currentDAO.UpdateAlipayWapTrade(pEntity, false);
        }

        /// <summary>
        /// �����̻���վΨһ�����Ÿ���֧��������״̬
        /// </summary>
        /// <param name="outTradeNo">�̻���վΨһ������</param>
        /// <param name="status">״ֵ̬ 1����������  2�����׳ɹ�  3������ʧ��</param>
        public void UpdateAlipayWapTradeStatus(string outTradeNo, string status)
        {
            this._currentDAO.UpdateAlipayWapTradeStatus(outTradeNo, status);
        }
    }
}