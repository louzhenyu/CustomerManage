using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.BS.Web.ApplicationInterface.Base;
using JIT.CPOS.BS.Web.Session;
using JIT.CPOS.DTO.Module.CardProduct.MakeVipCard.Request;
using JIT.CPOS.DTO.Module.CardProduct.MakeVipCard.Response;
using JIT.Utility.DataAccess.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JIT.CPOS.BS.Web.ApplicationInterface.Module.CardProduct.MakeVipCard
{
    /// <summary>
    /// 获取制卡列表接口 
    /// </summary>
    public class GetMakeVipCardListAH : BaseActionHandler<GetMakeVipCardListRP, GetMakeVipCardListRD>
    {
        protected override GetMakeVipCardListRD ProcessRequest(DTO.Base.APIRequest<GetMakeVipCardListRP> pRequest)
        {
            var rd = new GetMakeVipCardListRD();
            var para = pRequest.Parameters;
            var loggingSessionInfo = new SessionManager().CurrentUserLoginInfo;
            var VipCardBatchBLL = new VipCardBatchBLL(loggingSessionInfo);
            var SysVipCardTypeBLL = new SysVipCardTypeBLL(loggingSessionInfo);
            //查询参数
            List<IWhereCondition> complexCondition = new List<IWhereCondition> { };
            if (!string.IsNullOrEmpty(para.VipCardTypeCode))
                complexCondition.Add(new EqualsCondition() { FieldName = "VipCardTypeCode", Value = para.VipCardTypeCode });
            if (para.StareDate!=null)
                complexCondition.Add(new DirectCondition("a.BeginDate>='" + para.StareDate + "' "));
            if (para.EndDate!=null)
                complexCondition.Add(new DirectCondition("a.EndDate<='" + para.EndDate + "' "));

            //获取数据集
            var tempList = VipCardBatchBLL.PagedQuery(complexCondition.ToArray(), null, para.PageSize, para.PageIndex);
            rd.TotalPageCount = tempList.PageCount;
            rd.TotalCount = tempList.RowCount;

            rd.VipCardBatchInfoList = tempList.Entities.Select(t => new VipCardBatchInfo()
            {
                BatchNo = t.BatchNo == null ? 0 : t.BatchNo.Value,
                CardMedium = t.CardMedium == null ? "" : t.CardMedium,
                VipCardTypeCode = t.VipCardTypeCode == null ? "" : t.VipCardTypeCode,
                CardPrefix = t.CardPrefix == null ? "" : t.CardPrefix,
                StartCardNo = t.StartCardNo == null ? "" : t.StartCardNo,
                EndCardNo = t.EndCardNo == null ? "" : t.EndCardNo,
                CreateTime = t.CreateTime == null ? "" : t.CreateTime.Value.ToString("yyyy-MM-dd"),
                Qty = t.Qty == null ? 0 : t.Qty.Value,
                OutliersQty = t.OutliersQty == null ? 0 : t.OutliersQty.Value,
                ImportQty = t.ImportQty == null ? 0 : t.ImportQty.Value
            }).ToList();

            if (rd.VipCardBatchInfoList.Count>0)
            {
                foreach (var item in rd.VipCardBatchInfoList)
                {
                    SysVipCardTypeEntity TypeDate = SysVipCardTypeBLL.QueryByEntity(new SysVipCardTypeEntity() { VipCardTypeCode = item.VipCardTypeCode }, null).FirstOrDefault();
                    item.VipCardTypeName = TypeDate == null ? "" : TypeDate.VipCardTypeName;
                }
            }

            return rd;
        }
    }
}