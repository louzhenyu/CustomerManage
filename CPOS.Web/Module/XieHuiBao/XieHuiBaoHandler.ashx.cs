using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using JIT.Utility.Log;
using JIT.Utility.ExtensionMethod;
using JIT.Utility.DataAccess.Query;

using JIT.CPOS.Web;
using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Entity;

using JIT.CPOS.Web.Base.PageBase;
using JIT.CPOS.Web.SendSMSService;
using System.Data;
using System.IO;
using System.Configuration;
using JIT.CPOS.BS.Entity.Interface;
using JIT.CPOS.BS.BLL.Module.NoticeEmail;
using JIT.Utility.Notification;

namespace JIT.CPOS.Web.Module.XieHuiBao
{
    /// <summary>
    /// XieHuiBaoHandler 的摘要说明
    /// </summary>
    public class XieHuiBaoHandler : JITAjaxHandler
    {

        string customerId = "75a232c2cf064b45b1b6393823d2431e";
        #region CEIBS
        #region GetAlbumList
        /// <summary>
        /// 获取视频列表
        /// </summary>
        /// <returns></returns>
        public string GetAlbumList()
        {
            string content = string.Empty;
            var respData = new albumRespData();
            try
            {
                //接收参数
                string reqContent = HttpContext.Current.Request["ReqContent"];
                var reqObj = reqContent.DeserializeJSONTo<albumReqData>();
                reqObj = reqObj == null ? new albumReqData() : reqObj;

                //获取客户ID
                if (!string.IsNullOrEmpty(reqObj.common.customerId))
                {
                    customerId = reqObj.common.customerId;
                }

                //初始化返回对象
                respData.content = new albumRespContentData();
                respData.content.videoList = new List<albumRespContentDataItem>();

                //用户登录信息
                var loggingSessionInfo = Default.GetBSLoggingSession(customerId, "1");

                XieHuiBaoBLL bll = new XieHuiBaoBLL(loggingSessionInfo, "vip");

                int pageSize = 15, pageIndex = 1;
                if (!string.IsNullOrEmpty(reqObj.special.pageSize))
                {
                    pageSize = int.Parse(reqObj.special.pageSize);
                }
                if (!string.IsNullOrEmpty(reqObj.special.pageIndex))
                {
                    pageIndex = int.Parse(reqObj.special.pageIndex);
                }
                int rowCount = 0;
                var orderInfo = bll.GetAlbumList(reqObj.common.userId, pageSize, pageIndex, out rowCount);
                if (orderInfo != null && orderInfo.Count > 0)
                {
                    List<albumRespContentDataItem> list = new List<albumRespContentDataItem>();
                    foreach (var item in orderInfo)
                    {
                        albumRespContentDataItem info = new albumRespContentDataItem();
                        info.albumId = item.AlbumId;
                        info.moduleId = item.ModuleId;
                        info.moduleType = item.ModuleType;
                        info.moduleName = item.ModuleName;
                        info.type = item.Type;
                        info.imageUrl = item.ImageUrl;
                        info.title = item.Title;
                        info.description = item.Description;
                        info.sortOrder = item.SortOrder;
                        info.praiseCount = item.PraiseCount;
                        info.browseCount = item.BrowseCount;
                        info.keepCount = item.KeepCount;
                        info.isKeep = item.isKeep;
                        info.isPraise = item.isPraise;

                        list.Add(info);
                    }
                    respData.content.videoList = list;
                    respData.content.rowCount = rowCount;

                    pageSize = 15;
                    if (!string.IsNullOrEmpty(reqObj.special.pageSize))
                    {
                        int.TryParse(reqObj.special.pageSize, out pageSize);
                    }
                    respData.content.pageCount = (int)(Math.Ceiling((decimal)rowCount / pageSize));
                }
                else
                {
                    respData.code = "101";
                    respData.description = "没有获取到数据";
                }
            }
            catch (Exception ex)
            {
                respData.code = "103";
                respData.description = "数据库操作错误";
                respData.exception = ex.ToString();
            }
            content = respData.ToJSON();
            return content;
        }
        #endregion

        #region AddEventStats
        /// <summary>
        /// 添加视频、活动、资讯操作数据
        /// </summary>
        /// <returns></returns>
        public string AddEventStats()
        {
            string content = string.Empty;
            var respData = new albumRespData();
            try
            {
                //接收参数
                string reqContent = HttpContext.Current.Request["ReqContent"];
                var reqObj = reqContent.DeserializeJSONTo<albumReqData>();
                reqObj = reqObj == null ? new albumReqData() : reqObj;

                //获取客户ID
                if (!string.IsNullOrEmpty(reqObj.common.customerId))
                {
                    customerId = reqObj.common.customerId;
                }
                //数据验证
                //if (reqObj.common.userId == null || reqObj.common.userId.Equals(""))
                //{
                //    respData.code = "2201";
                //    respData.description = "用户不能为空";
                //    return respData.ToJSON().ToString();
                //}
                //数据类型 1.咨询 2.视频 3.活动
                if (reqObj.special.newsType == null || reqObj.special.newsType.Equals(""))
                {
                    respData.code = "2202";
                    respData.description = "数据类型不能为空";
                    return respData.ToJSON().ToString();
                }
                //操作类型 1:BrowseCount(浏览数量) 2:PraiseCount(赞的数量) 3:KeepCount(收藏数量)
                if (reqObj.special.countType == null || reqObj.special.countType.Equals(""))
                {
                    respData.code = "2203";
                    respData.description = "操作类型不能为空";
                    return respData.ToJSON().ToString();
                }

                if (reqObj.special.id == null || reqObj.special.id.Equals(""))
                {
                    respData.code = "2203";
                    respData.description = "ID不能为空";
                    return respData.ToJSON().ToString();
                }

                //初始化返回对象
                respData.content = new albumRespContentData();

                //用户登录信息
                var loggingSessionInfo = Default.GetBSLoggingSession(customerId, "1");
                XieHuiBaoBLL itemService = new XieHuiBaoBLL(loggingSessionInfo, "vip");
                //OnlineShoppingItemBLL itemService = new OnlineShoppingItemBLL(loggingSessionInfo);

                int result = itemService.AddEventStats(reqObj.special.id, reqObj.common.userId, reqObj.special.countType, reqObj.special.newsType);
                if (result > 0)
                {
                    respData.code = "200";
                    respData.description = "操作成功";
                    respData.content = null;
                    respData.PraiseNum = itemService.GetMostDetail(reqObj.special.id).ToString();
                }
                else
                {
                    respData.code = "100";
                    respData.description = "操作失败";
                    respData.content = null;
                }
            }
            catch (Exception ex)
            {
                respData.code = "103";
                respData.description = "数据库操作错误";
                respData.exception = ex.ToString();
            }
            content = respData.ToJSON();
            return content;
        }
        #endregion

