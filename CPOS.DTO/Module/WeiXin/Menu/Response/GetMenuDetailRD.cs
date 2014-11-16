using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.CPOS.DTO.Base;

namespace JIT.CPOS.DTO.Module.WeiXin.Menu.Response
{
    public class GetMenuDetailRD : IAPIResponseData
    {
        public MenuDetailInfo[] MenuList { get; set; }
    }

    public class MenuDetailInfo
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
        public int DisplayColumn { get; set; }
        /// <summary>
        /// 状态, 1=启用，0=停用	
        /// </summary>
        public int Status { get; set; }
        /// <summary>
        /// 微信帐号
        /// </summary>
        public string WeiXinId { get; set; }
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
        public string ImageId { get; set; }
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
        public string PageId { get; set; }
        /// <summary>
        /// 参数信息数组的JSON字符串
        /// </summary>
        public string PageParamJson { get; set; }

        public string PageUrlJson { get; set; }

        public string MessageType { get; set; }
    }
   
    public class MaterialTextIdInfo
    {
        public string TestId { get; set; }

        public int DisplayIndex { get; set; }

        public string Title { get; set; }
        public string ImageUrl { get; set; }

        public string Text { get; set; }
        public string OriginalUrl { get; set; }
        public string Author { get; set; }
    }
}
