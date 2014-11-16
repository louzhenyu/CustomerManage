using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using JIT.CPOS.DTO.Base;
using JIT.Utility.ExtensionMethod;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.BS.BLL;
using JIT.Utility;

namespace JIT.CPOS.Web.ApplicationInterface.Product.QiXinManage.ActionHandler
{
    /// <summary>
    /// AddUser的摘要说明
    /// </summary>
    [Export(typeof(IQiXinRequestHandler))]
    [ExportMetadata("Action", "AddUser")]
    public class AddUserHandler : IQiXinRequestHandler
    {
        public string DoAction(string pRequest)
        {
            return AddUser(pRequest);
        }

        public string AddUser(string pRequest)
        {
            var rd = new APIResponse<AddUserRD>();
            var rdData = new AddUserRD();
            var rp = pRequest.DeserializeJSONTo<APIRequest<AddUserRP>>();

            if (rp.Parameters == null)
                throw new ArgumentException();

            if (rp.Parameters != null)
                rp.Parameters.Validate();

            var loggingSessionInfo = Default.GetBSLoggingSession(rp.CustomerID, rp.UserID);
            T_UserBLL bll = new T_UserBLL(loggingSessionInfo);
            try
            {
                string email = rp.Parameters.UserEmail, customerID = rp.CustomerID;
                T_UserEntity tue = new T_UserEntity();
                tue = bll.GetUserEntityByEmail(rp.Parameters.UserEmail, customerID);
                if (tue == null)
                {
                    DateTime dt = DateTime.Now;

                    #region 保存用户
                    string userId = Guid.NewGuid().ToString().Replace("-", "").ToUpper();
                    tue = new T_UserEntity()
                    {
                        user_id = userId,
                        user_code = rp.Parameters.UserCode,
                        user_name = rp.Parameters.UserName,
                        user_name_en = rp.Parameters.UserNameEn,
                        user_email = rp.Parameters.UserEmail,
                        user_password = MD5Helper.Encryption("123"),//默认密码
                        user_gender = rp.Parameters.UserGender,
                        user_birthday = rp.Parameters.UserBirthday,
                        user_telephone = rp.Parameters.UserTelephone,
                        user_cellphone = rp.Parameters.UserCellphone,
                        user_status = "1",
                        user_status_desc = "正常",
                        fail_date = string.IsNullOrEmpty(rp.Parameters.FailDate) == true ? "2020-01-01" : rp.Parameters.FailDate,
                        customer_id = customerID,
                        create_time = dt.ToString("yyyy-MM-dd HH:mm:ss"),
                        create_user_id = rp.UserID,
                        modify_time = dt.ToString("yyyy-MM-dd HH:mm:ss"),
                        modify_user_id = rp.UserID
                    };
                    //T_User表
                    //Create(tue, tran);
                    bll.Create(tue);
                    #endregion

                    #region 保存用户角色
                    //T_User_Role 表
                    TUserRoleEntity ture = new TUserRoleEntity
                    {
                        user_role_id = Guid.NewGuid().ToString(),
                        user_id = userId,
                        role_id = rp.Parameters.RoleID,
                        unit_id = rp.Parameters.UnitID,
                        status = "1",
                        create_time = dt,
                        create_user_id = rp.UserID,
                        modify_time = dt,
                        modify_user_id = rp.UserID,
                        default_flag = "1"
                    };
                    bll.InsertUserRole(ture);
                    #endregion

                    #region 保存部门、职衔
                    UserDeptJobMappingBLL deptJobMapBll = new UserDeptJobMappingBLL(loggingSessionInfo);
                    //UserDeptJobMapping表
                    UserDeptJobMappingEntity udjme = new UserDeptJobMappingEntity
                    {
                        UserDeptID = Guid.NewGuid(),
                        UserID = userId,
                        JobFunctionID = rp.Parameters.JobFunctionID,
                        USERID = userId,
                        CustomerID = customerID,
                        CreateTime = dt,
                        CreateUserID = rp.UserID,
                        LastUpdateTime = dt,
                        LastUpdateUserID = rp.UserID,
                        IsDelete = 0,
                        UnitID = rp.Parameters.UnitID,
                        LineManagerID = rp.Parameters.LineManagerID,
                        UserLevel = "0"//默认0
                    };
                    deptJobMapBll.Create(udjme);
                    #endregion

                    rdData.UserID = userId;
                    rd.ResultCode = 0;
                }
                else
                {
                    rd.ResultCode = 101;
                    rd.Message = "邮箱已存在";
                }
                rd.Data = rdData;
            }
            catch (Exception ex)
            {
                rd.ResultCode = 103;
                rd.Message = ex.Message;
            }
            return rd.ToJSON();
        }
    }

    #region 添加用户
    public class AddUserRP : IAPIRequestParameter
    {
        public string UserCode { set; get; }      //用户编码
        public string UserName { set; get; }      //用户名称
        public string UserNameEn { set; get; }    //英文名
        public string UserGender { set; get; }    //用户性别： 0未知、1男、2女
        public string UserBirthday { set; get; }//生日 格式：“yyyy-MM-dd”
        public string UserEmail { set; get; }     //用户邮箱
        public string UserPassword { set; get; }  //密码（md5加密后的）
        public string UserTelephone { set; get; } //手机
        public string UserCellphone { set; get; } //电话
        public string UserStatus { set; get; }    //用户状态：0封锁 1启用
        public string FailDate { set; get; }	  //有效日期,默认2020-01-10
        public string JobFunctionID { set; get; } //职衔标识
        public string UnitID { set; get; }        //部门标识
        public string RoleID { set; get; }        //角色标识
        public string LineManagerID { set; get; } //直接上级ID
        public void Validate()
        {
            if (string.IsNullOrEmpty(UserCode))
                throw new APIException("【UserCode】不能为空") { ErrorCode = 102 };
            if (string.IsNullOrEmpty(UserName))
                throw new APIException("【UserName】不能为空") { ErrorCode = 102 };
            if (string.IsNullOrEmpty(UserGender))
                throw new APIException("【UserGender】不能为空") { ErrorCode = 102 };
            if (string.IsNullOrEmpty(UserEmail))
                throw new APIException("【UserEmail】不能为空") { ErrorCode = 102 };
            if (string.IsNullOrEmpty(UserPassword))
                throw new APIException("【UserPassword】不能为空") { ErrorCode = 102 };
            if (string.IsNullOrEmpty(UserTelephone))
                throw new APIException("【UserTelephone】不能为空") { ErrorCode = 102 };
            if (string.IsNullOrEmpty(UserStatus))
                throw new APIException("【UserStatus】不能为空") { ErrorCode = 102 };
            if (string.IsNullOrEmpty(JobFunctionID))
                throw new APIException("【JobFunctionID】不能为空") { ErrorCode = 102 };
            if (string.IsNullOrEmpty(UnitID))
                throw new APIException("【UnitID】不能为空") { ErrorCode = 102 };
            if (string.IsNullOrEmpty(RoleID))
                throw new APIException("【RoleID】不能为空") { ErrorCode = 102 };
            if (string.IsNullOrEmpty(LineManagerID))
                throw new APIException("【LineManagerID】不能为空") { ErrorCode = 102 };
        }
    }
    public class AddUserRD : IAPIResponseData
    {
        public string UserID { set; get; }
    }
    #endregion
}