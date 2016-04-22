using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.CPOS.DTO.Base;

namespace JIT.CPOS.DTO.Module.VIP.Evaluation.Response
{
    public class GetEvaluationListRD : IAPIResponseData
    {
        /// <summary>
        /// 评论总数
        /// </summary>
        public int Count { get; set; }
        public int TotalPageCount { get; set; }
        /// <summary>
        /// 好评率
        /// </summary>
        public string GoodPer{get;set;}
        /// <summary>
        /// 评论信息列表
        /// </summary>
        public EvaluationInfo[] EvaluationList { get; set; }
    }

    public class EvaluationInfo
    {
        /// <summary>
        /// 标识
        /// </summary>
        public string EvaluationID { get; set; }
        /// <summary>
        /// 评价内容
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// 星级
        /// </summary>
        public int? StarLevel { get; set; }
        /// <summary>
        /// 会员ID
        /// </summary>
        public string VipID { get; set; }
        /// <summary>
        /// 会员名称
        /// </summary>
        public string VipName { get; set; }
        /// <summary>
        /// 评论时间
        /// </summary>
        public string CreateTime { get; set; }
        /// <summary>
        /// 是否匿名 1=不匿名；2=匿名
        /// </summary>
        public int? IsAnonymity { get; set; }
        /// <summary>
        /// 备注 可保存会员购买商品的sku属性，如 颜色：红色/尺寸：M
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 头像信息
        /// </summary>
        public string HeadImgUrl { get; set; }

    }
}
