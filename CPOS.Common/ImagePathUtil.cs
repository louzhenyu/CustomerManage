using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;

namespace JIT.CPOS.Common
{
    public class ImagePathUtil
    {
        /// <summary>
        /// 获取图片缩略图路劲
        /// </summary>
        /// <param name="imagePath">原图路径</param>
        /// <param name="size">缩略图大小</param>
        /// <returns></returns>
        public static string GetImagePathStr(string imagePath, string size)
        {
            if (!string.IsNullOrEmpty(imagePath))
            {
                string temp = imagePath.Substring(0, imagePath.Length - 4);
                string extension = imagePath.Substring(imagePath.Length - 3);
                string newPath = temp + "_" + size + "." + extension;
                if (RemoteFileExists(newPath))
                    return newPath;
                else
                    return imagePath;
            }
            else
                return imagePath;
        }
        /// <summary>
        /// 判断远程文件是否存在
        /// </summary>
        /// <param name="fileUrl"></param>
        /// <returns></returns>
        private static bool RemoteFileExists(string fileUrl)
        {
            bool result = false;//判断结果

            WebResponse response = null;
            try
            {
                WebRequest req = WebRequest.Create(fileUrl);

                response = req.GetResponse();

                result = response == null ? false : true;

            }
            catch (Exception ex)
            {
                result = false;
            }
            finally
            {
                if (response != null)
                {
                    response.Close();
                }
            }

            return result;
        }
    }
}