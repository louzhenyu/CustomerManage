using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.CPOS.DTO.Base;

namespace JIT.CPOS.DTO.Module.SapMessageApi.Request
{
    public class RegisterUserRP : IAPIRequestParameter
    {
        /// <summary>
        /// 邮箱
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// 确认密码
        /// </summary>
        public string ConfirmPassword { get; set; }

        private string _fromSystem = "CRM";
        /// <summary>
        /// 来源系统   系统的简称；如：SAP、 WMS、 CRM、 BMP、 OA 等
        /// </summary>
        public string FromSystem { get { return _fromSystem; } set { _fromSystem = value; } }

        public void Validate()
        {
        }
    }
}
