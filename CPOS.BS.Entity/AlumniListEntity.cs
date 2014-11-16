using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.BS.Entity
{
    public class AlumniListEntity
    {
        /// <summary>
        /// 控件集合,查询条件的集合
        /// </summary>
        public Control[] Control { get; set; }

        /// <summary>
        /// 问卷ID，如果为空，则使用公用的问卷ID
        /// </summary>
        public string MobileModuleID { get; set; }

        /// <summary>
        /// 是否为本班级，适用于，我的通讯录里面的班级通讯录
        /// </summary>
        public int? IsSameClass { get; set; }

        /// <summary>
        /// 页码，从0开始
        /// </summary>
        public string Page { get; set; }

        /// <summary>
        /// 显示数
        /// </summary>
        public string PageSize { get; set; }
    }

    /// <summary>
    /// 控件集合,查询条件的集合
    /// </summary>
    public class Control
    {
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
        public string Value { get; set; }

        /// <summary>
        /// 1:文本；2:数字；3:小数型；4：日期；5：时间类型；6：下拉框；7 : 单选框 8: 多选框；
        /// 9:超文本,10:密码框,27.省,28.市,29.县,102.课程,103.班级，104.籍贯（市），
        /// 105.常驻，106.常往来，107.行业
        /// </summary>
        public int? ControlType { get; set; }
    }
}
