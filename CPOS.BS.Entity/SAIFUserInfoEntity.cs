using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.BS.Entity
{
    public class SAIFUserInfoEntity
    {
        /// <summary>
        /// 数据
        /// </summary>
        public Page[] Pages { get; set; }

        /// <summary>
        /// 页
        /// </summary>
        public class Page
        {
            /// <summary>
            /// 页的名称
            /// </summary>
            public string PageName { get; set; }
            /// <summary>
            /// 块的集合
            /// </summary>
            public Block[] Blocks { get; set; }
        }

        /// <summary>
        /// 块
        /// </summary>
        public class Block
        {
            /// <summary>
            /// 块的名称
            /// </summary>
            public string BlockName { get; set; }
            /// <summary>
            /// 用户的集合
            /// </summary>
            public Control[] Controls { get; set; }
        }

        /// <summary>
        /// 用户信息
        /// </summary>
        public class Control
        {
            /// <summary>
            /// 控件ID
            /// </summary>
            public string ControlID { get; set; }
            /// <summary>
            /// 字段描述
            /// </summary>
            public string ColumnDesc { get; set; }
            /// <summary>
            /// 字段名称
            /// </summary>
            public string ColumnName { get; set; }
            /// <summary>
            /// 查询的值
            /// </summary>
            public Value[] Values { get; set; }
            /// <summary>
            /// 1:文本；4：日期；5：时间类型；6：下拉框；7 : 单选框 8: 多选框；9:超文本,10:密码框,27.省,28.市,29.县,101.标签,102.课程,103.班级
            /// </summary>
            public int? ControlType { get; set; }
            /// <summary>
            /// 1：文本; 2: 整数；3：小数；4:日期；5：时间；6：邮件 ；7：电话；8：手机；9验证Url网址；10：验证身份证
            /// </summary>
            public int? AuthType { get; set; }
            /// <summary>
            /// 最小长度
            /// </summary>
            public int? MinLength { get; set; }
            /// <summary>
            /// 最大长度
            /// </summary>
            public int? MaxLength { get; set; }
            /// <summary>
            /// 对于多选题，最少选中项
            /// </summary>
            public int? MinSelected { get; set; }
            /// <summary>
            /// 对于多选题，最多选中项
            /// </summary>
            public int? MaxSelected { get; set; }
            /// <summary>
            /// 是否必填
            /// </summary>
            public int? IsMustDo { get; set; }
            /// <summary>
            /// 是否可编辑
            /// </summary>
            public int? IsEdit { get; set; }
            /// <summary>
            /// 是否有隐私权限
            /// </summary>
            public int? IsPrivacy { get; set; }
            /// <summary>
            /// 隐私值
            /// </summary>
            public string PrivacyValue { get; set; }
            /// <summary>
            /// 下拉选项，以及标签选项
            /// </summary>
            public Option[] Options { get; set; }
        }

        /// <summary>
        /// 下拉选项，以及标签选项
        /// </summary>
        public class Option
        {
            /// <summary>
            /// ID
            /// </summary>
            public string OptionID { get; set; }
            /// <summary>
            /// 显示值
            /// </summary>
            public string OptionText { get; set; }
            /// <summary>
            /// 是否选中
            /// </summary>
            public int? IsSelected { get; set; }
        }

        /// <summary>
        /// 查询的值
        /// </summary>
        public class Value
        {
            /// <summary>
            /// 保存的数据
            /// </summary>
            public string ID { get; set; }
            /// <summary>
            /// 显示的值
            /// </summary>
            public string Text { get; set; }
        }
    }
}