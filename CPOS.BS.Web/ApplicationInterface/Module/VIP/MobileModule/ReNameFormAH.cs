/*
 表单重命名
表：MobileModule

Action名	VIP.MobileModule.ReNameForm
调用方式	POST
说明	获取表单列表
参数	        参数类型    	说明
MobileModuleID	String	表单ID
Name	    string	名称
		
返回值	
属性	        类型  	描述
IsSuccess	bool	是否成功
Msg	        string	不成功描述

 */

using System;
using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.DTO.Module.VIP.MobileModule.Request;
using JIT.CPOS.DTO.Module.VIP.MobileModule.Response;
using JIT.CPOS.BS.Web.ApplicationInterface.Base;

namespace JIT.CPOS.BS.Web.ApplicationInterface.Module.VIP.MobileModule
{
    public class ReNameFormAH : BaseActionHandler<ReNameFormRP, ReNameFormRD>
    {

        protected override ReNameFormRD ProcessRequest(DTO.Base.APIRequest<ReNameFormRP> pRequest)
        {
            if ( string.IsNullOrEmpty(pRequest.Parameters.Name) )
            {
                return  new ReNameFormRD
                {
                    IsSuccess = false, Msg = "无效的名称"
                };
            }
            var result =  new ReNameFormRD{ IsSuccess = true  };
            var bll = new MobileModuleBLL(CurrentUserInfo);
            if (string.IsNullOrEmpty(pRequest.Parameters.MobileModuleID))
            {
                var entity = new MobileModuleEntity();
                entity.ModuleName =  pRequest.Parameters.Name;
                //entity.MobileModuleID = Guid.NewGuid();
                entity.CustomerID = CurrentUserInfo.ClientID;
                entity.IsTemplate = pRequest.Parameters.Type == 1 ? 1 : 0; //注册活动表单 默认为模板
                entity.ModuleType = pRequest.Parameters.Type;
                //bll.Create(entity);
                bll.CreateWithMobilePageBlock(entity);
            }
            else
            {
                var entity = bll.GetByID(pRequest.Parameters.MobileModuleID);
                entity.ModuleName = pRequest.Parameters.Name;
                bll.Update(entity);
                result.MobileModuleID = entity.MobileModuleID.ToString();
            }
           
            return result;

        }
    }
}