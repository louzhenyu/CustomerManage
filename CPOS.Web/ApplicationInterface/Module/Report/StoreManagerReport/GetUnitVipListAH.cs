using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.DTO.Module.Report.StoreManagerReport.Request;
using JIT.CPOS.DTO.Module.Report.StoreManagerReport.Response;
using JIT.CPOS.Web.ApplicationInterface.Base;
using JIT.Utility.DataAccess;
using JIT.Utility.DataAccess.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace JIT.CPOS.Web.ApplicationInterface.Module.Report.StoreManagerReport
{
    public class GetUnitVipListAH : BaseActionHandler<UnitVipListRP, UnitVipListRD>
    {
        protected override UnitVipListRD ProcessRequest(DTO.Base.APIRequest<UnitVipListRP> pRequest)
        {
            var rd = new UnitVipListRD();
            var rp = pRequest.Parameters;
            var agg_UnitPotentialVipBLL = new Agg_UnitPotentialVipBLL(CurrentUserInfo);
            var agg_Unit3MonthNoBackVipBLL = new Agg_Unit3MonthNoBackVipBLL(CurrentUserInfo);
            var agg_UnitBirthdayNoBackVipBLL = new Agg_UnitBirthdayNoBackVipBLL(CurrentUserInfo);
            var vipBLL = new VipBLL(CurrentUserInfo);
            var lstUnitPotentialVip = default(PagedQueryResult<Agg_UnitPotentialVipEntity>);
            var lstUnit3MonthNoBackVip = default(PagedQueryResult<Agg_Unit3MonthNoBackVipEntity>);
            var lstUnitBirthdayNoBackVip = default(PagedQueryResult<Agg_UnitBirthdayNoBackVipEntity>);
            var lstVip = default(PagedQueryResult<VipEntity>);

            #region 查询数据
            var tasks = new List<Task>();
            switch (rp.Type)
            {
                case 1://新增潜力会员列表
                    tasks.Add(Task.Factory.StartNew(() =>
                    {
                        lstUnitPotentialVip = agg_UnitPotentialVipBLL.PagedQueryForVip(rp.CustomerID, rp.UnitID, Convert.ToDateTime(rp.Date), rp.PageIndex, rp.PageSize);
                    }));
                    break;
                case 2://三个月内新增未复购会员列表                    
                    tasks.Add(Task.Factory.StartNew(() =>
                    {
                        lstUnit3MonthNoBackVip = agg_Unit3MonthNoBackVipBLL.PagedQueryForVip(rp.CustomerID, rp.UnitID, Convert.ToDateTime(rp.Date), rp.PageIndex, rp.PageSize);
                    }));
                    break;
                case 3://当月未回店生日会员列表
                    tasks.Add(Task.Factory.StartNew(() =>
                    {
                        lstUnitBirthdayNoBackVip = agg_UnitBirthdayNoBackVipBLL.PagedQueryForVip(rp.CustomerID, rp.UnitID, Convert.ToDateTime(rp.Date), false, rp.PageIndex, rp.PageSize);
                    }));
                    break;
                case 4://店铺会员列表
                    tasks.Add(Task.Factory.StartNew(() =>
                    {
                        lstVip = vipBLL.PagedQueryForCurrentVip(rp.CustomerID, rp.UnitID, rp.PageIndex, rp.PageSize);
                    }));
                    break;            }
            //查询数据库
            Task.WaitAll(tasks.ToArray());
            #endregion

            #region 设置返回
            switch (rp.Type)
            {
                case 1://新增潜力会员列表
                    rd.VipList = lstUnitPotentialVip.Entities.Select(p => new UnitVipList()
                    {
                        Sort = 0,
                        VipName = p.VipName,
                        VipPhone = p.Phone
                    }).ToList();
                    rd.TotalPageCount = lstUnitPotentialVip.PageCount;
                    rd.TotalCount = lstUnitPotentialVip.RowCount;
                    break;
                case 2://三个月内新增未复购会员列表
                    rd.VipList = lstUnit3MonthNoBackVip.Entities.Select(p => new UnitVipList()
                    {
                        Sort = 0,
                        VipName = p.VipName,
                        VipPhone = p.Phone
                    }).ToList();
                    rd.TotalPageCount = lstUnit3MonthNoBackVip.PageCount;
                    rd.TotalCount = lstUnit3MonthNoBackVip.RowCount;
                    break;
                case 3://当月未回店生日会员列表
                    rd.VipList = lstUnitBirthdayNoBackVip.Entities.Select(p => new UnitVipList()
                    {
                        Sort = 0,
                        VipName = p.VipName,
                        VipPhone = p.Phone
                    }).ToList();
                    rd.TotalPageCount = lstUnitBirthdayNoBackVip.PageCount;
                    rd.TotalCount = lstUnitBirthdayNoBackVip.RowCount;
                    break;
                case 4://店铺会员列表
                    rd.VipList = lstVip.Entities.Select(p => new UnitVipList()
                    {
                        Sort = 0,
                        VipName = p.VipName,
                        VipPhone = p.Phone
                    }).ToList();
                    rd.TotalPageCount = lstVip.PageCount;
                    rd.TotalCount = lstVip.RowCount;
                    break;
            }
            //排序
            for (int i = 0; i < rd.VipList.Count; i++) rd.VipList[i].Sort = i + 1;

            #endregion

            return rd;
        }
    }
}