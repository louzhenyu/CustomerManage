using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.BS.Entity.User;
using JIT.CPOS.DTO.Base;
using JIT.CPOS.Web.ApplicationInterface;
using JIT.Utility.ExtensionMethod;
using JIT.Utility;
using System.Data;
using JIT.CPOS.Web.RateLetterInterface.Common;
using JIT.Utility.Log;

namespace JIT.CPOS.Web.RateLetterInterface.Group
{
    /// <summary>
    /// GroupHandler 的摘要说明
    /// </summary>
    public class GroupHandler : BaseGateway
    {

        protected override string ProcessAction(string pType, string pAction, string pRequest)
        {
            var rst = string.Empty;
            switch (pAction)
            {
                //创建群组
                case "CreateGroup":
                    rst = CreateGroup(pRequest);
                    break;
                //获取用户所在群组列表
                case "GetGroupList":
                    rst = GetGroupList(pRequest);
                    break;
                //群组加人
                case "AddUserGroup":
                    rst = AddUserGroup(pRequest);
                    break;
                //群组踢人
                case "DelUserGroup":
                    rst = DelUserGroup(pRequest);
                    break;
                //删除群组
                case "DeleteGroup":
                    rst = DeleteGroup(pRequest);
                    break;
                //群组成员
                case "GetGroupUsers":
                    rst = GetGroupUsers(pRequest);
                    break;
                //企信群组信息
                case "GetIMGroupsInfo":
                    rst = GetIMGroupsInfo(pRequest);
                    break;
                //修改群组信息
                case "UpdateIMGroupInfo":
                    rst = UpdateIMGroupInfo(pRequest);
                    break;
                //用户主动退群
                case "UserQuitGroup":
                    rst = UserQuitGroup(pRequest);
                    break;
                //用户申请加人群组
                case "UserApplyJoinGroup":
                    rst = UserApplyJoinGroup(pRequest);
                    break;
                default:
                    throw new APIException(string.Format("未实现Action名为{0}的处理.", pAction)) { ErrorCode = 201 };
            }
            return rst;
        }

