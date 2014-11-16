using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using JIT.CPOS.DTO.Base;
using JIT.Utility.ExtensionMethod;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.BS.BLL;

namespace JIT.CPOS.Web.ApplicationInterface.Product.QiXinManage.ActionHandler.QiXinMock
{
    /// <summary>
    /// QiXinManageLogin的摘要说明
    /// </summary>
    [Export(typeof(IQiXinMockRequestHandler))]
    [ExportMetadata("Action", "QiXinManageLogin")]
    public class AddDeptHandler : IQiXinMockRequestHandler
    {
        public string DoAction(string pRequest)
        {
            return QiXinManageLogin(pRequest);
        }

        public string QiXinManageLogin(string pRequest)
        {
            //var rd = new APIResponse<AddDeptRD>();
            //var rdData = new AddDeptRD();
            //rdData.UnitID = Guid.NewGuid().ToString();
            //rd.Data = rdData;
            //rd.ResultCode = 0;

            var rd = new APIResponse<ModifyUserOrganizationRD>();
            var rdData = new ModifyUserOrganizationRD();

            var rp = pRequest.DeserializeJSONTo<APIRequest<AddDeptRP>>();

            if (rp.Parameters == null)
                throw new ArgumentException();

            if (rp.Parameters != null)
                rp.Parameters.Validate();

            var loggingSessionInfo = Default.GetBSLoggingSession(rp.CustomerID, rp.UserID);
            try
            {
                rdData.IsSuccess = true;
                rd.ResultCode = 0;
            }
            catch (Exception ex)
            {
                rd.ResultCode = 103;
                rd.Message = ex.Message;
            }
            rd.Data = rdData;
            return rd.ToJSON();
        }
    }

    #region 添加部门
    public class AddDeptRP : IAPIRequestParameter
    {
        public string Test { set; get; }
        public void Validate()
        {
        }
    }
    public class AddDeptRD : IAPIResponseData
    {
        public string UnitID { set; get; }
    }
    #endregion
}