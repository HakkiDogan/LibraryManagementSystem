using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class Punishment
    {
        public int Id { get; set; }
        public int MemberId { get; set; }
        public int BookTransactionId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime FinishDate { get; set; }
        public decimal Money { get; set; }
        public Member Member { get; set; }
        public BookTransaction BookTransaction { get; set; }
    }
}
