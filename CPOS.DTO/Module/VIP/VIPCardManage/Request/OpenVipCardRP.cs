using JIT.CPOS.DTO.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.DTO.Module.VIP.VIPCardManage.Request
{
    public class OpenVipCardRP : IAPIRequestParameter
    {
        /// <summary>
        /// 会员卡标识
        /// </summary>
        public string VipCardID { get; set; }
        /// <summary>
        /// 会员名称
        /// </summary>
        public string VipName { get; set; }
        /// <summary>
        /// 手机号
        /// </summary>
        public string Phone { get; set; }
        /// <summary>
        /// 生日
        /// </summary>
        public string Birthday { get; set; }
        /// <summary>
        /// 年龄段标签ID
        /// </summary>
        public string TagsID { get; set; }
        /// <summary>
        /// 性别（1=男；2=女）
        /// </summary>
        public int Gender { get; set; }
        /// <summary>
        /// 售卡员工
        /// </summary>
        public string SalesUserId { get; set; }

        /// <summary>
        /// 售卡员工姓名
        /// </summary>
        public string SalesUserName { get; set; }
        /// <summary>
        /// 身份证ID（对应Vip表Col18）
        /// </summary>
        public string IDCard { get; set; }
        /// <summary>
        /// 邮件
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// 区域ID
        /// </summary>
        public string CityID { get; set; }
        /// <summary>
        /// 详细地址
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// 是否赠送（0=赠送；1=不赠送）
        /// </summary>
        public int IsGift { get; set; }
        /// <summary>
        /// 会员卡号
        /// </summary>
        public string VipCardCode { get; set; }

        public void Validate() { }

    }
}
