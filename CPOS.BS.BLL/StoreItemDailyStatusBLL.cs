/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/1/18 19:10:46
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
    /// 业务处理：  
    /// </summary>
    public partial class StoreItemDailyStatusBLL
    {
        /// <summary>
        /// 批量更新房态信息
        /// </summary>
        /// <param name="dailyStatusEntities"></param>
        public void Update(IList<StoreItemDailyStatusEntity> dailyStatusEntities, string oLowestPrice, string oSourcePrice)
        {
            if (dailyStatusEntities != null)
            {
                foreach (var entity in dailyStatusEntities)
                {
                    var query =
                        this.QueryByEntity(
                            new StoreItemDailyStatusEntity { StatusDate = entity.StatusDate, StoreID = entity.StoreID, SkuID = entity.SkuID }, null);
                    if (query.Length > 0)  //如果有这个对象就更新，否则就添加
                    {
                        entity.StoreItemDailyStatusID = query[0].StoreItemDailyStatusID;//query[0]是查到的数据
                        if (string.IsNullOrEmpty(oLowestPrice))//如果为空
                        {
                            entity.LowestPrice = query[0].LowestPrice == null ? null : query[0].LowestPrice;
                        }
                        if (string.IsNullOrEmpty(oSourcePrice))//如果为空
                        {
                            entity.SourcePrice = query[0].SourcePrice == null ? null : query[0].SourcePrice;
                        }
                        Update(entity);
                    }
                    else
                    {
                        entity.StoreItemDailyStatusID = Guid.NewGuid();
                        Create(entity);
                    }
                }
            }
        }

        public IList<StoreItemDailyStatusEntity> GetList(string beginDate, string endDate, string storeID, string skuID, int pageIndex, int pageSize, out int totalCount)
        {
            DataSet ds = _currentDAO.GetList(beginDate, endDate, storeID, skuID,
                                       pageIndex, pageSize, out totalCount);
            return DataTableToObject.ConvertToList<StoreItemDailyStatusEntity>(ds.Tables[0]);
        }

        /// <summary>
        /// 获取 商品名称，单品ID
        /// </summary>
        /// <returns></returns>
        public DataTable GetItemList(string storeID)
        {
            return _currentDAO.GetItemList(storeID);
        }

          /// <summary>
        /// 获取终端列表
        /// </summary>
        /// <returns></returns>
        public DataTable GetStoreList()
        {
            return _currentDAO.GetStoreList();
        }
    }
}