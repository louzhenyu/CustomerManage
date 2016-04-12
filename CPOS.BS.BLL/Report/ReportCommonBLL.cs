using CPOS.Common.Core;
using JIT.CPOS.BS.Entity;
//using JIT.CPOS.BS.Entity;
using JIT.CPOS.DTO.Module.Report.StoreManagerReport.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.BS.BLL.Report
{
    /// <summary>
    /// 报表处理 / 公用
    /// </summary>
    public class ReportCommonBLL : LazyInstance<ReportCommonBLL>
    {
        /// <summary>
        /// 按照 时间 顺序,填充    /   使用于天   /  LM,2016/04/01 
        /// </summary>
        public void FillReportDatasByTime<TS, TD>(DateTime start, TS[] sourceList, ref List<TD> destinationList, Func<TS, TD, TD> converter)
        {
            // 时间列表
            var timeList = new List<DateTime>();

            //
            start = Convert.ToDateTime(start.ToString("yyyy-MM-dd"));

            //
            for (var i = 0; i < 7; i++)
            {
                timeList.Add(start.AddDays(i));
            }

            //
            destinationList = Activator.CreateInstance<List<TD>>();

            // sourceList 源无值
            if (sourceList == null || sourceList.Length <= 0)
            {
                foreach (var time in timeList)
                {
                    var dest = Activator.CreateInstance<TD>();
                    GenericHelper.GH.SetPropertyValue(dest, "Date", time.ToString());
                    destinationList.Add(dest);
                }
                return;
            }

            // sourceList 源有值
            foreach (var time in timeList)
            {
                var source = Activator.CreateInstance<TS>();
                source = sourceList.FirstOrDefault(it => GenericHelper.GH.GetPropertyValue<TS, DateTime>(it, "DateCode") == time);

                if (source == null)
                {
                    var dest = Activator.CreateInstance<TD>();
                    GenericHelper.GH.SetPropertyValue(dest, "Date", time.ToString());
                    destinationList.Add(dest);
                }
                else
                {
                    destinationList.Add(converter(source, Activator.CreateInstance<TD>()));
                }
            }
        }

        /// <summary>
        /// 计算增减比
        /// </summary>
        /// <param name="prevValue"></param>
        /// <param name="currValue"></param>
        /// <returns></returns>
        public decimal? CalcuDoD(decimal? prevValue, decimal? currValue)
        {
            if (prevValue == null || prevValue == 0)
            {
                if (currValue == null || currValue == 0)
                {
                    return 0;
                }
                else
                {
                    return 1;
                }
            }
            else
            {
                if (currValue == null || currValue == 0)
                {
                    return -1;
                }
                else
                {
                    return (currValue-prevValue)/prevValue;
                }
            }
        }
    }
}
