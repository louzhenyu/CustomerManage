using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.CPOS.DTO.Base;
using JIT.CPOS.BS.Entity;

namespace JIT.CPOS.DTO.Module.Event.Bargain.Response
{
    public class GetBargainListRD : IAPIResponseData
    {

        public int TotalCount { get; set; }
        public int TotalPageCount { get; set; }
        public List<BargainInfo> BargainList { get; set; }
    }
    public class BargainInfo
    {
        public string EventId { get; set; }
        public string EventName { get; set; }
        /// <summary>
        /// 商品数量
        /// </summary>
        public int ItemCount { get; set; }
        /// <summary>
        /// 发起人数
        /// </summary>
        public int PromotePersonCount { get; set; }
        /// <summary>
        /// 参与人数
        /// </summary>
        public int BargainPersonCount { get; set; }

        public string BeginTime { get; set; }
        public string EndTime { get; set; }
        //活动状态(1：未开始，2：进行中，3: 已结束,10:提前结束,20:启用)
        public int Status { get; set; }
    }
}
