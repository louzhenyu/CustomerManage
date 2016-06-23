using JIT.CPOS.DTO.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.DTO.Module.SuperRetailTraderProfit.RTExtend.Request
{
    public class SetExtendRP : IAPIRequestParameter
    {
        public List<CommonInfo> CommonList { get; set; }
        public List<PosterInfo> PosterList { get; set; }

        public void Validate()
        {
            //throw new NotImplementedException();
        }
    }
    public class CommonInfo {
        //CTW=创意仓库; Coupon=优惠券; MaterialText图文素材
        public string ToolType { get; set; }
        public string ObjectId { get; set; }
    }
    public class PosterInfo
    {
        public string Name { get; set; }
        public string ImageUrl { get; set; }
    }
}
