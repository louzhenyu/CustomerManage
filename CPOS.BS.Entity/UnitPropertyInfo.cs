using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.BS.Entity
{
    /// <summary>
    /// 单位的属性
    /// </summary>
    [Serializable]
    public class UnitPropertyInfo
    {
        /// <summary>
        /// Id
        /// </summary>
        public string Id {get;set;}

        /// <summary>
        /// 单位标识
        /// </summary>
        public string UnitId {get;set;}
   
        /// <summary>
        /// 属性的组标识
        /// </summary>
        public string PropertyCodeGroupId {get;set;}

        /// <summary>
        /// 属性标识
        /// </summary>
        public string PropertyCodeId {get;set;}
        
        /// <summary>
        /// 属性的详细标识
        /// </summary>
        public string PropertyDetailId {get;set;}
       
        /// <summary>
        /// 属性的值号码
        /// </summary>
        public string PropertyDetailCode {get;set;}

        /// <summary>
        /// 属性的值名称
        /// </summary>
        public string PropertyDetailName { get; set; }
        
        private int status = 1;
        /// <summary>
        /// 状态(1:有效)
        /// </summary>
        public int Status 
        {
            get { return status; }
            set { status = value; }
        }
        /// <summary>
        /// 属性组名称
        /// </summary>
        public string PropertyCodeGroupName { get; set; }
        /// <summary>
        /// 属性组号码
        /// </summary>
        public string PropertyCodeGroupCode { get; set; }
        /// <summary>
        /// 属性名称
        /// </summary>
        public string PropertyCodeName { get; set; }
        /// <summary>
        /// 属性号码
        /// </summary>
        public string PropertyCodeCode { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        public string Create_User_id { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public string Create_Time { get; set; }
    }
}