        #region GetEventStats
        /// <summary>
        /// 获取最受关注
        /// </summary>
        /// <returns></returns>
        public string GetEventStats()
        {
            string content = string.Empty;
            var respData = new concernRespData();
            try
            {
                //接收参数
                string reqContent = HttpContext.Current.Request["ReqContent"];
                var reqObj = reqContent.DeserializeJSONTo<concernReqData>();
                reqObj = reqObj == null ? new concernReqData() : reqObj;

                //获取客户ID
                if (!string.IsNullOrEmpty(reqObj.common.customerId))
                {
                    customerId = reqObj.common.customerId;
                }

                //初始化返回对象
                respData.content = new concernRespContentData();
                respData.content.concernList = new List<concernRespContentDataItem>();

                //用户登录信息
                var loggingSessionInfo = Default.GetBSLoggingSession(customerId, "1");
                if (!string.IsNullOrEmpty(reqObj.common.userId))
                {
                    loggingSessionInfo.UserID = reqObj.common.userId;
                }
                XieHuiBaoBLL itemService = new XieHuiBaoBLL(loggingSessionInfo, "vip");
                if (string.IsNullOrEmpty(reqObj.special.pageIndex))
                {
                    reqObj.special.pageIndex = "1";
                }
                var concernInfo = itemService.GetEventStats(int.Parse(reqObj.special.pageSize), int.Parse(reqObj.special.pageIndex) - 1);// itemService.GetMostConcern();
                if (concernInfo != null && concernInfo.Tables.Count > 0 && concernInfo.Tables[0].Rows.Count > 0)
                {
                    List<concernRespContentDataItem> list = new List<concernRespContentDataItem>();
                    for (int i = 0; i < concernInfo.Tables[0].Rows.Count; i++)
                    {
                        concernRespContentDataItem info = new concernRespContentDataItem();
                        info.newsID = concernInfo.Tables[0].Rows[i]["NewsID"].ToString();
                        info.eventStatsID = concernInfo.Tables[0].Rows[i]["eventStatsID"].ToString();
                        info.newsType = int.Parse(concernInfo.Tables[0].Rows[i]["NewsType"].ToString());
                        info.newsTypeText = concernInfo.Tables[0].Rows[i]["NewsTypeText"].ToString();
                        info.title = concernInfo.Tables[0].Rows[i]["Title"].ToString();
                        info.imageUrl = concernInfo.Tables[0].Rows[i]["ImageUrl"].ToString();
                        //info.description = concernInfo.Tables[0].Rows[i]["Description"].ToString();
                        info.createTime = concernInfo.Tables[0].Rows[i]["CreateTime"].ToString();
                        info.agoTime = concernInfo.Tables[0].Rows[i]["AgoTime"].ToString();
                        info.allCount = int.Parse(concernInfo.Tables[0].Rows[i]["AllCount"].ToString());
                        info.praiseCount = int.Parse(concernInfo.Tables[0].Rows[i]["PraiseCount"].ToString());
                        info.browseCount = int.Parse(concernInfo.Tables[0].Rows[i]["BrowseCount"].ToString());
                        info.keepCount = int.Parse(concernInfo.Tables[0].Rows[i]["KeepCount"].ToString());
                        info.videoUrl = concernInfo.Tables[0].Rows[i]["VideoUrl"].ToString();
                        info.intro = concernInfo.Tables[0].Rows[i]["Intro"].ToString();
                        info.shareCount = int.Parse(concernInfo.Tables[0].Rows[i]["ShareNum"].ToString());
                        info.isPraise = concernInfo.Tables[0].Rows[i]["isPraise"].ToString();
                        list.Add(info);
                    }
                    respData.content.concernList = list;
                    respData.content.pageCount = (int)concernInfo.Tables[1].Rows[0]["PageCount"];
                    respData.content.rowCount = (int)concernInfo.Tables[1].Rows[0]["RowsCount"];
                }
                else
                {
                    respData.code = "101";
                    respData.description = "没有获取到数据";
                }
            }
            catch (Exception ex)
            {
                respData.code = "103";
                respData.description = "数据库操作错误";
                respData.exception = ex.ToString();
            }
            content = respData.ToJSON();
            return content;
        }
        #endregion

        #region GetEventAlbumList
        /// <summary>
        /// 获取视频信息
        /// </summary>
        /// <returns></returns>
        public string GetEventAlbumList()
        {
            string content = string.Empty;
            var respData = new albumRespData();
            try
            {
                //接收参数
                string reqContent = HttpContext.Current.Request["ReqContent"];
                var reqObj = reqContent.DeserializeJSONTo<albumReqData>();
                reqObj = reqObj == null ? new albumReqData() : reqObj;

                //获取客户ID
                if (!string.IsNullOrEmpty(reqObj.common.customerId))
                {
                    customerId = reqObj.common.customerId;
                }

                //初始化返回对象
                respData.content = new albumRespContentData();
                respData.content.videoList = new List<albumRespContentDataItem>();

                //用户登录信息
                var loggingSessionInfo = Default.GetBSLoggingSession(customerId, "1");

                XieHuiBaoBLL itemService = new XieHuiBaoBLL(loggingSessionInfo, "vip");

                int pageSize = 15, pageIndex = 1;
                if (!string.IsNullOrEmpty(reqObj.special.pageSize))
                {
                    pageSize = int.Parse(reqObj.special.pageSize);
                }
                if (!string.IsNullOrEmpty(reqObj.special.pageIndex))
                {
                    pageIndex = int.Parse(reqObj.special.pageIndex);
                }
                int rowCount = 0;
                var orderInfo = itemService.GetEventAlbumList(reqObj.common.userId, pageSize, pageIndex, out rowCount);
                if (orderInfo != null && orderInfo.Count > 0)
                {
                    List<albumRespContentDataItem> list = new List<albumRespContentDataItem>();
                    foreach (var item in orderInfo)
                    {
                        albumRespContentDataItem info = new albumRespContentDataItem();
                        info.albumId = item.AlbumId;
                        info.moduleId = item.ModuleId;
                        info.moduleType = item.ModuleType;
                        info.moduleName = item.ModuleName;
                        info.type = item.Type;
                        info.imageUrl = item.ImageUrl;
                        info.title = item.Title;
                        info.description = item.Description;
                        info.sortOrder = item.SortOrder;
                        info.praiseCount = item.PraiseCount;
                        info.browseCount = item.BrowseCount;
                        info.keepCount = item.KeepCount;
                        info.isKeep = item.isKeep;
                        info.isPraise = item.isPraise;

                        list.Add(info);
                    }
                    respData.content.videoList = list;
                    respData.content.rowCount = rowCount;

                    pageSize = 15;
                    if (!string.IsNullOrEmpty(reqObj.special.pageSize))
                    {
                        int.TryParse(reqObj.special.pageSize, out pageSize);
                    }
                    respData.content.pageCount = (int)(Math.Ceiling((decimal)rowCount / pageSize));
                }
                else
                {
                    respData.code = "101";
                    respData.description = "没有获取到数据";
                }
            }
            catch (Exception ex)
            {
                respData.code = "103";
                respData.description = "数据库操作错误";
                respData.exception = ex.ToString();
            }
            content = respData.ToJSON();
            return content;

        }
        #endregion

        #region GetCourseInfo
        /// <summary>
        /// 获取课程详细
        /// </summary>
        /// <returns></returns>
        public string GetCourseInfo()
        {
            string content = string.Empty;
            var respData = new courseRespData();
            try
            {
                //接收参数
                string reqContent = HttpContext.Current.Request["ReqContent"];
                var reqObj = reqContent.DeserializeJSONTo<courseReqData>();
                reqObj = reqObj == null ? new courseReqData() : reqObj;

                //获取客户ID
                if (!string.IsNullOrEmpty(reqObj.common.customerId))
                {
                    customerId = reqObj.common.customerId;
                }

                //数据验证
                if (reqObj.special.type == null || reqObj.special.type.Equals(""))
                {
                    respData.code = "2201";
                    respData.description = "课程类型不能为空";
                    return respData.ToJSON().ToString();
                }

                //初始化返回对象
                respData.content = new courseRespContentData();
                respData.content.courseList = new List<courseRespContentDataItem>();
                respData.content.imageList = new List<courseRespContentDataImage>();

                //用户登录信息
                var loggingSessionInfo = Default.GetBSLoggingSession(customerId, "1");

                OnlineShoppingItemBLL itemService = new OnlineShoppingItemBLL(loggingSessionInfo);

                var courseInfo = itemService.GetCourseInfo(reqObj.special.type);
                if (courseInfo != null && courseInfo.Count > 0)
                {
                    List<courseRespContentDataItem> list = new List<courseRespContentDataItem>();
                    foreach (var item in courseInfo)
                    {
                        courseRespContentDataItem info = new courseRespContentDataItem();
                        info.courseID = item.CourseId;
                        info.couseDesc = item.CouseDesc;
                        info.courseName = item.CourseName;
                        info.courseSummary = item.CourseSummary;
                        info.courseFee = item.CourseFee;
                        info.courseStartTime = item.CourseStartTime;
                        info.couseCapital = item.CouseCapital;
                        info.couseContact = item.CouseContact;

                        list.Add(info);
                    }

                    //图片信息
                    var dsImages = itemService.GetItemImageList(courseInfo[0].CourseId);
                    if (dsImages != null && dsImages.Tables.Count > 0 && dsImages.Tables[0].Rows.Count > 0)
                    {
                        respData.content.imageList = DataTableToObject.ConvertToList<courseRespContentDataImage>(dsImages.Tables[0]);
                    }

                    respData.content.courseList = list;
                }
                else
                {
                    respData.code = "101";
                    respData.description = "没有获取到数据";
                }
            }
            catch (Exception ex)
            {
                respData.code = "103";
                respData.description = "数据库操作错误";
                respData.exception = ex.ToString();
            }
            content = respData.ToJSON();
            return content;
        }
        #endregion

