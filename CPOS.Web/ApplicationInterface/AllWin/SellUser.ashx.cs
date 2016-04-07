using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.DataAccess.Base;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.Common;
using JIT.CPOS.DTO.Base;
using JIT.CPOS.DTO.Module.VIP.Login.Request;
using JIT.Utility.ExtensionMethod;
using JIT.CPOS.DTO.Module.VIP.Register.Response;
using JIT.CPOS.DTO.Module.VIP.Register.Request;
using JIT.CPOS.DTO.Module.VIP.Login.Response;
using System.Configuration;
using JIT.Utility.Log;
using JIT.Utility.Web;
using JIT.Utility.DataAccess.Query;
using JIT.Utility;
using System.Text;


namespace JIT.CPOS.Web.ApplicationInterface.AllWin
{

    /// <summary>
    /// SellUser 的摘要说明
    /// </summary>
    public class SellUser : BaseGateway
    {

        #region 错误码
        private const int Error_CustomerCode_NotNull = 103;
        public const int Error_CustomerCode_NotExist = 104;
        public const int Error_UserName_InValid = 105;
        public const int Error_Password_InValid = 106;
        public const int Error_UserRole_NotExist = 107;
        #endregion


        #region 错误码
        const int ERROR_AUTHCODE_NOTEXISTS = 330;
        const int ERROR_AUTHCODE_INVALID = 331;
        const int ERROR_AUTHCODE_NOT_EQUALS = 333;
        const int ERROR_AUTHCODE_WAS_USED = 332;
        const int ERROR_LACK_MOBILE = 312;
        const int ERROR_LACK_VIP_SOURCE = 315;
        const int ERROR_AUTO_MERGE_MEMBER_FAILED = 313;
        const int ERROR_MEMBER_REGISTERED = 314;

        const int ERROR_VIPCARD_EXISTS = 340;   //会员已办卡
        #endregion

        protected override string ProcessAction(string pType, string pAction, string pRequest)
        {
            string rst = "";
            switch (pAction)
            {

                case "SaveRetailTrader"://保存分销商信息
                    rst = this.SaveRetailTrader(pRequest);
                    break;
                case "RetailTraderLogin"://分销商登陆
                    rst = this.RetailTraderLogin(pRequest);
                    break;
                case "SellUserMainAchieve":// 	获取当前销售员的分销商数量、会员数量、奖励
                    rst = this.SellUserMainAchieve(pRequest);
                    break;
                case "GetRetailTradersBySellUser":// 获取某个销售员下的分销商的列表信息
                    rst = this.GetRetailTradersBySellUser(pRequest);
                    break;
                case "GetMonthVipList"://本月新增会员列表
                    rst = this.GetMonthVipList(pRequest);
                    break;
                case "GetAchievements"://获取本月新增会员数量、累计会员数量、本月交易量接口
                    rst = this.GetAchievements(pRequest);
                    break;
                case "MonthVipRiseTrand"://会员增长趋势
                    rst = this.MonthVipRiseTrand(pRequest);
                    break;
                case "GetRetailTraderByID"://获取本月新增会员数量、累计会员数量、本月交易量接口
                    rst = this.GetRetailTraderByID(pRequest);
                    break;
                case "CurrentMonthRewards"://本月奖励金额、发展会员奖励、会员消费奖励接口
                    rst = this.CurrentMonthRewards(pRequest);
                    break;
                case "MonthRewards"://本月奖励金额、发展会员奖励、会员消费奖励接口
                    rst = this.MonthRewards(pRequest);
                    break;

                default:
                    throw new APIException(string.Format("找不到名为：{0}的action处理方法.", pAction))
                    {
                        ErrorCode = ERROR_CODES.INVALID_REQUEST_CAN_NOT_FIND_ACTION_HANDLER
                    };
            }
            return rst;
        }


