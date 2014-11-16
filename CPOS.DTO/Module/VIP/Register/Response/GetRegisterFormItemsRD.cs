using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.CPOS.DTO.Base;

namespace JIT.CPOS.DTO.Module.VIP.Register.Response
{
    public class GetRegisterFormItemsRD : IAPIResponseData
    {
        public PageInfo[] Pages { get; set; }        
    }

    public class PageInfo
    {
        public string ID { get; set; }
        public int ValidFlag { get; set; }        
        public int? DisplayIndex { get; set; }
        public BlockInfo[] Blocks { get; set; }
    }

    public class BlockInfo
    {
        public string ID { get; set; }
        public string PageID { get; set; }
        public int? DisplayIndex { get; set; }
        public PropertyDefineInfo[] PropertyDefineInfos { get; set; }
    }
    public class PropertyDefineInfo
    {
        public string ID { get; set; }
        public string BlockID { get; set; }
        public string Title { get; set; }
        public int? DisplayIndex { get; set; }
        public ControlInfo ControlInfo { get; set; }
    }
    public class ControlInfo
    {
        public int? ControlType { get; set; }
        public string ColumnDesc { get; set; }
        public string ColumnName { get; set; }
        public object DefaultValue { get; set; }
        public int? DisplayType { get; set; }
        public string ColumnDescEn { get; set; }
        public string IsMustDo { get; set; }
        public KeyValueInfo[] OptionValues { get; set; }

    }
    public class KeyValueInfo
    {
        public string Key { get; set; }
        public string Value { get; set; }
    }
}
