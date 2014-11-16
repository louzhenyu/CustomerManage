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
    /// GetDeptDirectMembers的摘要说明
    /// </summary>
    [Export(typeof(IQiXinRequestHandler))]
    [ExportMetadata("Action", "GetDeptDirectMembers")]
    public class GetDeptDirectMembersHandler : IQiXinRequestHandler
    {
        public string DoAction(string pRequest)
        {
            return GetDeptDirectMembers(pRequest);
        }

        public string GetDeptDirectMembers(string pRequest)
        {
            var rd = new APIResponse<GetDeptDirectMembersRD>();
            var rdData = new GetDeptDirectMembersRD();

            var rp = pRequest.DeserializeJSONTo<APIRequest<GetDeptDirectMembersRP>>();

            if (rp.Parameters == null)
                throw new ArgumentException();

            if (rp.Parameters != null)
                rp.Parameters.Validate();

            var loggingSessionInfo = Default.GetBSLoggingSession(rp.CustomerID, rp.UserID);
            try
            {
                TUnitBLL unitBll = new TUnitBLL(loggingSessionInfo);
                DepartmentInfoDataAccess departmentManager = new DepartmentInfoDataAccess(loggingSessionInfo);
                rdData.DeptDirectMemberList = departmentManager.GetDirectMembers(rp.Parameters.UnitID);
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

    #region 获取部门直接成员
    public class GetDeptDirectMembersRP : IAPIRequestParameter
    {
        public string UnitID { set; get; }
        public void Validate()
        {
            if (string.IsNullOrEmpty(UnitID)) throw new APIException("UnitID不能为空") { ErrorCode = 102 };
        }
    }
    public class GetDeptDirectMembersRD : IAPIResponseData
    {
        public List<DepartmentTotalMember> DeptDirectMemberList { set; get; }
    }
    #endregion
}