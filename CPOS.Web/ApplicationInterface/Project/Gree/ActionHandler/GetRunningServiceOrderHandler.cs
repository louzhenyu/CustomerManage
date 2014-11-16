using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using JIT.CPOS.DTO.Base;
using JIT.CPOS.Web.ApplicationInterface.Base;
using JIT.Utility.ExtensionMethod;
using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Entity;
using System.Data;

namespace JIT.CPOS.Web.ApplicationInterface.Project.Gree.ActionHandler
{
    /// <summary>
    /// 获取所有未定预约单信息的Handle
    /// </summary>
    [Export(typeof(IGreeRequestHandler))]
    [ExportMetadata("Action", "GetRunningServiceOrder")]
    public class GetRunningServiceOrderHandler : IGreeRequestHandler
    {
        public string DoAction(string pRequest)
        {
            return GetRunningServiceOrder(pRequest);
        }

        /// <summary>
        /// 获取所有未定预约单信息
        /// </summary>
        /// <param name="pRequest"></param>
        /// <returns></returns>
        public string GetRunningServiceOrder(string pRequest)
        {
            var rd = new APIResponse<GetRunningServiceOrderRD>();
            try
            {
                var rp = pRequest.DeserializeJSONTo<APIRequest<GetRunningServiceOrderRP>>();
                if (rp.Parameters == null)
                {
                    throw new ArgumentException();
                }

                rp.Parameters.Validate();

                var rdData = new GetRunningServiceOrderRD();
                rdData.SubOrderList = ServiceOrderManager.Instance.GetRunningServiceOrder();
                rd.Data = rdData;
                rd.ResultCode = 0;

                #region 自动保存师傅信息
                try
                {
                    var loggingSessionInfo = Default.GetBSLoggingSession(rp.CustomerID, rp.UserID);
                    GLServicePersonInfoBLL glspiblll = new GLServicePersonInfoBLL(loggingSessionInfo);
                    GLServicePersonInfoEntity glspie = glspiblll.GetByID(rp.UserID);
                    if (glspie == null)
                    {
                        T_UserBLL bll = new T_UserBLL(loggingSessionInfo);
                        T_UserEntity tue = bll.GetUserEntityByID(rp.UserID);
                        string headImage = "";
                        //获取头像
                        DataSet ds = bll.GetObjectImages(rp.UserID);
                        if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                            headImage = ds.Tables[0].Rows[0]["ImageURL"] == null ? "" : ds.Tables[0].Rows[0]["ImageURL"].ToString();
                        if (tue != null)
                        {
                            glspie = new GLServicePersonInfoEntity();
                            glspie.UserID = tue.user_id;
                            glspie.Name = tue.user_name;
                            glspie.Mobile = tue.user_telephone;
                            glspie.Picture = headImage;
                            glspie.OrderCount = 0;
                            glspie.Star = 5;
                            glspie.CustomerID = tue.customer_id;
                            glspiblll.Create(glspie);
                        }
                    }
                }
                catch (Exception ex) { }
                #endregion
            }
            catch (Exception ex)
            {
                rd.Message = ex.Message;
                rd.ResultCode = 101;
            }

            return rd.ToJSON();
        }
    }

    #region 获取所有未定预约单信息
    public class GetRunningServiceOrderRP : IAPIRequestParameter
    {
        public void Validate()
        {
        }
    }

    public class GetRunningServiceOrderRD : IAPIResponseData
    {
        public List<SubscribeOrderViewModel> SubOrderList { set; get; }
    }

    public class SubscribeOrderViewModel
    {
        public string ServiceOrderNO { set; get; }
        public int? ServiceType { set; get; }
        public DateTime? ServiceOrderDate { set; get; }
        public DateTime? ServiceOrderDateEnd { set; get; }
        public DateTime? InstallOrderDate { set; get; }
        public string ServiceAddress { set; get; }
        public string VipID { set; get; }
        public string CustomerName { set; get; }
        public string CustomerPhone { set; get; }
        //public string Coordinate { set; get; }
        public decimal? Latitude { set; get; }
        public decimal? Longitude { set; get; }
        public string Distance { set; get; }
        public string Message { set; get; }
        public int? InstallCount { set; get; }
        public List<InstallDeviceViewModel> DeviceList { set; get; }
    }
    #endregion
}