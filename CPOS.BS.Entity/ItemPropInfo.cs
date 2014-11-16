using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.BS.Entity
{
    /// <summary>
    /// 商品与属性关系
    /// </summary>
    public class ItemPropInfo
    {
        /// <summary>
        /// 商品标识【保存必须】
        /// </summary>
        public string Item_Id { get; set; }
        /// <summary>
        /// 商品属性标识【保存必须】
        /// </summary>
        public string Item_Property_Id { get; set; }

        /// <summary>
        /// 属性的组标识【保存必须】
        /// </summary>
        public string PropertyCodeGroupId { get; set; }

        /// <summary>
        /// 属性标识
        /// </summary>
        public string PropertyCodeId { get; set; }

        /// <summary>
        /// 属性的详细标识
        /// </summary>
        public string PropertyDetailId { get; set; }

        /// <summary>
        /// 属性的值【保存必须】
        /// </summary>
        public string PropertyCodeValue { get; set; }

        private string status = "1";
        /// <summary>
        /// 状态(1:有效)
        /// </summary>
        public string Status
        {
            get { return status; }
            set { status = value; }
        }
        /// <summary>
        /// 属性组名称
        /// </summary>
        public string PropertyCodeGroupName { get; set; }
        /// <summary>
        /// 属性名称
        /// </summary>
        public string PropertyCodeName { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        public string Create_User_id { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public string Create_Time { get; set; }

        /// <summary>
        /// 是否富文本编辑器
        /// </summary>
        public bool IsEditor { get; set; }
    }
}
