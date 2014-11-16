using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.Common;
using JIT.CPOS.DTO.Base;
using JIT.Utility.ExtensionMethod;
using System.Net;
using System.Globalization;
using JIT.CPOS.BS.DataAccess;
using JIT.CPOS.Web.RateLetterInterface;
using JIT.CPOS.Web.RateLetterInterface.PG;

namespace JIT.CPOS.Web.ApplicationInterface.Project.PG
{
    /// <summary>
    /// PGHandler 的摘要说明
    /// </summary>
    public class PGHandler : BaseGateway
    {
        /// <summary>
        /// 当地工会主席
        /// </summary>
        private string m_LocalLeadKey = "VIP020000";

        /// <summary>
        /// 宝洁Handler。
        /// </summary>
        /// <param name="pType"></param>
        /// <param name="pAction"></param>
        /// <param name="pRequest"></param>
        /// <returns></returns>
        protected override string ProcessAction(string pType, string pAction, string pRequest)
        {
            //根据action做不同的业务处理
            var rst = string.Empty;
            switch (pAction)
            {
                case "GetAllAvailablePowerHour":
                    rst = GetAllAvailablePowerHour(pRequest);      //1.按财年获取所有有效的PowerHour(讲座)
                    break;
                case "GetPowerHour":
                    rst = GetPowerHour(pRequest);       //根据PowerHour详细信息
                    break;
                case "GetLocalEmployeeCount":
                    rst = GetLocalEmployeeCount(pRequest);  //获取特定城市员工数
                    break;
                case "GetPowerHourResult":             //获取PowerHour完成后的统计数据
                    rst = GetPowerHourResult(pRequest);
                    break;
                case "GetAvailablePowerHourTopics":                 //获取注册PowerHour时可以用的所有Topic
                    rst = GetAvailablePowerHourTopics(pRequest);
                    break;
                case "RegisterPowerHour":                       //注册PowerHour
                    rst = RegisterPowerHour(pRequest);
                    break;
                case "GetValuableTrainningComments":            //获取Trainning 得到Improvable的 Comments 第12、13条答案列表.
                    rst = GetValuableTrainningComments(pRequest);
                    break;
                case "InviteJoinPowerHour":                             //邀请员工参加 PowerHour
                    rst = InviteJoinPowerHour(pRequest);
                    break;
                case "GetPowerHourInviteState":                //获取所有受邀参加PowerHour的人的反馈（接受/拒绝）
                    rst = GetPowerHourInviteState(pRequest);
                    break;
                case "GetAcceptInvite":                //根据PowerHourID返回所有接受邀请的学员的出席状态。
                    rst = GetAcceptInvite(pRequest);
                    break;
                case "SavePowerHourSitePicutre":                //保存PowerHour Trainning现场照片，并更新PowerHour记录的字段
                    rst = SavePowerHourSitePicutre(pRequest);
                    break;
                case "MarkPowerHourAttendence":                    //记录PowerHour Trainning的参加/缺席人员
                    rst = MarkPowerHourAttendence(pRequest);
                    break;
                case "AcceptPowerHourInvite":                   //接受/拒绝PowerHour邀请
                    rst = AcceptPowerHourInvite(pRequest);
                    break;
                case "CommentPowerHour":               //对PowerHour评分
                    rst = CommentPowerHour(pRequest);
                    break;
                case "VerifyIsLocalLuOwner"://返回当前用户是否是特定城市的工会主席
                    rst = VerifyIsLocalLuOwner(pRequest);
                    break;
                case "GetCityPowerHourState"://所有城市的Power Hour状态
                    rst = GetCityPowerHourState(pRequest);
                    break;
                case "GetSingleCityPowerHour"://特定城市的Power Hour 列表
                    rst = GetSingleCityPowerHour(pRequest);
                    break;
                case "ModifyPowerHourSiteAddress"://修改Power Hour SiteAddress
                    rst = ModifyPowerHourSiteAddress(pRequest);
                    break;
                case "CommonRequestReportForm"://请求宝洁报表
                    rst = CommonRequestReportForm(pRequest);
                    break;
                case "TrainingCalendarLog":
                    rst = VisitLog(pRequest);
                    break;
                case "LibraryLog":
                    rst = VisitLog(pRequest);
                    break;
                case "PowerHourLog":
                    rst = VisitLog(pRequest);
                    break;
                case "SurveyTestLog":
                    rst = VisitLog(pRequest);
                    break;
                case "ICoachLog":
                    rst = VisitLog(pRequest);
                    break;
                default:
                    throw new APIException(string.Format("未实现Action名为{0}的处理.", pAction)) { ErrorCode = 201 };
            }
            return rst;
        }

        #region  CommentPowerHour  对课程进行评分
        /// <summary>
        /// 对PowerHour评分
        /// </summary>
        /// <param name="pRequest"></param>
        /// <returns></returns>
        private string CommentPowerHour(string pRequest)
        {
            var rd = new APIResponse<CommentPowerHourRD>();
            var rdData = new CommentPowerHourRD();
            rdData.IsSuccess = true;
            try
            {
                var rp = pRequest.DeserializeJSONTo<APIRequest<CommentPowerHourRP>>();

                if (rp.Parameters == null) throw new ArgumentException();

                if (rp.Parameters != null)
                    rp.Parameters.Validate();

                if (string.IsNullOrEmpty(rp.CustomerID))
                {
                    throw new APIException("无效的CustomerID【CustomerID】") { ErrorCode = 121 };
                }

                #region  测试
                //List<CommentData> CommentList = new List<CommentData>();
                //CommentData data = new CommentData()
                //{
                //    Index = 1,
                //    Answer = "第1答案"
                //};
                //CommentList.Add(data);
                //data = new CommentData()
                //{
                //    Index = 2,
                //    Answer = "第2答案"
                //};
                //CommentList.Add(data);
                //data = new CommentData()
                //{
                //    Index = 3,
                //    Answer = "第3答案"
                //};
                //CommentList.Add(data);
                //data = new CommentData()
                //{
                //    Index = 4,
                //    Answer = "第4答案"
                //};
                //CommentList.Add(data);
                //data = new CommentData()
                //{
                //    Index = 5,
                //    Answer = "第5答案"
                //};
                //CommentList.Add(data);
                //data = new CommentData()
                //{
                //    Index = 6,
                //    Answer = "第6答案"
                //};
                //CommentList.Add(data);


                //var rdData = new CommentPowerHourRD();
                //rdData.CommentList = CommentList;
                //rd.Data = rdData;

                //return rd.ToJSON();
                #endregion

                var loggingSessionInfo = Default.GetBSLoggingSession(rp.CustomerID, rp.UserID);
                PowerHourInviteBLL inviteBll = new PowerHourInviteBLL(loggingSessionInfo);
                int attendence = inviteBll.GetPowerHourAttendence(rp.CustomerID, rp.Parameters.PowerHourID, rp.UserID);
                if (attendence == (int)AttendenceEnum.AtendenceTrain)     //出席培训
                {
                    if (rp.Parameters.CommentList == null)
                    {
                        throw new APIException("评论[CommentPowerHourList]不能为空");
                    }

                    PowerHourRemarkBLL bll = new PowerHourRemarkBLL(loggingSessionInfo);
                    foreach (var item in rp.Parameters.CommentList)
                    {
                        PowerHourRemarkEntity entity = new PowerHourRemarkEntity();
                        entity.PowerHourRemarkID = Guid.NewGuid();
                        entity.PowerHourID = rp.Parameters.PowerHourID;
                        entity.RemarkerUserID = rp.UserID;
                        entity.QuestionIndex = item.QuestionIndex;
                        entity.Answer = item.Answer;
                        entity.CustomerID = rp.CustomerID;
                        bll.Create(entity);
                    }
                }
                else
                {
                    rd.Message = "您未参加本次培训，不能进行评论";
                    rdData.IsSuccess = false;
                }
                rd.Data = rdData;
            }
            catch (Exception ex)
            {
                rd.ResultCode = 101;
                rd.Message = "业务处理失败";
            }

            return rd.ToJSON();
        }
        #endregion

