using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.CPOS.DTO.Base;
using Microsoft.SqlServer.Types;
using Newtonsoft.Json;

namespace JIT.CPOS.DTO.Module.UnitAndItem.Unit.Response
{
    public class GetDisplaynoneRD : IAPIResponseData
    {
        /// <summary>
        /// 是否显示全部
        /// </summary>
        public bool IsAllAccessoriesStores { get; set; }   //是否显示全部

        /// <summary>
        /// 是否显示搜索栏
        /// </summary>
        public bool IsSearchAccessoriesStores { get; set; } //是否显示搜索栏

        public string ForwardingMessageLogo { get; set; }
        public string ForwardingMessageTitle { get; set; }
        public string ForwardingMessageSummary { get; set; }
    }
}
