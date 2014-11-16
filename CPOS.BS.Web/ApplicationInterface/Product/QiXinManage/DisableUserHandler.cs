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
    [ExportMetadata("Action", "DisableUser")]
    public class DisableUserHandler : IQiXinRequestHandler
    {
        public string DoAction(string pRequest)
        {
            return DisableUser(pRequest);
        }

        public string DisableUser(string pRequest)
        {
            var rd = new APIResponse<DisableUserRD>();
            var rdData = new DisableUserRD();
            var rp = pRequest.DeserializeJSONTo<APIRequest<DisableUserRP>>();

            if (rp.Parameters == null)
                throw new ArgumentException();

            if (rp.Parameters != null)
                rp.Parameters.Validate();

            var loggingSessionInfo = new LoggingSessionManager().CurrentSession;

            T_UserBLL bll = new T_UserBLL(loggingSessionInfo);
            try
            {
                string[] staffIdArr = rp.Parameters.UserID.Split(',');
                T_UserEntity entity = null;
                for (int i = 0; i < staffIdArr.Length; i++)
                {
                    entity = bll.GetByID(staffIdArr[i]);
                    if (entity == null) continue;
                    entity.user_status = "0";
                    entity.user_status_desc = "离职";
                    entity.modify_time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    entity.modify_user_id = rp.UserID;
                    bll.Update(entity);
                }
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

    #region 禁用用户
    public class DisableUserRP : IAPIRequestParameter
    {
        public string UserID { set; get; }
        public void Validate()
        {
            if (string.IsNullOrEmpty(UserID))
                throw new APIException("【UserID】不能为空") { ErrorCode = 102 };
        }
    }
    public class DisableUserRD : IAPIResponseData
    {
        public bool IsSuccess { set; get; }
    }
    #endregion
}