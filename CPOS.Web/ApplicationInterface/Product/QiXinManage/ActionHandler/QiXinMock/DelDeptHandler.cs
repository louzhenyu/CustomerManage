using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using JIT.CPOS.DTO.Base;
using JIT.Utility.ExtensionMethod;

namespace JIT.CPOS.Web.ApplicationInterface.Product.QiXinManage.ActionHandler.QiXinMock
{
    /// <summary>
    /// DelDept的摘要说明
    /// </summary>
    [Export(typeof(IQiXinMockRequestHandler))]
    [ExportMetadata("Action", "DelDept")]
    public class DelDeptHandler : IQiXinMockRequestHandler
    {
        public string DoAction(string pRequest)
        {
            return DelDept(pRequest);
        }

        public string DelDept(string pRequest)
        {
            var rd = new APIResponse<DelDeptRD>();
            var rdData = new DelDeptRD();
            rdData.IsSuccess = true;
            rd.Data = rdData;
            rd.ResultCode = 0;
            return rd.ToJSON();
        }
    }

    #region 删除部门
    public class DelDeptRP : IAPIRequestParameter
    {
        public string Test { set; get; }
        public void Validate()
        {
        }
    }
    public class DelDeptRD : IAPIResponseData
    {
        public bool IsSuccess { set; get; }
    }
    #endregion
}