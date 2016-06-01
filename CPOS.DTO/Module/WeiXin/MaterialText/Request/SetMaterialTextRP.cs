using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.CPOS.DTO.Base;

namespace JIT.CPOS.DTO.Module.WeiXin.MaterialText.Request
{
    /// <summary>
    /// 设置图文素材接口(新增,修改)
    /// </summary>
    public class SetMaterialTextRP : IAPIRequestParameter
    {
        public MaterialTextInfo MaterialText { get; set; }

        public void Validate()
        {
            if (this.MaterialText == null)
                throw new Exception("参数MaterialText不能为NULL");
            if (this.MaterialText.ApplicationId == null)
                throw new Exception("微信帐号【ApplicationId】不能为NULL");
            if (this.MaterialText.Title == null)
                throw new Exception("标题【Title】不能为NULL");
            //if (this.MaterialText.TypeId == null)
            //    throw new Exception("分类【TypeId】不能为NULL");
            if (this.MaterialText.ImageUrl == null)
                throw new Exception("封面图片【ImageUrl】不能为NULL");
        }


    }

    public class MaterialTextInfo
    {
        /// <summary>
        ///标识	为空时是增加,否则为修改
        /// </summary>  
        public string TextId { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 图片URL	
        /// </summary>
        public string ImageUrl { get; set; }
        /// <summary>
        /// 原文连接
        /// </summary>
        public string OriginalUrl { get; set; }
        /// <summary>
        /// 文本内容	
        /// </summary>
        public string Text { get; set; }
        /// <summary>
        /// 作为请求是不作为参数
        /// </summary>
        public int DisplayIndex { get; set; }
        /// <summary>
        /// 申请接口主标识
        /// </summary>
        public string ApplicationId { get; set; }
        /// <summary>
        /// 图文类别	
        /// </summary>
        public string TypeId { get; set; }
        /// <summary>
        /// 链接类别ID
        /// </summary>
        public string UnionTypeId { get; set; }
        /// <summary>
        /// 模块ID
        /// </summary>
        public Guid? PageID { get; set; }
        /// <summary>
        /// 模块名称
        /// </summary>
        public string ModuleName { get; set; }
        /// <summary>
        /// 页面替换参数JSON	
        /// </summary>
        public string PageParamJson { get; set; }
        public int? IsTitlePageImage { get; set; }

        /// <summary>
        /// 摘要
        /// </summary>
        public string Abstract { get; set; }

    }
}
