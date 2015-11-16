using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Web.ApplicationInterface.Base;
using JIT.CPOS.BS.Web.Session;
using JIT.CPOS.DTO.Module.Basic.Menu.Request;
using JIT.CPOS.DTO.Module.Basic.Menu.Response;
using JIT.Utility.DataAccess.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JIT.CPOS.BS.Web.ApplicationInterface.Module.Basic.Menu
{
    public class GetFunctionListAH : BaseActionHandler<GetFuntionListRP, GetFuntionListRD>
    {
        protected override GetFuntionListRD ProcessRequest(DTO.Base.APIRequest<GetFuntionListRP> pRequest)
        {
            var rd = new GetFuntionListRD();
            var para = pRequest.Parameters;
            var loggingSessionInfo = new SessionManager().CurrentUserLoginInfo;
            var menuFunctionBLL = new T_Menu_FunctionBLL(loggingSessionInfo);

            //查询参数
            List<IWhereCondition> complexCondition = new List<IWhereCondition> { };
            complexCondition.Add(new EqualsCondition() { FieldName = "rmf.CustomerID", Value = loggingSessionInfo.ClientID });
            complexCondition.Add(new EqualsCondition() { FieldName = "rmf.RoleID", Value = loggingSessionInfo.CurrentUserRole.RoleId });
            complexCondition.Add(new EqualsCondition() { FieldName = "rmf.MenuID", Value = para.MenuID });

            var tempList = menuFunctionBLL.GetMenuFunctionList(complexCondition.ToArray(),null);

            rd.FunctionList = tempList.Select(t => new FunctionInfo()
            {
                FunctionCode=t.FunctionCode,
                FunctionName=t.FunctionName
            }).ToList();

            return rd;
        }
    }
}