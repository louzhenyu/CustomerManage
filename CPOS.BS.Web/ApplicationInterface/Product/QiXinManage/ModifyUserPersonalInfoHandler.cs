using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using JIT.CPOS.DTO.Base;
using JIT.Utility.ExtensionMethod;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.BS.BLL;
using JIT.Utility;

namespace JIT.CPOS.BS.Web.ApplicationInterface.Product.QiXinManage
{
    /// <summary>
    /// ModifyUserPersonalInfo的摘要说明
    /// </summary>
    [Export(typeof(IQiXinRequestHandler))]
    [ExportMetadata("Action", "ModifyUserPersonalInfo")]
    public class ModifyUserPersonalInfoHandler : IQiXinRequestHandler
    {
        public string DoAction(string pRequest)
        {
            return ModifyUserPersonalInfo(pRequest);
        }

        public string ModifyUserPersonalInfo(string pRequest)
        {
            var rd = new APIResponse<ModifyUserPersonalInfoRD>();
            var rdData = new ModifyUserPersonalInfoRD();
            var rp = pRequest.DeserializeJSONTo<APIRequest<ModifyUserPersonalInfoRP>>();

            if (rp.Parameters == null)
                throw new ArgumentException();

            if (rp.Parameters != null)
                rp.Parameters.Validate();

            var loggingSessionInfo = new LoggingSessionManager().CurrentSession;

            try
            {
                T_UserBLL bll = new T_UserBLL(loggingSessionInfo);
                T_UserEntity entity = bll.GetUserEntityByID(rp.Parameters.UserID);
                if (entity != null)
                {
                    entity.user_code = rp.Parameters.UserCode;
                    entity.user_name = rp.Parameters.UserName;
                    entity.user_gender = rp.Parameters.UserGender;
                    entity.user_email = rp.Parameters.UserEmail;
                    entity.user_telephone = rp.Parameters.UserTelephone;
                    //entity.user_status = rp.Parameters.UserStatus;

                    if (!string.IsNullOrEmpty(rp.Parameters.UserNameEn))
                        entity.user_name_en = rp.Parameters.UserNameEn;
                    if (!string.IsNullOrEmpty(rp.Parameters.UserBirthday))
                        entity.user_birthday = rp.Parameters.UserBirthday;
                    if (!string.IsNullOrEmpty(rp.Parameters.UserPassword))
                        entity.user_password = MD5Helper.Encryption(rp.Parameters.UserPassword);
                    if (!string.IsNullOrEmpty(rp.Parameters.UserCellphone))
                        entity.user_cellphone = rp.Parameters.UserCellphone;
                    if (!string.IsNullOrEmpty(rp.Parameters.FailDate))
                        entity.fail_date = rp.Parameters.FailDate;

                    bll.Update(entity);

                    //更新LineManagerID
                    UserDeptJobMappingBLL mappingBll = new UserDeptJobMappingBLL(loggingSessionInfo);
                    UserDeptJobMappingEntity mappingEntity = mappingBll.GetByUserID(entity.user_id);
                    if (mappingEntity != null)
                    {
                        if (!string.IsNullOrEmpty(rp.Parameters.LineManagerID))
                            mappingEntity.LineManagerID = rp.Parameters.LineManagerID;
                        if (!string.IsNullOrEmpty(rp.Parameters.UnitID))
                            mappingEntity.UnitID = rp.Parameters.UnitID;
                        if (!string.IsNullOrEmpty(rp.Parameters.JobFunctionID))
                            mappingEntity.JobFunctionID = rp.Parameters.JobFunctionID;
                        mappingBll.Update(mappingEntity);
                    }
                    else
                    {
                        mappingEntity = new UserDeptJobMappingEntity();
                        mappingEntity.UserID = entity.user_id;
                        mappingEntity.USERID = entity.user_id;
                        mappingEntity.LineManagerID = rp.Parameters.LineManagerID;
                        mappingEntity.UnitID = rp.Parameters.UnitID;
                        mappingEntity.JobFunctionID = rp.Parameters.JobFunctionID;
                        mappingBll.Create(mappingEntity);
                    }
                    rdData.IsSuccess = true;
                    rd.ResultCode = 0;
                }
                else
                {
                    rdData.IsSuccess = false;
                    rd.ResultCode = 101;
                    rd.Message = "用户不存在";
                }
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

    #region 变更用户个人信息
    public class ModifyUserPersonalInfoRP : IAPIRequestParameter
    {
        public string UserID { set; get; }
        public string UserCode { set; get; }      //用户编码
        public string UserName { set; get; }      //用户名称
        public string UserNameEn { set; get; }    //英文名
        public string UserGender { set; get; }    //用户性别： 0未知、1男、2女
        public string UserBirthday { set; get; }//生日 格式：“yyyy-MM-dd”
        public string UserEmail { set; get; }     //用户邮箱
        public string UserPassword { set; get; }  //密码（md5加密后的）
        public string UserTelephone { set; get; } //手机
        public string UserCellphone { set; get; } //电话
        //public string UserStatus { set; get; }    //用户状态：0封锁 1启用
        public string FailDate { set; get; }	  //有效日期,默认2020-01-10
        public string LineManagerID { set; get; } //直接上级

        public string UnitID { set; get; } //部门id
        public string JobFunctionID { set; get; } //职衔id

        public string RoleID { set; get; } //角色id


        public void Validate()
        {
            if (string.IsNullOrEmpty(UserID))
                throw new APIException("【UserID】不能为空") { ErrorCode = 102 };
            if (string.IsNullOrEmpty(UserCode))
                throw new APIException("【UserCode】不能为空") { ErrorCode = 102 };
            if (string.IsNullOrEmpty(UserName))
                throw new APIException("【UserName】不能为空") { ErrorCode = 102 };
            if (string.IsNullOrEmpty(UserGender))
                throw new APIException("【UserGender】不能为空") { ErrorCode = 102 };
            if (string.IsNullOrEmpty(UserEmail))
                throw new APIException("【UserEmail】不能为空") { ErrorCode = 102 };
            if (string.IsNullOrEmpty(UserTelephone))
                throw new APIException("【UserTelephone】不能为空") { ErrorCode = 102 };
            //if (string.IsNullOrEmpty(UserStatus))
            //    throw new APIException("【UserStatus】不能为空") { ErrorCode = 102 };
            if (string.IsNullOrEmpty(LineManagerID))
                throw new APIException("【LineManagerID】不能为空") { ErrorCode = 102 };
        }
    }
    public class ModifyUserPersonalInfoRD : IAPIResponseData
    {
        public bool IsSuccess { set; get; }
    }
    #endregion
}