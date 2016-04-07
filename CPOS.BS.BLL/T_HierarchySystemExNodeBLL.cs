/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2016/4/6 17:09:41
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
    /// ҵ����  
    /// </summary>
    public partial class T_HierarchySystemExNodeBLL
    {
        /// <summary>
        /// ������������չ��¼
        /// </summary>
        /// <param name="RetailTraderID"></param>
        public void AddHierarchySystemExNode(string RetailTraderID)
        {
            var Data = new T_HierarchySystemExNodeEntity();
            Data.HierarchySystemConfigId = null;
            Data.RetailTraderID = RetailTraderID;
            Data.CustomerId = CurrentUserInfo.ClientID;
            this._currentDAO.Create(Data);
        }
    }
}