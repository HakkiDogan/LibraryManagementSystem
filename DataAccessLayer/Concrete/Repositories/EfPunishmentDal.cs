﻿using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete.EntityFramework;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Concrete.Repositories
{
	public class EfPunishmentDal : EfEntityRepositoryBase<Punishment, Context>, IPunishmentDal
	{
	}
}
