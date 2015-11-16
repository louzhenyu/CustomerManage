/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2015-8-14 21:54:10
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
    public partial class VipCardBalanceChangeBLL
    {
        #region ��Ա�����䶯��¼
        /// <summary>
        /// ��ȡ��״̬�����¼��ѯ
        /// </summary>
        /// <param name="VipCardCode">����</param>
        /// <param name="pOrderBys">��������</param>
        /// <param name="pPageSize">Ҷ��С</param>
        /// <param name="pCurrentPageIndex">Ҷ����</param>
        /// <returns>����</returns>
        public PagedQueryResult<VipCardBalanceChangeEntity> GetVipCardBalanceChangeList(string VipCardCode, int pPageSize, int pCurrentPageIndex)
        {
            return this._currentDAO.GetVipCardBalanceChangeList(VipCardCode, pPageSize, pCurrentPageIndex);
        }
        #endregion
    }
}