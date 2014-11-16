/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/12/27 14:03:38
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
    /// 数据访问：  
    /// 表ItemCategoryMapping的数据访问类 
    /// TODO:
    /// 1.实现ICRUDable接口
    /// 2.实现IQueryable接口
    /// 3.实现Load方法
    /// </summary>
    public partial class ItemCategoryMappingDAO : Base.BaseCPOSDAO, ICRUDable<ItemCategoryMappingEntity>, IQueryable<ItemCategoryMappingEntity>
    {
        #region 根据商品获取集合
        /// <summary>
        /// 根据商品获取集合
        /// </summary>
        /// <param name="item_id"></param>
        /// <returns></returns>
        public DataSet GetItemCategoryListByItemId(string itemId)
        {
            DataSet ds = new DataSet();
            string sql = "select a.* "
                      + " ,(select ItemTagName from TItemTag x where CAST(x.ItemTagID AS VARCHAR(50)) = a.ItemCategoryId) ItemCategoryName "
                      + " From ItemCategoryMapping a  where a.itemId= '" + itemId + "' "
                      + " and a.isDelete = '0' ";
            //+ " and a.CustomerId = '" + this.loggingSessionInfo.CurrentLoggingManager.Customer_Id.ToString() + "'  ";

            ds = this.SQLHelper.ExecuteDataset(sql);
            return ds;

        }
        #endregion
    }
}
