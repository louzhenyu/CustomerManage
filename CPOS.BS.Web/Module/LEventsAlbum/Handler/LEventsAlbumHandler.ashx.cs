using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.Common;
using JIT.Utility.DataAccess.Query;
using JIT.Utility.ExtensionMethod;
using System.IO;
using System;

namespace JIT.CPOS.BS.Web.Module.LEventsAlbum.Handler
{
    /// <summary>
    /// LEventsAlbumHandler
    /// </summary>
    public class LEventsAlbumHandler : JIT.CPOS.BS.Web.PageBase.JITCPOSAjaxHandler
    {
        /// <summary>
        /// 页面入口
        /// </summary>
        /// <param name="pContext"></param>
        protected override void AjaxRequest(HttpContext pContext)
        {
            string content = "";
            switch (pContext.Request.QueryString["method"])
            {
                case "GetLEventsAlbumData":      //视频查询
                    content = GetLEventsAlbumData();
                    break;
                case "LEventsAlbumDeleteData":     //新闻删除
                    content = LEventsAlbumDeleteData();
                    break;
                case "GetLEventsAlbumById":  //根据ID获取新闻信息
                    content = GetLEventsAlbumById();
                    break;
                case "SaveLEventsAlbum":       //保存新闻信息
                    content = SaveLEventsAlbum(pContext);
                    break;
                case "UploadVideo":
                    content = UploadVideo(pContext);
                    break;
                case "GetlNewsType":
                    content = GetlNewsType();
                    break;
            }
            pContext.Response.Write(content);
            pContext.Response.End();
        }

        #region GetLEventsAlbumData 查询视频列表

        /// <summary>
        /// 查询视频列表
        /// </summary>
        public string GetLEventsAlbumData()
        {
            var LEventsAlbumService = new LEventsAlbumBLL(CurrentUserInfo);
            string content = string.Empty;
            string pWhere = "";
            if (!string.IsNullOrEmpty(Request("Title")))
            {
                pWhere = " and  la.Title like '%" + Request("Title").Replace("'", "") + "%'";
            }
            int startRowIndex = Utils.GetIntVal(FormatParamValue(Request("page")));
            int pPageCount = 0;
            var data = LEventsAlbumService.PagedQueryNews(pWhere, PageSize, startRowIndex, out pPageCount);

            content = string.Format("{{\"totalCount\":{1},\"topics\":{0}}}",
                data.Tables[0].ToJSON(),
                pPageCount);
            return content;
        }

        #endregion

        #region LEventsAlbumDeleteData 视频删除
        /// <summary>
        /// 视频删除
        /// </summary>
        public string LEventsAlbumDeleteData()
        {
            string content = string.Empty;
            string error = "";
            var responseData = new ResponseData();
            string key = string.Empty;
            if (FormatParamValue(Request("ids")) != null && FormatParamValue(Request("ids")) != string.Empty)
            {
                key = FormatParamValue(Request("ids")).ToString().Trim();
            }

            if (key == null || key.Trim().Length == 0)
            {
                responseData.success = false;
                responseData.msg = "视频ID不能为空";
                return responseData.ToJSON();
            }
            string[] ids = key.Split(',');
            new LEventsAlbumBLL(CurrentUserInfo).Delete(ids);
            responseData.success = true;
            responseData.msg = error;
            content = responseData.ToJSON();
            return content;
        }

        #endregion

        #region GetLEventsAlbumById 根据ID获取视频信息
        /// <summary>
        /// 根据ID获取视频信息
        /// </summary>
        public string GetLEventsAlbumById()
        {
            var LEventsAlbumService = new LEventsAlbumBLL(CurrentUserInfo);
            string content = string.Empty;

            string key = string.Empty;
            if (FormatParamValue(Request("LEventsAlbumId")) != null && FormatParamValue(Request("LEventsAlbumId")) != string.Empty)
            {
                key = FormatParamValue(Request("LEventsAlbumId")).ToString().Trim();
            }

            var condition = new List<IWhereCondition>();
            if (!key.Equals(string.Empty))
            {
                condition.Add(new EqualsCondition() { FieldName = "AlbumId", Value = key });
            }

            LEventsAlbumEntity data = new LEventsAlbumEntity();
            var news = LEventsAlbumService.Query(condition.ToArray(), null);

            if (news != null && news.Length > 0)
            {
                data = news.ToList().FirstOrDefault();
            }

            var jsonData = new JsonData();
            jsonData.totalCount = (data == null) ? "0" : "1";
            jsonData.data = data;

            content = string.Format("{{\"totalCount\":{1},\"topics\":{0}}}",
                data.ToJSON(),
                (data == null) ? "0" : "1");

            return content;
        }