        #region sendCode
        /// <summary>
        /// 中欧_发送短信验证码
        /// </summary>
        /// <returns></returns>
        public string sendCode()
        {
            string content = string.Empty;
            var respData = new Default.LowerRespData();
            try
            {
                string reqContent = HttpContext.Current.Request["ReqContent"];
                var reqObj = reqContent.DeserializeJSONTo<SendCodeReqData>();

                ReceiveService service = new ReceiveService();
                if (reqObj.special.mobile != "")
                {
                    //生成随机数 6位
                    Random rd = new Random();
                    string code = rd.Next(100000, 999999).ToString();
                    //调用接口 发送验证码
                    var res = service.Recieve(reqObj.special.mobile, "您的验证码是：" + code, "商学院联盟");

                    //将验证码保存到Session中
                    HttpContext.Current.Session["sendCode"] = code;

                    switch (res)
                    {
                        case "F":
                            respData.code = "101";
                            respData.description = "验证码发送失败，请稍候再试。";
                            break;
                        default:
                            break;
                    }
                }
                else
                {
                    respData.code = "102";
                    respData.description = "手机号码不能为空！";
                }
            }
            catch (Exception ex)
            {
                respData.code = "103";
                respData.description = "数据库操作错误";
                respData.exception = ex.ToString();

                Loggers.Exception(new ExceptionLogInfo()
                {
                    ErrorMessage = string.Format("sendCode: {0}", ex.ToJSON())
                });
            }
            content = respData.ToJSON();
            return content;
        }

        public class SendCodeReqData : Default.ReqData
        {
            public SendCodeReqSpecialData special;
        }
        public class SendCodeReqSpecialData
        {
            public string mobile { get; set; }//手机号码
        }
        #endregion

        #region ResetPassword
        /// <summary>
        /// 重置密码
        /// </summary>
        /// <returns></returns>
        public string ResetPassword()
        {
            string content = string.Empty;
            var respData = new vipRespData();
            try
            {
                //接收参数
                string reqContent = HttpContext.Current.Request["ReqContent"];
                var reqObj = reqContent.DeserializeJSONTo<vipReqData>();
                reqObj = reqObj == null ? new vipReqData() : reqObj;

                //获取客户ID
                if (!string.IsNullOrEmpty(reqObj.common.customerId))
                {
                    customerId = reqObj.common.customerId;
                }

                //初始化返回对象
                respData.content = new vipRespContentData();
                respData.content.vipList = new List<vipRespContentDataItem>();

                //用户登录信息
                var loggingSessionInfo = Default.GetBSLoggingSession(customerId, "1");

                XieHuiBaoBLL itemService = new XieHuiBaoBLL(loggingSessionInfo, "vip");

                #region 验证码
                if (HttpContext.Current.Session["sendCode"] != null)
                {
                    if (HttpContext.Current.Session["sendCode"].ToString() != null)
                    {
                        if (reqObj.special.code != HttpContext.Current.Session["sendCode"].ToString())
                        {
                            respData.code = "201";
                            respData.description = "验证码不正确.";
                            content = respData.ToJSON();
                            return content;
                        }
                    }
                }
                else
                {
                    respData.code = "202";
                    respData.description = "请先发送验证码后再填写验证码";
                    content = respData.ToJSON();
                    return content;
                }
                #endregion

                VipBLL vipBLL = new VipBLL(loggingSessionInfo);
                //是否有此人
                var vip = vipBLL.Query(new IWhereCondition[] { 
                    new EqualsCondition() { FieldName = "Phone", Value = reqObj.special.mobile },
                    new EqualsCondition() { FieldName = "Status", Value = "2" },
                    new EqualsCondition() { FieldName = "IsDelete", Value = "0" },
                    new EqualsCondition() { FieldName = "ClientID", Value = customerId }
                }, null).FirstOrDefault();

                VipEntity entity = new VipEntity();
                if (vip == null)
                {
                    respData.code = "203";
                    respData.description = "不存在此手机号的用户信息";
                    content = respData.ToJSON();
                    return content;
                }
                else
                {
                    entity = vipBLL.GetByID(vip.VIPID);
                    entity.VIPID = vip.VIPID;
                    entity.VipPasswrod = reqObj.special.newPassWord;
                    entity.LastUpdateTime = DateTime.Now;
                    vipBLL.Update(entity);

                    respData.code = "200";
                    respData.description = "操作成功";
                }
            }
            catch (Exception ex)
            {
                respData.code = "103";
                respData.description = "数据库操作错误";
                respData.exception = ex.ToString();
            }
            content = respData.ToJSON();
            return content;
        }
        #endregion

        #region GetUserInfo
        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <returns></returns>
        public string GetUserInfo()
        {
            string content = string.Empty;
            var respData = new vipRespData();
            try
            {
                //接收参数
                string reqContent = HttpContext.Current.Request["ReqContent"];
                var reqObj = reqContent.DeserializeJSONTo<vipReqData>();
                reqObj = reqObj == null ? new vipReqData() : reqObj;

                //获取客户ID
                if (!string.IsNullOrEmpty(reqObj.common.customerId))
                {
                    customerId = reqObj.common.customerId;
                }

                //初始化返回对象
                respData.content = new vipRespContentData();
                respData.content.vipList = new List<vipRespContentDataItem>();

                //用户登录信息
                var loggingSessionInfo = Default.GetBSLoggingSession(customerId, "1");

                XieHuiBaoBLL itemService = new XieHuiBaoBLL(loggingSessionInfo, "vip");

                var vipInfo = itemService.GetUserInfo(reqObj.common.userId);
                if (vipInfo != null && vipInfo.Count > 0)
                {
                    List<vipRespContentDataItem> list = new List<vipRespContentDataItem>();
                    foreach (var item in vipInfo)
                    {
                        vipRespContentDataItem info = new vipRespContentDataItem();
                        info.vipID = item.VIPID;
                        info.vipName = item.VipName;
                        info.hobby = item.Hobby;
                        info.headImg = item.HeadImgUrl;
                        info.notApproveReson = item.NotApproveReson;
                        info.status = item.Status;
                        info.optionTextEn = item.OptionTextEn;
                        info.pageIndex = item.PageIndex;
                        info.statusText = item.OptionText;

                        list.Add(info);
                    }
                    respData.content.vipList = list;
                }
                else
                {
                    respData.code = "101";
                    respData.description = "没有获取到数据";
                }
            }
            catch (Exception ex)
            {
                respData.code = "103";
                respData.description = "数据库操作错误";
                respData.exception = ex.ToString();
            }
            content = respData.ToJSON();
            return content;
        }
        #endregion

