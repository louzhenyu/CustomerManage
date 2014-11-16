/*
 表单列表
表：MobileModule

Action名	VIP.MobileModule.GetForms
调用方式	POST
说明	获取表单列表
参数	参数类型	说明
Page	    int	页码， 默认1
PageSize	int	显示数， 默认15
Type	    int	表单类型。1.注册表单，2活动表单
CustoemrID  string	ClientID
返回值	
属性  	类型  	描述	     -----------子属性------------------
Items	数组	    表单	    属性	            数据类型	    说明
			            MobileModuleID 	String	主键
			            ModuleName	    string	表单名称
			            UsedCount       int	    使用次数
			            IsTemplate      Int	    是否为模块表单。0：否，1是。
TotalRow int	总行数	

 */
using JIT.CPOS.BS.BLL;
using JIT.CPOS.DTO.Base;
using JIT.CPOS.DTO.Module.VIP.MobileModule.Request;
using JIT.CPOS.DTO.Module.VIP.MobileModule.Response;
using JIT.CPOS.Web.ApplicationInterface.Base;
using JIT.Utility.Reflection;

namespace JIT.CPOS.Web.ApplicationInterface.Module.VIP.MobileModule
{
    /// <summary>
    /// 获取表单元素
    /// </summary>
    public class GetFormsAH : BaseActionHandler<GetFormsRP, GetFormsRD>
    {

        protected override GetFormsRD ProcessRequest( APIRequest<GetFormsRP> pRequest)
        {
            pRequest.Parameters.Validate();
            int totalRow;
            var dt = new MobileModuleBLL(CurrentUserInfo).GetFormsTable(CurrentUserInfo.ClientID,
                pRequest.Parameters.Type, 
                pRequest.Parameters.Page, 
                pRequest.Parameters.PageSize,
                out totalRow);
            if (dt == null)
            {
                return new GetFormsRD{ Items =  new DTO.Module.VIP.MobileModule.Response.MobileModule[0]};
            }
            var result = new GetFormsRD();
            result.TotalRow = totalRow;
            result.Items = DataLoader.LoadFrom<DTO.Module.VIP.MobileModule.Response.MobileModule>(dt);
            return result;
        }
    }
}