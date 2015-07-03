using JIT.CPOS.DTO.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.DTO.Module.Basic.Customer.Request
{
    public class SetSalesSettingRP : IAPIRequestParameter
    {
        public void Validate()
        {

        }
        public int SocialSalesType { get; set; }
        public int EnableEmployeeSales { get; set; }
        public double EDistributionPricePer { get; set; }
        public double EOrderCommissionPer { get; set; }
        public int EnableVipSales { get; set; }
        public double VDistributionPricePer { get; set; }
        public double VOrderCommissionPer { get; set; }

        public double GetVipUserOrderPer { get; set; }
    }
}
