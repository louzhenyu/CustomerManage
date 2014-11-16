/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/5/5 14:58:10
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
    public partial class AttributeFormBLL
    {
        public AttributeFormEntity[] GetAttributeFormList(string pName, int? pOperationTypeID, int? pStatus,
            int? pAttributeTypeID, int? pPageIndex, int? pPageSize, out int pPageCount)
        {

            if (pPageIndex == null || pPageIndex < 0)
            {
                pPageIndex = 0;
            }
            if (pPageSize == null || pPageSize <= 0)
            {
                pPageSize = 15;
            }
            return this._currentDAO.GetAttributeFormList(pName, pOperationTypeID, pStatus, pAttributeTypeID, pPageIndex,
                pPageSize, out pPageCount);
        }
    }
}