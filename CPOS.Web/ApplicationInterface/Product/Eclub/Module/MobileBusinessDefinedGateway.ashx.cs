using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using JIT.CPOS.BS.BLL.Product.Eclub.Module;
using JIT.CPOS.BS.Entity.Product.Eclub.Module;
using JIT.Utility.ExtensionMethod;

namespace JIT.CPOS.Web.ApplicationInterface.Product.Eclub.Module
{
    /// <summary>
    /// Summary description for MobileBusinessDefinedGateway
    /// </summary>
    public class MobileBusinessDefinedGateway : JIT.CPOS.Web.ApplicationInterface.BaseGateway
    {
        protected override string ProcessAction(string pType, string pAction, string pRequest)
        {
            var result = string.Empty;
            //switch (pAction)
            //{
                //case "getUserByID":
                //    result = GetUserByID();
                //    break;
                //case "submitUserByID":
                //    result = SubmitUserByID();
                //    break;
                
            //}
            return result;
        }

        #region GetUserByID
        /// <summary>
        /// 获取个人信息、字段信息
        /// </summary>
        /// <returns></returns>
        //private string GetUserByID()
        //{
        //    ResponseEntity<ResponsePageListEntity> responseEntity = new ResponseEntity<ResponsePageListEntity>();
        //    if (!string.IsNullOrEmpty(HttpContext.Current.Request["ReqContent"]))
        //    {
        //        var reqObj = HttpContext.Current.Request["ReqContent"].DeserializeJSONTo<RequestGetUserByIDData>();
                
        //        if (reqObj != null)
        //        {
        //            var loggingSessionInfo = Default.GetBSLoggingSession(reqObj.CustomerId, "1");
        //            List<PageEntity> list = new MobileBusinessDefinedBLL(loggingSessionInfo).getUserByID(reqObj.Code);
        //            ResponsePageListEntity pageList = new ResponsePageListEntity();
        //            pageList.PageList = list;
        //            if (list != null && list.Count > 0)
        //            {
        //                responseEntity.code = "200";
        //                responseEntity.description = "操作成功";
        //                responseEntity.content = pageList;
        //            }
        //            else
        //            {
        //                responseEntity.code = "1";
        //                responseEntity.description = "操作失败";
        //                responseEntity.content = null;
        //            }
        //        }
        //    }
        //    return responseEntity.ToJSON();
        //}
        #endregion

        #region SubmitUserByID
        /// <summary>
        /// 提交个人信息、控件隐私信息
        /// </summary>
        /// <returns></returns>
        //private string SubmitUserByID()
        //{
        //    ResponseEntity<ResponseSubmitUserByIDData> responseEntity = new ResponseEntity<ResponseSubmitUserByIDData>();
        //    if (!string.IsNullOrEmpty(HttpContext.Current.Request["ReqContent"]))
        //    {
        //        var reqObj = HttpContext.Current.Request["ReqContent"].DeserializeJSONTo<RequestSubmitUserByIDData>();
        //        if (reqObj != null)
        //        {
        //            var loggingSessionInfo = Default.GetBSLoggingSession(reqObj.CustomerId, "1");

        //            if (reqObj != null && reqObj.Control != null && reqObj.Control.Count > 0)
        //            {
        //                if (new MobileBusinessDefinedBLL(loggingSessionInfo).SubmitUserByID(reqObj) > 0)
        //                {
        //                    responseEntity.code = "200";
        //                    responseEntity.description = "操作成功";
        //                }
        //                else
        //                {
        //                    responseEntity.code = "1";
        //                    responseEntity.description = "操作失败";
        //                }
        //            }
        //        }
        //    }
        //    return responseEntity.ToJSON();
        //}
        #endregion
        
    }
}