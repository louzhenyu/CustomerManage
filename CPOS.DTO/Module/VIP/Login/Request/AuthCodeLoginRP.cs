using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.CPOS.DTO.Base;

namespace JIT.CPOS.DTO.Module.VIP.Login.Request
{
    public class AuthCodeLoginRP : IAPIRequestParameter
    {
        /// <summary>
        /// 手机号
        /// </summary>
        public string Mobile { get; set; }
        /// <summary>
        /// 验证码
        /// </summary>
        public string AuthCode { get; set; }
        /// <summary>
        /// VIP来源
        /// <remarks>
        /// <para>Ø  1=会员APP    </para>
        /// <para>Ø  2=门店PAD    </para>
        /// <para>Ø  3=微信       </para>
        /// <para>Ø  4=活动二维码 </para>
        /// <para>Ø  5=电话客服   </para>
        /// <para>Ø  6=导入数据   </para>
        /// <para>Ø  7=微博       </para>
        /// <para>Ø  8=网站注册   </para>
        /// <para>Ø  9=门店二维码 </para>
        /// <para>Ø  10=微信转发  </para>
        /// <para>Ø  11=阿拉丁平台</para>
        /// </remarks>
        /// </summary>
        public int? VipSource { get; set; }
        /// <summary>
        /// 注册时直接输入真实姓名
        /// </summary>
        public string VipRealName { get; set; }

        /// <summary>
        /// 创意活动参数
        /// </summary>
        public string CTWEventId { get; set; }
        
        /// <summary>
        /// 优惠券种ID (区分优惠券与活动)
        /// </summary>
        public string couponId { get; set; }
        public void Validate()
        {

        }
    }
}
