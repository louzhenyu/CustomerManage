using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using JIT.CPOS.DTO.Base;
using JIT.Utility.ExtensionMethod;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.BS.BLL;
using JIT.Utility.DataAccess.Query;
using JIT.Utility.DataAccess;
using System.Data;

namespace JIT.CPOS.BS.Web.ApplicationInterface.Product.QiXinManage
{
    /// <summary>
    /// QiXinManageLogin的摘要说明
    /// </summary>
    [Export(typeof(IQiXinRequestHandler))]
    [ExportMetadata("Action", "GetRole")]
    public class GetRoleHandler : IQiXinRequestHandler
    {
        public string DoAction(string pRequest)
        {
            return GetRole(pRequest);
        }

        public string GetRole(string pRequest)
        {
            var rd = new APIResponse<GetRoleRD>();
            var rdData = new GetRoleRD();
            var rp = pRequest.DeserializeJSONTo<APIRequest<GetRoleRP>>();

            if (rp.Parameters == null)
                throw new ArgumentException();

            if (rp.Parameters != null)
                rp.Parameters.Validate();

            var loggingSessionInfo = new LoggingSessionManager().CurrentSession;

            try
            {
                var appSysService = new AppSysService(loggingSessionInfo);
                RoleModel list = new RoleModel();
                string key = "D8C5FF6041AA4EA19D83F924DBF56F93";
                //if (form.app_sys_id != null && form.app_sys_id != string.Empty)
                //{
                //    key = form.app_sys_id.Trim();
                //}
                list = appSysService.GetRolesByAppSysId(key, 1000, 0);
                rdData.RoleList =list.RoleInfoList;
                rdData.Count = list.RoleInfoList.Count;
                rd.Data = rdData;
                rd.ResultCode = 0;
            }
            catch (Exception ex)
            {
                rd.ResultCode = 103;
                rd.Message = ex.Message;
            }
            return rd.ToJSON();
        }
    }

    #region 获取角色
    public class GetRoleRP : IAPIRequestParameter
    {
        public void Validate()
        {
        }
    }
    public class GetRoleRD : IAPIResponseData
    {
        public object RoleList { set; get; }
        public int Count { set; get; }
    }
    #endregion
}