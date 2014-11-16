using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.Web.ApplicationInterface.Base;
using JIT.CPOS.DTO.Module.VIP.MobileModule.Response;
using JIT.CPOS.DTO.Module.VIP.MobileModule.Request;

namespace JIT.CPOS.Web.ApplicationInterface.Module.VIP.MobileModule
{
    public class FormSaveAH : BaseActionHandler<FormSaveRP, FormSaveRD>
    {

        protected override FormSaveRD ProcessRequest(DTO.Base.APIRequest<FormSaveRP> pRequest)
        {
            var items = pRequest.Parameters.Items ?? new MobileBunessDefinedSubInfo[0];
            var mobileModuleID = Guid.Parse(pRequest.Parameters.MobileModuleID);
            var bll = new MobileBussinessDefinedBLL(CurrentUserInfo);
           
            var entities = bll.QueryByEntity( 
                new MobileBussinessDefinedEntity
                {
                    MobileModuleID =  mobileModuleID
                }, 
                null);
            
            
            //1.add 
            var addEntities = items.Where(it => string.IsNullOrEmpty(it.MobileBunessDefinedID))
                .Select( it=>
                new MobileBussinessDefinedEntity
                {
                    MobileBussinessDefinedID = Guid.NewGuid(),
                    MobileModuleID = mobileModuleID,
                    ColumnName = it.ColumnName,
                    ColumnDesc = it.ColumnDesc,
                    ControlType = it.ControlType,
                    CorrelationValue = it.CorrelationValue,
                    CustomerID = CurrentUserInfo.ClientID
                }
                ).ToArray();
            //2.update delete
            var updateEntities = new List<MobileBussinessDefinedEntity>();
            var deleteEntities = new List<MobileBussinessDefinedEntity>();
            foreach (var e in entities)
            {
                var item =
                    items.FirstOrDefault(
                        it => it.MobileBunessDefinedID.ToLower() == e.MobileBussinessDefinedID.ToString().ToLower());

                if (item != null)
                {
                    e.ColumnName = item.ColumnName;
                    e.ColumnDesc = item.ColumnDesc;
                    e.ControlType = item.ControlType;
                    e.CorrelationValue = item.CorrelationValue;
                    updateEntities.Add(e);
                }
                else
                {
                    deleteEntities.Add(e);
                }
            }
            bll.EditMobileBussinessDefined(addEntities, updateEntities.ToArray(),deleteEntities.ToArray());
            return new FormSaveRD
            {
                IsSuccess = true
            };
        }
    }
}