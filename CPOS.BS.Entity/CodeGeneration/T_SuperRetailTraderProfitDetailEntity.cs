/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2016/6/24 14:53:08
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
using System.Text;
using JIT.Utility;
using JIT.Utility.Entity;

namespace JIT.CPOS.BS.Entity
{
    /// <summary>
    /// ʵ�壺  
    /// </summary>
    public partial class T_SuperRetailTraderProfitDetailEntity : BaseEntity ,IExtensionable
    {
        #region ���캯��
        /// <summary>
        /// ���캯�� 
        /// </summary>
        public T_SuperRetailTraderProfitDetailEntity()
        {
        }
        #endregion     

        #region ���Լ�
		/// <summary>
		/// 
		/// </summary>
		public Guid? Id { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Guid? SuperRetailTraderProfitConfigId { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Guid? SuperRetailTraderID { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? Level { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String ProfitType { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Decimal? Profit { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String OrderType { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String OrderId { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String OrderNo { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public DateTime? OrderDate { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Decimal? OrderActualAmount { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String SalesId { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String VipId { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String CreateBy { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public DateTime? CreateTime { get; set; }

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
		public String CustomerId { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? IsDelete { get; set; }


        #endregion
        
        #region ����������[��ȡ/����]����ֵ,����Ӧ�ò������ģʽ��д�÷���
        /// <summary>
        /// ������������ȡ����ֵ,����Ӧ�ò������ģʽ��д�÷���
        /// </summary>
        /// <param name="pPropertyName">������</param>
        /// <param name="pValue">ֵ</param>
        /// <returns>�Ƿ��ȡ�ɹ�</returns>
        public  bool GetValueByPropertyName(string pPropertyName, out object pValue)
        {
            pValue = null;
            //��������
            if (string.IsNullOrEmpty(pPropertyName))
                throw new ArgumentException("pPropertyName����Ϊ�ջ�null.");
            //��ȡ����
            bool result = false;
            switch (pPropertyName)
            {
				case "Id":
					pValue = this.Id;
					result = true;
					break;
				case "SuperRetailTraderProfitConfigId":
					pValue = this.SuperRetailTraderProfitConfigId;
					result = true;
					break;
				case "SuperRetailTraderID":
					pValue = this.SuperRetailTraderID;
					result = true;
					break;
				case "Level":
					pValue = this.Level;
					result = true;
					break;
				case "ProfitType":
					pValue = this.ProfitType;
					result = true;
					break;
				case "Profit":
					pValue = this.Profit;
					result = true;
					break;
				case "OrderType":
					pValue = this.OrderType;
					result = true;
					break;
				case "OrderId":
					pValue = this.OrderId;
					result = true;
					break;
				case "OrderNo":
					pValue = this.OrderNo;
					result = true;
					break;
				case "OrderDate":
					pValue = this.OrderDate;
					result = true;
					break;
				case "OrderActualAmount":
					pValue = this.OrderActualAmount;
					result = true;
					break;
				case "SalesId":
					pValue = this.SalesId;
					result = true;
					break;
				case "VipId":
					pValue = this.VipId;
					result = true;
					break;
				case "CreateBy":
					pValue = this.CreateBy;
					result = true;
					break;
				case "CreateTime":
					pValue = this.CreateTime;
					result = true;
					break;
				case "LastUpdateBy":
					pValue = this.LastUpdateBy;
					result = true;
					break;
				case "LastUpdateTime":
					pValue = this.LastUpdateTime;
					result = true;
					break;
				case "CustomerId":
					pValue = this.CustomerId;
					result = true;
					break;
				case "IsDelete":
					pValue = this.IsDelete;
					result = true;
					break;

            }
            //����
            return result;
        }
        /// <summary>
        /// ��������������ֵ������Ӧ�ò������ģʽ��д�÷���
        /// </summary>
        /// <param name="pPropertyName">������</param>
        /// <param name="pValue">ֵ</param>
        /// <returns>�Ƿ����óɹ�</returns>
        public bool SetValueByPropertyName(string pPropertyName, object pValue)
        {
            //��������
            if (string.IsNullOrEmpty(pPropertyName))
                throw new ArgumentException("pPropertyName����Ϊ�ջ�null.");
            //��������
            bool result = false;
            switch (pPropertyName)
            {
				case "Id":
					{
						if (pValue != null)
							this.Id = (Guid)pValue;
						else
							this.Id = null;
						result = true;
					 }
					break;
				case "SuperRetailTraderProfitConfigId":
					{
						if (pValue != null)
							this.SuperRetailTraderProfitConfigId = (Guid)pValue;
						else
							this.SuperRetailTraderProfitConfigId = null;
						result = true;
					 }
					break;
				case "SuperRetailTraderID":
					{
						if (pValue != null)
							this.SuperRetailTraderID = (Guid)pValue;
						else
							this.SuperRetailTraderID = null;
						result = true;
					 }
					break;
				case "Level":
					{
						if (pValue != null)
							this.Level =  Convert.ToInt32(pValue);
						else
							this.Level = null;
						result = true;
					 }
					break;
				case "ProfitType":
					{
						if (pValue != null)
							this.ProfitType = Convert.ToString(pValue);
						else
							this.ProfitType = null;
						result = true;
					 }
					break;
				case "Profit":
					{
						if (pValue != null)
							this.Profit = Convert.ToDecimal(pValue);
						else
							this.Profit = null;
						result = true;
					 }
					break;
				case "OrderType":
					{
						if (pValue != null)
							this.OrderType = Convert.ToString(pValue);
						else
							this.OrderType = null;
						result = true;
					 }
					break;
				case "OrderId":
					{
						if (pValue != null)
							this.OrderId = Convert.ToString(pValue);
						else
							this.OrderId = null;
						result = true;
					 }
					break;
				case "OrderNo":
					{
						if (pValue != null)
							this.OrderNo = Convert.ToString(pValue);
						else
							this.OrderNo = null;
						result = true;
					 }
					break;
				case "OrderDate":
					{
						if (pValue != null)
							this.OrderDate = Convert.ToDateTime(pValue);
						else
							this.OrderDate = null;
						result = true;
					 }
					break;
				case "OrderActualAmount":
					{
						if (pValue != null)
							this.OrderActualAmount = Convert.ToDecimal(pValue);
						else
							this.OrderActualAmount = null;
						result = true;
					 }
					break;
				case "SalesId":
					{
						if (pValue != null)
							this.SalesId = Convert.ToString(pValue);
						else
							this.SalesId = null;
						result = true;
					 }
					break;
				case "VipId":
					{
						if (pValue != null)
							this.VipId = Convert.ToString(pValue);
						else
							this.VipId = null;
						result = true;
					 }
					break;
				case "CreateBy":
					{
						if (pValue != null)
							this.CreateBy = Convert.ToString(pValue);
						else
							this.CreateBy = null;
						result = true;
					 }
					break;
				case "CreateTime":
					{
						if (pValue != null)
							this.CreateTime = Convert.ToDateTime(pValue);
						else
							this.CreateTime = null;
						result = true;
					 }
					break;
				case "LastUpdateBy":
					{
						if (pValue != null)
							this.LastUpdateBy = Convert.ToString(pValue);
						else
							this.LastUpdateBy = null;
						result = true;
					 }
					break;
				case "LastUpdateTime":
					{
						if (pValue != null)
							this.LastUpdateTime = Convert.ToDateTime(pValue);
						else
							this.LastUpdateTime = null;
						result = true;
					 }
					break;
				case "CustomerId":
					{
						if (pValue != null)
							this.CustomerId = Convert.ToString(pValue);
						else
							this.CustomerId = null;
						result = true;
					 }
					break;
				case "IsDelete":
					{
						if (pValue != null)
							this.IsDelete =  Convert.ToInt32(pValue);
						else
							this.IsDelete = null;
						result = true;
					 }
					break;

            }
            //���ó־û�״̬
            if (result == true && this.PersistenceHandle != null)
                this.PersistenceHandle.Modify();
            //����
            return result;
        }
       
        #endregion
    }
}