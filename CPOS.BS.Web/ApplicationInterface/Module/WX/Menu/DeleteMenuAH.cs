using System;
using System.Collections.Generic;
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
    public class DeleteMenuAH : BaseActionHandler<DeleteMenuRP, DeleteMenuRD>
    {
        public const int ErrorMenuIdInavial = 120;
        public const int ErrorFirstLevelNotDelete = 121;

        protected override DeleteMenuRD ProcessRequest(DTO.Base.APIRequest<DeleteMenuRP> pRequest)
        {
            var rd = new DeleteMenuRD();
            var menuId = pRequest.Parameters.MenuId;

            var bll = new WMenuBLL(CurrentUserInfo);

            var entity = bll.QueryByEntity(new WMenuEntity()
            {
                ID = menuId
            }, null);

            var level = entity[0].Level;
            if (level == "1")
            {
                throw new APIException("一级菜单不能删除") { ErrorCode = ErrorFirstLevelNotDelete };
            }
            if (entity.Length>0)
            {
                bll.Delete(entity);
            }
            else
            {
                throw new APIException("MenuId无效") { ErrorCode = ErrorMenuIdInavial };
            }

            return rd;

        }
    }
}