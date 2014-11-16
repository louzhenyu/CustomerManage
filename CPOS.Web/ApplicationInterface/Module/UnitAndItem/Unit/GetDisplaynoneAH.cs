using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using JIT.CPOS.Web.ApplicationInterface.Base;
using JIT.CPOS.DTO.Base;
using JIT.CPOS.DTO.Module.UnitAndItem.Unit.Request;
using JIT.CPOS.DTO.Module.UnitAndItem.Unit.Response;
using JIT.CPOS.DTO.Module.UnitAndItem.Unit;
using JIT.CPOS.BS.BLL;

namespace JIT.CPOS.Web.ApplicationInterface.Module.UnitAndItem.Unit
{
    public class GetDisplaynoneAH : BaseActionHandler<EmptyRequestParameter, GetDisplaynoneRD>
    {
        protected override GetDisplaynoneRD ProcessRequest(APIRequest<EmptyRequestParameter> pRequest)
        {
            GetDisplaynoneRD rd = new GetDisplaynoneRD();
            var UnitServiceBll = new UnitService(base.CurrentUserInfo);
            CustomerBasicSettingBLL customerBasic = new CustomerBasicSettingBLL(this.CurrentUserInfo);
            bool isShowAll = customerBasic.CheckIsAllAccessoriesStores();//判断是否显示所有门店
            bool isShowSearch = customerBasic.CheckIsSearchAccessoriesStores();//判断是否显示搜索栏
            string forwardingMessageLogo=customerBasic.GetForwardingMessageLogo();//转发消息图标
            string forwardingMessageTitle = customerBasic.GetForwardingMessageTitle();//转发消息默认标题
            string forwardingMessageSummary = customerBasic.GetForwardingMessageSummary();//转发消息默认摘要文字
            rd.IsAllAccessoriesStores = isShowAll; //是否显示全部
            rd.IsSearchAccessoriesStores = isShowSearch;  //是否显示搜索栏
            rd.ForwardingMessageLogo = forwardingMessageLogo;//转发消息图标
            rd.ForwardingMessageTitle = forwardingMessageTitle;//转发消息默认标题
            rd.ForwardingMessageSummary = forwardingMessageSummary;//转发消息默认摘要文字

            return rd;
        }
    }
}