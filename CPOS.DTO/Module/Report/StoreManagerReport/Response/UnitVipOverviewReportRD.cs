using JIT.CPOS.DTO.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.DTO.Module.Report.StoreManagerReport.Response
{
    public class UnitVipOverviewReportRD : IAPIResponseData
    {
        /// <summary>
        /// 全部会员数量（按会员卡等级）
        /// </summary>
        public List<UnitVipCountByVipCardLevel> UnitVipCountList { get; set; }

        /// <summary>
        /// 全部活跃会员数量（按会员卡等级）
        /// </summary>
        public List<UnitVipCountByVipCardLevel> UnitActiveVipCountList { get; set; }

        /// <summary>
        /// 高价值活跃会员数量（按会员卡等级）
        /// </summary>
        public List<UnitVipCountByVipCardLevel> UnitHighValueVipCountList { get; set; }

        /// <summary>
        /// 门店本月新增会员数量（按会员卡等级）
        /// </summary>
        public List<UnitVipCountByVipCardLevel> UnitCurrentMonthNewVipCountList { get; set; }

        /// <summary>
        /// 门店与上月新增会员增减数量
        /// </summary>
        public Int32 UnitCurrentMonthNewVipCountMoM { get; set; }

        /// <summary>
        /// 门店12月新增会员数量
        /// </summary>
        public List<UnitMonthNewVipCount> Unit12MonthNewVipCountList { get; set; }
    }

    /// <summary>
    /// 会员数量（按会员卡等级）
    /// </summary>
    public class UnitVipCountByVipCardLevel
    {
        /// <summary>
        /// 会员卡等级名称，为空是表示所有等级合计
        /// </summary>
        public string VipCardLevelName { get; set; }

        /// 会员数量
        /// </summary>
        public Int32? VipCount { get; set; }

        /// <summary>
        /// 合计所有的等级到合计行
        /// </summary>
        public static List<UnitVipCountByVipCardLevel> TotalAllLevel(List<UnitVipCountByVipCardLevel> lst)
        {
            //合计数量
            if (lst.Count > 0)
            {
                lst.Insert(0, new UnitVipCountByVipCardLevel()
                {
                    VipCardLevelName = "",
                    VipCount = lst.Sum(p => p.VipCount)
                });
            }

            return lst;
        }
    }

    /// <summary>
    /// 门店月新增加会员数量
    /// </summary>
    public class UnitMonthNewVipCount
    {
        /// <summary>
        /// 日期，yyyy-MM
        /// </summary>
        public string Month { get; set; }

        /// 门店新增加会员数量
        /// </summary>
        public Int32? NewVipCount { get; set; }

        /// <summary>
        /// 填充空白数据
        /// </summary>
        /// <param name="lst"></param>
        /// <returns></returns>
        public static List<UnitMonthNewVipCount> AddEmptyItem(List<UnitMonthNewVipCount> lst,DateTime startMonth,Int32 monthNumber)
        {
            for (Int32 i = 0; i < monthNumber;i++ )
            {
                string sMonth = startMonth.AddMonths(i).ToString("yyyy-MM");
                UnitMonthNewVipCount item = lst.Where(p => p.Month == sMonth).FirstOrDefault();
                if(item == null)
                {
                    lst.Add(new UnitMonthNewVipCount()
                    {
                        Month = sMonth,
                        NewVipCount = 0
                    });
                }
            }

            lst = lst.OrderBy(p => p.Month).ToList();

            return lst;
        }
    }
}
