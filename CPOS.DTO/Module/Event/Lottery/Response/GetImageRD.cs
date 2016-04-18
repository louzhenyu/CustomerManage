using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.CPOS.DTO.Base;
using JIT.CPOS.BS.Entity;

namespace JIT.CPOS.DTO.Module.Event.Lottery.Response
{
    public class GetImageRD : IAPIResponseData
    {
        public string BackGround { get; set; }
        public string BeforeGround { get; set; }
        public string Logo { get; set; }
        public string Rule { get; set; }
        public int RuleId { get; set; }
        public string RuleContent { get; set; }
        public string LT_kvPic { get; set; }
        public string LT_Rule { get; set; }
        public string LT_bgpic1 { get; set; }
        public string LT_bgpic2 { get; set; }
        public string LT_regularpic { get; set; }
        public string EventTitle { get; set; }
        public string EventContent { get; set; }

        public string BootUrl { get; set; }
        public string ShareRemark { get; set; }
        public string PosterImageUrl { get; set; }
        public string OverRemark { get; set; }
        public string ShareLogoUrl { get; set; }
        public int IsShare { get; set; }

        public int Qualification { get; set; }

        public string PrizeName { get; set; } 
        public List<ObjectImagesEntity> ImageList { get; set; }

        public LCoverEntity CoverInfo { get; set; }
        public ButtonInfo Reg { get; set; }
        public ButtonInfo Focus { get; set; }
        public ButtonInfo Share { get; set; }
        /// <summary>
        /// 1:创意仓库 0：一般活动
        /// </summary>
        public int IsCTW { get; set; }
        public string CTWEventId { get; set; }
        public string EventId { get; set; }
    }
    public class ButtonInfo
    {
        public string Text { get; set; }
        public string Color { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string Summary { get; set; }
        /// <summary>
        /// 背景图片
        /// </summary>
        public string BGImageUrl { get; set; }
        /// <summary>
        /// 关注二维码
        /// </summary>
        public string LeadPageQRCodeImageUrl { get; set; }

    }
}
