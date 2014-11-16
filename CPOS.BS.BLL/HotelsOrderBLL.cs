/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/7/21 18:19:53
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
    public partial class HotelsOrderBLL
    {
        /// <summary>
        /// �ҵġ��Ƶ꡿�����б�
        /// </summary>
        /// <param name="pVipId">��ԱID</param>
        /// <param name="pDataRange">ʱ�䷶Χ��1.�������µĶ�����2.������ǰ�Ķ�����</param>
        /// <returns></returns>
        public DataSet GetMyHotelsOrderList(string pVipId,int pDataRange)
        {
           return _currentDAO.GetMyHotelsOrderList(pVipId,pDataRange);
        }
        /// <summary>
        /// �ҵġ��Ƶ꡿�����б���ϸ
        /// </summary>
        /// <param name="pVipId">��ԱID</param>
        /// <param name="pOrderId">����ID</param>
        /// <returns></returns>
        public DataSet GetMyHotelsOrderListDetails(string pVipId, string pOrderId)
        {
            return _currentDAO.GetMyHotelsOrderListDetails(pVipId, pOrderId);
        }
        
    }
}