/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/7/18 15:41:41
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
    /// ʵ�壺 �ݷüƻ� 
    /// </summary>
    public partial class VisitingCalendarEntity : BaseEntity ,IExtensionable
    {
        #region ���캯��
        /// <summary>
        /// ���캯�� 
        /// </summary>
        public VisitingCalendarEntity()
        {
        }
        #endregion     

        #region ���Լ�
		/// <summary>
		/// �ݷ��ճ�ID
		/// </summary>
		public Guid? VisitingCalendarID { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Guid? EnterpriseMemberID { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Guid? EnterpriseMemberEmployeeID { get; set; }

		/// <summary>
		/// ��ҵ��Ա��֯�ܹ�ID
		/// </summary>
		public Guid? EnterpriseMemberStructureID { get; set; }

		/// <summary>
		/// �ƻ�ʱ��
		/// </summary>
		public DateTime? PlanningTime { get; set; }

		/// <summary>
		/// ʵ�ʰݷ�ʱ��
		/// </summary>
		public DateTime? ActuallyDate { get; set; }

		/// <summary>
		/// �Զ����
		/// </summary>
		public Guid? VisitingTaskDataID { get; set; }

		/// <summary>
		/// ��Ա���(����ClientUser��)
		/// </summary>
		public String ClientUserID { get; set; }

		/// <summary>
		/// �ݷ�˳��
		/// </summary>
		public Int32? Sequence { get; set; }

		/// <summary>
		/// �ݷ�״̬(0-δ�ݷ�,1-�Ѿ��ݷ�)
		/// </summary>
		public Int32? Status { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String Remark { get; set; }

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
				case "VisitingCalendarID":
					pValue = this.VisitingCalendarID;
					result = true;
					break;
				case "EnterpriseMemberID":
					pValue = this.EnterpriseMemberID;
					result = true;
					break;
				case "EnterpriseMemberEmployeeID":
					pValue = this.EnterpriseMemberEmployeeID;
					result = true;
					break;
				case "EnterpriseMemberStructureID":
					pValue = this.EnterpriseMemberStructureID;
					result = true;
					break;
				case "PlanningTime":
					pValue = this.PlanningTime;
					result = true;
					break;
				case "ActuallyDate":
					pValue = this.ActuallyDate;
					result = true;
					break;
				case "VisitingTaskDataID":
					pValue = this.VisitingTaskDataID;
					result = true;
					break;
				case "ClientUserID":
					pValue = this.ClientUserID;
					result = true;
					break;
				case "Sequence":
					pValue = this.Sequence;
					result = true;
					break;
				case "Status":
					pValue = this.Status;
					result = true;
					break;
				case "Remark":
					pValue = this.Remark;
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
				case "VisitingCalendarID":
					{
						if (pValue != null)
							this.VisitingCalendarID = (Guid)pValue;
						else
							this.VisitingCalendarID = null;
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
				case "PlanningTime":
					{
						if (pValue != null)
							this.PlanningTime = Convert.ToDateTime(pValue);
						else
							this.PlanningTime = null;
						result = true;
					 }
					break;
				case "ActuallyDate":
					{
						if (pValue != null)
							this.ActuallyDate = Convert.ToDateTime(pValue);
						else
							this.ActuallyDate = null;
						result = true;
					 }
					break;
				case "VisitingTaskDataID":
					{
						if (pValue != null)
							this.VisitingTaskDataID = (Guid)pValue;
						else
							this.VisitingTaskDataID = null;
						result = true;
					 }
					break;
				case "ClientUserID":
					{
						if (pValue != null)
							this.ClientUserID = Convert.ToString(pValue);
						else
							this.ClientUserID = null;
						result = true;
					 }
					break;
				case "Sequence":
					{
						if (pValue != null)
							this.Sequence =  Convert.ToInt32(pValue);
						else
							this.Sequence = null;
						result = true;
					 }
					break;
				case "Status":
					{
						if (pValue != null)
							this.Status =  Convert.ToInt32(pValue);
						else
							this.Status = null;
						result = true;
					 }
					break;
				case "Remark":
					{
						if (pValue != null)
							this.Remark = Convert.ToString(pValue);
						else
							this.Remark = null;
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