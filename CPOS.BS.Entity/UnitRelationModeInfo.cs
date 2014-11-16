using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.BS.Entity
{
    /// <summary>
    /// 单位与单位之间的关联模式
    /// </summary>
    [Serializable]
    public class UnitRelationModeInfo
    {
        /// <summary>
        /// 基本的关联模式的编码
        /// </summary>
        public static string CODE_BASIC = "BASIC";
        /// <summary>
        /// 企业的关联模式的编码
        /// </summary>
        public static string CODE_KA = "KA";
        /// <summary>
        /// 渠道客户的被汇总企业与上级汇总企业的关联模式的编码
        /// </summary>
        public static string CODE_COLLECT = "COLLECT";
        /// <summary>
        /// 渠道客户的销售单位的关联模式的编码
        /// </summary>
        public static string CODE_SALE = "SALE";
        /// <summary>
        /// 渠道客户的所属KA分公司的关联模式的编码
        /// </summary>
        public static string CODE_KA_BRANCH = "KABRANCH";

        private string id;
        private string code;
        private int defaultFlag = 0;
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
        /// 缺省标志(1:默认的单位与单位之间的关联模式)
        /// </summary>
        public int DefaultFlag
        {
            get { return defaultFlag; }
            set { defaultFlag = value; }
        }
        /// <summary>
        /// 描述
        /// </summary>
        public string Description
        {
            get { return description; }
            set { description = value; }
        }
    }
}
