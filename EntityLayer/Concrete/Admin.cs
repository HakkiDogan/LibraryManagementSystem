﻿using EntityLayer.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
	public class Admin : IEntity
	{
		public int AdminID { get; set; }
		public string Email { get; set; }
		public string Password { get; set; }
	}
}
