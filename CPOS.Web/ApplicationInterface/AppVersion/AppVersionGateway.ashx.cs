using JIT.CPOS.DTO.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using JIT.Utility.ExtensionMethod;
using JIT.CPOS.BS.BLL;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using JIT.CPOS.BS.Entity;
using System.IO;
using System.Text;

namespace JIT.CPOS.Web.ApplicationInterface
{
    /// <summary>
    /// AppVersionGateway 的摘要说明
    /// </summary>
    public class AppVersionGateway : BaseGateway
    {
        /// <summary>
        /// 增加下载记录
        /// </summary>
        /// <param name="pRequest"></param>
        /// <returns></returns>
        private string AddVersionDownLoadLog(string pRequest)
        {
            var rp = pRequest.DeserializeJSONTo<APIRequest<AppVersionDownloadLogRP>>();
            var loggingSessionInfo = Default.GetBSLoggingSession(rp.CustomerID, "1");
            if (rp.Parameters.Channel == 0 || string.IsNullOrEmpty(rp.CustomerID +　rp.Parameters.Plat + rp.Parameters.Version + rp.Parameters.DownloadURL))
                throw new APIException("缺少请求参数。")
                {
                    ErrorCode = ERROR_CODES.INVALID_REQUEST_LACK_REQUEST_PARAMETER
                };
            var appVersionLogBLL = new APPVersionDownloadVIPLogBLL(loggingSessionInfo);
            var entity = new APPVersionDownloadVIPLogEntity
            { 
                    Channel = rp.Parameters.Channel,
                    Plat = rp.Parameters.Plat,Version = rp.Parameters.Version,
                    DownloadUrl = rp.Parameters.DownloadURL, CustomerId = rp.CustomerID,
                    VipId = rp.Parameters.VipId
            };
            try
            {
                appVersionLogBLL.AddAppVersionDownloadLog(entity);
                var result = new EmptyResponseData();
                var rsp = new  SuccessResponse<IAPIResponseData>(result);
                return rsp.ToJSON();
            }
            catch (APIException ex)
            {
                throw new APIException("添加过程中出错.");
            }
        }
        /// <summary>
        /// 获取客户端版本信息
        /// </summary>
        /// <param name="pRequest"></param>
        /// <returns></returns>
        private string GetAPPVersion(string pRequest)
        {
            var rp = pRequest.DeserializeJSONTo<APIRequest<AppVersionRP>>();
            var loggingSessionInfo = Default.GetBSLoggingSession(rp.CustomerID, "1");
            if (rp.Parameters.Channel == 0 || string.IsNullOrEmpty(rp.CustomerID + rp.Parameters.Plat + rp.Parameters.CurrentVersionNo))
                throw new APIException("缺少请求参数。")
                {
                    ErrorCode = ERROR_CODES.INVALID_REQUEST_LACK_REQUEST_PARAMETER
                };
            var appVersionBLL = new APPVersionManagerBLL(loggingSessionInfo);
            var entity = appVersionBLL.GetAppVersion(rp.CustomerID, rp.Parameters.Channel, rp.Parameters.Plat);
            if(null == entity)
                throw new APIException("未找到对应版本信息。")
                {
                    ErrorCode = ERROR_CODES.INVALID_REQUEST_LACK_REQUEST_PARAMETER
                };
            var result = new AppVersionRD()
            {
                DownloadURL = entity.DownloadURL,
                Notice = entity.Notice.Replace("\\n","\n"),
                VersionNoUpdate = entity.VersionNoUpdate
            };
            var currVer = new Version(rp.Parameters.CurrentVersionNo);
            var updateVer = new Version(entity.VersionNoUpdate);
            var lowestVer = new Version(entity.VersionNoLowest);
            result.IsNewVersion = currVer < updateVer ? 1 : 0;  //如果小于数据库里的当前版本，就需要更新
            result.IsSkip = currVer < lowestVer ? 0 : 1;  //如果小于数据库里的最小版本，就需要跳过?IsSkip=1，表示非强制更新
            var rsp = new SuccessResponse<IAPIResponseData>(result);
            return rsp.ToJSON();
        }

