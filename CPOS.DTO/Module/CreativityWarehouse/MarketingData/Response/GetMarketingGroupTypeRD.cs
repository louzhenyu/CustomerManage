using JIT.CPOS.DTO.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.DTO.Module.CreativityWarehouse.MarketingData.Response
{
    public class GetMarketingGroupTypeRD : IAPIResponseData
    {
        public List<SysMarketingGroupTypeInfo> MarketingGroupTypeList { get; set; }
    }
    /// <summary>
    /// Banner信息
    /// </summary>
    public class SysMarketingGroupTypeInfo
    {
        #region 属性集
        /// <summary>
        /// 
        /// </summary>
        public Guid? ActivityGroupId { get; set; }

                /// <summary>
        /// 
        /// </summary>
        public string ActivityGroupCode { get; set; }

        

        /// <summary>
        /// 
        /// </summary>
        public String Name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String Remark { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? CreateTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String CreateBy { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? LastUpdateTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String LastUpdateBy { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Int32? IsDelete { get; set; }

        public Int32 WaitPublishEvent { get; set; }
        public Int32 RuningEvent { get; set; }
        public Int32 EndEvent { get; set; }

        #endregion
    }
}
