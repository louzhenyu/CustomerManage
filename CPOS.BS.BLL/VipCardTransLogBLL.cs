/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2015-8-14 21:54:10
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
using JIT.CPOS.DTO;
using JIT.CPOS.DTO.Module.VIP.VipCardTransLog.Response;

namespace JIT.CPOS.BS.BLL
{
    /// <summary>
    /// 业务处理：  
    /// </summary>
    public partial class VipCardTransLogBLL
    {  

        /// <summary>
        /// 获取会员卡交易记录
        /// </summary>
        /// <param name="VipCardCode"></param>
        /// <returns></returns>
        public List<VipCardTransLogInfo> GetVipCardTransLogList(string VipCardCode)
        {
            var dsTrans = this._currentDAO.GetVipCardTransLogList(VipCardCode);
            return DataTableToObject.ConvertToList<VipCardTransLogInfo>(dsTrans.Tables[0]);
        }
        public void UpdateVipCardTransLog(string p_StrSql)
        {
            this._currentDAO.UpdateVipCardTransLog(p_StrSql);
        }
    }
}