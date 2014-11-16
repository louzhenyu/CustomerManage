/*
 * Author		:Alex.Tian
 * EMail		:changjian.tian@jitmarketing.cn
 * Company		:JIT
 * Create On	:2014/4/18 11:43:00
 * Description	:
 * 1st Modified On	:
 * 1st Modified By	:
 * 1st Modified Desc:
 * 1st Modifier EMail	:
 * 2st Modified On	:
 * 2st Modified By	:
 * 2st Modifier EMail	:
 * 2st Modified Desc:
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using JIT.CPOS.Web.ApplicationInterface.Base;
using JIT.CPOS.DTO.Module.Order.Delivery.Request;
using JIT.CPOS.DTO.Module.Order.Delivery.Response;
using JIT.Utility.ExtensionMethod;
using JIT.CPOS.DTO.Base;

namespace JIT.CPOS.Web.ApplicationInterface.Module.Order.Delivery
{
    public class GetOrderDeliveryTimeRangeAH:BaseActionHandler<GetOrderDeliveryTimeRangeRP,GetOrderDeliveryTimeRangeRD>
    {
        protected override GetOrderDeliveryTimeRangeRD ProcessRequest(APIRequest<GetOrderDeliveryTimeRangeRP> pRequest)
        {
            GetOrderDeliveryTimeRangeRD rd = new GetOrderDeliveryTimeRangeRD();
            //定义变量，取当前时间
            var now = DateTime.Now;
            //当前小时
            var startPoint = now.Hour;

            //如果当前分钟大于0。则从下一小时开始取
            if (now.Minute > 0)
            {
                startPoint = startPoint + 1;
            }
            //开始时间
            DateTime begin = DateTime.Now;

            //结束时间
            DateTime end = DateTime.Now;
            //当前小时在0-9,不在工作时间范围的，统一从11点开始
            if (startPoint <= 9)
            {//早上9点之前
                begin = now.Date.AddHours(11);  //开始时间从11点开始
            }
            //当前小时在19点到凌晨24点的时间，开始时间统一从第二天早上11点开始
            else if (startPoint >= 19)
            {
                begin = now.Date.AddDays(1).AddHours(11);//晚上19：00之后的，开始时间都从第二天的11点开始
            }
            else
            {
                begin = now.Date.AddHours(startPoint + 2); //除此之外的时间段，开始时间统一在2小小时之后。
            }

            end = begin.Date.AddDays(3).AddHours(21);   //结束时间为起始时间的3天之后的晚上9点
            //获取时间分段
            List<DateInfo> list = new List<DateInfo>();
            var dt1 = new DateInfo();//第一天
            dt1.Date = begin.ToString("yyyy-MM-dd");//第一天的日期部分
            list.Add(dt1);

            List<DateTimeRangeInfo> range1 = new List<DateTimeRangeInfo>();
            var day1Begin = begin; //第一天的开始时间
            var day1End = begin.Date.AddHours(21);//第一天的结束时间
            while (day1Begin <day1End)//当开始时间小于结束时间的时候
            {
                var beginSection = day1Begin; //开始时间等于第一天的开始时间
                var endSection = day1Begin.AddHours(2);//结束时间等于开始时间+2小时 
                if (endSection.Hour <= 21) //当结束时间小于当天晚上21点的时候
                {//时间段的结束时间不超过晚9点的，则2小时一划分
                    DateTimeRangeInfo r1 = new DateTimeRangeInfo();
                    r1.BeginTime = beginSection.ToString("HH:mm");
                    r1.EndTime = endSection.ToString("HH:mm");
                    r1.Desc = string.Format("{0} - {1}",r1.BeginTime,r1.EndTime);
                    range1.Add(r1);
                }
                else
                {//否则结束时间就为晚9点
                    DateTimeRangeInfo r1 = new DateTimeRangeInfo();
                    r1.BeginTime = beginSection.ToString("HH:mm");
                    r1.EndTime = beginSection.Date.AddHours(21).ToString("HH:mm");
                    r1.Desc = string.Format("{0} - {1}", r1.BeginTime, r1.EndTime);
                    range1.Add(r1);
                }
                //设置下一个时间分段的起始时间
                day1Begin = day1Begin.AddHours(2);
            }
            dt1.Ranges = range1.ToArray();

            //第二天
            var dt2 = new DateInfo();
            //日期天加一天。其余小时部分都从早上9点到晚上9点
            dt2.Date = begin.Date.AddDays(1).ToString("yyyy-MM-dd");
            list.Add(dt2);

            dt2.Ranges = new DateTimeRangeInfo[] { 
                new DateTimeRangeInfo(){ BeginTime="9:00",EndTime ="11:00", Desc="9:00 - 11:00"}
                ,new DateTimeRangeInfo(){ BeginTime="11:00",EndTime ="13:00", Desc="11:00 - 13:00"}
                ,new DateTimeRangeInfo(){ BeginTime="13:00",EndTime ="15:00", Desc="13:00 - 15:00"}
                ,new DateTimeRangeInfo(){ BeginTime="15:00",EndTime ="17:00", Desc="15:00 - 17:00"}
                ,new DateTimeRangeInfo(){ BeginTime="17:00",EndTime ="19:00", Desc="17:00 - 19:00"}
                ,new DateTimeRangeInfo(){ BeginTime="19:00",EndTime ="21:00", Desc="19:00 - 21:00"}
            };

            //第三天

            var dt3 = new DateInfo();
            //日期天加二天。其余小时部分都从早上9点到晚上9点
            dt3.Date = begin.Date.AddDays(2).ToString("yyyy-MM-dd");
            list.Add(dt3);

            dt3.Ranges = new DateTimeRangeInfo[] { 
                new DateTimeRangeInfo(){ BeginTime="9:00",EndTime ="11:00", Desc="9:00 - 11:00"}
                ,new DateTimeRangeInfo(){ BeginTime="11:00",EndTime ="13:00", Desc="11:00 - 13:00"}
                ,new DateTimeRangeInfo(){ BeginTime="13:00",EndTime ="15:00", Desc="13:00 - 15:00"}
                ,new DateTimeRangeInfo(){ BeginTime="15:00",EndTime ="17:00", Desc="15:00 - 17:00"}
                ,new DateTimeRangeInfo(){ BeginTime="17:00",EndTime ="19:00", Desc="17:00 - 19:00"}
                ,new DateTimeRangeInfo(){ BeginTime="19:00",EndTime ="21:00", Desc="19:00 - 21:00"}
            };
            //
            rd.DateRange = list.ToArray();
            return rd;
        }
    }
}