using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.CPOS.DTO.Base;

namespace JIT.CPOS.DTO.Module.VIP.Evaluation.Request
{
    public class GetEvaluationListRP : IAPIRequestParameter
    {
        #region 属性
        /// <summary>
        /// 商品或者门店ID
        /// </summary>
        public string ObjectID { get; set; }
        /// <summary>
        /// 1-商品，2-门店
        /// </summary>
        public int? Type { get; set; }
        /// <summary>
        /// 页大小，默认15
        /// </summary>
        public int PageSize { get; set; }
        /// <summary>
        /// 页码，默认以0开始
        /// </summary>
        public int PageIndex { get; set; }
        /// <summary>
        /// 评论等级 1=好评；2=中评；3=差评
        /// </summary>
        public int StarLevel { get; set; }

        #endregion

        public void Validate()
        {

        }
    }
}
