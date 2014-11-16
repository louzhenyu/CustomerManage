using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using JIT.CPOS.DTO.Base;
using JIT.Utility.ExtensionMethod;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.BS.BLL;
using System.Data;
using JIT.CPOS.Web.RateLetterInterface;

namespace JIT.CPOS.Web.ApplicationInterface.Product.QiXinManage.ActionHandler
{
    /// <summary>
    /// GetPersonDetailInfo的摘要说明
    /// </summary>
    [Export(typeof(IQiXinRequestHandler))]
    [ExportMetadata("Action", "GetPersonDetailInfo")]
    public class GetPersonDetailInfoHandler : IQiXinRequestHandler
    {
        public string DoAction(string pRequest)
        {
            return GetPersonDetailInfo(pRequest);
        }

        public string GetPersonDetailInfo(string pRequest)
        {
            var rd = new APIResponse<GetPersonDetailInfoRD>();
            var rdData = new GetPersonDetailInfoRD();

            var rp = pRequest.DeserializeJSONTo<APIRequest<GetPersonDetailInfoRP>>();

            if (rp.Parameters == null)
                throw new ArgumentException();

            if (rp.Parameters != null)
                rp.Parameters.Validate();

            var loggingSessionInfo = Default.GetBSLoggingSession(rp.CustomerID, rp.UserID);
            try
            {
                T_UserBLL bll = new T_UserBLL(loggingSessionInfo);
                T_UserEntity entity = bll.GetUserEntityByID(rp.Parameters.UserID);
                PersonDetailInfo detail = null;
                if (entity != null)
                {
                    //用户信息
                    detail = new PersonDetailInfo
                    {
                        UserID = entity.user_id,
                        UserCode = entity.user_code,
                        UserName = entity.user_name,
                        UserGender = entity.user_gender,
                        UserBirthday = entity.user_birthday,
                        UserEmail = entity.user_email,
                        UserCellphone = entity.user_telephone,
                        UserTelephone = entity.user_telephone
                    };
                    //获取UnitID,UnitName
                    UserDeptJobMappingBLL mappingBll = new UserDeptJobMappingBLL(loggingSessionInfo);
                    UserDeptJobMappingEntity mappingEntity = mappingBll.GetByUserID(entity.user_id);
                    if (mappingEntity != null)
                    {
                        detail.UnitID = mappingEntity.UnitID;
                        TUnitBLL unitBll = new TUnitBLL(loggingSessionInfo);
                        TUnitEntity unitEntity = unitBll.GetByID(mappingEntity.UnitID);
                        if (unitEntity != null)
                            detail.UnitName = unitEntity.UnitName;
                    }
                    //获取是否有建群权限
                    detail.IsIMGroupCreator = bll.IsHasPower(entity.user_id, UserRightCode.USER_CREATE_GROUP_RIGHT_CODE);
                }
                rdData.DetailInfo = detail;
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

    #region 获取个人信息详情
    public class GetPersonDetailInfoRP : IAPIRequestParameter
    {
        public string UserID { set; get; }
        public void Validate()
        {
            if (string.IsNullOrEmpty(UserID)) throw new APIException("UserID不能为空") { ErrorCode = 102 };
        }
    }
    public class GetPersonDetailInfoRD : IAPIResponseData
    {
        public PersonDetailInfo DetailInfo { set; get; }
    }
    #endregion
}