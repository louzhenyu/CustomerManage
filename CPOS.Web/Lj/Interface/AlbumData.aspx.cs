using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Entity;
using JIT.Utility.DataAccess.Query;
using JIT.Utility.ExtensionMethod;
using JIT.Utility.Log;

namespace JIT.CPOS.Web.Lj.Interface
{
    public partial class AlbumData : System.Web.UI.Page
    {
        string customerId = "f6a7da3d28f74f2abedfc3ea0cf65c01";
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Clear();
            string content = string.Empty;
            try
            {
                string dataType = Request["action"].ToString().Trim();
                JIT.CPOS.Web.Module.Log.InterfaceWebLog.Logger.Log(Context, Request, dataType);

                switch (dataType)
                {
                    case "getEventAlbum":   //获取活动相册信息
                        content = GetEventAlbum();
                        break;
                    case "getPhoto":        //获取照片信息
                        content = GetPhoto();
                        break;
                    case "getVideo":        //获取视频信息
                        content = GetVideo();
                        break;
                    case "setReaderCount":  //添加阅读数量
                        content = SetReaderCount();
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                content = ex.Message;
            }
            Response.ContentEncoding = Encoding.UTF8;
            Response.Write(content);
            Response.End();
        }

        #region GetEventAlbum 获取活动相册信息
        /// <summary>
        /// 获取活动相册信息
        /// </summary>
        public string GetEventAlbum()
        {
            string content = string.Empty;

            var respData = new GetEventAlbumRespData();
            try
            {
                string reqContent = Request["ReqContent"];
                var reqObj = reqContent.DeserializeJSONTo<GetEventAlbumReqData>();

                string eventId = reqObj.special.eventId;    //活动ID
                string moduleType = (reqObj.special.eventType ?? 1).ToString();    //1: 活动

                if (string.IsNullOrEmpty(eventId))
                {
                    throw new Exception("活动ID不能为空");
                    //eventId = "3DD35B9A122F41C8A0E5D5B78D72CE65";
                }

                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format("GetEventAlbum: {0}", reqContent)
                });

                //判断客户ID是否传递
                if (!string.IsNullOrEmpty(reqObj.common.customerId))
                {
                    customerId = reqObj.common.customerId;
                }
                var loggingSessionInfo = Default.GetBSLoggingSession(customerId, "1");

                respData.content = new GetEventAlbumRespContentData();
                respData.content.albumList = new List<AlbumEntity>();

                var eventList = new LEventsBLL(loggingSessionInfo).QueryByEntity(new LEventsEntity { EventID = eventId }, null);

