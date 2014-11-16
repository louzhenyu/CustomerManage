using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.CPOS.DTO.Base;

namespace JIT.CPOS.DTO.Module.Event.EventPrizes.Response
{
    public class GetEventPrizesRD : IAPIResponseData
    {
        public EventPrizesInfo content { get; set; }
        /// <summary>
        /// 是否需要注册【会员】
        /// </summary>
        public int SignFlag { get; set; }

        public string BootUrl { get; set; }
        public string ShareRemark { get; set; }
        public string PosterImageUrl { get; set; }
        public string OverRemark { get; set; }
        public  string ShareLogoUrl { get; set; }
        public int IsShare { get; set; }
    }

    public class EventPrizesInfo
    {
        /// <summary>
        /// 是否在现场 1=是，0=否
        /// </summary>
        public int? IsSite { get; set; }
        /// <summary>
        /// 是否有抽奖机会 1=否 0=是
        /// </summary>
        public int? IsLottery { get; set; }
        /// <summary>
        /// 是否中奖 1=是，0=否
        /// </summary>
        public int? IsWinning { get; set; }
        /// <summary>
        /// 奖品描述
        /// </summary>
        public string WinningDesc { get; set; }
        /// <summary>
        /// 奖品说明
        /// </summary>
        public string WinningExplan { get; set; }
        /// <summary>
        /// 奖品轮次
        /// </summary>
        public string EventRound { get; set; }
        /// <summary>
        /// 顺序
        /// </summary>
        public int? PrizeIndex { get; set; }
        public string Remark { get; set; }
        /// <summary>
        /// 1	该活动抽奖轮次没有启动
        ///2	没有抽奖机会
        ///3	您已抽中奖品
        ///4	抽奖已抽完
        ///5	请先注册
        ///6	请先签到
        ///7	请先验证
        ///8	没有赶到现场不能抽奖
        ///15	红包抢完了
        ///20	活动尚未开始
        ///25	活动已结束
        /// </summary>
        public int Status { get; set; }
        /// <summary>
        /// 用于红包，奖品类型：优惠券，积分，金额
        /// </summary>
        public string PrizesTypeName { get; set; }
        /// <summary>
        /// 奖品名称
        /// </summary>

        public string PrizesName { get; set; }

        public int EventPoints { get; set; }//使用积分抽奖的积分值

        public int PersonPointsLotteryFlag { get; set; } //个人积分抽奖机会 0=没有，1=有
        public int EventPointsLotteryFlag { get; set; } //活动积分抽奖标识  0=没有，1=有
       // public PrizesEntity[] prizes { get; set; }
        public IList<PrizesEntity> PrizesList { get; set; }  //奖品集合
        public IList<WinnerList> WinnerList { get; set; }

    }
    public class PrizesEntity
    {
        public string PrizesID { get; set; }        //奖品标识
        public string PrizeName { get; set; }       //奖品名称
        public string PrizeDesc { get; set; }       //奖品描述
        public string DisplayIndex { get; set; }    //排序
        public string CountTotal { get; set; }      //奖品数量
        public string ImageUrl { get; set; }        //图片
        public string Sponsor { get; set; }         //赞助对应ContentText
    }

    public class WinnerList 
    {
        public string PrizesID { get; set; }        //奖品标识
        public string PrizeName { get; set; }       //奖品名称
        public string PrizeDesc { get; set; }       //奖品描述
        public int DisplayIndex { get; set; }    //排序
        public DateTime Createtime { get; set; }      //中奖时间
        public string ImageUrl { get; set; }        //图片
        public string Sponsor { get; set; }         //赞助对应ContentText
    }


   
}
