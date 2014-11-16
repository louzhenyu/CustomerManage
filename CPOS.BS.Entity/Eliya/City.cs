using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.BS.Entity.Eliya
{
    public class City
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public List<District> Districts { get; set; }
    }
}