        #region  GetAcceptInvite  根据PowerHourID返回所有接受邀请的学员的"出席"状态
        /// <summary>
        /// 根据PowerHourID 返回 所有接受邀请的学员的出席状态。
        /// </summary>
        /// <param name="pRequest"></param>
        /// <returns></returns>
        public string GetAcceptInvite(string pRequest)
        {
            var rd = new APIResponse<AcceptInviteRD>();
            try
            {
                var rp = pRequest.DeserializeJSONTo<APIRequest<AcceptInviteRP>>();
                if (rp.Parameters != null)
                    rp.Parameters.Validate();

                if (string.IsNullOrEmpty(rp.CustomerID))
                {
                    throw new APIException("无效的CustomerID【CustomerID】") { ErrorCode = 121 };
                }

                var loggingSessionInfo = Default.GetBSLoggingSession(rp.CustomerID, rp.UserID);
                PowerHourBLL bll = new PowerHourBLL(loggingSessionInfo);
                DataSet ds = bll.GetAcceptInviteState(rp.CustomerID, rp.Parameters.PowerHourID);
                List<AcceptInviteModel> list = new List<AcceptInviteModel>();
                if (ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                {
                    list = DataTableToObject.ConvertToList<AcceptInviteModel>(ds.Tables[0]);
                }
                //if (list != null && list.Count > 0)
                //{
                //    List<AcceptInviteModel> inviteStateList = new List<AcceptInviteModel>();
                //    AcceptInviteModel model = null;
                //    foreach (var m in list)
                //    {
                //        model = new AcceptInviteModel();
                //        model.UserID = m.UserID;
                //        model.UserName = m.UserName;
                //        model.Attendence = m.Attendence;
                //        model.Email = m.Email;
                //        //model.AttendenceState = ConvertAttendence(m.Attendence);
                //        inviteStateList.Add(model);
                //    }

                var rdData = new AcceptInviteRD();
                rdData.StateList = list;
                rd.Data = rdData;
                //}
            }
            catch (Exception ex)
            {
                rd.ResultCode = 101;
                rd.Message = "业务处理失败";
            }

            return rd.ToJSON();
        }
        #endregion

        #region   AcceptPowerHourInvite   记录当前用户接受/拒绝PowerHour邀请的状态
        /// <summary>
        /// 记录当前用户接受/拒绝PowerHour邀请的状态。
        /// </summary>
        /// <param name="pRequest"></param>
        /// <returns></returns>
        private string AcceptPowerHourInvite(string pRequest)
        {
            var rd = new APIResponse<AcceptPowerHourInviteRD>();
            try
            {
                var rp = pRequest.DeserializeJSONTo<APIRequest<AcceptPowerHourInviteRP>>();
                if (rp.Parameters != null)
                    rp.Parameters.Validate();

                if (string.IsNullOrEmpty(rp.CustomerID))
                {
                    throw new APIException("无效的CustomerID【CustomerID】") { ErrorCode = 121 };
                }

                var loggingSessionInfo = Default.GetBSLoggingSession(rp.CustomerID, rp.UserID);

                //需要检查当前用户是否收到了邀请。
                PowerHourInviteBLL inviteBll = new PowerHourInviteBLL(loggingSessionInfo);
                PowerHourInviteEntity entity = inviteBll.GetBeforeUserInvite(rp.CustomerID, rp.Parameters.PowerHourID, rp.UserID);
                if (entity != null)     //接受了邀请
                {
                    entity.AcceptState = rp.Parameters.State;
                    inviteBll.Update(entity);
                }
                else
                {
                    rd.Message = "您未收到邀请";
                }

                var rdData = new AcceptPowerHourInviteRD();
                rdData.IsSuccess = true;
                rd.Data = rdData;
            }
            catch (Exception ex)
            {
                rd.ResultCode = 101;
                rd.Message = "业务处理失败";
            }

            return rd.ToJSON();
        }
        #endregion

        #region  MarkPowerHourAttendence  记录PowerHour Trainning的出席状态（到场/缺席状态）点到
        /// <summary>
        /// 记录PowerHour Trainning的参加/缺席人员
        /// 
        /// 根据学员的email，PowerHourID, 记录学员的出席状态。
        /// </summary>
        /// <param name="pRequest"></param>
        /// <returns></returns>
        private string MarkPowerHourAttendence(string pRequest)
        {
            var rd = new APIResponse<MarkPowerHourAttendenceRD>();
            var rdData = new MarkPowerHourAttendenceRD();
            try
            {
                var rp = pRequest.DeserializeJSONTo<APIRequest<MarkPowerHourAttendenceRP>>();
                if (rp.Parameters != null)
                    rp.Parameters.Validate();

                if (string.IsNullOrEmpty(rp.CustomerID))
                    throw new APIException("【CustomerID】不能为空") { ErrorCode = 121 };

                var loggingSessionInfo = Default.GetBSLoggingSession(rp.CustomerID, rp.UserID);

                string StaffUserID = string.Empty;
                T_UserBLL tubll = new T_UserBLL(loggingSessionInfo);
                string typeID = System.Configuration.ConfigurationManager.AppSettings["TypeID"].ToString();
                DataSet dsUser = tubll.GetUserByEmailList(rp.CustomerID, new List<string> { rp.Parameters.StaffUserEmail }, typeID);
                if (dsUser != null && dsUser.Tables != null && dsUser.Tables[0].Rows.Count > 0)
                    StaffUserID = dsUser.Tables[0].Rows[0]["user_id"].ToString();

                PowerHourBLL phbll = new PowerHourBLL(loggingSessionInfo);
                PgUserBLL pgBll = new PgUserBLL(loggingSessionInfo);
                DataSet ds = phbll.GetPowerHourInfo(rp.Parameters.PowerHourID, rp.CustomerID);
                DataRow drRow = ds.Tables[0].Rows[0];
                IdentityEnum identity = IdentityEnum.Staff;
                if (rp.UserID == drRow["TrainerID"].ToString())
                    identity = IdentityEnum.Trainer;
                else if (pgBll.VerifyIsLocalLuOwner(rp.UserID, rp.CustomerID, drRow["CityName"].ToString()))
                    identity = IdentityEnum.LocalLUOwner;
                else identity = IdentityEnum.Staff;

                ////检查用户是否是“工会主席”
                ////key:VIP020000 value:后续修改。
                //T_UserBLL bll = new T_UserBLL(loggingSessionInfo);
                //DataSet ds = bll.GetUserRights(rp.UserID, rp.CustomerID);
                //bool flag = ValidateUserPermission(m_LocalLeadKey, ds);

                if (identity == IdentityEnum.LocalLUOwner)
                {
                    //检查当前用户是否收到了邀请。
                    PowerHourInviteBLL inviteBll = new PowerHourInviteBLL(loggingSessionInfo);
                    PowerHourInviteEntity entity = inviteBll.GetBeforeUserInvite(rp.CustomerID, rp.Parameters.PowerHourID, StaffUserID);
                    if (entity != null)     //接受了邀请
                    {
                        entity.Attendence = rp.Parameters.Attendence;
                        inviteBll.Update(entity);
                        rdData.IsSuccess = true;
                    }
                    else
                    {
                        rd.Message = "该员工未收到邀请";
                        rd.ResultCode = 102;
                        rdData.IsSuccess = false;
                    }
                }
                else
                {
                    rd.Message = "您没有权限记录当前用户的出席状态";
                    rd.ResultCode = 102;
                    rdData.IsSuccess = false;
                }
            }
            catch (Exception ex)
            {
                rdData.IsSuccess = false;
                rd.ResultCode = 101;
                rd.Message = ex.Message;
            }
            rdData.ServerDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            rd.Data = rdData;
            return rd.ToJSON();
        }

        /// <summary>
        /// 验证用户是否有权限做某件事
        /// </summary>
        /// <param name="pRightKey"></param>
        /// <param name="pDsUserRight"></param>
        /// <returns></returns>
        public bool ValidateUserPermission(string pRightKey, DataSet pDsUserRight)
        {
            bool f = false;
            //key:VIP020000 value:创建讨论组权限
            if (pDsUserRight.Tables != null && pDsUserRight.Tables.Count > 0 && pDsUserRight.Tables[0] != null && pDsUserRight.Tables[0].Rows.Count > 0)
            {
                if (pDsUserRight.Tables[0].Select("Menu_Code='" + pRightKey + "'").Length > 0)
                    f = true;
            }
            return f;
        }

        #endregion

        #region  SavePowerHourSitePicutre   保存PowerHour Trainning现场照片
        /// <summary>
        /// 保存PowerHour Trainning现场照片，并更新PowerHour记录的字段
        /// </summary>
        /// <param name="pRequest"></param>
        /// <returns></returns>
        private string SavePowerHourSitePicutre(string pRequest)
        {
            var rd = new APIResponse<SavePowerHourSitePicutreRD>();
            var rdData = new SavePowerHourSitePicutreRD();
            try
            {
                var rp = pRequest.DeserializeJSONTo<EMAPIRequest<SavePowerHourSitePicutreRP>>();

                //rp.CustomerID = "17fe67e2b69e4b50b67e725939586459";
                //rp.UserID = "4C2BCFC00D014ABB9BC5D8B5E9AB88C7";
                //rp.Parameters.PowerHourID = "1362B01E-234F-43E9-ABD4-014AD477358A";
                //rp.Parameters.SitePictureUrl = "http://www.o2omarketing.cn:9004/File/pg/Image/20140812/73C34235D70946029B014A6E0DE90835_240.jpg";
                ////rp.Parameters.SitePictureUrl = "http://localhost:23130/File/pg/Image/20140618/D68D4CCC730046BEAC4EE9675F117C41.jpg";


                if (rp.Parameters != null)
                    rp.Parameters.Validate();

                if (string.IsNullOrEmpty(rp.CustomerID))
                {
                    throw new APIException("无效的CustomerID【CustomerID】") { ErrorCode = 121 };
                }

                var loggingSessionInfo = Default.GetBSLoggingSession(rp.CustomerID, rp.UserID);
                //1. 保存培训现场照片
                string plat = rp.Plat;
                UploadFileResp uploadFile = UploadImageData(HttpContext.Current, plat, rp.Parameters.SitePictureUrl);
                //2. 更新培训表中的“培训现场图片字段”
                PowerHourBLL bll = new PowerHourBLL(loggingSessionInfo);
                PowerHourEntity entity = bll.GetByID(rp.Parameters.PowerHourID);
                if (entity != null)
                {

                    #region 保存缩略图
                    T_UserBLL tubll = new T_UserBLL(loggingSessionInfo);
                    tubll.SaveImageThumbs(uploadFile.file, uploadFile.thumbs, rp.Parameters.PowerHourID, rp.CustomerID, rp.UserID, "0");
                    #endregion

                    if (uploadFile.thumbs.Count >= 2)
                        entity.SitePictureUrl = uploadFile.thumbs[1].url;
                    else
                        entity.SitePictureUrl = uploadFile.file.url;

                    bll.Update(entity);
                }
                if (uploadFile != null)
                {
                    rdData.UploadFile = uploadFile;
                    rdData.IsSuccess = true;
                    rd.ResultCode = 0;
                }
                else
                {
                    rdData.IsSuccess = false;
                    rd.ResultCode = 101;
                }

                rd.Data = rdData;
            }
            catch (Exception ex)
            {
                rd.ResultCode = 101;
                rd.Message = "业务处理失败";
            }
            return rd.ToJSON();
        }
        #endregion

        #region  GetPowerHourInviteState  根据PowerHourId, 返回全部受邀请学员的状态，是否接受了邀请（接受/拒绝/无反馈）
        /// <summary>
        /// 根据PowerHourId, 返回全部受邀请学员的状态，是否接受了邀请（接受/拒绝/无反馈）
        /// </summary>
        /// <param name="pRequest"></param>
        /// <returns></returns>
        private string GetPowerHourInviteState(string pRequest)
        {
            var rd = new APIResponse<PowerHourInviteStateRD>();
            try
            {
                var rp = pRequest.DeserializeJSONTo<APIRequest<PowerHourInviteStateRP>>();
                if (rp.Parameters != null)
                    rp.Parameters.Validate();

                if (string.IsNullOrEmpty(rp.CustomerID))
                {
                    throw new APIException("无效的CustomerID【CustomerID】") { ErrorCode = 121 };
                }

                var loggingSessionInfo = Default.GetBSLoggingSession(rp.CustomerID, rp.UserID);
                PowerHourBLL bll = new PowerHourBLL(loggingSessionInfo);
                DataSet ds = bll.GetPowerHourInviteState(rp.CustomerID, rp.Parameters.PowerHourID);
                List<PowerHourInviteStateModel> list = null;
                if (ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                {
                    list = DataTableToObject.ConvertToList<PowerHourInviteStateModel>(ds.Tables[0]);
                }

                if (list != null && list.Count > 0)
                {
                    List<PowerHourInviteStateModel> inviteStateList = new List<PowerHourInviteStateModel>();
                    PowerHourInviteStateModel model = null;
                    foreach (var m in list)
                    {
                        model = new PowerHourInviteStateModel();
                        model.UserID = m.UserID;
                        model.UserName = m.UserName;
                        model.AcceptState = m.AcceptState;
                        model.Email = m.Email;
                        //model.StrState = ConvertAcceptState(m.AcceptState);
                        inviteStateList.Add(model);
                    }

                    var rdData = new PowerHourInviteStateRD();
                    rdData.InviteStateList = inviteStateList;
                    rd.Data = rdData;
                }
            }
            catch (Exception ex)
            {
                rd.ResultCode = 101;
                rd.Message = "业务处理失败";
            }

            return rd.ToJSON();
        }
        #endregion

        #region  InviteJoinPowerHour 邀请员工参加 PowerHour
        /// <summary>
        /// 邀请员工参加 PowerHour
        /// </summary>
        /// <param name="pRequest"></param>
        /// <returns></returns>
        private string InviteJoinPowerHour(string pRequest)
        {
            var rd = new APIResponse<InviteJoinPowerHourRD>();
            var rdData = new InviteJoinPowerHourRD();
            try
            {
                var rp = pRequest.DeserializeJSONTo<APIRequest<InviteJoinPowerHourRP>>();
                if (rp.Parameters != null)
                    rp.Parameters.Validate();
                //List<string> list = new List<string>();
                //list.Add("039C8E9739824317A0FBC672E99F9E2A");
                //list.Add("03A1C1F3ECB2434F85012C1E00099282");
                //list.Add("03D358844E974F318BC72179D5EAB8D5");
                //rd.Data = new InviteJoinPowerHourRD();
                //rd.Data.UserList = list;
                //rp.Parameters.UserList

                var loggingSessionInfo = Default.GetBSLoggingSession(rp.CustomerID, rp.UserID);
                PowerHourInviteBLL bll = new PowerHourInviteBLL(loggingSessionInfo);
                T_UserBLL tubll = new T_UserBLL(loggingSessionInfo);
                string typeID = System.Configuration.ConfigurationManager.AppSettings["TypeID"].ToString();
                DataSet dsUser = tubll.GetUserByEmailList(rp.CustomerID, rp.Parameters.UserList, typeID);
                if (dsUser != null && dsUser.Tables != null && dsUser.Tables[0].Rows.Count > 0)
                {
                    List<string> userList = rp.Parameters.UserList;
                    List<string> FailUserList = new List<string>();
                    string strUserID = string.Empty;
                    DataRow[] drs = null;
                    foreach (var item in userList)
                    {
                        try
                        {
                            drs = dsUser.Tables[0].Select("user_email='" + item.ToLower() + "'");
                            if (drs == null || drs.Length <= 0)
                            {
                                FailUserList.Add(item);
                                continue;
                            }
                            PowerHourInviteEntity phie = bll.VerifyPowerHourInvite(rp.CustomerID, rp.Parameters.PowerHourID, drs[0]["user_id"].ToString());
                            if (phie == null)
                            {
                                PowerHourInviteEntity entity = new PowerHourInviteEntity();
                                entity.PowerHourInviteID = Guid.NewGuid();
                                entity.PowerHourID = rp.Parameters.PowerHourID;
                                entity.InvitorUserID = rp.UserID;
                                entity.StaffUserID = drs[0]["user_id"].ToString();//item
                                entity.AcceptState = 0;//默认未处理
                                entity.Attendence = 0;
                                entity.CustomerID = rp.CustomerID;
                                bll.Create(entity);
                            }
                        }
                        catch (Exception ex)
                        {
                            FailUserList.Add(item);
                        }
                    }
                    rdData.IsSuccess = true;
                    rdData.FailUserList = FailUserList;
                }
                else
                {
                    rdData.IsSuccess = false;
                    rd.ResultCode = 101;
                    rd.Message = "不存在Email集合";
                    rdData.FailUserList = rp.Parameters.UserList;
                }
                rd.Data = rdData;
            }
            catch (Exception ex)
            {
                rd.ResultCode = 101;
                rd.Message = "业务处理失败";
            }

            return rd.ToJSON();
        }
        #endregion

        #region  GetValuableTrainningComments  返回PowerHour得到的文字评论(获取第12条/第13条评论的答案)
        /// <summary>
        /// 获取PowerHour得到的全部Most Valuable  Comments.
        /// </summary>
        /// <param name="pRequest"></param>
        /// <returns></returns>
        private string GetValuableTrainningComments(string pRequest)
        {
            var rd = new APIResponse<MostValuableTrainningCommentRD>();
            try
            {
                var rp = pRequest.DeserializeJSONTo<APIRequest<MostValuableTrainningCommentRP>>();
                if (rp.Parameters != null)
                    rp.Parameters.Validate();

                if (string.IsNullOrEmpty(rp.CustomerID))
                {
                    throw new APIException("无效的CustomerID【CustomerID】") { ErrorCode = 121 };
                }

                var loggingSessionInfo = Default.GetBSLoggingSession(rp.CustomerID, rp.UserID);
                PowerHourBLL bll = new PowerHourBLL(loggingSessionInfo);
                DataSet ds = bll.GetTrainningComments(rp.CustomerID, rp.Parameters.PowerHourID, rp.Parameters.Index, rp.Parameters.PageIndex, rp.Parameters.PageSize);
                List<MostValuableTrainningCommentModel> list = null;
                if (ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                {
                    list = DataTableToObject.ConvertToList<MostValuableTrainningCommentModel>(ds.Tables[0]);
                }
                List<string> listString = new List<string>();
                if (list != null && list.Count > 0)
                {

                    foreach (var item in list)
                    {
                        listString.Add(item.Answer);
                    }
                }

                var rdData = new MostValuableTrainningCommentRD();
                rdData.AnswerList = listString;
                rd.Data = rdData;
            }
            catch (Exception ex)
            {
                rd.ResultCode = 101;
                rd.Message = "业务处理失败";
            }

            return rd.ToJSON();
        }
        #endregion

        #region  GetAvailablePowerHourTopics     获取DefaultTopic数据
        /// <summary>
        /// 获取注册PowerHour时可以用的所有Topic
        /// </summary>
        /// <param name="pRequest"></param>
        /// <returns></returns>
        private string GetAvailablePowerHourTopics(string pRequest)
        {
            var rd = new APIResponse<DefaultTopicRD>();
            try
            {
                var rp = pRequest.DeserializeJSONTo<APIRequest<DefaultTopicRP>>();
                if (rp.Parameters != null)
                    rp.Parameters.Validate();

                if (string.IsNullOrEmpty(rp.CustomerID))
                {
                    throw new APIException("无效的CustomerID【CustomerID】") { ErrorCode = 121 };
                }

                var loggingSessionInfo = Default.GetBSLoggingSession(rp.CustomerID, rp.UserID);
                DefaultTopicBLL bll = new DefaultTopicBLL(loggingSessionInfo);
                DefaultTopicEntity[] list = bll.GetAll();

                var values = from u in list
                             orderby u.Index ascending
                             select u;

                List<DefaultTopicModel> listModel = null;
                if (list != null && list.Length > 0)
                {
                    listModel = new List<DefaultTopicModel>();
                    foreach (var item in values)
                    {
                        DefaultTopicModel model = new DefaultTopicModel();
                        model.DefaultTopicID = item.DefaultTopicID.ToString();
                        model.Topic = item.Topic;
                        model.Index = item.Index.Value;
                        listModel.Add(model);
                    }
                }

                var rdData = new DefaultTopicRD();
                rdData.DefaultTopicList = listModel;
                rd.Data = rdData;
            }
            catch (Exception ex)
            {
                rd.ResultCode = 101;
                rd.Message = "业务处理失败";
            }

            return rd.ToJSON();
        }
        #endregion

        #region    GetPowerHourResult   获取讲座完成后的统计数据
        /// <summary>
        /// 获取PowerHour完成后的统计数据
        /// </summary>
        /// <param name="pRequest"></param>
        /// <returns></returns>
        private string GetPowerHourResult(string pRequest)
        {
            var rd = new APIResponse<PowerHourResultRD>();
            try
            {
                var rp = pRequest.DeserializeJSONTo<APIRequest<PowerHourResultRP>>();
                if (rp.Parameters != null)
                    rp.Parameters.Validate();

                if (string.IsNullOrEmpty(rp.CustomerID))
                {
                    throw new APIException("无效的CustomerID【CustomerID】") { ErrorCode = 121 };
                }

                var loggingSessionInfo = Default.GetBSLoggingSession(rp.CustomerID, rp.UserID);
                var rdData = new PowerHourResultRD();

                PowerHourBLL bll = new PowerHourBLL(loggingSessionInfo);
                PowerHourEntity entity = bll.GetByID(rp.Parameters.PowerHourID);
                if (entity != null)
                {
                    rdData.SitePictureUrl = entity.SitePictureUrl;
                }

                DataSet ds = bll.GetPowerHourAttendStaffInfo(rp.CustomerID, rp.Parameters.PowerHourID);
                if (ds != null && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                {
                    DataRow row = ds.Tables[0].Rows[0];
                    //出席人数
                    rdData.AttendCount = Convert.ToInt32(row["AttendenceStaffCount"]);
                    //缺席人数
                    rdData.AbsentCount = Convert.ToInt32(row["AbsentStaffCount"]);
                }

                DataSet reviewScoreDS = bll.GetPowerHourRemarkReviewScore(rp.CustomerID, rp.Parameters.PowerHourID);
                if (reviewScoreDS != null && reviewScoreDS.Tables[0] != null && reviewScoreDS.Tables[0].Rows.Count > 0)
                {
                    DataRow row = reviewScoreDS.Tables[0].Rows[0];
                    //评论数
                    rdData.CommentCount = Convert.ToInt32(row["ReviewCount"]);
                    //平均得分
                    rdData.PowerHourAvg = Convert.ToInt32(row["AvgScore"]);
                }

                rd.Data = rdData;
            }
            catch (Exception ex)
            {
                rd.ResultCode = 101;
                rd.Message = "业务处理失败";
            }

            return rd.ToJSON();
        }
        #endregion

        #region  GetLocalEmployeeCount 获取特定城市的员工人数
        /// <summary>
        /// 获取特定城市员工数
        /// </summary>
        /// <param name="pRequest"></param>
        /// <returns></returns>
        private string GetLocalEmployeeCount(string pRequest)
        {
            var rd = new APIResponse<LocalEmployeeCountRD>();
            try
            {
                var rp = pRequest.DeserializeJSONTo<APIRequest<LocalEmployeeCountRP>>();
                if (rp.Parameters != null)
                    rp.Parameters.Validate();

                var loggingSessionInfo = Default.GetBSLoggingSession(rp.CustomerID, rp.UserID);
                PowerHourBLL bll = new PowerHourBLL(loggingSessionInfo);
                int count = bll.GetLocalEmployeeCount(rp.CustomerID, rp.Parameters.CityID);

                var rdData = new LocalEmployeeCountRD();
                rdData.EmployeeCount = count;
                rd.Data = rdData;
            }
            catch (Exception ex)
            {
                rd.ResultCode = 101;
                rd.Message = "业务处理失败";
            }

            return rd.ToJSON();
        }
        #endregion

        #region  GetAllAvailablePowerHour   按财年获取所有有效的PowerHour(讲座)
        /// <summary>
        /// 按财年获取所有有效的PowerHour(讲座)
        /// </summary>
        /// <param name="pRequest"></param>
        /// <returns></returns>
        private string GetAllAvailablePowerHour(string pRequest)
        {
            var rd = new APIResponse<PowerHourListRD>();
            try
            {
                var rp = pRequest.DeserializeJSONTo<APIRequest<PowerHourListRP>>();
                if (rp.Parameters != null)
                    rp.Parameters.Validate();

                var loggingSessionInfo = Default.GetBSLoggingSession(rp.CustomerID, rp.UserID);

                var rdData = new PowerHourListRD();
                PowerHourBLL bll = new PowerHourBLL(loggingSessionInfo);
                List<PowerHourListModel> list = null;
                DataSet ds = bll.GetAllAvailablePowerHour(rp.CustomerID, rp.Parameters.FinanceYear);
                if (ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                {
                    list = DataTableToObject.ConvertToList<PowerHourListModel>(ds.Tables[0]);
                }

                rdData.PowerHourList = list;
                rd.Data = rdData;
                rd.ResultCode = 22;
            }
            catch (Exception ex)
            {
                rd.ResultCode = 101;
                rd.Message = "业务处理失败";
            }

            return rd.ToJSON();
        }

        #endregion

        #region   RegisterPowerHour   注册PowerHour
        /// <summary>
        /// 注册PowerHour
        /// </summary>
        /// <param name="pRequest"></param>
        /// <returns></returns>
        private string RegisterPowerHour(string pRequest)
        {
            var rd = new APIResponse<RegisterPowerHourRD>();
            try
            {
                var rp = pRequest.DeserializeJSONTo<APIRequest<RegisterPowerHourRP>>();
                if (rp.Parameters != null)
                    rp.Parameters.Validate();

                var loggingSessionInfo = Default.GetBSLoggingSession(rp.CustomerID, rp.UserID);

                //需要检查当前用户的权限是否可以创建PowerHour（要求BAND3以上）
                //key:VIP020000 value:创建讨论组权限  暂时使用 这个权限，后续修改。
                T_UserBLL userBLL = new T_UserBLL(loggingSessionInfo);
                //DataSet ds = userBLL.GetUserRights(rp.UserID, rp.CustomerID);
                //bool flag = ValidateUserPermission(m_LocalLeadKey, ds);

                bool flag = false;
                string typeID = System.Configuration.ConfigurationManager.AppSettings["TypeID"].ToString();
                DataSet ds = userBLL.GetPgUserByID(rp.CustomerID, rp.UserID, typeID);
                if (ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                {
                    //判断band3
                    if (Convert.ToInt32(ds.Tables[0].Rows[0]["JobFunc"]) >= 3)
                        flag = true;
                }
                if (flag)
                {
                    PowerHourBLL bll = new PowerHourBLL(loggingSessionInfo);
                    PowerHourEntity entity = new PowerHourEntity();
                    entity.PowerHourID = Guid.NewGuid();
                    entity.SiteAddress = rp.Parameters.SiteAddress;
                    entity.TrainerID = rp.Parameters.TrainerID;
                    entity.Topic = rp.Parameters.Topic;
                    entity.CityID = rp.Parameters.CityID;
                    entity.StartTime = rp.Parameters.StartTime;
                    entity.EndTime = rp.Parameters.EndTime;
                    entity.CustomerID = rp.CustomerID;
                    entity.FinanceYear = rp.Parameters.FinanceYear == null ? GetFiscalYear() : rp.Parameters.FinanceYear;//获取财年
                    entity.ApproveState = 0;
                    bll.Create(entity);

                    var rdData = new RegisterPowerHourRD();
                    rdData.PowerHourID = entity.PowerHourID.ToString();
                    rd.Data = rdData;
                }
                else
                {
                    rd.Message = "您无权限注册PowerHour";
                    rd.ResultCode = 102;
                }
            }
            catch (Exception ex)
            {
                rd.ResultCode = 101;
                rd.Message = "业务处理失败";
            }

            return rd.ToJSON();
        }
        #endregion

        #region   GetPowerHour  获取培训课程详细信息
        /// <summary>
        /// 根据Id返回PowerHour对象
        /// </summary>
        /// <param name="pRequest"></param>
        /// <returns></returns>
        private string GetPowerHour(string pRequest)
        {
            var rd = new APIResponse<PowerHourRD>();
            var rdData = new PowerHourRD();
            try
            {
                var rp = pRequest.DeserializeJSONTo<APIRequest<PowerHourRP>>();
                if (rp.Parameters != null)
                    rp.Parameters.Validate();
                var loggingSessionInfo = Default.GetBSLoggingSession(rp.CustomerID, rp.UserID);
                PowerHourBLL bll = new PowerHourBLL(loggingSessionInfo);
                PgUserBLL pgBll = new PgUserBLL(loggingSessionInfo);
                string powerHourID = rp.Parameters.PowerHourID;
                string customerID = rp.CustomerID;
                string userID = rp.UserID;

                DataSet ds = bll.GetPowerHourInfo(powerHourID, customerID);
                DataRow drRow = ds.Tables[0].Rows[0];
                //Power Hour是否结束
                bool phIsEnd = bll.VerifyPowerHourIsEnd(powerHourID, customerID);
                //是否可以邀请
                bool phIsInvite = bll.VerifyPowerHourIsInvite(powerHourID, customerID);

                //学员评论/评论内容
                MemberReviewRemarkModel mrrm = new MemberReviewRemarkModel();

                IdentityEnum identity = IdentityEnum.Staff;
                if (rp.UserID == drRow["TrainerID"].ToString())
                    identity = IdentityEnum.Trainer;
                else if (pgBll.VerifyIsLocalLuOwner(rp.UserID, rp.CustomerID, drRow["CityName"].ToString()))
                    identity = IdentityEnum.LocalLUOwner;
                else identity = IdentityEnum.Staff;

                if (identity == IdentityEnum.Staff)//普通员工
                {
                    DataSet ds1 = bll.VerifyPowerHourInvite(customerID, userID, powerHourID);
                    DataSet ds2 = bll.GetPowerHourRemarkForMember(customerID, powerHourID, userID);
                    if (ds2 != null && ds2.Tables[0] != null && ds2.Tables[0].Rows.Count > 0)
                    {
                        mrrm.DoReviewState = true;
                        mrrm.ReviewRemark = DataTableToObject.ConvertToList<PowerHourRemark>(ds2.Tables[0]);
                    }
                    else
                        mrrm.DoReviewState = false;
                    if (ds1 != null && ds1.Tables[0] != null && ds1.Tables[0].Rows.Count > 0)
                    {
                        //收到邀请
                        drRow["IsInvite"] = "1";
                        //接受状态 0未处理 1接受 2拒绝
                        drRow["AcceptState"] = ds1.Tables[0].Rows[0]["AcceptState"];
                        //出席状态 0未出席 1出席
                        drRow["Attendence"] = ds1.Tables[0].Rows[0]["Attendence"];

                        //学员是否可评论（出席，>讲课开始时间，<30天）
                        if (drRow["Attendence"] != null && drRow["Attendence"].ToString().Equals("1") && !phIsInvite
                            && (!drRow["ExceedState"].ToString().Equals("2")))
                        {
                            drRow["IsAgreeRemark"] = true;
                        }
                        else
                            drRow["IsAgreeRemark"] = false;
                    }
                    else
                    {
                        drRow["IsInvite"] = "0";//未收到邀请
                        drRow["AcceptState"] = "0";
                        drRow["Attendence"] = "0";
                    }

                    drRow["InviteRight"] = "0";
                }
                else if (identity == IdentityEnum.Trainer)
                {
                    drRow["IsInvite"] = 1;
                    drRow["AcceptState"] = 1;
                    drRow["Attendence"] = 1;

                    if (!phIsInvite)
                    {
                        //不可以邀请
                        drRow["InviteRight"] = "0";
                    }
                    else
                    {
                        //可以邀请
                        drRow["InviteRight"] = "1";
                    }

                    drRow["IsAgreeRemark"] = false;
                }
                else if (identity == IdentityEnum.LocalLUOwner)//工会主席
                {
                    if (!phIsInvite)
                    {
                        //不可以邀请
                        drRow["InviteRight"] = "0";
                    }
                    else
                    {
                        //可以邀请
                        drRow["InviteRight"] = "1";
                    }

                    drRow["IsAgreeRemark"] = true;
                }
                //出席人数、缺席人数、评论数、平均得分
                if (!phIsInvite)
                    drRow = SetPowerHourReview(customerID, powerHourID, drRow, bll, loggingSessionInfo);

                drRow["Flag"] = Convert.ToInt32(identity);

                //同开始时间比较 >StartTime
                drRow["IsAboveStartTime"] = !phIsInvite;
                //同结束时间比较 >EndTime
                drRow["IsAboveEndTime"] = phIsEnd;

                PowerHourRD phModel = DataTableToObject.ConvertToObject<PowerHourRD>(drRow);
                phModel.MemberReviewRemark = mrrm;

                //power hour 现场图片集合
                DataSet dsImage = bll.GetImageThumbsList(customerID, powerHourID, string.Empty);
                if (dsImage != null && dsImage.Tables[0] != null && dsImage.Tables[0].Rows.Count > 0)
                    phModel.SitePictureSet = DataTableToObject.ConvertToList<SitePictureModel>(dsImage.Tables[0]);

                if (phModel != null)
                {
                    rdData = phModel;
                    rd.Data = rdData;
                    rd.ResultCode = 0;
                }
                else
                {
                    rd.ResultCode = 101;
                    rd.Message = "获取培训课程为空";
                }
            }
            catch (Exception ex)
            {
                rd.ResultCode = 101;
                rd.Message = "业务处理失败";
            }
            return rd.ToJSON();
        }
        #endregion

        #region  GetCityPowerHourState  按财年获取城市PowerHour状态
        /// <summary>
        /// 按财年获取城市PowerHour状态
        /// </summary>
        /// <param name="pRequest"></param>
        /// <returns></returns>
        private string GetCityPowerHourState(string pRequest)
        {
            var rd = new APIResponse<CityPowerHourStateRD>();
            try
            {
                var rp = pRequest.DeserializeJSONTo<APIRequest<CityPowerHourStateRP>>();
                if (rp.Parameters != null)
                    rp.Parameters.Validate();

                var loggingSessionInfo = Default.GetBSLoggingSession(rp.CustomerID, rp.UserID);

                var rdData = new CityPowerHourStateRD();
                PowerHourBLL bll = new PowerHourBLL(loggingSessionInfo);
                List<CityPowerHourStateModel> list = null;
                DataSet ds = bll.GetCityPowerHourState(rp.CustomerID, rp.Parameters.FinanceYear);
                if (ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                {
                    list = DataTableToObject.ConvertToList<CityPowerHourStateModel>(ds.Tables[0]);
                }
                rdData.CityPowerHourStateList = list;
                rd.Data = rdData;
                rd.ResultCode = 0;
            }
            catch (Exception ex)
            {
                rd.ResultCode = 101;
                rd.Message = "业务处理失败";
            }

            return rd.ToJSON();
        }
        #endregion

        #region  GetSingleCityPowerHour 按财年获取某城市有效的PowerHour(讲座)
        /// <summary>
        /// 按财年获取某城市有效的PowerHour(讲座)
        /// </summary>
        /// <param name="pRequest"></param>
        /// <returns></returns>
        private string GetSingleCityPowerHour(string pRequest)
        {
            var rd = new APIResponse<PowerHourListRD>();
            try
            {
                var rp = pRequest.DeserializeJSONTo<APIRequest<SpecificCityPowerHourListRP>>();
                if (rp.Parameters != null)
                    rp.Parameters.Validate();

                var loggingSessionInfo = Default.GetBSLoggingSession(rp.CustomerID, rp.UserID);

                var rdData = new PowerHourListRD();
                PowerHourBLL bll = new PowerHourBLL(loggingSessionInfo);
                List<PowerHourListModel> list = null;
                DataSet ds = bll.GetSpecificCityAvailablePowerHour(rp.CustomerID, rp.Parameters.FinanceYear, rp.Parameters.CityID);
                if (ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                {
                    list = DataTableToObject.ConvertToList<PowerHourListModel>(ds.Tables[0]);
                }
                rdData.PowerHourList = list;
                rd.Data = rdData;
                rd.ResultCode = 0;
            }
            catch (Exception ex)
            {
                rd.ResultCode = 101;
                rd.Message = "业务处理失败";
            }

            return rd.ToJSON();
        }
        #endregion

        #region  Util
        /// <summary>
        /// 设置培训信息
        /// 出席人数、缺席人数、评论数、平均得分
        /// </summary>
        /// <param name="pCustomerID"></param>
        /// <param name="pPowerHourID"></param>
        /// <param name="row"></param>
        /// <param name="bll"></param>
        /// <param name="pLoggingSessionInfo"></param>
        /// <returns></returns>
        public DataRow SetPowerHourReview(string pCustomerID, string pPowerHourID, DataRow row, PowerHourBLL bll, LoggingSessionInfo pLoggingSessionInfo)
        {
            if (bll == null)
                bll = new PowerHourBLL(pLoggingSessionInfo);

            DataSet ds3 = bll.GetPowerHourAttendStaffInfo(pCustomerID, pPowerHourID);
            if (ds3 != null && ds3.Tables[0] != null && ds3.Tables[0].Rows.Count > 0)
            {
                DataRow row1 = ds3.Tables[0].Rows[0];
                //出席人数
                row["AttendenceStaffCount"] = row1["AttendenceStaffCount"];
                //缺席人数
                row["AbsentStaffCount"] = row1["AbsentStaffCount"];
            }
            DataSet ds4 = bll.GetPowerHourRemarkReviewScore(pCustomerID, pPowerHourID);
            if (ds4 != null && ds4.Tables[0] != null && ds4.Tables[0].Rows.Count > 0)
            {
                DataRow row2 = ds4.Tables[0].Rows[0];
                //评论数
                row["ReviewCount"] = row2["ReviewCount"];
                //平均得分
                row["AvgScore"] = row2["AvgScore"];
            }
            return row;
        }

        /// <summary>
        /// 邀请学员参加讲座状态枚举类定义。
        /// </summary>
        public enum AcceptStateEnum
        {
            /// <summary>
            /// 无反馈
            /// </summary>
            NoResponse = 0,

            /// <summary>
            /// 接受邀请
            /// </summary>
            Accept = 1,

            /// <summary>
            /// 拒绝邀请
            /// </summary>
            Reject = 2
        }

        public static string ConvertAcceptState(int state)
        {
            string ret = string.Empty;
            switch (state)
            {
                case 1:
                    ret = "接受";
                    break;
                case 2:
                    ret = "拒绝";
                    break;
                default:
                    ret = "无反馈";
                    break;
            }

            return ret;
        }

        /// <summary>
        /// 出席培训状态枚举类定义。
        /// </summary>
        public enum AttendenceEnum
        {
            /// <summary>
            /// 无反馈
            /// </summary>
            NoResponse = 0,

            /// <summary>
            /// 出席培训
            /// </summary>
            AtendenceTrain = 1,

            /// <summary>
            /// 未出席培训
            /// </summary>
            NoAtendenceTrain = 2
        }


        public static string ConvertAttendence(int state)
        {
            string ret = string.Empty;
            switch (state)
            {
                case 1:
                    ret = "出席";
                    break;
                default:
                    ret = "无反馈";
                    break;
            }

            return ret;
        }

        #endregion

        #region UploadImageData
        public UploadFileResp UploadImageData(HttpContext context, string pPlat, string pImgHttpUrl)
        {
            var respObj = new UploadFileResp();
            string folderPath = string.Empty;
            string savepath = "";
            string fileName = Common.Utils.NewGuid();
            string fileFullName = "";
            string extension = "";
            string fileLocation = string.Empty;
            string hostUrl = string.Empty;
            string host = ConfigurationManager.AppSettings["customer_service_url"];
            if (!host.EndsWith("/")) host += "/";

            folderPath = "File/pg/Image/" + Utils.GetTodayString() + "/";
            savepath = HttpContext.Current.Server.MapPath("~/" + folderPath);
            if (!Directory.Exists(savepath)) Directory.CreateDirectory(savepath);

            if (pPlat.ToLower() == "android".ToLower())
            {
                HttpPostedFile postedFile = context.Request.Files["FileUp"];
                if (postedFile == null) postedFile = context.Request.Files[0];
                if (postedFile == null || postedFile.ContentLength == 0)
                {
                    respObj.success = false;
                    respObj.msg = "文件不能为空";
                    respObj.file = new FileData();
                    return respObj;
                }
                extension = Path.GetExtension(postedFile.FileName).ToLower();
                fileFullName = fileName + extension;
                fileLocation = string.Format("{0}/{1}", savepath, fileFullName);
                postedFile.SaveAs(fileLocation);

            }
            else if (pPlat.ToLower() == "iPhone".ToLower())
            {
                if (string.IsNullOrEmpty(pImgHttpUrl))
                {
                    respObj.success = false;
                    respObj.msg = "url地址不能为空";
                    respObj.file = new FileData();
                    return respObj;
                }
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3;
                WebClient webClient = new WebClient();
                //下载到本地文件
                extension = Path.GetExtension(pImgHttpUrl).ToLower();
                fileFullName = fileName + extension;
                fileLocation = savepath + fileFullName;
                webClient.DownloadFile(pImgHttpUrl, fileLocation);
            }

            hostUrl += host + folderPath + fileName + extension;

            var oldImage = new FileInfo(fileLocation);

            System.Drawing.Image originalImage = System.Drawing.Image.FromFile(fileLocation);

            //生成缩略图
            respObj.thumbs = new List<FileData>();
            if (true)
            {
                int thumbWidth = 120;
                var thumbFullName = fileName + "_" + thumbWidth.ToString() + extension;
                var thumbLocation = string.Format("{0}/{1}_{2}{3}", savepath, fileName, thumbWidth, extension);
                if (MakeThumbnail(originalImage, thumbLocation, thumbWidth, thumbWidth, "W"))
                {
                    var thumbImage = new FileInfo(thumbLocation);
                    respObj.thumbs.Add(new FileData()
                    {
                        url = host + folderPath + thumbFullName,
                        name = thumbFullName,
                        extension = extension,
                        size = thumbImage.Length,
                        type = thumbWidth.ToString()
                    });
                }
            }
            if (true)
            {
                int thumbWidth = 240;
                var thumbFullName = fileName + "_" + thumbWidth.ToString() + extension;
                var thumbLocation = string.Format("{0}/{1}_{2}{3}", savepath, fileName, thumbWidth, extension);
                if (MakeThumbnail(originalImage, thumbLocation, thumbWidth, thumbWidth, "W"))
                {
                    var thumbImage = new FileInfo(thumbLocation);
                    respObj.thumbs.Add(new FileData()
                    {
                        url = host + folderPath + thumbFullName,
                        name = thumbFullName,
                        extension = extension,
                        size = thumbImage.Length,
                        type = thumbWidth.ToString()
                    });
                }
            }
            if (true)
            {
                int thumbWidth = 480;
                var thumbFullName = fileName + "_" + thumbWidth.ToString() + extension;
                var thumbLocation = string.Format("{0}/{1}_{2}{3}", savepath, fileName, thumbWidth, extension);
                if (MakeThumbnail(originalImage, thumbLocation, thumbWidth, thumbWidth, "W"))
                {
                    var thumbImage = new FileInfo(thumbLocation);
                    respObj.thumbs.Add(new FileData()
                    {
                        url = host + folderPath + thumbFullName,
                        name = thumbFullName,
                        extension = extension,
                        size = thumbImage.Length,
                        type = thumbWidth.ToString()
                    });
                }
            }
            if (true)
            {
                int thumbWidth = 960;
                var thumbFullName = fileName + "_" + thumbWidth.ToString() + extension;
                var thumbLocation = string.Format("{0}/{1}_{2}{3}", savepath, fileName, thumbWidth, extension);
                if (MakeThumbnail(originalImage, thumbLocation, thumbWidth, thumbWidth, "W"))
                {
                    var thumbImage = new FileInfo(thumbLocation);
                    respObj.thumbs.Add(new FileData()
                    {
                        url = host + folderPath + thumbFullName,
                        name = thumbFullName,
                        extension = extension,
                        size = thumbImage.Length,
                        type = thumbWidth.ToString()
                    });
                }
            }
            //respObj.o = true;
            respObj.success = true;
            respObj.msg = "";
            respObj.file = new FileData();
            respObj.file.url = hostUrl;
            respObj.file.name = fileFullName;
            respObj.file.extension = extension;
            respObj.file.size = oldImage.Length;
            if (originalImage != null) originalImage.Dispose();

            return respObj;
        }

        //public UploadFileResp UploadImageData(HttpContext context, string pPlat, string pImgHttpUrl)
        //{
        //    var respObj = new UploadFileResp();
        //    string folderPath = string.Empty;
        //    string savepath = "";
        //    string fileName = Common.Utils.NewGuid();
        //    string fileFullName = "";
        //    string extension = "";
        //    string fileLocation = string.Empty;
        //    string hostUrl = string.Empty;
        //    string host = ConfigurationManager.AppSettings["customer_service_url"];
        //    if (!host.EndsWith("/")) host += "/";

        //    folderPath = "File/pg/Image/" + Utils.GetTodayString() + "/";
        //    savepath = HttpContext.Current.Server.MapPath("~/" + folderPath);
        //    if (!Directory.Exists(savepath)) Directory.CreateDirectory(savepath);

        //    if (pPlat.ToLower() == "android".ToLower())
        //    {
        //        HttpPostedFile postedFile = context.Request.Files["FileUp"];
        //        if (postedFile == null) postedFile = context.Request.Files[0];
        //        if (postedFile == null || postedFile.ContentLength == 0)
        //        {
        //            respObj.success = false;
        //            respObj.msg = "文件不能为空";
        //            respObj.file = new FileData();
        //            return respObj;
        //        }
        //        extension = Path.GetExtension(postedFile.FileName).ToLower();
        //        fileFullName = fileName + extension;
        //        fileLocation = string.Format("{0}/{1}", savepath, fileFullName);
        //        postedFile.SaveAs(fileLocation);

        //    }
        //    else if (pPlat.ToLower() == "iPhone".ToLower())
        //    {
        //        if (string.IsNullOrEmpty(pImgHttpUrl))
        //        {
        //            respObj.success = false;
        //            respObj.msg = "url地址不能为空";
        //            respObj.file = new FileData();
        //            return respObj;
        //        }
        //        ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3;
        //        WebClient webClient = new WebClient();
        //        //下载到本地文件
        //        extension = Path.GetExtension(pImgHttpUrl).ToLower();
        //        fileFullName = fileName + extension;
        //        fileLocation = savepath + fileFullName;
        //        webClient.DownloadFile(pImgHttpUrl, fileLocation);
        //    }

        //    hostUrl += host + folderPath + fileName + extension;

        //    var oldImage = new FileInfo(fileLocation);

        //    System.Drawing.Image originalImage = System.Drawing.Image.FromFile(fileLocation);

        //    //生成缩略图
        //    respObj.thumbs = new List<FileData>();
        //    if (true)
        //    {
        //        int thumbWidth = 120;
        //        var thumbFullName = fileName + "_" + thumbWidth.ToString() + extension;
        //        var thumbLocation = string.Format("{0}/{1}_{2}{3}", savepath, fileName, thumbWidth, extension);
        //        if (MakeThumbnail(originalImage, thumbLocation, thumbWidth, thumbWidth, "W"))
        //        {
        //            var thumbImage = new FileInfo(thumbLocation);
        //            respObj.thumbs.Add(new FileData()
        //            {
        //                url = host + folderPath + thumbFullName,
        //                name = thumbFullName,
        //                extension = extension,
        //                size = thumbImage.Length,
        //                type = thumbWidth.ToString()
        //            });
        //        }
        //    }
        //    if (true)
        //    {
        //        int thumbWidth = 240;
        //        var thumbFullName = fileName + "_" + thumbWidth.ToString() + extension;
        //        var thumbLocation = string.Format("{0}/{1}_{2}{3}", savepath, fileName, thumbWidth, extension);
        //        if (MakeThumbnail(originalImage, thumbLocation, thumbWidth, thumbWidth, "W"))
        //        {
        //            var thumbImage = new FileInfo(thumbLocation);
        //            respObj.thumbs.Add(new FileData()
        //            {
        //                url = host + folderPath + thumbFullName,
        //                name = thumbFullName,
        //                extension = extension,
        //                size = thumbImage.Length,
        //                type = thumbWidth.ToString()
        //            });
        //        }
        //    }
        //    if (true)
        //    {
        //        int thumbWidth = 480;
        //        var thumbFullName = fileName + "_" + thumbWidth.ToString() + extension;
        //        var thumbLocation = string.Format("{0}/{1}_{2}{3}", savepath, fileName, thumbWidth, extension);
        //        if (MakeThumbnail(originalImage, thumbLocation, thumbWidth, thumbWidth, "W"))
        //        {
        //            var thumbImage = new FileInfo(thumbLocation);
        //            respObj.thumbs.Add(new FileData()
        //            {
        //                url = host + folderPath + thumbFullName,
        //                name = thumbFullName,
        //                extension = extension,
        //                size = thumbImage.Length,
        //                type = thumbWidth.ToString()
        //            });
        //        }
        //    }
        //    if (true)
        //    {
        //        int thumbWidth = 960;
        //        var thumbFullName = fileName + "_" + thumbWidth.ToString() + extension;
        //        var thumbLocation = string.Format("{0}/{1}_{2}{3}", savepath, fileName, thumbWidth, extension);
        //        if (MakeThumbnail(originalImage, thumbLocation, thumbWidth, thumbWidth, "W"))
        //        {
        //            var thumbImage = new FileInfo(thumbLocation);
        //            respObj.thumbs.Add(new FileData()
        //            {
        //                url = host + folderPath + thumbFullName,
        //                name = thumbFullName,
        //                extension = extension,
        //                size = thumbImage.Length,
        //                type = thumbWidth.ToString()
        //            });
        //        }
        //    }
        //    //respObj.o = true;
        //    respObj.success = true;
        //    respObj.msg = "";
        //    respObj.file = new FileData();
        //    respObj.file.url = hostUrl;
        //    respObj.file.name = fileFullName;
        //    respObj.file.extension = extension;
        //    respObj.file.size = oldImage.Length;
        //    if (originalImage != null) originalImage.Dispose();

        //    return respObj;
        //}

        /// <summary>
        /// 生成缩略图
        /// </summary>
        /// <param name="originalImagePath">源图路径（物理路径）</param>
        /// <param name="thumbnailPath">缩略图路径（物理路径）</param>
        /// <param name="width">缩略图宽度</param>
        /// <param name="height">缩略图高度</param>
        /// <param name="mode">生成缩略图的方式</param>
        public bool MakeThumbnail(System.Drawing.Image originalImage, string thumbnailPath, int width, int height, string mode)
        {
            //System.Drawing.Image originalImage = System.Drawing.Image.FromFile(originalImagePath);
            int towidth = width;
            int toheight = height;
            int x = 0;
            int y = 0;
            int ow = originalImage.Width;
            int oh = originalImage.Height;

            //如果原图的尺寸比缩略图要求的尺寸小,则不进行任何处理
            //if (originalImage.Width <= width && originalImage.Height <= height)
            if (originalImage.Width < width && originalImage.Height < height)
            {
                return false;
            }

            switch (mode)
            {
                case "HW"://指定高宽缩放（可能变形）                
                    break;
                case "W"://指定宽，高按比例                    
                    toheight = originalImage.Height * width / originalImage.Width;
                    break;
                case "H"://指定高，宽按比例
                    towidth = originalImage.Width * height / originalImage.Height;
                    break;
                case "Cut"://指定高宽裁减（不变形）                
                    if ((double)originalImage.Width / (double)originalImage.Height > (double)towidth / (double)toheight)
                    {
                        oh = originalImage.Height;
                        ow = originalImage.Height * towidth / toheight;
                        y = 0;
                        x = (originalImage.Width - ow) / 2;
                    }
                    else
                    {
                        ow = originalImage.Width;
                        oh = originalImage.Width * height / towidth;
                        x = 0;
                        y = (originalImage.Height - oh) / 2;
                    }
                    break;
                default:
                    break;
            }

            //新建一个bmp图片
            System.Drawing.Image bitmap = new System.Drawing.Bitmap(towidth, toheight);

            //新建一个画板
            System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(bitmap);

            //设置高质量插值法
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;

            //设置高质量,低速度呈现平滑程度
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

            //清空画布并以透明背景色填充
            g.Clear(System.Drawing.Color.Transparent);

            //在指定位置并且按指定大小绘制原图片的指定部分
            g.DrawImage(originalImage, new System.Drawing.Rectangle(0, 0, towidth, toheight),
                new System.Drawing.Rectangle(x, y, ow, oh),
                System.Drawing.GraphicsUnit.Pixel);

            try
            {
                //以jpg格式保存缩略图
                bitmap.Save(thumbnailPath, System.Drawing.Imaging.ImageFormat.Jpeg);
                return true;
            }
            catch (System.Exception e)
            {
                return false;
                throw e;

            }
            finally
            {
                //originalImage.Dispose();
                bitmap.Dispose();
                g.Dispose();
            }
        }
        #endregion

        #region 返回当前用户是否是特定城市的工会主席
        /// <summary>
        /// 返回当前用户是否是特定城市的工会主席
        /// </summary>
        /// <param name="pRequest"></param>
        /// <returns></returns>
        public string VerifyIsLocalLuOwner(string pRequest)
        {
            var rd = new APIResponse<VerifyLocalLuOwnerRD>();
            var rdData = new VerifyLocalLuOwnerRD();
            try
            {
                var rp = pRequest.DeserializeJSONTo<APIRequest<VerifyLocalLuOwnerRP>>();
                if (rp.Parameters != null)
                    rp.Parameters.Validate();
                var loggingSessionInfo = Default.GetBSLoggingSession(rp.CustomerID, rp.UserID);
                PgUserBLL bll = new PgUserBLL(loggingSessionInfo);
                bool f = bll.VerifyIsLocalLuOwner(rp.UserID, rp.CustomerID, rp.Parameters.CityName);
                rdData.IsLocalLuOwner = f;
                rd.Data = rdData;
            }
            catch (Exception ex)
            {
                rd.ResultCode = 101;
                rd.Message = ex.Message;
            }
            return rd.ToJSON();
        }
        #endregion

        #region 修改Power Hour 培训地址
        /// <summary>
        ///  修改Power Hour 培训地址
        /// </summary>
        /// <param name="pRequest"></param>
        /// <returns></returns>
        public string ModifyPowerHourSiteAddress(string pRequest)
        {
            var rd = new APIResponse<ModifySiteAddressRD>();
            var rdData = new ModifySiteAddressRD();
            try
            {
                var rp = pRequest.DeserializeJSONTo<APIRequest<ModifySiteAddressRP>>();
                if (rp.Parameters != null)
                    rp.Parameters.Validate();
                var loggingSessionInfo = Default.GetBSLoggingSession(rp.CustomerID, rp.UserID);
                PowerHourBLL bll = new PowerHourBLL(loggingSessionInfo);
                PowerHourEntity phe = bll.GetByID(rp.Parameters.PowerHourID);
                if (phe != null)
                {
                    phe.SiteAddress = rp.Parameters.SiteAddress;
                    bll.Update(phe);
                    rdData.IsSuccess = true;
                    rd.ResultCode = 0;
                }
                else
                {
                    rdData.IsSuccess = false;
                    rd.Message = "Power Hour 不存在";
                    rd.ResultCode = 101;
                }
                rd.Data = rdData;
            }
            catch (Exception ex)
            {
                rd.ResultCode = 101;
                rd.Message = ex.Message;
            }
            return rd.ToJSON();
        }
        #endregion

        #region 请求宝洁报表公共方法
        /// <summary>
        ///  请求宝洁报表
        /// </summary>
        /// <param name="pRequest"></param>
        /// <returns></returns>
        public string CommonRequestReportForm(string pRequest)
        {
            var rd = new APIResponse<CommonRequestReportFormRD>();
            var rdData = new CommonRequestReportFormRD();
            try
            {
                var rp = pRequest.DeserializeJSONTo<APIRequest<CommonRequestReportFormRP>>();

                if (rp.Parameters == null) throw new ArgumentException();

                if (rp.Parameters != null)
                    rp.Parameters.Validate();

                PGHelper pghelper = new PGHelper();
                rdData.JsonResult = pghelper.RequestReport(rp.Parameters.PGToken, rp.Parameters.ReportAction, rp.Parameters.DynamicParam.Replace("$", "&"));
                rd.Data = rdData;
            }
            catch (Exception ex)
            {
                rd.ResultCode = 101;
                rd.Message = ex.Message;
            }
            return rd.ToJSON();
        }
        #endregion


        #region 记录访问
        public string VisitLog(string pRequest)
        {
            var rd = new APIResponse<VisitRD>();
            var rdData = new VisitRD();
            rdData.IsSuccess = true;
            rd.Data = rdData;
            rd.ResultCode = 0;
            return rd.ToJSON();
        }
        #endregion


        #region 工具方法
        public int GetFiscalYear()
        {
            int fiscalYear = DateTime.Now.Year;
            //当年7.1-次年6.30为一个财年
            //2012.7.1-2013.6.30为2012财年
            //2013.7.1-2014.6.30为2013财年
            DateTime dtSplit = Convert.ToDateTime(fiscalYear + "-07-01");
            if (dtSplit <= DateTime.Now)
                fiscalYear++;
            return fiscalYear;
        }
        #endregion
    }

    #region 请求参数及响应结果的数据结构

    #region 对课程进行评分
    public class CommentPowerHourRP : IAPIRequestParameter
    {
        /// <summary>
        /// 培训ID
        /// </summary>
        public String PowerHourID { get; set; }

        /// <summary>
        /// 评论集合
        /// </summary>
        //public List<CommentData> CommentList { get; set; }
        public List<PowerHourRemark> CommentList { get; set; }

        #region IAPIRequestParameter 成员
        public void Validate()
        {
            if (string.IsNullOrEmpty(PowerHourID))
            {
                throw new APIException("培训ID[PowerHourID]不能为空");
            }
        }

        #endregion
    }

    public class CommentData
    {
        /// <summary>
        /// 问题索引
        /// </summary>
        public Int32 Index { get; set; }

        /// <summary>
        /// 答题结果
        /// </summary>
        public String Answer { get; set; }
    }

    public class CommentPowerHourRD : IAPIResponseData
    {
        public bool IsSuccess { get; set; }
    }
    #endregion

    #region 根据PowerHourID返回所有接受邀请的学员的"出席"状态
    public class AcceptInviteRP : IAPIRequestParameter
    {
        public string PowerHourID { get; set; }

        #region IAPIRequestParameter 成员
        public void Validate()
        {
            if (string.IsNullOrEmpty(PowerHourID))
            {
                throw new APIException("无效的培训课程ID[PowerHourID]") { ErrorCode = 121 };
            }
        }

        #endregion
    }

    public class AcceptInviteRD : IAPIResponseData
    {
        /// <summary>
        /// 接受邀请的学员的"出席"状态列表
        /// </summary>
        public List<AcceptInviteModel> StateList { get; set; }
    }
    public class AcceptInviteModel
    {
        /// <summary>
        /// UserID
        /// </summary>
        public string UserID { get; set; }

        /// <summary>
        /// 学员名称
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 邮箱
        /// </summary>
        public string Email { set; get; }

        /// <summary>
        /// 出席状态表述
        /// </summary>
        //public string AttendenceState { get; set; }

        /// <summary>
        /// 出席状态标识
        /// </summary>
        public int Attendence { get; set; }
    }
    #endregion

    #region 记录当前用户接受/拒绝PowerHour邀请的状态
    public class AcceptPowerHourInviteRP : IAPIRequestParameter
    {
        /// <summary>
        /// 讲座ID
        /// </summary>
        public string PowerHourID { get; set; }

        /// <summary>
        /// 接受状态
        /// </summary>
        public int State { get; set; }

        #region IAPIRequestParameter 成员
        public void Validate()
        {
            if (string.IsNullOrEmpty(PowerHourID))
            {
                throw new APIException("无效的培训课程ID[PowerHourID]") { ErrorCode = 121 };
            }
        }

        #endregion
    }

    public class AcceptPowerHourInviteRD : IAPIResponseData
    {
        public bool IsSuccess { get; set; }
    }
    #endregion

    #region 记录PowerHour Trainning的出席状态（到场/缺席状态）点到
    public class MarkPowerHourAttendenceRP : IAPIRequestParameter
    {
        /// <summary>
        /// 课程ID
        /// </summary>
        public string PowerHourID { get; set; }

        /// <summary>
        /// 员工邮箱
        /// </summary>
        public string StaffUserEmail { set; get; }

        /// <summary>
        /// 出席状态
        /// </summary>
        public int? Attendence { get; set; }

        #region IAPIRequestParameter 成员
        public void Validate()
        {
            if (string.IsNullOrEmpty(PowerHourID))
                throw new APIException("无效的[PowerHourID]") { ErrorCode = 101 };

            if (string.IsNullOrEmpty(StaffUserEmail))
                throw new APIException("【StaffUserEmail】不能为空");
            if (Attendence == null)
                throw new APIException("【Attendence】不能为空");
        }
        #endregion
    }

    public class MarkPowerHourAttendenceRD : IAPIResponseData
    {
        public bool IsSuccess { get; set; }
        /// <summary>
        /// 服务器时间
        /// </summary>
        public string ServerDateTime { set; get; }
    }
    #endregion

    #region 保存PowerHour Trainning现场照片
    public class SavePowerHourSitePicutreRP : IAPIRequestParameter
    {
        /// <summary>
        /// 培训ID
        /// </summary>
        public string PowerHourID { get; set; }

        /// <summary>
        /// 培训现场照片
        /// </summary>
        public string SitePictureUrl { get; set; }

        #region IAPIRequestParameter 成员
        public void Validate()
        {
            if (string.IsNullOrEmpty(PowerHourID))
            {
                throw new APIException("请提供PowerHourID");
            }
        }
        #endregion
    }

    public class SavePowerHourSitePicutreRD : IAPIResponseData
    {
        public UploadFileResp UploadFile { set; get; }
        public bool IsSuccess { get; set; }
    }
    #endregion

    #region 根据PowerHourId, 返回全部受邀请学员的状态，是否接受了邀请（接受/拒绝/无反馈）
    public class PowerHourInviteStateRP : IAPIRequestParameter
    {
        /// <summary>
        /// 课程ID
        /// </summary>
        public string PowerHourID { get; set; }

        /// <summary>
        /// 每页显示条数
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// 当前页
        /// </summary>
        public int PageIndex { get; set; }

        #region IAPIRequestParameter 成员
        public void Validate()
        {
            if (string.IsNullOrEmpty(PowerHourID))
            {
                throw new APIException("无效的[PowerHourID]") { ErrorCode = 101 };
            }
        }
        #endregion
    }

    public class PowerHourInviteStateRD : IAPIResponseData
    {
        /// <summary>
        /// 答案
        /// </summary>
        public List<PowerHourInviteStateModel> InviteStateList { set; get; }
    }

    public class PowerHourInviteStateModel
    {
        /// <summary>
        /// UserID
        /// </summary>
        public string UserID { get; set; }

        /// <summary>
        /// 学员名称
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 邮箱
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// 接受状态描述
        /// </summary>
        //public string StrState { get; set; }

        /// <summary>
        /// 接受邀请状态
        /// </summary>
        public int AcceptState { get; set; }
    }
    #endregion

    #region 邀请员工参加 PowerHour
    public class InviteJoinPowerHourRP : IAPIRequestParameter
    {
        /// <summary>
        /// 培训课程ID
        /// </summary>
        public string PowerHourID { get; set; }

        /// <summary>
        /// 被邀请人Email列表
        /// </summary>
        public List<string> UserList { get; set; }

        #region IAPIRequestParameter 成员

        public void Validate()
        {
            if (string.IsNullOrEmpty(PowerHourID))
            {
                throw new APIException("无效的培训课程ID[PowerHourID]") { ErrorCode = 121 };
            }

            if (UserList != null && UserList.Count <= 0)
            {
                throw new APIException("邀请邮箱列表不能为空") { ErrorCode = 121 };
            }
        }
        #endregion
    }

    public class InviteJoinPowerHourRD : IAPIResponseData
    {
        public bool IsSuccess { get; set; }
        /// <summary>
        /// 邀请失败的Email列表
        /// </summary>
        public List<string> FailUserList { get; set; }
    }
    #endregion

    #region 返回PowerHour得到的文字评论(获取第12条/第13条评论的答案)
    public class MostValuableTrainningCommentRP : IAPIRequestParameter
    {
        /// <summary>
        /// 课程ID
        /// </summary>
        public string PowerHourID { get; set; }

        /// <summary>
        /// 索引：1,12 ；2,13
        /// </summary>
        public int Index { get; set; }

        /// <summary>
        /// 每页显示条数
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// 当前页
        /// </summary>
        public int PageIndex { get; set; }

        #region IAPIRequestParameter 成员
        public void Validate()
        {
            if (string.IsNullOrEmpty(PowerHourID))
            {
                throw new APIException("无效的[PowerHourID]") { ErrorCode = 101 };
            }

            if (Index == 0)
            {
                throw new APIException("无效的[索引[Index]]") { ErrorCode = 102 };
            }
        }
        #endregion
    }

    public class MostValuableTrainningCommentRD : IAPIResponseData
    {
        /// <summary>
        /// 答案
        /// </summary>
        public List<string> AnswerList { set; get; }
    }

    public class MostValuableTrainningCommentModel
    {
        public Int64 Row { get; set; }

        public string Answer { get; set; }
    }
    #endregion

    #region 获取DefaultTopic数据
    public class DefaultTopicRD : IAPIResponseData
    {
        public List<DefaultTopicModel> DefaultTopicList { get; set; }
    }

    /// <summary>
    /// 默认Topic RP
    /// </summary>
    public class DefaultTopicRP : IAPIRequestParameter
    {
        #region IAPIRequestParameter 成员
        public void Validate()
        {

        }

        #endregion
    }

    public class DefaultTopicModel
    {
        public string DefaultTopicID { get; set; }

        public string Topic { get; set; }

        public int Index { get; set; }
    }
    #endregion

    #region 获取讲座完成后的统计数据
    public class PowerHourResultRP : IAPIRequestParameter
    {
        /// <summary>
        /// 讲座ID
        /// </summary>
        public string PowerHourID { get; set; }

        #region IAPIRequestParameter 成员
        public void Validate()
        {
            if (string.IsNullOrEmpty(PowerHourID))
            {
                throw new APIException("PowerHourID不能为空！") { ErrorCode = 123 };
            }
        }

        #endregion
    }

    public class PowerHourResultRD : IAPIResponseData
    {
        /// <summary>
        /// 培训现场照片Url
        /// </summary>
        public string SitePictureUrl { get; set; }

        /// <summary>
        /// 出席人数
        /// </summary>
        public int AttendCount { get; set; }

        /// <summary>
        /// 缺席人数
        /// </summary>
        public int AbsentCount { get; set; }

        /// <summary>
        /// 评论数
        /// </summary>
        public int CommentCount { get; set; }

        /// <summary>
        /// 讲座平均分
        /// </summary>
        public int PowerHourAvg { get; set; }
    }
    #endregion

    #region 获取特定城市的员工人数
    public class LocalEmployeeCountRP : IAPIRequestParameter
    {
        public string CityID { get; set; }

        #region IAPIRequestParameter 成员
        public void Validate()
        {
            if (string.IsNullOrEmpty(CityID))
            {
                throw new APIException("城市ID[CityID]") { ErrorCode = 122 };
            }
        }

        #endregion
    }

    public class LocalEmployeeCountRD : IAPIResponseData
    {
        public int EmployeeCount { get; set; }
    }

    #endregion

    #region 按财年获取所有有效的PowerHour(讲座)
    public class PowerHourListRP : IAPIRequestParameter
    {
        /// <summary>
        /// 财年
        /// </summary>
        public int FinanceYear { get; set; }

        #region IAPIRequestParameter 成员
        public void Validate()
        {
            if (FinanceYear == 0)
            {
                throw new APIException("无效的财年信息【FinanceYear】") { ErrorCode = 123 };
            }
        }

        #endregion
    }

    public class PowerHourListRD : IAPIResponseData
    {
        public List<PowerHourListModel> PowerHourList { get; set; }
    }

    public class PowerHourListModel
    {
        /// <summary>
        ///  培训ID
        /// </summary>
        public Guid PowerHourID { get; set; }

        /// <summary>
        /// 培训地址
        /// </summary>
        public string SiteAddress { get; set; }

        /// <summary>
        /// 培训讲师的姓名
        /// </summary>
        public string TrainerName { get; set; }

        /// <summary>
        /// 培训主题
        /// </summary>
        public string Topic { get; set; }

        /// <summary>
        /// 培训所在城市名称
        /// </summary>
        public string CityName { get; set; }

        /// <summary>
        /// 培训开始时间
        /// </summary>
        public DateTime StartTime { get; set; }

        /// <summary>
        /// 培训结束时间
        /// </summary>
        public DateTime EndTime { get; set; }

        /// <summary>
        /// 培训现场照片
        /// </summary>
        public String SitePictureUrl { get; set; }

        /// <summary>
        /// 确定参加人数
        /// </summary>
        public int ConfirmNum { get; set; }

        /// <summary>
        /// 城市标识
        /// </summary>
        public Guid? CityID { set; get; }
        /// <summary>
        /// 经度
        /// </summary>
        public decimal? Longitude { set; get; }
        /// <summary>
        /// 纬度
        /// </summary>
        public decimal? Latitude { set; get; }

    }
    #endregion

    #region 注册PowerHour

    public class RegisterPowerHourRP : IAPIRequestParameter
    {
        /// <summary>
        /// 培训地址
        /// </summary>
        public String SiteAddress { get; set; }

        /// <summary>
        /// 培训讲师的UserID(外键)
        /// </summary>
        public String TrainerID { get; set; }

        /// <summary>
        /// 培训主题
        /// </summary>
        public String Topic { get; set; }

        /// <summary>
        /// 培训所在城市(外键)
        /// </summary>
        public String CityID { get; set; }

        /// <summary>
        ///     培训开始时间
        /// </summary>
        public DateTime? StartTime { get; set; }

        /// <summary>
        /// 培训结束时间
        /// </summary>
        public DateTime? EndTime { get; set; }

        /// <summary>
        /// 培训现场照片
        /// </summary>
        public String SitePictureUrl { get; set; }

        /// <summary>
        /// 培训所属财年
        /// </summary>
        public Int32? FinanceYear { get; set; }

        #region IAPIRequestParameter 成员
        public void Validate()
        {

            if (string.IsNullOrEmpty(TrainerID))
            {
                throw new APIException("讲师[TrainerID]不能为空");
            }
            if (string.IsNullOrEmpty(Topic))
            {
                throw new APIException("培训主题[Topic]不能为空");
            }
            if (string.IsNullOrEmpty(CityID))
            {
                throw new APIException("培训所在城市[CityID]不能为空");
            }
        }

        #endregion
    }

    public class RegisterPowerHourRD : IAPIResponseData
    {
        /// <summary>
        /// 培训ID
        /// </summary>
        public string PowerHourID { get; set; }
    }
    #endregion

    #region 获取培训课程详细信息
    public class PowerHourRP : IAPIRequestParameter
    {
        /// <summary>
        /// Power Hour ID
        /// </summary>
        public string PowerHourID { get; set; }

        #region IAPIRequestParameter 成员
        public void Validate()
        {
            if (string.IsNullOrEmpty(PowerHourID))
            {
                throw new APIException("[PowerHourID]不能为空") { ErrorCode = 121 };
            }
        }
        #endregion
    }

    public class PowerHourRD : IAPIResponseData
    {
        /// <summary>
        /// 主题
        /// </summary>
        public string Topic { set; get; }
        /// <summary>
        /// 讲师名称
        /// </summary>
        public string TrainerName { set; get; }
        /// <summary>
        /// City名称
        /// </summary>
        public string CityName { set; get; }
        /// <summary>
        /// PH开始时间
        /// </summary>
        public DateTime StartTime { set; get; }
        /// <summary>
        /// PH结束时间
        /// </summary>
        public DateTime EndTime { set; get; }
        /// <summary>
        /// 是否收到邀请
        /// </summary>
        public string IsInvite { set; get; }
        /// <summary>
        /// 是否已接受邀请
        /// </summary>
        public string AcceptState { set; get; }
        /// <summary>
        /// 当地员工数
        /// </summary>
        public int LocalStaffCount { set; get; }
        /// <summary>
        /// 是否可以邀请
        /// </summary>
        public string InviteRight { set; get; }
        /// <summary>
        /// 是否出席了PH
        /// </summary>
        public string Attendence { set; get; }
        /// <summary>
        /// 现场照片链接
        /// </summary>
        public string SitePictureUrl { set; get; }
        /// <summary>
        /// 出席人数
        /// </summary>
        public string AttendenceStaffCount { set; get; }
        /// <summary>
        /// 缺席人数
        /// </summary>
        public string AbsentStaffCount { set; get; }
        /// <summary>
        /// 评论数
        /// </summary>
        public string ReviewCount { set; get; }
        /// <summary>
        /// 平均得分
        /// </summary>
        public string AvgScore { set; get; }

        /// <summary>
        /// 身份标识
        /// </summary>
        public string Flag { set; get; }

        /// <summary>
        /// PowerHour过期状态
        /// 0未过期
        /// 1过期少于30天
        /// 2过期大于30天
        /// </summary>
        public int ExceedState { set; get; }

        public string IsAboveStartTime { set; get; }

        public string IsAboveEndTime { set; get; }
        /// <summary>
        /// 是否可以评论
        /// </summary>
        public string IsAgreeRemark { set; get; }

        /// <summary>
        /// 评论详细
        /// </summary>
        public MemberReviewRemarkModel MemberReviewRemark { set; get; }

        public List<SitePictureModel> SitePictureSet { set; get; }
    }

    public class SitePictureModel
    {
        public string ImageID { set; get; }
        public string ObjectID { set; get; }
        public string ImageURL { set; get; }
        //public int? DisplayIndex { set; get; }
        public string Title { set; get; }
        public string Description { set; get; }
    }

    /// <summary>
    /// 枚举Power Hour身份
    /// </summary>
    public enum IdentityEnum
    {
        /// <summary>
        /// 未知
        /// </summary>
        Unknown = 0,
        /// <summary>
        /// 普通员工
        /// </summary>
        Staff = 1,
        /// <summary>
        /// 工会主席
        /// </summary>
        LocalLUOwner = 2,
        /// <summary>
        /// 讲师
        /// </summary>
        Trainer = 3
    }

    public class MemberReviewRemarkModel
    {
        /// <summary>
        /// 是否已经做出评论
        /// </summary>
        public bool DoReviewState { set; get; }
        /// <summary>
        /// 评论/评分内容
        /// </summary>
        public List<PowerHourRemark> ReviewRemark { set; get; }
    }

    public class PowerHourRemark
    {
        /// <summary>
        /// 问题索引
        /// </summary>
        public int QuestionIndex { set; get; }
        /// <summary>
        /// 答题结果
        /// </summary>
        public string Answer { set; get; }
    }
    #endregion

    #region 按财年获取城市PowerHour状态
    public class CityPowerHourStateRP : IAPIRequestParameter
    {
        /// <summary>
        /// 财年
        /// </summary>
        public int FinanceYear { get; set; }

        #region IAPIRequestParameter 成员
        public void Validate()
        {
            if (FinanceYear == 0)
            {
                throw new APIException("无效的财年信息【FinanceYear】") { ErrorCode = 123 };
            }
        }

        #endregion
    }

    public class CityPowerHourStateRD : IAPIResponseData
    {
        public List<CityPowerHourStateModel> CityPowerHourStateList { get; set; }
    }

    public class CityPowerHourStateModel
    {
        /// <summary>
        /// 城市标识
        /// </summary>
        public Guid? CityID { set; get; }
        /// <summary>
        /// 城市名称
        /// </summary>
        public string CityName { set; get; }
        /// <summary>
        /// 经度
        /// </summary>
        public decimal? Longitude { set; get; }
        /// <summary>
        /// 纬度
        /// </summary>
        public decimal? Latitude { set; get; }
        /// <summary>
        /// 员工数
        /// </summary>
        public int LocalStaffCount { get; set; }
        /// <summary>
        /// 城市的Powerhour状态
        /// </summary>
        public string CityPowerHourState { set; get; }
    }
    /// <summary>
    /// 城市Powerhour状态枚举
    /// </summary>
    public enum CityPowerhourEnum
    {
        Open = 0,
        Comming = 1,
        Finished = 2
    }
    #endregion

    #region 按财年获取某城市有效的PowerHour(讲座)
    public class SpecificCityPowerHourListRP : IAPIRequestParameter
    {
        /// <summary>
        /// 财年
        /// </summary>
        public int FinanceYear { get; set; }
        /// <summary>
        /// 城市ID
        /// </summary>
        public string CityID { set; get; }

        #region IAPIRequestParameter 成员
        public void Validate()
        {
            if (FinanceYear == 0)
            {
                throw new APIException("无效的财年信息【FinanceYear】") { ErrorCode = 123 };
            }
            if (string.IsNullOrEmpty(CityID))
            {
                throw new APIException("CityID不可为空") { ErrorCode = 123 };
            }
        }

        #endregion
    }
    #endregion

    #region UploadImageData
    public class UploadFileResp
    {
        //public bool o { get; set; }
        public bool success { get; set; }
        public string msg { get; set; }
        public FileData file { get; set; }
        public IList<FileData> thumbs { get; set; }
    }
    //public class FileData
    //{
    //    public string url { get; set; }
    //    public string name { get; set; }
    //    public string extension { get; set; }
    //    public long size { get; set; }
    //    public string type { get; set; }
    //}
    #endregion

    #region 返回当前用户是否是特定城市的工会主席
    public class VerifyLocalLuOwnerRP : IAPIRequestParameter
    {
        /// <summary>
        /// 城市名称
        /// </summary>
        public string CityName { set; get; }

        public void Validate()
        {
            if (string.IsNullOrEmpty(CityName))
            {
                throw new APIException("城市名称不能为空");
            }
        }
    }

    public class VerifyLocalLuOwnerRD : IAPIResponseData
    {
        //是否是工会主席
        public bool IsLocalLuOwner { set; get; }
    }
    #endregion

    #region 修改Power Hour 培训地址
    public class ModifySiteAddressRP : IAPIRequestParameter
    {
        /// <summary>
        /// PowerHourID
        /// </summary>
        public string PowerHourID { set; get; }
        /// <summary>
        /// SiteAddress
        /// </summary>
        public string SiteAddress { set; get; }

        public void Validate()
        {
            if (string.IsNullOrEmpty(PowerHourID))
            {
                throw new APIException("PowerHourID不能为空");
            }

            if (string.IsNullOrEmpty(SiteAddress))
            {
                throw new APIException("SiteAddress不能为空");
            }
        }
    }

    public class ModifySiteAddressRD : IAPIResponseData
    {
        /// <summary>
        /// 是否成功
        /// </summary>
        public bool IsSuccess { set; get; }
    }
    #endregion

    #region 请求宝洁报表
    public class CommonRequestReportFormRP : IAPIRequestParameter
    {
        /// <summary>
        /// token
        /// </summary>
        public string PGToken { set; get; }
        /// <summary>
        /// action
        /// </summary>
        public string ReportAction { set; get; }
        /// <summary>
        /// 动态参数
        /// </summary>
        public string DynamicParam { set; get; }

        public void Validate()
        {
            if (string.IsNullOrEmpty(PGToken))
                throw new APIException("【PGToken】不能为空") { ErrorCode = 102 };
            if (string.IsNullOrEmpty(ReportAction))
                throw new APIException("【ReportAction】不能为空") { ErrorCode = 102 };
            if (string.IsNullOrEmpty(DynamicParam))
                throw new APIException("【DynamicParam】不能为空") { ErrorCode = 102 };
        }
    }

    public class CommonRequestReportFormRD : IAPIResponseData
    {
        public string JsonResult { set; get; }
    }
    #endregion

    #region 记录访问
    public class VisitRP : IAPIRequestParameter
    {
        public void Validate()
        {
        }
    }
    public class VisitRD : IAPIResponseData
    {
        public bool IsSuccess { set; get; }
    }
    #endregion

    #endregion
}