        #region GetUserList
        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <returns></returns>
        public string GetUserList()
        {
            string content = string.Empty;
            var respData = new vipRespData();
            try
            {
                //接收参数
                string reqContent = HttpContext.Current.Request["ReqContent"];
                var reqObj = reqContent.DeserializeJSONTo<vipReqData>();
                reqObj = reqObj == null ? new vipReqData() : reqObj;

                //获取客户ID
                if (!string.IsNullOrEmpty(reqObj.common.customerId))
                {
                    customerId = reqObj.common.customerId;
                }

                //初始化返回对象
                respData.content = new vipRespContentData();
                respData.content.vipList = new List<vipRespContentDataItem>();

                //用户登录信息
                var loggingSessionInfo = Default.GetBSLoggingSession(customerId, "1");

                XieHuiBaoBLL itemService = new XieHuiBaoBLL(loggingSessionInfo, "vip");

                var vipInfo = itemService.GetUserList(reqObj.special.vipName, reqObj.special.className, reqObj.special.company, reqObj.special.addr, reqObj.common.userId);
                if (vipInfo != null && vipInfo.Count > 0)
                {
                    List<vipRespContentDataItem> list = new List<vipRespContentDataItem>();
                    foreach (var item in vipInfo)
                    {
                        vipRespContentDataItem info = new vipRespContentDataItem();
                        info.vipID = item.VIPID;
                        info.vipName = item.VipName;
                        info.hobby = item.Hobby;
                        info.headImg = item.HeadImgUrl;
                        info.position = item.PositionName;
                        info.className = item.ClassName;
                        info.searchCount = int.Parse(item.SearchCount);

                        list.Add(info);
                    }
                    respData.searchCount = list[0].searchCount;
                    respData.content.vipList = list;
                }
                else
                {
                    respData.code = "101";
                    respData.description = "没有获取到数据";
                }
            }
            catch (Exception ex)
            {
                respData.code = "103";
                respData.description = "数据库操作错误";
                respData.exception = ex.ToString();
            }
            content = respData.ToJSON();
            return content;
        }
        #endregion

        #region FileUpload
        /// <summary>
        /// 上传图片
        /// </summary>
        /// <param name="files"></param>
        /// <returns></returns>
        public string FileUpload()
        {
            string res = "";
            HttpPostedFile files = HttpContext.Current.Request.Files["fileUp"];
            UpLoadAttachment(files, out res);
            return res;
        }
        #endregion

        #region UpLoadAttachment
        /// <summary>
        /// 上传附件
        /// </summary>
        /// <param name="files"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public string UpLoadAttachment(HttpPostedFile files, out string msg)
        {
            msg = "";
            string filename = "";
            string fileName = "";
            HttpPostedFile postedFile = files;
            if (postedFile != null && postedFile.FileName != null && postedFile.FileName != "")
            {
                filename = postedFile.FileName;
                string suffixname = "";
                if (filename != null)
                {
                    suffixname = filename.Substring(filename.LastIndexOf(".")).ToLower();
                }
                string tempPath = "/File/ceibs/images/";
                fileName = DateTime.Now.ToString("yyyy.MM.dd.mm.ss.ffffff") + suffixname;
                string savepath = HttpContext.Current.Server.MapPath(tempPath);
                if (!Directory.Exists(savepath))
                {
                    Directory.CreateDirectory(savepath);
                }
                postedFile.SaveAs(savepath + @"/" + fileName);//保存
                msg = "{code:200,content:'" + ConfigurationManager.AppSettings["customer_service_url"].ToString().TrimEnd('/') + tempPath + fileName + "'}";
            }
            else
            {
                msg = "{code:300,content:'请上传.jpg,.png,.gif文件'}";
            }
            return fileName;
        }
        #endregion

        #region GetImagesList
        /// <summary>
        /// 获取活动图片列表
        /// </summary>
        /// <returns></returns>
        public string GetImagesList()
        {
            string content = string.Empty;
            var respData = new courseRespData();
            try
            {
                //接收参数
                string reqContent = HttpContext.Current.Request["ReqContent"];
                var reqObj = reqContent.DeserializeJSONTo<courseReqData>();
                reqObj = reqObj == null ? new courseReqData() : reqObj;

                //获取客户ID
                if (!string.IsNullOrEmpty(reqObj.common.customerId))
                {
                    customerId = reqObj.common.customerId;
                }

                //活动ID不能为空
                if (reqObj.special.eventId == null || reqObj.special.eventId.Equals(""))
                {
                    respData.code = "2201";
                    respData.description = "活动ID不能为空";
                    return respData.ToJSON().ToString();
                }

                //初始化返回对象
                respData.content = new courseRespContentData();
                respData.content.imageList = new List<courseRespContentDataImage>();

                //用户登录信息
                var loggingSessionInfo = Default.GetBSLoggingSession(customerId, "1");

                OnlineShoppingItemBLL itemService = new OnlineShoppingItemBLL(loggingSessionInfo);

                //图片信息
                var dsImages = itemService.GetItemImageList(reqObj.special.eventId);
                if (dsImages != null && dsImages.Tables.Count > 0 && dsImages.Tables[0].Rows.Count > 0)
                {
                    respData.content.imageList = DataTableToObject.ConvertToList<courseRespContentDataImage>(dsImages.Tables[0]);
                }
                else
                {
                    respData.code = "101";
                    respData.description = "没有获取到数据";
                }
            }
            catch (Exception ex)
            {
                respData.code = "103";
                respData.description = "数据库操作错误";
                respData.exception = ex.ToString();
            }
            content = respData.ToJSON();
            return content;
        }
        #endregion

        #region getNewsDetailByNewsID
        public string getNewsDetailByNewsID()
        {
            string res = string.Empty;
            string reqContent = string.Empty;
            RequestEntity<reqNewsEntity> requestEntity = new RequestEntity<reqNewsEntity>();
            if (!string.IsNullOrEmpty(HttpContext.Current.Request["ReqContent"]))
            {
                reqContent = HttpContext.Current.Request["ReqContent"];
                var reqObj = reqContent.DeserializeJSONTo<ReqData<getNewsDetailByNewsIDEntity>>();
                if (reqObj != null)
                {
                    var loggingSessionInfo = Default.GetBSLoggingSession(reqObj.common.customerId, "1");
                    if (!string.IsNullOrEmpty(reqObj.common.userId))
                    {
                        loggingSessionInfo.UserID = reqObj.common.userId;
                    }
                    reqNewsEntity pReqNewsEntity = new OnlineShoppingItemBLL(loggingSessionInfo).getEventstatsDetailByNewsID(reqObj);
                    if (pReqNewsEntity != null)
                    {
                        if (!string.IsNullOrEmpty(pReqNewsEntity.News.Content))
                        {
                            pReqNewsEntity.News.Content = HttpUtility.HtmlDecode(pReqNewsEntity.News.Content);
                        }
                        requestEntity.code = "200";
                        requestEntity.description = "操作成功";
                        requestEntity.content = pReqNewsEntity;
                    }
                    else
                    {
                        requestEntity.code = "103";
                        requestEntity.description = "数据库操作失误";
                        requestEntity.content = null;
                    }
                }
            }
            res = requestEntity.ToJSON();
            return res;
        }
        #endregion

