using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Web;
using JIT.CPOS.DTO.Base;
using JIT.CPOS.Web.ApplicationInterface.Base;
using JIT.CPOS.Web.ApplicationInterface.Project.Gree.ActionHandler;
using JIT.Utility.ExtensionMethod;

namespace JIT.CPOS.Web.ApplicationInterface.Project.Gree
{
    /// <summary>
    /// GreeHandler 的摘要说明
    /// </summary>
    public class GreeHandler : BaseGateway
    {
        /// <summary>
        /// 格力空调项目。
        /// </summary>
        /// <param name="pType"></param>
        /// <param name="pAction"></param>
        /// <param name="pRequest"></param>
        /// <returns></returns>
        protected override string ProcessAction(string pType, string pAction, string pRequest)
        {

            //根据action做不同的业务处理
            return RequestHandlerManager.Instance.HandleGreeRequest(pAction, pRequest);
            /*
            switch (pAction)
            {
                case "GetOrder":
                    rst = GetOrder(pRequest);
                    break;
                case "SubmitAppOrder":
                    rst = SubmitAppOrder(pRequest);
                    break;
                case "GetReceiveMaster":
                    rst = GetReceiveMaster(pRequest);
                    break;
                case "GetRunningServiceOrder":
                    rst = GetRunningServiceOrder(pRequest);
                    break;
                case "GetSubscribeOrderDetail":
                    rst = GetSubscribeOrderDetail(pRequest);
                    break;
                case "GetServicePerson":
                    rst = GetServicePerson(pRequest);
                    break;
                case "GetMasterTaskList":
                    rst = GetMasterTaskList(pRequest);
                    break;
                case "GetNotfyMarketCount":
                    rst = GetNotfyMarketCount(pRequest);
                    break;
                case "GetApplyCount":
                    rst = GetApplyCount(pRequest);
                    break;
                case "SelectedServicePerson":
                    rst = SelectedServicePerson(pRequest);
                    break;
                default: throw new APIException(string.Format("未实现Action名为{0}的处理.", pAction)) { ErrorCode = 201 };
            }

            return rst;
             */
        }

        #region 业务处理

        #endregion
    }

    #region 请求参数及响应结果的数据结构

    #endregion
}