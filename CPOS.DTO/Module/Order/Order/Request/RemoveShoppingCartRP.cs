using JIT.CPOS.DTO.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.DTO.Module.Order.Order.Request
{
    public class RemoveShoppingCartRP : IAPIRequestParameter
    {
        /// <summary>
        /// 购物车ID
        /// </summary>
        public string ShoppingCardId { get; set; }
        public  void Validate()
        {
        }
    }
}
