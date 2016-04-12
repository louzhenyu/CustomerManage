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
    public class GetUnitBirthdayVipReportAH : BaseActionHandler<UnitBirthdayVipReportRP, UnitBirthdayVipReportRD>
    {
        protected override UnitBirthdayVipReportRD ProcessRequest(DTO.Base.APIRequest<UnitBirthdayVipReportRP> pRequest)
        {
            var rd = new UnitBirthdayVipReportRD();
            var rp = pRequest.Parameters;
            var agg_UnitMonthly_VipCardTypeBLL = new Agg_UnitMonthly_VipCardTypeBLL(CurrentUserInfo);
            var lstMonthVipCardType = default(Agg_UnitMonthly_VipCardTypeEntity[]);

            #region 查询数据
            var tasks = new List<Task>();
            //查询日结报表
            tasks.Add(Task.Factory.StartNew(() =>
            {
                lstMonthVipCardType = agg_UnitMonthly_VipCardTypeBLL.GetEntitiesForVip(rp.CustomerID, rp.UnitID, Convert.ToDateTime(rp.Date));
            }));
            //查询数据库
            Task.WaitAll(tasks.ToArray());
            #endregion

            #region 当月生日会员数量（按会员卡等级）
            //有会员等级的数量
            var lstMonthBirthdayVIPCount = lstMonthVipCardType.Select(p => new UnitVipCountByVipCardLevel()
            {
                VipCardLevelName = p.VipCardTypeName,//会员卡等级名称
                VipCount = p.BirthdayVipCount//当月生日会员数量
            }).ToList();

            //合计行并返回值
            rd.UnitCurrentMonthBirthdayVipCountList = UnitVipCountByVipCardLevel.TotalAllLevel(lstMonthBirthdayVIPCount);
            #endregion

            #region 当月生日未回店会员数量（按会员卡等级）
            //有会员等级的数量
            var lstMonthBirthdayVIPNoBackCount = lstMonthVipCardType.Select(p => new UnitVipCountByVipCardLevel()
            {
                VipCardLevelName = p.VipCardTypeName,//会员卡等级名称
                VipCount = p.BirthdayNoBackVipCount//当月生日未回店会员数量
            }).ToList();

            //合计行并返回值
            rd.UnitCurrentMonthBirthdayVipNoBackCountList = UnitVipCountByVipCardLevel.TotalAllLevel(lstMonthBirthdayVIPNoBackCount);
            #endregion

            return rd;
        }
    }
}