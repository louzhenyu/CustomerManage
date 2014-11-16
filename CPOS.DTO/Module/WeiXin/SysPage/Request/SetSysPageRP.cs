using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using JIT.CPOS.DTO.Base;
namespace JIT.CPOS.DTO.Module.WeiXin.SysPage.Request
{
    public class SetSysPageRP:IAPIRequestParameter
    {
        #region 属性
        public Guid? PageId { get; set; }//String	否	标识,为空新增,不为空则修改
        public string Author { get; set; }//String	是	作者
        public string Version { get; set; }//String	是	版本
        public string PageJson { get; set; }//String	是	Page内容的Json格式
        //public string ModuleName { get; set; }//String	是	模块名
        #endregion

        #region 错误码
        const int ERROR_NULL_Author = 301;  //作者不能为空
        const int ERROR_NULL_Version = 302;  //版本为空
        const int ERROR_NULL_PageJson = 303;  //Page内容的Json格式
        const int ERROR_NULL_ModuleName = 304; //模块名

        #endregion
        public void Validate()
        {
            if (string.IsNullOrWhiteSpace(Author))
            {
                throw new APIException("作者名称不能为空！") { ErrorCode = ERROR_NULL_Author };
            }
            if (string.IsNullOrWhiteSpace(Version))
            {
                throw new APIException("版本不能为空！") {ErrorCode=ERROR_NULL_Version };
            }
            if (string.IsNullOrWhiteSpace(PageJson))
            {
                throw new APIException("Page内容的Json内容不能为空！") {ErrorCode=ERROR_NULL_PageJson };
            }
            //if (string.IsNullOrWhiteSpace(ModuleName))
            //{
            //    throw new APIException("模块名称不能为空！") { ErrorCode = ERROR_NULL_ModuleName };
            //}
        }
    }
}
