using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.BS.Entity
{
    /// <summary>
    /// 属性（商品,或者组织或者其他信息的辅助信息）
    /// </summary>
    [Serializable]
    public class PropInfo
    {
        /// <summary>
        /// 属性标识
        /// </summary>
        public string Prop_Id { get; set; }
        /// <summary>
        /// 属性号码
        /// </summary>
        public string Prop_Code { get; set; }
        /// <summary>
        /// 属性名称
        /// </summary>
        public string Prop_Name { get; set; }
        /// <summary>
        /// 属性英文名称
        /// </summary>
        public string Prop_Eng_Name { get; set; }
        /// <summary>
        /// 属性类型 1=组,2=属性,3=属性明细';
        /// </summary>
        public string Prop_Type { get; set; }
        /// <summary>
        /// 父节点,第一层的父节点默认为-99
        /// </summary>
        public string Parent_Prop_id { get; set; }
        /// <summary>
        /// 属性层次 譬如：1,2,3,。。。。
        /// </summary>
        public int Prop_Level { get; set; } 
        /// <summary>
        /// 属性域,譬如：UNIT,ITEM,ITEMPRICE
        /// </summary>
        public string Prop_Domain { get; set; }
        /// <summary>
        /// text,select,radio,checkbox......
        /// </summary>
        public string Prop_Input_Flag { get; set; }
        /// <summary>
        /// 输入信息长度
        /// </summary>
        public int Prop_Max_Length { get; set; }
        /// <summary>
        /// 默认值
        /// </summary>
        public string Prop_Default_Value { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public int Prop_Status { get; set; }
        /// <summary>
        /// 序号
        /// </summary>
        public int Display_Index { get; set; }
        /// <summary>
        /// 创建人标识
        /// </summary>
        public string Create_User_Id { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public string Create_Time { get; set; }
        /// <summary>
        /// 修改人标识
        /// </summary>
        public string Modify_User_Id { get; set; }
        /// <summary>
        /// 修改时间
        /// </summary>
        public string Modify_Time { get; set; }
        /// <summary>
        /// 状态描述
        /// </summary>
        public string Prop_Status_Desc { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        public string Create_User_Name { get; set; }
        /// <summary>
        /// 修改人
        /// </summary>
        public string Modify_User_Name { get; set; }

        public string Parent_Prop_Name { get; set; }
        public string CreateByName { get; set; }
        public string OrderBy { get; set; }
        public string Prop_Type_Name { get; set; }
        /// <summary>
        /// 客户标识
        /// </summary>
        public string CustomerId { get; set; }
    }
}
