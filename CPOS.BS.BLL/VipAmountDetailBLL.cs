/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/5/23 11:40:35
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
using System.Data.SqlClient;
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
    public partial class VipAmountDetailBLL
    {
        /// <summary>
        /// ��ȡ����ʹ�õ����֧��/�����ҵֿ۽�� update by Henry 2014-10-13
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="userId"></param>
        /// <param name="amountSourceId"></param>
        /// <returns></returns>
        public decimal GetVipAmountByOrderId(string orderId, string userId, int amountSourceId)
        {
            return this._currentDAO.GetVipAmountByOrderId(orderId, userId, amountSourceId);
        }
        
        /// <summary>
        /// ��ȡ���/���ֱ����ϸ
        /// </summary>
        /// <param name="pWhereConditions">ɸѡ����</param>
        /// <param name="pOrderBys">����</param>
        /// <param name="pPageSize">ÿҳ�ļ�¼��</param>
        /// <param name="pCurrentPageIndex">��0��ʼ�ĵ�ǰҳ��</param>
        /// <returns></returns>
        public PagedQueryResult<VipAmountDetailEntity> GetVipAmountDetailList(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
        {
            return this._currentDAO.GetVipAmountDetailList(pWhereConditions, pOrderBys, pPageSize, pCurrentPageIndex);
        }
    }
}