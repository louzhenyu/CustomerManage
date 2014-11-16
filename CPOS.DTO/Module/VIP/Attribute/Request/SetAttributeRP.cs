using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.CPOS.DTO.Base;

namespace JIT.CPOS.DTO.Module.VIP.Attribute.Request
{
    public class SetAttributeRP : IAPIRequestParameter
    {
        #region 属性
        /// <summary>
        /// 
        /// </summary>
        public Guid? AttributeFormID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string OptionRemark { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? Sequence { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? OperationTypeID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? Status { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? AttributeTypeID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ClientBussinessDefinedID { get; set; }
        #endregion

        public void Validate()
        {

        }
    }
}
