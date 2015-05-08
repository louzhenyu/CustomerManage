using JIT.CPOS.DTO.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.DTO.Module.VIP.Evaluation.Request
{
    public class SetEvaluationItemRP : IAPIRequestParameter
    {
        #region 属性
        /// <summary>
        /// 商品或者门店ID/订单ID
        /// </summary>
        public string OrderID { get; set; }
        /// <summary>
        /// 1-商品，2-门店，3-订单
        /// </summary>
        public int? Type { get; set; }
        /// <summary>
        /// 评价内容
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// 星级
        /// </summary>
        public int? StarLevel { get; set; }
        /// <summary>
        /// 星级1
        /// </summary>
        public int? StarLevel1 { get; set; }
        /// <summary>
        /// 星级2
        /// </summary>
        public int? StarLevel2 { get; set; }
        /// <summary>
        /// 星级3
        /// </summary>
        public int? StarLevel3 { get; set; }
        /// <summary>
        /// 星级4
        /// </summary>
        public int? StarLevel4 { get; set; }
        /// <summary>
        /// 星级5
        /// </summary>
        public int? StarLevel5 { get; set; }
        /// <summary>
        /// 会员ID
        /// </summary>
        public string VipID { get; set; }
        /// <summary>
        /// 平台，1-Android,2-IOS
        /// </summary>
        public string Platform { get; set; }
        /// <summary>
        /// 是否匿名
        /// </summary>
        public int IsAnonymity { get; set; }
        public ItemEvaluationInfo[] ItemEvaluationInfo { get; set; }
        #endregion

        public void Validate()
        {

        }

    }
    public class ItemEvaluationInfo
    {
        /// <summary>
        /// 商品ID/门店ID/订单ID
        /// </summary>
        public string ObjectID { get; set; }
        /// <summary>
        /// 评论等级1=好评；2=中评；3=差评
        /// </summary>
        public int StarLevel { get; set; }
        /// <summary>
        /// 评论内容
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// 备注：可保存会员购买商品的sku属性，如 颜色：红色/尺寸：M
        /// </summary>
        public string Remark { get; set; }
    }
}
