using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using JIT.CPOS.BS.BLL;
using JIT.CPOS.DTO.Module.VIP.MobileModule.Request;
using JIT.CPOS.DTO.Module.VIP.MobileModule.Response;
using JIT.CPOS.BS.Web.ApplicationInterface.Base;
using JIT.Utility.Reflection;

namespace JIT.CPOS.BS.Web.ApplicationInterface.Module.VIP.MobileModule
{
    public class GetClientBunessDefinedAH : BaseActionHandler<GetClientBusinessDefinedRP, GetClientBusinessDefinedRD>
    {
        protected override GetClientBusinessDefinedRD ProcessRequest(DTO.Base.APIRequest<GetClientBusinessDefinedRP> pRequest)
        {
            pRequest.Parameters.Validate();
            int totalRow;
            
            if (pRequest.Parameters.Type == 1 )
            {
                var dt = new MobileModuleBLL(CurrentUserInfo).GetClientBunessDefined(CurrentUserInfo.ClientID,
                    pRequest.Parameters.PageIndex, pRequest.Parameters.PageSize, out totalRow);
                if (dt == null || dt.Rows.Count ==0)
                {
                    return new GetClientBusinessDefinedRD
                    {
                        TotalRow = 0 ,
                        BasicItems = new ClientBunessDefinedSubInfo[0],
                        ExtendItems = new ClientBunessDefinedSubInfo[0],
                        //SeniorItems = new ClientBunessDefinedSubInfo[0]
                    };
                }
                var result = new GetClientBusinessDefinedRD();
                result.TotalRow = totalRow;
                var entities = DataLoader.LoadFrom<ClientBunessDefinedSubInfo>(dt);
                result.BasicItems = entities.Where(it => it.AttributeType == 1).ToArray();
                result.ExtendItems = entities.Where(it => it.AttributeType == 2).ToArray();
                //result.SeniorItems = entity.Where(it => it.AttributeType == 3).ToArray();
                return result;
            }

            if (pRequest.Parameters.Type == 2)
            {
                var basicItems = new List<ClientBunessDefinedSubInfo>();
                basicItems.Add(new ClientBunessDefinedSubInfo
                {
                    TableName = "LEventSignUp",
                    AttributeType = 1,
                    ColumnName = "Name",
                    ColumnDesc = "姓名",
                    ControlType = 1
                });
                basicItems.Add(new ClientBunessDefinedSubInfo
                {
                    TableName = "LEventSignUp",
                    AttributeType = 1,
                    ColumnName = "Phone",
                    ColumnDesc = "电话",
                    ControlType = 1
                });
                basicItems.Add(new ClientBunessDefinedSubInfo
                {
                    TableName = "LEventSignUp",
                    AttributeType = 1,
                    ColumnName = "City",
                    ColumnDesc = "城市",
                    ControlType = 1
                });
                
                var result = new GetClientBusinessDefinedRD();
                result.TotalRow = 3;
                
                result.BasicItems = basicItems.ToArray();

                var dt = new MobileModuleBLL(CurrentUserInfo).GetLeventSignUpAttri(CurrentUserInfo.ClientID,
                    pRequest.Parameters.PageIndex, pRequest.Parameters.PageSize, out totalRow);
                if (dt == null || dt.Rows.Count == 0)
                {
                    result.ExtendItems = new ClientBunessDefinedSubInfo[0];
                }
                else
                {
                    result.TotalRow += totalRow;
                    result.ExtendItems = DataLoader.LoadFrom<ClientBunessDefinedSubInfo>(dt);
                }
                return result;
            }

            return new GetClientBusinessDefinedRD
            {
                TotalRow = 0,
                BasicItems = new ClientBunessDefinedSubInfo[0],
                ExtendItems = new ClientBunessDefinedSubInfo[0]
            };

        }
    }
}