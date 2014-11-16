using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.BS.Entity
{
    /// <summary>
    /// 表单状态
    /// </summary>
    [Serializable]
    public class BillStatusModel
    {
        /// <summary>
        /// 默认
        /// </summary>
        public const string LANGUAGE_OBJECT_KIND_CODE = "Bill.Status";

        /// <summary>
        /// Id
        /// </summary>
        public string Id
        { get; set; }

        /// <summary>
        /// 所属表单的种类的Id
        /// </summary>
        public string KindId
        { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public string Status
        { get; set; }

        /// <summary>
        /// 起始标志(1:该状态为对应表单种类的起始状态)
        /// </summary>
        public int BeginFlag
        { get; set; }

        /// <summary>
        /// 结束标志(1:该状态为对应表单种类的结束状态)
        /// </summary>
        public int EndFlag
        { get; set; }

        /// <summary>
        /// 删除标志(1:该状态已经被删除)
        /// </summary>
        public int DeleteFlag
        { get; set; }

        /// <summary>
        /// 自定义标志
        /// </summary>
        public int CustomFlag
        { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description
        { get; set; }

        /// <summary>
        /// 表单的种类的描述
        /// </summary>
        public string BillKindDescription
        { get; set; }

        /// <summary>
        /// 创建用户
        /// </summary>
        public String CreateUserID
        { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public String CreateTime
        { get; set; }

        /// <summary>
        /// 客户标识
        /// </summary>
        public string customer_id { get; set; }
    }
}
