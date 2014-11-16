using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using JIT.CPOS.DTO.Module.VIP.Register.Response;
using JIT.CPOS.DTO.Module.VIP.Register.Request;
using JIT.CPOS.Web.ApplicationInterface.Base;
using System.Data;
using JIT.CPOS.BS.BLL;

namespace JIT.CPOS.Web.ApplicationInterface.Module.VIP.Register
{
    public class GetRegisterFormItemsAH : BaseActionHandler<GetRegisterFormItemsRP, GetRegisterFormItemsRD>
    {
        protected override GetRegisterFormItemsRD ProcessRequest(DTO.Base.APIRequest<GetRegisterFormItemsRP> pRequest)
        {
            GetRegisterFormItemsRD rd = new GetRegisterFormItemsRD();

            string eventCode = pRequest.Parameters.EventCode;
            string customerId = this.CurrentUserInfo.ClientID;
            MobileBussinessDefinedBLL bll = new MobileBussinessDefinedBLL(this.CurrentUserInfo);
            var ds = bll.GetPagesInfo(eventCode, customerId);

            var temp = ds.Tables[0].AsEnumerable().OrderBy(t => t["DisplayIndex"]).Select(t => new PageInfo()
            {
                ID = t["ID"].ToString(),
                Blocks = ds.Tables[1].AsEnumerable().OrderBy(b => b["DisplayIndex"]).Where(b => b["PageId"].ToString() == t["ID"].ToString()).Select(b => new BlockInfo
                {
                    ID = b["ID"].ToString(),
                    PageID = b["PageId"].ToString(),
                    DisplayIndex = b["DisplayIndex"] as int?,// == null ? default(int?) : (int?)b["DisplayIndex"],
                    PropertyDefineInfos = ds.Tables[2].AsEnumerable().OrderBy(c => c["DisplayIndex"]).Where(c => c["BlockId"].ToString() == b["ID"].ToString()).Select(c => new PropertyDefineInfo
                    {
                        ID = c["MobileBussinessDefinedID"].ToString(),
                        BlockID = c["BlockId"].ToString(),
                        Title = c["Title"].ToString(),
                        DisplayIndex = c["DisplayIndex"] as int?,//== null ? default(int?) : (int?)c["DisplayIndex"],
                         
                        ControlInfo = ds.Tables[3].AsEnumerable().Where(d => d["MobileBussinessDefinedID"].ToString() == c["MobileBussinessDefinedID"].ToString()).Select(d=> new ControlInfo
                        {
                            ControlType = d["ControlType"] as int?,// == null ? default(int?) : (int?)d["ControlType"],
                            ColumnDesc = d["ColumnDesc"].ToString(),
                            DisplayType = d["DisplayType"] as int?,// == null ? default(int?) : (int?)(d["DisplayType"]),
                            ColumnDescEn = d["ColumnDescEn"].ToString(),
                            IsMustDo = d["IsMustDo"].ToString(),
                            OptionValues = ds.Tables[4].AsEnumerable().Where(e => e["MobileBussinessDefinedID"].ToString() == d["MobileBussinessDefinedID"].ToString()).OrderBy(e => e["OptionValue"]).Select(e => new KeyValueInfo
                            {
                                 Key = e["OptionValue"].ToString(),
                                 Value = e["OptionText"].ToString()
                            }).ToArray(),
                        }).First(),

                    }).ToArray(),
                }).ToArray(),
                DisplayIndex = Convert.ToInt32(t["DisplayIndex"])
            });
            rd.Pages = temp.OrderBy(t=>t.DisplayIndex).ToArray();

            return rd;
        }
    }
}