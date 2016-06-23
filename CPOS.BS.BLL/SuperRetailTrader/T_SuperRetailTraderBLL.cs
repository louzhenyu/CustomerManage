/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2016/5/31 9:08:39
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
    public partial class T_SuperRetailTraderBLL
    {
        /// <summary>
        /// ��ȡ��������Ϣ {ע���Ϊ������֮ǰ����ϢҪĬ�Ϲ��˵�}
        /// </summary>
        /// <param name="customerId">�̻����</param>
        /// <param name="pWhereConditions">��������</param>
        /// <param name="pOrderBys">��������</param>
        /// <param name="PageIndex">��ǰҳ��</param>
        /// <param name="PageSize">ÿҳ��ʾ����</param>
        /// <returns></returns>
        public PagedQueryResult<T_SuperRetailTraderEntity> FindListByCustomerId(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys, int PageIndex, int PageSize, string CustomerId)
        {
            return this._currentDAO.FindListByCustomerId(pWhereConditions, pOrderBys, PageIndex, PageSize, CustomerId);
        }


        /// <summary>
        /// ��ȡ��������Ϣ {ע���Ϊ������֮ǰ����ϢҪĬ�Ϲ��˵�} ���� ����Ҫ��ҳ��ʾ
        /// </summary>
        /// <param name="customerId">�̻����</param>
        /// <param name="pWhereConditions">��������</param>
        /// <param name="pOrderBys">��������</param>
        /// <returns></returns>
        public PagedQueryResult<T_SuperRetailTraderEntity> FindListByCustomerId(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys, string CustomerId)
        {
            return this._currentDAO.FindListByCustomerId(pWhereConditions, pOrderBys, CustomerId);
        }
        public DataSet GetAllFather(string strSuperRetailTraderId)
        {
            return this._currentDAO.GetAllFather(strSuperRetailTraderId);

        }
    }
}