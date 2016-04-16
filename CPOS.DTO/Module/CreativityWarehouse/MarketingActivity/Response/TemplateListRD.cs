using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.CPOS.DTO.Base;
using JIT.CPOS.BS.Entity;
namespace JIT.CPOS.DTO.Module.CreativityWarehouse.MarketingActivity.Response
{
    public class TemplateListRD : IAPIResponseData
    {
        public List<BannerInfo> BannerList { get; set; }
        public List<TemplateInfo> TemplateList { get; set; }
        public List<PlanInfo> PlanList { get; set; }
        public string PlanImageUrl { get; set; }

    }
    /// <summary>
    /// Banner信息
    /// </summary>
    public class BannerInfo
    {
        public string AdId { get; set; }
        public string ActivityGroupCode { get; set; }
        public string ActivityGroupId { get; set; }
        public string TemplateId { get; set; }
        public string ImageURL { get; set; }
        public string BannerUrl { get; set; }
        public string BannerName { get; set; }
        public int DisplayIndex { get; set; }
        public string Status { get; set; }
        public string QRCodeUrl { get; set; }
    }
    /// <summary>
    /// 主题模版信息
    /// </summary>
    public class TemplateInfo
    {
        public string TemplateId { get; set; }
        /// <summary>
        /// 主题模版名称
        /// </summary>
        public string TemplateName { get; set; }
        /// <summary>
        /// 主题模版图片
        /// </summary>
        public string ImageURL { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public string TemplateStatus { get; set; }
        /// <summary>
        /// 主题类型code
        /// </summary>
        public string ActivityGroupCode { get; set; }
        public string ActivityGroupId { get; set; }
        public string RCodeUrl { get; set; }
    }
    /// <summary>
    /// 全年计划信息
    /// </summary>
    public class PlanInfo
    {
        public string PlanDate { get; set; }
        public string PlanName { get; set; }
        public int DisplayIndex { get; set; }
    }
}