                if (eventList != null && eventList.Length > 0)
                {
                    var eventEntity = eventList.FirstOrDefault();

                    respData.content.title = eventEntity.Title;
                    respData.content.description = eventEntity.Description;
                    respData.content.imageUrl = eventEntity.ImageURL;

                    #region 相册信息

                    LEventsAlbumBLL albumService = new LEventsAlbumBLL(loggingSessionInfo);

                    var albumList = albumService.QueryByEntity(new LEventsAlbumEntity { ModuleId = eventId, ModuleType = moduleType },
                            new OrderBy[] { new OrderBy { FieldName = " SortOrder ", Direction = OrderByDirections.Asc } });

                    if (albumList != null && albumList.Length > 0)
                    {
                        foreach (var item in albumList)
                        {
                            var entity = new AlbumEntity()
                            {
                                albumId = item.AlbumId,
                                albumTitle = item.Title,
                                albumType = item.Type,
                                imageUrl = item.ImageUrl,
                                count=item.Count
                            };

                            respData.content.albumList.Add(entity);
                        }
                    }

                    #endregion
                }
            }
            catch (Exception ex)
            {
                respData.code = "103";
                respData.description = "数据库操作错误";
                //respData.exception = ex.ToString();
            }
            content = respData.ToJSON();
            return content;
        }

        public class GetEventAlbumRespData : Default.LowerRespData
        {
            public GetEventAlbumRespContentData content;
        }
        public class GetEventAlbumRespContentData
        {
            public string title { get; set; }        //标题
            public string description { get; set; }   //描述
            public string imageUrl { get; set; }     //图片链接地址
            public IList<AlbumEntity> albumList { get; set; }     //相册集合
        }
        public class AlbumEntity
        {
            public string albumId { get; set; }      //相册ID
            public string albumTitle { get; set; }    //相册标题
            public string albumType { get; set; }     //相册类型   0： 相片  1： 视频
            public string imageUrl { get; set; }      //相册图片链接地址
            public int count { get; set; }
        }
        public class GetEventAlbumReqData : Default.ReqData
        {
            public GetEventAlbumReqSpecialData special;
        }
        public class GetEventAlbumReqSpecialData
        {
            public string eventId { get; set; }      //事件标识
            public int? eventType { get; set; }      //事件类型
        }

        #endregion

        #region GetPhoto 获取照片信息
        /// <summary>
        /// 获取照片信息
        /// </summary>
        public string GetPhoto()
        {
            string content = string.Empty;

            var respData = new GetPhotoRespData();
            try
            {
                string reqContent = Request["ReqContent"];
                var reqObj = reqContent.DeserializeJSONTo<GetPhotoReqData>();

                string albumId = reqObj.special.albumId;    //活动ID

                if (string.IsNullOrEmpty(albumId))
                {
                    albumId = "3DD35B9A122F41C8A0E5D5B78D72CE65";
                }

                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format("GetPhoto: {0}", reqContent)
                });

                //判断客户ID是否传递
                if (!string.IsNullOrEmpty(reqObj.common.customerId))
                {
                    customerId = reqObj.common.customerId;
                }
                var loggingSessionInfo = Default.GetBSLoggingSession(customerId, "1");

                var bll = new LEventsAlbumPhotoBLL(loggingSessionInfo);

                respData.content = bll.GetByAlbumID(reqObj.special.albumId);
            }
            catch (Exception ex)
            {
                respData.code = "103";
                respData.description = "数据库操作错误";
                //respData.exception = ex.ToString();
            }
            content = respData.ToJSON();
            return content;
        }

        public class GetPhotoRespData : Default.LowerRespData
        {
            public object content;
        }
        public class GetPhotoRespContentData
        {
            public IList<PhotoEntity> photoList;    //照片列表
        }
        public class PhotoEntity
        {
            public string photoId;      //照片ID
            public string title;        //标题
            public string readerCount;  //阅读数量
            public string linkUrl;      //链接地址
            public string displayIndex; //序号
        }
        public class GetPhotoReqData : Default.ReqData
        {
            public GetPhotoReqSpecialData special;
        }
        public class GetPhotoReqSpecialData
        {
            public string albumId;      //相册ID
        }

        #endregion

        #region GetVideo 获取视频信息
        /// <summary>
        /// 获取视频信息
        /// </summary>
        public string GetVideo()
        {
            string content = string.Empty;

            var respData = new GetVideoRespData();
            try
            {
                string reqContent = Request["ReqContent"];
                var reqObj = reqContent.DeserializeJSONTo<GetVideoReqData>();

                string albumId = reqObj.special.albumId;    //活动ID

                if (string.IsNullOrEmpty(albumId))
                {
                    albumId = "3DD35B9A122F41C8A0E5D5B78D72CE65";
                }

                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format("GetVideo: {0}", reqContent)
                });

                //判断客户ID是否传递
                if (!string.IsNullOrEmpty(reqObj.common.customerId))
                {
                    customerId = reqObj.common.customerId;
                }
                var loggingSessionInfo = Default.GetBSLoggingSession(customerId, "1");

                var albumList = new LEventsAlbumBLL(loggingSessionInfo).QueryByEntity(new LEventsAlbumEntity { AlbumId = albumId }, null);

                if (albumList != null && albumList.Length > 0)
                {
                    respData.content = albumList.FirstOrDefault().Description;
                }
            }
            catch (Exception ex)
            {
                respData.code = "103";
                respData.description = "数据库操作错误";
                //respData.exception = ex.ToString();
            }
            content = respData.ToJSON();
            return content;
        }

        public class GetVideoRespData : Default.LowerRespData
        {
            public string content;
        }
        public class GetVideoReqData : Default.ReqData
        {
            public GetVideoReqSpecialData special;
        }
        public class GetVideoReqSpecialData
        {
            public string albumId;      //相册ID
        }

        #endregion

        #region SetReaderCount 添加阅读数量
        /// <summary>
        /// 添加阅读数量
        /// </summary>
        public string SetReaderCount()
        {
            string content = string.Empty;

            var respData = new SetReaderCountRespData();
            try
            {
                string reqContent = Request["ReqContent"];
                var reqObj = reqContent.DeserializeJSONTo<SetReaderCountReqData>();

                string photoId = reqObj.special.photoId;    //活动ID

                if (string.IsNullOrEmpty(photoId))
                {
                    photoId = "3DD35B9A122F41C8A0E5D5B78D72CE65";
                }

                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format("SetReaderCount: {0}", reqContent)
                });

                //判断客户ID是否传递
                if (!string.IsNullOrEmpty(reqObj.common.customerId))
                {
                    customerId = reqObj.common.customerId;
                }
                var loggingSessionInfo = Default.GetBSLoggingSession(customerId, "1");

                var photoService = new LEventsAlbumPhotoBLL(loggingSessionInfo);
                var photoList = photoService.QueryByEntity(new LEventsAlbumPhotoEntity { PhotoId = photoId }, null);

                if (photoList != null && photoList.Length > 0)
                {
                    var photoEntity = photoList.FirstOrDefault();

                    photoEntity.ReaderCount += 1;

                    photoService.Update(photoEntity);
                }
            }
            catch (Exception ex)
            {
                respData.code = "103";
                respData.description = "数据库操作错误";
                //respData.exception = ex.ToString();
            }
            content = respData.ToJSON();
            return content;
        }

        public class SetReaderCountRespData : Default.LowerRespData
        {

        }
        public class SetReaderCountReqData : Default.ReqData
        {
            public SetReaderCountReqSpecialData special;
        }
        public class SetReaderCountReqSpecialData
        {
            public string photoId;  //照片ID
        }

        #endregion
    }
}