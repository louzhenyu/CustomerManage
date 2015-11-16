using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.CPOS.DTO.Base;
namespace JIT.CPOS.DTO.Module.VIP.VIPCardManage.Request
{
    public class GetVIPCardRP:IAPIRequestParameter
    {
        public void Validate() { }
        #region 属性
        /// <summary>
        /// 卡内码
        /// </summary>
        public string VipCardISN { get; set; }
        /// <summary>
        /// 卡ID
        /// </summary>
        public string VipCardID { get; set; }
        /// <summary>
        /// VipID
        /// </summary>
        public string VipID { get; set; }
        #endregion
    }
}
