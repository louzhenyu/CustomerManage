using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.CPOS.DTO.Base;

namespace JIT.CPOS.DTO.Module.AppConfig.HomePageConfig.Request
{
    public class SetEventItemRP : IAPIRequestParameter
    {
        #region IAPIRequestParameter 成员

        public void Validate()
        {
        }

        public int OperateType { get; set; }
        public ItemEventAreaInfoPara[] ItemEventAreaInfoPara { get; set; }
        #endregion
    }

    public class ItemEventAreaInfoPara
    {
        public Guid? ItemAreaId { get; set; }
        public string ItemID { get; set; }
        public Guid? HomeID { get; set; }
        public Guid EventID { get; set; }
        public bool? IsUrl { get; set; }
        public int? DisplayIndex { get; set; }
    }

}
