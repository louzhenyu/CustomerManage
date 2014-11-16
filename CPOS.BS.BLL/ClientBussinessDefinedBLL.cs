/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/8/7 14:57:03
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
using JIT.CPOS.DTO.Base;
using JIT.CPOS.Common;

namespace JIT.CPOS.BS.BLL
{
    /// <summary>
    /// 业务处理：  
    /// </summary>
    public partial class ClientBussinessDefinedBLL
    {
        #region RequestParameter
        public class DynamicVipPropertySaveRP : IAPIRequestParameter
        {
            public string PropertyID { get; set; }
            public string DisplayType { get; set; }
            public string PropertyName { get; set; }
            public JIT.CPOS.BS.BLL.OptionsBLL.Option[] OptionList { get; set; }

            public void Validate()
            {
                //if (string.IsNullOrEmpty(PropertyID))
                //    throw new APIException(201, "属性ID不能为空！");
            }
        }
        #endregion

        public string DynamicVipPropertySave(DynamicVipPropertySaveRP dynamicVipPropertySaveRP)
        {
            string result = "";

            DataTable dataTable = Utils.TableParameterCommon;
            foreach (var item in dynamicVipPropertySaveRP.OptionList)
            {
                DataRow dr = dataTable.NewRow();
                dr["Column1"] = item.OptionText;
                dataTable.Rows.Add(dr);
            }

            result = this._currentDAO.DynamicVipPropertySave(dynamicVipPropertySaveRP.PropertyID, dynamicVipPropertySaveRP.DisplayType, dynamicVipPropertySaveRP.PropertyName, dataTable);

            return result;
        }
    }
}