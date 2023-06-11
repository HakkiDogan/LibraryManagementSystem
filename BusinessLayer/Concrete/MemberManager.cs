using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
	public class MemberManager : IMemberService
	{
		IMemberDal _memberDal;
		IBookService _bookService;

		public MemberManager(IMemberDal memberDal, IBookService bookService)
		{
			_memberDal = memberDal;
			_bookService = bookService;
		}

		public void AddMember(Member member)
		{
			_memberDal.Add(member);
		}

		public List<Member> GetAll()
		{
			return _memberDal.GetAll().Where(w => w.IsDeleted == false).ToList();
		}

		public Member GetById(int id)
		{
			return _memberDal.Get(c => c.MemberId == id);
		}

		public void RemoveMember(int id)
		{
			var member = GetById(id);
			if (member != null)
			{
				member.IsDeleted = true;
				_memberDal.Update(member);
			}
		}

		public void UpdateMember(Member member)
		{
			_memberDal.Update(member);
		}
	}
}
