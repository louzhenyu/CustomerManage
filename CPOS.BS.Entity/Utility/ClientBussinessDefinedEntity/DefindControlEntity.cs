using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.Utility;
using JIT.Utility.Entity;
namespace JIT.CPOS.BS.Entity
{
   
    public class DefindControlEntity : BaseEntity
    {
        public string jitSize { get; set; }
        /// <summary>
        /// 控件类型
        /// </summary>
        public int? ControlType { get; set; }
        /// <summary>
        /// 关联值
        /// </summary>
        public string CorrelationValue { get; set; }
        /// <summary>
        /// 控件定义
        /// </summary>
        public string xtype { get; set; }
        /// <summary>
        /// 控件标签
        /// </summary>
        public string  fieldLabel { get; set; }
        /// <summary>
        /// 控件英文名称
        /// </summary>
        public string ControlName { get; set; }
        /// <summary>
        /// 控件值
        /// </summary>
        public string ControlValue { get; set; }
        /// <summary>
        /// 控件值类型
        /// </summary>
        public int? ControlValueDataType { get; set; }
        /// <summary>
        /// 是否必填
        /// </summary>
        public int? IsMustDo { get; set; }
        /// <summary>
        /// 是否记忆
        /// </summary>
        public int? IsUse { get; set; }
        /// <summary>
        /// 是否重复 0 不需要判断重复 , 1 只要这个数据在数据库有，就为重复 , 2是为几个字段加起来，如果数据表中有就为重复
        /// </summary>
        public int? IsRepeat { get; set; }
        /// <summary>
        /// 是否只读
        /// </summary>
        public int? IsRead { get; set; }


    }
}
