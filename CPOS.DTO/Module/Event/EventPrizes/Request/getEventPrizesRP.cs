using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.CPOS.DTO.Base;

namespace JIT.CPOS.DTO.Module.Event.EventPrizes.Request
{
    public class GetEventPrizesRP : IAPIRequestParameter
    {
        public void Validate()
        {
        }
        /// <summary>
        ///  推荐人
        /// </summary>
        public string RecommandId { get; set; }

        public string EventId { get; set; }

        public float Longitude { get; set; }

        public float Latitude { get; set; }

        public int PointsLotteryFlag { get; set; }

    }
}
