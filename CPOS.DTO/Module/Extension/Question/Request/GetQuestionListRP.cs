using JIT.CPOS.DTO.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.DTO.Module.Extension.Question.Request
{
    public class GetQuestionListRP:IAPIRequestParameter
    {
        #region 属性
        /// <summary>
        /// 页大小，默认10
        /// </summary>
        public int PageSize { get; set; }
        /// <summary>
        /// 页码，默认以0开始
        /// </summary>
        public int PageIndex { get; set; }
     
        #endregion

        public void Validate()
        {

        }
    }
}
