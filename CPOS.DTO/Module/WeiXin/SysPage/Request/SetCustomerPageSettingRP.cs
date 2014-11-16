using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using JIT.CPOS.DTO.Base;

namespace JIT.CPOS.DTO.Module.WeiXin.SysPage.Request
{
   public class SetCustomerPageSettingRP:IAPIRequestParameter
   {
       #region 属性
       public string MappingId { get; set; } //客户和版本

       public string PageKey { get; set; }//PageKey

       public string[] Node { get; set; }  //节点

       public string[] NodeValue { get; set; } //节点值
       #endregion

       #region 错误码
       const int NULL_MappingId = 301;
       const int NULL_EXISTS_PageKey = 302;
       const int NULL_EXISTS_Node = 303;
       const int NULL_EXISTS_NodeValue = 304;
       #endregion
       public void Validate()
        {
            if (string.IsNullOrWhiteSpace(this.MappingId))
            {
                throw new APIException(NULL_MappingId, "未传入参数MappingId");
            }
            if (string.IsNullOrWhiteSpace(this.PageKey))
            {
                throw new APIException(NULL_EXISTS_PageKey, "未传入参数PageKey");
            }
            if (this.Node.Length<1)
            {
                throw new APIException(NULL_EXISTS_Node, "参数Node传入错误");
            }
            if (this.NodeValue.Length<1)
            {
                throw new APIException(NULL_EXISTS_NodeValue, "参数NodeValue传入错误");
            }
        }
    }
}
