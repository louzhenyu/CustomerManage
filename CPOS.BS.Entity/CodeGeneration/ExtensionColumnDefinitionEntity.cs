/*
 * Author		:yong.liu
 * EMail		:yong.liu@jitmarketing.cn
 * Company		:JIT
 * Create On	:10/30/2012 9:34:44 AM
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
using JIT.Utility.Entity;

namespace JIT.ManagementPlatform.Entity 
{
    /// <summary>
    /// StandardClass1 
    /// </summary>
    public class ExtensionColumnDefinitionEntity : BaseEntity, IExtensionable
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public ExtensionColumnDefinitionEntity()
        {
        }
        #endregion

        #region Model       
        /// <summary>
        /// 自增主键
        /// </summary>
        public int ExtensionColumnDefinitionID { get; set; }
        /// <summary>
        /// 表Table_definition的外键
        /// </summary>
        public int? TableDefinitionID { get; set; }
        /// <summary>
        /// 扩展数据列的列名
        /// </summary>
        public string ColumnName { get; set; }
        /// <summary>
        /// 列类型
        /// </summary>
        public int? ColumnType { get; set; }
        /// <summary>
        /// 当列类型为下拉列表时，通过Dictionary_name获取数据源
        /// </summary>
        public string DictionaryName { get; set; }
        /// <summary>
        /// 客户ID
        /// </summary>
        public string ClientID { get; set; }
        /// <summary>
        /// 创建者用户ID
        /// </summary>
        public int? CreaterID { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? CreateTime { get; set; }
        /// <summary>
        /// 最后更新者用户ID
        /// </summary>
        public int? LastUpdaterID { get; set; }
        /// <summary>
        /// 最后更新时间
        /// </summary>
        public DateTime? LastUpdateTime { get; set; }
        /// <summary>
        /// 是否删除
        /// </summary>
        public bool? IsDelete { get; set; }
        #endregion Model
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
                case "ExtensionColumnDefinitionID":
                    pValue = this.ExtensionColumnDefinitionID;
                    result = true;
                    break;
                case "TableDefinitionID":
                    pValue = this.TableDefinitionID;
                    result = true;
                    break;
                case "ColumnName":
                    pValue = this.ColumnName;
                    result = true;
                    break;
                case "ColumnType":
                    pValue = this.ColumnType;
                    result = true;
                    break;
                case "DictionaryName":
                    pValue = this.DictionaryName;
                    result = true;
                    break;
                case "ClientID":
                    pValue = this.ClientID;
                    result = true;
                    break;
                case "CreaterID":
                    pValue = this.CreaterID;
                    result = true;
                    break;
                case "CreateTime":
                    pValue = this.CreateTime;
                    result = true;
                    break;
                case "LastUpdaterID":
                    pValue = this.LastUpdaterID;
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
                case "ExtensionColumnDefinitionID":
                    {
                        if (pValue != null)
                            this.ExtensionColumnDefinitionID = Convert.ToInt32(pValue);
                        else
                            this.ExtensionColumnDefinitionID = 0;
                        result = true;
                    }
                    break;
                case "TableDefinitionID":
                    {
                        if (pValue != null)
                            this.TableDefinitionID = Convert.ToInt32(pValue);
                        else
                            this.TableDefinitionID = null;
                        result = true;
                    }
                    break;
                case "ColumnName":
                    {
                        if (pValue != null)
                            this.ColumnName = pValue.ToString();
                        else
                            this.ColumnName = null;
                        result = true;
                    }
                    break;
                case "ColumnType":
                    {
                        if (pValue != null)
                            this.ColumnType = Convert.ToInt32(ColumnType);
                        else
                            this.ColumnType = null;
                        result = true;
                    }
                    break;
                case "DictionaryName":
                    {
                        if (pValue != null)
                            this.DictionaryName = pValue.ToString();
                        else
                            this.DictionaryName = null;
                        result = true;
                    }
                    break;
                case "ClientID":
                    {
                        if (pValue != null)
                            this.ClientID = pValue.ToString();
                        else
                            this.ClientID = null;
                        result = true;
                    }
                    break;
                case "CreaterID":
                    {
                        if (pValue != null)
                            this.CreaterID = Convert.ToInt32(pValue);
                        else
                            this.CreaterID = null;
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
                case "LastUpdaterID":
                    {
                        if (pValue != null)
                            this.LastUpdaterID = Convert.ToInt32(pValue);
                        else
                            this.LastUpdaterID = null;
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
                            this.IsDelete = Convert.ToBoolean(pValue);
                        else
                            this.IsDelete = null;
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
