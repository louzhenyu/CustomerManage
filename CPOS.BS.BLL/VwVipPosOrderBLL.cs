/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/11/7 15:18:24
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
    public partial class VwVipPosOrderBLL
    {
        #region ��ȡ��ҳ��������̳Ƕ���
        /// <summary>
        /// ��ȡ��ҳ��������̳Ƕ���
        /// </summary>
        /// <param name="UnitId">�ŵ��ʶ</param>
        /// <param name="StatusId">״̬</param>
        /// <returns></returns>
        public IList<VwVipPosOrderEntity> GetPosOrderList(string UnitId, string StatusId)
        {
            IList<VwVipPosOrderEntity> list = new List<VwVipPosOrderEntity>();
            DataSet ds = new DataSet();
            ds = _currentDAO.GetPosOrderList(UnitId, StatusId);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                list = DataTableToObject.ConvertToList<VwVipPosOrderEntity>(ds.Tables[0]);
            }
            return list;
        }
        #endregion
    }
}