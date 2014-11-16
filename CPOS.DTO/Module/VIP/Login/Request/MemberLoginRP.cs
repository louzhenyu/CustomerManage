using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.CPOS.DTO.Base;

namespace JIT.CPOS.DTO.Module.VIP.Login.Request
{
   public class MemberLoginRP:IAPIRequestParameter
   {
       #region 属性
       /// <summary>
       /// 手机号码/会员账号
       /// </summary>
       public string VipNo { get; set; }
       /// <summary>
       /// 密码 
       /// </summary>
       public string Password { get; set; }
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
       #endregion

       public void Validate()
       { 
       
       }
   }
}
