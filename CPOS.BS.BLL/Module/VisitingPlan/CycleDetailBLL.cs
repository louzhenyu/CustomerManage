/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/4/3 14:19:42
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

using JIT.CPOS.BS.DataAccess.Base;

namespace JIT.CPOS.BS.BLL
{   
    /// <summary>
    /// ҵ���� ѭ������ 
    /// </summary>
    public partial class CycleDetailBLL
    {
        #region GetCycleDetailByCID
        /// <summary>
        /// ͨ��cycleid��ȡ������ϸ��Ϣ
        /// </summary>
        /// <param name="cycleID"></param>
        /// <returns></returns>
        public CycleDetailEntity[] GetCycleDetailByCID(Guid cycleID)
        {
            return new CycleDetailDAO(CurrentUserInfo).GetCycleDetailByCID(cycleID);
        }
        #endregion
    }
}