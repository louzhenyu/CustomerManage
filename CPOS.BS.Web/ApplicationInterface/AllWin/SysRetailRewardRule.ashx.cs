using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Web.Session;
using JIT.CPOS.DTO.Base;
using JIT.Utility.ExtensionMethod;
using JIT.Utility.Log;
using Aspose.Cells;
using JIT.Utility;
using JIT.CPOS.BS.Web.Base.Excel;
using JIT.CPOS.BS.Web.ApplicationInterface.Base;
using JIT.CPOS.BS.Entity;
using System.Web.Script.Serialization;
using JIT.CPOS.BS.Web.ApplicationInterface.Vip;

namespace JIT.CPOS.BS.Web.ApplicationInterface.AllWin
{
    /// <summary>
    /// SysRetailRewardRule 的摘要说明
    /// </summary>
    public class SysRetailRewardRule : BaseGateway
    {

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

                case "SaveRetailRewardRule"://  保存奖励模板（含分销商）
                    rst = this.SaveRetailRewardRule(pRequest);
                    break;
                case "GetSysRetailRewardRule":// 获取奖励模板数据（/或者某个分销商的数据）
                    rst = this.GetSysRetailRewardRule(pRequest);
                    break;
        
                default:
                    throw new APIException(string.Format("找不到名为：{0}的action处理方法.", pAction))
                    {
                        ErrorCode = ERROR_CODES.INVALID_REQUEST_CAN_NOT_FIND_ACTION_HANDLER
                    };
            }
            return rst;
        }

        #region   保存奖励规则
        public string SaveRetailRewardRule(string pRequest)
        {
            var rp = pRequest.DeserializeJSONTo<APIRequest<SaveRetailRewardRuleRP>>();

            var loggingSessionInfo = new SessionManager().CurrentUserLoginInfo;
            var bll = new SysRetailRewardRuleBLL(loggingSessionInfo);
            var rd = new SaveRetailRewardRuleRD();
            var rsp = new SuccessResponse<IAPIResponseData>(rd);
            if (rp.Parameters.IsTemplate == null)
            {
                throw new APIException("缺少参数【IsTemplate】或参数值为空") { ErrorCode = 135 };
            }
            //if (string.IsNullOrEmpty(rp.Parameters.CooperateType))
            //{
            //    throw new APIException("缺少参数【CooperateType】或参数值为空") { ErrorCode = 135 };
            //}
            if (rp.Parameters.IsTemplate == 0 && string.IsNullOrEmpty(rp.Parameters.RetailTraderID))
            {
                throw new APIException("缺少参数【RetailTraderID】或参数值为空") { ErrorCode = 135 };
            }
            if (rp.Parameters.SysRetailRewardRuleList==null)
            {
                throw new APIException("缺少参数【SysRetailRewardRuleList】或参数值为空") { ErrorCode = 135 };
            }

            //如果是分销商，需要改变分销商的相关数据
            RetailTraderBLL _RetailTraderBLL = new RetailTraderBLL(loggingSessionInfo);

            //先要删除相关的数据
            //如果是模板的就设置该合作方式下的分销商的上次使用的相关数据为已经被删除isdelete=0                
            //如果是分销商，就删除该分销商下上次使用的相关数据。            
            //bll.UpdateSysRetailRewardRule(rp.Parameters.IsTemplate, rp.Parameters.CooperateType, rp.Parameters.RetailTraderID, loggingSessionInfo.ClientID);
            if (rp.Parameters.IsTemplate==0)//非模板时
            {
                 bll.UpdateSysRetailRewardRule(rp.Parameters.IsTemplate, "","", rp.Parameters.RetailTraderID, loggingSessionInfo.ClientID);
        
                //RetailTraderEntity enRT = new RetailTraderEntity();
                //enRT.RetailTraderID = rp.Parameters.RetailTraderID;
                //enRT.CooperateType = "";//修改合作方式
                //enRT.SalesType = "";//修改销售方式
                //_RetailTraderBLL.Update(enRT, null, false);//不更新空的
            }
            else
            {
                bll.UpdateSysRetailRewardRule(rp.Parameters.IsTemplate, "", "", rp.Parameters.RetailTraderID, loggingSessionInfo.ClientID);
            }
         
        

            //获取分销商的信息，loggingSessionInfo.ClientID
            foreach (var item in rp.Parameters.SysRetailRewardRuleList)
            {
                if (rp.Parameters.IsTemplate == 0 && !string.IsNullOrEmpty(rp.Parameters.RetailTraderID))//
                {
                    if (item.CooperateType != "Sales")
                    {
                        RetailTraderEntity enRT = new RetailTraderEntity();
                        enRT.RetailTraderID = rp.Parameters.RetailTraderID;
                        enRT.CooperateType = item.CooperateType;//修改合作方式
                        _RetailTraderBLL.Update(enRT, null, false);//不更新空的
                    }
                    else
                    {
                        RetailTraderEntity enRT = new RetailTraderEntity();
                        enRT.RetailTraderID = rp.Parameters.RetailTraderID;
                        enRT.SalesType = item.CooperateType;//修改销售方式
                        _RetailTraderBLL.Update(enRT, null, false);//不更新空的
                    }
                }

                SysRetailRewardRuleEntity en = new SysRetailRewardRuleEntity();
                en.RetailRewardRuleID = Guid.NewGuid().ToString();//每次都创建新的
                en.CooperateType = item.CooperateType;//合作方式

                en.RewardTypeName = item.RewardTypeName;
                en.RewardTypeCode = item.RewardTypeCode;
                en.IsTemplate = rp.Parameters.IsTemplate;//是否模板
                en.SellUserReward = item.SellUserReward;               
                en.RetailTraderReward = item.RetailTraderReward;
                en.ItemSalesPriceRate = (item.ItemSalesPriceRate == null ? 0 : item.ItemSalesPriceRate);//销售设置
                en.AmountOrPercent = item.AmountOrPercent;

                en.CreateTime = DateTime.Now;
                en.CreateBy = loggingSessionInfo.UserID;
                en.LastUpdateTime = DateTime.Now;
                en.LastUpdateBy = loggingSessionInfo.UserID;
                if (item.CooperateType == "Sales" && item.ItemSalesPriceRate == 0)
                {
                    en.Status = "0";
                }
                else
                    en.Status = "1";
                en.IsDelete = 0;
                en.CustomerId = loggingSessionInfo.ClientID;
                
                en.BeginTime = DateTime.Now;//开始时间
                en.EndTime = null;
                
                en.RetailTraderID = rp.Parameters.RetailTraderID;//如果是模板的就不要加
                bll.Create(en);//保存奖励规则
            }



          

            return rsp.ToJSON();
        }
        #endregion
        
        #region   获取奖励规则
        public string GetSysRetailRewardRule(string pRequest)
        {
            var rp = pRequest.DeserializeJSONTo<APIRequest<SaveRetailRewardRuleRP>>();

            var loggingSessionInfo = new SessionManager().CurrentUserLoginInfo;
            var bll = new SysRetailRewardRuleBLL(loggingSessionInfo);
            var rd = new SaveRetailRewardRuleRD();
            var rsp = new SuccessResponse<IAPIResponseData>(rd);
            if (rp.Parameters.IsTemplate != 0 && rp.Parameters.IsTemplate != 1)
            {
                throw new APIException("缺少参数【IsTemplate】或参数值为空") { ErrorCode = 135 };
            }
            if ( string.IsNullOrEmpty(  rp.Parameters.CooperateType))
            {
                throw new APIException("缺少参数【CooperateType】或参数值为空") { ErrorCode = 135 };
            }
            if (rp.Parameters.IsTemplate == 0 && string.IsNullOrEmpty(rp.Parameters.RetailTraderID))
            {
                throw new APIException("缺少参数【RetailTraderID】或参数值为空") { ErrorCode = 135 };
            }


            SysRetailRewardRuleEntity[] ds = null;
         
            //如果是分销商，需要改变分销商的相关数据
            if (rp.Parameters.IsTemplate == 1)
            {
                SysRetailRewardRuleEntity en = new SysRetailRewardRuleEntity();
                en.IsTemplate=1;
                en.CooperateType=rp.Parameters.CooperateType;
                en.IsDelete=0;
                en.CustomerId = loggingSessionInfo.ClientID;//不要少写了CustomerId
                //获取奖励模板
                ds = bll.GetSysRetailRewardRule(en);
                //ds=bll.QueryByEntity(en, null);
            }
            else {
                SysRetailRewardRuleEntity en = new SysRetailRewardRuleEntity();
                en.IsTemplate=0;
                //en.CooperateType=rp.Parameters.CooperateType;
                en.RetailTraderID = rp.Parameters.RetailTraderID;
                en.IsDelete=0;
                en.CustomerId = loggingSessionInfo.ClientID;//不要少写了CustomerId
                //获取奖励模板
                ds = bll.GetSysRetailRewardRule(en);
                //如果该分销商没有奖励规则，就取他所属的合作类型的奖励模板的数据
                //if (ds == null || ds.Count() <= 0)
                //{
                //    SysRetailRewardRuleEntity en2 = new SysRetailRewardRuleEntity();
                //    en2.IsTemplate = 1;
                //    en2.CooperateType = rp.Parameters.CooperateType;
                //    en2.IsDelete = 0;
                //    en2.CustomerId = loggingSessionInfo.ClientID;//不要少写了CustomerId
                //    //获取奖励模板
                //    ds = bll.GetSysRetailRewardRule(en2);
                //}

            }



            rd.SysRetailRewardRuleList = ds;
          

            return rsp.ToJSON();
        }
        #endregion

      
    }
    public class SaveRetailRewardRuleRP : IAPIRequestParameter
    {

        public int IsTemplate { get; set; }
        public string CooperateType { get; set; }
        public string RetailTraderID { get; set; }
        public List<RetailRewardRuleInfo> SysRetailRewardRuleList { get; set; }
        public void Validate()
        {
        }
    }

    public class SaveRetailRewardRuleRD : IAPIResponseData
    {
        public SysRetailRewardRuleEntity[] SysRetailRewardRuleList { get; set; }


    }

    public class RetailRewardRuleInfo {
        public String RetailRewardRuleID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String CooperateType { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String RewardTypeName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String RewardTypeCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Decimal? SellUserReward { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Int32? IsTemplate { get; set; }

        /// <summary>
        /// 
        /// </summary>

        public Decimal? RetailTraderReward { get; set; }
        public Decimal? ItemSalesPriceRate { get; set; }
   
        public Int32? AmountOrPercent { get; set; }

        
        /// <summary>
        /// 
        /// </summary>
        public String Status { get; set; }

     
        public DateTime? BeginTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? EndTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String RetailTraderID { get; set; }


    }


}