        #region getEventList 4.9 活动列表
        public string getEventList()
        {
            string res = string.Empty;
            string reqContent = string.Empty;
            RequestEntity<SearchListEntity<reqActivityListEntity[]>> requestEntity = new RequestEntity<SearchListEntity<reqActivityListEntity[]>>();
            if (!string.IsNullOrEmpty(HttpContext.Current.Request["ReqContent"]))
            {
                reqContent = HttpContext.Current.Request["ReqContent"];
                var reqObj = reqContent.DeserializeJSONTo<ReqData<getActivityListEntity>>();
                if (reqObj != null)
                {
                    var loggingSessionInfo = Default.GetBSLoggingSession(reqObj.common.customerId, "1");
                    SearchListEntity<reqActivityListEntity[]> searchListEntity = new DynamicInterfaceBLL(loggingSessionInfo).getEventList(reqObj);
                    if (searchListEntity != null)
                    {
                        requestEntity.code = "200";
                        requestEntity.description = "操作成功";
                        requestEntity.content = searchListEntity;
                    }
                    else
                    {
                        requestEntity.code = "1";
                        requestEntity.description = "操作失败";
                        requestEntity.content = null;
                    }
                }
            }
            res = requestEntity.ToJSON();
            return res;
        }
        #endregion

        #region getEventByEventID    4.10
        public string getEventByEventID()
        {
            string res = string.Empty;
            string reqContent = string.Empty;
            RequestEntity<reqActivityEntity> requestEntity = new RequestEntity<reqActivityEntity>();
            if (!string.IsNullOrEmpty(HttpContext.Current.Request["ReqContent"]))
            {
                reqContent = HttpContext.Current.Request["ReqContent"];
                var reqObj = reqContent.DeserializeJSONTo<ReqData<getActivityByActivityIDEntity>>();
                if (reqObj != null)
                {
                    var loggingSessionInfo = Default.GetBSLoggingSession(reqObj.common.customerId, "1");
                    reqActivityEntity pReqNewsEntity = new DynamicInterfaceBLL(loggingSessionInfo).getEventByEventID(reqObj);

                    if (pReqNewsEntity != null)
                    {
                        if (!string.IsNullOrEmpty(pReqNewsEntity.Activity.ActivityContent))
                        {
                            pReqNewsEntity.Activity.ActivityContent = HttpUtility.HtmlDecode(pReqNewsEntity.Activity.ActivityContent);
                        }
                        if (!string.IsNullOrEmpty(pReqNewsEntity.Activity.BeginTime))
                        {
                            DateTime dt = new DateTime();
                            if (DateTime.TryParse(pReqNewsEntity.Activity.BeginTime, out dt))
                            {
                                string pWeek = "";
                                switch (dt.DayOfWeek)
                                {
                                    case DayOfWeek.Friday:
                                        pWeek = "星期五";
                                        break;
                                    case DayOfWeek.Monday:
                                        pWeek = "星期一";
                                        break;
                                    case DayOfWeek.Saturday:
                                        pWeek = "星期六";
                                        break;
                                    case DayOfWeek.Sunday:
                                        pWeek = "星期天";
                                        break;
                                    case DayOfWeek.Thursday:
                                        pWeek = "星期四";
                                        break;
                                    case DayOfWeek.Tuesday:
                                        pWeek = "星期二";
                                        break;
                                    case DayOfWeek.Wednesday:
                                        pWeek = "星期三";
                                        break;
                                    default:
                                        pWeek = "";
                                        break;
                                }
                                pReqNewsEntity.Activity.ActivityTime = dt.ToString("yyyy-MM-dd") + " " + pWeek + " " + dt.ToString("HH:mm");
                            }

                        }
                        requestEntity.code = "200";
                        requestEntity.description = "操作成功";
                        requestEntity.content = pReqNewsEntity;
                    }
                    else
                    {
                        requestEntity.code = "1";
                        requestEntity.description = "操作失败";
                        requestEntity.content = null;
                    }
                }
            }
            res = requestEntity.ToJSON();
            return res;
        }
        #endregion

        #region getUserDefinedByUserID 4.4 根据用户获取字段配置信息
        public string getUserDefinedByUserID()
        {
            string res = string.Empty;
            string reqContent = string.Empty;
            RequestEntity<reqPageListEntity> requestEntity = new RequestEntity<reqPageListEntity>();
            if (!string.IsNullOrEmpty(HttpContext.Current.Request["ReqContent"]))
            {
                reqContent = HttpContext.Current.Request["ReqContent"];
                var reqObj = reqContent.DeserializeJSONTo<ReqData<getUserDefinedByUserIDEntity>>();
                if (reqObj.special.TypeID < 1)
                {
                    reqObj.special.TypeID = 1;
                }
                if (reqObj != null)
                {
                    var loggingSessionInfo = Default.GetBSLoggingSession(reqObj.common.customerId, "1");
                    List<PageEntity> list = new DynamicInterfaceBLL(loggingSessionInfo).getUserDefinedByUserID(reqObj);
                    reqPageListEntity pageList = new reqPageListEntity();
                    pageList.pageList = list;
                    if (list != null && list.Count > 0)
                    {
                        requestEntity.code = "200";
                        requestEntity.description = "操作成功";
                        requestEntity.content = pageList;
                    }
                    else
                    {
                        requestEntity.code = "1";
                        requestEntity.description = "操作失败";
                        requestEntity.content = null;
                    }
                }
            }
            res = requestEntity.ToJSON();
            return res;
        }
        #endregion

        #region addEventInfo
        //public string addEventInfo()
        //{
        //    string res = string.Empty;
        //    string reqContent = string.Empty;
        //    string email = string.Empty;
        //    RequestEntity<reqEventInfoEntity> requestEntity = new RequestEntity<reqEventInfoEntity>();
        //    if (!string.IsNullOrEmpty(HttpContext.Current.Request["ReqContent"]))
        //    {
        //        reqContent = HttpContext.Current.Request["ReqContent"];
        //        var reqObj = reqContent.DeserializeJSONTo<ReqData<submitActivityInfoEntity>>();
        //        if (reqObj != null)
        //        {
        //            var loggingSessionInfo = Default.GetBSLoggingSession(reqObj.common.customerId, "1");
        //            string pEventVipTicketID = new DynamicInterfaceBLL(loggingSessionInfo).submitEventsInfo(reqObj);
        //            if (!string.IsNullOrEmpty(pEventVipTicketID))
        //            {
        //                reqEventInfoEntity reqEntity = new BS.Entity.reqEventInfoEntity();
        //                reqEntity.OrderID = pEventVipTicketID.Split('|')[0];
        //                reqEntity.VipID = pEventVipTicketID.Split('|')[1];
        //                requestEntity.code = "200";
        //                requestEntity.description = "操作成功";
        //                requestEntity.content = reqEntity;
        //            }
        //            else
        //            {
        //                requestEntity.code = "1";
        //                requestEntity.description = "操作失败";
        //                requestEntity.content = null;
        //            }
        //        }
        //    }
        //    res = requestEntity.ToJSON();
        //    return res;
        //}

        public string addEventInfo()
        {
            string res = string.Empty;
            string reqContent = string.Empty;
            string email = string.Empty;
            RequestEntity<reqEventInfoEntity> requestEntity = new RequestEntity<reqEventInfoEntity>();
            if (!string.IsNullOrEmpty(HttpContext.Current.Request["ReqContent"]))
            {
                reqContent = HttpContext.Current.Request["ReqContent"];
                var reqObj = reqContent.DeserializeJSONTo<ReqData<submitActivityInfoEntity>>();
                if (reqObj != null)
                {
                    var loggingSessionInfo = Default.GetBSLoggingSession(reqObj.common.customerId, "1");
                    int result = new DynamicInterfaceBLL(loggingSessionInfo).submitEventsInfo(reqObj);
                    if (result > 0)
                    {
                        requestEntity.code = "200";
                        requestEntity.description = "操作成功";
                        requestEntity.content = null;
                    }
                    else
                    {
                        requestEntity.code = "1";
                        requestEntity.description = "操作失败";
                        requestEntity.content = null;
                    }
                }
            }
            res = requestEntity.ToJSON();
            return res;
        }
        #endregion

