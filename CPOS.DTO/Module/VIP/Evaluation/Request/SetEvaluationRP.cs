using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.CPOS.DTO.Base;

namespace JIT.CPOS.DTO.Module.VIP.Evaluation.Request
{
    public class SetEvaluationRP : IAPIRequestParameter
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
        public string MemberID { get; set; }
        /// <summary>
        /// 平台，1-Android,2-IOS
        /// </summary>
        public string Platform { get; set; }
        #endregion

        public void Validate()
        {

        }
    }
}
