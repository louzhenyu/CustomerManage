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
        /// 对象（商品、门店、订单或员工）ID
        /// </summary>
        public string ObjectID { get; set; }
        /// <summary>
        /// 订单ID（涉及到订单时使用）
        /// </summary>
        public string OrderID { get; set; }
        /// <summary>
        /// 1=商品，2=门店，3=订单，4=员工
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
        /// 平台，1-Android,2-IOS
        /// </summary>
        public string Platform { get; set; }
        /// <summary>
        /// 是否匿名
        /// </summary>
        public int IsAnonymity { get; set; }
        #endregion

        public void Validate()
        {

        }
    }
}
