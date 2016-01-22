/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2015-7-3 10:30:22
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
using System.Data;
using System.Data.SqlClient;
using System.Text;

using JIT.Utility;
using JIT.Utility.Entity;
using JIT.Utility.ExtensionMethod;
using JIT.Utility.DataAccess;
using JIT.Utility.Log;
using JIT.Utility.DataAccess.Query;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.BS.DataAccess.Base;

namespace JIT.CPOS.BS.DataAccess
{

    /// <summary>
    /// ���ݷ��ʣ�  
    /// ��T_SalesReturn�����ݷ����� 
    /// TODO:
    /// 1.ʵ��ICRUDable�ӿ�
    /// 2.ʵ��IQueryable�ӿ�
    /// 3.ʵ��Load����
    /// </summary>
    public partial class T_SalesReturnDAO : Base.BaseCPOSDAO, ICRUDable<T_SalesReturnEntity>, IQueryable<T_SalesReturnEntity>
    {
        /// <summary>
        /// ������ȡ�˻���
        /// </summary>
        /// <param name="SalesReturnNo"></param>
        /// <param name="DeliveryType"></param>
        /// <param name="Status"></param>
        /// <param name="paymentcenterId"></param>
        /// <param name="payId"></param>
        /// <returns></returns>
        public DataSet GetWhereSalesReturnOrder(string SalesReturnNo, int DeliveryType, int Status, string paymentcenterId, string payId)
        {
            StringBuilder StrSql = new StringBuilder();
            StrSql.Append(@"select r.SalesReturnNo as '�˻�����',s.item_name as '��Ʒ����',d.enter_price as '����(Ԫ)',
                           r.Qty as '����',v.VipName as '��Ա',de.DeliveryName as '���ͷ�ʽ',r.CreateTime as '��������',
                            case r.[Status] when 1 then '�����' when 2 then'ȡ������' when 3 then'��˲�ͨ��'
                            when 4 then'���ջ�(���ͨ��)' when 5 then'�ܾ��ջ�' when 6 then'�����(���˿�)' when 7 then'�����(���˿�)' else '' end as '״̬',
                            i.paymentcenter_id as '�̻�����',p.Payment_Type_Name as '֧����ʽ' 
                            from T_SalesReturn as r 
                            left join t_inout_detail as d on r.OrderID=d.order_id and d.sku_id=r.skuID 
                            left join vw_sku as s on r.skuid=s.sku_id 
                            left join vip as v on r.vipid=v.vipid and v.IsDelete=0 
                            left join Delivery as de on r.DeliveryType=de.DeliveryId and de.IsDelete=0 
                            left join T_Inout as i on r.OrderID=i.order_id 
                            left join T_Payment_Type as p on i.pay_id=p.Payment_Type_Id and p.IsDelete=0 
                            where r.isdelete=0 ");
            //�̻�ID
            StrSql.AppendFormat("and r.CustomerID='{0}' ",CurrentUserInfo.ClientID);
            //����
            if (!string.IsNullOrWhiteSpace(SalesReturnNo))
                StrSql.AppendFormat("and r.SalesReturnNo='{0}' ", SalesReturnNo);
            if (DeliveryType > 0)
                StrSql.AppendFormat("and r.DeliveryType={0}", DeliveryType);
            if (Status < 8 && Status > 0)
                StrSql.AppendFormat("and r.[Status]={0}", Status);
            if (Status == 8)
                StrSql.Append("and (r.[Status]=6 or r.[Status]=7) ");
            if (!string.IsNullOrWhiteSpace(paymentcenterId))
                StrSql.AppendFormat("and i.paymentcenter_id='{0}' ", paymentcenterId);
            if (!string.IsNullOrWhiteSpace(payId))
                StrSql.AppendFormat("and i.pay_id='{0}' ", payId);
            //����
            StrSql.Append("order by r.CreateTime desc");
            return this.SQLHelper.ExecuteDataset(StrSql.ToString());
        }
    }
}
