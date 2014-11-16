using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections.Specialized;
using JIT.CPOS.BS.BLL;
using JIT.Utility.DataAccess;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.BS.Web.Extension;
using JIT.Utility.ExtensionMethod;
using System.Data;
using System.Text;
using System.Collections;
using ICSharpCode.SharpZipLib.Zip;
using System.IO;

namespace JIT.CPOS.BS.Web.Module.VisitingData.Photo.Handler
{
    /// <summary>
    /// TaskDataPhotoHandler 的摘要说明
    /// </summary>
    public class TaskDataPhotoHandler : JIT.CPOS.BS.Web.PageBase.JITCPOSAjaxHandler
    {
        /// <summary>
        /// 页面入口
        /// </summary>
        /// <param name="pContext"></param>
        protected override void AjaxRequest(HttpContext pContext)
        {
            string res = "";
            switch (this.BTNCode)
            {
                case "search":
                    if (!string.IsNullOrEmpty(this.Method))
                    {
                        switch (this.Method)
                        {
                            case "GetParameterPhotoList":
                                res = GetParameterPhotoList(pContext.Request.Form);
                                break;
                            case "GetVisitingTaskPhoto":
                                res = GetVisitingTaskPhoto(pContext.Request.Form);
                                break;
                            case "GetTaskList":
                                res = GetTaskList();
                                break;
                            case "GetStepByTID":
                                res = GetStepByTID(pContext.Request.QueryString);
                                break;
                        }
                    }
                    break;
                case "export":
                    if (!string.IsNullOrEmpty(this.Method))
                    {
                        switch (this.Method)
                        {
                            case "ExportVisitingTaskPhoto":
                                ExportVisitingTaskPhoto(pContext.Request.QueryString);
                                break;
                        }
                    }
                    break;
            }
            pContext.Response.Write(res);
            pContext.Response.End();
        }

        #region GetParameterPhotoList
        private string GetParameterPhotoList(NameValueCollection rParams)
        {
            int pageSize = 50; 
            int pageIndex = 1;
            if (!string.IsNullOrEmpty(rParams["page"]))
            {
                pageIndex = rParams["page"].ToInt();
            }
            if (!string.IsNullOrEmpty(rParams["limit"]))
            {
                pageSize = rParams["limit"].ToInt();
            }
            #region 接受条件
            Dictionary<string, object> queryParams = new Dictionary<string, object>();
            if (!string.IsNullOrEmpty(rParams["ClientStructureID"]))
            {
                queryParams.Add("ClientStructureID", rParams["ClientStructureID"]);
            }
            if (!string.IsNullOrEmpty(rParams["ClientPositionID"]) && rParams["ClientPositionID"].ToInt() > 0)
            {
                queryParams.Add("ClientPositionID", rParams["ClientPositionID"]);
            }           
            if (!string.IsNullOrEmpty(rParams["VisitingTaskID"]) && rParams["VisitingTaskID"].ToInt() > 0)
            {
                queryParams.Add("VisitingTaskID", rParams["VisitingTaskID"]);
            }
            if (!string.IsNullOrEmpty(rParams["VisitingTaskStepID"]) && rParams["VisitingTaskStepID"].ToInt() > 0)
            {
                queryParams.Add("VisitingTaskStepID", rParams["VisitingTaskStepID"]);
            }
            if (!string.IsNullOrEmpty(rParams["DateFrom"]) && rParams["DateFrom"] != "null")
            {
                queryParams.Add("DateFrom", rParams["DateFrom"]);
            }
            if (!string.IsNullOrEmpty(rParams["DateTo"]) && rParams["DateTo"] != "null")
            {
                queryParams.Add("DateTo", rParams["DateTo"]);
            }
            if (!string.IsNullOrEmpty(rParams["ObjectName"]) && rParams["ObjectName"] != "null")
            {
                queryParams.Add("ObjectName", rParams["ObjectName"]);
            }
            if (!string.IsNullOrEmpty(rParams["POPName"]) && rParams["POPName"] != "null")
            {
                queryParams.Add("POPName", rParams["POPName"]);
            }
            if (!string.IsNullOrEmpty(rParams["ClientUserName"]) && rParams["ClientUserName"] != "null")
            {
                queryParams.Add("ClientUserName", rParams["ClientUserName"]);
            }
            #endregion
            PagedQueryResult<VisitingTaskPicturesViewEntity> result = new VisitingTaskDataBLL(CurrentUserInfo).GetVisitingTaskPictures(queryParams, null, pageSize, pageIndex);

            return string.Format("[{{\"totalCount\":{1},\"topics\":{0}}}]",
               result.Entities.ToJSON(),
                result.RowCount);
        }
        #endregion