        #region  新建/编辑分销商接口
        public string SaveRetailTrader(string pRequest)
        {
            var rp = pRequest.DeserializeJSONTo<APIRequest<SaveRetailTraderRP>>();
            if (rp.Parameters.RetailTraderInfo == null)
            {
                throw new APIException("缺少参数【RetailTraderInfo】或参数值为空") { ErrorCode = 135 };
            }
            if (rp.Parameters.IsNewHeadImg == null)
            {
                throw new APIException("缺少参数【IsNewHeadImg】或参数值为空") { ErrorCode = 135 };
            }
            var loggingSessionInfo = Default.GetBSLoggingSession(rp.CustomerID, "1");
            var bll = new RetailTraderBLL(loggingSessionInfo);
            var rd = new SaveRetailTraderRD();
            var rsp = new SuccessResponse<IAPIResponseData>(rd);
            //判断登陆名是否有重复的，要从ap库里取
            var ds = bll.getRetailTraderInfoByLogin2(rp.Parameters.RetailTraderInfo.RetailTraderLogin, "", loggingSessionInfo.ClientID);
            var retailTraderInfo = new RetailTraderInfo();
            //判断账号是否存在
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                var tempDt = ds.Tables[0];
                retailTraderInfo = DataTableToObject.ConvertToObject<RetailTraderInfo>(tempDt.Rows[0]);//直接根据所需要的字段反序列化
                if (retailTraderInfo.RetailTraderID != rp.Parameters.RetailTraderInfo.RetailTraderID)//如果取出来的实体的id和传过来的参数的id不一样，说明不是同一个实体
                {
                    rsp.Message = "该登陆账号已经存在，不能重复使用";
                    rsp.ResultCode = 137;
                    return rsp.ToJSON();//这里要返回
                }
            }


            //从RetailTraderInfo转到RetailTraderEntity
            RetailTraderEntity en = new RetailTraderEntity();
            en.RetailTraderID = rp.Parameters.RetailTraderInfo.RetailTraderID;
            if (rp.Parameters.RetailTraderInfo.RetailTraderID == null || rp.Parameters.RetailTraderInfo.RetailTraderID.ToString() == "")
            {
                en.RetailTraderID = Guid.NewGuid().ToString();
                //获取当前最大的
                //如果是新增，则取本客户下的最大编号作为分销商编号
                en.RetailTraderCode = bll.getMaxRetailTraderCode(rp.CustomerID) + 1;

                en.CreateTime = DateTime.Now;
                en.CreateBy = rp.UserID;
            }
            en.RetailTraderName = rp.Parameters.RetailTraderInfo.RetailTraderName;
            en.RetailTraderLogin = rp.Parameters.RetailTraderInfo.RetailTraderLogin;
            if (!string.IsNullOrEmpty(rp.Parameters.RetailTraderInfo.RetailTraderPass))
            {
                en.RetailTraderPass = MD5Helper.Encryption(rp.Parameters.RetailTraderInfo.RetailTraderPass);
            }
            en.RetailTraderMan = rp.Parameters.RetailTraderInfo.RetailTraderMan;
            en.RetailTraderPhone = rp.Parameters.RetailTraderInfo.RetailTraderPhone;
            en.RetailTraderAddress = rp.Parameters.RetailTraderInfo.RetailTraderAddress;
            en.CooperateType = rp.Parameters.RetailTraderInfo.CooperateType;
            en.SalesType = rp.Parameters.RetailTraderInfo.SalesType;
            en.SellUserID = rp.Parameters.RetailTraderInfo.SellUserID;
            en.UnitID = rp.Parameters.RetailTraderInfo.UnitID;
            en.Status = "1";//启用状态

            en.LastUpdateTime = DateTime.Now;
            en.LastUpdateBy = rp.UserID;
            en.IsDelete = 0;
            en.CustomerId = rp.CustomerID;
            if (rp.Parameters.RetailTraderInfo.RetailTraderID == null || rp.Parameters.RetailTraderInfo.RetailTraderID.ToString() == "")
            {
                bll.Create(en);
              //  rp.Parameters.RetailTraderInfo.RetailTraderID = en.RetailTraderID;//为了返回数据时使用
            }
            else
            {
                bll.Update(en, null, false);//不更新空值的字段
            }
            //另外要保存到ap库里，这样才能登陆时从ap库里统一取出分销商信息，和对应的customerid
            if (rp.Parameters.RetailTraderInfo.RetailTraderID == null || rp.Parameters.RetailTraderInfo.RetailTraderID.ToString() == "")
            {
                bll.Create2Ap(en);//ap库里的RetailTraderID和商户里的RetailTraderID是一样的
               rp.Parameters.RetailTraderInfo.RetailTraderID = en.RetailTraderID;//为了返回数据时使用,到这里才赋值***
            }
            else
            {
                bll.Update2Ap(en, null, false);//不更新空值的字段
            }

