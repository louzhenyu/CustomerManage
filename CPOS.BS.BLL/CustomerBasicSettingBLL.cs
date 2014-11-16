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
    }
}