using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.DTO.ValueObject
{
    public enum SapObjectType
    {
        /// <summary>
        /// 未定义-默认值
        /// </summary>
        Undefine,
        /// <summary>
        /// sap-sku详情
        /// </summary>
        Items,
        /// <summary>
        /// 物料分类
        /// </summary>
        UDAOITC,
        /// <summary>
        /// 发货区域
        /// </summary>
        UDAOREW,
        /// <summary>
        /// 物料区域
        /// </summary>
        ItemsLocation,
        /// <summary>
        /// 库存
        /// </summary>
        ItemOnHand,
        /// <summary>
        /// 会员
        /// </summary>
        BusinessPartners,
        /// <summary>
        /// 订单
        /// </summary>
        UDSORDR,
        /// <summary>
        /// 订单开票信息
        /// </summary>
        USORDERINVOICE,
        /// <summary>
        /// 订单物流变化信息
        /// </summary>
        UDSORDER,
        /// <summary>
        /// 产品树/多利礼盒
        /// </summary>
        ProductTrees,
        /// <summary>
        /// UDIOOSO
        /// </summary>
        UDIOOSO,

    }
}
