using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using JIT.CPOS.DTO.Module.VIP.Login.Response;
using JIT.CPOS.Web.ApplicationInterface.Base;
using JIT.CPOS.DTO.Module.VIP.Login.Request;
using JIT.CPOS.DTO.Base;
using JIT.CPOS.BS.BLL;
using System.Data;
using JIT.CPOS.BS.Entity;

namespace JIT.CPOS.Web.ApplicationInterface.Module.VIP.Login
{
    public class SetSignInAH : BaseActionHandler<SetSignInRP, SetSignInRD>
    {
        #region 错误码
        private const int Error_CustomerCode_NotNull = 103;
        public const int Error_CustomerCode_NotExist = 104;
        public const int Error_UserName_InValid = 105;
        public const int Error_Password_InValid = 106;
        public const int Error_UserRole_NotExist = 107;
        #endregion

        protected override SetSignInRD ProcessRequest(DTO.Base.APIRequest<SetSignInRP> pRequest)
        {
            SetSignInRD rd = new SetSignInRD();

            string customerCode = pRequest.Parameters.CustomerCode;

            string phone = pRequest.Parameters.LoginName;

            string password = pRequest.Parameters.Password;

            if (string.IsNullOrEmpty(customerCode))
            {
                throw new APIException("客户代码不能为空") { ErrorCode = Error_CustomerCode_NotNull };
            }

            WMenuBLL menuServer = new WMenuBLL(Default.GetAPLoggingSession(""));
            string customerId = menuServer.GetCustomerIDByCustomerCode(customerCode);

            if (string.IsNullOrEmpty(customerId))
            {
                throw new APIException("客户代码对应的客户不存在") { ErrorCode = Error_CustomerCode_NotExist };              
            }
            var currentUserInfo = Default.GetBSLoggingSession(customerId, "1");

            VipBLL vipBll = new VipBLL(currentUserInfo);
          
            #region 判断用户是否存在
            if (!vipBll.JudgeUserExist(phone, customerId))
            {
                throw new APIException("用户名无效") { ErrorCode = Error_UserName_InValid }; 
            }
            
            #endregion

            #region 判断密码是否正确
            if (!vipBll.JudgeUserPasswordExist(phone, customerId,password))
            {
                throw new APIException("登录密码错误") { ErrorCode = Error_Password_InValid };
            }

            #endregion

            #region 判断该客服人员是否有客服或操作订单的权限
            //if (!vipBll.JudgeUserRoleExist(phone, customerId, password))
            //{
            //    throw new APIException("该账号无权登录本系统") { ErrorCode = Error_Password_InValid };
            //}
            #endregion

            #region 获取UserId
            var ds = vipBll.GetUserIdByUserNameAndPassword(phone, customerId, password);
            rd.UserId = ds.Tables[0].Rows[0]["user_id"].ToString();
            rd.UserName = ds.Tables[0].Rows[0]["user_name"].ToString();
            rd.Status =int.Parse(ds.Tables[0].Rows[0]["user_status"].ToString());
            rd.CustomerId = customerId;

            #endregion
            //如果状态不等于1，就说明已经停用，
            if (rd.Status != 1)
            {
                throw new APIException("该员工已经被停用，请联系管理员") { ErrorCode = Error_Password_InValid };
            }


            #region 获取角色列表
            var roleCodeDs = vipBll.GetRoleCodeByUserId(rd.UserId, customerId);

            var tmp = roleCodeDs.Tables[0].AsEnumerable().Select(t => new RoleCodeInfo()
            {
                RoleCode = t["role_code"].ToString()
            });

            #endregion
            rd.UnitId = vipBll.GetUnitByUserId(rd.UserId);//获取会集店
            TUnitBLL tUnitBLL = new TUnitBLL(currentUserInfo);
            if (!string.IsNullOrEmpty(rd.UnitId))
            {
                rd.UnitName = tUnitBLL.GetByID(rd.UnitId).UnitName;
            }
            else
            {
                rd.UnitName = "";
            }


            //app登陆用户权限 add by henry 2015-3-26
            var roleCodeList = vipBll.GetAppMenuByUserId(rd.UserId);
            if (roleCodeList != null)
            {
                rd.MenuCodeList = DataTableToObject.ConvertToList<Menu>(roleCodeList.Tables[0]);
            }

            rd.RoleCodeList = tmp.ToArray();
            rd.CustomerName = currentUserInfo.ClientName;


            //销售员头像
            ObjectImagesBLL _ObjectImagesBLL = new ObjectImagesBLL(currentUserInfo);
            ObjectImagesEntity en = new ObjectImagesEntity();
            en.ObjectId = rd.UserId;//根据获取的用户ID
            List<ObjectImagesEntity> ImgList = _ObjectImagesBLL.QueryByEntity(en, null).OrderByDescending(p => p.CreateTime).ToList();
            if (ImgList != null && ImgList.Count != 0)
            {
                // string fileDNS = customerBasicSettingBll.GetSettingValueByCode("FileDNS"); ;//http://182.254.156.57:811
                rd.HeadImg = ImgList[0].ImageURL;
            }


            return rd;
        }
    }
}