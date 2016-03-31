using JIT.CPOS.BS.Entity;
using JIT.CPOS.DTO.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.DTO.Module.Order.Delivery.Response
{
    public class GetPickingQuantumRD : IAPIResponseData
    {
        public List<QuantumInfo> QuantumList{get;set;}
    }

    public class QuantumInfo
    {
        public string Quantum { get; set; }
    }
}
