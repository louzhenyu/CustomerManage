using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JIT.CPOS.Web.ApplicationInterface.Project.HuaAn.Receive
{
    /// <summary>
    /// 请求支付消息类定义
    /// </summary>
    public class ReceivePayMessage
    {
        /// <summary>
        /// 商家(ID)
        /// </summary>
        public string MerchantID { get; set; }

        /// <summary>
        /// 商户日期
        /// </summary>
        public string Merchantdate { get; set; }

        /// <summary>
        /// 订单号
        /// </summary>
        public string OrderNO { get; set; }

        /// <summary>
        /// 支付金额（单位：圆）
        /// </summary>
        public string Totalpay { get; set; }

        /// <summary>
        /// 客户协议号
        /// </summary>
        public string Assignbuyer { get; set; }

        /// <summary>
        /// 客户姓名
        /// </summary>
        public string Assbuyername { get; set; }

        /// <summary>
        /// 客户手机号
        /// </summary>
        public string Assbuyermobile { get; set; }

        /// <summary>
        /// 总折扣
        /// </summary>
        public string Totaldiscount { get; set; }

        /// <summary>
        /// 总抵扣
        /// </summary>
        public string Totaldeduction { get; set; }

        /// <summary>
        /// 应付金额(此处与支付金额一致)
        /// </summary>
        public string Actualtotal { get; set; }

        /// <summary>
        /// 手续费类型(此处暂为0)
        /// </summary>
        public string Feetype { get; set; }

        /// <summary>
        /// 手续费(此处暂为0)
        /// </summary>
        public string Fee { get; set; }

        /// <summary>
        /// 交易号(对应资产)
        /// </summary>
        public string Logisticsinfo { get; set; }

        /// <summary>
        /// 公共回传字段
        /// </summary>
        public string Commonreturn { get; set; }

        /// <summary>
        /// 是否直接赎回
        /// </summary>
        public string ISDirectRedeem { get; set; }

        /// <summary>
        /// 返回地址
        /// </summary>
        public string RetURL { get; set; }

        /// <summary>
        /// 回调地址
        /// </summary>
        public string PageURL { get; set; }

        /// <summary>
        /// 定单描述
        /// </summary>
        public string Memo { get; set; }

        /// <summary>
        /// 房产相关
        /// </summary>
        public Goodsinfo[] Goodsinfos { get; set; }
    }

    /// <summary>
    /// 楼盘相关
    /// </summary>
    public class Goodsinfo
    {
        /// <summary>
        /// 商品信息id
        /// </summary>
        public string id { get; set; }

        /// <summary>
        /// 楼盘名称
        /// </summary>
        public string Goodsname { get; set; }

        /// <summary>
        /// 楼盘展示URL
        /// </summary>
        public string GoodsURL { get; set; }

        /// <summary>
        /// 楼盘图片地址
        /// </summary>
        public string Goodspicture { get; set; }

        /// <summary>
        /// 楼盘ID
        /// </summary>
        public string Goodsmodle { get; set; }

        /// <summary>
        /// 楼盘描述
        /// </summary>
        public string Goodsdesc { get; set; }

        /// <summary>
        /// 楼盘计量单位
        /// </summary>
        public string Goodsunit { get; set; }

        /// <summary>
        /// 楼盘单价
        /// </summary>
        public string Goodsprice { get; set; }

        /// <summary>
        /// 楼盘数量
        /// </summary>
        public string Goodsquantity { get; set; }

        /// <summary>
        /// 楼盘折扣
        /// </summary>
        public string Goodsdiscount { get; set; }

        /// <summary>
        /// 楼盘抵扣
        /// </summary>
        public string Goodsdeduction { get; set; }

        /// <summary>
        /// 楼盘总价
        /// </summary>
        public string Goodstotalpay { get; set; }

        /// <summary>
        /// 楼盘应付
        /// </summary>
        public string Goodsactualtotal { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string memo { get; set; }
    }
}