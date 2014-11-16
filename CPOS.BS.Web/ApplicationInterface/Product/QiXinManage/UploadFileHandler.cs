using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using JIT.CPOS.DTO.Base;
using JIT.Utility.ExtensionMethod;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.BS.BLL;
using System.Data;
using System.Data.OleDb;
using JIT.Utility.DataAccess.Query;
using System.Linq;
using System.Text;
using JIT.Utility.Log;
using JIT.Utility;
using System.Web;
using System.IO;
using System.Configuration;

namespace JIT.CPOS.BS.Web.ApplicationInterface.Product.QiXinManage
{
    /// <summary>
    ///  BatchImportUserList的摘要说明
    /// </summary>
    [Export(typeof(IQiXinRequestHandler))]
    [ExportMetadata("Action", "UploadFile")]
    public class UploadFileHandler : IQiXinRequestHandler
    {
        public string DoAction(string pRequest)
        {
            return UploadFile(pRequest);
        }

        public string UploadFile(string pRequest)
        {
            var rd = new APIResponse<UploadFileRD>();
            var rdData = new UploadFileRD();
            var rp = pRequest.DeserializeJSONTo<APIRequest<UploadFileRP>>();

            if (rp.Parameters == null)
                throw new ArgumentException();

            if (rp.Parameters != null)
                rp.Parameters.Validate();

            var loggingSessionInfo = new LoggingSessionManager().CurrentSession;
            try
            {
                HttpPostedFile files = HttpContext.Current.Request.Files[0];
                string filename = "";
                string fileName = "";

                HttpPostedFile postedFile = files;
                if (postedFile != null && postedFile.FileName != null && postedFile.FileName != "")
                {
                    filename = postedFile.FileName;
                    string suffixname = "", name = "";
                    if (filename != null)
                    {
                        name = filename.Substring(0, filename.LastIndexOf("."));
                        suffixname = filename.Substring(filename.LastIndexOf(".")).ToLower();
                    }
                    string tempPath = "/File/QiXinManage/";
                    fileName = name + DateTime.Now.ToString("yyyy.MM.dd.mm.ss") + suffixname;
                    string savepath = HttpContext.Current.Server.MapPath(tempPath);
                    if (!Directory.Exists(savepath))
                    {
                        Directory.CreateDirectory(savepath);
                    }
                    string path = savepath + @"/" + fileName;
                    postedFile.SaveAs(path);//保存
                    rdData.Path = tempPath + fileName;
                    rdData.FileUrl = ConfigurationManager.AppSettings["glt_ht_service_url"].ToString().TrimEnd('/') + rdData.Path;
                }
                else
                {
                    throw new APIException("请上传文件") { ErrorCode = 102 };
                }
                rd.ResultCode = 0;
                rdData.IsSuccess = true;
            }
            catch (Exception ex)
            {
                rd.ResultCode = 101;
                rdData.IsSuccess = false;
                rd.Message = ex.Message;
            }
            rd.Data = rdData;
            return rd.ToJSON();
        }
    }

    #region 批量导入用户名单
    public class UploadFileRP : IAPIRequestParameter
    {
        public void Validate()
        {
            if (HttpContext.Current.Request.Files.Count <= 0)
                throw new APIException("上传文件为空") { ErrorCode = 102 };
        }
    }
    public class UploadFileRD : IAPIResponseData
    {
        public bool IsSuccess { set; get; }
        public string FileUrl { set; get; }
        public string Path { set; get; }

    }
    #endregion
}