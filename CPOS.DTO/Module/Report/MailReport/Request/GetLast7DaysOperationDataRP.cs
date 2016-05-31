using JIT.CPOS.DTO.Base;
using System;

namespace JIT.CPOS.DTO.Module.Report.MailReport.Request
{
    public class GetLast7DaysOperationDataRP : IAPIRequestParameter
    {
        /// <summary>
        /// 筛选日期
        /// </summary>
        public string DateCode { get; set; }
        public void Validate()
        {
            if (string.IsNullOrWhiteSpace(DateCode))
            {
                throw new APIException("请输入筛选日期") { ErrorCode = ERROR_CODES.DEFAULT_ERROR };
            }
            try
            {
                DateTime.Parse(DateCode);
            }
            catch
            {
                throw new APIException("输入的字符串不是时间格式!") { ErrorCode = ERROR_CODES.DEFAULT_ERROR };
            }
        }
    }
}
