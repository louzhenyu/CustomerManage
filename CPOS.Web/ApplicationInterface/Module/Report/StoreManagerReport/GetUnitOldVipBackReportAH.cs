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
    public class GetUnitOldVipBackReportAH : BaseActionHandler<UnitOldVipBackReportRP, UnitOldVipBackReportRD>
    {
        protected override UnitOldVipBackReportRD ProcessRequest(DTO.Base.APIRequest<UnitOldVipBackReportRP> pRequest)
        {
            var rd = new UnitOldVipBackReportRD();
            var rp = pRequest.Parameters;
            var agg_UnitMonthly_VipCardTypeBLL = new Agg_UnitMonthly_VipCardTypeBLL(CurrentUserInfo);
            var agg_UnitYearly_VipCardTypeBLL = new Agg_UnitYearly_VipCardTypeBLL(CurrentUserInfo);
            var lstMonthVipCardType = default(Agg_UnitMonthly_VipCardTypeEntity[]);
            var lstYearVipCardType = default(Agg_UnitYearly_VipCardTypeEntity[]);

            #region 查询数据
            var tasks = new List<Task>();
            //查询日结报表
            tasks.Add(Task.Factory.StartNew(() =>
            {
                lstMonthVipCardType = agg_UnitMonthly_VipCardTypeBLL.GetEntitiesForVip(rp.CustomerID, rp.UnitID, Convert.ToDateTime(rp.Date));
            }));
            //查询七天销售额
            tasks.Add(Task.Factory.StartNew(() =>
            {
                lstYearVipCardType = agg_UnitYearly_VipCardTypeBLL.GetEntitiesForVip(rp.CustomerID, rp.UnitID, Convert.ToDateTime(rp.Date));
            }));
            //查询数据库
            Task.WaitAll(tasks.ToArray());
            #endregion 

            #region 当月复购会员数量（按会员卡等级）
            //有会员等级的数量
            var lstMonthOldBackCount = lstMonthVipCardType.Select(p => new UnitVipCountByVipCardLevel()
            {
                VipCardLevelName = p.VipCardTypeName,//会员卡等级名称
                VipCount = p.OldVipBackCount//当月复购会员数量
            }).ToList();

            //合计行并返回值
            rd.UnitCurrentMonthOldVipBackCountList = UnitVipCountByVipCardLevel.TotalAllLevel(lstMonthOldBackCount);
            #endregion

            #region 当年复购会员数量（按会员卡等级）
            //有会员等级的数量
            var lstYearOldBackCount = lstYearVipCardType.Select(p => new UnitVipCountByVipCardLevel()
            {
                VipCardLevelName = p.VipCardTypeName,//会员卡等级名称
                VipCount = p.OldVipBackCount//当月复购会员数量
            }).ToList();

            //合计行并返回值
            rd.UnitCurrentYearOldVipBackCountList = UnitVipCountByVipCardLevel.TotalAllLevel(lstYearOldBackCount);
            #endregion

            return rd;
        }
    }
}