using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.BS.Entity
{
    /// <summary>
    /// 查询Bill的动作与角色的关系
    /// </summary>
    [Serializable]
    public class SelectBillActionRoleInfo : BillActionRoleModel
    {
        private string billKindDescription;
        private string billActionDescription;
        private string roleDescription;
        private string previousBillStatusDescription;
        private string currentBillStatusDescription;
        /// <summary>
        /// Bill种类的描述
        /// </summary>
        public string KindDescription
        {
            get { return billKindDescription; }
            set { billKindDescription = value; }
        }
        /// <summary>
        /// Bill动作的描述
        /// </summary>
        public string ActionDescription
        {
            get { return billActionDescription; }
            set { billActionDescription = value; }
        }
        /// <summary>
        /// 角色的描述
        /// </summary>
        public string RoleDescription
        {
            get { return roleDescription; }
            set { roleDescription = value; }
        }
        /// <summary>
        /// 前置状态的描述
        /// </summary>
        public string PreviousStatusDescription
        {
            get { return previousBillStatusDescription; }
            set { previousBillStatusDescription = value; }
        }
        /// <summary>
        /// 当前状态的描述
        /// </summary>
        public string CurrentStatusDescription
        {
            get { return currentBillStatusDescription; }
            set { currentBillStatusDescription = value; }
        }
    }
}
