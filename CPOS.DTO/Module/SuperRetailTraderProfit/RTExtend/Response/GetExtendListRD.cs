using JIT.CPOS.DTO.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.DTO.Module.SuperRetailTraderProfit.RTExtend.Response
{
    public class GetExtendListRD : IAPIResponseData
    {
        public Guid? SetoffEventID { get; set; }
        public List<GetExtendInfo> List { get; set; }
    }
    public class GetExtendInfo
    {
        public string SetoffToolId { get; set; }
        public string ToolType { get; set; }
        public string Name { get; set; }
        public string BeginData { get; set; }
        public string EndData { get; set; }
        public int SurplusCount { get; set; }
        public int ServiceLife { get; set; }
        public string ImageUrl { get; set; }
        public string Text { get; set; }
        public string ObjectId { get; set; }
    }
}
