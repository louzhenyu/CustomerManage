/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/3/26 16:10:53
 * Description	:
 * 1st Modified On	:
 * 1st Modified By	:
 * 1st Modified Desc:
 * 1st Modifier EMail	:
 * 2st Modified On	:
 * 2st Modified By	:
 * 2st Modifier EMail	:
 * 2st Modified Desc:
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Reflection;
using JIT.Utility;
using JIT.Utility.ExtensionMethod;
using JIT.CPOS.BS.DataAccess;
using JIT.CPOS.BS.Entity;
using JIT.Utility.DataAccess;
using JIT.Utility.DataAccess.Query;
using System.Collections;

namespace JIT.CPOS.BS.BLL
{
    /// <summary>
    /// 业务处理：  
    /// </summary>
    public partial class CustomerBasicSettingBLL
    {
        public bool CheckSKUExist()
        {
            //默认有SKU
            bool result = true;
            var setting = Query(new IWhereCondition[] { 
                new EqualsCondition() { FieldName = "SettingCode", Value = "SKUExist"},
                new EqualsCondition() { FieldName = "CustomerID", Value = this.CurrentUserInfo.ClientID}
            }, null);

            if (setting != null && setting.Length > 0)
            {
                //若大于0，则显示SKU
                result = Convert.ToInt32(setting[0].SettingValue) > 0;
            }

            return result;
        }
        /// <summary>
        /// 取数据库配置表中CustomerBasicSetting附近范围
        /// </summary>
        /// <returns></returns>
        public int SearchRangeAccessoriesStores()
        {
            string setValue = string.Empty;
            var setting = Query(new IWhereCondition[] {
            new EqualsCondition(){FieldName="SettingCode",Value="RangeAccessoriesStores"},
            new EqualsCondition(){FieldName="IsDelete",Value="0"},
            new EqualsCondition(){FieldName="CustomerID",Value=this.CurrentUserInfo.ClientID}            
            }, null);
            if (setting != null && setting.Length > 0)
            {
                setValue = setting[0].SettingValue;
            }
            return Convert.ToInt32(setValue);
        }

        /// <summary>
        /// 是否显示搜索栏
        /// </summary>
        /// <returns></returns>
        public bool CheckIsSearchAccessoriesStores()
        {
            bool result = true;
            var setting = Query(new IWhereCondition[] { 
                new EqualsCondition() { FieldName = "SettingCode", Value = "IsSearchAccessoriesStores"},
                new EqualsCondition() { FieldName = "CustomerID", Value = this.CurrentUserInfo.ClientID}
            }, null);

            if (setting != null && setting.Length > 0)
            {
                //若大于0，则显示搜索栏
                result = Convert.ToInt32(setting[0].SettingValue) > 0;
            }
            return result;
        }
        /// <summary>
        /// 是否显示所有门店
        /// </summary>
        /// <returns></returns>
        public bool CheckIsAllAccessoriesStores()
        {
            bool result = true;
            var setting = Query(new IWhereCondition[] { 
                new EqualsCondition() { FieldName = "SettingCode", Value = "IsAllAccessoriesStores"},
                new EqualsCondition() { FieldName = "CustomerID", Value = this.CurrentUserInfo.ClientID}
            }, null);

            if (setting != null && setting.Length > 0)
            {
                //若大于0，则显示搜索栏
                result = Convert.ToInt32(setting[0].SettingValue) > 0;
            }
            return result;
        }
        /// <summary>
        /// 转发消息图标
        /// </summary>
        /// <returns></returns>
        public string GetForwardingMessageLogo()
        {
            string result = string.Empty;
            var setting = Query(new IWhereCondition[] { 
                new EqualsCondition() { FieldName = "SettingCode", Value = "ForwardingMessageLogo"},
                new EqualsCondition() { FieldName = "CustomerID", Value = this.CurrentUserInfo.ClientID}
            }, null);

            if (setting != null && setting.Length > 0)
            {
                result = setting[0].SettingValue;
            }
            return result;
        }
        /// <summary>
        /// 转发消息默认标题
        /// </summary>
        /// <returns></returns>
        public string GetForwardingMessageTitle()
        {
            string result = string.Empty;
            var setting = Query(new IWhereCondition[] { 
                new EqualsCondition() { FieldName = "SettingCode", Value = "ForwardingMessageTitle"},
                new EqualsCondition() { FieldName = "CustomerID", Value = this.CurrentUserInfo.ClientID}
            }, null);

            if (setting != null && setting.Length > 0)
            {
                //若大于0，则显示搜索栏
                result = setting[0].SettingValue;
            }
            return result;
        }
        /// <summary>
        /// 转发消息默认摘要文字
        /// </summary>
        /// <returns></returns>
        public string GetForwardingMessageSummary()
        {
            string result = string.Empty;
            var setting = Query(new IWhereCondition[] { 
                new EqualsCondition() { FieldName = "SettingCode", Value = "ForwardingMessageSummary"},
                new EqualsCondition() { FieldName = "CustomerID", Value = this.CurrentUserInfo.ClientID}
            }, null);

            if (setting != null && setting.Length > 0)
            {
                //若大于0，则显示搜索栏
                result = setting[0].SettingValue;
            }
            return result;
        }

