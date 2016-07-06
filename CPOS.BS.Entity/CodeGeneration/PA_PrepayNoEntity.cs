/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2016/5/13 20:41:21
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
using JIT.Utility.Entity;

namespace CPOS.BS.Entity
{
    /// <summary>
    /// ʵ�壺  
    /// </summary>
    public partial class PA_PrepayNoEntity : BaseEntity ,IExtensionable
    {
        #region ���캯��
        /// <summary>
        /// ���캯�� 
        /// </summary>
        public PA_PrepayNoEntity()
        {
        }
        #endregion

        #region ���Լ�
        /// <summary>
        /// 
        /// </summary>
        public String OrderId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String OrderNo { get; set; }

        /// <summary>
        /// Ԥ������,����֧����
        /// </summary>
        public String PrepayNo { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? CreateTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String CustomerId { get; set; }

        /// <summary>
        /// ֧������
        /// </summary>
        public String TradeType { get; set; }

        /// <summary>
        /// �����û�ID
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
        public bool GetValueByPropertyName(string pPropertyName, out object pValue)
        {
            pValue = null;
            //��������
            if (string.IsNullOrEmpty(pPropertyName))
                throw new ArgumentException("pPropertyName����Ϊ�ջ�null.");
            //��ȡ����
            bool result = false;
            switch (pPropertyName)
            {
                case "OrderId":
                    pValue = this.OrderId;
                    result = true;
                    break;
                case "OrderNo":
                    pValue = this.OrderNo;
                    result = true;
                    break;
                case "PrepayNo":
                    pValue = this.PrepayNo;
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
                case "TradeType":
                    pValue = this.TradeType;
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
                case "PrepayNo":
                    {
                        if (pValue != null)
                            this.PrepayNo = Convert.ToString(pValue);
                        else
                            this.PrepayNo = null;
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
                case "TradeType":
                    {
                        if (pValue != null)
                            this.TradeType = Convert.ToString(pValue);
                        else
                            this.TradeType = null;
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