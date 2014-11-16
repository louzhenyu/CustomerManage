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
    /// GetDeptAllMembers的摘要说明
    /// </summary>
    [Export(typeof(IQiXinRequestHandler))]
    [ExportMetadata("Action", "GetDeptAllMembers")]
    public class GetDeptAllMembersHandler : IQiXinRequestHandler
    {
        public string DoAction(string pRequest)
        {
            return GetDeptAllMembers(pRequest);
        }

        public string GetDeptAllMembers(string pRequest)
        {
            var rd = new APIResponse<GetDeptAllMembersRD>();
            var rdData = new GetDeptAllMembersRD();

            var rp = pRequest.DeserializeJSONTo<APIRequest<GetDeptAllMembersRP>>();

            if (rp.Parameters == null)
                throw new ArgumentException();

            if (rp.Parameters != null)
                rp.Parameters.Validate();

            var loggingSessionInfo = Default.GetBSLoggingSession(rp.CustomerID, rp.UserID);
            try
            {
                TUnitBLL unitBll = new TUnitBLL(loggingSessionInfo);
                DepartmentInfoDataAccess departmentManager = new DepartmentInfoDataAccess(loggingSessionInfo);
                TUnitEntity entity = unitBll.GetByID(rp.Parameters.UnitID);
                if (entity != null)
                {
                    rdData.Member = departmentManager.GetDeptAllMembers(entity.UnitID, entity.UnitName);
                    rd.ResultCode = 0;
                }
                else
                {
                    rd.ResultCode = 101;
                    rd.Message = "部门不存在";
                }
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

    #region 获取部门及其各级子部门的所有成员
    public class GetDeptAllMembersRP : IAPIRequestParameter
    {
        public string UnitID { set; get; }
        public void Validate()
        {
            if (string.IsNullOrEmpty(UnitID)) throw new APIException("UnitID不能为空") { ErrorCode = 102 };
        }
    }
    public class GetDeptAllMembersRD : IAPIResponseData
    {
        public DepartmentTotalMember Member { set; get; }
    }
    #endregion
}