/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2016/5/24 14:51:06
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
    public partial class T_Order_Reason_TypeEntity : BaseEntity ,IExtensionable
    {
        #region ���캯��
        /// <summary>
        /// ���캯�� 
        /// </summary>
        public T_Order_Reason_TypeEntity()
        {
        }
        #endregion     

        #region ���Լ�
		/// <summary>
		/// 
		/// </summary>
		public String reason_type_id { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String reason_type_code { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String reason_type_name { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String remark { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String status { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? level { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String parent_id { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String create_time { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String create_user_id { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String modify_time { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String modify_user_id { get; set; }


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
				case "reason_type_id":
					pValue = this.reason_type_id;
					result = true;
					break;
				case "reason_type_code":
					pValue = this.reason_type_code;
					result = true;
					break;
				case "reason_type_name":
					pValue = this.reason_type_name;
					result = true;
					break;
				case "remark":
					pValue = this.remark;
					result = true;
					break;
				case "status":
					pValue = this.status;
					result = true;
					break;
				case "level":
					pValue = this.level;
					result = true;
					break;
				case "parent_id":
					pValue = this.parent_id;
					result = true;
					break;
				case "create_time":
					pValue = this.create_time;
					result = true;
					break;
				case "create_user_id":
					pValue = this.create_user_id;
					result = true;
					break;
				case "modify_time":
					pValue = this.modify_time;
					result = true;
					break;
				case "modify_user_id":
					pValue = this.modify_user_id;
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
				case "reason_type_id":
					{
						if (pValue != null)
							this.reason_type_id = Convert.ToString(pValue);
						else
							this.reason_type_id = null;
						result = true;
					 }
					break;
				case "reason_type_code":
					{
						if (pValue != null)
							this.reason_type_code = Convert.ToString(pValue);
						else
							this.reason_type_code = null;
						result = true;
					 }
					break;
				case "reason_type_name":
					{
						if (pValue != null)
							this.reason_type_name = Convert.ToString(pValue);
						else
							this.reason_type_name = null;
						result = true;
					 }
					break;
				case "remark":
					{
						if (pValue != null)
							this.remark = Convert.ToString(pValue);
						else
							this.remark = null;
						result = true;
					 }
					break;
				case "status":
					{
						if (pValue != null)
							this.status = Convert.ToString(pValue);
						else
							this.status = null;
						result = true;
					 }
					break;
				case "level":
					{
						if (pValue != null)
							this.level =  Convert.ToInt32(pValue);
						else
							this.level = null;
						result = true;
					 }
					break;
				case "parent_id":
					{
						if (pValue != null)
							this.parent_id = Convert.ToString(pValue);
						else
							this.parent_id = null;
						result = true;
					 }
					break;
				case "create_time":
					{
						if (pValue != null)
							this.create_time = Convert.ToString(pValue);
						else
							this.create_time = null;
						result = true;
					 }
					break;
				case "create_user_id":
					{
						if (pValue != null)
							this.create_user_id = Convert.ToString(pValue);
						else
							this.create_user_id = null;
						result = true;
					 }
					break;
				case "modify_time":
					{
						if (pValue != null)
							this.modify_time = Convert.ToString(pValue);
						else
							this.modify_time = null;
						result = true;
					 }
					break;
				case "modify_user_id":
					{
						if (pValue != null)
							this.modify_user_id = Convert.ToString(pValue);
						else
							this.modify_user_id = null;
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