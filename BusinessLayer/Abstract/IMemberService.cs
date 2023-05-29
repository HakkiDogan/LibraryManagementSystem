using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
	public interface IMemberService
	{
		List<Member> GetAll();

		void AddMember(Member member);

		void RemoveMember(int id);

		void UpdateMember(Member member);

		Member GetById(int id);
	}
}
