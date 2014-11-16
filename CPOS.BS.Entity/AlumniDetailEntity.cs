using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.BS.Entity
{
    public class AlumniDetailEntity
    {
        /// <summary>
        /// 页信息集
        /// </summary>
        public Page[] Pages { get; set; }
        /// <summary>
        /// 是否收藏
        /// </summary>
        public string IsBookMark { get; set; }

        /// <summary>
        /// 数据响应参数实体
        /// </summary>
        public class Page
        {
            /// <summary>
            /// 页的绵长
            /// </summary>
            public string PageName { get; set; }
            /// <summary>
            /// 块的集合
            /// </summary>
            public Block[] Blocks { get; set; }
        }
        /// <summary>
        /// 块的集合
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
        /// 控件集合
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
            /// 1:文本；2:数字；3:小数型；4：日期；5：时间类型；
            /// 6：下拉框；7 : 单选框 8: 多选框；9:超文本,10:密码框,
            /// 27.省,28.市,29.县,102.课程,103.班级，104.籍贯（市），
            /// 105.常驻，106.常往来，107.行业
            /// </summary>
            public int? ControlType { get; set; }
            /// <summary>
            /// 查询的值
            /// </summary>
            public Value[] Values { get; set; }
        }
        /// <summary>
        /// 值集合
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
