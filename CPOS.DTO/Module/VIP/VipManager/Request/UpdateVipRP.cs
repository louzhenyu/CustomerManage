using JIT.CPOS.DTO.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.DTO.Module.VIP.VipManager.Request
{
    public class UpdateVipRP :IAPIRequestParameter
    {
        /// <summary>
        /// 会员ID
        /// </summary>
        public string VipID { get; set; }
        /// <summary>
        /// 会员名称
        /// </summary>
        public string VipName { get; set; }
        /// <summary>
        /// 手机号
        /// </summary>
        public string Phone { get; set; }
        /// <summary>
        /// 身份证
        /// </summary>
        public string Birthday { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        public int Gender { get; set; }
        /// <summary>
        /// 身份证
        /// </summary>
        public string IDNumber { get; set; }
        /// <summary>
        /// 是否可修改会员生日（Y：可以，N：不可以）
        /// </summary>
        public string Col22 { get; set; }
        public void Validate() { }
    }
}
