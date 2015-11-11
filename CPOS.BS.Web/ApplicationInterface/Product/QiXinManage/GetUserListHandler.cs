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
    [ExportMetadata("Action", "GetUserList")]
    public class GetUserListHandler : IQiXinRequestHandler
    {
        public string DoAction(string pRequest)
        {
            return GetUserList(pRequest);
        }

        public string GetUserList(string pRequest)
        {
            var rd = new APIResponse<GetUserRD>();
            var rdData = new GetUserRD();
            var rp = pRequest.DeserializeJSONTo<APIRequest<GetUserRP>>();

            if (rp.Parameters == null)
                throw new ArgumentException();

            if (rp.Parameters != null)
                rp.Parameters.Validate();

            var loggingSessionInfo = new LoggingSessionManager().CurrentSession;

            try
            {

                //获取普通员工employee角色标识
                string roleId = string.Empty;
                var appSysService = new AppSysService(loggingSessionInfo);
                RoleModel list = new RoleModel();
                string key = "D8C5FF6041AA4EA19D83F924DBF56F93";
                list = appSysService.GetRolesByAppSysId(key, 1000, 0,"","","");

                foreach (var item in list.RoleInfoList)
                {
                    if (item.Role_Code.ToLower() == "employee")
                    {
                        roleId = item.Role_Id;
                        break;
                    }
                }
                if (string.IsNullOrEmpty(roleId) || roleId == "")
                    throw new APIException("employee的roleId未获取到") { ErrorCode = 103 };

                T_UserBLL userBll = new T_UserBLL(loggingSessionInfo);
                //string typeID = System.Configuration.ConfigurationManager.AppSettings["TypeID"].ToString();
                int totalPage = 0;
                QueryUserEntity entity = new QueryUserEntity();
                entity.QUserName = rp.Parameters.Keyword;
                entity.QUnitID = rp.Parameters.UnitID;
                entity.QJobFunctionID = rp.Parameters.JobFunctionID;
                entity.QRoleID = roleId;
                DataTable dTable = userBll.GetUserList(rp.Parameters.UserID, rp.Parameters.PageIndex, rp.Parameters.PageSize, out totalPage, entity);

                //排序
                DataView dv = dTable.DefaultView;
                string sort = string.IsNullOrEmpty(rp.Parameters.sort) ? "UserEmail asc" : rp.Parameters.sort;
                sort = "UserStatus desc," + sort;
                dv.Sort = sort;
                DataTable dt2 = dv.ToTable();
                dTable = dt2;


                if (dTable != null)
                    rdData.UserList = DataTableToObject.ConvertToList<UserInfo>(dTable);
                rdData.TotalPage = totalPage;
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

    #region 获取用户
    public class GetUserRP : IAPIRequestParameter
    {
        public string UserID { set; get; }
        public int PageIndex { set; get; }
        public int PageSize { set; get; }
        public string Keyword { set; get; }

        public string UnitID { set; get; }
        public string JobFunctionID { set; get; }

        public string sort { set; get; }

        public void Validate()
        {
            //if (string.IsNullOrEmpty(UserID))
            //    throw new APIException("【UserID】不能为空") { ErrorCode = 102 };
            if (PageSize == 0) PageSize = 15;
        }
    }
    public class GetUserRD : IAPIResponseData
    {
        public List<UserInfo> UserList { set; get; }
        public int TotalPage { set; get; }
    }
    #endregion
}