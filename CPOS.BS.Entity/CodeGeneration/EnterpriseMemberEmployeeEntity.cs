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
    public partial class EnterpriseMemberEmployeeEntity : BaseEntity ,IExtensionable
    {
        #region ���캯��
        /// <summary>
        /// ���캯�� 
        /// </summary>
        public EnterpriseMemberEmployeeEntity()
        {
        }
        #endregion     

        #region ���Լ�
		/// <summary>
		/// 
		/// </summary>
		public Guid? EnterpriseMemberEmployeeID { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Guid? EnterpriseMemberStructureID { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Guid? EnterpriseMemberID { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String MemberName { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String MemberNameEn { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? Gender { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String Telephone { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String Mobile { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String Email { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String ClientID { get; set; }

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
				case "EnterpriseMemberEmployeeID":
					pValue = this.EnterpriseMemberEmployeeID;
					result = true;
					break;
				case "EnterpriseMemberStructureID":
					pValue = this.EnterpriseMemberStructureID;
					result = true;
					break;
				case "EnterpriseMemberID":
					pValue = this.EnterpriseMemberID;
					result = true;
					break;
				case "MemberName":
					pValue = this.MemberName;
					result = true;
					break;
				case "MemberNameEn":
					pValue = this.MemberNameEn;
					result = true;
					break;
				case "Gender":
					pValue = this.Gender;
					result = true;
					break;
				case "Telephone":
					pValue = this.Telephone;
					result = true;
					break;
				case "Mobile":
					pValue = this.Mobile;
					result = true;
					break;
				case "Email":
					pValue = this.Email;
					result = true;
					break;
				case "ClientID":
					pValue = this.ClientID;
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
				case "EnterpriseMemberEmployeeID":
					{
						if (pValue != null)
							this.EnterpriseMemberEmployeeID = (Guid)pValue;
						else
							this.EnterpriseMemberEmployeeID = null;
						result = true;
					 }
					break;
				case "EnterpriseMemberStructureID":
					{
						if (pValue != null)
							this.EnterpriseMemberStructureID = (Guid)pValue;
						else
							this.EnterpriseMemberStructureID = null;
						result = true;
					 }
					break;
				case "EnterpriseMemberID":
					{
						if (pValue != null)
							this.EnterpriseMemberID = (Guid)pValue;
						else
							this.EnterpriseMemberID = null;
						result = true;
					 }
					break;
				case "MemberName":
					{
						if (pValue != null)
							this.MemberName = Convert.ToString(pValue);
						else
							this.MemberName = null;
						result = true;
					 }
					break;
				case "MemberNameEn":
					{
						if (pValue != null)
							this.MemberNameEn = Convert.ToString(pValue);
						else
							this.MemberNameEn = null;
						result = true;
					 }
					break;
				case "Gender":
					{
						if (pValue != null)
							this.Gender =  Convert.ToInt32(pValue);
						else
							this.Gender = null;
						result = true;
					 }
					break;
				case "Telephone":
					{
						if (pValue != null)
							this.Telephone = Convert.ToString(pValue);
						else
							this.Telephone = null;
						result = true;
					 }
					break;
				case "Mobile":
					{
						if (pValue != null)
							this.Mobile = Convert.ToString(pValue);
						else
							this.Mobile = null;
						result = true;
					 }
					break;
				case "Email":
					{
						if (pValue != null)
							this.Email = Convert.ToString(pValue);
						else
							this.Email = null;
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