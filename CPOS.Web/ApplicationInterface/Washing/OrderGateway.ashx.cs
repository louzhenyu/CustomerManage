using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

using JIT.CPOS.DTO.Base;
using JIT.CPOS.BS.BLL;
using JIT.Utility.ExtensionMethod;
using JIT.CPOS.BS.Entity;
using JIT.Utility.DataAccess.Query;
using System.Collections;
using System.Configuration;
using JIT.CPOS.Common;
using JIT.Utility.Log;
using System.IO;


namespace JIT.CPOS.Web.ApplicationInterface.Washing
{
    /// <summary>
    /// OrderGateway 的摘要说明
    /// </summary>
    public class OrderGateway : BaseGateway
    {


        protected override string ProcessAction(string pType, string pAction, string pRequest)
        {
            string rst;
            switch (pAction)
            {
               
                case "UploadFiles"://上传图片通用方法
                    rst = UploadFiles(pRequest);
                    break;
                default:
                    throw new APIException(string.Format("找不到名为：{0}的Action方法。", pAction));
            }
            return rst;
        }

        #region 方法 add by Henry 2015-1-14       
  
     
        /// <summary>
        /// 上传图片通用方法
        /// </summary>
        /// <param name="pRequest"></param>
        /// <returns></returns>
        protected string UploadFiles(string pRequest)
        {
            var rp = pRequest.DeserializeJSONTo<APIRequest<UploadFilesRP>>();
            rp.Parameters.Validate();

            var loggingSessionInfo = Default.GetBSLoggingSession(rp.CustomerID, rp.UserID);


            var rd = new EmptyRD();

            var objectImageBll = new ObjectImagesBLL(loggingSessionInfo);   //图片BLL实例化

            var customerBasicSettingBll = new CustomerBasicSettingBLL(loggingSessionInfo);
            string expandName = rp.Parameters.ExpandName;

            //随机生成文件名称
            string fileName = StringUtil.GetRandomStr(18).ToLower() + "." + expandName;

            //允许上传的扩展名
            string allowExpandName = customerBasicSettingBll.GetSettingValueByCode("AllowUploadExpandName");
            allowExpandName = !string.IsNullOrEmpty(allowExpandName) ? allowExpandName : "gif,jpg,jpeg,png,bmp";//设置缺省图片格式
            //不允许
            if (allowExpandName.IndexOf(expandName) == -1)
            {
                var rsp = new SuccessResponse<IAPIResponseData>(rd);
                rsp.ResultCode = 1;
                rsp.Message = "请上传" + allowExpandName + "格式的文件";
                return rsp.ToJSON();
            }
            //转base64的编码,获取流
            MemoryStream stream = new MemoryStream(Convert.FromBase64String(rp.Parameters.Base64str));
            byte[] bytes = new byte[stream.Length];
            stream.Read(bytes, 0, bytes.Length);
            stream.Seek(0, SeekOrigin.Begin);

            if (allowExpandName.IndexOf(expandName) > -1)
            {
                //允许上传的图片最大限制
                string uploadImageMax = customerBasicSettingBll.GetSettingValueByCode("UploadImageMax");
                if (!string.IsNullOrEmpty(uploadImageMax))
                {
                    int maxSize = int.Parse(uploadImageMax);
                    if (stream.Length > maxSize)
                    {
                        var rsp = new SuccessResponse<IAPIResponseData>(rd);
                        rsp.ResultCode = 1;
                        rsp.Message = "请上传" + maxSize + "M以内的图片";
                        return rsp.ToJSON();
                    }
                }
            }
            //文件目录
            string uploadFilePath = customerBasicSettingBll.GetSettingValueByCode("UploadFilePath");
           uploadFilePath=string.IsNullOrEmpty(uploadFilePath)? "/Images/":uploadFilePath;//如果为空，就取默认的

            uploadFilePath += DateTime.Now.Year + "/" + DateTime.Now.Month + "." + DateTime.Now.Day + "/";
            if (!IOUtil.ExistsFile(IOUtil.MapPath(uploadFilePath)))
                IOUtil.CreateDirectoryIfNotExists(IOUtil.MapPath(uploadFilePath));

            // 把 byte[] 写入文件 
            FileStream fs = new FileStream(IOUtil.MapPath(uploadFilePath) + fileName, FileMode.Create);//通过IOUtil.MapPath由网络目录转换成物理目录
            BinaryWriter bw = new BinaryWriter(fs);
            bw.Write(bytes);
            bw.Close();
            fs.Close();

            objectImageBll.Create(new ObjectImagesEntity() { ImageId = Guid.NewGuid().ToString(), ObjectId = rp.Parameters.ObjectID, ImageURL = uploadFilePath + fileName });

            //文件DNS地址
            string fileDNS = customerBasicSettingBll.GetSettingValueByCode("FileDNS"); ;

            var rsp1 = new SuccessResponse<IAPIResponseData>(rd);
            rsp1.Message = fileDNS + uploadFilePath + fileName;//当前上传的图片路径
            return rsp1.ToJSON();
        }
        #endregion

        #region 请求/返回参数 add by Henry 2015-1-14
    
        public class UploadFilesRP : IAPIRequestParameter
        {
            /// <summary>
            /// 订单ID（其他对象ID也可使用）
            /// </summary>
            public string ObjectID { get; set; }
            /// <summary>
            /// 图片的base 64位编码(传输前需要将 + 编码成 %2b )
            /// </summary>
            public string Base64str { get; set; }
            /// <summary>
            /// 文件的扩展名(如：jpg、png)
            /// </summary>
            public string ExpandName { get; set; }
            public void Validate()
            {
            }
        }
        #endregion
    }
}