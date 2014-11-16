using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using JIT.CPOS.DTO.Base;
using JIT.Utility.ExtensionMethod;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.BS.BLL;

namespace JIT.CPOS.Web.ApplicationInterface.Product.QiXinManage.ActionHandler
{
    /// <summary>
    /// GetDeptDirectSubDept的摘要说明
    /// </summary>
    [Export(typeof(IQiXinRequestHandler))]
    [ExportMetadata("Action", "GetDeptDirectSubDept")]
    public class GetDeptDirectSubDeptHandler : IQiXinRequestHandler
    {
        public string DoAction(string pRequest)
        {
            return GetDeptDirectSubDept(pRequest);
        }

        public string GetDeptDirectSubDept(string pRequest)
        {
            var rd = new APIResponse<GetDeptDirectSubDeptRD>();
            var rdData = new GetDeptDirectSubDeptRD();

            var rp = pRequest.DeserializeJSONTo<APIRequest<GetDeptDirectSubDeptRP>>();

            if (rp.Parameters == null)
                throw new ArgumentException();

            if (rp.Parameters != null)
                rp.Parameters.Validate();

            var loggingSessionInfo = Default.GetBSLoggingSession(rp.CustomerID, rp.UserID);
            try
            {
                TUnitBLL unitBll = new TUnitBLL(loggingSessionInfo);
                DepartmentInfoDataAccess departmentManager = new DepartmentInfoDataAccess(loggingSessionInfo);
                rdData.DepartmentList = departmentManager.GetDeptDirectSubDept(rp.Parameters.UnitID);
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

    #region 获取部门全部直接子部门
    public class GetDeptDirectSubDeptRP : IAPIRequestParameter
    {
        public string UnitID { set; get; }
        public void Validate()
        {
            if (string.IsNullOrEmpty(UnitID)) throw new APIException("UnitID不能为空") { ErrorCode = 102 };
        }
    }
    public class GetDeptDirectSubDeptRD : IAPIResponseData
    {
        public List<DepartmentInfo> DepartmentList { set; get; }
    }
    #endregion
}