        #region register
        /// <summary>
        /// 中欧注册信息
        /// </summary>
        /// <returns></returns>
        public string register()
        {
            string res = string.Empty;
            string reqContent = string.Empty;
            string email = string.Empty;
            RequestEntity<reqGetUserIDByOpenIDEntity> requestEntity = new RequestEntity<reqGetUserIDByOpenIDEntity>();
            if (!string.IsNullOrEmpty(HttpContext.Current.Request["ReqContent"]))
            {
                reqContent = HttpContext.Current.Request["ReqContent"];
                var reqObj = reqContent.DeserializeJSONTo<submitUserInfoEntity>();
                if (reqObj != null)
                {
                    var loggingSessionInfo = Default.GetBSLoggingSession(reqObj.common.customerId, "1");

                    //是否有Email
                    bool noEmail = true;
                    //是否有Phone
                    bool noPhone = true;
                    //是否有VipPasswrod
                    bool noVipPasswrod = true;
                    if (reqObj != null && reqObj.special.Control != null && reqObj.special.Control.Count > 0)
                    {
                        foreach (ControlUpdateEntity cEntity in reqObj.special.Control)
                        {
                            if (cEntity != null)
                            {
                                if (cEntity.ColumnName.ToLower() == "email")
                                {
                                    if (string.IsNullOrEmpty(cEntity.Value))
                                    {
                                        requestEntity.code = "2";
                                        requestEntity.description = "操作失败，Email不能为空";
                                        requestEntity.content = null;

                                        res = requestEntity.ToJSON();
                                        return res;
                                    }
                                    else
                                    {
                                        if (new DynamicInterfaceBLL(loggingSessionInfo).checkUserEmail(cEntity.Value, reqObj.common.userId, reqObj.common.customerId))
                                        {
                                            requestEntity.code = "3";
                                            requestEntity.description = "操作失败，Email已存在";
                                            requestEntity.content = null;

                                            res = requestEntity.ToJSON();
                                            return res;
                                        }
                                    }
                                    email = cEntity.Value;
                                    noEmail = false;
                                }
                                else if (cEntity.ColumnName.ToLower() == "phone")
                                {
                                    if (string.IsNullOrEmpty(cEntity.Value))
                                    {
                                        requestEntity.code = "4";
                                        requestEntity.description = "操作失败，Phone不能为空";
                                        requestEntity.content = null;

                                        res = requestEntity.ToJSON();
                                        return res;
                                    }
                                    else
                                    {
                                        if (new DynamicInterfaceBLL(loggingSessionInfo).checkUserPhone(cEntity.Value, reqObj.common.userId, reqObj.common.customerId))
                                        {
                                            requestEntity.code = "5";
                                            requestEntity.description = "操作失败，Phone已存在";
                                            requestEntity.content = null;

                                            res = requestEntity.ToJSON();
                                            return res;
                                        }
                                    }

                                    noPhone = false;
                                }
                                else if (cEntity.ColumnName.ToLower() == "vippasswrod")
                                {
                                    if (string.IsNullOrEmpty(cEntity.Value))
                                    {
                                        requestEntity.code = "6";
                                        requestEntity.description = "操作失败，密码不能为空";
                                        requestEntity.content = null;

                                        res = requestEntity.ToJSON();
                                        return res;
                                    }

                                    noVipPasswrod = false;
                                }
                            }
                        }

                        //获取前台传入的第一个控件ID
                        var pDefindInfo = new DynamicInterfaceBLL(loggingSessionInfo).GetPageBlockID(reqObj.special.Control[0].ControlID);
                        string status = null;
                        if (pDefindInfo != null && pDefindInfo.Count > 0)
                        {
                            //获取订单的状态
                            status = pDefindInfo[0].Remark.Split(',')[0];
                            //判断是否需要发送邮件
                            if (pDefindInfo[0].Remark.Contains("email"))
                            {
                                #region 邮件发送
                                try
                                {
                                    var pEmail = new DynamicInterfaceBLL(loggingSessionInfo).GetUserEmail(reqObj.common.userId);
                                    if (loggingSessionInfo.ClientID == "a2573925f3b94a32aca8cac77baf6d33")
                                    {
                                        XmlManager xml = new XmlManager(ConfigurationManager.AppSettings["xmlFile"]);

                                        FromSetting fs = new FromSetting();
                                        fs.SMTPServer = xml.SelectNodeText("//Root/EMBAEmail//SMTPServer", 0);
                                        fs.SendFrom = xml.SelectNodeText("//Root/EMBAEmail//MailSendFrom", 0);
                                        fs.UserName = xml.SelectNodeText("//Root/EMBAEmail//MailUserName", 0);
                                        fs.Password = xml.SelectNodeText("//Root/EMBAEmail//MailUserPassword", 0);
                                        Mail.SendMail(fs, pEmail + "," + xml.SelectNodeText("//Root/EMBAEmail//MailTo", 0), xml.SelectNodeText("//Root/EMBAEmail//MailTitle", 0), xml.SelectNodeText("//Root/EMBAEmail//EMBAMailTemplate", 0), null);
                                    }
                                }
                                catch
                                {
                                    requestEntity.code = "1";
                                    requestEntity.description = "邮件发送操作失败";
                                    requestEntity.content = null;
                                }
                                #endregion
                            }
                            List<ControlUpdateEntity> cEntity = reqObj.special.Control;
                            ControlUpdateEntity entity = new BS.Entity.ControlUpdateEntity();
                            entity.ColumnName = "Status";
                            entity.Value = status;
                            cEntity.Add(entity);
                            reqObj.special.Control = cEntity;
                        }
                    }

                    //若Email、Phone和密码全部没有，且存在用户ID，则跳过这步验证
                    if (!(noEmail && noPhone && noVipPasswrod && !string.IsNullOrEmpty(reqObj.common.userId)))
                    {
                        if (noEmail || noPhone || noVipPasswrod)
                        {
                            requestEntity.code = "7";
                            requestEntity.description = "操作失败，Email、Phone或密码配置不正确";
                            requestEntity.content = null;

                            res = requestEntity.ToJSON();
                            return res;
                        }
                    }

                    string pUserID = reqObj.common.userId;

                    int result = new DynamicInterfaceBLL(loggingSessionInfo).register(reqObj);

                    if (result > 0)
                    {
                        var pDefindInfo = new DynamicInterfaceBLL(loggingSessionInfo).GetPageBlockID(reqObj.special.Control[0].ControlID);
                        if (pDefindInfo != null && pDefindInfo.Count > 0)
                        {
                            if (pDefindInfo[0].Remark.Contains("email"))
                            {
                                //if (string.IsNullOrEmpty(pUserID))
                                //{
                                string pContent = null;
                                VipInfoMaillBLL vipInfoMaillBll = new VipInfoMaillBLL(loggingSessionInfo);
                                XmlManager xml = new XmlManager(ConfigurationManager.AppSettings["xmlFile"]);
                                if (loggingSessionInfo.ClientID == "a2573925f3b94a32aca8cac77baf6d33")
                                {
                                    pContent = xml.GetNodeText("//Root//EMBABackgroundEmail/MailContent");
                                    email = xml.SelectNodeText("//Root//EMBABackgroundEmail/MailTo");
                                }
                                else if (loggingSessionInfo.ClientID == "e137e8e040bb4db3be17d90feeefa7bf")
                                {
                                    pContent = xml.GetNodeText("//Root//XWTMail/XWTMailTemplate");
                                    email = xml.SelectNodeText("//Root//XWTMail/MailTo");
                                }
                                else if (loggingSessionInfo.ClientID == "75a232c2cf064b45b1b6393823d2431e")
                                {
                                    pContent = xml.GetNodeText("//Root//EMBAMail/EMBAMailTemplate");
                                    email = xml.SelectNodeText("//Root//EMBAMail/MailTo");
                                }
                                vipInfoMaillBll.SendNoticeMail(email, pContent);
                                //}
                            }
                        }
                        reqGetUserIDByOpenIDEntity reqEntity = new BS.Entity.reqGetUserIDByOpenIDEntity();
                        reqEntity.userId = reqObj.common.userId;
                        requestEntity.code = "200";
                        requestEntity.description = "操作成功";
                        requestEntity.content = reqEntity;
                    }
                    else
                    {
                        requestEntity.code = "1";
                        requestEntity.description = "操作失败";
                        requestEntity.content = null;
                    }
                }
            }
            res = requestEntity.ToJSON();
            return res;
        }
        #endregion

