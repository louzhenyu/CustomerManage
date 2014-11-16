using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.Common
{
    public enum PackageTypeMethod 
    {
        /// <summary>
        /// 商品
        /// </summary>
        ITEMS, 

        /// <summary>
        /// SKU
        /// </summary>
        SKUS,

        /// <summary>
        /// 门店
        /// </summary>
        UNITS,

        /// <summary>
        /// 门店用户权限配置
        /// </summary>
        USERS,

        /// <summary>
        /// 商品分类
        /// </summary>
        ITEMCATES,

        /// <summary>
        /// SKU属性
        /// </summary>
        SKUPROPS,

        /// <summary>
        /// 商品属性
        /// </summary>
        ITEMPROPS,

        /// <summary>
        /// 商品价格
        /// </summary>
        ITEMPRICES,

        /// <summary>
        /// SKU价格
        /// </summary>
        SKUPRICES
    }

    public enum PackageGenTypeMethod
    {
        /// <summary>
        /// 手工
        /// </summary>
        MANUAL,

        /// <summary>
        /// 自动任务
        /// </summary>
        AUTO_TASK,

        /// <summary>
        /// 客户端请求
        /// </summary>
        CLIENT_REQUEST
    }
}
