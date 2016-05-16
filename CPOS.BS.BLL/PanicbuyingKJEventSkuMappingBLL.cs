/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2016/4/27 13:54:52
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
    public partial class PanicbuyingKJEventSkuMappingBLL
    {
        public KJItemSkuInfo GetKJItemSkuInfo(string eventId,string skuId, string KJEventJoinId)
        {
            KJItemSkuInfo KJItemSkuInfo = new KJItemSkuInfo();
            DataSet ds = this._currentDAO.GetKJItemSkuInfo(eventId, skuId, KJEventJoinId);
            if (ds.Tables[0].Rows.Count > 0)
            {
                KJItemSkuInfo = DataTableToObject.ConvertToObject<KJItemSkuInfo>(ds.Tables[0].Rows[0]);
            }
            return KJItemSkuInfo;
        }
        /// <summary>
        /// 获取Sku关系对象配置的底价、原价
        /// </summary>
        /// <param name="EventItemMappingID"></param>
        /// <param name="Code"></param>
        /// <returns></returns>
        public decimal GetConfigPrice(string EventItemMappingID,string Code)
        {
            return this._currentDAO.GetConfigPrice(EventItemMappingID, Code);
        }
    }
}