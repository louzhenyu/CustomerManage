using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.Utility.Entity;

namespace JIT.CPOS.BS.Entity
{
    public class VIPCollectionDatEntity : BaseEntity
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public VIPCollectionDatEntity()
        {
        }
        #endregion

        #region 属性集
        /// <summary>
        /// 
        /// </summary>
        public String VIPID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String ParameterName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String ParameterValue { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? CreateTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String CreateBy { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? LastUpdateTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String LastUpdateBy { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Int32? IsDelete { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? EffectiveDate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Int32? IsAdd { get; set; }

        /// <summary>
        /// 来源会员标识
        /// </summary>
        public String FromVipID { get; set; }


        #endregion

    }
}
