/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2015/4/15 11:33:15
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
    /// 业务处理：  
    /// </summary>
    public partial class T_ItemSkuPropBLL
    {
        public bool DeleteByItemID(string itemID)
        {
            return _currentDAO.DeleteByItemID(itemID);
        }

        /// <summary>
        /// 根据商品获取Sku
        /// </summary>
        /// <param name="itemId">商品标识</param>
        /// <returns></returns>
        public T_ItemSkuPropInfo GetItemSkuPropByItemId(string itemId)
        {
            T_ItemSkuPropInfo skuInfoList = new T_ItemSkuPropInfo();
            DataSet ds = new DataSet();
            ds = _currentDAO.GetItemSkuPropByItemId(itemId);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                skuInfoList = DataTableToObject.ConvertToObject<T_ItemSkuPropInfo>(ds.Tables[0].Rows[0]);
            }
            return skuInfoList;
        }

    }
}