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
    /// ҵ����  
    /// </summary>
    public partial class CustomerBasicSettingBLL
    {
        public bool CheckSKUExist()
        {
            //Ĭ����SKU
            bool result = true;
            var setting = Query(new IWhereCondition[] { 
                new EqualsCondition() { FieldName = "SettingCode", Value = "SKUExist"},
                new EqualsCondition() { FieldName = "CustomerID", Value = this.CurrentUserInfo.ClientID}
            }, null);

            if (setting != null && setting.Length > 0)
            {
                //������0������ʾSKU
                result = Convert.ToInt32(setting[0].SettingValue) > 0;
            }

            return result;
        }
        /// <summary>
        /// ȡ���ݿ����ñ���CustomerBasicSetting������Χ
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
        /// �Ƿ���ʾ������
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
                //������0������ʾ������
                result = Convert.ToInt32(setting[0].SettingValue) > 0;
            }
            return result;
        }
        /// <summary>
        /// �Ƿ���ʾ�����ŵ�
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
                //������0������ʾ������
                result = Convert.ToInt32(setting[0].SettingValue) > 0;
            }
            return result;
        }
        /// <summary>
        /// ת����Ϣͼ��
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
        /// ת����ϢĬ�ϱ���
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
                //������0������ʾ������
                result = setting[0].SettingValue;
            }
            return result;
        }
        /// <summary>
        /// ת����ϢĬ��ժҪ����
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
                //������0������ʾ������
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
        /// ����SettingCode��ȡ������Ϣ
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
                //������0������ʾ������
                result = setting[0].SettingValue;
            }
            return result;
        }
        /// <summary>
        /// ��ȡ��ữ�������úͻ��ַ�������
        /// </summary>
        /// <param name="salesPrice"></param>
        /// <param name="channelId"></param>
        /// <returns></returns>
        public Hashtable GetSocialSetting()
        {
            //�����̻�����
            var settingList =this._currentDAO.QueryByEntity(new CustomerBasicSettingEntity() { CustomerID = this.CurrentUserInfo.ClientID }, null);

            #region ��ữ��������
            //��ữ������������ 0=��������1=����Ʒ
            int socialSalesType = 0;
            var socialSalesTypeSetting = settingList.Where(t => t.SettingCode == "SocialSalesType").FirstOrDefault();
            //����Ա����������(0=�����ã�1=����)
            int enableEmployeeSales = 0;
            CustomerBasicSettingEntity enableEmployeeSalesSetting = settingList.Where(t => t.SettingCode == "EnableEmployeeSales").FirstOrDefault();
            //Ա������Ʒ�����۱��� 
            decimal eDistributionPricePer = 0;
            CustomerBasicSettingEntity EDistributionPricePerSetting = settingList.Where(t => t.SettingCode == "EDistributionPricePer").FirstOrDefault();
            //���û�Ա��������(0=�����ã�1=����)
            int enableVipSales = 0;
            var EnableVipSalesSetting = settingList.Where(t => t.SettingCode == "EnableVipSales").FirstOrDefault();
            //��Ա����Ʒ�����۱���
            decimal vDistributionPricePer = 0;
            var VDistributionPricePerSetting = settingList.Where(t => t.SettingCode == "VDistributionPricePer").FirstOrDefault();
            //Ա���Ķ��������ɱ���
            decimal eOrderCommissionPer = 0;
            var eOrderCommissionPerSetting = settingList.Where(t => t.SettingCode == "EOrderCommissionPer").FirstOrDefault();
            //��Ա�Ķ��������ɱ���
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

            #region ���ַ�������
            //�������� 0=��������1=����Ʒ
            var rewardsTypeSetting = settingList.Where(t => t.SettingCode == "RewardsType").FirstOrDefault();
            //������������
            var enableIntegralSetting = settingList.Where(t => t.SettingCode == "EnableIntegral").FirstOrDefault();
            //������������
            var enableRewardCashSetting = settingList.Where(t => t.SettingCode == "EnableRewardCash").FirstOrDefault();
            //���ػ��ֱ���
            var rewardPointsPerSetting = settingList.Where(t => t.SettingCode == "RewardPointsPer").FirstOrDefault();
            //�������ʹ������
            var pointsRedeemLowestLimitSetting = settingList.Where(t => t.SettingCode == "PointsRedeemLowestLimit").FirstOrDefault();
            //ÿ�����ͻ�������
            var pointsOrderUpLimitSetting = settingList.Where(t => t.SettingCode == "PointsOrderUpLimit").FirstOrDefault();
            //���ֱ���
            var rewardCashPerSetting = settingList.Where(t => t.SettingCode == "RewardCashPer").FirstOrDefault();
            //�������ʹ������
            var cashRedeemLowestLimitSetting = settingList.Where(t => t.SettingCode == "CashRedeemLowestLimit").FirstOrDefault();
            //ÿ����������
            var cashOrderUpLimitSetting = settingList.Where(t => t.SettingCode == "CashOrderUpLimit").FirstOrDefault();
            //����ʹ�����ޱ���
            var pointsRedeemUpLimitSetting = settingList.Where(t => t.SettingCode == "PointsRedeemUpLimit").FirstOrDefault();
            //����ʹ�����ޱ���
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