        #region GetVisitingTaskPhotoList
        /// <summary>
        /// 公用方法,用于查询数据和导出方法
        /// </summary>
        /// <param name="rParams"></param>
        /// <param name="SumCount">总数</param>
        /// <returns></returns>
        private VisitingTaskPhotoShowViewEntity[] GetVisitingTaskPhotoList(NameValueCollection rParams, out int SumCount)
        {
            int pageSize = 50;
            int pageIndex = 1;
            #region 分页信息
            if (!string.IsNullOrEmpty(rParams["page"]))
            {
                pageIndex = rParams["page"].ToInt();
            }
            if (!string.IsNullOrEmpty(rParams["limit"]))
            {
                pageSize = rParams["limit"].ToInt();
            }
            #endregion
            #region 接受数据条件
            Dictionary<string, object> queryParams = new Dictionary<string, object>();
            if (!string.IsNullOrEmpty(rParams["ClientStructureID"]))
            {
                queryParams.Add("ClientStructureID", rParams["ClientStructureID"]);
            }
            if (!string.IsNullOrEmpty(rParams["ClientPositionID"]) && rParams["ClientPositionID"].ToInt() > 0)
            {
                queryParams.Add("ClientPositionID", rParams["ClientPositionID"]);
            }
            if (!string.IsNullOrEmpty(rParams["VisitingTaskID"]) && rParams["VisitingTaskID"] != "null")
            {
                queryParams.Add("VisitingTaskID", rParams["VisitingTaskID"]);
            }
            if (!string.IsNullOrEmpty(rParams["VisitingTaskStepID"]) && rParams["VisitingTaskStepID"] != "null")
            {
                queryParams.Add("VisitingTaskStepID", rParams["VisitingTaskStepID"]);
            }
            if (!string.IsNullOrEmpty(rParams["IsInOutPic"]) && rParams["IsInOutPic"] != "null")
            {
                queryParams.Add("IsInOutPic", rParams["IsInOutPic"]);
            }
            if (!string.IsNullOrEmpty(rParams["DateFrom"]) && rParams["DateFrom"] != "null")
            {
                queryParams.Add("DateFrom", rParams["DateFrom"]);
            }
            if (!string.IsNullOrEmpty(rParams["DateTo"]) && rParams["DateTo"] != "null")
            {
                queryParams.Add("DateTo", rParams["DateTo"]);
            }
            if (!string.IsNullOrEmpty(rParams["ObjectName"]) && rParams["ObjectName"] != "null")
            {
                queryParams.Add("ObjectName", rParams["ObjectName"]);
            }
            if (!string.IsNullOrEmpty(rParams["POPName"]) && rParams["POPName"] != "null")
            {
                queryParams.Add("POPName", rParams["POPName"]);
            }
            if (!string.IsNullOrEmpty(rParams["ClientUserName"]) && rParams["ClientUserName"] != "null")
            {
                queryParams.Add("ClientUserName", rParams["ClientUserName"]);
            }
            #endregion
            SumCount = 0;
            return new VisitingTaskDataBLL(CurrentUserInfo).GetVisitingTaskPhoto(queryParams, pageSize, pageIndex, out SumCount);
        }
        #endregion

        #region GetVisitingTaskPhoto
        private string GetVisitingTaskPhoto(NameValueCollection rParams)
        {
            int SumCount = 0;
            VisitingTaskPhotoShowViewEntity[] entityList = GetVisitingTaskPhotoList(rParams, out SumCount);  
            return string.Format("[{{\"totalCount\":{1},\"topics\":{0}}}]",
               entityList.ToJSON(),
               SumCount);
        }
        #endregion

        #region GetTaskList
        private string GetTaskList()
        {
            VisitingTaskViewEntity entity = new VisitingTaskViewEntity();
            int pageSize = 100000;
            int pageIndex = 1;
            int rowCount = 0;
            return new VisitingTaskBLL(CurrentUserInfo).GetList(entity, pageIndex, pageSize, out rowCount).ToJSON();
        }
        #endregion

        #region GetStepByTID
        private string GetStepByTID(NameValueCollection rParams)
        {
            VisitingTaskStepViewEntity entity = new VisitingTaskStepViewEntity();
            if (!string.IsNullOrEmpty(rParams["tid"]))
            {
                entity.VisitingTaskID = Guid.Parse(rParams["tid"]);
            }
            else {
                entity.VisitingTaskID = Guid.NewGuid();
            }
            int pageSize = 100000;
            int pageIndex = 1;
            int rowCount = 0;
            return new VisitingTaskStepBLL(CurrentUserInfo).GetList(entity, pageIndex, pageSize, out rowCount).ToJSON();
        }
        #endregion

