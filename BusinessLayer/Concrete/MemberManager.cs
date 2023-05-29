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

		public MemberManager(IMemberDal memberDal)
		{
			_memberDal = memberDal;
		}

		public void AddMember(Member member)
		{
			member.IsActive = true;
			_memberDal.Add(member);
		}

		public List<Member> GetAll()
		{
			return _memberDal.GetAll().Where(w => w.IsActive).ToList();
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
				member.IsActive = false;
				_memberDal.Update(member);
			}
		}

		public void UpdateMember(Member member)
		{
			member.IsActive = true;
			_memberDal.Update(member);
		}
	}
}
