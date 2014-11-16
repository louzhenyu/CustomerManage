using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using JIT.CPOS.DTO.Base;
using JIT.Utility.ExtensionMethod;

namespace JIT.CPOS.Web.ApplicationInterface.Product.QiXinManage.ActionHandler.QiXinMock
{
    /// <summary>
    /// AddJobFunc的摘要说明
    /// </summary>
    [Export(typeof(IQiXinMockRequestHandler))]
    [ExportMetadata("Action", "AddJobFunc")]
    public class AddJobFuncHandler : IQiXinMockRequestHandler
    {
        public string DoAction(string pRequest)
        {
            return AddJobFunc(pRequest);
        }

        public string AddJobFunc(string pRequest)
        {
            var rd = new APIResponse<AddJobFuncRD>();
            var rdData = new AddJobFuncRD();
            rdData.JobFunctionID = Guid.NewGuid().ToString();
            rd.Data = rdData;
            rd.ResultCode = 0;
            return rd.ToJSON();
        }
    }

    #region 添加职衔
    public class AddJobFuncRP : IAPIRequestParameter
    {
        public string Test { set; get; }
        public void Validate()
        {
        }
    }
    public class AddJobFuncRD : IAPIResponseData
    {
        public string JobFunctionID { set; get; }
    }
    #endregion
}