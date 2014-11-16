/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/6/14 14:03:25
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
    public partial class PowerHourInviteBLL
    {
        /// <summary>
        /// 检查当前用户是否参加了培训。
        /// </summary>
        /// <param name="customerID"></param>
        /// <param name="powerHouerID"></param>
        /// <returns></returns>
        public int GetPowerHourAttendence(string customerID, string powerHouerID, string staffUserID)
        {
            return _currentDAO.GetPowerHourAttendence(customerID, powerHouerID, staffUserID);
        }

        /// <summary>
        /// 检查当前用户是否收到了讲座邀请。
        /// </summary>
        /// <param name="customerID"></param>
        /// <param name="powerHouerID"></param>
        /// <param name="staffUserID"></param>
        /// <returns></returns>
        public PowerHourInviteEntity GetBeforeUserInvite(string customerID, string powerHouerID, string staffUserID)
        {
            return _currentDAO.GetBeforeUserInvite(customerID, powerHouerID, staffUserID);
        }

        public PowerHourInviteEntity VerifyPowerHourInvite(string pCustomerID, string pPowerHouerID, string pStaffUserID)
        {
            return _currentDAO.VerifyPowerHourInvite(pCustomerID, pPowerHouerID, pStaffUserID);
        }
    }
}