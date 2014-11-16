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
    /// GetDeptDirectPersMembers的摘要说明
    /// </summary>
    [Export(typeof(IQiXinRequestHandler))]
    [ExportMetadata("Action", "GetDeptDirectPersMembers")]
    public class GetDeptDirectPersMembersHandler : IQiXinRequestHandler
    {
        public string DoAction(string pRequest)
        {
            return GetDeptDirectPersMembers(pRequest);
        }

        public string GetDeptDirectPersMembers(string pRequest)
        {
            var rd = new APIResponse<GetDeptDirectPersMembersRD>();
            var rdData = new GetDeptDirectPersMembersRD();

            var rp = pRequest.DeserializeJSONTo<APIRequest<GetDeptDirectPersMembersRP>>();

            if (rp.Parameters == null)
                throw new ArgumentException();

            if (rp.Parameters != null)
                rp.Parameters.Validate();

            var loggingSessionInfo = Default.GetBSLoggingSession(rp.CustomerID, rp.UserID);
            try
            {
                TUnitBLL unitBll = new TUnitBLL(loggingSessionInfo);
                DepartmentInfoDataAccess departmentManager = new DepartmentInfoDataAccess(loggingSessionInfo);
                rdData.PersonMemberList = departmentManager.GetDirectPersMembers(rp.Parameters.UnitID);
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

    #region 获取部门的全部直接个人成员
    public class GetDeptDirectPersMembersRP : IAPIRequestParameter
    {
        public string UnitID { set; get; }
        public void Validate()
        {
            if (string.IsNullOrEmpty(UnitID)) throw new APIException("UnitID不能为空") { ErrorCode = 102 };
        }
    }
    public class GetDeptDirectPersMembersRD : IAPIResponseData
    {
        public List<PersonListItemInfo> PersonMemberList { set; get; }
    }
    #endregion
}