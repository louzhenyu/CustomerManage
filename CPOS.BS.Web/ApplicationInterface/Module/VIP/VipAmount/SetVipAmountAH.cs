using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.BS.Web.ApplicationInterface.Base;
using JIT.CPOS.BS.Web.Session;
using JIT.CPOS.DTO.Base;
using JIT.CPOS.DTO.Module.VIP.VipAmount.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JIT.CPOS.BS.Web.ApplicationInterface.Module.VIP.VipAmount
{
    public class SetVipAmountAH : BaseActionHandler<SetVipAmountRP, EmptyResponseData>
    {
        protected override EmptyResponseData ProcessRequest(APIRequest<SetVipAmountRP> pRequest)
        {
            var rd = new EmptyResponseData();
            var para = pRequest.Parameters;
            var loggingSessionInfo = new SessionManager().CurrentUserLoginInfo; //登录状态信息
            var vipAmountBLL = new VipAmountBLL(loggingSessionInfo);
            var vipAmountDetailBLL = new VipAmountDetailBLL(loggingSessionInfo);
            var objectImagesBLL = new ObjectImagesBLL(loggingSessionInfo);       //图片表业务对象实例化
            var unitBLL = new t_unitBLL(loggingSessionInfo);

            var vipBLL = new VipBLL(loggingSessionInfo);

            var vipInfo = vipBLL.GetByID(para.VipID);
            var unitInfo = unitBLL.GetByID(loggingSessionInfo.CurrentUserRole.UnitId);
            var pTran = vipAmountBLL.GetTran();   //事务  

            var vipAmountEntity = vipAmountBLL.QueryByEntity(new VipAmountEntity() { VipId = vipInfo.VIPID, VipCardCode = vipInfo.VipCode }, null).FirstOrDefault();

            using (pTran.Connection)
            {
                try
                {
                    var amountDetail = new VipAmountDetailEntity()
                    {
                        Amount = para.Amount,
                        AmountSourceId = para.AmountSourceID,
                        ObjectId = "",
                        Reason = para.Reason,
                        Remark = para.Remark
                    };
                    string vipAmountDetailId = string.Empty;
                    if (para.AmountSourceID == "23")        //人工调整余额
                        vipAmountDetailId = vipAmountBLL.AddVipAmount(vipInfo, unitInfo, vipAmountEntity, amountDetail, pTran, loggingSessionInfo);
                    else if (para.AmountSourceID == "24")   //人工调整返现
                        vipAmountDetailId = vipAmountBLL.AddReturnAmount(vipInfo, unitInfo, vipAmountEntity, amountDetail, pTran, loggingSessionInfo);

                    //增加图片上传
                    if (!string.IsNullOrEmpty(para.ImageUrl))
                    {
                        var objectImagesEntity = new ObjectImagesEntity()
                        {
                            ImageId = Guid.NewGuid().ToString(),
                            ObjectId = vipAmountDetailId,
                            ImageURL = para.ImageUrl
                        };
                        objectImagesBLL.Create(objectImagesEntity);
                    }
                    pTran.Commit();  //提交事物
                }
                catch (APIException apiEx)
                {
                    pTran.Rollback();//回滚事物
                    throw new APIException(apiEx.ErrorCode, apiEx.Message);
                }
                catch (Exception ex)
                {
                    pTran.Rollback();//回滚事物
                    throw new Exception(ex.Message);
                }
            }
            return rd;
        }
    }
}