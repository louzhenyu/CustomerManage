using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.CPOS.DTO.Base;

namespace JIT.CPOS.DTO.Module.VIP.Attribute.Request
{
    public class GetAttributeListRP : IAPIRequestParameter
    {
        #region 属性
        /// <summary>
        /// 状态  1为正常 2为待确认 3为处理中 4为已处理  
        /// </summary>
        public int Status { get; set; }
        /// <summary>
        /// 会员属性  1会员基础属性、2会员扩展属性和3高级定制属性
        /// </summary>
        public int? AttributeTypeID { get; set; }
        /// <summary>
        /// 操作类型 1无操作、2添加、3修改、4删除
        /// </summary>
        public int? OperationTypeID { get; set; }
        /// <summary>
        /// 属性名
        /// </summary>
        public string Name { get; set; }  
        /// <summary>
        /// 页大小，默认15
        /// </summary>
        public int? PageSize { get; set; }
        /// <summary>
        /// 页码，默认以0开始
        /// </summary>
        public int? PageIndex { get; set; }
     
        #endregion

        public void Validate()
        {

        }
    }
}
