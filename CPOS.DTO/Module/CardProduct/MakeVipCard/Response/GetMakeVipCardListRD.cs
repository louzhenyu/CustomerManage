using JIT.CPOS.DTO.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.DTO.Module.CardProduct.MakeVipCard.Response
{
    /// <summary>
    /// 获取制卡列表响应对象
    /// </summary>
    public class GetMakeVipCardListRD : IAPIResponseData
    {
        /// <summary>
        /// 总叶数
        /// </summary>
        public int TotalPageCount { get; set; }
        /// <summary>
        /// 总记录数
        /// </summary>
        public int TotalCount { get; set; }

        /// <summary>
        /// 制卡列表信息集合
        /// </summary>
        public List<VipCardBatchInfo> VipCardBatchInfoList { get; set; }
    }

    public class VipCardBatchInfo
    {
        /// <summary>
        /// 批次
        /// </summary>
        public int BatchNo { get; set; }

        /// <summary>
        /// 卡介质
        /// </summary>
        public string CardMedium { get; set; }
        /// <summary>
        /// 卡类型编号
        /// </summary>
        public string VipCardTypeCode { get; set; }
        /// <summary>
        /// 卡类型名称
        /// </summary>
        public string VipCardTypeName { get; set; }
        /// <summary>
        /// 卡前缀
        /// </summary>
        public string CardPrefix { get; set; }
        /// <summary>
        /// 开始卡号
        /// </summary>
        public string StartCardNo { get; set; }
        /// <summary>
        /// 结束卡号
        /// </summary>
        public string EndCardNo { get; set; }
        /// <summary>
        /// 制卡日期
        /// </summary>
        public string CreateTime { get; set; }
        /// <summary>
        /// 总数
        /// </summary>
        public int Qty { get; set; }
        /// <summary>
        /// 异常加载
        /// </summary>
        public int OutliersQty { get; set; }
        /// <summary>
        /// 导入数量
        /// </summary>
        public int ImportQty { get; set; }
    }
}
