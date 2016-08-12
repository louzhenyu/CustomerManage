using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.DTO.Base;
using JIT.CPOS.DTO.Module.VIP.VipGold.Request;
using JIT.CPOS.DTO.Module.VIP.VipGold.Response;
using JIT.CPOS.Web.ApplicationInterface.Base;
using JIT.Utility.DataAccess.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JIT.CPOS.Web.ApplicationInterface.Module.VIP.VipGolden
{
    /// <summary>
    /// 获取卡等级相关信息
    /// </summary>
    public class GetVipCardTypeAH : BaseActionHandler<GetVipCardTypeRP, GetVipCardTypeRD>
    {
        protected override GetVipCardTypeRD ProcessRequest(APIRequest<GetVipCardTypeRP> pRequest)
        {
            var rd = new GetVipCardTypeRD();
            var para = pRequest.Parameters;
            LoggingSessionInfo loggingSessionInfo = Default.GetBSLoggingSession(pRequest.CustomerID, pRequest.UserID);
            //声明卡等级相关逻辑
            var bllVipCardType = new SysVipCardTypeBLL(loggingSessionInfo);
            var bllVip=new VipBLL(loggingSessionInfo);
            string strPhone = string.Empty;
            int? CurVipLevel = 0;
            List<IWhereCondition> wheres = new List<IWhereCondition>();
            wheres.Add(new EqualsCondition() { FieldName = "clientid", Value = pRequest.CustomerID });
            wheres.Add(new EqualsCondition() { FieldName = "VipID", Value = pRequest.UserID });
            wheres.Add(new EqualsCondition() { FieldName = "Status", Value = 2 });
            //wheres.Add(new DirectCondition("Phone!=''"));
            var vipInfo = bllVip.Query(wheres.ToArray(), null).FirstOrDefault();
            if (vipInfo != null)
            {
                strPhone = vipInfo.Phone;
                var vipCardVipMappingBLL = new VipCardVipMappingBLL(CurrentUserInfo);
                var vipCardBLL = new VipCardBLL(CurrentUserInfo);
                var vipCardTypeBLL = new SysVipCardTypeBLL(CurrentUserInfo);                
                var vipCardMappingInfo = vipCardVipMappingBLL.QueryByEntity(new VipCardVipMappingEntity() { VIPID = pRequest.UserID, CustomerID = CurrentUserInfo.ClientID },
                    new OrderBy[] { new OrderBy() { FieldName = "CreateTime", Direction = OrderByDirections.Desc } }).FirstOrDefault();
                if (vipCardMappingInfo != null)//根据当前会员与卡关联表获取当前会员等级
                {
                    var vipCardInfo = vipCardBLL.QueryByEntity(new VipCardEntity() { VipCardID = vipCardMappingInfo.VipCardID, VipCardStatusId = 1 }, null).FirstOrDefault();
                    if (vipCardInfo != null)
                    {
                        var vipCardTypeInfo = vipCardTypeBLL.QueryByEntity(new SysVipCardTypeEntity() { VipCardTypeID = vipCardInfo.VipCardTypeID }, null).FirstOrDefault();
                        if (vipCardTypeInfo != null)//获取当前会员等级
                        {                            
                            CurVipLevel = vipCardTypeInfo.VipCardLevel;
                            if (CurVipLevel <= 1)
                            {
                                CurVipLevel = 1;
                            }                        
                        }
                    }
                }
            }
            else
            {
                strPhone = para.Phone;
            }
            try
            {
                //获取当前会员可绑卡列表
                var VipCardTypeSysInfoList = bllVipCardType.GetBindVipCardTypeInfo(loggingSessionInfo.ClientID, strPhone, pRequest.UserID, CurVipLevel);
                if (VipCardTypeSysInfoList != null && VipCardTypeSysInfoList.Tables[0].Rows.Count > 0)
                {
                    rd.VipCardTypeList = DataTableToObject.ConvertToList<VipCardRelateInfo>(VipCardTypeSysInfoList.Tables[0]);
                }
                else
                {
                    throw new APIException("未检测到实体卡！") { ErrorCode = ERROR_CODES.INVALID_BUSINESS };
                }
            }
            catch (APIException ex)
            {
                throw ex;
            }
            
            return rd;
        }
    }
}