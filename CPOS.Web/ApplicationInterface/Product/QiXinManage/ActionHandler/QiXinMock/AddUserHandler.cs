using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using JIT.CPOS.DTO.Base;
using JIT.Utility.ExtensionMethod;

namespace JIT.CPOS.Web.ApplicationInterface.Product.QiXinManage.ActionHandler.QiXinMock
{
    /// <summary>
    /// AddUser的摘要说明
    /// </summary>
    [Export(typeof(IQiXinMockRequestHandler))]
    [ExportMetadata("Action", "AddUser")]
    public class AddUserHandler : IQiXinMockRequestHandler
    {
        public string DoAction(string pRequest)
        {
            return AddUser(pRequest);
        }

        public string AddUser(string pRequest)
        {
            var rd = new APIResponse<AddUserRD>();
            var rdData = new AddUserRD();
            rdData.UserID = Guid.NewGuid().ToString().Replace("-", "");
            rd.Data = rdData;
            rd.ResultCode = 0;
            return rd.ToJSON();
        }
    }

    #region 添加用户
    public class AddUserRP : IAPIRequestParameter
    {
        public string Test { set; get; }
        public void Validate()
        {
        }
    }
    public class AddUserRD : IAPIResponseData
    {
        public string UserID { set; get; }
    }
    #endregion
}