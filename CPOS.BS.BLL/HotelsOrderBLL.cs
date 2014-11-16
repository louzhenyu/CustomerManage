/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/7/21 18:19:53
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
    public partial class HotelsOrderBLL
    {
        /// <summary>
        /// 我的【酒店】订单列表
        /// </summary>
        /// <param name="pVipId">会员ID</param>
        /// <param name="pDataRange">时间范围【1.近三个月的订单。2.三个月前的订单】</param>
        /// <returns></returns>
        public DataSet GetMyHotelsOrderList(string pVipId,int pDataRange)
        {
           return _currentDAO.GetMyHotelsOrderList(pVipId,pDataRange);
        }
        /// <summary>
        /// 我的【酒店】订单列表明细
        /// </summary>
        /// <param name="pVipId">会员ID</param>
        /// <param name="pOrderId">订单ID</param>
        /// <returns></returns>
        public DataSet GetMyHotelsOrderListDetails(string pVipId, string pOrderId)
        {
            return _currentDAO.GetMyHotelsOrderListDetails(pVipId, pOrderId);
        }
        
    }
}