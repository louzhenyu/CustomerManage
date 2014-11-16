using System.Collections.Generic;
using System.ComponentModel.Composition;
using JIT.CPOS.DTO.Base;
using JIT.CPOS.Web.ApplicationInterface.Base;
using JIT.Utility.ExtensionMethod;
using System;

namespace JIT.CPOS.Web.ApplicationInterface.Project.Gree.ActionHandler
{
    /// <summary>
    /// GetMasterTaskListHandler 的摘要说明
    /// </summary>
    [Export(typeof(IGreeRequestHandler))]
    [ExportMetadata("Action", "GetMasterTaskList")]
    public class GetMasterTaskListHandler : IGreeRequestHandler
    {
        public string DoAction(string pRequest)
        {
            return GetMasterTaskList(pRequest);
        }

        /// <summary>
        /// 获取当前师傅任务列表
        /// </summary>
        /// <param name="pRequest"></param>
        /// <returns></returns>
        public string GetMasterTaskList(string pRequest)
        {
            var rd = new APIResponse<GetMasterTaskListRD>();
            var rdData = new GetMasterTaskListRD();

            var rp = pRequest.DeserializeJSONTo<APIRequest<GetMasterTaskListRP>>();

            if (rp.Parameters == null)
                throw new ArgumentException();

            if (rp.Parameters != null)
                rp.Parameters.Validate();

            if (string.IsNullOrEmpty(rp.UserID))
                throw new APIException("【UserID】不能为空");

            var loggingSessionInfo = Default.GetBSLoggingSession(rp.CustomerID, rp.UserID);

            rdData.TaskList = new ServiceOrderDataAccess(loggingSessionInfo).GetSubscribeOrderList(rp.CustomerID, rp.UserID);

            rd.Data = rdData;
            rd.ResultCode = 0;
            return rd.ToJSON();
        }
    }

    #region 获取当前师傅任务列表
    public class GetMasterTaskListRP : IAPIRequestParameter
    {
        public void Validate()
        {
        }
    }
    public class GetMasterTaskListRD : IAPIResponseData
    {
        public List<SubscribeOrderViewModel> TaskList { set; get; }
    }
    #endregion
}