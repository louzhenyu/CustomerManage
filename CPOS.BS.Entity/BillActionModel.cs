using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.BS.Entity
{
    /// <summary>
    /// 表单的动作
    /// </summary>
    [Serializable]
    public class BillActionModel
    {
        /// <summary>
        /// 默认
        /// </summary>
        public const string LANGUAGE_OBJECT_KIND_CODE = "Bill.Action";

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
        /// 编码
        /// </summary>
        public string Code 
        { get; set; }

        /// <summary>
        /// 新建标志(1:该动作为新建动作)
        /// </summary>
        public int CreateFlag
        { get; set; }

        /// <summary>
        /// 批准标志(1:该动作为批准动作)
        /// </summary>
        public int ApproveFlag
        { get; set; }

        /// <summary>
        /// 退回标志(1:该动作为退回动作)
        /// </summary>
        public int RejectFlag
        { get; set; }

        /// <summary>
        /// 修改标志(1:该动作为修改动作)
        /// </summary>
        public int ModifyFlag
        { get; set; }

        /// <summary>
        /// 删除标志(1:该动作为删除动作)
        /// </summary>
        public int CancelFlag
        { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description
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
        /// <summary>
        /// 排序
        /// </summary>
        public int display_index { get; set; }
    }
}
