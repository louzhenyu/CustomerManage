using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using JIT.CPOS.DTO.Base;
using JIT.Utility.ExtensionMethod;

namespace JIT.CPOS.Web.ApplicationInterface.Product.QiXinManage.ActionHandler.QiXinMock
{
    /// <summary>
    /// ModifyUserOrganization的摘要说明
    /// </summary>
    [Export(typeof(IQiXinMockRequestHandler))]
    [ExportMetadata("Action", "ModifyUserOrganization")]
    public class ModifyUserOrganizationHandler : IQiXinMockRequestHandler
    {
        public string DoAction(string pRequest)
        {
            return ModifyUserOrganization(pRequest);
        }

        public string ModifyUserOrganization(string pRequest)
        {
            var rd = new APIResponse<ModifyUserOrganizationRD>();
            var rdData = new ModifyUserOrganizationRD();
            rdData.IsSuccess = true;
            rd.Data = rdData;
            rd.ResultCode = 0;
            return rd.ToJSON();
        }
    }

    #region 变更用户组织信息
    public class ModifyUserOrganizationRP : IAPIRequestParameter
    {
        public string Test { set; get; }
        public void Validate()
        {
        }
    }
    public class ModifyUserOrganizationRD : IAPIResponseData
    {
        public bool IsSuccess { set; get; }
    }
    #endregion
}