        #region getUserByLogin    4.12 登陆
        public string getUserByLogin()
        {
            string res = string.Empty;
            string reqContent = string.Empty;
            RequestEntity<reqGetUserIDByOpenIDEntity> requestEntity = new RequestEntity<reqGetUserIDByOpenIDEntity>();
            if (!string.IsNullOrEmpty(HttpContext.Current.Request["ReqContent"]))
            {
                reqContent = HttpContext.Current.Request["ReqContent"];
                var reqObj = reqContent.DeserializeJSONTo<ReqData<getUserByLoginEntity>>();
                if (reqObj != null)
                {
                    var loggingSessionInfo = Default.GetBSLoggingSession(reqObj.common.customerId, "1");
                    string pUserID = new DynamicInterfaceBLL(loggingSessionInfo).getUserByLogin(reqObj);
                    if (!string.IsNullOrEmpty(pUserID))
                    {
                        if (pUserID == "-1")
                        {
                            requestEntity.code = "2";
                            requestEntity.description = "账号不存在或密码错误";
                            requestEntity.content = null;
                        }
                        else if (pUserID == "-2")
                        {
                            requestEntity.code = "3";
                            requestEntity.description = "账号已存在";
                            requestEntity.content = null;
                        }
                        else
                        {
                            reqGetUserIDByOpenIDEntity pEntity = new reqGetUserIDByOpenIDEntity();
                            pEntity.userId = pUserID;
                            requestEntity.code = "200";
                            requestEntity.description = "操作成功";
                            requestEntity.content = pEntity;
                        }
                    }
                    else
                    {
                        requestEntity.code = "1";
                        requestEntity.description = "操作失败";
                        requestEntity.content = null;
                    }
                }
            }
            res = requestEntity.ToJSON();
            return res;
        }
        #endregion

        #region 根据日期生成订单号
        public string GetOrdersNo()
        {
            Random ro = new Random();//得到随机数 
            string OdNo = "HS" + DateTime.Now.Year.ToString()
            + DateTime.Now.Month.ToString()
            + DateTime.Now.Day.ToString()
            + DateTime.Now.Hour.ToString()
            + DateTime.Now.Minute.ToString()
            + DateTime.Now.Second.ToString()
            + ro.Next(10000).ToString();//订单号
            return OdNo;
        }
        #endregion

        #region GetVipPayMent
        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <returns></returns>
        public string GetVipPayMent()
        {
            string content = string.Empty;
            var respData = new VipPriceResData();
            RequestEntity<VipPriceEntity> requestEntity = new RequestEntity<VipPriceEntity>();
            try
            {


                //接收参数
                string reqContent = HttpContext.Current.Request["ReqContent"];
                var reqObj = reqContent.DeserializeJSONTo<VipPriceResData>();
                reqObj = reqObj == null ? new VipPriceResData() : reqObj;

                //获取客户ID
                if (!string.IsNullOrEmpty(reqObj.common.customerId))
                {
                    customerId = reqObj.common.customerId;
                }
                //用户登录信息
                var loggingSessionInfo = Default.GetBSLoggingSession(customerId, "1");

                XieHuiBaoBLL itemService = new XieHuiBaoBLL(loggingSessionInfo, "vip");

                VipPriceEntity vipEntity = itemService.GetVipPayMent(reqObj.common.userId);
                if (vipEntity != null)
                {
                    requestEntity.code = "200";
                    requestEntity.description = "操作成功";
                    requestEntity.content = vipEntity;
                }
                else
                {
                    requestEntity.code = "101";
                    requestEntity.description = "没有获取到数据";
                }

            }
            catch (Exception ex)
            {
                requestEntity.code = "103";
                requestEntity.description = "数据库操作失误";
            }
            content = requestEntity.ToJSON();
            return content;
        }
        #endregion

        #region SubmitVipPayMent
        /// <summary>
        /// 缴会费，提交订单
        /// </summary>
        /// <returns></returns>
        public string SubmitVipPayMent()
        {
            string content = string.Empty;
            //RequestEntity<VipPriceEntity> requestEntity = new RequestEntity<VipPriceEntity>();
            VipResData vipReq = new VipResData();
            try
            {
                string reqContent = HttpContext.Current.Request["ReqContent"];

                var reqObj = reqContent.DeserializeJSONTo<VipPriceResData>();
                reqObj = reqObj == null ? new VipPriceResData() : reqObj;
                if (reqObj.common.userId == null || reqObj.common.userId.Equals(""))
                {
                    vipReq.code = "2201";
                    vipReq.description = "用户不能为空";
                    return vipReq.ToJSON();

                }
                if (string.IsNullOrEmpty(reqObj.special.itemId))
                {

                    vipReq.code = "2201";
                    vipReq.description = "会费ID不能为空";

                    return vipReq.ToJSON();

                }
                //获取客户ID
                if (!string.IsNullOrEmpty(reqObj.common.customerId))
                {
                    customerId = reqObj.common.customerId;
                }
                var loggingSessionInfo = Default.GetBSLoggingSession(customerId, "1");
                XieHuiBaoBLL itemService = new XieHuiBaoBLL(loggingSessionInfo, "vip");
                //添加主表信息
                SetOrderEntity orderInfo = new SetOrderEntity();
                orderInfo.OrderId = Guid.NewGuid().ToString().Replace("-", "");
                UnitService unitServer = new UnitService(loggingSessionInfo);
                orderInfo.StoreId = unitServer.GetUnitByUnitType("OnlineShopping", null).Id;

                if (orderInfo.StoreId == null || orderInfo.StoreId.Equals(""))
                {
                    vipReq.code = "2201";
                    vipReq.description = "该客户未配置在线商城";
                    return vipReq.ToJSON();
                }
                if (string.IsNullOrEmpty(orderInfo.StoreId))
                {
                    orderInfo.PurchaseUnitId = orderInfo.StoreId;
                }
                if (!string.IsNullOrEmpty(orderInfo.StoreId))
                {
                    TUnitExpandBLL serviceUnitExpand = new TUnitExpandBLL(loggingSessionInfo);
                    orderInfo.OrderCode = serviceUnitExpand.GetUnitOrderNo(loggingSessionInfo, orderInfo.StoreId);
                }
                orderInfo.TotalAmount = itemService.GetPriceByItemId(reqObj.special.itemId);
                orderInfo.Status = "100";
                orderInfo.StatusDesc = "未审核";
                orderInfo.OpenId = reqObj.common.openId;
                orderInfo.OrderDate = System.DateTime.Now.ToString("yyyy-MM-dd");
                orderInfo.CustomerId = reqObj.common.customerId;


                //添加明细信息
                List<InoutDetailInfo> list = new List<InoutDetailInfo>();
                InoutDetailInfo orderDetailInfo = new InoutDetailInfo();
                orderDetailInfo.order_detail_id = Guid.NewGuid().ToString().Replace("_", "");
                orderDetailInfo.order_id = orderInfo.OrderId;
                orderDetailInfo.sku_id = itemService.GetSkuIdByItemId(reqObj.special.itemId);
                orderDetailInfo.retail_amount = orderInfo.TotalAmount;
                list.Add(orderDetailInfo);
                orderInfo.OrderDetailInfoList = list;

                string strError = string.Empty;
                string strMsg = string.Empty;
                InoutService service = new InoutService(loggingSessionInfo);
                bool bReturn = service.SetOrderOnlineShoppingNew(orderInfo, out strError, out strMsg);
                if (bReturn)
                {
                    int i = itemService.SubmitVipPayMent(reqObj.special.itemId, reqObj.common.userId, orderInfo.OrderId, orderDetailInfo.retail_amount);

                }
                else
                {
                    vipReq.code = "100";
                    vipReq.description = strMsg;
                    return vipReq.ToJSON();
                }
                VipResItem vipres = new VipResItem();
                vipres.orderID = orderInfo.OrderId;

                vipReq.content = vipres;
                vipReq.code = "200";
                vipReq.description = "操作成功";

                return vipReq.ToJSON();
            }
            catch (Exception)
            {
                vipReq.code = "103";
                vipReq.description = "数据库操作失误";
                content = vipReq.ToJSON();
            }
            return content;
        }

