/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2015/9/10 18:14:44
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
    public partial class t_award_poolBLL
    {  
        //public DataSet CheckUserIsWinner(string strVipId, string strEventId)
        //{
        //    this._currentDAO.CheckUserIsWinner(strVipId, strEventId);
        //}
        /// <summary>
        /// 根据EventId 获取PrizeId
        /// </summary>
        /// <param name="strEventId"></param>
        /// <returns></returns>
        //public t_award_poolEntity GetPrizeByEventId(string strEventId)
        //{
        //    return DataTableToObject.ConvertToObject<t_award_poolEntity>(this._currentDAO.GetPrizeByEventId(strEventId).Tables[0].Rows[0]);
        //}
        public DataSet GetPrizeByEventId(string strEventId)
        {
            return this._currentDAO.GetPrizeByEventId(strEventId);
        }

    }
}