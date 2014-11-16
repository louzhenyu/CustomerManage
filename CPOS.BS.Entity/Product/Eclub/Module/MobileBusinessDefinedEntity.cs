using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.BS.Entity.Product.Eclub.Module
{
    /// <summary>
    /// 页的实体类
    /// </summary>
    public class PageEntity
    {
        public string PageName { get; set; }  //页的标题
        public int PageNum { get; set; }  //页码 1,2,3
        public List<BlockEntity> Block { get; set; } //页所包含的块信息
    }

    /// <summary>
    /// 块的实体类
    /// </summary>
    public class BlockEntity
    {
        public string BlockName { get; set; }  //块的标题
        public int BlockSort { get; set; }  //块的排序
        public string Remark { get; set; }//备注
        public List<ControlEntity> Control { get; set; } //块所包含的控件信息
    }
    /// <summary>
    /// 控件的实体类
    /// </summary>
    public class ControlEntity
    {
        public string ControlID { get; set; }  //控件ID
        public bool NeedSaveCookie { get; set; } //是否需要保存Cookie
        public string CookieName { get; set; }  //保存Cookie的名称
        public string ColumnDesc { get; set; } //列的名称
        public string ColumnDescEN { get; set; } //列的英文名称
        public string ColumnName { get; set; } //字段的名称
        public string LinkageItem { get; set; } //联动项
        public string ExampleValue { get; set; }  //例如项
        public string ControlType { get; set; } //控件类型
        public string AuthType { get; set; } //验证的类型
        public int MinLength { get; set; } //最小长度
        public int MaxLength { get; set; } //最大长度
        public int MinSelected { get; set; } //多选最少选择
        public int MaxSelected { get; set; } //多选最多选择
        public bool IsMustDo { get; set; }  //是否必填
        public string Value { get; set; }   //值

        public string CorrelationValue { get; set; }
        public string IsPrivacy { get; set; }

        public string OperationStatus { get; set; }

        public List<ConOptionsEntity> Options { get; set; } //单选，多选，下拉的选项
    }
    public class ConOptionsEntity
    {
        public string OptionID { get; set; } //option的值
        public string OptionText { get; set; } //option显示的值
        public bool IsSelected { get; set; } //是否选中
    }




    public class ResponseEntity<T>
    {
        public string code { get; set; }
        public string description { get; set; }
        public T content { get; set; }
    }
    public class RequestGetUserByIDData
    {
        public string UserId;
        public string OpenId;
        public string CustomerId;

        public string Code;//mobilemodule=>modulecode
    }
    public class ResponsePageListEntity
    {
        public List<PageEntity> PageList; //页集合
    }


    public class RequestSubmitUserByIDData
    {
        public string UserId;
        public string OpenId;
        public string CustomerId;

        public List<ControlUpdateEntity> Control { get; set; } //上传的内容
    }
    public class ControlUpdateEntity
    {
        public string ControlID { get; set; }  //控件ID
        public string ColumnDesc { get; set; } //列的描述
        public string ColumnName { get; set; } //字段名称
        public string Value { get; set; }  //数据的值
        public int IsPrivacy { get; set; }
        public string OperationStatus { get; set; }
    }
    public class ResponseSubmitUserByIDData
    {

    }
}
