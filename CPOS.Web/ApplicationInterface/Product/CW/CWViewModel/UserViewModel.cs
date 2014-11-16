using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JIT.CPOS.Web.ApplicationInterface.Product.CW
{
    /// <summary>
    /// 用户视图模型。
    /// </summary>
    public class UserViewModel
    {

        /// <summary>
        /// 用户标识
        /// </summary>
        public string UserID { get; set; }

        /// <summary>
        /// 第三方用户标识
        /// </summary>
        public string VoipAccount { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 用户图像
        /// </summary>
        public string UserImgURL { get; set; }

    }
}