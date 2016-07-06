using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.DTO.ValueObject
{
    public enum SapTransType
    {
        /// <summary>
        /// 新增
        /// </summary>
        A,
        /// <summary>
        /// 修改
        /// </summary>
        U,
        /// <summary>
        /// 删除
        /// </summary>
        D,
        /// <summary>
        /// 取消
        /// </summary>
        C,
        /// <summary>
        /// 关闭
        /// </summary>
        L
    }
}
