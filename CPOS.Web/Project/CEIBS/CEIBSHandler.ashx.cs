using System;
using System.Web;
using System.Linq;
using System.Collections.Generic;

using JIT.CPOS.BS.BLL;
using JIT.Utility.ExtensionMethod;
using JIT.Utility.DataAccess.Query;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.Web.SendSMSService;
using System.Configuration;
using JIT.Utility.Log;
using System.IO;
using System.Data;
using JIT.Utility.Reflection;
using JIT.CPOS.Web.Module.XieHuiBao;


namespace JIT.CPOS.Web.Project.CEIBS
{
    /// <summary>
    /// CEIBSHandler 的摘要说明
    /// </summary>
    public class CEIBSHandler : JIT.CPOS.Web.Base.PageBase.JITAjaxHandler
    {
        string customerId = "75a232c2cf064b45b1b6393823d2431e";

        public XieHuiBaoHandler _xieHuiBaoHandler;
        public XieHuiBaoHandler xieHuiBaoHandler
        {
            get
            {
                if (_xieHuiBaoHandler==null)
                {
                    _xieHuiBaoHandler = new XieHuiBaoHandler();
                }
                return _xieHuiBaoHandler;
            }

        }
   

        #region GetAlbumList
        /// <summary>
        /// 获取视频列表
        /// </summary>
        /// <returns></returns>
        public string GetAlbumList()
        {

            return this.xieHuiBaoHandler.GetEventAlbumList(); 
        }
        #endregion

        #region AddEventStats
        /// <summary>
        /// 添加视频的操作数据
        /// </summary>
        /// <returns></returns>
        public string AddEventStats()
        {
            return this.xieHuiBaoHandler.AddEventStats();
        }
        #endregion
       
        #region GetEventStats
        /// <summary>
        /// 获取最受关注
        /// </summary>
        /// <returns></returns>
        public string GetEventStats()
        {
            return this.xieHuiBaoHandler.GetEventStats();
        }
        #endregion

        #region GetEventAlbumList
        /// <summary>
        /// 获取视频信息
        /// </summary>
        /// <returns></returns>
        public string GetEventAlbumList()
        {
            return this.xieHuiBaoHandler.GetEventAlbumList();

        }
        #endregion

        #region GetCourseInfo
        /// <summary>
        /// 获取课程详细
        /// </summary>
        /// <returns></returns>
        public string GetCourseInfo()
        {
          return   this.xieHuiBaoHandler.GetCourseInfo();
        }
        #endregion

        #region sendCode
        /// <summary>
        /// 中欧_发送短信验证码
        /// </summary>
        /// <returns></returns>
        public string sendCode()
        {
            return this.xieHuiBaoHandler.sendCode();
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
            return this.xieHuiBaoHandler.ResetPassword();
        }
        #endregion

        #region GetUserInfo
        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <returns></returns>
        public string GetUserInfo()
        {
            return this.xieHuiBaoHandler.GetUserInfo();
        }
        #endregion

        #region GetUserList
        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <returns></returns>
        public string GetUserList()
        {
            return this.xieHuiBaoHandler.GetUserList();
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
            return this.xieHuiBaoHandler.FileUpload();
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
           return this.xieHuiBaoHandler.UpLoadAttachment(files,out msg);
        }
        #endregion

        #region GetImagesList
        /// <summary>
        /// 获取活动图片列表
        /// </summary>
        /// <returns></returns>
        public string GetImagesList()
        {
            return this.xieHuiBaoHandler.GetImagesList();
        }
        #endregion

        #region getNewsDetailByNewsID

        public string getNewsDetailByNewsID()
        {
            return this.xieHuiBaoHandler.getNewsDetailByNewsID();
        }
        #endregion

        #region SubmitVipPayMent
        /// <summary>
        /// 订单提交
        /// </summary>
        /// <returns></returns>
        public string SubmitVipPayMent()
        {
            return this.xieHuiBaoHandler.SubmitVipPayMent();
        }
        #endregion

        #region GetVipPayMent
        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <returns></returns>
        public string GetVipPayMent()
        {
            return this.xieHuiBaoHandler.GetVipPayMent();
        }
        #endregion

    

    }
}