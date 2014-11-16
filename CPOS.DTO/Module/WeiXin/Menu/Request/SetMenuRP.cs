using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.CPOS.DTO.Base;
using JIT.CPOS.DTO.Module.WeiXin.Menu.Response;

namespace JIT.CPOS.DTO.Module.WeiXin.Menu.Request
{
   public class SetMenuRP : IAPIRequestParameter
    {
        /// <summary>
        /// 标识
        /// </summary>
        public string MenuId { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        public string ParentId { get; set; }
        /// <summary>
        /// 层级
        /// </summary>
        public int Level { get; set; }
        /// <summary>
        /// 排序(第一层横排,其它层竖排)	
        /// </summary>
        public string DisplayColumn { get; set; }
        /// <summary>
        /// 状态, 1=启用，0=停用	
        /// </summary>
        public int Status { get; set; }
        /// <summary>
        /// 微信帐号
        /// </summary>
        public string ApplicationId { get; set; }
        /// <summary>
        /// 文本内容(不超过2048)
        /// </summary>
        public string Text { get; set; }
        /// <summary>
        /// 菜单连接地址
        /// </summary>
        public string MenuUrl { get; set; }
        /// <summary>
        /// 图片连接
        /// </summary>
        public string ImageUrl { get; set; }
        /// <summary>
        /// 关联到类型,1=链接,2=消息,3=系统功能	
        /// </summary>
        public int UnionTypeId { get; set; }
        /// <summary>
        /// 图文素材标识数组
        /// </summary>
        public MaterialTextIdInfo[] MaterialTextIds { get; set; }
        /// <summary>
        /// 模板ID
        /// </summary>
        public Guid? PageId { get; set; }
        /// <summary>
        /// 参数信息数组的JSON字符串
        /// </summary>
        public string PageParamJson { get; set; }

       public string PageUrlJson { get; set; }
  
       public string MessageType { get; set; }
        public void Validate()
        {
            if (DisplayColumn == "" || string.IsNullOrEmpty(DisplayColumn))
            {
                throw new Exception("参数DisplayColumn不能为NULL");
            }
            if (this.Name == null)
            {
                throw new Exception("参数Name不能为NULL");
            }
            if (this.Level.ToString()=="0")
            {
                throw new Exception("参数Level不能为NULL");
            }
            //if (this.Text.Length > 2048)
            //{
            //    throw new Exception("参数Text超出规定长度");
            //}
        }
    }
}
