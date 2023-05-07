using EntityLayer.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class BookTransaction : IEntity
    {
        public int BookTransactionId { get; set; }
        public int BookId { get; set; }
        public int MemberId { get; set; }
        public int StaffId { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime? RetrunDate { get; set; }
        public Book Book { get; set; }
        public Member Member { get; set; }
        public Staff Staff { get; set;}
    }
}
