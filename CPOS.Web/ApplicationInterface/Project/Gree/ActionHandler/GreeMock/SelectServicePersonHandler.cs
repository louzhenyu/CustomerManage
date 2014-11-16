using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using JIT.CPOS.DTO.Base;
using JIT.Utility.ExtensionMethod;

namespace JIT.CPOS.Web.ApplicationInterface.Project.Gree.ActionHandler.GreeMock
{
    /// <summary>
    /// 本接口方法用于设定选中的师傅， 并通过app接口通知到手机app，
    /// 用从接口参数中接收一个url, 拼接师傅的userId后，推送到客户的微信端。供客户查看师傅的详情
    /// </summary>
    [Export(typeof(IGreeMockRequestHandler))]
    [ExportMetadata("Action", "SelectedServicePerson")]
    public class SelectServicePersonHandler : IGreeMockRequestHandler
    {
        public string DoAction(string pRequest)
        {
            return SelectedServicePerson(pRequest);
        }

        /// <summary>
        /// 选择师傅 
        /// </summary>
        /// <param name="pRequest"></param>
        /// <returns></returns>
        public string SelectedServicePerson(string pRequest)
        {
            var rd = new APIResponse<SelectedServicePersonRD>();
            var rdData = new SelectedServicePersonRD();
            rdData.IsSuccess = true;
            rd.Data = rdData;
            rd.ResultCode = 0;
            return rd.ToJSON();
        }
    }

    #region 选择师傅

    public class SelectedServicePersonRP : IAPIRequestParameter
    {
        public string ServiceOrderNO { set; get; }
        public string PersonID { set; get; }
        public string RetUrl { set; get; }

        public void Validate()
        {
            if (string.IsNullOrEmpty(ServiceOrderNO)) throw new APIException("【ServiceOrderNO】不能为空");
            if (string.IsNullOrEmpty(PersonID)) throw new APIException("【PersonID】不能为空");
            if (string.IsNullOrEmpty(RetUrl)) throw new APIException("【RetUrl】不能为空");
        }
    }

    public class SelectedServicePersonRD : IAPIResponseData
    {
        public bool? IsSuccess { set; get; }
    }
    #endregion
}