using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using JIT.CPOS.BS.Web.ApplicationInterface.Base;
using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.DTO.Module.VIP.Attribute.Request;
using JIT.CPOS.DTO.Module.VIP.Attribute.Response;
using JIT.Utility.ExtensionMethod;
using JIT.CPOS.DTO.Base;
using JIT.CPOS.BLL;

namespace JIT.CPOS.BS.Web.ApplicationInterface.Module.VIP.Attribute
{
    public class SetAttributeAH : BaseActionHandler<SetAttributeRP, SetAttributeRD>
    {

        protected override SetAttributeRD ProcessRequest(APIRequest<SetAttributeRP> pRequest)
        {
            SetAttributeRD rd = new SetAttributeRD();
            SetAttributeRP rp = pRequest.Parameters;
            try
            {
                var pBll = new AttributeFormBLL(CurrentUserInfo);
                if (!string.IsNullOrEmpty(rp.AttributeFormID.ToString()))
                {
                    var entity = pBll.GetByID(rp.AttributeFormID);
                    entity.OperationTypeID = 4;
                    entity.Status = 2;
                    entity.LastUpdateBy = CurrentUserInfo.UserID;
                    entity.LastUpdateTime = DateTime.Now;
                    pBll.Update(entity);
                }
                else
                {
                    var entity = new AttributeFormEntity()
                    {
                        CustomerId = CurrentUserInfo.ClientID,
                        CreateBy = CurrentUserInfo.UserID,
                        OperationTypeID = 2,
                        Name = rp.Name,
                        AttributeTypeID = rp.AttributeTypeID,
                        AttributeFormID = rp.AttributeFormID,
                        IsDelete = 0,
                        OptionRemark = rp.OptionRemark,
                        Remark = rp.Remark,
                        Status = 2,
                        Sequence = rp.Sequence,
                        Type = rp.Type
                    };
                    pBll.Create(entity);
                }
                rd.IsSuccess = true;
                rd.Msg = "操作成功";
            }
            catch (Exception ex)
            {
                rd.IsSuccess = false;
                rd.Msg = ex.Message;
            }
            return rd;
        }
    }
}