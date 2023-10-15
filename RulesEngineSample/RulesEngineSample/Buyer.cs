using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RulesEngineSample
{
    public class Buyer
    {
        public Buyer()
        {
        }

        public int Id { get; set; }
        public int Age { get; set; }
        public bool Authenticated { get; set; }
    }

    public class VIP
    {
        public VIP()
        {
        }

        public int Id { get; set; }
        public bool IsVIP { get; set; }
    }
    public class Discount
    {
        public Discount()
        {
        }

        public double Value
        {
            get; set;
        }
    }
}
