using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.BS.Entity.Interface
{
    public class ShakeOffLotteryResult
    {
        /// <summary>
        /// 返回中奖号码 1=中奖0=不中奖
        /// </summary>
        public string result_code { get; set; }
        /// <summary>
        /// 返回消息
        /// </summary>
        public string result_message { get; set; }
        /// <summary>
        /// 返回消息2
        /// </summary>
        public string result_message2 { get; set; }
        /// <summary>
        /// 已经中奖数量
        /// </summary>
        public int has_prize_left { get; set; }
        /// <summary>
        /// 中奖名称
        /// </summary>
        public string resultPrizeName { get; set; }
    }
}
