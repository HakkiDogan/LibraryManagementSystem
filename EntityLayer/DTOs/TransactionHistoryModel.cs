using EntityLayer.Abstract;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.DTOs
{
	public class TransactionHistoryModel :IDTO
	{
		public int BookTransactionId { get; set; }
		public int MemberId { get; set; }
		public string BookName { get; set; }
		public string MemberName { get; set; }
		public string StaffName { get; set; }
		public DateTime IssueDate { get; set; }
		public DateTime? MemberReturnDate { get; set; }
		public decimal Money { get; set; }

	}
}