            //如果IsNewHeadImg为1时，即上传图片时，则删除之前的关联图片（逻辑删除）
            if (rp.Parameters.IsNewHeadImg == 1)
            {
                ObjectImagesBLL _ObjectImagesBLL = new ObjectImagesBLL(loggingSessionInfo);
                ObjectImagesEntity ObjectImagesEn = new ObjectImagesEntity();

                _ObjectImagesBLL.DeleteByObjectID(en.RetailTraderID.ToString());
            }

            rd.RetailTraderInfo = rp.Parameters.RetailTraderInfo;


            return rsp.ToJSON();
        }

        #endregion



        #region  分销商登陆
        public string GetRetailTraderByID(string pRequest)
        {
            var rp = pRequest.DeserializeJSONTo<APIRequest<RetailTraderLoginRP>>();
            if (rp.Parameters.RetailTraderID == null)
            {
                throw new APIException("缺少参数【RetailTraderID】或参数值为空") { ErrorCode = 135 };
            }

            var loggingSessionInfo = Default.GetBSLoggingSession(rp.CustomerID, "1");
            var bll = new RetailTraderBLL(loggingSessionInfo);
            var rd = new SaveRetailTraderRD();

            var rsp = new SuccessResponse<IAPIResponseData>(rd);
            //获取分销商的信息，包括头像等loggingSessionInfo.ClientID
            var ds = bll.getRetailTraderInfoByLogin("", rp.Parameters.RetailTraderID, loggingSessionInfo.ClientID);
            var retailTraderInfo = new RetailTraderInfo();
            //判断账号是否存在
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                var tempDt = ds.Tables[0];
                retailTraderInfo = DataTableToObject.ConvertToObject<RetailTraderInfo>(tempDt.Rows[0]);//直接根据所需要的字段反序列化
            }

          

            //正确时返回分销商数据
            rd.RetailTraderInfo = retailTraderInfo;//分销商

            //获取奖励规则
            SysRetailRewardRuleEntity[] ruleList = null;
            SysRetailRewardRuleBLL _SysRetailRewardRuleBLL = new SysRetailRewardRuleBLL(loggingSessionInfo);
            //先看该分销商自己是否有奖励规则
            SysRetailRewardRuleEntity en = new SysRetailRewardRuleEntity();
            en.IsTemplate = 0;
            en.CooperateType = retailTraderInfo.CooperateType;
            en.RetailTraderID = rp.Parameters.RetailTraderID;
            en.CustomerId = rp.CustomerID;
            en.IsDelete = 0;
            //获取奖励模板
            ruleList = _SysRetailRewardRuleBLL.QueryByEntity(en, null);

           
              //  SysRetailRewardRuleEntity en = new SysRetailRewardRuleEntity();
            if (ruleList == null || ruleList.Count()==0 )
            {
                SysRetailRewardRuleEntity en2 = new SysRetailRewardRuleEntity();
                en2.IsTemplate = 1;
                en2.CooperateType = retailTraderInfo.CooperateType;
                en2.IsDelete = 0;
                en2.CustomerId = rp.CustomerID;//加上这个，否则多个客户的奖励规则一起取了
                //获取奖励模板
                ruleList = _SysRetailRewardRuleBLL.QueryByEntity(en2, null);//这里不能用en了，用en会包含之前的RetailTraderID属性了
            }

            StringBuilder sb = new StringBuilder("");
            if (ruleList != null || ruleList.Count() != 0)
            {
                for (int i = 0; i < ruleList.Count(); i++)
                {
                    if (ruleList[i].RetailTraderReward > 0)
                    {
                        sb.Append(ruleList[i].RewardTypeName);
                        int temp = (int)ruleList[i].RetailTraderReward;//判断一下小数点后面是不是0，如果是0，就不要了
                        if (ruleList[i].RetailTraderReward > temp)
                        {
                            sb.Append(ruleList[i].RetailTraderReward);
                        }
                        else {
                            sb.Append(temp);
                        }
                        if (ruleList[i].AmountOrPercent == 1)
                        {
                            sb.Append("元");
                        }
                        else
                        {
                            sb.Append("%");
                        }
                        if (ruleList[i].RewardTypeCode == "AttentThreeMonth")
                        {
                            sb.Append("（不包含首笔交易）");
                        }
                        sb.Append("，");
                    }
                }
                if(sb.ToString()!="")
                {
                  sb.Replace('，', '。', sb.Length - 1, 1);
                }
            }
            else {
                if (retailTraderInfo.CooperateType == "TwoWay")
                {
                    sb.Append(retailTraderInfo.UnitName).Append("门店帮您发放优惠券，也可向您的门店带来潜在的顾客。 ");
                }
            }




