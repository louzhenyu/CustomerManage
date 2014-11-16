using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using JIT.CPOS.DTO.Base;
using JIT.Utility.ExtensionMethod;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.BS.BLL;

namespace JIT.CPOS.BS.Web.ApplicationInterface.Product.QiXinManage
{
    /// <summary>
    /// DelUser的摘要说明
    /// </summary>
    [Export(typeof(IQiXinRequestHandler))]
    [ExportMetadata("Action", "DelUser")]
    public class DelUserHandler : IQiXinRequestHandler
    {
        public string DoAction(string pRequest)
        {
            return DelUser(pRequest);
        }

        public string DelUser(string pRequest)
        {
            var rd = new APIResponse<DelUserRD>();
            var rdData = new DelUserRD();
            var rp = pRequest.DeserializeJSONTo<APIRequest<DelUserRP>>();

            if (rp.Parameters == null)
                throw new ArgumentException();

            if (rp.Parameters != null)
                rp.Parameters.Validate();

            var loggingSessionInfo = new LoggingSessionManager().CurrentSession;

            T_UserBLL bll = new T_UserBLL(loggingSessionInfo);
            try
            {
                T_UserEntity entity = bll.GetByID(rp.Parameters.UserID);
                entity.user_status = "-1";
                entity.user_status_desc = "已删除";
                entity.modify_time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                entity.modify_user_id = rp.UserID;
                bll.Update(entity);
                rdData.IsSuccess = true;
                rd.ResultCode = 0;
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

    #region 移除用户
    public class DelUserRP : IAPIRequestParameter
    {
        public string UserID { set; get; }
        public void Validate()
        {
            if (string.IsNullOrEmpty(UserID))
                throw new APIException("【UserID】不能为空") { ErrorCode = 102 };
        }
    }
    public class DelUserRD : IAPIResponseData
    {
        public bool IsSuccess { set; get; }
    }
    #endregion
}