using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.CPOS.DTO.Base;

namespace JIT.CPOS.DTO.Module.VIP.Attribute.Response
{
    public class GetAttributeListRD : IAPIResponseData
    {
        /// <summary>
        /// 评论总数
        /// </summary>
        public int Count { get; set; }
        /// <summary>
        /// 评论信息列表
        /// </summary>
        public AttributeFormInfo[] AttributeList { get; set; }
    }

    public class AttributeFormInfo
    {
        /// <summary>
        /// 
        /// </summary>
        public Guid? AttributeFormID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String Name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String Type { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String OptionRemark { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String Remark { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Int32? Sequence { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Int32? OperationTypeID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Int32? Status { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Int32? AttributeTypeID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String ClientBussinessDefinedID { get; set; }
    }
}
