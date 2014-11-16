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
    public class VipClearBLL
    {
        private LoggingSessionInfo CurrentUserInfo;
        public VipClearBLL(LoggingSessionInfo pUserInfo)
        {
            this.CurrentUserInfo = pUserInfo;

        }

        #region GetVipCliearList
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pStartDate"></param>
        /// <param name="pEndDate"></param>
        /// <param name="pPageSize"></param>
        /// <param name="pPageIndex"></param>
        /// <param name="pRowCount"></param>
        /// <returns></returns>
        public PagedQueryResult<VipClearEntity> GetVipCliearList(Dictionary<string, string> pParems, int pPageSize, int pPageIndex, out int pRowCount)
        {
            return new VipCliearDAO(this.CurrentUserInfo).GetVipCliearList(pParems, pPageSize, pPageIndex, out pRowCount);
        }
        #endregion
    }
}
