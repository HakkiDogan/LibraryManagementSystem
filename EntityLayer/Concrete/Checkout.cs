using EntityLayer.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class Checkout : IEntity
    {
        public int CheckoutId { get; set; }
        public string Month { get; set; }
        public decimal Amount { get; set; }
    }
}
