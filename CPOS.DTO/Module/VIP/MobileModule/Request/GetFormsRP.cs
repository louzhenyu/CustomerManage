using JIT.CPOS.DTO.Base;

namespace JIT.CPOS.DTO.Module.VIP.MobileModule.Request
{

    /// <summary>
    /// 获取表单元素请求参数
    /// </summary>
    public class GetFormsRP : IAPIRequestParameter
    {
        public int PageIndex { get; set; }
        
        public int PageSize { get; set; }

        public int Type { get; set; }

        //public string CustomerID { get; set; }

        public void Validate()
        {
            if (this.PageIndex < 0)
            {
                PageIndex = 0;
            }
            if (this.PageSize < 1)
            {
                PageSize = 15;
            }
        }
    }
}
