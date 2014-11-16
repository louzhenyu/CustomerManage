using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Web;
using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.DTO.Base;
using JIT.Utility.ExtensionMethod;
using System.Data;

namespace JIT.CPOS.Web.ApplicationInterface.Project.Gree.ActionHandler
{
    [Export(typeof(IGreeRequestHandler))]
    [ExportMetadata("Action", "RemarkServicePerson")]
    public class RemarkServicePersonHandler : IGreeRequestHandler
    {
        public string DoAction(string pRequest)
        {
            return NotifyRemark(pRequest);
        }

        public string NotifyRemark(string pRequest)
        {
            var rd = new APIResponse<RemarkServicePersonRD>();
            var rdData = new RemarkServicePersonRD { IsSuccess = true };
            try
            {
                var req = pRequest.DeserializeJSONTo<APIRequest<RemarkServicePersonRP>>();
                if (req.Parameters == null)
                {
                    throw new ArgumentException();
                }

                req.Parameters.Validate();
                var session = Default.GetBSLoggingSession(req.CustomerID, req.UserID);
                //var serviceOrder = ServiceOrderManager.Instance.GetServiceOrder(req.Parameters.ServiceOrderNO);
                //if (!string.IsNullOrEmpty(serviceOrder.VipID))
                //{
                //VipEntity vipInfo = new VipBLL(session).GetByID(serviceOrder.VipID);
                //VipEntity vipInfo = new VipBLL(session).GetByID(serviceOrder.VipID);
                GLServiceOrderBLL glsobll = new GLServiceOrderBLL(session);
                DataSet ds = glsobll.GetServiceOrderByServiceOrderNo(req.CustomerID, req.Parameters.ServiceOrderNO);
                if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    string vipID = ds.Tables[0].Rows[0]["VipID"] == null ? "" : ds.Tables[0].Rows[0]["VipID"].ToString();
                    VipEntity vipInfo = new VipBLL(session).GetByID(vipID);
                    if (vipInfo == null || vipInfo.VIPID.Equals(""))
                        throw new APIException("用户不存在") { ErrorCode = 102 };

                    #region 推送师傅联系方式url到用户的微信端。
                    string message = "评价安装师傅的服务，请点击<a href='" + req.Parameters.RetUrl + "'></a>";
                    //string code = JIT.CPOS.BS.BLL.WX.CommonBLL.SendWeixinMessage(message, serviceOrder.VipID, session, vipInfo);
                    string code = JIT.CPOS.BS.BLL.WX.CommonBLL.SendWeixinMessage(message, vipID, session, vipInfo);
                    #endregion
                    rd.ResultCode = 0;
                }
                else
                {
                    rdData.IsSuccess = false;
                    rd.Message = "不存在的预约订单号";
                    rd.ResultCode = 102;
                }
                rd.Data = rdData;
            }
            catch (Exception ex)
            {
                rd.Message = ex.Message;
                rd.ResultCode = 101;
            }

            return rd.ToJSON();
        }
    }

    #region 选择师傅

    public class RemarkServicePersonRP : IAPIRequestParameter
    {
        public string ServiceOrderNO { set; get; }
        public string RetUrl { set; get; }

        public void Validate()
        {
            if (string.IsNullOrEmpty(ServiceOrderNO)) throw new APIException("【ServiceOrderNO】不能为空") { ErrorCode = 101 };
            if (string.IsNullOrEmpty(RetUrl)) throw new APIException("【RetUrl】不能为空") { ErrorCode = 101 };
        }
    }

    public class RemarkServicePersonRD : IAPIResponseData
    {
        public bool? IsSuccess { set; get; }
    }
    #endregion
}