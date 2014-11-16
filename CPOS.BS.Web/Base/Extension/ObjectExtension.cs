/*
 * Author		:roy.tian
 * EMail		:jun.tian@jitmarketing.cn
 * Company		:JIT
 * Create On	:19/2/2012 10:03:10 AM
 * Description	:
 * 1st Modified On	:
 * 1st Modified By	:
 * 1st Modified Desc:
 * 1st Modifier EMail	:
 * 2st Modified On	:
 * 2st Modified By	:
 * 2st Modifier EMail	:
 * 2st Modified Desc:
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JIT.CPOS.BS.Web.Extension
{
    public static class ObjectExtension
    {
        #region int类型转换的方法
        /// <summary>
        /// int 类型转换的方法
        /// </summary>
        /// <param name="id">string 值</param>
        /// <returns>返回转换后的值</returns>
        public static int ToInt(this object id)
        {
            int idhold = 0;
            return int.TryParse(id.ToString(), out idhold) == true ? idhold : 0;
        }
        #endregion
        #region datetime类型转换的方法
        /// <summary>
        /// datetime 类型转换的方法
        /// </summary>
        /// <param name="id">string 值</param>
        /// <returns>返回转换后的值</returns>
        public static DateTime ToDateTime(this string time)
        {
            DateTime timehold = DateTime.Now;
            return DateTime.TryParse(time, out timehold) == true ? timehold : DateTime.Now;
        }
        #endregion
        #region decimal类型转换的方法
        /// <summary>
        /// decimal 类型转换的方法
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static decimal ToDecimal(this string id)
        {
            decimal idhold = 0;
            return decimal.TryParse(id, out idhold) == true ? idhold : 0;
        }
        #endregion
    }
}