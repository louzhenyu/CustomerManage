/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/10/28 15:52:20
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
    public partial class ESalesBLL
    {
        #region ����ϵͳ�ӿ�
        #region ��ѯ
        /// <summary>
        /// ��ѯ
        /// </summary>
        /// <param name="salesInfo">��������</param>
        /// <param name="Page">ҳ��</param>
        /// <param name="PageSize">ҳ������</param>
        /// <param name="TotalCount">���ؽ����������</param>
        /// <returns></returns>
        public IList<ESalesEntity> GetSalesList(ESalesEntity salesInfo, int Page, int PageSize, out int TotalCount)
        {
            IList<ESalesEntity> list = new List<ESalesEntity>();
            TotalCount = 0;
            if (PageSize <= 0) PageSize = 15;
            DataSet ds = new DataSet();
            ds = _currentDAO.GetSalesList(salesInfo, Page, PageSize,out TotalCount);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                list = DataTableToObject.ConvertToList<ESalesEntity>(ds.Tables[0]);
            }
            return list;
        }
        #endregion

        #region ��ȡ���۸����˼���
        /// <summary>
        /// ��ȡ���۸����˼���
        /// </summary>
        /// <returns></returns>
        public DataSet GetESalesChargeVipList()
        {
            return _currentDAO.GetESalesChargeVipList();
        }
        #endregion
        #endregion
    }
}