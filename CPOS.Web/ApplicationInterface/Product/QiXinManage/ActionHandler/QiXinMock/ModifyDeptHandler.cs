using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using JIT.CPOS.DTO.Base;
using JIT.Utility.ExtensionMethod;

namespace JIT.CPOS.Web.ApplicationInterface.Product.QiXinManage.ActionHandler.QiXinMock
{
    /// <summary>
    /// ModifyDept的摘要说明
    /// </summary>
    [Export(typeof(IQiXinMockRequestHandler))]
    [ExportMetadata("Action", "ModifyDept")]
    public class ModifyDeptHandler : IQiXinMockRequestHandler
    {
        public string DoAction(string pRequest)
        {
            return ModifyDept(pRequest);
        }

        public string ModifyDept(string pRequest)
        {
            var rd = new APIResponse<ModifyDeptRD>();
            var rdData = new ModifyDeptRD();
            rdData.IsSuccess = true;
            rd.Data = rdData;
            rd.ResultCode = 0;
            return rd.ToJSON();
        }
    }

    #region 部门信息变更
    public class ModifyDeptRP : IAPIRequestParameter
    {
        public string Test { set; get; }
        public void Validate()
        {
        }
    }
    public class ModifyDeptRD : IAPIResponseData
    {
        public bool IsSuccess { set; get; }
    }
    #endregion
}