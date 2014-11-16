using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.BS.Entity
{
    /// <summary>
    /// Bill的动作的种类
    /// </summary>
    public enum BillActionType
    {
        /// <summary>
        /// 新建
        /// </summary>
        Create,
        /// <summary>
        /// 修改
        /// </summary>
        Modify,
        /// <summary>
        /// 批准
        /// </summary>
        Approve,
        /// <summary>
        /// 退回
        /// </summary>
        Reject,
        /// <summary>
        /// 删除
        /// </summary>
        Cancel,
        /// <summary>
        /// 启用
        /// </summary>
        Open,
        /// <summary>
        /// 停用
        /// </summary>
        Stop
    }
}
