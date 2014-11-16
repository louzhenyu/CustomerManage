using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using JIT.CPOS.DTO.Base;
using JIT.Utility.ExtensionMethod;

namespace JIT.CPOS.Web.ApplicationInterface.Product.QiXinManage.ActionHandler.QiXinMock
{
    /// <summary>
    /// ModifyJobFunc的摘要说明
    /// </summary>
    [Export(typeof(IQiXinMockRequestHandler))]
    [ExportMetadata("Action", "ModifyJobFunc")]
    public class ModifyJobFuncHandler : IQiXinMockRequestHandler
    {
        public string DoAction(string pRequest)
        {
            return ModifyJobFunc(pRequest);
        }

        public string ModifyJobFunc(string pRequest)
        {
            var rd = new APIResponse<ModifyJobFuncRD>();
            var rdData = new ModifyJobFuncRD();
            rdData.IsSuccess = true;
            rd.Data = rdData;
            rd.ResultCode = 0;
            return rd.ToJSON();
        }
    }

    #region 职衔信息变更
    public class ModifyJobFuncRP : IAPIRequestParameter
    {
        public string Test { set; get; }
        public void Validate()
        {
        }
    }
    public class ModifyJobFuncRD : IAPIResponseData
    {
        public bool IsSuccess { set; get; }
    }
    #endregion
}