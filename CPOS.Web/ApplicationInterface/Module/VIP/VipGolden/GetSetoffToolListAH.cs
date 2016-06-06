using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.DTO.Base;
using JIT.CPOS.DTO.Module.VIP.VipGold.Request;
using JIT.CPOS.DTO.Module.VIP.VipGold.Response;
using JIT.CPOS.Web.ApplicationInterface.Base;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;

namespace JIT.CPOS.Web.ApplicationInterface.Module.VIP.VipGolden
{
    /// <summary>
    /// 获取工具列表
    /// </summary>
    public class GetSetoffToolListAH : BaseActionHandler<GetSetoffToolsRP, GetSetoffToolsRD>
    {
        protected override GetSetoffToolsRD ProcessRequest(APIRequest<GetSetoffToolsRP> pRequest)
        {

            SetoffToolsBLL setOffToolsBLL = new SetoffToolsBLL(CurrentUserInfo);
            IincentiveRuleBLL incentiveRuleBLL = new IincentiveRuleBLL(CurrentUserInfo);
            var para = pRequest.Parameters;
            var setoffToolsEntity = new SetoffToolsEntity();
            if (para.ToolsType != null)
            {
                setoffToolsEntity.ToolType = para.ToolsType.ToString();
            }
            GetSetoffToolsRD setoffToolsRD = new GetSetoffToolsRD();
            var setoffEventBLL=new SetoffEventBLL(CurrentUserInfo);
            //获取集客活动信息
            string NoticePlatType = string.Empty;
            if (string.IsNullOrEmpty(para.ApplicationType))
            {
                para.ApplicationType = "0";
                if (para.ApplicationType == "3")
                {
                    NoticePlatType = "2";
                }
            }
            var setOffEventInfo = setoffEventBLL.QueryByEntity(new SetoffEventEntity() { Status = "10", IsDelete = 0, SetoffType = Convert.ToInt32(para.ApplicationType == "3" ? "2" : para.ApplicationType), CustomerId = CurrentUserInfo.CurrentUser.customer_id }, null);
            int SetoffToolsTotalCount=0;
            DataSet SetoffToolsList=null;
            if (setOffEventInfo.Length != 0)
            {
                //获取集客活动工具数量
                SetoffToolsTotalCount = setOffToolsBLL.GeSetoffToolsListCount(setoffToolsEntity, para.ApplicationType,para.BeShareVipID,setOffEventInfo[0].SetoffEventID.ToString());
                //获取集客活动工具列表
                SetoffToolsList = setOffToolsBLL.GetSetoffToolsList(setoffToolsEntity, para.ApplicationType, para.BeShareVipID, para.PageIndex, para.PageSize, setOffEventInfo[0].SetoffEventID.ToString());
            }
           
            //获取集客奖励信息
            var IncentiveRule = incentiveRuleBLL.GetIncentiveRule(para.ApplicationType);
            setoffToolsRD.TotalCount = SetoffToolsTotalCount;
            int remainder = 0;
            if (para.PageSize == 0)
            {
                para.PageSize = 10;
            }
            setoffToolsRD.TotalPageCount = Math.DivRem(SetoffToolsTotalCount, para.PageSize, out remainder);
            if (remainder > 0)
                setoffToolsRD.TotalPageCount++;
            string strHost = ConfigurationManager.AppSettings["website_url"].Trim();
            if (SetoffToolsList != null && SetoffToolsList.Tables[0].Rows.Count > 0)
            {
                //为优惠券和海报添加图片路径
                SetoffToolsList.Tables[0].Columns.Add("ImageUrl", typeof(string));
                var CustomerBasicSettingBLL =new CustomerBasicSettingBLL(CurrentUserInfo);
                var CustomerBasicInfo = CustomerBasicSettingBLL.QueryByEntity(new CustomerBasicSettingEntity() { CustomerID = CurrentUserInfo.CurrentUser.customer_id, SettingCode = "SetoffPosterWeChatDefaultPhoto" }, null).FirstOrDefault();
                var setOffPosterBLL = new SetoffPosterBLL(CurrentUserInfo);
                var ObjectImagesBLL = new ObjectImagesBLL(CurrentUserInfo);
                var T_CTW_LEventBLL = new T_CTW_LEventBLL(CurrentUserInfo);
                string goUrl = string.Empty;
                string goCouponUrl = strHost + "/HtmlApps/html/common/GatheringClient/Coupon.html?customerId=";//拼优惠券详细页面
                string goPosterUrl = strHost + "/HtmlApps/html/common/GatheringClient/poster.html?customerId=";//拼海报详细页面
                string strOAuthUrl = strHost + "/WXOAuth/AuthUniversal.aspx?scope=snsapi_userinfo&SourceId=3&customerId=";//拼OAuth认证
                foreach (DataRow dr in SetoffToolsList.Tables[0].Rows)
                {
                    if (dr["ToolType"].ToString() == "Coupon")
                    {
                        if (CustomerBasicInfo!=null)
                        {
                            dr["ImageUrl"] = CustomerBasicInfo.SettingValue;
                        }
                        //Oauth认证加商品详情页
                        goUrl = goCouponUrl + CurrentUserInfo.CurrentUser.customer_id + "&ShareVipId=" + CurrentUserInfo.UserID + "&couponId=" + dr["ObjectId"] + "&version=";
                        dr["URL"] = strOAuthUrl + CurrentUserInfo.CurrentUser.customer_id + "&ShareVipID=" + CurrentUserInfo.UserID + "&goUrl=" + System.Web.HttpUtility.UrlEncode(goUrl);
                    }
                    if (dr["ToolType"].ToString() == "SetoffPoster")
                    {
                        var setOffPosterInfo = setOffPosterBLL.QueryByEntity(new SetoffPosterEntity() { SetoffPosterID = new Guid(dr["ObjectId"].ToString()) }, null).FirstOrDefault();
                        if (setOffPosterInfo != null)
                        {
                            var ObjectImgsInfo = ObjectImagesBLL.QueryByEntity(new ObjectImagesEntity() { ImageId = setOffPosterInfo.ImageId }, null).FirstOrDefault();
                            if (ObjectImgsInfo != null)
                            {
                                dr["ImageUrl"] = ObjectImgsInfo.ImageURL;
                            }
                        }
                        //Oauth认证加海报详情页
                        goUrl = goPosterUrl + CurrentUserInfo.CurrentUser.customer_id + "&&ShareVipId=" + CurrentUserInfo.UserID + "&ObjectId=" + dr["ObjectId"] + "&version=";
                        dr["URL"] = strOAuthUrl + CurrentUserInfo.CurrentUser.customer_id + "&ShareVipID=" + CurrentUserInfo.UserID + "&goUrl=" + System.Web.HttpUtility.UrlEncode(goUrl);
                    }
                    if (dr["ToolType"].ToString() == "CTW")
                    {
                        var T_CTW_LEventInfo = T_CTW_LEventBLL.QueryByEntity(new T_CTW_LEventEntity() { CTWEventId = new Guid(dr["ObjectId"].ToString()) }, null).FirstOrDefault();
                        if (T_CTW_LEventInfo != null)
                        {
                            dr["ImageUrl"] = T_CTW_LEventInfo.ImageURL;
                        }
                    }

                }
                setoffToolsRD.SetOffToolsList = DataTableToObject.ConvertToList<SetOffToolsInfo>(SetoffToolsList.Tables[0]);
            }
            if (IncentiveRule != null && IncentiveRule.Tables[0].Rows.Count > 0)
            {
                setoffToolsRD.SetoffRegAwardType = Convert.ToInt32(IncentiveRule.Tables[0].Rows[0]["SetoffRegAwardType"].ToString());//激励类型
                setoffToolsRD.SetoffRegPrize = IncentiveRule.Tables[0].Rows[0]["SetoffRegPrize"].ToString();//激励值
                setoffToolsRD.SetoffOrderPer = Convert.ToDecimal(IncentiveRule.Tables[0].Rows[0]["SetoffOrderPer"].ToString());//订单成交提成比例
            }
            return setoffToolsRD;

        }
    }
}