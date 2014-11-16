/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/6/21 17:36:55
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
    public partial class CityEntity : BaseEntity ,IExtensionable
    {
        #region ���캯��
        /// <summary>
        /// ���캯�� 
        /// </summary>
        public CityEntity()
        {
        }
        #endregion     

        #region ���Լ�
		/// <summary>
		/// ���б�ʶ
		/// </summary>
		public Guid? CityID { get; set; }

		/// <summary>
		/// ������
		/// </summary>
		public String CityName { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? LocalStaffCount { get; set; }

		/// <summary>
		/// �ͻ�ID
		/// </summary>
		public String CustomerID { get; set; }

		/// <summary>
		/// ������
		/// </summary>
		public String CreateBy { get; set; }

		/// <summary>
		/// ����ʱ��
		/// </summary>
		public DateTime? CreateTime { get; set; }

		/// <summary>
		/// ��������
		/// </summary>
		public String LastUpdateBy { get; set; }

		/// <summary>
		/// ������ʱ��
		/// </summary>
		public DateTime? LastUpdateTime { get; set; }

		/// <summary>
		/// ɾ����־
		/// </summary>
		public Int32? IsDelete { get; set; }

		/// <summary>
		/// ����
		/// </summary>
		public Decimal? Longitude { get; set; }

		/// <summary>
		/// γ��
		/// </summary>
		public Decimal? Latitude { get; set; }


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
				case "CityID":
					pValue = this.CityID;
					result = true;
					break;
				case "CityName":
					pValue = this.CityName;
					result = true;
					break;
				case "LocalStaffCount":
					pValue = this.LocalStaffCount;
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
				case "Longitude":
					pValue = this.Longitude;
					result = true;
					break;
				case "Latitude":
					pValue = this.Latitude;
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
				case "CityID":
					{
						if (pValue != null)
							this.CityID = (Guid)pValue;
						else
							this.CityID = null;
						result = true;
					 }
					break;
				case "CityName":
					{
						if (pValue != null)
							this.CityName = Convert.ToString(pValue);
						else
							this.CityName = null;
						result = true;
					 }
					break;
				case "LocalStaffCount":
					{
						if (pValue != null)
							this.LocalStaffCount =  Convert.ToInt32(pValue);
						else
							this.LocalStaffCount = null;
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
				case "Longitude":
					{
						if (pValue != null)
							this.Longitude = Convert.ToDecimal(pValue);
						else
							this.Longitude = null;
						result = true;
					 }
					break;
				case "Latitude":
					{
						if (pValue != null)
							this.Latitude = Convert.ToDecimal(pValue);
						else
							this.Latitude = null;
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