        /// <summary>
        /// 保存客户端错误日志
        /// </summary>
        /// add by donal 2014-9-26 14:50:33
        /// <param name="pRequest"></param>
        /// <returns></returns>
        private string AddAppLog(string pRequest)
        {
            var rp = pRequest.DeserializeJSONTo<APIRequest<AppLogRP>>();

            if (string.IsNullOrWhiteSpace(rp.Parameters.Base64str)
                || string.IsNullOrWhiteSpace(rp.Parameters.Extensions)
                || string.IsNullOrWhiteSpace(rp.Parameters.ProjectName)
                || string.IsNullOrWhiteSpace(rp.Parameters.Platform)
                )
            {
                throw new APIException("缺少请求参数。")
                {
                    ErrorCode = ERROR_CODES.INVALID_REQUEST_LACK_REQUEST_PARAMETER
                };
            }

            try
            {
                //根目录
                string basePath = System.AppDomain.CurrentDomain.BaseDirectory + "logs\\AppLogs";
                
                //文件路径
                StringBuilder sbdPath = new StringBuilder();
                sbdPath.Append(basePath);

                #region 根据参数获取地址

                string[] PlatformList = new string[] { "ANDROID", "IOS" };
                string[] ProjectNameList = new string[] { "ALD", "CUSTOMER", "SERVICE" };

                if (PlatformList.Contains(rp.Parameters.Platform.ToUpper()))
                    sbdPath.Append("\\" + rp.Parameters.Platform.ToUpper());
                else
                    throw new APIException("平台参数不存在.");

                if (ProjectNameList.Contains(rp.Parameters.ProjectName.ToUpper()))
                    sbdPath.Append("\\"+rp.Parameters.ProjectName.ToUpper());
                else
                    throw new APIException("项目名称参数不存在.");

                string fileName = string.Format("{0}{1}{2}{3}{4}{5}{6}"
                       , DateTime.Now.Year.ToString()
                       , DateTime.Now.Month.ToString()
                       , DateTime.Now.Day.ToString()
                       , DateTime.Now.Hour.ToString()
                       , DateTime.Now.Minute.ToString()
                       , DateTime.Now.Second.ToString()
                       , DateTime.Now.Millisecond.ToString()); 
                sbdPath.Append(string.Format("\\{0}.{1}",fileName,rp.Parameters.Extensions));

                #endregion

                //转base64的编码,获取流
                MemoryStream stream = new MemoryStream(Convert.FromBase64String(rp.Parameters.Base64str));
                byte[] bytes = new byte[stream.Length];
                stream.Read(bytes, 0, bytes.Length);
                stream.Seek(0, SeekOrigin.Begin);

                // 把 byte[] 写入文件 
                FileStream fs = new FileStream(sbdPath.ToString(), FileMode.Create);
                BinaryWriter bw = new BinaryWriter(fs);
                bw.Write(bytes);
                bw.Close();
                fs.Close();


                //记录上传的日志  
                string logpath = string.Format("{0}\\{1}\\{2}{3}{4}.txt", basePath, "UpLogs", DateTime.Today.Year.ToString(),DateTime.Today.Month.ToString(),DateTime.Today.Day.ToString());
                using (StreamWriter sw = File.AppendText(logpath))
                {
                    string w = string.Format("{0}     [Platform:{1}]-[ProjectName:{2}]-[FileName:{3}]-[Extensions:{4}]"
                    , DateTime.Now.ToString(), rp.Parameters.Platform, rp.Parameters.ProjectName
                    , rp.Parameters.FileName, rp.Parameters.Extensions);
                    sw.WriteLine(w);
                    sw.Close();
                }

                var result = new EmptyResponseData();
                var rsp = new SuccessResponse<IAPIResponseData>(result);
                return rsp.ToJSON();
            }
            catch (APIException ex)
            {
                throw new APIException("添加过程中出错.");
            }
        }

                
        protected override string ProcessAction(string pType, string pAction, string pRequest)
        {
            string result;
            switch (pAction)
            {
                case "GetAPPVersion":
                    result = GetAPPVersion(pRequest);
                    break;
                case "AddVersionDownLoadLog":
                    result = AddVersionDownLoadLog(pRequest);
                    break;
                case "UploadFiles":
                    result = AddAppLog(pRequest); // add by nonal 2014-9-26 14:51:03
                    break;
                default:
                    throw new APIException(string.Format("找不到名为：{0}的action处理方法.", pAction))
                    {
                        ErrorCode = ERROR_CODES.INVALID_REQUEST_CAN_NOT_FIND_ACTION_HANDLER
                    };

            }
            return result;
        }

        
    }
    public class AppVersionRP : IAPIRequestParameter
    {
        /// <summary>
        /// 渠道
        /// </summary>
        public int Channel { get; set; }
        /// <summary>
        /// 平台
        /// Android,IOS
        /// </summary>
        public string Plat { get; set; }
        /// <summary>
        /// 当前版本号
        /// </summary>
        public string CurrentVersionNo { get; set; }

        public void Validate()
        {
            
        }
    }
    public class AppVersionRD : IAPIResponseData
    {
        /// <summary>
        /// 是否有新版本
        /// 0:没有，1:有
        /// </summary>
        public int IsNewVersion { get; set; }
        /// <summary>
        /// 是否强制更新
        /// 0:不可忽略，强制更新，1:可忽略
        /// </summary>
        public int IsSkip { get; set; }
        /// <summary>
        /// 最新版本号
        /// </summary>
        public string VersionNoUpdate { get; set; }
        /// <summary>
        /// 更新内容(多行内容用#分隔，移动端处理时见#换行)
        /// </summary>
        public string Notice { get; set; }
        /// <summary>
        /// 下载路径
        /// </summary>
        public string DownloadURL { get; set; }
    }
   
    public class AppVersionDownloadLogRP : IAPIRequestParameter
    {
        public string VipId { get; set; }
        /// <summary>
        /// 渠道（1=IOS标准；2=IOS外置；3=Android下载）
        /// </summary>
        public int Channel { get; set; }
        public string Plat { get; set; }
        public string Version { get; set; }
        public string DownloadURL { get; set; }

        public void Validate()
        {
            
        }
    }

    /// <summary>
    /// app端日志请求参数
    /// </summary>
    public class AppLogRP : IAPIRequestParameter
    {
        /// <summary>
        /// 平台
        /// </summary>
        public string Platform { get; set; } 
        /// <summary>
        /// 项目名
        /// </summary>
        public string ProjectName { get; set; }
        /// <summary>
        /// 文件名
        /// </summary>
        public string FileName { get; set; }
        /// <summary>
        /// 文件扩展名
        /// </summary>
        public string Extensions { get; set; }
        /// <summary>
        /// Base64文件流
        /// </summary>
        public string Base64str { get; set; }

        public void Validate()
        { 
        }
    }
}