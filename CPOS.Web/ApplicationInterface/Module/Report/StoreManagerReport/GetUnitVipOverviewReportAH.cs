using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.DTO.Module.Report.StoreManagerReport.Request;
using JIT.CPOS.DTO.Module.Report.StoreManagerReport.Response;
using JIT.CPOS.Web.ApplicationInterface.Base;
using JIT.Utility.DataAccess.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace JIT.CPOS.Web.ApplicationInterface.Module.Report.StoreManagerReport
{
    public class GetUnitVipOverviewReportAH : BaseActionHandler<UnitVipOverviewReportRP, UnitVipOverviewReportRD>
    {
        protected override UnitVipOverviewReportRD ProcessRequest(DTO.Base.APIRequest<UnitVipOverviewReportRP> pRequest)
        {
            var rd = new UnitVipOverviewReportRD();
            var rp = pRequest.Parameters;
            var agg_UnitDaily_VipCardTypeBLL = new Agg_UnitDaily_VipCardTypeBLL(CurrentUserInfo);
            var agg_UnitMonthly_VipCardTypeBLL = new Agg_UnitMonthly_VipCardTypeBLL(CurrentUserInfo);
            var agg_UnitMonthlyBLL = new Agg_UnitMonthlyBLL(CurrentUserInfo);
            var lstDayVipCardType = default(Agg_UnitDaily_VipCardTypeEntity[]);
            var lstMonthVipCardType = default(Agg_UnitMonthly_VipCardTypeEntity[]);
            var lstMonth = default(Agg_UnitMonthlyEntity[]);

            #region 查询数据
            var tasks = new List<Task>();
            //门店日结基础数据-会员等级
            tasks.Add(Task.Factory.StartNew(() =>
            {
                lstDayVipCardType = agg_UnitDaily_VipCardTypeBLL.GetEntitiesForVip(rp.CustomerID, rp.UnitID, Convert.ToDateTime(rp.Date));
            }));

            //门店月结数据-会员等级
            tasks.Add(Task.Factory.StartNew(() =>
            {
                lstMonthVipCardType = agg_UnitMonthly_VipCardTypeBLL.GetEntitiesForVip(rp.CustomerID, rp.UnitID, Convert.ToDateTime(rp.Date));
            }));

            //门店月结数据
            tasks.Add(Task.Factory.StartNew(() =>
            {
                lstMonth = agg_UnitMonthlyBLL.Get1YearEntities(rp.CustomerID, rp.UnitID, Convert.ToDateTime(rp.Date));
            }));

            //查询数据
            Task.WaitAll(tasks.ToArray());
            #endregion

            #region 全部会员数量（按会员卡等级）
            //有会员等级的数量
            var lstVipCount = lstDayVipCardType.Select(p => new UnitVipCountByVipCardLevel()
            {
                VipCardLevelName = p.VipCardTypeName,//会员卡等级名称
                VipCount = p.VipCount//会员数量
            }).ToList();

            //合计行并返回值
            rd.UnitVipCountList = UnitVipCountByVipCardLevel.TotalAllLevel(lstVipCount);
            #endregion

            #region 全部活跃会员数量（按会员卡等级）
            //有会员等级的数量
            var lstActiveVipCount = lstDayVipCardType.Select(p => new UnitVipCountByVipCardLevel()
            {
                VipCardLevelName = p.VipCardTypeName,//会员卡等级名称
                VipCount = p.ActiveVipCount//活跃会员数量
            }).ToList();

            //返回值
            rd.UnitActiveVipCountList = UnitVipCountByVipCardLevel.TotalAllLevel(lstActiveVipCount);
            #endregion

            #region 高价值会员数量（按会员卡等级）
            //有会员等级的数量
            var lstHighValueVipCount = lstDayVipCardType.Select(p => new UnitVipCountByVipCardLevel()
            {
                VipCardLevelName = p.VipCardTypeName,//会员卡等级名称
                VipCount = p.HighValueVipCount//高价值会员数量
            }).ToList();

            //返回值
            rd.UnitHighValueVipCountList = UnitVipCountByVipCardLevel.TotalAllLevel(lstHighValueVipCount);
            #endregion

            #region 门店本月新增会员数量（按会员卡等级）
            //有会员等级的数量
            var lstNewVipCount = lstMonthVipCardType.Select(p => new UnitVipCountByVipCardLevel()
            {
                VipCardLevelName = p.VipCardTypeName,//会员卡等级名称
                VipCount = p.NewVipCount//会员数量
            }).ToList();

            //合计行并返回值
            rd.UnitCurrentMonthNewVipCountList = UnitVipCountByVipCardLevel.TotalAllLevel(lstNewVipCount);
            #endregion

            #region 门店12月新增会员数量
            //门店12月新增会员数量
            var lstMonthNewVipCount = lstMonth.Select(p => new UnitMonthNewVipCount()
            {
                Month = Convert.ToDateTime(p.DateCode).ToString("yyyy-MM"),//月份
                NewVipCount = p.NewVipCount//新增会员数量
            }).ToList();

            //填充空缺数据
            DateTime startMonth = new DateTime(Convert.ToDateTime(rp.Date).Year, Convert.ToDateTime(rp.Date).Month, 1).AddMonths(-11);
            lstMonthNewVipCount = UnitMonthNewVipCount.AddEmptyItem(lstMonthNewVipCount, startMonth, 12);

            //返回值
            rd.Unit12MonthNewVipCountList = lstMonthNewVipCount;
            #endregion

            #region 门店与上月新增会员增减数量
            //本月新增会员增减数量
            UnitMonthNewVipCount CurrentMonthAddVipCount = lstMonthNewVipCount.Where(p => p.Month == Convert.ToDateTime(rp.Date).ToString("yyyy-MM")).FirstOrDefault();
            Int32 nCurrentMonthAddVipCount = (CurrentMonthAddVipCount == null) ? 0 : (Int32)CurrentMonthAddVipCount.NewVipCount;
            //上月新增会员增减数量
            UnitMonthNewVipCount LastMonthAddVipCount = lstMonthNewVipCount.Where(p => p.Month == Convert.ToDateTime(rp.Date).AddMonths(-1).ToString("yyyy-MM")).FirstOrDefault();
            Int32 nLastMonthAddVipCount = (LastMonthAddVipCount == null) ? 0 : (Int32)LastMonthAddVipCount.NewVipCount;
            //门店与上月新增会员增减数量
            rd.UnitCurrentMonthNewVipCountMoM = nCurrentMonthAddVipCount - nLastMonthAddVipCount;
            #endregion

            return rd;
        }
    }
}