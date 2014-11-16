using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Web.ApplicationInterface.Base;
using JIT.CPOS.DTO.Module.WeiXin.Menu.Request;
using JIT.CPOS.DTO.Module.WeiXin.Menu.Response;

namespace JIT.CPOS.BS.Web.ApplicationInterface.Module.WX.Menu
{
    public class GetMenuDetailAH : BaseActionHandler<GetMenuDetailRP, GetMenuDetailRD>
    {
        protected override GetMenuDetailRD ProcessRequest(DTO.Base.APIRequest<GetMenuDetailRP> pRequest)
        {
            var rd = new GetMenuDetailRD();

            string menuId = pRequest.Parameters.MenuId;
            
            var bll = new WMenuBLL(this.CurrentUserInfo);

            var ds = bll.GetMenuDetail(menuId);
            var textDs = bll.GetMenuTextIdListByMenuId(CurrentUserInfo.ClientID, menuId);
      
            var temp = ds.Tables[0].AsEnumerable().Select(t => new MenuDetailInfo()
            {
                MenuId = t["ID"].ToString(),
                PageUrlJson = t["PageUrlJson"].ToString(),
                WeiXinId = t["WeiXinID"].ToString(),
                Name = t["Name"].ToString(),
                DisplayColumn = Convert.ToInt32(t["DisplayColumn"]),
                Text = t["Text"].ToString(),
                MenuUrl = t["MenuUrl"].ToString(),
                Level = Convert.ToInt32(t["Level"]),
                MessageType = t["materialTypeId"].ToString(),
                PageId = string.IsNullOrEmpty(t["PageId"].ToString()) == true ? "" : t["PageId"].ToString(),
                ImageUrl = t["imageUrl"].ToString(),
                PageParamJson = t["PageParamJson"].ToString(),
                ParentId = t["ParentId"].ToString(),
                Status = Convert.ToInt32(t["Status"]),
                UnionTypeId = Convert.ToInt32(t["BeLinkedType"]),
                ImageId = t["ImageId"].ToString(),
                MaterialTextIds =
                    textDs.Tables[0].AsEnumerable()
                        .Where(tt => tt["MenuId"].ToString() == t["ID"].ToString())
                        .OrderBy(tt=>tt["DisplayIndex"].ToString())
                        .Select(tt => new MaterialTextIdInfo
                        {
                            TestId = string.IsNullOrEmpty(tt["TextId"].ToString()) ? "" : tt["TextId"].ToString(),
                            DisplayIndex =
                                string.IsNullOrEmpty(tt["DisplayIndex"].ToString())
                                    ? 0
                                    : Convert.ToInt32(tt["DisplayIndex"]),
                            ImageUrl =
                                string.IsNullOrEmpty(tt["CoverImageUrl"].ToString()) ? "" : tt["CoverImageUrl"].ToString(),
                            Title = string.IsNullOrEmpty(tt["Title"].ToString()) ? "" : tt["Title"].ToString(),
                            Author = string.IsNullOrEmpty(tt["Author"].ToString()) ? "" : tt["Author"].ToString(),
                            Text = string.IsNullOrEmpty(tt["Text"].ToString()) ? "" : tt["Text"].ToString(),
                            OriginalUrl = string.IsNullOrEmpty(tt["OriginalUrl"].ToString()) ? "" : tt["OriginalUrl"].ToString()
                        }).DefaultIfEmpty().ToArray()
            });
            rd.MenuList = temp.ToArray();
            return rd;
        }
    }
}