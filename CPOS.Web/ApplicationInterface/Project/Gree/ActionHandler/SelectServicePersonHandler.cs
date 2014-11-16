using System;
using System.ComponentModel.Composition;
using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.DTO.Base;
using JIT.CPOS.Web.ApplicationInterface.Base;
using JIT.Utility.ExtensionMethod;
using JIT.Utility.DataAccess.Query;


namespace JIT.CPOS.Web.ApplicationInterface.Project.Gree.ActionHandler
{
    /// <summary>
    /// 本接口方法用于设定选中的师傅， 并通过app接口通知到手机app，
    /// 用从接口参数中接收一个url, 拼接师傅的userId后，推送到客户的微信端。供客户查看师傅的详情
    /// </summary>
    [Export(typeof(IGreeRequestHandler))]
    [ExportMetadata("Action", "SelectedServicePerson")]
    public class SelectServicePersonHandler : IGreeRequestHandler
    {
        public string DoAction(string pRequest)
        {
            return SelectServicePerson(pRequest);
        }

        /// <summary>
        /// 选择师傅 
        /// </summary>
        /// <param name="pRequest"></param>
        /// <returns></returns>
        public string SelectServicePerson(string pRequest)
        {
            var rd = new APIResponse<SelectServicePersonRD>();
            try
            {
                var req = pRequest.DeserializeJSONTo<APIRequest<SelectServicePersonRP>>();
                if (req.Parameters == null)
                {
                    throw new ArgumentException();
                }

                req.Parameters.Validate();

                // 创建任务
                var session = Default.GetBSLoggingSession(req.CustomerID, req.UserID);
                var bll = new GLServiceTaskBLL(session);
                var serviceOrder = ServiceOrderManager.Instance.GetServiceOrder(req.Parameters.ServiceOrderNO);
                if (serviceOrder == null)
                {
                    throw new ArgumentException();
                }

                //删除
                var entity = bll.Query(new IWhereCondition[]
                    {
                        new EqualsCondition{DateTimeAccuracy = null, FieldName = "ServiceOrderID", Value = req.Parameters.ServiceOrderNO},
                        new EqualsCondition{DateTimeAccuracy = null, FieldName = "CustomerID", Value =req.CustomerID },
                        new EqualsCondition{DateTimeAccuracy = null, FieldName = "IsDelete", Value = "0"},
                        new EqualsCondition{DateTimeAccuracy = null, FieldName = "UserID", Value =req.Parameters.PersonID }
                    }, null);
                if (entity != null && entity.Length > 0)
                {
                    bll.Delete(entity);
                }

                bll.Create(new GLServiceTaskEntity
                {
                    CreateBy = "",
                    CreateTime = DateTime.Now,
                    CustomerID = req.CustomerID,
                    IsDelete = 0,
                    LastUpdateBy = "",
                    LastUpdateTime = DateTime.Now,
                    ServiceDate = serviceOrder.InstallOrderDate,
                    ServiceOrderID = req.Parameters.ServiceOrderNO,
                    ServiceTaskID = GreeCommon.GetGuid(),
                    UserID = req.Parameters.PersonID
                });

                // 标记赢得预约单的师傅
                ServiceOrderManager.Instance.SelectServicePerson(req.Parameters.ServiceOrderNO, req.Parameters.PersonID);

                if (!string.IsNullOrEmpty(serviceOrder.VipID))
                {
                    VipEntity vipInfo = new VipBLL(session).GetByID(serviceOrder.VipID);
                    if (vipInfo == null || vipInfo.VIPID.Equals(""))
                        throw new APIException("用户不存在");

                    #region 推送师傅联系方式url到用户的微信端。
                    string message = "安装师傅联系方式，请点击<a href='" + req.Parameters.RetUrl + "'>查看师傅详情 </a>";
                    string code = JIT.CPOS.BS.BLL.WX.CommonBLL.SendWeixinMessage(message, serviceOrder.VipID, session, vipInfo);
                    #endregion
                }

                var rdData = new SelectServicePersonRD { IsSuccess = true };
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

    #region 选择师傅

    public class SelectServicePersonRP : IAPIRequestParameter
    {
        public string ServiceOrderNO { set; get; }
        public string PersonID { set; get; }
        public string RetUrl { set; get; }

        public void Validate()
        {
            if (string.IsNullOrEmpty(ServiceOrderNO)) throw new APIException("【ServiceOrderNO】不能为空") { ErrorCode = 101 };
            if (string.IsNullOrEmpty(PersonID)) throw new APIException("【PersonID】不能为空") { ErrorCode = 101 };
            if (string.IsNullOrEmpty(RetUrl)) throw new APIException("【RetUrl】不能为空") { ErrorCode = 101 };
        }
    }

    public class SelectServicePersonRD : IAPIResponseData
    {
        public bool? IsSuccess { set; get; }
    }
    #endregion
}