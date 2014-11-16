/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/6/25 13:29:48
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
    public partial class PowerHourEntity : BaseEntity ,IExtensionable
    {
        #region ���캯��
        /// <summary>
        /// ���캯�� 
        /// </summary>
        public PowerHourEntity()
        {
        }
        #endregion     

        #region ���Լ�
		/// <summary>
		/// 
		/// </summary>
		public Guid? PowerHourID { get; set; }

		/// <summary>
		/// ��ѵ��ַ
		/// </summary>
		public String SiteAddress { get; set; }

		/// <summary>
		/// ��ѵ��ʦ��UserID(���)
		/// </summary>
		public String TrainerID { get; set; }

		/// <summary>
		/// ��ѵ����
		/// </summary>
		public String Topic { get; set; }

		/// <summary>
		/// ��ѵ���ڳ���(���)
		/// </summary>
		public String CityID { get; set; }

		/// <summary>
		/// ��ѵ��ʼʱ��
		/// </summary>
		public DateTime? StartTime { get; set; }

		/// <summary>
		/// ��ѵ����ʱ��
		/// </summary>
		public DateTime? EndTime { get; set; }

		/// <summary>
		/// ��ѵ�ֳ���Ƭ
		/// </summary>
		public String SitePictureUrl { get; set; }

		/// <summary>
		/// ��ѵ��������
		/// </summary>
		public Int32? FinanceYear { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String CustomerID { get; set; }

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
		public Int32? IsDelete { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? ApproveState { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String Approver { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public DateTime? ApproveTime { get; set; }


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
				case "PowerHourID":
					pValue = this.PowerHourID;
					result = true;
					break;
				case "SiteAddress":
					pValue = this.SiteAddress;
					result = true;
					break;
				case "TrainerID":
					pValue = this.TrainerID;
					result = true;
					break;
				case "Topic":
					pValue = this.Topic;
					result = true;
					break;
				case "CityID":
					pValue = this.CityID;
					result = true;
					break;
				case "StartTime":
					pValue = this.StartTime;
					result = true;
					break;
				case "EndTime":
					pValue = this.EndTime;
					result = true;
					break;
				case "SitePictureUrl":
					pValue = this.SitePictureUrl;
					result = true;
					break;
				case "FinanceYear":
					pValue = this.FinanceYear;
					result = true;
					break;
				case "CustomerID":
					pValue = this.CustomerID;
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
				case "IsDelete":
					pValue = this.IsDelete;
					result = true;
					break;
				case "ApproveState":
					pValue = this.ApproveState;
					result = true;
					break;
				case "Approver":
					pValue = this.Approver;
					result = true;
					break;
				case "ApproveTime":
					pValue = this.ApproveTime;
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
				case "PowerHourID":
					{
						if (pValue != null)
							this.PowerHourID = (Guid)pValue;
						else
							this.PowerHourID = null;
						result = true;
					 }
					break;
				case "SiteAddress":
					{
						if (pValue != null)
							this.SiteAddress = Convert.ToString(pValue);
						else
							this.SiteAddress = null;
						result = true;
					 }
					break;
				case "TrainerID":
					{
						if (pValue != null)
							this.TrainerID = Convert.ToString(pValue);
						else
							this.TrainerID = null;
						result = true;
					 }
					break;
				case "Topic":
					{
						if (pValue != null)
							this.Topic = Convert.ToString(pValue);
						else
							this.Topic = null;
						result = true;
					 }
					break;
				case "CityID":
					{
						if (pValue != null)
							this.CityID = Convert.ToString(pValue);
						else
							this.CityID = null;
						result = true;
					 }
					break;
				case "StartTime":
					{
						if (pValue != null)
							this.StartTime = Convert.ToDateTime(pValue);
						else
							this.StartTime = null;
						result = true;
					 }
					break;
				case "EndTime":
					{
						if (pValue != null)
							this.EndTime = Convert.ToDateTime(pValue);
						else
							this.EndTime = null;
						result = true;
					 }
					break;
				case "SitePictureUrl":
					{
						if (pValue != null)
							this.SitePictureUrl = Convert.ToString(pValue);
						else
							this.SitePictureUrl = null;
						result = true;
					 }
					break;
				case "FinanceYear":
					{
						if (pValue != null)
							this.FinanceYear =  Convert.ToInt32(pValue);
						else
							this.FinanceYear = null;
						result = true;
					 }
					break;
				case "CustomerID":
					{
						if (pValue != null)
							this.CustomerID = Convert.ToString(pValue);
						else
							this.CustomerID = null;
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
				case "IsDelete":
					{
						if (pValue != null)
							this.IsDelete =  Convert.ToInt32(pValue);
						else
							this.IsDelete = null;
						result = true;
					 }
					break;
				case "ApproveState":
					{
						if (pValue != null)
							this.ApproveState =  Convert.ToInt32(pValue);
						else
							this.ApproveState = null;
						result = true;
					 }
					break;
				case "Approver":
					{
						if (pValue != null)
							this.Approver = Convert.ToString(pValue);
						else
							this.Approver = null;
						result = true;
					 }
					break;
				case "ApproveTime":
					{
						if (pValue != null)
							this.ApproveTime = Convert.ToDateTime(pValue);
						else
							this.ApproveTime = null;
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