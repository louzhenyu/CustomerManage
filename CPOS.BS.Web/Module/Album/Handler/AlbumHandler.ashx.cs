using System.Collections.Generic;
using System.Web;
using System.Linq;
using JIT.Utility.ExtensionMethod;
using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Entity;
using JIT.Utility.DataAccess.Query;
using System;
using System.Text;
using System.Data;
using JIT.Utility.Log;
using System.Configuration;
using JIT.CPOS.Common;

namespace JIT.CPOS.BS.Web.Module.Album.Handler
{
    /// <summary>
    /// AlbumHandler
    /// </summary>
    public class AlbumHandler : JIT.CPOS.BS.Web.PageBase.JITCPOSAjaxHandler
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
                case "album_query":         //相册查询
                    content = GetAlbumData();
                    break;
                case "album_delete":        //相册删除
                    content = AlbumDelete();
                    break;
                case "get_album_by_id":     //根据ID获取相册信息
                    content = GetAlbumById();
                    break;
                case "album_save":          //保存相册信息
                    content = AlbumSave();
                    break;
                case "album_module_query":  //绑定模块查询
                    content = GetAlbumModuleData();
                    break;
                case "album_image_query":   //查询相片列表
                    content = GetAlbumImageList();
                    break;
                case "get_album_image_by_id":  //根据ID获取相片信息
                    content = GetAlbumImageById();
                    break;
                case "album_image_delete":   //删除活动图片
                    content = DeleteAlbumImage();
                    break;
                case "album_image_save":   //保存活动图片
                    content = SaveAlbumImage();
                    break;
                case "getAlbumType":
                    content = GetAlbumType();
                    break;

            }
            pContext.Response.Write(content);
            pContext.Response.End();
        }

        #region GetAlbumData 相册查询

        /// <summary>
        /// 相册查询
        /// </summary>
        public string GetAlbumData()
        {
            string content = string.Empty;

            var albumService = new LEventsAlbumBLL(this.CurrentUserInfo);
            var form = Request("form").DeserializeJSONTo<LEventsAlbumQueryEntity>();

            int pageIndex = Utils.GetIntVal(FormatParamValue(Request("page"))) - 1;

            LEventsAlbumEntity queryEntity = new LEventsAlbumEntity();
            queryEntity.Title = FormatParamValue(form.Title);
            queryEntity.Type = FormatParamValue(form.AlbumType);
            queryEntity.ModuleType = FormatParamValue(form.AlbumModuleType);
            queryEntity.ModuleName = FormatParamValue(form.ModuleTypeName);

            var data = albumService.GetAlbumList(queryEntity, pageIndex, PageSize);
            var dataTotalCount = albumService.GetAlbumCount(queryEntity);

            content = string.Format("{{\"totalCount\":{1},\"topics\":{0}}}",
                data.ToJSON(),
                dataTotalCount);

            return content;
        }

        public class LEventsAlbumQueryEntity
        {
            public string Title;
            public string AlbumType;
            public string AlbumModuleType;
            public string ModuleTypeName;
        }

        #endregion

        #region AlbumDelete 相册删除

        /// <summary>
        /// 相册删除
        /// </summary>
        public string AlbumDelete()
        {
            string content = string.Empty;

            var error = string.Empty;
            var responseData = new ResponseData();

            string key = string.Empty;
            if (FormatParamValue(Request("ids")) != null && FormatParamValue(Request("ids")) != string.Empty)
            {
                key = FormatParamValue(Request("ids")).ToString().Trim();
            }


            if (key == null || key.Trim().Length == 0)
            {
                responseData.success = false;
                responseData.msg = "相册ID不能为空";
                return responseData.ToJSON();
            }
            string[] ids = key.Split(',');
            new LEventsAlbumBLL(this.CurrentUserInfo).Delete(ids);
            foreach (var item in ids)
            {
                var entity = new LEventsAlbumPhotoBLL(this.CurrentUserInfo).QueryByEntity(new LEventsAlbumPhotoEntity { AlbumId = item }, null).FirstOrDefault();
                //new LEventsAlbumPhotoBLL(this.CurrentUserInfo).Delete(new object[1]{entity.PhotoId});
                if (entity!=null)
                { 
                    new LEventsAlbumPhotoBLL(this.CurrentUserInfo).DeleAlbumPhoto(entity.AlbumId);
                }
            }

          
            responseData.success = true;
            responseData.msg = error;

            content = responseData.ToJSON();

            return content;
        }

        #endregion

        #region GetAlbumById 根据ID获取相册信息

        /// <summary>
        /// 根据ID获取相册信息
        /// </summary>
        public string GetAlbumById()
        {
            string content = string.Empty;

            var albumService = new LEventsAlbumBLL(this.CurrentUserInfo);
            string albumId = FormatParamValue(Request("AlbumId"));

            var condition = new List<IWhereCondition>();
            if (!albumId.Equals(string.Empty))
            {
                condition.Add(new EqualsCondition() { FieldName = "AlbumId", Value = albumId });
            }

            var data = albumService.Query(condition.ToArray(), null).ToList().FirstOrDefault();

            var jsonData = new JsonData();
            jsonData.totalCount = (data == null) ? "0" : "1";
            jsonData.data = data;

            content = string.Format("{{\"totalCount\":{1},\"topics\":{0}}}",
                data.ToJSON(),
                (data == null) ? "0" : "1");

            return content;
        }

        #endregion

        #region AlbumSave 保存相册信息

        /// <summary>
        /// 保存相册信息
        /// </summary>
        public string AlbumSave()
        {
            string content = string.Empty;

            var albumService = new LEventsAlbumBLL(this.CurrentUserInfo);
            var error = string.Empty;
            var responseData = new ResponseData();

            var albumId = FormatParamValue(Request("AlbumId"));
            var album = FormatParamValue(Request("album"));

            var albumEntity = album.DeserializeJSONTo<LEventsAlbumEntity>();

            #region 验证不能为空

            if (string.IsNullOrEmpty(albumEntity.Type))
            {
                responseData.success = false;
                responseData.msg = "相册类型不能为空";
                return responseData.ToJSON();
            }
            if (string.IsNullOrEmpty(albumEntity.Title))
            {
                responseData.success = false;
                responseData.msg = "图片标题不能为空";
                return responseData.ToJSON();
            }
            if (string.IsNullOrEmpty(albumEntity.ImageUrl))
            {
                responseData.success = false;
                responseData.msg = "封面图片不能为空";
                return responseData.ToJSON();
            }
            if (string.IsNullOrEmpty(albumEntity.ModuleId) ||
                string.IsNullOrEmpty(albumEntity.ModuleName) ||
                string.IsNullOrEmpty(albumEntity.ModuleType))
            {
                responseData.success = false;
                responseData.msg = "模块不能为空";
                return responseData.ToJSON();
            }

            #endregion

            try
            {
                if (string.IsNullOrEmpty(albumId))
                {
                    albumEntity.AlbumId = Utils.NewGuid();
                    albumService.Create(albumEntity);
                }
                else
                {
                    albumEntity.AlbumId = albumId;
                    albumService.Update(albumEntity, false);
                }

                responseData.success = true;
                responseData.msg = error;
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

        #region GetAlbumModuleData 绑定模块查询

        /// <summary>
        /// 绑定模块查询
        /// </summary>
        public string GetAlbumModuleData()
        {
            string content = string.Empty;

            var albumService = new LEventsAlbumBLL(this.CurrentUserInfo);

            var moduleType = FormatParamValue(Request("ModuleType"));
            var moduleName = FormatParamValue(Request("ModuleName"));
            int pageIndex = Utils.GetIntVal(FormatParamValue(Request("page"))) - 1;

            moduleType = string.IsNullOrEmpty(moduleType) ? "1" : moduleType;
            var ds = albumService.GetAlbumModuleList(moduleType, moduleName, pageIndex, PageSize);

            var data = new List<AlbumModuleEntity>();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                data = DataTableToObject.ConvertToList<AlbumModuleEntity>(ds.Tables[0]);
            }

            var dataTotalCount = albumService.GetAlbumModuleCount(moduleType, moduleName);

            content = string.Format("{{\"totalCount\":{1},\"topics\":{0}}}",
                data.ToJSON(),
                dataTotalCount);

            return content;
        }

        public class AlbumModuleEntity
        {
            public string ID { get; set; }
            public string Title { get; set; }
            public DateTime? CreateTime { get; set; }
        }

        #endregion

        #region GetAlbumImageList 查询相片列表

        /// <summary>
        /// 查询相片列表
        /// </summary>
        public string GetAlbumImageList()
        {
            var albumBLL = new LEventsAlbumBLL(this.CurrentUserInfo);

            string content = string.Empty;

            int pageIndex = Utils.GetIntVal(FormatParamValue(Request("page"))) - 1;
            var albumId = FormatParamValue(Request("AlbumId"));

            var data = albumBLL.GetAlbumImageList(albumId, pageIndex, PageSize);
            var dataTotalCount = albumBLL.GetAlbumImageCount(albumId);

            content = string.Format("{{\"totalCount\":{1},\"topics\":{0}}}",
                data.ToJSON(),
                dataTotalCount);

            return content;
        }

        #endregion

        #region DeleteAlbumImage 删除活动图片

        /// <summary>
        /// 删除活动图片
        /// </summary>
        public string DeleteAlbumImage()
        {
            string content = string.Empty;

            string error = string.Empty;
            var responseData = new ResponseData();

            string key = string.Empty;
            if (FormatParamValue(Request("ids")) != null && FormatParamValue(Request("ids")) != string.Empty)
            {
                key = FormatParamValue(Request("ids")).ToString().Trim();
            }

            if (key == null || key.Trim().Length == 0)
            {
                responseData.success = false;
                responseData.msg = "相片ID不能为空";
                return responseData.ToJSON();
            }

            string[] ids = key.Split(',');
            new LEventsAlbumPhotoBLL(this.CurrentUserInfo).Delete(ids);

            responseData.success = true;
            responseData.msg = error;

            content = responseData.ToJSON();
            return content;
        }

        #endregion

        #region GetAlbumImageById 根据报名ID获取相片信息

        /// <summary>
        /// 根据报名ID获取相片信息
        /// </summary>
        public string GetAlbumImageById()
        {
            string content = string.Empty;

            var photoService = new LEventsAlbumPhotoBLL(this.CurrentUserInfo);
            string photoId = FormatParamValue(Request("PhotoId"));

            var condition = new List<IWhereCondition>();
            if (!photoId.Equals(string.Empty))
            {
                condition.Add(new EqualsCondition() { FieldName = "PhotoId", Value = photoId });
            }

            var data = photoService.Query(condition.ToArray(), null).ToList().FirstOrDefault();

            var jsonData = new JsonData();
            jsonData.totalCount = (data == null) ? "0" : "1";
            jsonData.data = data;

            content = string.Format("{{\"totalCount\":{1},\"topics\":{0}}}",
                data.ToJSON(),
                (data == null) ? "0" : "1");

            return content;
        }

        #endregion

        #region SaveAlbumImage 保存活动图片

        /// <summary>
        /// 保存活动图片
        /// </summary>
        public string SaveAlbumImage()
        {
            string content = string.Empty;

            var imageService = new LEventsAlbumPhotoBLL(this.CurrentUserInfo);
            string error = string.Empty;
            var responseData = new ResponseData();

            var photo = FormatParamValue(Request("image"));
            var photoId = FormatParamValue(Request("PhotoId"));

            var photoEntity = photo.DeserializeJSONTo<LEventsAlbumPhotoEntity>();

            #region 验证不能为空

            if (photoEntity.SortOrder == null)
            {
                responseData.success = false;
                responseData.msg = "序号不能为空";
                return responseData.ToJSON();
            }
            if (string.IsNullOrEmpty(photoEntity.LinkUrl))
            {
                responseData.success = false;
                responseData.msg = "图片不能为空";
                return responseData.ToJSON();
            }
            if (string.IsNullOrEmpty(photoEntity.Title))
            {
                responseData.success = false;
                responseData.msg = "标题不能为空";
                return responseData.ToJSON();
            }

            #endregion

            try
            {
                if (string.IsNullOrEmpty(photoId))
                {
                    //新增
                    photoEntity.PhotoId = Utils.NewGuid();
                    photoEntity.ReaderCount = 0;
                    imageService.Create(photoEntity);
                }
                else
                {
                    //修改
                    photoEntity.PhotoId = photoId;
                    imageService.Update(photoEntity, false);
                }

                responseData.success = true;
                responseData.msg = error;
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

        #region MyRegion
        public string GetAlbumType()
        {
            LEventsAlbumTypeBLL bll = new LEventsAlbumTypeBLL(this.CurrentUserInfo);
            OrderBy[] orderby = new OrderBy[1];
            orderby[0] = new OrderBy { FieldName = "DisplayIndex", Direction = OrderByDirections.Asc };
            LEventsAlbumTypeEntity[] entitys = bll.QueryByEntity(new LEventsAlbumTypeEntity { IsDelete = 0, CustomerId = this.CurrentUserInfo.ClientID },
                  orderby);
            return entitys.ToJSON();


        }
        #endregion
    }
}