        #region  创建群组
        /// <summary>
        /// 创建群组： 1)如果创建群组成功，验证则调用云通讯接口：在云通讯上创建群组
        /// </summary>
        /// <param name="pRequest"></param>
        /// <returns></returns>
        private string CreateGroup(string reqContent)
        {
            var rd = new APIResponse<IMGroupRD>();
            var rdData = new IMGroupRD();
            try
            {
                var rp = reqContent.DeserializeJSONTo<EMAPIRequest<IMGroupRP>>();
                if (rp.Parameters != null)
                    rp.Parameters.Validate();

                var loggingSessionInfo = Default.GetBSLoggingSession(rp.CustomerID, rp.UserID);

                //限定创建讨论组权限判断
                DataSet dsUserRight = GetUserRights(rp.UserID, rp.CustomerID, loggingSessionInfo);
                if (!IsHasCreateGroupJuri(rp.UserID, rp.CustomerID, UserRightCode.USER_CREATE_GROUP_RIGHT_CODE, dsUserRight))
                {
                    rdData.IsSuccess = false;
                    rd.Data = rdData;
                    rd.ResultCode = 320;
                    rd.Message = "无创建讨论组权限";
                    return rd.ToJSON();
                }

                IMGroupBLL bll = new IMGroupBLL(loggingSessionInfo);
                IMGroupUserBLL imgubll = new IMGroupUserBLL(loggingSessionInfo);

                //判断当前用户列表是否已存在群，若存在返回GroupID
                string valChatGroupID = ValidateUser(rp.UserID, rp.Parameters.UserIDList, rp.CustomerID, loggingSessionInfo);
                CreateGroupViewModel viewModel = null;
                string retCode = "", retMsg = "";
                if (string.IsNullOrEmpty(valChatGroupID))
                {
                    //调用云通讯创建群组
                    CloudRequestFactory crf = new CloudRequestFactory();
                    viewModel = crf.CreateGroup(YunTongXunAppSetting.RestAddress, YunTongXunAppSetting.RestPort, rp.SubAccountSid, rp.SubToken, rp.Parameters.GroupName, rp.Parameters.GroupLevel.ToString(), rp.Parameters.ApproveNeededLevel.ToString(), rp.Parameters.Description);
                    if (viewModel.statusCode == "000000")
                    {
                        //企信创建聊天群组
                        Guid chatGroupID = Guid.NewGuid();
                        valChatGroupID = chatGroupID.ToString();
                        rp.Parameters.ChatGroupID = chatGroupID;
                        rp.Parameters.CustomerID = rp.CustomerID;
                        //第三方群组ID赋值（云通讯群组ID）
                        rp.Parameters.BindGroupID = viewModel.groupId;
                        rp.Parameters.UserCount = rp.Parameters.UserIDList.Count;
                        bll.Create(rp.Parameters);

                        //据企信用户ID获取云通讯voipAccount集合
                        List<string> voipAccountList = GetVoipAccountList(rp.Parameters.UserIDList, rp.CustomerID, loggingSessionInfo);
                        //调用云通讯群主拉人
                        FoundationsViewModel fviewModel = crf.InviteJoinGroup(YunTongXunAppSetting.RestAddress, YunTongXunAppSetting.RestPort, rp.SubAccountSid, rp.SubToken, viewModel.groupId, voipAccountList, rp.Parameters.Confirm, rp.Parameters.Declared);
                        if (fviewModel.statusCode == "000000")
                        {
                            //将用户加入用户群组
                            imgubll.AddUsersGroup(rp.Parameters.UserIDList, chatGroupID.ToString(), rp.CustomerID);
                            retCode = fviewModel.statusCode;
                            retMsg = fviewModel.statusMsg;

                            //rdData.IMGroupInfo = bll.GetByID(valChatGroupID);
                            //rdData.IMGroupsInfo = DataTableToObject.ConvertToList<IMGroupEntity>(ds.Tables[0]);
                            DataSet ds = bll.GetIMGroups(rp.CustomerID, valChatGroupID, rp.Parameters.BindGroupID);
                            if (ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                            {
                                rdData.IMGroupInfo = DataTableToObject.ConvertToList<IMGroupContainMembers>(ds.Tables[0])[0];
                                //返回群组成员
                                DataSet dsMembers = imgubll.GetIMGroupUser(rdData.IMGroupInfo.ChatGroupID.ToString(), rp.CustomerID, 0, 1000);
                                if (dsMembers.Tables != null && dsMembers.Tables.Count > 0 && dsMembers.Tables[0] != null && dsMembers.Tables[0].Rows.Count > 0)
                                {
                                    rdData.IMGroupInfo.IMGroupMembers = DataTableToObject.ConvertToList<IMGroupMemberModel>(dsMembers.Tables[0]);
                                }
                            }
                        }
                        else
                        {
                            //如果拉人失败
                            rdData.IsSuccess = false;
                            rd.Message = "失败";
                            rd.ResultCode = 310;
                            retCode = fviewModel.statusCode;
                            retMsg = fviewModel.statusMsg;
                        }
                    }
                    else
                    {
                        rd.ResultCode = 310;
                        rdData.IsSuccess = false;
                        rd.Message = "失败";
                        retCode = viewModel.statusCode;
                        retMsg = viewModel.statusMsg;
                    }
                }
                else
                {
                    rdData.IsSuccess = true;
                    //rdData.IMGroupInfo = bll.GetByID(valChatGroupID);
                    DataSet ds = bll.GetIMGroups(rp.CustomerID, valChatGroupID, rp.Parameters.BindGroupID);
                    if (ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                    {
                        rdData.IMGroupInfo = DataTableToObject.ConvertToList<IMGroupContainMembers>(ds.Tables[0])[0];
                        //返回群组成员
                        DataSet dsMembers = imgubll.GetIMGroupUser(rdData.IMGroupInfo.ChatGroupID.ToString(), rp.CustomerID, 0, 1000);
                        if (dsMembers.Tables != null && dsMembers.Tables.Count > 0 && dsMembers.Tables[0] != null && dsMembers.Tables[0].Rows.Count > 0)
                        {
                            rdData.IMGroupInfo.IMGroupMembers = DataTableToObject.ConvertToList<IMGroupMemberModel>(dsMembers.Tables[0]);
                        }
                    }
                }
                rdData.TheThirdPartyStatusCode = retCode;
                rdData.TheThirdPartyStatusMsg = retMsg;
                rd.Message = "成功";
                rd.Data = rdData;
            }
            catch (Exception)
            {
                rd.ResultCode = 310;
                rd.Message = "保存失败";
                rdData.IsSuccess = false;
                rd.Data = rdData;
                throw;
            }

            Loggers.DEFAULT.Debug(new DebugLogInfo() { Message = rd.ToJSON() });
            return rd.ToJSON();
        }
        #endregion

        #region   获取用户所在群组列表
        /// <summary>
        /// 获取用户所在群组列表
        /// </summary>
        /// <param name="reqContent"></param>
        /// <returns></returns>
        private string GetGroupList(string reqContent)
        {
            var rd = new APIResponse<IMGroupsRD>();
            try
            {
                var rp = reqContent.DeserializeJSONTo<EMAPIRequest<IMGroupsRP>>();
                if (rp.Parameters != null)
                    rp.Parameters.Validate();
                var loggingSessionInfo = Default.GetBSLoggingSession(rp.CustomerID, rp.UserID);
                IMGroupBLL bll = new IMGroupBLL(loggingSessionInfo);
                IMGroupUserBLL imgubll = new IMGroupUserBLL(loggingSessionInfo);

                DataSet ds = bll.GetUserInGroups(rp.UserID, rp.CustomerID, rp.Parameters.PageIndex, rp.Parameters.PageSize);
                if (ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                {
                    var rdData = new IMGroupsRD();
                    rdData.IMGroupsInfo = DataTableToObject.ConvertToList<IMGroupContainMembers>(ds.Tables[0]);
                    for (int i = 0; i < rdData.IMGroupsInfo.Count; i++)
                    {
                        //返回群组成员
                        DataSet dsMembers = imgubll.GetIMGroupUser(rdData.IMGroupsInfo[i].ChatGroupID.ToString(), rp.CustomerID, 0, 1000);
                        if (dsMembers.Tables != null && dsMembers.Tables.Count > 0 && dsMembers.Tables[0] != null && dsMembers.Tables[0].Rows.Count > 0)
                        {
                            rdData.IMGroupsInfo[i].IMGroupMembers = DataTableToObject.ConvertToList<IMGroupMemberModel>(dsMembers.Tables[0]);
                        }
                    }
                    rd.Data = rdData;
                    rd.Message = "获取成功";
                }
                else
                    rd.Message = "未查询到和用户相关的群组";
            }
            catch (Exception)
            {
                rd.ResultCode = 312;
                rd.Message = "获取失败";
                throw;
            }
            return rd.ToJSON();
        }
        #endregion

        #region   群组加人
        /// <summary>
        /// 群组加人
        /// </summary>
        /// <param name="reqContent"></param>
        /// <returns></returns>
        private string AddUserGroup(string reqContent)
        {
            var rd = new APIResponse<AddUserGroupRD>();
            var rdData = new AddUserGroupRD();
            try
            {
                var rp = reqContent.DeserializeJSONTo<EMAPIRequest<AddUserGroupRP>>();
                if (rp.Parameters != null)
                    rp.Parameters.Validate();
                var loggingSessionInfo = Default.GetBSLoggingSession(rp.CustomerID, rp.UserID);
                //过滤在指定群组未出现的用户集合
                List<string> difUserIDList = GetDiffGroupUser(rp.Parameters.ChatGroupID, rp.UserID, rp.Parameters.UserIDList, rp.CustomerID, loggingSessionInfo);
                if (difUserIDList != null && difUserIDList.Count > 0)
                {
                    IMGroupUserBLL bll = new IMGroupUserBLL(loggingSessionInfo);
                    IMGroupBLL imgBll = new IMGroupBLL(loggingSessionInfo);
                    IMGroupEntity imge = imgBll.GetByID(rp.Parameters.ChatGroupID);
                    //据企信用户ID获取云通讯voipAccount集合
                    List<string> voipAccountList = GetVoipAccountList(difUserIDList, rp.CustomerID, loggingSessionInfo);
                    //调用云通讯拉人
                    CloudRequestFactory crf = new CloudRequestFactory();
                    FoundationsViewModel viewModel = crf.InviteJoinGroup(YunTongXunAppSetting.RestAddress, YunTongXunAppSetting.RestPort, rp.SubAccountSid, rp.SubToken, imge.BindGroupID, voipAccountList, rp.Parameters.Confirm, rp.Parameters.Declared);
                    if (viewModel.statusCode == "000000")
                    {
                        //将用户加入用户群组
                        bll.AddUsersGroup(difUserIDList, rp.Parameters.ChatGroupID, rp.CustomerID);
                        //更新群组用户数
                        imgBll.UpdateGroupUserCount(difUserIDList.Count, rp.Parameters.ChatGroupID);

                        rdData.IsSuccess = true;
                        rd.Message = "保存成功";
                    }
                    else
                    {
                        //如果拉人失败
                        rdData.IsSuccess = false;
                        rd.ResultCode = 311;
                        rd.Message = "云通讯用户加入失败";
                    }
                    rdData.TheThirdPartyStatusCode = viewModel.statusCode;
                    rdData.TheThirdPartyStatusMsg = viewModel.statusMsg;
                }
                else
                {
                    rd.Message = "提示：未发现需要添加的用户";
                    rdData.IsSuccess = true;
                }
            }
            catch (Exception)
            {
                rdData.IsSuccess = false;
                rd.ResultCode = 311;
                rd.Message = "保存失败";
                throw;
            }
            rd.Data = rdData;
            return rd.ToJSON();
        }
        #endregion

        #region   群组踢人
        /// <summary>
        /// 群组踢人
        /// </summary>
        /// <param name="reqContent"></param>
        /// <returns></returns>
        private string DelUserGroup(string reqContent)
        {
            var rd = new APIResponse<DelUserGroupRD>();
            var rdData = new DelUserGroupRD();
            try
            {
                var rp = reqContent.DeserializeJSONTo<EMAPIRequest<DelUserGroupRP>>();
                if (rp.Parameters != null)
                    rp.Parameters.Validate();
                var loggingSessionInfo = Default.GetBSLoggingSession(rp.CustomerID, rp.UserID);
                //过滤指定企信群组未出现的用户集合
                List<string> waitUserIDList = FilterDelUserID(rp.Parameters.UserIDList, rp.Parameters.ChatGroupID, rp.UserID, rp.CustomerID, loggingSessionInfo);
                if (waitUserIDList != null && waitUserIDList.Count > 0)
                {
                    IMGroupBLL imgBll = new IMGroupBLL(loggingSessionInfo);
                    IMGroupEntity imge = imgBll.GetByID(rp.Parameters.ChatGroupID);
                    //据企信用户ID获取云通讯voipAccount集合
                    List<string> voipAccountList = GetVoipAccountList(waitUserIDList, rp.CustomerID, loggingSessionInfo);
                    //调用云通讯踢人
                    CloudRequestFactory crf = new CloudRequestFactory();
                    FoundationsViewModel viewModel = crf.DeleteGroupMember(YunTongXunAppSetting.RestAddress, YunTongXunAppSetting.RestPort, rp.SubAccountSid, rp.SubToken, imge.BindGroupID, voipAccountList);
                    if (viewModel.statusCode == "000000")
                    {
                        //将企信用户在企信群组中删除
                        IMGroupUserBLL bll = new IMGroupUserBLL(loggingSessionInfo);
                        bll.DelUsersGroup(waitUserIDList, rp.Parameters.ChatGroupID, rp.CustomerID);
                        //更新群组用户数
                        imgBll.UpdateGroupUserCount(-waitUserIDList.Count, rp.Parameters.ChatGroupID);
                        rdData.IsSuccess = true;
                        rd.Message = "保存成功";
                    }
                    else
                    {
                        //云通讯删除用户失败
                        rdData.IsSuccess = false;
                        rd.ResultCode = 313;
                        rd.Message = "云通讯删除用户失败";
                    }
                    rdData.TheThirdPartyStatusCode = viewModel.statusCode;
                    rdData.TheThirdPartyStatusMsg = viewModel.statusMsg;
                }
                else rdData.IsSuccess = true;
            }
            catch (Exception)
            {
                rdData.IsSuccess = false;
                rd.ResultCode = 313;
                throw;
            }
            rd.Data = rdData;
            return rd.ToJSON();
        }
        #endregion

        #region 删除群组
        /// <summary>
        /// 删除群组
        /// </summary>
        /// <param name="reqContent"></param>
        /// <returns></returns>
        private string DeleteGroup(string reqContent)
        {
            var rd = new APIResponse<DelGroupRD>();
            var rdData = new DelGroupRD();
            try
            {
                var rp = reqContent.DeserializeJSONTo<EMAPIRequest<DelGroupRP>>();
                if (rp.Parameters != null)
                    rp.Parameters.Validate();
                var loggingSessionInfo = Default.GetBSLoggingSession(rp.CustomerID, rp.UserID);
                IMGroupBLL bll = new IMGroupBLL(loggingSessionInfo);
                IMGroupBLL imgBll = new IMGroupBLL(loggingSessionInfo);
                IMGroupEntity imge = imgBll.GetByID(rp.Parameters.ChatGroupID);
                //判断该组是否存在
                if (imge != null)
                {
                    //调用云通讯删除组
                    CloudRequestFactory crf = new CloudRequestFactory();
                    FoundationsViewModel viewModel = crf.DeleteGroup(YunTongXunAppSetting.RestAddress, YunTongXunAppSetting.RestPort, rp.SubAccountSid, rp.SubToken, imge.BindGroupID);
                    if (viewModel.statusCode == "000000")
                    {
                        //删除聊天群组
                        bll.DeleteGroup(rp.UserID, rp.CustomerID, rp.Parameters.ChatGroupID);
                        //删除用户群组里该群组的ID
                        IMGroupUserBLL imgUBll = new IMGroupUserBLL(loggingSessionInfo);
                        imgUBll.DelUsersGroup(null, rp.Parameters.ChatGroupID, rp.CustomerID);
                        rd.Message = "删除成功";
                        rdData.IsSuccess = true;
                    }
                    else
                    {
                        //群主删除用户失败
                        rdData.IsSuccess = false;
                        rd.ResultCode = 314;
                        rd.Message = "云通讯群组删除失败";
                    }
                    rdData.TheThirdPartyStatusCode = viewModel.statusCode;
                    rdData.TheThirdPartyStatusMsg = viewModel.statusMsg;
                }
                else
                {
                    rdData.IsSuccess = true;
                    rd.Message = "该组不存在或已删除";
                }
            }
            catch (Exception)
            {
                rd.ResultCode = 314;
                rdData.IsSuccess = false;
                throw;
            }
            rd.Data = rdData;
            return rd.ToJSON();
        }
        #endregion

        #region 群组成员
        /// <summary>
        /// 群组成员
        /// </summary>
        /// <param name="reqContent"></param>
        /// <returns></returns>
        private string GetGroupUsers(string reqContent)
        {
            var rd = new APIResponse<IMGroupUserRD>();
            var rdData = new IMGroupUserRD();
            try
            {
                var rp = reqContent.DeserializeJSONTo<EMAPIRequest<IMGroupUserRP>>();
                if (rp.Parameters != null)
                    rp.Parameters.Validate();
                var loggingSessionInfo = Default.GetBSLoggingSession(rp.CustomerID, rp.UserID);
                IMGroupUserBLL bll = new IMGroupUserBLL(loggingSessionInfo);

                DataSet ds = bll.GetIMGroupUser(rp.Parameters.ChatGroupID, rp.CustomerID, rp.Parameters.PageIndex, rp.Parameters.PageSize);
                if (ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                {
                    rdData.IMGroupUsers = DataTableToObject.ConvertToList<IMGroupUserModel>(ds.Tables[0]);
                    rd.Data = rdData;
                }
                rd.Message = "获取成功";
            }
            catch (Exception)
            {
                rd.ResultCode = 315;
                throw;
            }
            return rd.ToJSON();
        }
        #endregion

        #region 企信群组,群组详细
        public string GetIMGroupsInfo(string reqContent)
        {
            var rd = new APIResponse<IMGroupInfoRD>();
            var rdData = new IMGroupInfoRD();
            try
            {
                var rp = reqContent.DeserializeJSONTo<EMAPIRequest<IMGroupInfoRP>>();
                if (rp.Parameters != null)
                    rp.Parameters.Validate();
                var loggingSessionInfo = Default.GetBSLoggingSession(rp.CustomerID, rp.UserID);
                IMGroupBLL bll = new IMGroupBLL(loggingSessionInfo);
                DataSet ds = bll.GetIMGroups(rp.Parameters.CreateUserId, rp.CustomerID, rp.Parameters.GroupName, rp.Parameters.ChatGroupID, rp.Parameters.BindGroupID, rp.Parameters.PageIndex, rp.Parameters.PageSize);
                if (ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                {
                    rdData.IMGroupsInfo = DataTableToObject.ConvertToList<IMGroupEntity>(ds.Tables[0]);
                    //返回群组成员
                    IMGroupUserBLL imgubll = new IMGroupUserBLL(loggingSessionInfo);
                    DataSet dsGroupUser = imgubll.GetIMGroupUser(rp.Parameters.ChatGroupID, rp.CustomerID, 0, 1000);
                    if (dsGroupUser.Tables != null && dsGroupUser.Tables.Count > 0 && dsGroupUser.Tables[0] != null && dsGroupUser.Tables[0].Rows.Count > 0)
                    {
                        rdData.IMGroupUsers = DataTableToObject.ConvertToList<IMGroupUserModel>(dsGroupUser.Tables[0]);
                    }
                    rd.Data = rdData;
                    rd.Message = "获取成功";
                }
                else
                    rd.Message = "未查询到相关数据";
            }
            catch (Exception)
            {
                rd.ResultCode = 316;
                throw;
            }
            return rd.ToJSON();
        }
        #endregion

        #region 群组信息修改
        public string UpdateIMGroupInfo(string reqContent)
        {
            var rd = new APIResponse<IMGroupInfoUpdateRD>();
            var rdData = new IMGroupInfoUpdateRD();
            try
            {
                var rp = reqContent.DeserializeJSONTo<EMAPIRequest<IMGroupInfoUpdateRP>>();
                if (rp.Parameters != null)
                    rp.Parameters.Validate();
                var loggingSessionInfo = Default.GetBSLoggingSession(rp.CustomerID, rp.UserID);
                IMGroupBLL bll = new IMGroupBLL(loggingSessionInfo);
                //获取群组对象
                IMGroupEntity imge = bll.GetByID(rp.Parameters.ChatGroupID);
                //更新替换数据
                imge.GroupName = rp.Parameters.GroupName;
                if (!string.IsNullOrEmpty(rp.Parameters.Description))
                    imge.Description = rp.Parameters.Description;
                if (!string.IsNullOrEmpty(rp.Parameters.ApproveNeededLevel))
                    imge.ApproveNeededLevel = Convert.ToInt32(rp.Parameters.ApproveNeededLevel);

                if (imge != null)
                {
                    //更新云通讯上群组信息
                    CloudRequestFactory crf = new CloudRequestFactory();
                    FoundationsViewModel viewModel = crf.ModifyGroup(YunTongXunAppSetting.RestAddress, YunTongXunAppSetting.RestPort, rp.SubAccountSid, rp.SubToken, imge.BindGroupID, imge.GroupName, imge.ApproveNeededLevel.ToString(), imge.Description);
                    if (viewModel.statusCode == "000000")
                    {
                        //提交至数据库
                        bll.Update(imge);
                        rd.Message = "保存成功";
                        rdData.IsSuccess = true;
                    }
                    else
                    {
                        rdData.IsSuccess = false;
                        rd.Message = "获取成功";
                    }
                    rdData.IMGroupInfo = imge;
                    rdData.TheThirdPartyStatusCode = viewModel.statusCode;
                    rdData.TheThirdPartyStatusMsg = viewModel.statusMsg;
                }
                else
                    rd.Message = "未查询到相关数据";
            }
            catch (Exception)
            {
                rd.ResultCode = 317;
                throw;
            }
            rd.Data = rdData;
            return rd.ToJSON();
        }
        #endregion

        #region 用户主动退群
        /// <summary>
        /// 用户主动退群
        /// </summary>
        /// <param name="reqContent"></param>
        /// <returns></returns>
        public string UserQuitGroup(string reqContent)
        {
            var rd = new APIResponse<UserQuitGroupRD>();
            var rdData = new UserQuitGroupRD();
            try
            {
                var rp = reqContent.DeserializeJSONTo<EMAPIRequest<UserQuitGroupRP>>();
                if (rp.Parameters != null)
                    rp.Parameters.Validate();

                var loggingSessionInfo = Default.GetBSLoggingSession(rp.CustomerID, rp.UserID);
                //过滤指定企信群组未出现的用户集合
                List<string> waitUserIDList = new List<string> { rp.UserID }; //FilterDelUserID(new List<string> { rp.UserID }, rp.Parameters.ChatGroupID, rp.UserID, rp.CustomerID, loggingSessionInfo);
                if (waitUserIDList != null && waitUserIDList.Count > 0)
                {
                    IMGroupBLL imgBll = new IMGroupBLL(loggingSessionInfo);
                    IMGroupEntity imge = imgBll.GetByID(rp.Parameters.ChatGroupID);
                    if (imge.CreateBy != rp.UserID)
                    {
                        //调用云通讯主动退群
                        CloudRequestFactory crf = new CloudRequestFactory();
                        FoundationsViewModel viewModel = crf.LogoutGroup(YunTongXunAppSetting.RestAddress, YunTongXunAppSetting.RestPort, rp.SubAccountSid, rp.SubToken, imge.BindGroupID);
                        if (viewModel.statusCode == "000000")
                        {
                            //将企信用户在企信群组中删除
                            IMGroupUserBLL bll = new IMGroupUserBLL(loggingSessionInfo);
                            bll.DelUsersGroup(waitUserIDList, rp.Parameters.ChatGroupID, rp.CustomerID);
                            //更新群组用户数
                            imgBll.UpdateGroupUserCount(-waitUserIDList.Count, rp.Parameters.ChatGroupID);
                            rdData.IsSuccess = true;
                            rd.Message = "退群成功";
                        }
                        else
                        {
                            //云通讯用户退群失败
                            rdData.IsSuccess = false;
                            rd.ResultCode = 318;
                            rd.Message = "云通讯用户退群失败";
                        }
                        rdData.TheThirdPartyStatusCode = viewModel.statusCode;
                        rdData.TheThirdPartyStatusMsg = viewModel.statusMsg;
                    }
                    else
                    {
                        rdData.IsSuccess = false;
                        rd.Message = "群主不可已退群";
                    }
                }
                else
                {
                    rd.Message = "群组不存在该用户";
                    rdData.IsSuccess = true;
                }
            }
            catch (Exception)
            {
                rdData.IsSuccess = false;
                rd.ResultCode = 318;
                throw;
            }
            rd.Data = rdData;
            return rd.ToJSON();
        }
        #endregion

        #region   用户申请入群
        /// <summary>
        /// 用户申请入群
        /// </summary>
        /// <param name="reqContent"></param>
        /// <returns></returns>
        private string UserApplyJoinGroup(string reqContent)
        {
            var rd = new APIResponse<UserApplyJoinGroupRD>();
            var rdData = new UserApplyJoinGroupRD();
            try
            {
                var rp = reqContent.DeserializeJSONTo<EMAPIRequest<UserApplyJoinGroupRP>>();
                if (rp.Parameters != null)
                    rp.Parameters.Validate();
                var loggingSessionInfo = Default.GetBSLoggingSession(rp.CustomerID, rp.UserID);
                IMGroupBLL imgBll = new IMGroupBLL(loggingSessionInfo);
                IMGroupEntity imge = imgBll.GetByID(rp.Parameters.ChatGroupID);
                //过滤在指定群组未出现的用户集合
                List<string> difUserIDList = GetDiffGroupUser(rp.Parameters.ChatGroupID, imge.CreateBy, rp.Parameters.UserIDList, rp.CustomerID, loggingSessionInfo);
                if (difUserIDList != null && difUserIDList.Count > 0)
                {
                    IMGroupUserBLL bll = new IMGroupUserBLL(loggingSessionInfo);

                    //据企信用户ID获取云通讯用户集合
                    DataTable dTable = GetClondUsertList(difUserIDList, loggingSessionInfo);
                    if (dTable != null)
                    {
                        //调用云通讯加入群
                        CloudRequestFactory crf = new CloudRequestFactory();
                        FoundationsViewModel viewModel = null;
                        foreach (DataRow row in dTable.Rows)
                        {
                            viewModel = crf.JoinGroup(YunTongXunAppSetting.RestAddress, YunTongXunAppSetting.RestPort, row["SubAccountSid"].ToString(), row["SubToken"].ToString(), imge.BindGroupID, rp.Parameters.Declared);
                        }
                        if (viewModel.statusCode == "000000")
                        {
                            //将用户加入用户群组
                            bll.AddUsersGroup(difUserIDList, rp.Parameters.ChatGroupID, rp.CustomerID);
                            //更新群组用户数
                            imgBll.UpdateGroupUserCount(difUserIDList.Count, rp.Parameters.ChatGroupID);

                            rdData.IsSuccess = true;
                            rd.Message = "用户申请加入成功";
                        }
                        else
                        {
                            //如果拉人失败
                            rdData.IsSuccess = false;
                            rd.ResultCode = 319;
                            rd.Message = "用户申请加入失败";
                        }
                        rdData.TheThirdPartyStatusCode = viewModel.statusCode;
                        rdData.TheThirdPartyStatusMsg = viewModel.statusMsg;
                    }
                }
                else
                {
                    rd.Message = "提示：未发现需要添加的用户";
                    rdData.IsSuccess = true;
                }
            }
            catch (Exception)
            {
                rdData.IsSuccess = false;
                rd.ResultCode = 319;
                rd.Message = "保存失败";
                throw;
            }
            rd.Data = rdData;
            return rd.ToJSON();
        }
        #endregion

        #region 工具方法
        //1.  获取用户所有的群组，2. 循环遍历群组得到群组用户集合3. 比较
        #region 提取用户群组中群组未添加的用户
        /// <summary>
        /// 提取用户群组中群组未添加的用户
        /// </summary>
        /// <param name="pGroupID">群组ID</param>
        /// <param name="pUserID">当前用户ID</param>
        /// <param name="pUserIDList">待添加用户列表</param>
        /// <param name="pCustomerID">客户ID</param>
        /// <param name="pLoggingSessionInfo"></param>
        /// <returns>
        /// 需要添加的用户列表。
        /// </returns>
        public List<string> GetDiffGroupUser(string pGroupID, string pUserID, List<string> pUserIDList, string pCustomerID, LoggingSessionInfo pLoggingSessionInfo)
        {
            List<string> notExistUserIDs = new List<string>();
            if (pUserIDList == null && pUserIDList.Count <= 0) return notExistUserIDs;

            bool isGroupName = ValidateUser(pUserID, pGroupID, pCustomerID, pLoggingSessionInfo);    //验证该用户是否是群主，如果不是则返回null。
            if (!isGroupName) return notExistUserIDs;

            //取出群组成员列表
            var groupUsers = GetGroupUserByUseID(pGroupID, pCustomerID, pLoggingSessionInfo);  //群组成员列表
            if (groupUsers != null && groupUsers.Count > 0)
            {
                notExistUserIDs = ToAddUsersList(groupUsers, pUserIDList);
            }

            return notExistUserIDs;
        }

        /// <summary>
        /// 验证用户是否是群主。
        /// </summary>
        /// <param name="userID">当前用户ID</param>
        /// <param name="groupID">群组ID</param>
        /// <param name="customerID">客户ID</param>
        /// <param name="loggingSessionInfo"></param>
        /// <returns></returns>
        private bool ValidateUser(string userID, string groupID, string customerID, LoggingSessionInfo loggingSessionInfo)
        {
            IMGroupBLL imgBll = new IMGroupBLL(loggingSessionInfo);
            DataSet ds = imgBll.GetIMGroupUserByGroupID(userID, groupID, customerID);
            IMGroupEntity entity = null;
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                entity = DataTableToObject.ConvertToObject<IMGroupEntity>(ds.Tables[0].Rows[0]);
            }

            if (entity != null)  //是群主
            {
                return true;
            }
            return false;
        }


        /// <summary>
        /// 取出需要处理的用户ID列表。
        /// </summary>
        /// <param name="list1">群组中已存在成员List</param>
        /// <param name="list2">新成员列表</param>
        /// <returns></returns>
        private List<string> ToAddUsersList(List<string> list1, List<string> list2)
        {
            List<string> newList = new List<string>();
            if (list1 != null && list1.Count > 0 && list2 != null && list2.Count > 0)
            {
                newList = list2.Except(list1).ToList();
            }

            return newList;
        }

        #endregion

        #region 判断用户列表所在的群是否存在，存在返回GroupID
        /// <summary>
        /// 验证待添加用户列表是否和群主之前添加的群组列表用户相同。
        /// </summary>
        /// <param name="pUserID">群主用户ID</param>
        /// <param name="pUsers">待添加用户ID</param>
        /// <param name="pCustomerID">客户ID</param>
        /// <param name="pLoggingSessionInfo"></param>
        /// <returns>
        /// 群组ID：
        /// </returns>
        public string ValidateUser(string pUserID, List<string> pUsers, string pCustomerID, LoggingSessionInfo pLoggingSessionInfo)
        {
            var groupList = GetGroupByUser(pUserID, pCustomerID, pLoggingSessionInfo);
            if (groupList != null && groupList.Count > 0)
            {
                foreach (var group in groupList)
                {
                    var groupUsers = GetGroupUserByUseID(group.ChatGroupID.ToString(), pCustomerID, pLoggingSessionInfo);  //群组成员列表
                    if (groupUsers != null && groupUsers.Count > 0)
                    {
                        var flag = groupUsers.OrderBy(m => m).SequenceEqual(pUsers.OrderBy(m => m));
                        if (flag)  //true：相等
                        {
                            return group.ChatGroupID.ToString();
                        }
                    }
                }
            }

            return string.Empty;
        }

        /// <summary>
        /// 获取用户所有群组。
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="customerID"></param>
        /// <param name="loggingSessionInfo"></param>
        /// <returns></returns>
        private List<Group> GetGroupByUser(string userID, string customerID, LoggingSessionInfo loggingSessionInfo)
        {
            IMGroupBLL imgBll = new IMGroupBLL(loggingSessionInfo);
            DataSet ds = imgBll.GetIMGroupByUserID(userID, customerID);
            List<Group> list = null;
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                list = DataTableToObject.ConvertToList<Group>(ds.Tables[0]);
            }

            return list;
        }

        /// <summary>
        /// 获取群组下面的所有用户ID列表。
        /// </summary>
        /// <param name="groupID"></param>
        /// <param name="customerID"></param>
        /// <param name="loggingSessionInfo"></param>
        /// <returns></returns>
        private List<string> GetGroupUserByUseID(string groupID, string customerID, LoggingSessionInfo loggingSessionInfo)
        {
            IMGroupUserBLL bll = new IMGroupUserBLL(loggingSessionInfo);
            DataSet ds = bll.GetGroupUserByGroupID(groupID, customerID);
            List<GroupUser> list = new List<GroupUser>();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                list = DataTableToObject.ConvertToList<GroupUser>(ds.Tables[0]);
            }
            List<string> strList = new List<string>();
            foreach (var item in list)
            {
                strList.Add(item.UserID);
            }

            return strList;
        }
        #endregion

        #region 据userID获取云通讯ID
        public List<string> GetVoipAccountList(List<string> pUserIDList, string pCustomerID, LoggingSessionInfo pLoggingSessionInfo)
        {
            TUserThirdPartyMappingBLL tutpmBll = new TUserThirdPartyMappingBLL(pLoggingSessionInfo);
            return tutpmBll.GetVoipAccountList(pUserIDList);
        }
        #endregion

        #region 据userID获取云通讯ID
        public DataTable GetClondUsertList(List<string> pUserIDList, LoggingSessionInfo pLoggingSessionInfo)
        {
            TUserThirdPartyMappingBLL tutpmBll = new TUserThirdPartyMappingBLL(pLoggingSessionInfo);
            return tutpmBll.GetCloudUserList(pUserIDList);
        }
        #endregion

        #region 判断待剔人用户列表是否存在该群组内,返回在的用户列表
        public List<string> FilterDelUserID(List<string> pUserIDList, string pChatGroupID, string pUserID, string pCustomerID, LoggingSessionInfo pLoggingSessionInfo)
        {
            List<string> waitDelUserID = new List<string>();
            IMGroupUserBLL imguBll = new IMGroupUserBLL(pLoggingSessionInfo);
            DataSet ds = imguBll.GetIMGroupUser("", pChatGroupID, pCustomerID);
            if (ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
            {
                foreach (string item in pUserIDList)
                {
                    foreach (DataRow rowItem in ds.Tables[0].Rows)
                    {
                        if (item == rowItem["UserID"].ToString() && item != pUserID)
                        {
                            waitDelUserID.Add(item);
                            break;
                        }
                    }
                }
            }
            return waitDelUserID;
        }
        #endregion

        #region 验证用户是否存在创建讨论组权限
        public DataSet GetUserRights(string pUserID, string pCustomerID, LoggingSessionInfo pLoggingSessionInfo)
        {
            //key:VIP020000 value:创建讨论组权限
            T_UserBLL bll = new T_UserBLL(pLoggingSessionInfo);
            DataSet ds = bll.GetUserRights(pUserID, pCustomerID);
            return ds;
        }

        public bool IsHasCreateGroupJuri(string pUserID, string pCustomerID, string pRightKey, DataSet pDsUserRight)
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

        #endregion
    }

    #region 定义接口的请求参数及响应结果的数据结构

    #region 创建群组
    public class IMGroupRP : IMGroupEntity, IAPIRequestParameter
    {
        /// <summary>
        /// 创建群组时并添加的用户列表
        /// </summary>
        public List<string> UserIDList { set; get; }

        /// <summary>
        /// 是否需要被邀请人确认 0 ：需要 1：不需要（自动加入群组）
        /// </summary>
        public string Confirm { set; get; }

        /// <summary>
        /// 邀请理由
        /// </summary>
        public string Declared { set; get; }

        public void Validate()
        {
            //用户数
            if (UserCount == null)
                UserCount = 0;

            //群组类型 0：临时组(上限100人)  1：普通组(上限300人)  2：VIP组 (上限500人)
            if (GroupLevel == null)
                GroupLevel = 0;
            //申请加入模式 0：默认直接加入1：需要身份验证 2:私有群组
            if (ApproveNeededLevel == null)
                ApproveNeededLevel = 1;

            if (string.IsNullOrEmpty(GroupName))
            {
                throw new APIException("请输入群组名称");
            }
            if (UserIDList == null || UserIDList.Count <= 0)
            {
                throw new APIException("至少提供一个用户ID");
            }
            if (string.IsNullOrEmpty(Description))
            {
                throw new APIException("请输入描述");
            }
            if (string.IsNullOrEmpty(Confirm))
            {
                Confirm = "1";
            }
        }
    }

    public class IMGroupRD : CloudMessage, IAPIResponseData
    {
        public bool IsSuccess { set; get; }
        public IMGroupContainMembers IMGroupInfo { set; get; }
    }


    #endregion

    #region 用户所在群组列表
    public class IMGroupsRP : IAPIRequestParameter
    {
        public string SubAccountSid { set; get; }
        public string SubToken { set; get; }
        public string ChatGroupID { set; get; }
        public string GroupName { set; get; }
        public int PageIndex { set; get; }
        public int PageSize { set; get; }
        public void Validate()
        {
            if (PageIndex <= 0) PageIndex = 0;
            if (PageSize <= 0) PageSize = 15;
        }
    }
    public class IMGroupsRD : IAPIResponseData
    {
        public List<IMGroupContainMembers> IMGroupsInfo { set; get; }
    }
    public class IMGroupContainMembers : IMGroupModel
    {
        public List<IMGroupMemberModel> IMGroupMembers { set; get; }
    }
    public class IMGroupMemberModel
    {
        public string HighImageUrl { set; get; }
        public string user_name { set; get; }
        public string user_name_en { set; get; }
    }

    #endregion

    #region 群组加人
    public class AddUserGroupRP : IAPIRequestParameter
    {
        public string ChatGroupID { set; get; }
        public List<string> UserIDList { set; get; }

        /// <summary>
        /// 是否需要被邀请人确认 0 ：需要 1：不需要（自动加入群组）
        /// </summary>
        public string Confirm { set; get; }

        /// <summary>
        /// 邀请理由
        /// </summary>
        public string Declared { set; get; }

        public void Validate()
        {
            if (string.IsNullOrEmpty(ChatGroupID))
            {
                throw new APIException("请提供群组ID");
            }
            if (UserIDList.Count <= 0)
            {
                throw new APIException("请至少提供一个用户ID");
            }
            if (string.IsNullOrEmpty(Confirm))
            {
                Confirm = "1";
            }
        }
    }
    public class AddUserGroupRD : CloudMessage, IAPIResponseData
    {
        public bool IsSuccess { set; get; }
    }
    #endregion

    #region 群组踢人
    public class DelUserGroupRP : IAPIRequestParameter
    {
        public string ChatGroupID { set; get; }
        public List<string> UserIDList { set; get; }
        public void Validate()
        {
            if (string.IsNullOrEmpty(ChatGroupID))
            {
                throw new APIException("请提供群组ID");
            }
            if (UserIDList.Count <= 0)
            {
                throw new APIException("请至少提供一个用户ID");
            }
        }
    }
    public class DelUserGroupRD : CloudMessage, IAPIResponseData
    {
        public bool IsSuccess { set; get; }
    }
    #endregion

    #region 删除群组
    public class DelGroupRP : IAPIRequestParameter
    {
        public string ChatGroupID { set; get; }
        public void Validate()
        {
            if (string.IsNullOrEmpty(ChatGroupID))
            {
                throw new APIException("请提供群组ID");
            }
        }
    }

    public class DelGroupRD : CloudMessage, IAPIResponseData
    {
        public bool IsSuccess { set; get; }
    }
    #endregion

    #region 群组成员
    public class IMGroupUserRP : IAPIRequestParameter
    {
        public string ChatGroupID { set; get; }
        public int PageIndex { set; get; }
        public int PageSize { set; get; }
        public void Validate()
        {
            if (PageIndex == null) PageIndex = 0;
            if (PageSize <= 0) PageSize = 15;

            if (string.IsNullOrEmpty(ChatGroupID))
            {
                throw new APIException("请提供群组ID");
            }
        }
    }
    public class IMGroupUserRD : IAPIResponseData
    {
        public List<IMGroupUserModel> IMGroupUsers { set; get; }

    }
    public class IMGroupUserModel : IMGroupUserEntity
    {
        public string highImageUrl { set; get; }
        public string user_code { set; get; }
        public string user_name { set; get; }
        public string user_email { set; get; }
        /// <summary>
        /// 云通讯ID
        /// </summary>
        public string VoipAccount { set; get; }
    }

    #endregion

    #region 群组列表
    public class IMGroupInfoRP : IAPIRequestParameter
    {
        public string BindGroupID { set; get; }
        public string CreateUserId { set; get; }
        public string ChatGroupID { set; get; }
        public string GroupName { set; get; }
        public int PageIndex { set; get; }
        public int PageSize { set; get; }
        public void Validate()
        {
            if (PageIndex <= 0) PageIndex = 0;
            if (PageSize <= 0) PageSize = 15;
        }
    }
    public class IMGroupInfoRD : IAPIResponseData
    {
        public List<IMGroupEntity> IMGroupsInfo { set; get; }
        public List<IMGroupUserModel> IMGroupUsers { set; get; }
    }
    #endregion

    #region 群组信息修改
    public class IMGroupInfoUpdateRP : IAPIRequestParameter
    {
        public string ChatGroupID { set; get; }
        public string GroupName { set; get; }
        /// <summary>
        /// 申请加入模式
        /// </summary>
        public string ApproveNeededLevel { set; get; }

        /// <summary>
        /// 描述(群组公告，最长为200个字符)
        /// </summary>
        public string Description { set; get; }

        public void Validate()
        {
            if (string.IsNullOrEmpty(GroupName))
                throw new APIException("请提供群组名称");
            if (string.IsNullOrEmpty(Description) || Description.Length > 200)
                throw new APIException("群组描述不能为空且不能超过200字符");
        }
    }
    public class IMGroupInfoUpdateRD : CloudMessage, IAPIResponseData
    {
        public bool IsSuccess { set; get; }
        public IMGroupEntity IMGroupInfo { set; get; }
    }
    #endregion

    #region 用户主动退群
    public class UserQuitGroupRP : IAPIRequestParameter
    {
        public string ChatGroupID { set; get; }
        public void Validate()
        {
            if (string.IsNullOrEmpty(ChatGroupID))
            {
                throw new APIException("请提供群组ID");
            }
        }
    }
    public class UserQuitGroupRD : CloudMessage, IAPIResponseData
    {
        public bool IsSuccess { set; get; }
    }
    #endregion

    #region 用户申请入群
    public class UserApplyJoinGroupRP : IAPIRequestParameter
    {
        public string ChatGroupID { set; get; }
        public List<string> UserIDList { set; get; }

        /// <summary>
        /// 邀请理由
        /// </summary>
        public string Declared { set; get; }

        public void Validate()
        {
            if (string.IsNullOrEmpty(ChatGroupID))
            {
                throw new APIException("请提供群组ID");
            }
            if (UserIDList.Count != 1)
            {
                throw new APIException("目前只支持邀请一位用户");
            }
        }
    }
    public class UserApplyJoinGroupRD : CloudMessage, IAPIResponseData
    {
        public bool IsSuccess { set; get; }
    }
    #endregion

    public class CloudMessage
    {
        /// <summary>
        /// 云通讯返回状态代码
        /// </summary>
        public string TheThirdPartyStatusCode { set; get; }
        /// <summary>
        /// 云通讯返回态消息
        /// </summary>
        public string TheThirdPartyStatusMsg { set; get; }
    }

    #endregion

    #region 工具类
    public class Group
    {
        /// <summary>
        /// 群组ID
        /// </summary>
        public Guid ChatGroupID { get; set; }

        /// <summary>
        /// 第三方群组ID
        /// </summary>
        public string BindGroupID { get; set; }
    }

    public class GroupUser
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public string UserID { get; set; }

        ///// <summary>
        ///// 群组ID
        ///// </summary>
        //public string IMGroupID { get; set; }
    }
    #endregion
}