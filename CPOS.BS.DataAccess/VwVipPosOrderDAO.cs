/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/11/7 15:23:36
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
using System.Data;
using System.Data.SqlClient;
using System.Text;

using JIT.Utility;
using JIT.Utility.Entity;
using JIT.Utility.ExtensionMethod;
using JIT.Utility.DataAccess;
using JIT.Utility.Log;
using JIT.CPOS.BS.Entity;
using JIT.Utility.DataAccess.Query;
using JIT.CPOS.BS.DataAccess.Base;

namespace JIT.CPOS.BS.DataAccess
{
    
    /// <summary>
    /// ���ݷ��ʣ�  
    /// ��vwVipPosOrder�����ݷ����� 
    /// TODO:
    /// 1.ʵ��ICRUDable�ӿ�
    /// 2.ʵ��IQueryable�ӿ�
    /// 3.ʵ��Load����
    /// </summary>
    public partial class VwVipPosOrderDAO : Base.BaseCPOSDAO, ICRUDable<VwVipPosOrderEntity>, IQueryable<VwVipPosOrderEntity>
    {
        #region ��ȡ��ҳ��������̳Ƕ���
        public DataSet GetPosOrderList(string UnitId, string StatusId)
        {
            DataSet ds = new DataSet();
            string sql = string.Empty;
            if (StatusId.Equals("1"))
            {
                sql = "select * From vwVipPosOrder a where (a.SalesUnitId = '" + UnitId + "' OR a.PurchaseUnitId = '" + UnitId + "') and a.statusId in ('1','2') and IsDelete = '0' order by a.CreateTime desc ";
            }
            else {
                sql = "select * From vwVipPosOrder a where (a.SalesUnitId = '" + UnitId + "' OR a.PurchaseUnitId = '" + UnitId + "') and a.statusId in ('3') and IsDelete = '0' order by a.CreateTime desc ";
            }
            ds = this.SQLHelper.ExecuteDataset(sql);
            return ds;
        }
        #endregion
    }
}
