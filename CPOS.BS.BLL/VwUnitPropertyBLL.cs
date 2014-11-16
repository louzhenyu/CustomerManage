/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/9/10 10:27:47
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
    public partial class VwUnitPropertyBLL
    {
        #region ���ݶ�����ʶ����ȡ�ŵ꼯��
        public IList<VwUnitPropertyEntity> GetUnitPropertyByOrderId(string OrderId)
        {
            IList<VwUnitPropertyEntity> list = new List<VwUnitPropertyEntity>();
            DataSet ds = new DataSet();
            ds = _currentDAO.GetUnitPropertyByOrderId(OrderId);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                list = DataTableToObject.ConvertToList<VwUnitPropertyEntity>(ds.Tables[0]);
            }
            return list;
        }
        #endregion

        #region ��ȡ�����ŵ��ά��
        /// <summary>
        /// ��ȡ�����ŵ��ά��
        /// </summary>
        /// <returns></returns>
        public int GetUnitWXCode(string UnitId)
        {
            int iCode = 0;
            string weiXinCode = _currentDAO.GetUnitWXCode(UnitId);
            if (weiXinCode == null || weiXinCode.Equals(""))
            {
                iCode = 1;
            }
            else {
                iCode = Convert.ToInt32(weiXinCode) + 1;
            }
            return iCode;
        }
        #endregion
    }
}