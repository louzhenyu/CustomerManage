using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using JIT.CPOS.Web.ApplicationInterface.Base;
using JIT.CPOS.DTO.Module.VIP.Attribute.Request;
using JIT.CPOS.DTO.Module.VIP.Attribute.Response;
using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Entity;
using JIT.Utility.ExtensionMethod;
using JIT.CPOS.DTO.Base;
using JIT.CPOS.BLL;

namespace JIT.CPOS.Web.ApplicationInterface.Module.VIP.Attribute
{
    public class GetAttributeListAH : BaseActionHandler<GetAttributeListRP, GetAttributeListRD>
    {
        protected override GetAttributeListRD ProcessRequest(APIRequest<GetAttributeListRP> pRequest)
        {
            GetAttributeListRD rd = new GetAttributeListRD();
            var pBll = new AttributeFormBLL(CurrentUserInfo);
            int pPageCount = 0;
            var pList = pBll.GetAttributeFormList(pRequest.Parameters.Name,
               pRequest.Parameters.OperationTypeID, pRequest.Parameters.AttributeTypeID, pRequest.Parameters.Status, pRequest.Parameters.PageIndex, pRequest.Parameters.PageSize, out pPageCount);
            var bll = new ObjectEvaluationBLL(CurrentUserInfo);
            if (pList.Length == 0)
                return rd;
            rd.Count = pPageCount;
            rd.AttributeList = pList.Select(t => new AttributeFormInfo
            {
                AttributeFormID = t.AttributeFormID,
                Name = t.Name,
                Type = t.Type,
                Sequence=t.Sequence,
                ClientBussinessDefinedID=t.ClientBussinessDefinedID,
                OptionRemark = t.OptionRemark,
                Remark = t.Remark,
                OperationTypeID = t.OperationTypeID,
                Status = t.Status,
                AttributeTypeID = t.AttributeTypeID
            }).ToArray();
            return rd;
        }
    }
}