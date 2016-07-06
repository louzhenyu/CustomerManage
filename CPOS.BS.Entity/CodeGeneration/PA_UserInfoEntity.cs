/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2016/5/17 11:49:55
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

namespace CPOS.BS.Entity
{
    /// <summary>
    /// ʵ�壺  
    /// </summary>
    public partial class PA_UserInfoEntity : BaseEntity ,IExtensionable
    {
        #region ���캯��
        /// <summary>
        /// ���캯�� 
        /// </summary>
        public PA_UserInfoEntity()
        {
        }
        #endregion     

        #region ���Լ�
		/// <summary>
		/// �û�ID
		/// </summary>
		public String OpenId { get; set; }

		/// <summary>
		/// �����˱��
		/// </summary>
		public String AgentNo { get; set; }

		/// <summary>
		/// ����������
		/// </summary>
		public String AgentType { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public DateTime? CreateTime { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String CustomerId { get; set; }

		/// <summary>
		/// �ǳ�
		/// </summary>
		public String EmpType { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String alias { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String Field1 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String Field2 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String Field3 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String Field4 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String Field5 { get; set; }


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
				case "OpenId":
					pValue = this.OpenId;
					result = true;
					break;
				case "AgentNo":
					pValue = this.AgentNo;
					result = true;
					break;
				case "AgentType":
					pValue = this.AgentType;
					result = true;
					break;
				case "CreateTime":
					pValue = this.CreateTime;
					result = true;
					break;
				case "CustomerId":
					pValue = this.CustomerId;
					result = true;
					break;
				case "EmpType":
					pValue = this.EmpType;
					result = true;
					break;
				case "alias":
					pValue = this.alias;
					result = true;
					break;
				case "Field1":
					pValue = this.Field1;
					result = true;
					break;
				case "Field2":
					pValue = this.Field2;
					result = true;
					break;
				case "Field3":
					pValue = this.Field3;
					result = true;
					break;
				case "Field4":
					pValue = this.Field4;
					result = true;
					break;
				case "Field5":
					pValue = this.Field5;
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
				case "OpenId":
					{
						if (pValue != null)
							this.OpenId = Convert.ToString(pValue);
						else
							this.OpenId = null;
						result = true;
					 }
					break;
				case "AgentNo":
					{
						if (pValue != null)
							this.AgentNo = Convert.ToString(pValue);
						else
							this.AgentNo = null;
						result = true;
					 }
					break;
				case "AgentType":
					{
						if (pValue != null)
							this.AgentType = Convert.ToString(pValue);
						else
							this.AgentType = null;
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
				case "CustomerId":
					{
						if (pValue != null)
							this.CustomerId = Convert.ToString(pValue);
						else
							this.CustomerId = null;
						result = true;
					 }
					break;
				case "EmpType":
					{
						if (pValue != null)
							this.EmpType = Convert.ToString(pValue);
						else
							this.EmpType = null;
						result = true;
					 }
					break;
				case "alias":
					{
						if (pValue != null)
							this.alias = Convert.ToString(pValue);
						else
							this.alias = null;
						result = true;
					 }
					break;
				case "Field1":
					{
						if (pValue != null)
							this.Field1 = Convert.ToString(pValue);
						else
							this.Field1 = null;
						result = true;
					 }
					break;
				case "Field2":
					{
						if (pValue != null)
							this.Field2 = Convert.ToString(pValue);
						else
							this.Field2 = null;
						result = true;
					 }
					break;
				case "Field3":
					{
						if (pValue != null)
							this.Field3 = Convert.ToString(pValue);
						else
							this.Field3 = null;
						result = true;
					 }
					break;
				case "Field4":
					{
						if (pValue != null)
							this.Field4 = Convert.ToString(pValue);
						else
							this.Field4 = null;
						result = true;
					 }
					break;
				case "Field5":
					{
						if (pValue != null)
							this.Field5 = Convert.ToString(pValue);
						else
							this.Field5 = null;
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