/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/10/24 15:38:52
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
using System.Text;
using System.Data;
using System.Reflection;
using JIT.Utility;
using JIT.Utility.ExtensionMethod;
using JIT.CPOS.BS.DataAccess;
using JIT.CPOS.BS.Entity;
using JIT.Utility.DataAccess;
using JIT.Utility.DataAccess.Query;

namespace JIT.CPOS.BS.BLL
{
    /// <summary>
    /// 业务处理：  
    /// </summary>
    public partial class ShortUrlChangeBLL
    {
        #region 原链接换成短连接 Jermyn20131024
        /// <summary>
        /// 原链接换成短连接
        /// </summary>
        /// <param name="OldUrl">原链接</param>
        /// <param name="ShortUrl">返回短连接</param>
        /// <param name="strError">返回提示</param>
        /// <returns>是否成功</returns>
        public bool SetShortUrlChange(string OldUrl,out string ShortUrl, out string strError)
        {
            ShortUrl = string.Empty;
            try
            {
                if (OldUrl == null || OldUrl.Trim().Equals(""))
                {
                    strError = "原链接不能为空";
                    return false;
                }
                ShortUrlChangeEntity changeInfo = new ShortUrlChangeEntity();
                IList<ShortUrlChangeEntity> shortUrlList = QueryByEntity(new ShortUrlChangeEntity
                {
                    OldUrl = OldUrl
                }, null);
                if (shortUrlList == null || shortUrlList.Count == 0 || shortUrlList[0].ShortUrl == null || shortUrlList[0].ShortUrl.Equals(""))
                {
                    ShortUrl = "http://o2oapi.aladingyidong.com/t.aspx?" + GenerateRandom(6);
                    changeInfo.ChangeId = BaseService.NewGuidPub();
                    changeInfo.AccessCount = 1;
                    changeInfo.ChangeCount = 0;
                    changeInfo.OldUrl = OldUrl;
                    changeInfo.ShortUrl = ShortUrl;
                    Create(changeInfo);
                }
                else {
                    changeInfo = shortUrlList[0];
                    changeInfo.AccessCount = changeInfo.AccessCount + 1;
                    Update(changeInfo, false);
                }
                strError = "成功.";
                ShortUrl = changeInfo.ShortUrl;
                return true;
            }
            catch (Exception ex) {
                strError = ex.ToString();
                return false;
            }
            
        }

        #endregion

        #region 短连接换成长连接
        /// <summary>
        /// 短连接转换成长连接
        /// </summary>
        /// <param name="ShortUrl">短连接</param>
        /// <param name="OldUrl">输出长连接</param>
        /// <param name="strError">错误信息</param>
        /// <returns>返回True，false</returns>
        public bool GetShortUrlChange(string ShortUrl, out string OldUrl, out string strError)
        {
            OldUrl = string.Empty;
            try
            {
                if (ShortUrl == null || ShortUrl.Trim().Equals(""))
                {
                    strError = "短连接不能为空";
                    return false;
                }
                ShortUrlChangeEntity changeInfo = new ShortUrlChangeEntity();
                IList<ShortUrlChangeEntity> shortUrlList = QueryByEntity(new ShortUrlChangeEntity
                {
                    ShortUrl = ShortUrl
                }, null);

                if (shortUrlList == null || shortUrlList.Count == 0 || shortUrlList[0].ShortUrl == null || shortUrlList[0].ShortUrl.Equals(""))
                {
                    strError = "不存在对应的转换关系";
                    return false;
                }
                else {
                    changeInfo = shortUrlList[0];
                    changeInfo.ChangeCount = changeInfo.ChangeCount + 1;
                    Update(changeInfo, false);
                    OldUrl = changeInfo.OldUrl.Trim().ToString();
                }
                strError = "成功.";
                return true;
            }
            catch (Exception ex) {
                strError = ex.ToString();
                return false;
            }
        }
        #endregion

        private static char[] constant =
        {
           '0','1','2','3','4','5','6','7','8','9',
           'a','b','c','d','e','f','g','h','i','j','k','l','m','n','o','p','q','r','s','t','u','v','w','x','y','z',
           'A','B','C','D','E','F','G','H','I','J','K','L','M','N','O','P','Q','R','S','T','U','V','W','X','Y','Z'
        };
        public static string GenerateRandom(int Length)
        {
            System.Text.StringBuilder newRandom = new System.Text.StringBuilder(62);
            Random rd = new Random();
            for (int i = 0; i < Length; i++)
            {
                newRandom.Append(constant[rd.Next(62)]);
            }
            return newRandom.ToString();
        }
        
    }
}