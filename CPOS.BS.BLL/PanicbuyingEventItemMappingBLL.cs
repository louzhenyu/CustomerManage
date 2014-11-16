/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/7/25 13:51:20
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
    public partial class PanicbuyingEventItemMappingBLL
    {

        public int GteDisIndex(string EventId)
        {
            //string str = "select MAX(isnull(DisplayIndex,0)) DisplayIndex from PanicbuyingEventItemMapping where IsDelete=0 and EventId='"+EventId+"'";

            return this._currentDAO.GteDisIndex(EventId);
        }
        /// <summary>
        /// 获取商品名称和限购数量
        /// </summary>
        /// <param name="eventId"></param>
        /// <param name="itemId"></param>
        /// <returns></returns>
        public DataSet GetEventItemInfo(string eventId, string itemId)
        {
            return this._currentDAO.GetEventItemInfo(eventId, itemId);
        }
    }
}