        #endregion

        #region 公共请求参数
        public class ReqData
        {
            public ReqCommonData common { get; set; }
        }
        public class ReqCommonData
        {
            public string locale;
            public string userId;
            public string openId;
            public string customerId;

        }
        #endregion

        #region 视频相关参数及返回值信息
        public class albumRespData : Default.LowerRespData
        {
            public albumRespContentData content { get; set; }
            public string PraiseNum { get; set; }
        }
        public class albumRespContentData
        {
            public IList<albumRespContentDataItem> videoList { get; set; }
            public int rowCount { get; set; }
            public int pageCount { get; set; }
        }

        public class albumRespContentDataItem
        {
            public string type { get; set; }
            public int isKeep { get; set; }
            public string title { get; set; }
            public int isPraise { get; set; }
            public int? sortOrder { get; set; }
            public int? keepCount { get; set; }
            public string albumId { get; set; }
            public string moduleId { get; set; }
            public string imageUrl { get; set; }
            public int? praiseCount { get; set; }
            public int? browseCount { get; set; }
            public string moduleType { get; set; }
            public string moduleName { get; set; }
            public string description { get; set; }
        }

        public class albumReqData : ReqData
        {
            public albumReqSpecialData special;
        }
        public class albumReqSpecialData
        {
            public string countType { get; set; }
            public string newsType { get; set; }
            public string id { get; set; }
            public string eventStatsID { set; get; }
            public string pageSize { get; set; }
            public string pageIndex { get; set; }
        }

        #endregion

        #region 关注相关参数及返回值信息
        public class concernRespData : Default.LowerRespData
        {
            public concernRespContentData content { get; set; }
        }
        public class concernRespContentData
        {
            public IList<concernRespContentDataItem> concernList { get; set; }
            public int rowCount { get; set; }
            public int pageCount { get; set; }
        }

        public class concernRespContentDataItem
        {
            public string intro { get; set; }
            public string eventStatsID { set; get; }
            public string title { get; set; }
            public string newsID { get; set; }
            public int? allCount { get; set; }
            public int? newsType { get; set; }
            public string agoTime { get; set; }
            public int? keepCount { get; set; }
            public string imageUrl { get; set; }
            public string videoUrl { get; set; }
            public int? praiseCount { get; set; }
            public int? browseCount { get; set; }
            public int? shareCount { set; get; }
            public string createTime { get; set; }
            public string description { get; set; }
            public string newsTypeText { get; set; }
            public string isPraise { set; get; }
        }

        public class concernReqData : ReqData
        {
            public concernReqSpecialData special;
        }
        public class concernReqSpecialData
        {
            public string type { get; set; }
            public string concernId { get; set; }
            public string pageSize { get; set; }
            public string pageIndex { get; set; }
        }

        #endregion

        #region 课程相关参数及返回值信息
        public class courseRespData : Default.LowerRespData
        {
            public courseRespContentData content { get; set; }
        }
        public class courseRespContentData
        {
            public IList<courseRespContentDataItem> courseList { get; set; }
            public IList<courseRespContentDataImage> imageList { get; set; }     //图片集合
        }

        public class courseRespContentDataItem
        {
            public string courseID { get; set; }
            public string couseDesc { get; set; }
            public string courseName { get; set; }
            public string courseSummary { get; set; }
            public string courseFee { get; set; }
            public string courseStartTime { get; set; }
            public string couseCapital { get; set; }
            public string couseContact { get; set; }
        }

        public class courseRespContentDataImage
        {
            public string imageId { get; set; }     //图片标识
            public string imageURL { get; set; }    //图片链接地址
        }

        public class courseReqData : ReqData
        {
            public courseReqSpecialData special;
        }
        public class courseReqSpecialData
        {
            public string type { get; set; }
            public string eventId { get; set; }
        }

        #endregion

        #region 用户相关参数及返回值信息
        public class vipRespData : Default.LowerRespData
        {
            public vipRespContentData content { get; set; }
        }
        public class vipRespContentData
        {
            public IList<vipRespContentDataItem> vipList { get; set; }
        }

        public class vipRespContentDataItem
        {
            public string vipID { get; set; }
            public string hobby { get; set; }
            public string vipFee { get; set; }
            public string vipName { get; set; }
            public string headImg { get; set; }
            public string position { get; set; }
            public string couseDesc { get; set; }
            public string className { get; set; }
            /// <summary>
            /// 会员审核未通过原因
            /// </summary>
            public string notApproveReson { get; set; }

            /// <summary>
            /// 会员状态
            /// 11、15：未提交认证
            /// 12：已提交认证
            /// </summary>
            public int status { get; set; }

            /// <summary>
            /// 会员状态
            /// </summary>
            public string optionTextEn { get; set; }

            /// <summary>
            /// 需要跳转到第几页
            /// </summary>
            public int pageIndex { get; set; }

            public string statusText { get; set; }

            /// <summary>
            /// 会员查询次数
            /// </summary>
            public int searchCount { get; set; }

            public string vipSummary { get; set; }
            public string vipStartTime { get; set; }
            public string couseCapital { get; set; }
            public string couseContact { get; set; }
        }

        public class vipRespContentDataImage
        {
            public string imageId { get; set; }     //图片标识
            public string imageURL { get; set; }    //图片链接地址
        }

        public class vipReqData : ReqData
        {
            public vipReqSpecialData special;
        }
        public class vipReqSpecialData
        {
            public string addr { get; set; }
            public string code { get; set; }
            public string vipID { get; set; }
            public string mobile { get; set; }
            public string vipName { get; set; }
            public string company { get; set; }
            public string passWord { get; set; }
            public string className { get; set; }
            public string newPassWord { get; set; }
        }

        #endregion

        #region 用户商品价格

        public class VipPriceResData : ReqData
        {
            public VipPriceDataItem special { set; get; }
        }

        public class VipResData : Default.LowerRespData
        {
            public VipResItem content { set; get; }
        }

        public class VipResItem
        {
            public string orderID { set; get; }
        }
        public class VipPriceDataItem
        {
            public string itemPrice { set; get; }//商品价格
            public string itemCode { set; get; }//商品编号
            public string itemName { set; get; }//商品名称
            public string itemId { set; get; }//商品ID
            public string vipName { set; get; }//用户名

        }
        #endregion
        #endregion

        #region EMBA

        #endregion


    }
}