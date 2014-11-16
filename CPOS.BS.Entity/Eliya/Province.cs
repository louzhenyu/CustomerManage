using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.BS.Entity.Eliya
{
    public class Province
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public List<City> Citys { get; set; }
    }
}
