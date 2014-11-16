using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using JIT.CPOS.DTO.Base;
using JIT.CPOS.Web.ApplicationInterface.Base;
using JIT.Utility.ExtensionMethod;

namespace JIT.CPOS.Web.ApplicationInterface.Project.Gree.ActionHandler
{
    /// <summary>
    /// 处理获取接单师傅信息列表的Handler
    /// </summary>
    [Export(typeof(IGreeRequestHandler))]
    [ExportMetadata("Action", "GetReceiveMaster")]
    public class GetReceiveMasterHandler : IGreeRequestHandler
    {
        public string DoAction(string pRequest)
        {
            return GetReceiveMaster(pRequest);
        }

        /// <summary>
        /// 获取接单师傅信息列表
        /// </summary>
        /// <param name="pRequest"></param>
        /// <returns></returns>
        public string GetReceiveMaster(string pRequest)
        {
            var rd = new APIResponse<GetReceiveMasterRD>();
            try
            {
                var req = pRequest.DeserializeJSONTo<APIRequest<GetReceiveMasterRP>>();
                if (req.Parameters == null)
                {
                    throw new ArgumentException();
                }

                req.Parameters.Validate();

                // 根据师傅id查询师傅详情
                var personIdList = ServiceOrderManager.Instance.GetAppliedServicePerson(req.Parameters.ServiceOrderNO);
                var resultList = personIdList.Select(id => GreeCommon.GetServicePerson(req.CustomerID, req.UserID, id)).ToList();

                var rdData = new GetReceiveMasterRD
                {
                    ServicePersonList = resultList
                };

                rd.Data = rdData;
                rd.ResultCode = 0;
            }
            catch (Exception ex)
            {
                rd.Message = ex.Message;
                rd.ResultCode = 101;
            }

            return rd.ToJSON();
        }
    }

    #region 获取接单师傅信息列表
    public class GetReceiveMasterRP : IAPIRequestParameter
    {
        public string ServiceOrderNO { set; get; }

        public void Validate()
        {
        }
    }

    public class GetReceiveMasterRD : IAPIResponseData
    {
        public List<ServicePersonViewModel> ServicePersonList { set; get; }
    }

    public class ServicePersonViewModel
    {
        public string ServicePersonId { set; get; }
        public string Name { set; get; }
        public string Mobile { set; get; }
        public string Picture { set; get; }
        public decimal? Star { set; get; }
        public int? OrderCount { set; get; }
        public int? TodayOrder { set; get; }
    }
    #endregion
}