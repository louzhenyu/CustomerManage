using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using JIT.CPOS.DTO.Base;
using JIT.Utility.ExtensionMethod;

namespace JIT.CPOS.Web.ApplicationInterface.Product.QiXinManage.ActionHandler.QiXinMock
{
    /// <summary>
    /// ModifyUserPersonalInfo的摘要说明
    /// </summary>
    [Export(typeof(IQiXinMockRequestHandler))]
    [ExportMetadata("Action", "ModifyUserPersonalInfo")]
    public class ModifyUserPersonalInfoHandler : IQiXinMockRequestHandler
    {
        public string DoAction(string pRequest)
        {
            return ModifyUserPersonalInfo(pRequest);
        }

        public string ModifyUserPersonalInfo(string pRequest)
        {
            var rd = new APIResponse<ModifyUserPersonalInfoRD>();
            var rdData = new ModifyUserPersonalInfoRD();
            rdData.IsSuccess = true;
            rd.Data = rdData;
            rd.ResultCode = 0;
            return rd.ToJSON();
        }
    }

    #region 变更用户个人信息
    public class ModifyUserPersonalInfoRP : IAPIRequestParameter
    {
        public string Test { set; get; }
        public void Validate()
        {
        }
    }
    public class ModifyUserPersonalInfoRD : IAPIResponseData
    {
        public bool IsSuccess { set; get; }
    }
    #endregion
}