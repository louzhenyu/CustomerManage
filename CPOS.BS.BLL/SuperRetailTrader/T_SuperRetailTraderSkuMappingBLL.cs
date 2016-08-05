/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2016/5/28 10:12:43
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
    public partial class T_SuperRetailTraderSkuMappingBLL
    {
        /// <summary>
        /// ��ȡ��Ʒ��Sku�б�
        /// </summary>
        /// <param name="ItemName"></param>
        /// <param name="itemCategoryId"></param>
        /// <returns></returns>
        public PagedQueryResult<T_SuperRetailTraderSkuMappingEntity> GetSkuList(string ItemName, string itemCategoryId, int pageSize, int pageIndex)
        {
            return _currentDAO.GetSkuList(ItemName, itemCategoryId, pageSize, pageIndex);
        }
        /// <summary>
        /// ��ȡ��������Ʒ��Sku�б�
        /// </summary>
        /// <param name="itemName"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public PagedQueryResult<T_SuperRetailTraderSkuMappingEntity> GetSuperRetailTraderSkuList(string itemCategoryId, string itemName, int status, int pageSize, int pageIndex)
        {
            return _currentDAO.GetSuperRetailTraderSkuList(itemCategoryId,itemName,status,pageSize,pageIndex);
        }
        /// <summary>
        /// ��ȡSku��ϸ��Ϣ
        /// </summary>
        /// <param name="itemId"></param>
        /// <returns></returns>
        public List<T_SuperRetailTraderSkuMappingEntity> GetSuperRetailTraderSkuDetail(string itemId,string customerId)
        {
            return _currentDAO.GetSuperRetailTraderSkuDetail(itemId, customerId);
        }

        /// <summary>
        /// ��ȡ����������Sku�۸�
        /// </summary>
        /// <param name="skuId"></param>
        /// <returns></returns>
        public List<SkuPrice> GetSuperRetailTraderSkuPrice(string skuId,string customerId)
        {
            return _currentDAO.GetSuperRetailTraderSkuPrice(skuId,customerId);
        }
    }
}