            rd.RewardNotic = sb.ToString();//奖励规则说明



            return rsp.ToJSON();
        }
        #endregion

        #region  分销商登陆
        public string RetailTraderLogin(string pRequest)
        {
            var rp = pRequest.DeserializeJSONTo<APIRequest<RetailTraderLoginRP>>();

            //string customerCode = rp.Parameters.CustomerCode;
            //if (string.IsNullOrEmpty(customerCode))
            //{
            //    throw new APIException("客户代码不能为空") { ErrorCode = 135 };
            //}

            if (rp.Parameters.RetailTraderLogin == null)
            {
                throw new APIException("缺少参数【RetailTraderLogin】或参数值为空") { ErrorCode = 135 };
            }
            if (rp.Parameters.RetailTraderPass == null)
            {
                throw new APIException("缺少参数【RetailTraderPass】或参数值为空") { ErrorCode = 135 };
            }
           // var loggingSessionInfo = Default.GetBSLoggingSession(rp.CustomerID, "1");

            //WMenuBLL menuServer = new WMenuBLL(Default.GetAPLoggingSession(""));
            //string customerId = menuServer.GetCustomerIDByCustomerCode(customerCode);

            //if (string.IsNullOrEmpty(customerId))
            //{
            //    throw new APIException("客户代码对应的客户不存在") { ErrorCode = Error_CustomerCode_NotExist };
            //}


            var bll2 = new RetailTraderBLL(Default.GetAPLoggingSession(""));//用空的登陆信息去查

            var ds = bll2.getRetailTraderInfoByLogin2(rp.Parameters.RetailTraderLogin, "", "");
            var retailTraderInfo = new RetailTraderInfo();


            var rd = new SaveRetailTraderRD();
            var rsp = new SuccessResponse<IAPIResponseData>(rd);
            string customerId = "";
            //判断账号是否存在
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                var tempDt = ds.Tables[0];
                retailTraderInfo = DataTableToObject.ConvertToObject<RetailTraderInfo>(tempDt.Rows[0]);//直接根据所需要的字段反序列化
              
                customerId = retailTraderInfo.CustomerId;
            }    else
            {
                rsp.Message = "登陆名不存在";
                rsp.ResultCode = 136;
                return rsp.ToJSON();
            }

        

            var loggingSessionInfo = Default.GetBSLoggingSession(customerId, "1");
            var bll = new RetailTraderBLL(loggingSessionInfo);


           
            //获取分销商的信息，包括头像等loggingSessionInfo.ClientID
            ds = bll.getRetailTraderInfoByLogin(rp.Parameters.RetailTraderLogin, "", loggingSessionInfo.ClientID);
            retailTraderInfo = new RetailTraderInfo();
            //判断账号是否存在
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                var tempDt = ds.Tables[0];
                retailTraderInfo = DataTableToObject.ConvertToObject<RetailTraderInfo>(tempDt.Rows[0]);//直接根据所需要的字段反序列化
            }
            else
            {
                rsp.Message = "登陆名不存在";
                rsp.ResultCode = 136;
                return rsp.ToJSON();
            }
            //判断密码是否正确
            if (retailTraderInfo.RetailTraderPass != rp.Parameters.RetailTraderPass)
            {
                rsp.Message = "登陆密码不正确";
                rsp.ResultCode = 136;
                return rsp.ToJSON();
            }
            //判断密码是否正确
            //if (retailTraderInfo.Status != "1")
            //{
            //    rsp.Message = "该分销商账号已经被停用";
            //    rsp.ResultCode = 136;
            //    return rsp.ToJSON();
            //}


            //正确时返回分销商数据
            rd.RetailTraderInfo = retailTraderInfo;


            return rsp.ToJSON();
        }

        #endregion

        #region   获取某个销售员下的分销商的列表信息
        public string GetRetailTradersBySellUser(string pRequest)
        {
            var rp = pRequest.DeserializeJSONTo<APIRequest<SellUserMainAchieveRP>>();
            if (string.IsNullOrEmpty(rp.UserID))
            {
                throw new APIException("缺少参数【UserID】或参数值为空") { ErrorCode = 135 };
            }

            var loggingSessionInfo = Default.GetBSLoggingSession(rp.CustomerID, "1");
            var bll = new RetailTraderBLL(loggingSessionInfo);
            var rd = new GetRetailTradersBySellUserRD();
            var rsp = new SuccessResponse<IAPIResponseData>(rd);
            //获取分销商的信息，loggingSessionInfo.ClientID
            var ds = bll.GetRetailTradersBySellUser(rp.Parameters.RetailTraderName, rp.UserID, loggingSessionInfo.ClientID);   //获取
            //判断账号是否存在
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                var tempDt = ds.Tables[0];
                rd.RetailTraderList = DataTableToObject.ConvertToList<RetailTraderInfo>(tempDt);//直接根据所需要的字段反序列化
            }

            return rsp.ToJSON();
        }

        #endregion

        #region   获取当前销售员的分销商数量、会员数量、奖励
        public string SellUserMainAchieve(string pRequest)
        {
            var rp = pRequest.DeserializeJSONTo<APIRequest<SellUserMainAchieveRP>>();
            if (string.IsNullOrEmpty(rp.UserID))
            {
                throw new APIException("缺少参数【UserID】或参数值为空") { ErrorCode = 135 };
            }

            var loggingSessionInfo = Default.GetBSLoggingSession(rp.CustomerID, "1");
            var bll = new RetailTraderBLL(loggingSessionInfo);
            var rd = new SellUserMainAchieveRD();
            var rsp = new SuccessResponse<IAPIResponseData>(rd);
            //获取分销商的信息，loggingSessionInfo.ClientID
            var ds = bll.GetRetailTradersBySellUser("", rp.UserID, loggingSessionInfo.ClientID);   //获取
            //判断账号是否存在
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                rd.RetailTraderCount = ds.Tables[0].Rows.Count;
            }
            else
            {
                rd.RetailTraderCount = 0;
            }
            //取该账号下面的分销商的会员的数量
            int VipCount = bll.GetVipCountBySellUser(rp.UserID, loggingSessionInfo.ClientID);   //获取
            rd.VipCount = VipCount;

            //销售员头像
            ObjectImagesBLL _ObjectImagesBLL = new ObjectImagesBLL(loggingSessionInfo);
            ObjectImagesEntity en = new ObjectImagesEntity();
            en.ObjectId = rp.UserID;
            List<ObjectImagesEntity> ImgList = _ObjectImagesBLL.QueryByEntity(en, null).OrderByDescending(p => p.CreateTime).ToList();
            if (ImgList != null && ImgList.Count != 0)
            {
                // string fileDNS = customerBasicSettingBll.GetSettingValueByCode("FileDNS"); ;//http://182.254.156.57:811
                rd.HeadImg = ImgList[0].ImageURL;
            }


            return rsp.ToJSON();
        }

        #endregion

        #region   本月新增会员列表
        public string GetMonthVipList(string pRequest)
        {
            var rp = pRequest.DeserializeJSONTo<APIRequest<SellUserMainAchieveRP>>();
            if (string.IsNullOrEmpty(rp.UserID))
            {
                throw new APIException("缺少参数【UserID】或参数值为空") { ErrorCode = 135 };
            }

            var loggingSessionInfo = Default.GetBSLoggingSession(rp.CustomerID, "1");
            var bll = new RetailTraderBLL(loggingSessionInfo);
            var rd = new GetMonthVipListRD();
            var rsp = new SuccessResponse<IAPIResponseData>(rd);
            //获取分销商的信息，loggingSessionInfo.ClientID
            int year = DateTime.Now.Year;
            int month = DateTime.Now.Month;
            var ds = bll.GetMonthVipList(rp.UserID, loggingSessionInfo.ClientID, month, year);   //获取
            //判断账号是否存在
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                var tempDt = ds.Tables[0];
                rd.VipList = DataTableToObject.ConvertToList<VipInfo>(tempDt);//直接根据所需要的字段反序列化
            }

            return rsp.ToJSON();
        }

        #endregion


        #region   获取当前销售员的分销商数量、会员数量、奖励
        public string GetAchievements(string pRequest)//	获取本月新增会员数量、累计会员数量、本月交易量接口(取该会员的订单数)
        {
            var rp = pRequest.DeserializeJSONTo<APIRequest<SellUserMainAchieveRP>>();
            if (string.IsNullOrEmpty(rp.UserID))
            {
                throw new APIException("缺少参数【UserID】或参数值为空") { ErrorCode = 135 };
            }

            var loggingSessionInfo = Default.GetBSLoggingSession(rp.CustomerID, "1");
            var bll = new RetailTraderBLL(loggingSessionInfo);
            var rd = new GetAchievementsRD();
            var rsp = new SuccessResponse<IAPIResponseData>(rd);
            //本月新增会员数量
            int year = DateTime.Now.Year;
            int month = DateTime.Now.Month;
            var ds = bll.GetMonthVipList(rp.UserID, loggingSessionInfo.ClientID, month, year);   //获取       
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                var tempDt = ds.Tables[0];
                rd.MonthVipCount = tempDt.Rows.Count;//直接根据所需要的字段反序列化
            }
            //累计会员数量
            var ds2 = bll.GetMonthVipList(rp.UserID, loggingSessionInfo.ClientID, -1, -1);   //获取        
            if (ds2 != null && ds2.Tables.Count > 0 && ds2.Tables[0].Rows.Count > 0)
            {
                var tempDt = ds2.Tables[0];
                rd.TotalVipCount = tempDt.Rows.Count;//直接根据所需要的字段反序列化
            }
            //本月交易量
            int MonthTradeCount = bll.GetMonthTradeCount(rp.UserID, loggingSessionInfo.ClientID, month, year);   //获取
            rd.MonthTradeCount = MonthTradeCount;

            return rsp.ToJSON();
        }

        #endregion




        #region 	月度会员增长趋势列表数据接口
        public string MonthVipRiseTrand(string pRequest)
        {
            var rp = pRequest.DeserializeJSONTo<APIRequest<SellUserMainAchieveRP>>();
            if (string.IsNullOrEmpty(rp.UserID))
            {
                throw new APIException("缺少参数【UserID】或参数值为空") { ErrorCode = 135 };
            }

            var loggingSessionInfo = Default.GetBSLoggingSession(rp.CustomerID, "1");
            var bll = new RetailTraderBLL(loggingSessionInfo);
            var rd = new MonthVipRiseTrandRD();
            var rsp = new SuccessResponse<IAPIResponseData>(rd);
            //获取分销商的信息，loggingSessionInfo.ClientID
            // int month = DateTime.Now.Month;
            int year = DateTime.Now.Year;//目前只取今年的
            var ds = bll.MonthVipRiseTrand(rp.UserID, loggingSessionInfo.ClientID, year);   //获取
            var VipRiseList = new List<VipMonthRiseTrand>();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                var tempDt = ds.Tables[0];
                VipRiseList = DataTableToObject.ConvertToList<VipMonthRiseTrand>(tempDt);//直接根据所需要的字段反序列化
            }
            int currentMonth = DateTime.Now.Month;
            //判断账号是否存在
            rd.VipRiseList = new List<VipMonthRiseTrand>();
            for (int i = 1; i <= currentMonth; i++)
            {
                VipMonthRiseTrand temp = VipRiseList.Where(p => p.Month == i).SingleOrDefault();//直接根据所需要的字段反序列化
                if (temp == null)
                {
                    temp = new VipMonthRiseTrand();
                    temp.Year = year;
                    temp.Month = i;
                    temp.VipCount = 0;
                }
                rd.VipRiseList.Add(temp);

            }
            rd.VipRiseList.Reverse();//顺序反转
            return rsp.ToJSON();
        }

        #endregion

        #region   	本月奖励金额、发展会员奖励（17）、会员消费奖励接口(消费奖励分为14会员首次消费奖励和15关注三个月内奖励)
        public string CurrentMonthRewards(string pRequest)
        {
            var rp = pRequest.DeserializeJSONTo<APIRequest<SellUserMainAchieveRP>>();
            if (string.IsNullOrEmpty(rp.Parameters.UserOrRetailID))
            {
                throw new APIException("缺少参数【UserOrRetailID】或参数值为空") { ErrorCode = 135 };
            }

            var loggingSessionInfo = Default.GetBSLoggingSession(rp.CustomerID, "1");
            var bll = new RetailTraderBLL(loggingSessionInfo);
            var rd = new CurrentMonthRewardsRD();
            var rsp = new SuccessResponse<IAPIResponseData>(rd);
            //本月新增会员数量
            int year = DateTime.Now.Year;
            int month = DateTime.Now.Month;
            var UserOrRetailID = rp.Parameters.UserOrRetailID;
            //本月首次关注奖励
            decimal attenAmount = bll.RetailRewardByAmountSource(UserOrRetailID, loggingSessionInfo.ClientID, year, month, -1, "17");   //获取

            //本月首次消费奖励
            decimal firstTradeAmount = bll.RetailRewardByAmountSource(UserOrRetailID, loggingSessionInfo.ClientID, year, month, -1, "14");   //获取 = MonthTradeCount;
            //本月三月内消费奖励
            decimal threeMonthAmount = bll.RetailRewardByAmountSource(UserOrRetailID, loggingSessionInfo.ClientID, year, month, -1, "15");   //获取 = MonthTradeCount;


            rd.MonthVipRewards = attenAmount;
            rd.MonthTradeRewards = firstTradeAmount + threeMonthAmount;
            rd.MonthRewards = rd.MonthVipRewards + rd.MonthTradeRewards;
            return rsp.ToJSON();
        }

        #endregion


        #region 	历史每月奖励条状图接口
        public string MonthRewards(string pRequest)
        {
            var rp = pRequest.DeserializeJSONTo<APIRequest<SellUserMainAchieveRP>>();
            if (string.IsNullOrEmpty(rp.Parameters.UserOrRetailID))
            {
                throw new APIException("缺少参数【UserOrRetailID】或参数值为空") { ErrorCode = 135 };
            }

            var loggingSessionInfo = Default.GetBSLoggingSession(rp.CustomerID, "1");
            var bll = new RetailTraderBLL(loggingSessionInfo);
            var rd = new MonthRewardsRD();
            var rsp = new SuccessResponse<IAPIResponseData>(rd);
            //获取分销商的信息，loggingSessionInfo.ClientID
            // int month = DateTime.Now.Month;
            int year = DateTime.Now.Year;//目前只取今年的
            int month = DateTime.Now.Month;//目前只取今年的
            var ds = bll.MonthRewards(rp.Parameters.UserOrRetailID, loggingSessionInfo.ClientID, year);   //获取
            var VipRiseList = new List<MonthRewardInfo>();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                var tempDt = ds.Tables[0];
                VipRiseList = DataTableToObject.ConvertToList<MonthRewardInfo>(tempDt);//直接根据所需要的字段反序列化
            }


            int currentMonth = DateTime.Now.Month;
            //判断账号是否存在
            rd.MonthRewardList = new List<MonthRewardInfo>();
            for (int i = 1; i <= currentMonth; i++)
            {
                MonthRewardInfo temp = VipRiseList.Where(p => p.Month == i).SingleOrDefault();//直接根据所需要的字段反序列化
                if (temp == null)
                {
                    temp = new MonthRewardInfo();
                    temp.Year = year;
                    temp.Month = i;
                    temp.MonthAmount = 0;
                    temp.MonthVipAmount = 0;
                    temp.MonthTradeAmount = 0;

                }
                rd.MonthRewardList.Add(temp);

            }

            rd.MonthRewardList.Reverse();//顺序反转
            return rsp.ToJSON();
        }

        #endregion



    }


    public class SaveRetailTraderRP : IAPIRequestParameter
    {
        //是否上传新头像图片（1为上传，0为不上传）（如果不上传，就不删除他的头像信息）
        public int? IsNewHeadImg { get; set; }
        public RetailTraderInfo RetailTraderInfo { get; set; }


        public void Validate()
        {
        }
    }

    public class SaveRetailTraderRD : IAPIResponseData
    {
        public RetailTraderInfo RetailTraderInfo { get; set; }
        public string RewardNotic { get; set; }

    }


    public class RetailTraderLoginRP : IAPIRequestParameter
    {

        public string RetailTraderID { get; set; }
        public string RetailTraderLogin { get; set; }
        public string RetailTraderPass { get; set; }
        public string CustomerCode { get; set; }

        public void Validate()
        {
        }
    }




    public class SellUserMainAchieveRP : IAPIRequestParameter
    {

        public string RetailTraderID { get; set; }//专门是分销商的ID
        public string UserOrRetailID { get; set; }//销售员或者分销商的ID
        public string RetailTraderName { get; set; }  //分销商名称
        public void Validate()
        {
        }
    }



    public class SellUserMainAchieveRD : IAPIResponseData
    {
        public int RetailTraderCount { get; set; }
        public int VipCount { get; set; }
        public string HeadImg { get; set; }//销售员头像

    }


    public class GetRetailTradersBySellUserRD : IAPIResponseData
    {
        public List<RetailTraderInfo> RetailTraderList { get; set; }

    }

    public class RetailTraderInfo
    {
        #region 属性集
        /// <summary>
        /// 
        /// </summary>
        public string RetailTraderID { get; set; }
        public string Status { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int? RetailTraderCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String RetailTraderName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String RetailTraderLogin { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String RetailTraderPass { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String RetailTraderType { get; set; }


        /// <summary>
        /// 
        /// </summary>
        public String RetailTraderMan { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String RetailTraderPhone { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String RetailTraderAddress { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String CooperateType { get; set; }

        public String SalesType { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public String SellUserID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String UnitID { get; set; }


        public string _CreateTime;

        public string CreateTime { get { return Convert.ToDateTime( _CreateTime).ToString("yyyy-MM-dd"); } set { _CreateTime = value.ToString(); } }

        /// <summary>
        /// 
        /// </summary>
        public String CreateBy { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String LastUpdateBy { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? LastUpdateTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Int32? IsDelete { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String CustomerId { get; set; }


        #endregion

        public String UnitName { get; set; }
        public String UserName { get; set; }
        public String ImageURL { get; set; }

        public int VipCount { get; set; }
    }


    public class GetMonthVipListRD : IAPIResponseData
    {
        public List<VipInfo> VipList { get; set; }
    }


    public class GetAchievementsRD : IAPIResponseData
    {
        public int MonthVipCount { get; set; }
        public int TotalVipCount { get; set; }
        public int MonthTradeCount { get; set; }

    }
    public class MonthVipRiseTrandRD : IAPIResponseData
    {
        public List<VipMonthRiseTrand> VipRiseList { get; set; }
    }


    public class CurrentMonthRewardsRD : IAPIResponseData
    {
        public decimal MonthRewards { get; set; }
        public decimal MonthVipRewards { get; set; }
        public decimal MonthTradeRewards { get; set; }

    }


    public class MonthRewardsRD : IAPIResponseData
    {
        public List<MonthRewardInfo> MonthRewardList { get; set; }
    }




    public class VipMonthRiseTrand
    {

        public int Year { get; set; }
        public int Month { get; set; }

        public int VipCount { get; set; }
    }



    public class VipInfo
    {
        #region 属性集
        /// <summary>
        /// 
        /// </summary>
        public String VIPID { get; set; }

        /// <summary>
        /// 会员名称
        /// </summary>
        public String VipName { get; set; }

        /// <summary>
        /// 会员级别
        /// </summary>
        public Int32? VipLevel { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String VipCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String WeiXin { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String WeiXinUserId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Int32? Gender { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Int32? Age { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String Phone { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String SinaMBlog { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String TencentMBlog { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String Birthday { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String Qq { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String Email { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Int32? Status { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String VipSourceId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Decimal? Integration { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String ClientID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? RecentlySalesTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? RegistrationTime { get; set; }

    




        public string _CreateTime;

        public string CreateTime { get { return Convert.ToDateTime(_CreateTime).ToString("yyyy-MM-dd"); } set { _CreateTime = value.ToString(); } }

 
        /// 上线会员主标识
        /// </summary>
        public String HigherVipID { get; set; }

        /// <summary>
        /// QRVipCode
        /// </summary>
        public String QRVipCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String City { get; set; }

        /// <summary>
        /// 优惠券URL
        /// </summary>
        public String CouponURL { get; set; }

        /// <summary>
        ///  会籍店（优惠券信息）
        /// </summary>
        public String CouponInfo { get; set; }

        /// <summary>
        /// 购买金额
        /// </summary>
        public Decimal? PurchaseAmount { get; set; }

        /// <summary>
        /// 购买次数
        /// </summary>
        public Int32? PurchaseCount { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String DeliveryAddress { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String Longitude { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String Latitude { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String VipPasswrod { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String HeadImgUrl { get; set; }



        /// <summary>
        /// 
        /// </summary>
        public String Col17 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String Col18 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String Col19 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String Col20 { get; set; }


        public String VipRealName { get; set; }


        /// <summary>
        /// 
        /// </summary>
        public String SetoffUserId { get; set; }





        #endregion
    }

    public class MonthRewardInfo
    {
        public int Year { get; set; }
        public int Month { get; set; }
        /// <summary>
        /// 引流
        /// </summary>
        public decimal MonthAmount { get; set; }
        public decimal MonthVipAmount { get; set; }
        public decimal MonthTradeAmount { get; set; }
        /// <summary>
        /// 销售
        /// </summary>
        public decimal MonthSalesAmount { get; set; }
    }
}