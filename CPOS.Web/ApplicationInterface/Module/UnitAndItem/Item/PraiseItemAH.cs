using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using JIT.CPOS.DTO.Module.UnitAndItem.Item.Response;
using JIT.CPOS.DTO.Module.UnitAndItem.Item.Request;
using JIT.CPOS.Web.ApplicationInterface.Base;
using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Entity;

namespace JIT.CPOS.Web.ApplicationInterface.Module.UnitAndItem.Item
{
    public class PraiseItemAH : BaseActionHandler<PraiseItemRP, PraiseItemRD>
    {
        protected override PraiseItemRD ProcessRequest(DTO.Base.APIRequest<PraiseItemRP> pRequest)
        {
            PraiseItemRD rd = new PraiseItemRD();
            string itemId = pRequest.Parameters.ItemID.ToString();
            string userId = CurrentUserInfo.CurrentUser.User_Id;
            var  bll = new ItemPraiseBLL(CurrentUserInfo);
            bool b = bll.JudgeItemPraiseByUser(itemId, userId);

            if (!b)
            {
                var entity = new ItemPraiseEntity()
                {
                    PraiseId = Guid.NewGuid(),
                    VipId = userId,
                    ItemID = itemId
                };
                bll.Create(entity);                
            }

            return rd;

        }
    }
}