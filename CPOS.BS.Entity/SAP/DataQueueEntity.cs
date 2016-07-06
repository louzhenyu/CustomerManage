using JIT.Utility.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.BS.Entity.SAP
{
    public class DataQueueEntity : BaseEntity, IExtensionable
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public DataQueueEntity()
        {
        }
        #endregion

        #region 属性集
        /// <summary>
        /// 
        /// </summary>
        public Int32 Id { get; set; }

        /// <summary>
        /// 1完成    0未完成
        /// </summary>
        public Int32 IsComplete { get; set; }

        /// <summary>
        /// 表示该消息是什么消息   自己定义
        /// </summary>
        public String ObjectType { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String CustomerId { get; set; }

        /// <summary>
        /// 新增(A)，修改(U)，删除(D)，取消(C)
        /// </summary>
        public String TransType { get; set; }

        /// <summary>
        /// 如果是取消或者删除的时候可以把唯一标识字段名写在这里，然后唯一值写在下面的一个字段，这样就不用读取value里面详细的数据了
        /// </summary>
        public String FieldNames { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String FieldValues { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Int32 FieldsInKey { get; set; }

        /// <summary>
        /// 被消费的次数
        /// </summary>
        public Int32 ConsumptionCount { get; set; }

        /// <summary>
        /// 上次消费时间
        /// </summary>
        public String PrevTime { get; set; }

        /// <summary>
        /// 最后消费时间
        /// </summary>
        public String LastTime { get; set; }

        /// <summary>
        /// 错误消息
        /// </summary>
        public String ErrorMsg { get; set; }

        /// <summary>
        /// 下次待消费时间
        /// </summary>
        public DateTime NextTime { get; set; }

        /// <summary>
        /// 消费内容
        /// </summary>
        public String Value { get; set; }

        /// <summary>
        /// 消息创建的时间
        /// </summary>
        public String Flied1 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String Flied2 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String Flied3 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String Flied4 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String Flied5 { get; set; }


        #endregion

        #region 根据属性名[获取/设置]属性值,子类应该采用组合模式重写该方法
        /// <summary>
        /// 根据属性名获取属性值,子类应该采用组合模式重写该方法
        /// </summary>
        /// <param name="pPropertyName">属性名</param>
        /// <param name="pValue">值</param>
        /// <returns>是否获取成功</returns>
        public bool GetValueByPropertyName(string pPropertyName, out object pValue)
        {
            pValue = null;
            //参数处理
            if (string.IsNullOrEmpty(pPropertyName))
                throw new ArgumentException("pPropertyName不能为空或null.");
            //获取数据
            bool result = false;
            switch (pPropertyName)
            {
                case "Id":
                    pValue = this.Id;
                    result = true;
                    break;
                case "IsComplete":
                    pValue = this.IsComplete;
                    result = true;
                    break;
                case "ObjectType":
                    pValue = this.ObjectType;
                    result = true;
                    break;
                case "CustomerId":
                    pValue = this.CustomerId;
                    result = true;
                    break;
                case "TransType":
                    pValue = this.TransType;
                    result = true;
                    break;
                case "FieldNames":
                    pValue = this.FieldNames;
                    result = true;
                    break;
                case "FieldValues":
                    pValue = this.FieldValues;
                    result = true;
                    break;
                case "FieldsInKey":
                    pValue = this.FieldsInKey;
                    result = true;
                    break;
                case "ConsumptionCount":
                    pValue = this.ConsumptionCount;
                    result = true;
                    break;
                case "PrevTime":
                    pValue = this.PrevTime;
                    result = true;
                    break;
                case "LastTime":
                    pValue = this.LastTime;
                    result = true;
                    break;
                case "ErrorMsg":
                    pValue = this.ErrorMsg;
                    result = true;
                    break;
                case "NextTime":
                    pValue = this.NextTime;
                    result = true;
                    break;
                case "Value":
                    pValue = this.Value;
                    result = true;
                    break;
                case "Flied1":
                    pValue = this.Flied1;
                    result = true;
                    break;
                case "Flied2":
                    pValue = this.Flied2;
                    result = true;
                    break;
                case "Flied3":
                    pValue = this.Flied3;
                    result = true;
                    break;
                case "Flied4":
                    pValue = this.Flied4;
                    result = true;
                    break;
                case "Flied5":
                    pValue = this.Flied5;
                    result = true;
                    break;

            }
            //返回
            return result;
        }
        /// <summary>
        /// 根据属性名设置值，子类应该采用组合模式重写该方法
        /// </summary>
        /// <param name="pPropertyName">属性名</param>
        /// <param name="pValue">值</param>
        /// <returns>是否设置成功</returns>
        public bool SetValueByPropertyName(string pPropertyName, object pValue)
        {
            //参数处理
            if (string.IsNullOrEmpty(pPropertyName))
                throw new ArgumentException("pPropertyName不能为空或null.");
            //设置数据
            bool result = false;
            switch (pPropertyName)
            {
                case "Id":
                    {
                        if (pValue != null)
                            this.Id = Convert.ToInt32(pValue);
                        result = true;
                    }
                    break;
                case "IsComplete":
                    {
                        if (pValue != null)
                            this.IsComplete = Convert.ToInt32(pValue);
                        result = true;
                    }
                    break;
                case "ObjectType":
                    {
                        if (pValue != null)
                            this.ObjectType = Convert.ToString(pValue);
                        else
                            this.ObjectType = null;
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
                case "TransType":
                    {
                        if (pValue != null)
                            this.TransType = Convert.ToString(pValue);
                        else
                            this.TransType = null;
                        result = true;
                    }
                    break;
                case "FieldNames":
                    {
                        if (pValue != null)
                            this.FieldNames = Convert.ToString(pValue);
                        else
                            this.FieldNames = null;
                        result = true;
                    }
                    break;
                case "FieldValues":
                    {
                        if (pValue != null)
                            this.FieldValues = Convert.ToString(pValue);
                        else
                            this.FieldValues = null;
                        result = true;
                    }
                    break;
                case "FieldsInKey":
                    {
                        if (pValue != null)
                            this.FieldsInKey = Convert.ToInt32(pValue);
                        result = true;
                    }
                    break;
                case "ConsumptionCount":
                    {
                        if (pValue != null)
                            this.ConsumptionCount = Convert.ToInt32(pValue);
                        result = true;
                    }
                    break;
                case "PrevTime":
                    {
                        if (pValue != null)
                            this.PrevTime = Convert.ToString(pValue);
                        else
                            this.PrevTime = null;
                        result = true;
                    }
                    break;
                case "LastTime":
                    {
                        if (pValue != null)
                            this.LastTime = Convert.ToString(pValue);
                        else
                            this.LastTime = null;
                        result = true;
                    }
                    break;
                case "ErrorMsg":
                    {
                        if (pValue != null)
                            this.ErrorMsg = Convert.ToString(pValue);
                        else
                            this.ErrorMsg = null;
                        result = true;
                    }
                    break;
                case "NextTime":
                    {
                        if (pValue != null)
                            this.NextTime = Convert.ToDateTime(pValue);
                        result = true;
                    }
                    break;
                case "Value":
                    {
                        if (pValue != null)
                            this.Value = Convert.ToString(pValue);
                        else
                            this.Value = null;
                        result = true;
                    }
                    break;
                case "Flied1":
                    {
                        if (pValue != null)
                            this.Flied1 = Convert.ToString(pValue);
                        else
                            this.Flied1 = null;
                        result = true;
                    }
                    break;
                case "Flied2":
                    {
                        if (pValue != null)
                            this.Flied2 = Convert.ToString(pValue);
                        else
                            this.Flied2 = null;
                        result = true;
                    }
                    break;
                case "Flied3":
                    {
                        if (pValue != null)
                            this.Flied3 = Convert.ToString(pValue);
                        else
                            this.Flied3 = null;
                        result = true;
                    }
                    break;
                case "Flied4":
                    {
                        if (pValue != null)
                            this.Flied4 = Convert.ToString(pValue);
                        else
                            this.Flied4 = null;
                        result = true;
                    }
                    break;
                case "Flied5":
                    {
                        if (pValue != null)
                            this.Flied5 = Convert.ToString(pValue);
                        else
                            this.Flied5 = null;
                        result = true;
                    }
                    break;

            }
            //重置持久化状态
            if (result == true && this.PersistenceHandle != null)
                this.PersistenceHandle.Modify();
            //返回
            return result;
        }

        #endregion
    }
}

