/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/7/18 15:41:40
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
    public partial class AttachmentEntity : BaseEntity ,IExtensionable
    {
        #region ���캯��
        /// <summary>
        /// ���캯�� 
        /// </summary>
        public AttachmentEntity()
        {
        }
        #endregion     

        #region ���Լ�
		/// <summary>
		/// 
		/// </summary>
		public Guid? AttachmentID { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String AttachmentTitle { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String FileName { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String Path { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Decimal? AttachmentSize { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String ClientID { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public DateTime? CreateTime { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String CreateBy { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public DateTime? LastUpdateTime { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String LastUpdateBy { get; set; }

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
				case "AttachmentID":
					pValue = this.AttachmentID;
					result = true;
					break;
				case "AttachmentTitle":
					pValue = this.AttachmentTitle;
					result = true;
					break;
				case "FileName":
					pValue = this.FileName;
					result = true;
					break;
				case "Path":
					pValue = this.Path;
					result = true;
					break;
				case "AttachmentSize":
					pValue = this.AttachmentSize;
					result = true;
					break;
				case "ClientID":
					pValue = this.ClientID;
					result = true;
					break;
				case "CreateTime":
					pValue = this.CreateTime;
					result = true;
					break;
				case "CreateBy":
					pValue = this.CreateBy;
					result = true;
					break;
				case "LastUpdateTime":
					pValue = this.LastUpdateTime;
					result = true;
					break;
				case "LastUpdateBy":
					pValue = this.LastUpdateBy;
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
				case "AttachmentID":
					{
						if (pValue != null)
							this.AttachmentID = (Guid)pValue;
						else
							this.AttachmentID = null;
						result = true;
					 }
					break;
				case "AttachmentTitle":
					{
						if (pValue != null)
							this.AttachmentTitle = Convert.ToString(pValue);
						else
							this.AttachmentTitle = null;
						result = true;
					 }
					break;
				case "FileName":
					{
						if (pValue != null)
							this.FileName = Convert.ToString(pValue);
						else
							this.FileName = null;
						result = true;
					 }
					break;
				case "Path":
					{
						if (pValue != null)
							this.Path = Convert.ToString(pValue);
						else
							this.Path = null;
						result = true;
					 }
					break;
				case "AttachmentSize":
					{
						if (pValue != null)
							this.AttachmentSize = Convert.ToDecimal(pValue);
						else
							this.AttachmentSize = null;
						result = true;
					 }
					break;
				case "ClientID":
					{
						if (pValue != null)
							this.ClientID = Convert.ToString(pValue);
						else
							this.ClientID = null;
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
				case "CreateBy":
					{
						if (pValue != null)
							this.CreateBy = Convert.ToString(pValue);
						else
							this.CreateBy = null;
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
				case "LastUpdateBy":
					{
						if (pValue != null)
							this.LastUpdateBy = Convert.ToString(pValue);
						else
							this.LastUpdateBy = null;
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