using JIT.CPOS.DTO.Base;

namespace JIT.CPOS.DTO.Module.VIP.MobileModule.Response
{
    public class GetFormsRD : IAPIResponseData
    {
        /// <summary>
        /// 数据
        /// </summary>
        public MobileModule[] Items { get; set; }

        /// <summary>
        /// 总记录数
        /// </summary>
        public int TotalRow { get; set; }
    }

    /// <summary>
    /// 表单列表字段
    /// </summary>
    public class MobileModule
    {
        /// <summary>
        /// ID
        /// </summary>
        public string MobileModuleID { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string ModuleName { get; set; }

        /// <summary>
        /// 是否为模块 1是0否
        /// </summary>
        public int IsTemplate { get; set; }

        /// <summary>
        /// 使用次数
        /// </summary>
        public int UsedCount { get; set; }
    }
}
