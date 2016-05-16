/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2016/2/16 14:37:46
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
    public partial class PanicbuyingKJEventJoinBLL
    {
        public DataSet GetBuyerList(string EventId, string ItemId, int PageSize, int PageIndex)
        {
            return this._currentDAO.GetBuyerList(EventId, ItemId, PageSize, PageIndex);
        }

        public DataSet GetKJEventJoinList(string Vipid, int PageIndex, int PageSize)
        {
           return this._currentDAO.GetKJEventJoinList(Vipid, PageIndex, PageSize);
        }
        /// <summary>
        /// 获取商品图片URL
        /// </summary>
        /// <param name="ItemID"></param>
        /// <returns></returns>
        public string GetItemImageURL(string ItemID) {
            return this._currentDAO.GetItemImageURL(ItemID);
        }
    }
}