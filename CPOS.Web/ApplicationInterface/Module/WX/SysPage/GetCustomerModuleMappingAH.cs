/*
 * Author		:Alex.Tian
 * EMail		:changjian.tian@jitmarketing.cn
 * Company		:JIT
 * Create On	:2014/6/11 9:37
 * Description	:获取客户所属套餐列表
 * 1st Modified On	:
 * 1st Modified By	:
 * 1st Modified Desc:
 * 1st Modifier EMail	:
 * 2st Modified On	:
 * 2st Modified By	:
 * 2st Modifier EMail	:
 * 2st Modified Desc:
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


using JIT.CPOS.Web.ApplicationInterface.Base;
using JIT.CPOS.DTO.Module.WeiXin.SysPage.Request;
using JIT.CPOS.DTO.Module.WeiXin.SysPage.Response;
using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Entity;
using JIT.Utility.ExtensionMethod;
using JIT.CPOS.DTO.Base;

namespace JIT.CPOS.Web.ApplicationInterface.Module.WX.SysPage
{
    public class GetCustomerModuleMappingAH: BaseActionHandler<GetCustomerModuleMappingRP, GetCustomerModuleMappingRD>
    {

        protected override GetCustomerModuleMappingRD ProcessRequest(APIRequest<GetCustomerModuleMappingRP> pRequest)
        {
            GetCustomerModuleMappingRD rd = new GetCustomerModuleMappingRD();

            var userInfo = this.CurrentUserInfo;
            userInfo.CurrentLoggingManager.Connection_String = System.Configuration.ConfigurationManager.AppSettings["Conn_ap"];
            CustomerModuleMappingBLL bll = new CustomerModuleMappingBLL(userInfo);
            CustomerModuleMappingEntity entity=new CustomerModuleMappingEntity ();
            entity.CustomerID= pRequest.Parameters.CustomerId;
            var list = bll.QueryByEntity(entity, null);
            var temp = new List<string> { };
            if (list!=null)
            {
                foreach (var item in list)
                {
                    string customerMapping = item.VocaVerMappingID.ToString();
                    temp.Add(customerMapping);
                }
                rd.CustomerModuleMapping = temp.ToArray();
            }
            return rd;
        }
    }
}