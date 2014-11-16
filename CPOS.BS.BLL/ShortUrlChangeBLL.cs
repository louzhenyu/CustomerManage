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
    /// ҵ����  
    /// </summary>
    public partial class ShortUrlChangeBLL
    {
        #region ԭ���ӻ��ɶ����� Jermyn20131024
        /// <summary>
        /// ԭ���ӻ��ɶ�����
        /// </summary>
        /// <param name="OldUrl">ԭ����</param>
        /// <param name="ShortUrl">���ض�����</param>
        /// <param name="strError">������ʾ</param>
        /// <returns>�Ƿ�ɹ�</returns>
        public bool SetShortUrlChange(string OldUrl,out string ShortUrl, out string strError)
        {
            ShortUrl = string.Empty;
            try
            {
                if (OldUrl == null || OldUrl.Trim().Equals(""))
                {
                    strError = "ԭ���Ӳ���Ϊ��";
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
                strError = "�ɹ�.";
                ShortUrl = changeInfo.ShortUrl;
                return true;
            }
            catch (Exception ex) {
                strError = ex.ToString();
                return false;
            }
            
        }

        #endregion

        #region �����ӻ��ɳ�����
        /// <summary>
        /// ������ת���ɳ�����
        /// </summary>
        /// <param name="ShortUrl">������</param>
        /// <param name="OldUrl">���������</param>
        /// <param name="strError">������Ϣ</param>
        /// <returns>����True��false</returns>
        public bool GetShortUrlChange(string ShortUrl, out string OldUrl, out string strError)
        {
            OldUrl = string.Empty;
            try
            {
                if (ShortUrl == null || ShortUrl.Trim().Equals(""))
                {
                    strError = "�����Ӳ���Ϊ��";
                    return false;
                }
                ShortUrlChangeEntity changeInfo = new ShortUrlChangeEntity();
                IList<ShortUrlChangeEntity> shortUrlList = QueryByEntity(new ShortUrlChangeEntity
                {
                    ShortUrl = ShortUrl
                }, null);

                if (shortUrlList == null || shortUrlList.Count == 0 || shortUrlList[0].ShortUrl == null || shortUrlList[0].ShortUrl.Equals(""))
                {
                    strError = "�����ڶ�Ӧ��ת����ϵ";
                    return false;
                }
                else {
                    changeInfo = shortUrlList[0];
                    changeInfo.ChangeCount = changeInfo.ChangeCount + 1;
                    Update(changeInfo, false);
                    OldUrl = changeInfo.OldUrl.Trim().ToString();
                }
                strError = "�ɹ�.";
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