        #endregion

        #region SaveLEventsAlbum 保存视频信息
        /// <summary>
        /// 保存视频信息
        /// </summary>
        public string SaveLEventsAlbum(HttpContext pContext)
        {
            var LEventsAlbumService = new LEventsAlbumBLL(CurrentUserInfo);
            string content = string.Empty;
            string error = "";
            var responseData = new ResponseData();
            string key = string.Empty;
            string LEventsAlbumId = string.Empty;
            var LEventsAlbum = Request("LEventsAlbum");

            if (FormatParamValue(LEventsAlbum) != null && FormatParamValue(LEventsAlbum) != string.Empty)
            {
                key = FormatParamValue(LEventsAlbum).ToString().Trim();
            }
            if (FormatParamValue(Request("LEventsAlbumId")) != null && FormatParamValue(Request("LEventsAlbumId")) != string.Empty)
            {
                LEventsAlbumId = FormatParamValue(Request("LEventsAlbumId")).ToString().Trim();
            }

            var newsEntity = key.DeserializeJSONTo<LEventsAlbumEntity>();
            string host = ConfigurationManager.AppSettings["host"];
            if (LEventsAlbumId.Trim().Length == 0)
            {
                newsEntity.AlbumId = Utils.NewGuid();
                newsEntity.CustomerID = CurrentUserInfo.CurrentUser.customer_id;
                LEventsAlbumService.Create(newsEntity);
            }
            else
            {
                newsEntity.AlbumId = LEventsAlbumId;
                newsEntity.CustomerID = this.CurrentUserInfo.ClientID;
                LEventsAlbumService.Update(newsEntity);
            }
            responseData.success = true;
            responseData.msg = error;
            content = responseData.ToJSON();
            return content;
        }

        #endregion

        #region UploadVideo 上传视频数据
        /// <summary>
        /// 保存视频信息
        /// </summary>
        public string UploadVideo(HttpContext pContext)
        {
            string content = string.Empty;
            var responseData = new ResponseData();
            try
            {
                HttpPostedFile postedFile = pContext.Request.Files["file-path"];
                string pVoideUrl = "";
                string filePath = "/File/Video/MP4";
                if (postedFile != null && postedFile.FileName != null && postedFile.FileName != "")
                {
                    string suffixname = "";
                    if (postedFile.FileName != null)
                    {
                        suffixname = postedFile.FileName.Substring(postedFile.FileName.LastIndexOf(".")).ToLower();
                    }
                    if (suffixname == ".mp4") //文件格式判断
                    {

                        string savepath = HttpContext.Current.Server.MapPath(filePath);
                        if (
                            !Directory.Exists(savepath)) //创建目录
                        {
                            Directory.CreateDirectory(savepath);
                        }
                        string times = DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".mp4";
                        savepath = savepath + "/" + times;
                        pVoideUrl = filePath + "/" + times;
                        postedFile.SaveAs(savepath);
                        string host = ConfigurationManager.AppSettings["host"];
                        responseData.success = true;
                        responseData.msg = host + pVoideUrl;
                    }
                    else
                    {
                        responseData.success = false;
                        responseData.msg = "请上传MP4格式的视频";
                    }
                }
            }
            catch (Exception ex)
            {
                responseData.success = false;
                responseData.msg = ex.Message;
            }
            content = responseData.ToJSON();
            return content;
        }
        #endregion

        #region GetlNewsType
        /// <summary>
        ///获取类型
        /// </summary>
        /// <returns></returns>
        public string GetlNewsType()
        {
            LEventsAlbumTypeBLL server = new LEventsAlbumTypeBLL(this.CurrentUserInfo);
            LEventsAlbumTypeEntity[] entity = server.QueryByEntity(new LEventsAlbumTypeEntity { IsDelete = 0, CustomerId = this.CurrentUserInfo.ClientID }, null);
            if (entity!=null&&entity.Length>0)
            {
                return entity.ToJSON();
            }
            return "";
        }
        #endregion
    }
}