        #region ExportVisitingTaskPhoto 导出照片功能      
        private void ExportVisitingTaskPhoto(NameValueCollection rParams)
        {
            string DateFrom = "";
            string DateTo = "";
            if (!string.IsNullOrEmpty(rParams["DateFrom"]) && rParams["DateFrom"] != "null")
            {
                DateFrom=rParams["DateFrom"].ToString();
            }
            if (!string.IsNullOrEmpty(rParams["DateTo"]) && rParams["DateTo"] != "null")
            {
                DateTo = rParams["DateTo"].ToString();
            }
            HttpContext context = System.Web.HttpContext.Current;
            List<VisitingTaskImageViewEntity> list = new List<VisitingTaskImageViewEntity>();
            int SumCount = 0;
            VisitingTaskPhotoShowViewEntity[] entityList = GetVisitingTaskPhotoList(rParams, out SumCount);
            string file_path = "";    //file_path示例：D:\mobile\picture\28346_1348630109476.jpg
            string file_name = "";    //file_name示例： /郑州市/终端3711968_河南康旭宠物医院/河南康旭宠物医院_28346_1348630109476.jpg
            string file_zipPath = ""; //file_zipPath示例：郑州市\\终端3711968_河南康旭宠物医院
            string ImgUrl = "";
            string TaskTitle = "";
            string POPTitle = "";
            string PhotoTitle = "";
            string StepTitle = "";
            string UserTitle = "";
            string DateTitle = "";
            string UserIDTitle = "";
            string PositinTitle = "";
            if (entityList != null && entityList.Length > 0)
            {

                for (int i = 0; i < entityList.Length; i++)
                {
                    ImgUrl = entityList[i].PhotoUrl.ToString();
                    TaskTitle = entityList[i].TaskName.ToString().Replace("/", "-");
                    POPTitle = entityList[i].POPName.ToString().Replace("/", "-");
                    PhotoTitle = entityList[i].PhotoName.ToString().Replace("/", "-");
                    StepTitle = entityList[i].StepName.ToString().Replace("/", "-");
                    UserTitle = entityList[i].ClientUserName.Replace("/", "-");
                    DateTitle = entityList[i].PhotoDateTime.ToString().Replace(":", "-").Replace("/", "-");
                    PositinTitle = entityList[i].PositionName.ToString().Replace("/", "-");
                    UserIDTitle = entityList[i].ClientUserID.ToString();
                    //  /File/MobileDevices/photo/客户ID/人员ID/1369990987854.jpg
                    file_path = context.Server.MapPath("/File/MobileDevices/photo/" + this.CurrentUserInfo.ClientID + "/" + UserIDTitle + "/" + ImgUrl);
                    // /任务名称/终端名称/步骤_参数对象_人员名称_时间_图片名称
                    file_name = "/Photo/" + POPTitle + "_" + TaskTitle + "_" + StepTitle + "_" + PositinTitle + "_" + UserTitle + "_" + PhotoTitle + "_" + DateTitle + "_" + ImgUrl;
                    // 文件夹 Photo\\PhotoName
                    file_zipPath = "Photo";
                    if (File.Exists(file_path))
                    {
                        list.Add(new VisitingTaskImageViewEntity(file_path, file_name, file_zipPath));
                    }
                }
                CreateZipAndResponse(list, context.Response, DateFrom,DateTo);
            }
        }

        /// <summary>
        /// 输出zip
        /// </summary>
        /// <param name="list"></param>
        /// <param name="response"></param>
        private void CreateZipAndResponse(List<VisitingTaskImageViewEntity> list, HttpResponse response, string DateFrom, string DateTo)
        {
            string FileName = "";
            FileName = DateFrom + "-" + DateTo + "_Photo";
            if (list != null && list.Count > 0)
            {
                ArrayList nStr = new ArrayList();
                for (int j = 0; j < list.Count; j++)
                {
                    if (!nStr.Contains(list[j].File_ZipPath))
                    {
                        nStr.Add(list[j].File_ZipPath);
                    }
                }
                using (MemoryStream stream = new MemoryStream())
                {
                    using (ZipFile zip = new ZipFile(stream))
                    {
                        zip.BeginUpdate();
                        //
                        for (int a = 0; a < nStr.Count; a++)
                        {
                            zip.AddDirectory(nStr[a].ToString());
                        }

                        for (int i = 0; i < list.Count; i++)
                        {
                            zip.Add(list[i].FilePath, list[i].FileName);
                        }

                        zip.CommitUpdate();
                        response.Clear();
                        response.ContentType = "application/octet-stream";
                        response.AddHeader("Content-Disposition", "attachment; filename=" + FileName + ".zip");

                        stream.WriteTo(response.OutputStream);
                        stream.Close();
                        response.End();
                    }
                }
            }
        }
        #endregion
    }
}