using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.BS.DataAccess
{
    public static partial class SqlMap
    {
        #region Orders
        public const string SQL_GETORDERLIST =
            @"    select od.OrdersID,od.IsDelete,od.OrdersNo,od.OrdersTime,od.OrdersType,op.OptionText OrdersTypeName,
                od.StoreID,cu.StoreName,od.ClientID,ct.ClientName,od.OrdersAmount,od.Status,op1.OptionText StatusName,
                od.clientUserID,cr.Name operater,os1.StatusChangedTime StockUpTime,os2.StatusChangedTime  DeliveryTime 
                 from Orders od 
                left join Options op on op.OptionValue = od.OrdersType and op.OptionName='OrdersType'
                left join Store cu  on cu.StoreID = od.StoreID
                left join Client ct  on od.ClientID = ct.ClientID
                left join Options op1 on od.Status = op1.OptionValue and op1.OptionName ='OrdersStatus'
                left join OrdersStatus os  on od.OrdersID = os.OrdersID and od.Status = os.Status 
                left join ClientUser cr on os.ClientUserID = cr.ClientUserID
                left join OrdersStatus os1 on od.OrdersID = os1.OrdersID and os1.Status =4 --备货日期
                left join OrdersStatus os2 on od.OrdersID = os1.OrdersID and os1.Status =5 --配送日期
                left join ClientStructureUserMapping cm on cm.ClientID = od.ClientID and cm.ClientUserID = od.ClientUserID
    where od.IsDelete=0
            ";
        public const string SQL_DELETEORDER =
            @"
            update Orders set IsDelete =1 
            where OrdersID in ('{0}')
            update OrdersDetail set IsDelete=1
            where OrdersID in ('{0}')
            update OrdersStatus set IsDelete=1
            where OrdersID in ('{0}')
            ";
        /// <summary>
        /// 获取订单商品详情列表 By Yuangxi
        /// </summary>
        public const string SQL_GETORDERDETAILLIST =
            @"  select od.OrdersNo,op.OptionText OrdersTypeName,
 sku.SKUNo,sku.SKUName,ODD.Quantity,ODD.OrdersPrice,TotalAmout = ODD.OrdersPrice * ODD.Quantity 
			 from OrdersDetail ODD
                inner join Orders od on ODD.OrdersID=od.OrdersID and od.IsDelete=0
                inner join SKU sku on ODD.SKUID = sku.SKUID and sku.IsDelete=0
                left join Options op on op.OptionValue = od.OrdersType and op.OptionName='OrdersType' and op.IsDelete=0
                left join Store cu  on cu.StoreID = od.StoreID and cu.IsDelete=0
                left join Client ct  on od.ClientID = ct.ClientID and ct.IsDelete=0
                left join Options op1 on od.Status = op1.OptionValue and op1.OptionName ='OrdersStatus' and op1.IsDelete=0
                left join OrdersStatus os  on od.OrdersID = os.OrdersID and od.Status = os.Status and os.IsDelete=0
                left join ClientUser c3 on od.CreateBy = c3.ClientUserID and c3.IsDelete=0
                left join ClientUser c1 on od.ClientUserID = c1.ClientUserID and c1.IsDelete=0
                left join ClientUser cr on os.ClientUserID = cr.ClientUserID and cr.IsDelete=0
                left join OrdersStatus os1 on od.OrdersID = os1.OrdersID and os1.Status =4  and os1.IsDelete=0 --备货日期
                left join ClientUser cr1 on os1.ClientUserID = cr1.ClientUserID and cr1.IsDelete=0 
                left join OrdersStatus os2 on od.OrdersID = os2.OrdersID and os2.Status =5  and os2.IsDelete=0  --配送日期
                left join ClientUser cr2 on os2.ClientUserID = cr2.ClientUserID and cr2.IsDelete=0 
                left join ClientStructureUserMapping cm on cm.ClientID = od.ClientID and cm.ClientUserID = od.ClientUserID   and cm.IsDelete=0 
    where    od.ClientUserID ='{0}' and od.StoreID='{1}' and DATEDIFF(day,od.OrdersTime,'{2}')=0
            ";
        #endregion
    }
}