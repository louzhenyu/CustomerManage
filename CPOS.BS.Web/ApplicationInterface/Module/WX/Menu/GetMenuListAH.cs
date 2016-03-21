using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.BS.Web.ApplicationInterface.Base;
using JIT.CPOS.DTO.Base;
using JIT.CPOS.DTO.Module.WeiXin.Menu.Request;
using JIT.CPOS.DTO.Module.WeiXin.Menu.Response;

namespace JIT.CPOS.BS.Web.ApplicationInterface.Module.WX.Menu
{
    public class GetMenuListAH : BaseActionHandler<GetMenuListRP, GetMenuListRD>
    {
        protected override GetMenuListRD ProcessRequest(DTO.Base.APIRequest<GetMenuListRP> pRequest)
        {
            var rd = new GetMenuListRD();

            string applicationId = pRequest.Parameters.ApplicationId;
            string customerId = this.CurrentUserInfo.ClientID;

            var bll = new WMenuBLL(this.CurrentUserInfo);

            var wappBll = new WApplicationInterfaceBLL(CurrentUserInfo);

            var wappEntity = wappBll.QueryByEntity(new WApplicationInterfaceEntity()
            {
                ApplicationId = applicationId
            }, null);

            if (wappEntity.Length == 0 || wappEntity.Any() == false)
            {
                throw new APIException("该微信公众号无效") {ErrorCode = 120};
            }
            var weixinId = wappEntity[0].WeiXinID;


            var ds = bll.GetMenuList(customerId, applicationId);

            if (ds.Tables[0].Rows.Count == 0)
            {
                rd.MenuList = null;
            }
            else
            {
                string menuList =
                    ds.Tables[0].AsEnumerable().Aggregate("", (x, j) =>
                    {
                        x += string.Format("'{0}',", j["ID"].ToString());
                        return x;
                    }).Trim(',');

                #region 根据菜单ID从图文映射表里面关联到图文表里面数据

           //  var textDs = bll.GetMenuTextIdList(customerId, weixinId, menuList);

                #endregion

                var temp =
                    ds.Tables[0].AsEnumerable()
                       .Where(t => t["level"].ToString() == "1")  //先取第一层
                        .OrderBy(t => t["Status"].ToString())
                        .OrderBy(t => Convert.ToInt32(t["DisplayColumn"]))
                        .Select(t => new MenuInfo
                        {
                            MenuId = t["ID"].ToString(),
                            WeiXinId = t["WeiXinID"].ToString(),
                            ApplicationId = applicationId,
                            Name = t["Name"].ToString(),
                            DisplayColumn = Convert.ToInt32(t["DisplayColumn"]),
                            Level = Convert.ToInt32(t["Level"]),
                            ParentId = t["ParentId"].ToString(),
                            Status = Convert.ToInt32(t["Status"]),
                            SubMenus =
                                ds.Tables[0].AsEnumerable()
                                    .Where(
                                        tt =>
                                            tt["ParentId"].ToString() == t["ID"].ToString() &&
                                            tt["level"].ToString() == "2")//子菜单里取第二层
                                    .OrderBy(tt => tt["Status"].ToString())
                                    .OrderBy(tt => Convert.ToInt32(tt["DisplayColumn"]))
                                    .Select(tt => new MenuChildInfo
                                    {
                                        MenuId = tt["ID"].ToString(),
                                        WeiXinId = tt["WeiXinID"].ToString(),
                                        ApplicationId = applicationId,
                                        Name = tt["Name"].ToString(),
                                        DisplayColumn = Convert.ToInt32(tt["DisplayColumn"]),
                                        Level = Convert.ToInt32(tt["Level"]),
                                        ParentId = tt["ParentId"].ToString(),
                                        Status = Convert.ToInt32(tt["Status"]),
                                    }).ToArray()
                        });
                rd.MenuList = temp.ToArray();
            }
            return rd;
        }
    }
}