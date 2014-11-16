using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using JIT.CPOS.DTO.Base;
using JIT.Utility.ExtensionMethod;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.BS.BLL;
using JIT.Utility.DataAccess.Query;
using System.Data;


namespace JIT.CPOS.BS.Web.ApplicationInterface.Product.QiXinManage
{
    [Export(typeof(IQiXinRequestHandler))]
    [ExportMetadata("Action", "GetUserDict")]
    public class GetUserDictHandler : IQiXinRequestHandler
    {
        public string DoAction(string pRequest)
        {
            return GetUserDict(pRequest);
        }

        public string GetUserDict(string pRequest)
        {
            var rd = new APIResponse<GetUserDictRD>();
            var rdData = new GetUserDictRD();
            var rp = pRequest.DeserializeJSONTo<APIRequest<GetUserDictRP>>();

            if (rp.Parameters == null)
                throw new ArgumentException();

            if (rp.Parameters != null)
                rp.Parameters.Validate();

            var loggingSessionInfo = new LoggingSessionManager().CurrentSession;

            try
            {
                T_UserBLL userBll = new T_UserBLL(loggingSessionInfo);
                DataTable dTable = userBll.GetStaffDict(rp.Parameters.UserID, rp.Parameters.UserName.Trim());
                if (dTable != null)
                    rdData.UserList = DataTableToObject.ConvertToList<UserDict>(dTable);
                rd.Data = rdData;
                rd.ResultCode = 0;
            }
            catch (Exception ex)
            {
                rd.ResultCode = 103;
                rd.Message = ex.Message;
            }
            return rd.ToJSON();
        }
    }

    #region 获取员工字典
    public class GetUserDictRP : IAPIRequestParameter
    {
        public string UserID { set; get; }
        public string UserName { set; get; }

        public void Validate()
        {

        }
    }
    public class GetUserDictRD : IAPIResponseData
    {
        public List<UserDict> UserList { set; get; }
    }
    #endregion

    public class UserDict
    {
        /// <summary>
        /// 用户标识
        /// </summary>
        public string UserID { set; get; }
        /// <summary>
        /// 姓名
        /// </summary>
        public string UserName { set; get; }
    }
}