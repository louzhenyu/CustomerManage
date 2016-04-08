using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using JIT.CPOS.BS.Web.ApplicationInterface.Base;
using JIT.CPOS.BS.Web.Session;
using JIT.CPOS.DTO.Module.VIP.VipCard.Request;
using JIT.CPOS.DTO.Module.VIP.VipCard.Response;
using JIT.Utility.DataAccess.Query;
using JIT.CPOS.BS.BLL;
using JIT.Utility.DataAccess;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.DTO.Base;

namespace JIT.CPOS.BS.Web.ApplicationInterface.Module.VIP.VIPCard
{
    public class GetVipCardTypeListAH : BaseActionHandler<GetVipCardTypeListRP, GetVipCardTypeListRD>
    {
        protected override GetVipCardTypeListRD ProcessRequest(DTO.Base.APIRequest<GetVipCardTypeListRP> pRequest)
        {
            GetVipCardTypeListRP rp = pRequest.Parameters;
            GetVipCardTypeListRD rd = new GetVipCardTypeListRD();
            rd.VipCardTypeIdList = new List<VipCardTypeInfo>();


            VipCardBLL vipCardBll = new VipCardBLL(CurrentUserInfo);
            SysVipCardTypeBLL sysVipCardTypeBll = new SysVipCardTypeBLL(CurrentUserInfo);

            var vipCardInfo = vipCardBll.GetVipCardByVipMapping(rp.VipId);
            List<SysVipCardTypeEntity> sysVipCardTypeAllList = sysVipCardTypeBll.QueryByEntity(new SysVipCardTypeEntity() { CustomerID = CurrentUserInfo.ClientID }, null).ToList();
            if (sysVipCardTypeAllList != null && vipCardInfo != null)
            {
                SysVipCardTypeEntity sysVipCardTypeEntity = sysVipCardTypeBll.GetByID(vipCardInfo.VipCardTypeID);
                if (sysVipCardTypeEntity != null)
                {
                    List<SysVipCardTypeEntity> sysVipCardTypeList = sysVipCardTypeAllList.AsEnumerable().Where(t => t.VipCardLevel > sysVipCardTypeEntity.VipCardLevel).ToList();
                    if (sysVipCardTypeList != null)
                    {
                        foreach (var item in sysVipCardTypeList)
                        {
                            VipCardTypeInfo vipCardTypeInfo = new VipCardTypeInfo();
                            vipCardTypeInfo.VipCardTypeId = item.VipCardTypeID.ToString();
                            vipCardTypeInfo.VipCardTypeName = item.VipCardTypeName.ToString();

                            rd.VipCardTypeIdList.Add(vipCardTypeInfo);
                        }
                    }
                }
            }
            return rd;
        }
    }
}