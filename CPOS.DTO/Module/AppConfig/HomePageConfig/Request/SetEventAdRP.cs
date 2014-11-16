using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.CPOS.DTO.Base;

namespace JIT.CPOS.DTO.Module.AppConfig.HomePageConfig.Request
{
    public class SetEventAdRP : IAPIRequestParameter
    {
        #region IAPIRequestParameter 成员

        public void Validate()
        {
        }
        public int OperateType { get; set; }
        public AdAreaInfoPara[] AdAreaInfoPara { get; set; }
        #endregion
    }

    public class AdAreaInfoPara
    {
        public Guid? AdAreaId { get; set; }
        public Guid? HomeID { get; set; }
        public string ImageUrl { get; set; }
        public string ObjectID { get; set; }
        public int? ObjectTypeID { get; set; }
        public int? DisplayIndex { get; set; }
        public string Url { get; set; }
    }
}
