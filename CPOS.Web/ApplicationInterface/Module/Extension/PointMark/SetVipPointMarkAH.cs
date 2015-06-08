using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.DTO.Base;
using JIT.CPOS.DTO.Module.Extension.PointMark.Request;
using JIT.CPOS.Web.ApplicationInterface.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JIT.CPOS.Web.ApplicationInterface.Module.Extension.PointMark
{
    public class SetVipPointMarkAH : BaseActionHandler<SetVipPointMarkRP, EmptyResponseData>
    {

        protected override EmptyResponseData ProcessRequest(DTO.Base.APIRequest<SetVipPointMarkRP> pRequest)
        {
            var rd = new EmptyResponseData();
            var para = pRequest.Parameters;
            var pointMarkBLL = new X_VipPointMarkBLL(CurrentUserInfo);
            var pointMarkDetailBLL = new X_VipPointMarkDetailBLL(CurrentUserInfo);

            X_VipPointMarkEntity pointMarkEntity = null;
            pointMarkEntity = pointMarkBLL.QueryByEntity(new X_VipPointMarkEntity() { VipID = CurrentUserInfo.UserID }, null).FirstOrDefault();
            if (pointMarkEntity != null)
            {
                if (para.Count > 0)
                    pointMarkEntity.TotalCount += para.Count;
                pointMarkEntity.Count += para.Count;
                pointMarkBLL.Update(pointMarkEntity);
            }
            else
            {
                pointMarkEntity = new X_VipPointMarkEntity();
                pointMarkEntity.VipID = CurrentUserInfo.UserID;
                pointMarkEntity.CustomerID = CurrentUserInfo.ClientID;
                pointMarkEntity.Count = para.Count;
                pointMarkEntity.TotalCount = para.Count;
                pointMarkBLL.Create(pointMarkEntity);
            }

            X_VipPointMarkDetailEntity detail = new X_VipPointMarkDetailEntity()
            {
                VipID = CurrentUserInfo.UserID,
                Count = para.Count,
                Source = para.Source,
                CustomerID = CurrentUserInfo.ClientID
            };
            pointMarkDetailBLL.Create(detail);
            return rd;
        }
    }
}