        public string[] GetMemberBenefits(string pCustomerID)
        {
            var temp = this._currentDAO.GetBasicSettings(pCustomerID, "MemberBenefits");
            return temp.Select(t => t.SettingValue).ToArray();
        }

        public DataSet GetCustomerBaiscSettingInfo(string customerId)
        {
            return this._currentDAO.GetCustomerBaiscSettingInfo(customerId);
        }

        /// <summary>
        /// 根据SettingCode获取描述信息
        /// </summary>
        /// <returns></returns>
        public string GetSettingValueByCode(string settingCode)
        {
            string result = string.Empty;
            var setting = Query(new IWhereCondition[] { 
                new EqualsCondition() { FieldName = "SettingCode", Value =settingCode},
                new EqualsCondition() { FieldName = "CustomerID", Value = this.CurrentUserInfo.ClientID}
            }, null);

            if (setting != null && setting.Length > 0)
            {
                //若大于0，则显示搜索栏
                result = setting[0].SettingValue;
            }
            return result;
        }
        /// <summary>
        /// 获取社会化销售配置和积分返现配置
        /// </summary>
        /// <param name="salesPrice"></param>
        /// <param name="channelId"></param>
        /// <returns></returns>
        public Hashtable GetSocialSetting()
        {
            //所有商户配置
            var settingList =this._currentDAO.QueryByEntity(new CustomerBasicSettingEntity() { CustomerID = this.CurrentUserInfo.ClientID }, null);

            #region 社会化销售配置
            //社会化销售设置类型 0=按订单；1=按商品
            int socialSalesType = 0;
            var socialSalesTypeSetting = settingList.Where(t => t.SettingCode == "SocialSalesType").FirstOrDefault();
            //启用员工分销设置(0=不启用；1=启用)
            int enableEmployeeSales = 0;
            CustomerBasicSettingEntity enableEmployeeSalesSetting = settingList.Where(t => t.SettingCode == "EnableEmployeeSales").FirstOrDefault();
            //员工的商品分销价比例 
            decimal eDistributionPricePer = 0;
            CustomerBasicSettingEntity EDistributionPricePerSetting = settingList.Where(t => t.SettingCode == "EDistributionPricePer").FirstOrDefault();
            //启用会员分销设置(0=不启用；1=启用)
            int enableVipSales = 0;
            var EnableVipSalesSetting = settingList.Where(t => t.SettingCode == "EnableVipSales").FirstOrDefault();
            //会员的商品分销价比例
            decimal vDistributionPricePer = 0;
            var VDistributionPricePerSetting = settingList.Where(t => t.SettingCode == "VDistributionPricePer").FirstOrDefault();
            //员工的订单金额提成比例
            decimal eOrderCommissionPer = 0;
            var eOrderCommissionPerSetting = settingList.Where(t => t.SettingCode == "EOrderCommissionPer").FirstOrDefault();
            //会员的订单金额提成比例
            decimal vOrderCommissionPer = 0;
            var vOrderCommissionPerSetting = settingList.Where(t => t.SettingCode == "VOrderCommissionPer").FirstOrDefault();

            if (socialSalesTypeSetting != null)
                socialSalesType = int.Parse(socialSalesTypeSetting.SettingValue);

            if (enableEmployeeSalesSetting != null)
                enableEmployeeSales = int.Parse(enableEmployeeSalesSetting.SettingValue);

            if (EDistributionPricePerSetting != null)
                eDistributionPricePer = decimal.Parse(EDistributionPricePerSetting.SettingValue);

            if (EnableVipSalesSetting != null)
                enableVipSales = int.Parse(EnableVipSalesSetting.SettingValue);

            if (VDistributionPricePerSetting != null)
                vDistributionPricePer = decimal.Parse(VDistributionPricePerSetting.SettingValue);

            if (eOrderCommissionPerSetting != null)
                eOrderCommissionPer = decimal.Parse(eOrderCommissionPerSetting.SettingValue);
            if (vOrderCommissionPerSetting != null)
                vOrderCommissionPer = decimal.Parse(vOrderCommissionPerSetting.SettingValue);
            #endregion

            #region 积分返现配置
            //奖励类型 0=按订单；1=按商品
            var rewardsTypeSetting = settingList.Where(t => t.SettingCode == "RewardsType").FirstOrDefault();
            //积分启用配置
            var enableIntegralSetting = settingList.Where(t => t.SettingCode == "EnableIntegral").FirstOrDefault();
            //返现启用配置
            var enableRewardCashSetting = settingList.Where(t => t.SettingCode == "EnableRewardCash").FirstOrDefault();
            //返回积分比例
            var rewardPointsPerSetting = settingList.Where(t => t.SettingCode == "RewardPointsPer").FirstOrDefault();
            //积分最低使用限制
            var pointsRedeemLowestLimitSetting = settingList.Where(t => t.SettingCode == "PointsRedeemLowestLimit").FirstOrDefault();
            //每单赠送积分上限
            var pointsOrderUpLimitSetting = settingList.Where(t => t.SettingCode == "PointsOrderUpLimit").FirstOrDefault();
            //返现比例
            var rewardCashPerSetting = settingList.Where(t => t.SettingCode == "RewardCashPer").FirstOrDefault();
            //返现最低使用限制
            var cashRedeemLowestLimitSetting = settingList.Where(t => t.SettingCode == "CashRedeemLowestLimit").FirstOrDefault();
            //每单返现上限
            var cashOrderUpLimitSetting = settingList.Where(t => t.SettingCode == "CashOrderUpLimit").FirstOrDefault();
            //积分使用上限比例
            var pointsRedeemUpLimitSetting = settingList.Where(t => t.SettingCode == "PointsRedeemUpLimit").FirstOrDefault();
            //返现使用上限比例
            var cashRedeemUpLimitSetting = settingList.Where(t => t.SettingCode == "CashRedeemUpLimit").FirstOrDefault();

            int rewardsType = 0;
            int enableIntegral = 0;
            int enableRewardCash = 0;
            decimal rewardPointsPer = 0;
            decimal rewardCashPer = 0;
            decimal pointsRedeemLowestLimit = 0;
            decimal cashRedeemLowestLimit = 0;
            int pointsOrderUpLimit = 0;
            decimal cashOrderUpLimit = 0;
            decimal pointsRedeemUpLimit = 0;
            decimal cashRedeemUpLimit = 0;


            if (rewardsTypeSetting != null)
                rewardsType = int.Parse(rewardsTypeSetting.SettingValue);
            if (enableIntegralSetting != null)
                enableIntegral = int.Parse(enableIntegralSetting.SettingValue);
            if (enableRewardCashSetting != null)
                enableRewardCash = int.Parse(enableRewardCashSetting.SettingValue);
            if (rewardPointsPerSetting != null)
                rewardPointsPer = decimal.Parse(rewardPointsPerSetting.SettingValue);
            if (rewardCashPerSetting != null)
                rewardCashPer = decimal.Parse(rewardCashPerSetting.SettingValue);
            if (pointsRedeemLowestLimitSetting != null)
                pointsRedeemLowestLimit = decimal.Parse(pointsRedeemLowestLimitSetting.SettingValue);
            if (cashRedeemLowestLimitSetting != null)
                cashRedeemLowestLimit = decimal.Parse(cashRedeemLowestLimitSetting.SettingValue);
            if (pointsOrderUpLimitSetting != null)
                pointsOrderUpLimit = int.Parse(pointsOrderUpLimitSetting.SettingValue);
            if (cashOrderUpLimitSetting != null)
                cashOrderUpLimit = decimal.Parse(cashOrderUpLimitSetting.SettingValue);
            if (pointsRedeemUpLimitSetting != null)
                pointsRedeemUpLimit = decimal.Parse(pointsRedeemUpLimitSetting.SettingValue);
            if (cashRedeemUpLimitSetting != null)
                cashRedeemUpLimit = decimal.Parse(cashRedeemUpLimitSetting.SettingValue);

            #endregion


            Hashtable htSetting = new Hashtable();
            htSetting["socialSalesType"] = socialSalesType;
            htSetting["enableEmployeeSales"] = enableEmployeeSales;
            htSetting["eDistributionPricePer"] = eDistributionPricePer;
            htSetting["enableVipSales"] = enableVipSales;
            htSetting["vDistributionPricePer"] = vDistributionPricePer;
            htSetting["eOrderCommissionPer"] = eOrderCommissionPer;
            htSetting["vOrderCommissionPer"] = vOrderCommissionPer;

            htSetting["rewardsType"] = rewardsType;
            htSetting["enableIntegral"] = enableIntegral;
            htSetting["enableRewardCash"] = enableRewardCash;
            htSetting["rewardPointsPer"] = rewardPointsPer;
            htSetting["rewardCashPer"] = rewardCashPer;
            htSetting["pointsRedeemLowestLimit"] = pointsRedeemLowestLimit;
            htSetting["cashRedeemLowestLimit"] = cashRedeemLowestLimit;

            htSetting["pointsOrderUpLimit"] = pointsOrderUpLimit;
            htSetting["cashOrderUpLimit"] = cashOrderUpLimit;

            htSetting["pointsRedeemUpLimit"] = pointsRedeemUpLimit;
            htSetting["cashRedeemUpLimit"] = cashRedeemUpLimit;
            return htSetting;
        }
    }
}