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
    /// ModifyUserOrganization的摘要说明
    /// </summary>
    [Export(typeof(IQiXinRequestHandler))]
    [ExportMetadata("Action", "ModifyUserOrganization")]
    public class ModifyUserOrganizationHandler : IQiXinRequestHandler
    {
        public string DoAction(string pRequest)
        {
            return ModifyUserOrganization(pRequest);
        }

        public string ModifyUserOrganization(string pRequest)
        {
            var rd = new APIResponse<ModifyUserOrganizationRD>();
            var rdData = new ModifyUserOrganizationRD();

            var rp = pRequest.DeserializeJSONTo<APIRequest<ModifyUserOrganizationRP>>();

            if (rp.Parameters == null)
                throw new ArgumentException();

            if (rp.Parameters != null)
                rp.Parameters.Validate();

            var loggingSessionInfo = Default.GetBSLoggingSession(rp.CustomerID, rp.UserID);
            try
            {
                UserDeptJobMappingBLL mappingBll = new UserDeptJobMappingBLL(loggingSessionInfo);
                UserDeptJobMappingEntity entity = mappingBll.GetByUserID(rp.Parameters.UserID);
                if (entity != null)
                {
                    entity.UnitID = rp.Parameters.UnitID;
                    entity.JobFunctionID = rp.Parameters.JobFunctionID;
                    if (!string.IsNullOrEmpty(rp.Parameters.UserLevel))
                        entity.UserLevel = rp.Parameters.UserLevel;
                    entity.LineManagerID = rp.Parameters.LineManagerID;
                    mappingBll.Update(entity);
                    rdData.IsSuccess = true;
                    rd.ResultCode = 0;
                }
                else
                {
                    rdData.IsSuccess = false;
                    rd.ResultCode = 101;
                    rd.Message = "对象不存在";
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

    #region 变更用户组织信息
    public class ModifyUserOrganizationRP : IAPIRequestParameter
    {
        public string UserID { set; get; }
        public string UnitID { set; get; }
        public string JobFunctionID { set; get; }
        public string UserLevel { set; get; }
        public string LineManagerID { set; get; }
        public void Validate()
        {
            if (string.IsNullOrEmpty(UserID)) throw new APIException("UserID不能为空") { ErrorCode = 102 };
            if (string.IsNullOrEmpty(UnitID)) throw new APIException("UnitID不能为空") { ErrorCode = 102 };
            if (string.IsNullOrEmpty(JobFunctionID)) throw new APIException("JobFunctionID不能为空") { ErrorCode = 102 };
            //if (string.IsNullOrEmpty(UserLevel)) throw new APIException("UserLevel不能为空") { ErrorCode = 102 };
            if (string.IsNullOrEmpty(LineManagerID)) throw new APIException("LineManagerID不能为空") { ErrorCode = 102 };
        }
    }
    public class ModifyUserOrganizationRD : IAPIResponseData
    {
        public bool IsSuccess { set; get; }
    }
    #endregion
}