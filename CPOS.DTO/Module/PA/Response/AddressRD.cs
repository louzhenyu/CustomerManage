using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.DTO.Module.PA.Response
{
    /// <summary>
    /// 平安地址响应接口
    /// </summary>
    public class AddressRD
    {
        /// <summary>
        /// 姓名
        /// </summary>
        public string userName { get; set; }
        /// <summary>
        /// AES加密的手机号
        /// </summary>
        public string phoneNum { get; set; }
        /// <summary>
        /// 邮编
        /// </summary>
        public string postCode { get; set; }
        /// <summary>
        /// 省份Code
        /// </summary>
        public string provinceCode { get; set; }
        /// <summary>
        /// 省份
        /// </summary>
        public string province { get; set; }
        /// <summary>
        /// 城市Code
        /// </summary>
        public string cityCode { get; set; }
        /// <summary>
        /// 城市
        /// </summary>
        public string city { get; set; }
        /// <summary>
        /// 区域Code
        /// </summary>
        public string areaCode { get; set; }
        /// <summary>
        /// 区域
        /// </summary>
        public string area { get; set; }
        /// <summary>
        /// 详细地址
        /// </summary>
        public string address { get; set; }
        /// <summary>
        /// 默认标识 1表示默认；0表示非默认
        /// </summary>
        public string defaultFlag { get; set; }
        /// <summary>
        /// 地址id
        /// </summary>
        public string addressId { get; set; }
    }
}
