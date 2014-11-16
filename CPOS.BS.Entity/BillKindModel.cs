using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.BS.Entity
{
    /// <summary>
    /// 表单种类
    /// </summary>
    public class BillKindModel
    {
        #region 用户
        /// <summary>
        /// 用户新建表单的种类的编码
        /// </summary>
        public const string CODE_USER_NEW = "NewUser";
        /// <summary>
        /// 用户修改表单的种类的编码
        /// </summary>
        public const string CODE_USER_MODIFY = "ModifyUser";
        /// <summary>
        /// 用户停用表单的种类的编码
        /// </summary>
        public const string CODE_USER_DISABLE = "DisableUser";
        /// <summary>
        /// 用户启用表单的种类的编码
        /// </summary>
        public const string CODE_USER_ENABLE = "EnableUser";
        #endregion
        #region 表单种类属性定义
        private string id;
        private string code;
        private int moneyFlag;
        private string queryUrl;
        private string createUrl;
        private string modifyUrl;
        private string approveUrl;
        private string description;
        /// <summary>
        /// Id
        /// </summary>
        public string Id
        {
            get { return id; }
            set { id = value; }
        }
        /// <summary>
        /// 编码
        /// </summary>
        public string Code
        {
            get { return code; }
            set { code = value; }
        }
        /// <summary>
        /// 金额标志(0:该种表单不需要金额控制)
        /// </summary>
        public int MoneyFlag
        {
            get { return moneyFlag; }
            set { moneyFlag = value; }
        }
        /// <summary>
        /// 查看该种类型的表单时,跳转的页面
        /// </summary>
        public string QueryUrl
        {
            get { return queryUrl; }
            set { queryUrl = value; }
        }
        /// <summary>
        /// 创建该种类型的表单时,跳转的页面
        /// </summary>
        public string CreateUrl
        {
            get { return createUrl; }
            set { createUrl = value; }
        }
        /// <summary>
        /// 修改该种类型的表单时,跳转的页面
        /// </summary>
        public string ModifyUrl
        {
            get { return modifyUrl; }
            set { modifyUrl = value; }
        }
        /// <summary>
        /// 审核,退回,删除该种类型的表单时,跳转的页面
        /// </summary>
        public string ApproveUrl
        {
            get { return approveUrl; }
            set { approveUrl = value; }
        }
        /// <summary>
        /// 描述
        /// </summary>
        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        #endregion
    }
}
