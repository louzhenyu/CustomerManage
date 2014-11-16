using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.BS.Entity
{
    /// <summary>
    /// 表单和角色的关联
    /// </summary>
    [Serializable]
    public class BillActionRoleModel
    {
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
        /// 所属的表单的动作的Id
        /// </summary>
        public string ActionId
        { get; set; }

        /// <summary>
        /// 角色
        /// </summary>
        public string RoleId
        { get; set; }

        /// <summary>
        /// 前置状态
        /// </summary>
        public string PreviousStatus
        { get; set; }

        /// <summary>
        /// 当前状态
        /// </summary>
        public string CurrentStatus
        { get; set; }

        /// <summary>
        /// 最小金额(如果表单的种类有金额限制时)
        /// </summary>
        public string MinMoney
        { get; set; }

        /// <summary>
        /// 最大金额(如果表单的种类有金额限制时)
        /// </summary>
        public string MaxMoney
        { get; set; }

        #region 日期控制相关

        /// <summary>
        /// 日期控制类型
        /// </summary>
        public string DateControlType
        { get; set; }


        /// <summary>
        /// 日期控制类型名称
        /// </summary>
        public string DateControlTypeName
        { get; set; }


        /// <summary>
        /// 日期
        /// </summary>
        public string DateTime
        { get; set; }

        /// <summary>
        /// 截至日期
        /// </summary>
        public string ValidateDate
        { get; set; }

        #endregion

        /// <summary>
        /// 创建用户
        /// </summary>
        public string CreateUserID
        { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public string CreateTime
        { get; set; }
    }
}
