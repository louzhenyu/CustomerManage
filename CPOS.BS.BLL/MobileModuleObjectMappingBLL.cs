/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/4/18 14:05:11
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
using JIT.CPOS.Common;

namespace JIT.CPOS.BS.BLL
{
    /// <summary>
    /// ҵ������  
    /// </summary>
    public partial class MobileModuleObjectMappingBLL
    {
        internal bool DynamicVipFormSceneSave(MobileModuleBLL.DynamicVipFormSceneSaveRP dynamicVipFormSceneSaveRP)
        {
            bool result = false;

            DataTable dataTable = Utils.ToDataTable(dynamicVipFormSceneSaveRP.SceneList);
            dataTable.Columns.Add("Column4");
            dataTable.Columns.Add("Column5");
            //dataTable.Columns.Add("Column6");

            _currentDAO.DynamicVipFormSceneSave(dynamicVipFormSceneSaveRP.FormID, dataTable);

            return result;
        }
    }
}