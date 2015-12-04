using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using JIT.CPOS.Web.ApplicationInterface.Base;
using JIT.CPOS.DTO.Module.Event.Lottery.Request;
using JIT.CPOS.DTO.Module.Event.Lottery.Response;
using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.DTO.Base;
using JIT.Utility.Log;
using JIT.Utility.ExtensionMethod;
using JIT.Utility.DataAccess.Query;
using JIT.CPOS.BS.BLL.WX;


namespace JIT.CPOS.Web.ApplicationInterface.Module.Event.Lottery
{
    public class RouletteAH : BaseActionHandler<LotteryRP, LotteryRD>
    {
        //随机数
        static Random Rnd = new Random();
        protected override LotteryRD ProcessRequest(DTO.Base.APIRequest<LotteryRP> pRequest)
        {
            var rd = new LotteryRD();//返回值

            string result = string.Empty;
            int[] arr = { 67, 112, 202, 292, 337 };
            //定义奖品和概率
            var szZP = new List<JiangPin>{ 
                new JiangPin{id=1,Name="恭喜您抽中的一等奖", gailv=1,zhizhen=157}, 
                new JiangPin{id=2,Name="恭喜您抽中的二等奖", gailv=2,zhizhen=247},
                new JiangPin{id=3,Name="恭喜您抽中的三等奖", gailv=3,zhizhen=22},
                new JiangPin{id=0,Name="很遗憾，这次您未抽中奖", gailv=800,zhizhen=0}
            };
            //模拟一千次抽奖
            Enumerable.Range(1, 2).ToList().ForEach(x =>
            {
                if (ChouJiang(szZP).id == 0)
                {
                    result = "" + arr[Rnd.Next(0, 4)] + "|" + ChouJiang(szZP).Name + "";
                }
                else
                {
                    result = "" + ChouJiang(szZP).zhizhen + "|" + ChouJiang(szZP).Name + "";
                }
            });
            return rd;
        }
        private static JiangPin ChouJiang(List<JiangPin> szZP)
        {
            return (from x in Enumerable.Range(0, 1000000)  //最多随机100万次
                    let sjcp = szZP[Rnd.Next(szZP.Count())]
                    let zgz = Rnd.Next(0, 1000)  //概率按照千分之几计算
                    where zgz < sjcp.gailv
                    select sjcp).First();
        }
        //奖品实体类
        class JiangPin
        {
            public int id;//奖品ID
            public string Name;//奖品名字
            public int gailv;//奖品概率
            public int zhizhen;//奖品指针所致方向
        }
    }
}